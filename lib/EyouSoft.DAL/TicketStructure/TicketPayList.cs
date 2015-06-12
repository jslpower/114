using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 订单支付信息数据访问层
    /// </summary>
    /// 周文超 2010-10-28 
    public class TicketPayList : DALBase, EyouSoft.IDAL.TicketStructure.ITicketPayList
    {

        private Database _dataBase = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketPayList()
        {
            this._dataBase = base.TicketStore;
        }

        #region SqlString

        private const string Sql_MoneyDetailList_Insert = " INSERT INTO [tbl_TicketMoneyDetailList] ([PayId],[ItemId],[ItemType],[PayType],[CurrCompanyId],[CurrUserId],[TradeNo],[PayNumber],[BatchNo],[PayTime],[PayPrice],[PayState],[IssueTime],[Remark]) VALUES (@PayId,@ItemId,@ItemType,@PayType,@CurrCompanyId,@CurrUserId,@TradeNo,@PayNumber,@BatchNo,@PayTime,@PayPrice,@PayState,@IssueTime,@Remark) ; ";

        private const string Sql_TicketPay_Select = " SELECT [PayId],[ItemId],[ItemType],[PayType],[CurrCompanyId],[CurrUserId],[TradeNo],[PayNumber],[BatchNo],[PayTime],[PayPrice],[PayState],[IssueTime],[Remark] FROM [tbl_TicketPay] where 1 = 1 ";

        #endregion

        #region ITicketPayList 成员

        /// <summary>
        /// 添加机票系统-支付明细记录
        /// </summary>
        /// <param name="model">添加机票系统支付明细实体</param>
        /// <returns>返回true表示成功</returns>
        public virtual bool AddTicketPay(EyouSoft.Model.TicketStructure.TicketPay model)
        {
            if (model == null)
                return false;

            bool IsResult = false;

            model.PayId = Guid.NewGuid().ToString();

            DbCommand dc = this._dataBase.GetStoredProcCommand("proc_TicketPay_Add");
            this._dataBase.AddInParameter(dc, "PayId", DbType.AnsiStringFixedLength, model.PayId);
            this._dataBase.AddInParameter(dc, "ItemId", DbType.AnsiStringFixedLength, model.ItemId);
            this._dataBase.AddInParameter(dc, "ItemType", DbType.Byte, (int)model.ItemType);
            this._dataBase.AddInParameter(dc, "PayType", DbType.Byte, (int)model.PayType);
            this._dataBase.AddInParameter(dc, "CurrCompanyId", DbType.AnsiStringFixedLength, model.CurrCompanyId);
            this._dataBase.AddInParameter(dc, "CurrUserId", DbType.AnsiStringFixedLength, model.CurrUserId);
            this._dataBase.AddInParameter(dc, "TradeNo", DbType.String, model.TradeNo.Trim());
            this._dataBase.AddInParameter(dc, "PayPrice", DbType.Decimal, model.PayPrice);
            this._dataBase.AddInParameter(dc, "PayState", DbType.Byte, (int)model.PayState);
            this._dataBase.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._dataBase.AddOutParameter(dc, "BatchNo", DbType.String, 50);

            DbHelper.RunProcedure(dc, this._dataBase);

            object obj = this._dataBase.GetParameterValue(dc, "BatchNo");
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                model.BatchNo = obj.ToString();
                IsResult = true;
            }

            return IsResult;
        }

        /// <summary>
        /// 获取机票系统-支付明细记录
        /// </summary>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="TradeNo">提交给支付接口的交易号</param>
        /// <param name="BatchNo">提交给支付接口的批次号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketPay> GetTicketPay(string ItemId
            , EyouSoft.Model.TicketStructure.ItemType? ItemType, string TradeNo, string BatchNo)
        {
            IList<EyouSoft.Model.TicketStructure.TicketPay> list = new List<EyouSoft.Model.TicketStructure.TicketPay>();
            StringBuilder strSql = new StringBuilder(Sql_TicketPay_Select);
            if (!string.IsNullOrEmpty(ItemId))
                strSql.AppendFormat(" and ItemId = '{0}' ", ItemId);
            if (!string.IsNullOrEmpty(TradeNo))
                strSql.AppendFormat(" and TradeNo = '{0}' ", TradeNo);
            if (!string.IsNullOrEmpty(BatchNo))
                strSql.AppendFormat(" and BatchNo = '{0}' ", BatchNo);
            if (ItemType.HasValue)
                strSql.AppendFormat(" and ItemType = {0} ", (int)ItemType);

            DbCommand dc = this._dataBase.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._dataBase))
            {
                EyouSoft.Model.TicketStructure.TicketPay model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketPay();

                    model.PayId = dr["PayId"].ToString();
                    model.ItemId = dr["ItemId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ItemType")))
                        model.ItemType = (EyouSoft.Model.TicketStructure.ItemType)int.Parse(dr["ItemType"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("PayType")))
                        model.PayType = (EyouSoft.Model.TicketStructure.TicketAccountType)int.Parse(dr["PayType"].ToString());
                    model.CurrCompanyId = dr["CurrCompanyId"].ToString();
                    model.CurrUserId = dr["CurrUserId"].ToString();
                    model.TradeNo = dr["TradeNo"].ToString();
                    model.PayNumber = dr["PayNumber"].ToString();
                    model.BatchNo = dr["BatchNo"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("PayTime")))
                        model.PayTime = DateTime.Parse(dr["PayTime"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("PayPrice")))
                        model.PayPrice = decimal.Parse(dr["PayPrice"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("PayState")))
                        model.PayState = (EyouSoft.Model.TicketStructure.PayState)decimal.Parse(dr["PayState"].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                        model.IssueTime = DateTime.Parse(dr["IssueTime"].ToString());
                    model.Remark = dr["Remark"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 支付回调函数
        /// </summary>
        /// <param name="PayNumber">支付接口返回的交易号</param>
        /// <param name="PayState">交易状态</param>
        /// <param name="Remark">备注（更新后的备注=原来的+现在的）</param>
        /// <param name="TradeNo">提交到支付接口的交易号</param>
        /// <param name="BatchNo">批次号（没有批次号null）</param>
        /// <param name="PayType">支付接口类型</param>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="CurrCompanyId">当前操作公司编号</param>
        /// <param name="CurrUserId">当前操作用户编号</param>
        /// <returns>返回true表示成功</returns>
        public virtual bool PayCallback(string PayNumber, EyouSoft.Model.TicketStructure.PayState PayState, string Remark,
            string TradeNo, string BatchNo, EyouSoft.Model.TicketStructure.TicketAccountType PayType,
            out string ItemId, out EyouSoft.Model.TicketStructure.ItemType? ItemType, out string CurrCompanyId, out string CurrUserId)
        {
            ItemId = string.Empty;
            ItemType = null;
            CurrCompanyId = string.Empty;
            CurrUserId = string.Empty;

            if (string.IsNullOrEmpty(PayNumber) || string.IsNullOrEmpty(TradeNo))
                return false;

            bool IsResult = false;
            DbCommand dc = this._dataBase.GetStoredProcCommand("proc_TicketPay_PayCallback");
            this._dataBase.AddInParameter(dc, "PayNumber", DbType.String, PayNumber);
            this._dataBase.AddInParameter(dc, "PayState", DbType.Byte, (int)PayState);
            this._dataBase.AddInParameter(dc, "Remark", DbType.String, Remark);
            this._dataBase.AddInParameter(dc, "TradeNo", DbType.String, TradeNo);
            this._dataBase.AddInParameter(dc, "BatchNo", DbType.String, BatchNo);
            this._dataBase.AddInParameter(dc, "PayType", DbType.Byte, (int)PayType);

            using (IDataReader dr = DbHelper.RunReaderProcedure(dc, this._dataBase))
            {
                if (dr.Read())
                {
                    ItemId = dr["ItemId"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("ItemType")))
                        ItemType = (EyouSoft.Model.TicketStructure.ItemType)int.Parse(dr["ItemType"].ToString());
                    CurrCompanyId = dr["CurrCompanyId"].ToString();
                    CurrUserId = dr["CurrUserId"].ToString();

                    IsResult = true;
                }
            }

            return IsResult;
        }

        #endregion
    }
}
