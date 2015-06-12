/*Author：汪奇志 2010-11-09*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    /// 财务管理-收款付款登记数据访问
    /// </summary>
    public class FundRegister : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.ToolStructure.IFundRegister
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_Add = "INSERT INTO [tbl_FundRegister]([RegisterId],[RegisterType],[ItemId],[ItemTime],[ContactName],[ItemAmount],[PayType],[IsBilling],[BillingAmount],[InvoiceNo],[Remark],[IsChecked],[OperatorId]) VALUES (@RegisterId,@RegisterType,@ItemId,@ItemTime,@ContactName,@ItemAmount,@PayType,@IsBilling,@BillingAmount,@InvoiceNo,@Remark,@IsChecked,@OperatorId)";
        private const string SQL_DELETE_Delete = "DELETE FROM [tbl_FundRegister] WHERE [RegisterId]=@RegisterId";
        private const string SQL_SELECT_GetInfo = "SELECT * FROM [tbl_FundRegister] WHERE [RegisterId]=@RegisterId";
        private const string SQL_SELECT_GetRegisters = "SELECT * FROM [tbl_FundRegister] WHERE [ItemId]=@ItemId";
        private const string SQL_UPDATE_Update = "UPDATE [tbl_FundRegister] SET [ItemTime]=@ItemTime,[ContactName]=@ContactName,[ItemAmount]=@ItemAmount,[PayType]=@PayType,[IsBilling]=@IsBilling,[BillingAmount]=@BillingAmount,[InvoiceNo]=@InvoiceNo,[Remark]=@Remark,[IsChecked]=@IsChecked WHERE [RegisterId]=@RegisterId";
        private const string SQL_UPDATE_Check = "UPDATE [tbl_FundRegister] SET IsChecked=@IsChecked WHERE RegisterId=@RegisterId";
        #endregion

        #region constructor
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public FundRegister() { this._db = this.SystemStore; }
        #endregion

        #region IFundRegister 成员
        /// <summary>
        /// 添加收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.ToolStructure.FundRegisterInfo register)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_INSERT_Add);
            this._db.AddInParameter(cmd, "RegisterId", DbType.AnsiStringFixedLength, register.RegisterId);
            this._db.AddInParameter(cmd, "RegisterType", DbType.AnsiStringFixedLength, (int)register.RegisterType);
            this._db.AddInParameter(cmd, "ItemId", DbType.AnsiStringFixedLength, register.ItemId);
            this._db.AddInParameter(cmd, "ItemTime", DbType.DateTime, register.ItemTime);
            this._db.AddInParameter(cmd, "ContactName", DbType.String, register.ContactName);
            this._db.AddInParameter(cmd, "ItemAmount", DbType.Decimal, register.ItemAmount);
            this._db.AddInParameter(cmd, "PayType", DbType.Byte, register.PayType);
            this._db.AddInParameter(cmd, "IsBilling", DbType.AnsiStringFixedLength, register.IsBilling ? "1" : "0");
            this._db.AddInParameter(cmd, "BillingAmount", DbType.Decimal, register.BillingAmount);
            this._db.AddInParameter(cmd, "InvoiceNo", DbType.String, register.InvoiceNo);
            this._db.AddInParameter(cmd, "Remark", DbType.String, register.Remark);
            this._db.AddInParameter(cmd, "IsChecked", DbType.AnsiStringFixedLength, register.IsChecked ? "1" : "0");
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, register.OperatorId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 删除收款或付款登记
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <returns></returns>
        public virtual bool Delete(string registerId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_DELETE_Delete);
            this._db.AddInParameter(cmd, "RegisterId", DbType.AnsiStringFixedLength, registerId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取收款或付款登记信息
        /// </summary>
        /// <param name="registerId">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ToolStructure.FundRegisterInfo GetInfo(string registerId)
        {
            EyouSoft.Model.ToolStructure.FundRegisterInfo register = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetInfo);
            this._db.AddInParameter(cmd, "RegisterId", DbType.AnsiStringFixedLength, registerId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    register = new EyouSoft.Model.ToolStructure.FundRegisterInfo();

                    register.BillingAmount = rdr.GetDecimal(rdr.GetOrdinal("BillingAmount"));
                    register.ContactName = rdr["ContactName"].ToString();
                    register.InvoiceNo = rdr["InvoiceNo"].ToString();
                    register.IsBilling = rdr.GetString(rdr.GetOrdinal("IsBilling")) == "1" ? true : false;
                    register.IsChecked = rdr.GetString(rdr.GetOrdinal("IsChecked")) == "1" ? true : false;
                    register.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    register.ItemAmount = rdr.GetDecimal(rdr.GetOrdinal("ItemAmount"));
                    register.ItemId = rdr.GetString(rdr.GetOrdinal("ItemId"));
                    register.ItemTime = rdr.GetDateTime(rdr.GetOrdinal("ItemTime"));
                    register.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    register.PayType = (EyouSoft.Model.ToolStructure.FundPayType)rdr.GetByte(rdr.GetOrdinal("PayType"));
                    register.RegisterId = rdr.GetString(rdr.GetOrdinal("RegisterId"));
                    register.RegisterType = (EyouSoft.Model.ToolStructure.FundRegisterType)int.Parse(rdr.GetString(rdr.GetOrdinal("RegisterType")));
                    register.Remark = rdr["Remark"].ToString();
                }
            }

            return register;
        }

        /// <summary>
        /// 获取收款或付款登记信息集合
        /// </summary>
        /// <param name="itemId">收款或付款登记编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ToolStructure.FundRegisterInfo> GetRegisters(string itemId)
        {
            IList<EyouSoft.Model.ToolStructure.FundRegisterInfo> registers = new List<EyouSoft.Model.ToolStructure.FundRegisterInfo>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetRegisters);
            this._db.AddInParameter(cmd, "ItemId", DbType.AnsiStringFixedLength, itemId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.ToolStructure.FundRegisterInfo register = new EyouSoft.Model.ToolStructure.FundRegisterInfo();

                    register.BillingAmount = rdr.GetDecimal(rdr.GetOrdinal("BillingAmount"));
                    register.ContactName = rdr["ContactName"].ToString();
                    register.InvoiceNo = rdr["InvoiceNo"].ToString();
                    register.IsBilling = rdr.GetString(rdr.GetOrdinal("IsBilling")) == "1" ? true : false;
                    register.IsChecked = rdr.GetString(rdr.GetOrdinal("IsChecked")) == "1" ? true : false;
                    register.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    register.ItemAmount = rdr.GetDecimal(rdr.GetOrdinal("ItemAmount"));
                    register.ItemId = rdr.GetString(rdr.GetOrdinal("ItemId"));
                    register.ItemTime = rdr.GetDateTime(rdr.GetOrdinal("ItemTime"));
                    register.OperatorId = rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    register.PayType = (EyouSoft.Model.ToolStructure.FundPayType)rdr.GetByte(rdr.GetOrdinal("PayType"));
                    register.RegisterId = rdr.GetString(rdr.GetOrdinal("RegisterId"));
                    register.RegisterType = (EyouSoft.Model.ToolStructure.FundRegisterType)int.Parse(rdr.GetString(rdr.GetOrdinal("RegisterType")));
                    register.Remark = rdr["Remark"].ToString();

                    registers.Add(register);
                }
            }

            return registers;
        }

        /// <summary>
        /// 更新收款或付款登记
        /// </summary>
        /// <param name="register">收款付款登记信息业务实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.ToolStructure.FundRegisterInfo register)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_Update);            
            this._db.AddInParameter(cmd, "ItemTime", DbType.DateTime, register.ItemTime);
            this._db.AddInParameter(cmd, "ContactName", DbType.String, register.ContactName);
            this._db.AddInParameter(cmd, "ItemAmount", DbType.Decimal, register.ItemAmount);
            this._db.AddInParameter(cmd, "PayType", DbType.Byte, register.PayType);
            this._db.AddInParameter(cmd, "IsBilling", DbType.AnsiStringFixedLength, register.IsBilling ? "1" : "0");
            this._db.AddInParameter(cmd, "BillingAmount", DbType.Decimal, register.BillingAmount);
            this._db.AddInParameter(cmd, "InvoiceNo", DbType.String, register.InvoiceNo);
            this._db.AddInParameter(cmd, "Remark", DbType.String, register.Remark);
            this._db.AddInParameter(cmd, "IsChecked", DbType.AnsiStringFixedLength, register.IsChecked ? "1" : "0");
            this._db.AddInParameter(cmd, "RegisterId", DbType.AnsiStringFixedLength, register.RegisterId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 收款或付款登记审核
        /// </summary>
        /// <param name="registerId">登记编号</param>
        /// <param name="isChecked">审核状态 true:审核 false:未审核</param>
        /// <returns></returns>
        public virtual bool Check(string registerId, bool isChecked)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_Check);
            this._db.AddInParameter(cmd, "IsChecked", DbType.AnsiStringFixedLength, isChecked ? "1" : "0");
            this._db.AddInParameter(cmd, "RegisterId", DbType.AnsiStringFixedLength, registerId);
            
            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 设置收款(已收/未审核已收)或付款(已付/未审核已付)金额
        /// </summary>
        /// <param name="itemId">收款或付款编号</param>
        /// <param name="registerType">登记类型</param>
        /// <returns></returns>
        public virtual bool SetAmount(string itemId, EyouSoft.Model.ToolStructure.FundRegisterType registerType)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_FundRegister_SetAmount");
            this._db.AddInParameter(cmd, "ItemId", DbType.AnsiStringFixedLength, itemId);
            this._db.AddInParameter(cmd, "RegisterType", DbType.AnsiStringFixedLength, (int)registerType);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }


        public virtual bool TestTran1()
        {
            DbCommand cmd = this.TicketStore.GetStoredProcCommand("proc_test_trans");
            this.TicketStore.AddOutParameter(cmd, "result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, this.TicketStore);

            return Convert.ToInt32(this.TicketStore.GetParameterValue(cmd, "result")) == 1 ? true : false;
        }

        public virtual bool TestTran2()
        {
            DbCommand cmd = this.SystemStore.GetSqlStringCommand("INSERT INTO test_trans([id],[name]) values(1,'SQL0001')");

            return DbHelper.ExecuteSql(cmd, this.SystemStore) == 1 ? true : false;
        }
        #endregion
    }
}
