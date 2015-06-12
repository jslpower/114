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

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 平台机票供应商（采购商）公司账户信息DAL
    /// </summary>
    /// 创建人：luofx 2010-10-28
    public class TicketCompanyAccount : DALBase, IDAL.TicketStructure.ITicketCompanyAccount
    {
        #region private const SQL
        private const string Sql_Update_TicketCompanyAccount = " UPDATE tbl_TicketCompanyAccount SET [AccountNumber]=@AccountNumber,[InterfaceType]=@InterfaceType,[IsSign]=@IsSign WHERE [ID]=@ID AND [CompanyId]=@CompanyId ";
        private const string Sql_Insert_TicketCompanyAccount = " INSERT INTO tbl_TicketCompanyAccount([CompanyId],[AccountNumber],[InterfaceType],[IsSign]) VALUES (@CompanyId,@AccountNumber,@InterfaceType,@IsSign) ";
        private const string Sql_Delete_TicketCompanyAccount = " DELETE FROM tbl_TicketCompanyAccount WHERE [ID]=@ID ";
        private const string Sql_Select_TicketCompanyAccount = " SELECT [id],[CompanyId],[AccountNumber],[InterfaceType],[IsSign] FROM tbl_TicketCompanyAccount WHERE [CompanyId]=@CompanyId ";
        private const string SQL_UPDATE_SetIsSign = "UPDATE [tbl_TicketCompanyAccount] SET [IsSign]=@IsSign WHERE [CompanyId]=@CompanyId AND [InterfaceType]=@AccountType";
        #endregion

        #region constructor
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public TicketCompanyAccount() { this._db = this.TicketStore; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 获取公司账户信息实体(机票平台)
        /// </summary>
        /// <param name="AcountId">帐号Id（主键）</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.TicketCompanyAccount GetModel(int AcountId)
        {
            string StrSql = " SELECT [Id],[CompanyId],[AccountNumber],[InterfaceType],[IsSign] FROM [tbl_TicketCompanyAccount]  WHERE [id]=@AcountId ";
            DbCommand dc = null;
            dc = base.TourStore.GetSqlStringCommand(StrSql);
            base.TourStore.AddInParameter(dc, "AcountId", DbType.Int32, AcountId);
            EyouSoft.Model.TicketStructure.TicketCompanyAccount model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketCompanyAccount();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("InterfaceType")))
                    {
                        model.InterfaceType = (EyouSoft.Model.TicketStructure.TicketAccountType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketAccountType), dr.GetByte(dr.GetOrdinal("InterfaceType")).ToString());
                    }
                    model.IsSign = dr.GetString(dr.GetOrdinal("IsSign"))!="0" ? false : true;
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.AccountNumber = dr.GetString(dr.GetOrdinal("AccountNumber"));                    
                }
            }
            return model;
        }

        /// <summary>
        /// 添加机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        public virtual int Add(EyouSoft.Model.TicketStructure.TicketCompanyAccount model)
        {
            int EffectCount = 0;
            if (model == null) return EffectCount;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Insert_TicketCompanyAccount);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "AccountNumber", DbType.AnsiString, model.AccountNumber);
            this._db.AddInParameter(dc, "InterfaceType", DbType.Byte, (int)model.InterfaceType);
            this._db.AddInParameter(dc, "IsSign", DbType.AnsiStringFixedLength, model.IsSign ? "1" : "0");

            EffectCount = DbHelper.ExecuteSql(dc, this._db);
            return EffectCount;
        }
        /// <summary>
        /// 修改机票公司账户信息
        /// </summary>
        /// <param name="model">平台机票供应商（采购商）公司账户信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        public virtual int Update(EyouSoft.Model.TicketStructure.TicketCompanyAccount model)
        {
            int EffectCount = 0;
            if (model == null) return EffectCount;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Update_TicketCompanyAccount);
            this._db.AddInParameter(dc, "ID", DbType.Int32, model.Id);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "AccountNumber", DbType.AnsiString, model.AccountNumber);
            this._db.AddInParameter(dc, "InterfaceType", DbType.Byte, (int)model.InterfaceType);
            this._db.AddInParameter(dc, "IsSign", DbType.AnsiStringFixedLength, model.IsSign ? "1" : "0");
            EffectCount = DbHelper.ExecuteSql(dc, this._db);
            return EffectCount;
        }
        /// <summary>
        /// 删除公司账户
        /// </summary>
        /// <param name="TicketIds">公司账户信息Id</param>
        /// <returns>大于1：成功，0：失败</returns>
        public virtual int Delete(params int[] TicketIds)
        {
            int EffectCount = 0;
            if (TicketIds == null || TicketIds.Length == 0) return EffectCount;
            DbCommand dc = null;
            if (TicketIds.Length == 1)
            {
                dc = base.TourStore.GetSqlStringCommand(Sql_Delete_TicketCompanyAccount);
                this._db.AddInParameter(dc, "ID", DbType.Int32, TicketIds[0]);
            }
            else
            {
                string StrSql = " DELETE FROM tbl_TicketCompanyAccount WHERE [ID] IN ({0}) ";
                string StrIds = "";
                foreach (int id in TicketIds)
                {
                    StrIds += "'" + id.ToString() + "',";
                }
                StrSql = string.Format(StrSql, StrIds.TrimEnd(','));
                dc = base.TourStore.GetSqlStringCommand(StrSql);
            }
            EffectCount = DbHelper.ExecuteSql(dc, this._db);
            return EffectCount;
        }
        /// <summary>
        /// 根据公司Id获取机票平台的公司账户信息
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns>返回公司账户信息集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> GetList(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId)) return null;
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> ResultList = new List<EyouSoft.Model.TicketStructure.TicketCompanyAccount>();
            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_Select_TicketCompanyAccount);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                EyouSoft.Model.TicketStructure.TicketCompanyAccount model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketCompanyAccount();
                    model.Id = dr.GetInt32(dr.GetOrdinal("id"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("companyid"));
                    model.AccountNumber = dr.GetString(dr.GetOrdinal("AccountNumber"));
                    model.InterfaceType = (EyouSoft.Model.TicketStructure.TicketAccountType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketAccountType), dr.GetByte(dr.GetOrdinal("InterfaceType")).ToString());
                    model.IsSign = dr.GetString(dr.GetOrdinal("IsSign")) == "1" ? true : false;

                    ResultList.Add(model);
                }
                model = null;
            }
            return ResultList;
        }

        /// <summary>
        /// 根据公司编号及支付接口类型设置是否加入支付圈电子协议签约
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="isSign">是否加入支付圈电子协议签约</param>
        /// <returns></returns>
        public virtual bool SetIsSign(string companyId, EyouSoft.Model.TicketStructure.TicketAccountType accountType, bool isSign)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_SetIsSign);
            this._db.AddInParameter(cmd, "IsSign", DbType.AnsiStringFixedLength, isSign ? "1" : "0");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            this._db.AddInParameter(cmd, "AccountType", DbType.Byte, accountType);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }
        #endregion
    }
}
