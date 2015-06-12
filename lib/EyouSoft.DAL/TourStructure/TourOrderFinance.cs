using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.DAL;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 订单财务信息数据访问
    /// </summary>
    /// 周文超 2010-06-22
    public class TourOrderFinance : DALBase, IDAL.TourStructure.ITourOrderFinance
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrderFinance() { }

        #region SqlString

        private const string Sql_TourOrderFinance_Add = " INSERT INTO [tbl_TourOrderFinance] ([Id],[OrderId],[OrderNo],[TourId],[TourNo],[TourClassId],[RouteName],[LeaveDate],[BuyCompanyID],[BuyCompanyName],[PeopleNumber],[ContactName],[ContactTel],[ContactFax],[OperatorID],[OperatorName],[CompanyID],[SumPrice]) VALUES (@Id,@OrderId,@OrderNo,@TourId,@TourNo,@TourClassId,@RouteName,@LeaveDate,@BuyCompanyID,@BuyCompanyName,@PeopleNumber,@ContactName,@ContactTel,@ContactFax,@OperatorID,@OperatorName,@CompanyID,@SumPrice) ";

        private const string Sql_TourOrderFinance_Update = " UPDATE [tbl_TourOrderFinance] SET [PeopleNumber] = @PeopleNumber,[ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactFax] = @ContactFax,[SumPrice] = @SumPrice WHERE [OrderId] = @OrderId  ";

        private const string Sql_TourOrderFinance_UpdateSuccessTime = " UPDATE [tbl_TourOrderFinance] SET [SuccessTime] = @SuccessTime WHERE [OrderId] = @OrderId ";

        #endregion

        #region ITourOrderFinance 成员

        /// <summary>
        /// 新增订单财务信息
        /// </summary>
        /// <param name="model">订单财务业务实体</param>
        /// <returns>返回受印象的行数</returns>
        public virtual int AddTourOrderFinance(EyouSoft.Model.TourStructure.TourOrderFinance model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_TourOrderFinance_Add);

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            base.TourStore.AddInParameter(dc, "OrderId", DbType.String, model.OrderId);
            base.TourStore.AddInParameter(dc, "OrderNo", DbType.String, model.OrderNo);
            base.TourStore.AddInParameter(dc, "TourId", DbType.String, model.TourId);
            base.TourStore.AddInParameter(dc, "TourNo", DbType.String, model.TourNo);
            base.TourStore.AddInParameter(dc, "TourClassId", DbType.Byte, (int)model.TourType);
            base.TourStore.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(dc, "LeaveDate", DbType.DateTime, model.LeaveDate);
            base.TourStore.AddInParameter(dc, "BuyCompanyID", DbType.String, model.BuyCompanyID);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            base.TourStore.AddInParameter(dc, "PeopleNumber", DbType.Int32, model.PeopleNumber);
            base.TourStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.TourStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.TourStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.TourStore.AddInParameter(dc, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            base.TourStore.AddInParameter(dc, "CompanyID", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);

            #endregion

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 根据订单ID修改订单财务信息(只修改钱、联系人、订单人数信息)
        /// </summary>
        /// <param name="model">订单财务业务实体</param>
        /// <returns>返回受印象的行数</returns>
        public virtual int UpdateTourOrderFinance(EyouSoft.Model.TourStructure.TourOrderFinance model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_TourOrderFinance_Update);

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.OrderId);
            base.TourStore.AddInParameter(dc, "PeopleNumber", DbType.Int32, model.PeopleNumber);
            base.TourStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.TourStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.TourStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.TourStore.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);

            #endregion

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 设置订单财务信息的订单成交时间
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="SuccessTime">成交时间</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetSuccessTime(string OrderId, DateTime SuccessTime)
        {
            if (string.IsNullOrEmpty(OrderId))
                return false;

            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_TourOrderFinance_UpdateSuccessTime);

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, OrderId);
            base.TourStore.AddInParameter(dc, "SuccessTime", DbType.DateTime, SuccessTime);

            #endregion

            int Result =  DbHelper.ExecuteSql(dc, base.TourStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
