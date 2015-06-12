using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店系统-结算帐号实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelAccount : DALBase, EyouSoft.IDAL.HotelStructure.IHotelAccount
    {
        #region SQL语句
        private const string Sql_insert_HotelAccount = "INSERT INTO [tbl_HotelAccount]([CompanyId],[AccountType],[Settlement],[AccountName],[BankName],[BankNo],[IsMailInvoice]) VALUES (@CompanyId,@AccountType,@Settlement,@AccountName,@BankName,@BankNo,@IsMailInvoice)";
        private const string Sql_Update_HotelAccount = "UPDATE [tbl_HotelAccount] SET [AccountType]=@AccountType,[Settlement]=@Settlement ,[AccountName]=@AccountName,[BankName]=@BankName,[BankNo]=@BankNo,[IsMailInvoice]=@IsMailInvoice WHERE [ID]=@ID ";
        private const string Sql_Model_HotelAccount = "SELECT [ID],[CompanyId],[AccountType],[Settlement],[AccountName],[BankName],[BankNo],[IsMailInvoice],[IssueTime] FROM [tbl_HotelAccount] WHERE [CompanyId]=@CompanyId";
        #endregion

        private Database _db = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelAccount()
        {
            this._db = base.HotelStore;
        }
        /// <summary>
        /// 添加结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.HotelStructure.HotelAccount model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_insert_HotelAccount);
            this._db.AddInParameter(dc, "AccountName", DbType.String, model.AccountName);
            this._db.AddInParameter(dc, "AccountType", DbType.AnsiStringFixedLength, (int)model.AccountType);
            this._db.AddInParameter(dc, "BankName", DbType.String, model.BankName);
            this._db.AddInParameter(dc, "BankNo", DbType.String, model.BankNo);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "IsMailInvoice", DbType.AnsiStringFixedLength, model.IsMailInvoice ? "1" : "0");           
            this._db.AddInParameter(dc, "Settlement", DbType.AnsiStringFixedLength, (int)model.Settlement);
            int AddCount = DbHelper.ExecuteSql(dc, this._db);
            if (AddCount > 0) IsTrue = true;
            return IsTrue;
        }
        /// <summary>
        /// 修改结算帐号信息
        /// </summary>
        /// <param name="model">结算帐号信息实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.HotelStructure.HotelAccount model)
        {
            bool IsTrue = false;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Update_HotelAccount);
            this._db.AddInParameter(dc, "ID", DbType.String, model.Id);
            this._db.AddInParameter(dc, "AccountName", DbType.String, model.AccountName);
            this._db.AddInParameter(dc, "AccountType", DbType.AnsiStringFixedLength, (int)model.AccountType);
            this._db.AddInParameter(dc, "BankName", DbType.String, model.BankName);
            this._db.AddInParameter(dc, "BankNo", DbType.AnsiStringFixedLength, model.BankNo);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "IsMailInvoice", DbType.AnsiStringFixedLength, model.IsMailInvoice ? "1" : "0");
            this._db.AddInParameter(dc, "Settlement", DbType.AnsiStringFixedLength, (int)model.Settlement);
            int UpdateCount = DbHelper.ExecuteSql(dc, this._db);
            if (UpdateCount > 0) IsTrue = true;
            return IsTrue;
        }
        /// <summary>
        /// 获取结算帐号信息实体
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelAccount GetModel(string CompanyId)
        {
            EyouSoft.Model.HotelStructure.HotelAccount model = null;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Model_HotelAccount);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelAccount();
                    model.AccountName = dr.IsDBNull(dr.GetOrdinal("AccountName")) ? "" : dr.GetString(dr.GetOrdinal("AccountName"));
                    model.AccountType = (EyouSoft.Model.HotelStructure.HotelAccountType)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.HotelAccountType), dr.GetString(dr.GetOrdinal("AccountType")));
                    model.BankName = dr.IsDBNull(dr.GetOrdinal("BankName")) ? "" : dr.GetString(dr.GetOrdinal("BankName"));
                    model.BankNo = dr.GetString(dr.GetOrdinal("BankNo"));
                    model.CompanyId = dr.GetString(dr.GetOrdinal("CompanyId"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IsMailInvoice = dr.GetString(dr.GetOrdinal("IsMailInvoice")) == "1" ? true : false;
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.Settlement = (EyouSoft.Model.HotelStructure.HotelSettlement)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.HotelSettlement), dr.GetString(dr.GetOrdinal("Settlement")));
                }
            }
            return model;
        }
        /// <summary>
        /// 判断账户是否存在
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public virtual bool IsExist(string CompanyId)
        {
            bool IsTrue = false;
            string StrSql = "SELECT 1 FROM tbl_HotelAccount WHERE [CompanyId]=@CompanyId";
            DbCommand dc = dc = this._db.GetSqlStringCommand(StrSql);            
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);           
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    IsTrue = true;
                }
            }
            return IsTrue;
        }
    }
}
