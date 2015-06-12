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

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 订单评价信息数据访问  
    /// </summary>
    /// 周文超 2010-05-11
    public class RateOrder : DALBase, IDAL.TourStructure.IRateOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RateOrder()
        { }

        #region SqlString

        private const string Sql_RateOrder_Add = " INSERT INTO [tbl_RateOrder]([ID],[BuyCompanyId],[BuyCompanyName],[BuyUserId],[BuyUserContactName],[SellCompanyId],[RateType],[ServiceQuality],[PriceQuality],[TravelPlan],[AgreeLevel],[RateScore],[RateContent],[OrderId],[TourId],[RouteName],[SettlePeoplePrice],[SettleChildPrice],[IssueTime],[ExpireTime],[IsSystemRate]) VALUES (@ID,@BuyCompanyId,@BuyCompanyName,@BuyUserId,@BuyUserContactName,@SellCompanyId,@RateType,@ServiceQuality,@PriceQuality,@TravelPlan,@AgreeLevel,@RateScore,@RateContent,@OrderId,@TourId,@RouteName,@SettlePeoplePrice,@SettleChildPrice,@IssueTime,@ExpireTime,@IsSystemRate) ";
        private const string Sql_RateOrder_Update = " UPDATE [tbl_RateOrder]SET [RateType] = @RateType,[ServiceQuality] = @ServiceQuality,[PriceQuality] = @PriceQuality,[TravelPlan] = @TravelPlan,[AgreeLevel] = @AgreeLevel,[RateScore] = @RateScore,[RateContent] = @RateContent,[SettlePeoplePrice] = @SettlePeoplePrice,[SettleChildPrice] = @SettleChildPrice WHERE [ID] = @ID ";
        private const string Sql_RateOrder_Select = " select [ID],[BuyCompanyId],[BuyCompanyName],[BuyUserId],[BuyUserContactName],[SellCompanyId],[RateType],[ServiceQuality],[PriceQuality],[TravelPlan],[AgreeLevel],[RateScore],[RateContent],[OrderId],[TourId],[RouteName],[SettlePeoplePrice],[SettleChildPrice],[IssueTime],[ExpireTime],[IsSystemRate] from [tbl_RateOrder] ";

        #endregion

        #region 函数成员

        /// <summary>
        /// 增加一条订单评价信息
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>受影响行数</returns>
        public virtual int AddRateOrder(EyouSoft.Model.TourStructure.RateOrder model)
        {
            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_RateOrder_Add);

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            base.TourStore.AddInParameter(dc, "BuyCompanyId", DbType.AnsiStringFixedLength, model.BuyCompanyId);
            base.TourStore.AddInParameter(dc, "BuyCompanyName", DbType.String, model.BuyCompanyName);
            base.TourStore.AddInParameter(dc, "BuyUserId", DbType.AnsiStringFixedLength, model.BuyUserId);
            base.TourStore.AddInParameter(dc, "BuyUserContactName", DbType.String, model.BuyUserContactName);
            base.TourStore.AddInParameter(dc, "SellCompanyId", DbType.AnsiStringFixedLength, model.SellCompanyId);
            base.TourStore.AddInParameter(dc, "RateType", DbType.Byte, (int)model.RateType);
            base.TourStore.AddInParameter(dc, "ServiceQuality", DbType.Decimal, model.ServiceQuality);
            base.TourStore.AddInParameter(dc, "PriceQuality", DbType.Decimal, model.PriceQuality);
            base.TourStore.AddInParameter(dc, "TravelPlan", DbType.Decimal, model.TravelPlan);
            base.TourStore.AddInParameter(dc, "AgreeLevel", DbType.Decimal, model.AgreeLevel);
            base.TourStore.AddInParameter(dc, "RateScore", DbType.Decimal, model.RateScore);
            base.TourStore.AddInParameter(dc, "RateContent", DbType.String, model.RateContent);
            base.TourStore.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.OrderId);
            base.TourStore.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            base.TourStore.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(dc, "SettlePeoplePrice", DbType.Decimal, model.SettlePeoplePrice);
            base.TourStore.AddInParameter(dc, "SettleChildPrice", DbType.Decimal, model.SettleChildPrice);
            base.TourStore.AddInParameter(dc, "IssueTime", DbType.DateTime, model.IssueTime);
            base.TourStore.AddInParameter(dc, "ExpireTime", DbType.DateTime, model.ExpireTime);
            base.TourStore.AddInParameter(dc, "IsSystemRate", DbType.AnsiStringFixedLength, model.IsSystemRate ? "1" : "0");

            #endregion

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 更新一条订单评价信息(只更新评价部分,订单、团队、公司、用户以及评价时间不更新)
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateRateOrderById(EyouSoft.Model.TourStructure.RateOrder model)
        {
            DbCommand dc = base.TourStore.GetSqlStringCommand(Sql_RateOrder_Update);

            #region 参数赋值

            base.TourStore.AddInParameter(dc, "RateType", DbType.Byte, (int)model.RateType);
            base.TourStore.AddInParameter(dc, "ServiceQuality", DbType.Decimal, model.ServiceQuality);
            base.TourStore.AddInParameter(dc, "PriceQuality", DbType.Decimal, model.PriceQuality);
            base.TourStore.AddInParameter(dc, "TravelPlan", DbType.Decimal, model.TravelPlan);
            base.TourStore.AddInParameter(dc, "AgreeLevel", DbType.Decimal, model.AgreeLevel);
            base.TourStore.AddInParameter(dc, "RateScore", DbType.Decimal, model.RateScore);
            base.TourStore.AddInParameter(dc, "RateContent", DbType.String, model.RateContent);
            base.TourStore.AddInParameter(dc, "SettlePeoplePrice", DbType.Decimal, model.SettlePeoplePrice);
            base.TourStore.AddInParameter(dc, "SettleChildPrice", DbType.Decimal, model.SettleChildPrice);
            base.TourStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);

            #endregion

            return DbHelper.ExecuteSql(dc, base.TourStore);
        }

        /// <summary>
        /// 获取某一公司作为卖家时所有的订单评价数据
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0:IssueTime升序；1:IssueTime降序</param>
        /// <param name="SellCompanyId">卖家公司ID(必须传值)</param>
        /// <param name="RateType">评价类型(小于等于0不作条件)</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.RateOrder> GetRateOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string SellCompanyId, int RateType)
        {
            IList<EyouSoft.Model.TourStructure.RateOrder> list = new List<EyouSoft.Model.TourStructure.RateOrder>();

            string strFiles = " [ID],[BuyCompanyId],[BuyCompanyName],[BuyUserId],[BuyUserContactName],[SellCompanyId],[RateType],[ServiceQuality],[PriceQuality],[TravelPlan],[AgreeLevel],[RateScore],[RateContent],[OrderId],[TourId],[RouteName],[SettlePeoplePrice],[SettleChildPrice],[IssueTime],[ExpireTime],[IsSystemRate] ";
            string strOrder = string.Empty;
            string strWhere = string.Format(" [SellCompanyId] = '{0}' ", SellCompanyId);

            if (RateType > 0)
                strWhere += string.Format(" and RateType = {0} ", RateType);

            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.TourStore, PageSize, PageIndex, ref RecordCount, "tbl_RateOrder", "[ID]", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.TourStructure.RateOrder model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.RateOrder();
                    model.ID = dr[0].ToString();
                    model.BuyCompanyId = dr[1].ToString();
                    model.BuyCompanyName = dr[2].ToString();
                    model.BuyUserId = dr[3].ToString();
                    model.BuyUserContactName = dr[4].ToString();
                    model.SellCompanyId = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.RateType = (Model.TourStructure.RateType)dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.ServiceQuality = dr.GetDecimal(7);
                    if (!dr.IsDBNull(8))
                        model.PriceQuality = dr.GetDecimal(8);
                    if (!dr.IsDBNull(9))
                        model.TravelPlan = dr.GetDecimal(9);
                    if (!dr.IsDBNull(10))
                        model.AgreeLevel = dr.GetDecimal(10);
                    if (!dr.IsDBNull(11))
                        model.RateScore = dr.GetDecimal(11);
                    model.RateContent = dr[12].ToString();
                    model.OrderId = dr[13].ToString();
                    model.TourId = dr[14].ToString();
                    model.RouteName = dr[15].ToString();
                    if (!dr.IsDBNull(16))
                        model.SettlePeoplePrice = dr.GetDecimal(16);
                    if (!dr.IsDBNull(17))
                        model.SettleChildPrice = dr.GetDecimal(17);
                    if (!dr.IsDBNull(18))
                        model.IssueTime = dr.GetDateTime(18);
                    if (!dr.IsDBNull(19))
                        model.ExpireTime = dr.GetDateTime(19);
                    if (!dr.IsDBNull(20))
                        model.IsSystemRate = dr[20].ToString() == "1" ? true : false;

                    list.Add(model);
                }

                model = null;
            }

            return list;
        }

        #region 私有方法 执行查询命令，返回订单评价实体集合

        /// <summary>
        /// 执行查询命令返回订单评价实体集合
        /// </summary>
        /// <param name="dc">查询命令</param>
        /// <returns>返回订单评价实体集合</returns>
        private IList<EyouSoft.Model.TourStructure.RateOrder> GetQueryList(DbCommand dc)
        {
            IList<EyouSoft.Model.TourStructure.RateOrder> list = new List<EyouSoft.Model.TourStructure.RateOrder>();
            using (IDataReader dr = base.TourStore.ExecuteReader(dc))
            {
                EyouSoft.Model.TourStructure.RateOrder model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TourStructure.RateOrder();
                    model.ID = dr[0].ToString();
                    model.BuyCompanyId = dr[1].ToString();
                    model.BuyCompanyName = dr[2].ToString();
                    model.BuyUserId = dr[3].ToString();
                    model.BuyUserContactName = dr[4].ToString();
                    model.SellCompanyId = dr[5].ToString();
                    if (!dr.IsDBNull(6))
                        model.RateType = (Model.TourStructure.RateType)dr.GetInt32(6);
                    if (!dr.IsDBNull(7))
                        model.ServiceQuality = dr.GetDecimal(7);
                    if (!dr.IsDBNull(8))
                        model.PriceQuality = dr.GetDecimal(8);
                    if (!dr.IsDBNull(9))
                        model.TravelPlan = dr.GetDecimal(9);
                    if (!dr.IsDBNull(10))
                        model.AgreeLevel = dr.GetDecimal(10);
                    if (!dr.IsDBNull(11))
                        model.RateScore = dr.GetDecimal(11);
                    model.RateContent = dr[12].ToString();
                    model.OrderId = dr[13].ToString();
                    model.TourId = dr[14].ToString();
                    model.RouteName = dr[15].ToString();
                    if (!dr.IsDBNull(16))
                        model.SettlePeoplePrice = dr.GetDecimal(16);
                    if (!dr.IsDBNull(17))
                        model.SettleChildPrice = dr.GetDecimal(17);
                    if (!dr.IsDBNull(18))
                        model.IssueTime = dr.GetDateTime(18);
                    if (!dr.IsDBNull(19))
                        model.ExpireTime = dr.GetDateTime(19);
                    if (!dr.IsDBNull(20))
                        model.IsSystemRate = dr[20].ToString() == "1" ? true : false;

                    list.Add(model);
                }

                model = null;
            }
            return list;
        }

        #endregion

        #endregion
    }
}
