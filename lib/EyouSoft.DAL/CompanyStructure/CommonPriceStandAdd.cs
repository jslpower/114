using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：待新增公用价格等级
    /// </summary>
    public class CommonPriceStandAdd : DALBase, ICommonPriceStandAdd
    {
        #region SQL变量
        const string SQL_CommonPriceStandAdd_INSERT = "INSERT INTO tbl_CommonPriceStandAdd([CompanyID],[OperatorID],[priceStandName]) values(@companyid,@operatorid,@pricestandname)";
        const string SQL_CommonPriceStandAdd_SELECT_List = "Select [ID],[CompanyID],[OperatorID],[priceStandName],[IssueTime] from tbl_CommonPriceStandAdd";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CommonPriceStandAdd()
        {
            this._database = base.CompanyStore;
        }

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">待新增公用价格等级实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.CommonPriceStandAdd model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommonPriceStandAdd_INSERT);
            this._database.AddInParameter(dc, "companyid", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "operatorid", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "pricestandname", DbType.String, model.PriceStandName);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CommonPriceStandAdd> GetList()
        {
            IList<EyouSoft.Model.CompanyStructure.CommonPriceStandAdd> list = new List<EyouSoft.Model.CompanyStructure.CommonPriceStandAdd>();
            EyouSoft.Model.CompanyStructure.CommonPriceStandAdd model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CommonPriceStandAdd_SELECT_List);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CommonPriceStandAdd();
                    model.ID = dr.GetInt32(dr.GetOrdinal("ID"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("companyId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("issueTime"));
                    model.OperatorId = dr.GetString(dr.GetOrdinal("operatorId"));
                    model.PriceStandName = dr.GetString(dr.GetOrdinal("priceStandName"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion
    }
}
