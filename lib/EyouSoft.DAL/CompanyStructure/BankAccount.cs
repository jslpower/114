using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 银行帐号信息数据层
    /// </summary>
    /// 创建人：张志瑜 2010-05-31
    public class BankAccount : DALBase, EyouSoft.IDAL.CompanyStructure.IBankAccount
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BankAccount() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL变量        
        /// <summary>
        /// 删除公司所有银行帐号
        /// </summary>
        private const string SQL_BankAccount_DELETE = "DELETE FROM [tbl_BankAccount] WHERE CompanyId=@CompanyId";
        private const string SQL_BankAccount_SELECT = "SELECT [CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId] FROM [tbl_BankAccount] WHERE companyId=@companyId";
        #endregion SQL变量
        /// <summary>
        /// 批量添加银行帐号
        /// </summary>
        /// <param name="models">银行帐号实体列表</param>
        /// <returns></returns>
        public virtual bool Add(IList<EyouSoft.Model.CompanyStructure.BankAccount> models)
        {
            if (models == null || models.Count <= 0)
                return false;
            string CompanyID = models[0].CompanyID;
            StringBuilder strSqlRoot = new StringBuilder("<ROOT>");
            if (models != null && models.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.BankAccount model in models)
                {
                    if (model != null)
                    {
                        strSqlRoot.AppendFormat("<AddBankAccount AccountNumber='{0}' ", model.AccountNumber);
                        strSqlRoot.AppendFormat(" TypeId='{0}' ", Convert.ToInt32(model.AccountType));
                        strSqlRoot.AppendFormat(" BankAccountName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(model.BankAccountName));
                        strSqlRoot.AppendFormat(" BankName='{0}' ", EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(model.BankName));
                        strSqlRoot.Append(" />");
                    }
                }
            }
            strSqlRoot.Append("</ROOT>");

            DbCommand dc = this._database.GetStoredProcCommand("proc_AddBankAccount");
            this._database.AddInParameter(dc, "XMLRoot", DbType.String, strSqlRoot.ToString());
            this._database.AddInParameter(dc, "CompanyID", DbType.String, CompanyID);
            this._database.AddOutParameter(dc, "ReturnValue", DbType.String, 1);

            DbHelper.RunProcedure(dc, this._database);

            object obj = this._database.GetParameterValue(dc, "ReturnValue");

            if (obj != null && Convert.ToString(obj) == "1")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除公司所有银行帐号
        /// </summary>
        /// <param name="companyid">银行帐号所属的公司ID</param>
        /// <returns></returns>
        public virtual bool Delete(string companyid)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_BankAccount_DELETE);
            this._database.AddInParameter(dc, "companyid", DbType.AnsiStringFixedLength, companyid);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得银行帐号信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.BankAccount> GetList(string companyId)
        {
            IList<EyouSoft.Model.CompanyStructure.BankAccount> items = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_BankAccount_SELECT);
            this._database.AddInParameter(dc, "companyId", DbType.AnsiStringFixedLength, companyId);
            using(IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.CompanyStructure.BankAccount item = new EyouSoft.Model.CompanyStructure.BankAccount();
                    item.AccountNumber = rdr.IsDBNull(rdr.GetOrdinal("AccountNumber")) == true ? "" : rdr.GetString(rdr.GetOrdinal("AccountNumber"));
                    item.AccountType = (EyouSoft.Model.CompanyStructure.BankAccountType)rdr.GetByte(rdr.GetOrdinal("TypeId"));
                    item.BankAccountName = rdr.IsDBNull(rdr.GetOrdinal("BankAccountName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("BankAccountName"));
                    item.BankName = rdr.IsDBNull(rdr.GetOrdinal("BankName")) == true ? "" : rdr.GetString(rdr.GetOrdinal("BankName"));
                    item.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
