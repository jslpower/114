using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-公司推荐数据访问类
    /// </summary>
    public class RateHoldUp : DALBase, EyouSoft.IDAL.CreditSystemStructure.IRateHoldUp
    {
        #region IRateHoldUp 成员

        //static constants
        private const string SQL_SELECT_ISRECOMMENDED = "SELECT COUNT(*) AS Amount FROM tbl_RateHoldUp WHERE ToCompanyId=@TOCOMPANYID AND FromCompanyId=@FROMCOMPANYID";
        private const string SQL_SELECT_GETAMOUNT = "SELECT COUNT(*) AS Amount FROM tbl_RateHoldUp WHERE ToCompanyId=@TOCOMPANYID";

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RateHoldUp()
        {
            this._database = base.CompanyStore;
        }


        /// <summary>
        /// 是否已经推荐
        /// </summary>
        /// <param name="toCompanyId">推荐的公司编号</param>
        /// <param name="fromCompanyId">推荐人所在公司编号</param>
        /// <returns></returns>
        public virtual bool IsHoldUp(string toCompanyId, string fromCompanyId)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SELECT_ISRECOMMENDED);

            this._database.AddInParameter(dc, "TOCOMPANYID", DbType.AnsiStringFixedLength, toCompanyId);
            this._database.AddInParameter(dc, "FROMCOMPANYID", DbType.AnsiStringFixedLength, fromCompanyId);
            return DbHelper.Exists(dc, this._database);            
        }

        /// <summary>
        /// 获取指定公司的推荐数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetAmount(string companyId)
        {
            int amount = 0;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SELECT_GETAMOUNT);
            this._database.AddInParameter(dc, "TOCOMPANYID", DbType.AnsiStringFixedLength, companyId);

            object obj = DbHelper.GetSingle(dc, this._database);

            if (obj != null)
            {
                amount = Convert.ToInt32(obj);
            }

            return amount;
        }

        /// <summary>
        /// 获取指定公司的推荐列表
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CreditSystemStructure.RateHoldUp> GetHoldUps(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            IList<EyouSoft.Model.CreditSystemStructure.RateHoldUp> items = new List<EyouSoft.Model.CreditSystemStructure.RateHoldUp>();
            StringBuilder commandText = new StringBuilder();
            string tableName = "tbl_RateHoldUp";
            string primaryKey = "Id ";
            string orderByString = "IssueTime DESC";
            string fields = " ID, FromCompanyId, FromCompanyName, FromUserId, FromUserContactName, ToCompanyId, HoldUpScore, IssueTime ";

            commandText.AppendFormat(" ToCompanyId='{0}' ", companyId.ToString());
            
            using (IDataReader rdr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, commandText.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    items.Add(new EyouSoft.Model.CreditSystemStructure.RateHoldUp(
                        rdr.GetString(rdr.GetOrdinal("ID")),
                        rdr.GetString(rdr.GetOrdinal("FromCompanyId")),
                        rdr.GetString(rdr.GetOrdinal("FromCompanyName")),
                        rdr.GetString(rdr.GetOrdinal("FromUserId")),
                        rdr.GetString(rdr.GetOrdinal("FromUserContactName")),
                        rdr.GetString(rdr.GetOrdinal("ToCompanyId")),
                        (double)rdr.GetInt32(rdr.GetOrdinal("HoldUpScore")),
                        rdr.GetDateTime(rdr.GetOrdinal("IssueTime"))
                        ));
                }
            }

            return items.Count > 0 ? items : null;
        }

        /// <summary>
        /// 插入一条推荐信息,同时更新推荐的公司推荐次数与推荐分值
        /// </summary>
        /// <param name="info">公司推荐业务实体</param>
        /// <returns>失败 成功 已推荐过该公司</returns>
        public virtual EyouSoft.Model.ResultStructure.ResultInfo Insert(EyouSoft.Model.CreditSystemStructure.RateHoldUp info)
        {
            EyouSoft.Model.ResultStructure.ResultInfo result = EyouSoft.Model.ResultStructure.ResultInfo.Error;
            DbCommand dc = this._database.GetStoredProcCommand("proc_RateHoldUp_Insert");
            this._database.AddInParameter(dc, "FromCompanyId", DbType.AnsiStringFixedLength, info.FromCompanyId);
            this._database.AddInParameter(dc, "FromCompanyName", DbType.String, info.FromCompanyName);
            this._database.AddInParameter(dc, "FromUserId", DbType.AnsiStringFixedLength, info.FromUserId);
            this._database.AddInParameter(dc, "FromUserContactName", DbType.String, info.FromUserContactName);
            this._database.AddInParameter(dc, "ToCompanyId", DbType.AnsiStringFixedLength, info.ToCompanyId);
            this._database.AddInParameter(dc, "HoldUpScore", DbType.Double, info.HoldUpScore);
            this._database.AddOutParameter(dc, "result", DbType.Int32, 4);  //0:失败 1:成功 2:已推荐过该公司
            DbHelper.RunProcedure(dc, this._database);
            int flag = Convert.ToInt32(this._database.GetParameterValue(dc, "result"));
            switch (flag)
            { 
                case 0:
                    result = EyouSoft.Model.ResultStructure.ResultInfo.Error;
                    break;
                case 1:
                    result = EyouSoft.Model.ResultStructure.ResultInfo.Succeed;
                    break;
                case 2:
                    result = EyouSoft.Model.ResultStructure.ResultInfo.Exists;
                    break;
            }
            return result;
        }
        #endregion IRateHoldUp 成员
    }
}
