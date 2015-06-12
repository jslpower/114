using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;
using System.Xml.Linq;
namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店订单-业务数据层
    /// </summary>
    /// 鲁功源  2010-12-02
    public class HotelOrder :DALBase,EyouSoft.IDAL.HotelStructure.IHotelOrder
    {
        #region SQL变量
        private const string Sql_HotelOrder_INSERT = "INSERT INTO tbl_HotelOrder([OrderId],[ResOrderId],[HotelCode],[HotelName],[CityCode],[RoomTypeCode],[RoomTypeName],[CheckInDate],[CheckOutDate],[TotalAmount],[CreateDateTime],[OrderState],[BuyerCId],[BuyerCName],[BuyerUId],[BuyerUName],[BuyerUFullname],[ContacterFullname],[ContacterMobile],[ContacterTelephone],[CommissionType],[CommissionFix],[CommissionPercent],[PaymentType],[OrderType],[CheckState],[IsMobileContact],[RatePlanCode],[VendorCode],[Remark]) VALUES(@OrderId,@ResOrderId,@HotelCode,@HotelName,@CityCode,@RoomTypeCode,@RoomTypeName,@CheckInDate,@CheckOutDate,@TotalAmount,@CreateDateTime,@OrderState,@BuyerCId,@BuyerCName,@BuyerUId,@BuyerUName,@BuyerUFullname,@ContacterFullname,@ContacterMobile,@ContacterTelephone,@CommissionType,@CommissionFix,@CommissionPercent,@PaymentType,@OrderType,@CheckState,@IsMobileContact,@RatePlanCode,@VendorCode,@Remark);" +
            "INSERT im_message(src,dst,[message],flag,IssueTime) values (10000,35791,'收到一个酒店订单,请尽快处理',0,getdate()); ";
        private const string Sql_HotelOrder_SETORDERSTATE = "Update tbl_HotelOrder set OrderState=@OrderState where ResOrderId=@ResOrderId";
        private const string Sql_HotelOrder_SETCHECKSTATE = "Update tbl_HotelOrder set CheckState=@CheckState where ResOrderId=@ResOrderId";
        private const string Sql_HotelTraveller_INSERT = " INSERT INTO tbl_HotelTraveller([TravellerId],[OrderId],[TravellerName],[TravellerType],[Mobile]) VALUES(newid(),'{0}','{1}','{2}','{3}'); ";
        private const string Sql_HotelOrder_GETORDERINFO = "SELECT OrderId,ResOrderId,BuyerUName,BuyerUFullname,ContacterFullname,ContacterMobile,ContacterTelephone,OrderState,CheckState,HotelName,RoomTypeName,CheckInDate,CheckOutDate,TotalAmount,CommissionPercent,CommissionFix,BuyerCId,Remark,(select TravellerId,OrderId,TravellerName,TravellerType,Mobile from tbl_HotelTraveller where OrderId=tbl_HotelOrder.OrderId for xml auto,root('Traveller')) as Travellers from tbl_HotelOrder where ResOrderId=@ResOrderId";
        #endregion

        #region 构造函数
        /// <summary>
        /// 数据库链接
        /// </summary>
        Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelOrder() 
        {
            _database = base.HotelStore;
        }
        #endregion

        #region IHotelOrder 成员
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="model">酒店订单实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.HotelStructure.OrderInfo model)
        {
            model.OrderId = Guid.NewGuid().ToString();
            #region 构造旅客信息SQL
            StringBuilder strTravellers = new StringBuilder();//存储旅客SQL
            if (model.ResGuests != null && model.ResGuests.Count > 0)
            {
                foreach (EyouSoft.HotelBI.HBEResGuestInfo info in model.ResGuests)
                {
                    strTravellers.AppendFormat(Sql_HotelTraveller_INSERT, model.OrderId, info.PersonName, (int)info.GuestTypeIndicator, info.Mobile);
                }
            }
            #endregion
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelOrder_INSERT+strTravellers.ToString());
            this._database.AddInParameter(dc, "OrderId", DbType.AnsiStringFixedLength, model.OrderId);
            this._database.AddInParameter(dc, "BuyerCId", DbType.AnsiStringFixedLength, model.BuyerCId);
            this._database.AddInParameter(dc, "BuyerCName", DbType.String, model.BuyerCName);
            this._database.AddInParameter(dc, "BuyerUFullname", DbType.String, model.BuyerUFullName);
            this._database.AddInParameter(dc, "BuyerUId", DbType.AnsiStringFixedLength, model.BuyerUId);
            this._database.AddInParameter(dc, "BuyerUName", DbType.String, model.BuyerUName);
            this._database.AddInParameter(dc, "CheckInDate", DbType.DateTime, model.CheckInDate);
            this._database.AddInParameter(dc, "CheckOutDate", DbType.DateTime, model.CheckOutDate);
            this._database.AddInParameter(dc, "CheckState", DbType.Int32, (int)model.CheckState);
            this._database.AddInParameter(dc, "CityCode", DbType.String, model.CityCode);
            this._database.AddInParameter(dc, "CommissionFix", DbType.Decimal, model.CommissionFix);
            this._database.AddInParameter(dc, "CommissionPercent", DbType.Decimal, model.CommissionPercent);
            this._database.AddInParameter(dc, "CommissionType", DbType.Int32, (int)model.CommissionType);
            this._database.AddInParameter(dc, "ContacterFullname", DbType.String, model.ContacterFullname);
            this._database.AddInParameter(dc, "ContacterMobile", DbType.String, model.ContacterMobile);
            this._database.AddInParameter(dc, "ContacterTelephone", DbType.String, model.ContacterTelephone);
            this._database.AddInParameter(dc, "CreateDateTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "HotelCode", DbType.String, model.HotelCode);
            this._database.AddInParameter(dc, "HotelName", DbType.String, model.HotelName);
            this._database.AddInParameter(dc, "IsMobileContact", DbType.String, model.IsMobileContact?"1":"0");
            this._database.AddInParameter(dc, "OrderState", DbType.Int32, (int)model.ResStatus);
            this._database.AddInParameter(dc, "OrderType", DbType.Int32, (int)model.OrderType);
            this._database.AddInParameter(dc, "PaymentType", DbType.Int32, (int)model.PaymentType);
            this._database.AddInParameter(dc, "ResOrderId", DbType.String, model.ResOrderId);
            this._database.AddInParameter(dc, "RoomTypeCode", DbType.String, model.RoomTypeCode);
            this._database.AddInParameter(dc, "RoomTypeName", DbType.String, model.RoomTypeName);
            this._database.AddInParameter(dc, "TotalAmount", DbType.Decimal, model.TotalAmount);
            this._database.AddInParameter(dc, "RatePlanCode", DbType.String, model.RatePlanCode);
            this._database.AddInParameter(dc, "VendorCode", DbType.String, model.VendorCode);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Comments);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <param name="OrderState">订单状态[枚举]</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetOrderState(string ResOrderId, EyouSoft.HotelBI.HBEResStatus OrderState)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelOrder_SETORDERSTATE);
            this._database.AddInParameter(dc, "OrderState", DbType.Int32, (int)OrderState);
            this._database.AddInParameter(dc, "ResOrderId", DbType.String, ResOrderId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置订单审核状态
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <param name="CheckState">审核状态[枚举]</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetCheckState(string ResOrderId, EyouSoft.Model.HotelStructure.CheckStateList CheckState)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelOrder_SETCHECKSTATE);
            this._database.AddInParameter(dc, "CheckState", DbType.Int32, (int)CheckState);
            this._database.AddInParameter(dc, "ResOrderId", DbType.String, ResOrderId);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="ResOrderId">订单号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.OrderInfo GetOrderInfo(string ResOrderId)
        {
            EyouSoft.Model.HotelStructure.OrderInfo model = null;
            DbCommand dc = this._database.GetSqlStringCommand(Sql_HotelOrder_GETORDERINFO);
            this._database.AddInParameter(dc, "ResOrderId", DbType.String, ResOrderId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc,this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.OrderInfo();
                    model.OrderId = dr[dr.GetOrdinal("OrderId")].ToString();
                    model.ResOrderId = dr[dr.GetOrdinal("ResOrderId")].ToString();
                    model.BuyerUName = dr[dr.GetOrdinal("BuyerUName")].ToString();
                    model.BuyerUFullName = dr[dr.GetOrdinal("BuyerUFullname")].ToString();
                    model.ContacterFullname = dr[dr.GetOrdinal("ContacterFullname")].ToString();
                    model.ContacterMobile = dr[dr.GetOrdinal("ContacterMobile")].ToString();
                    model.ContacterTelephone = dr[dr.GetOrdinal("ContacterTelephone")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("OrderState")))
                        model.ResStatus = (EyouSoft.HotelBI.HBEResStatus)int.Parse(dr[dr.GetOrdinal("OrderState")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckState")))
                        model.CheckState = (EyouSoft.Model.HotelStructure.CheckStateList)int.Parse(dr[dr.GetOrdinal("CheckState")].ToString());
                    model.HotelName = dr[dr.GetOrdinal("HotelName")].ToString();
                    model.RoomTypeName = dr[dr.GetOrdinal("RoomTypeName")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckInDate")))
                        model.CheckInDate = DateTime.Parse(dr[dr.GetOrdinal("CheckInDate")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckOutDate")))
                        model.CheckOutDate = DateTime.Parse(dr[dr.GetOrdinal("CheckOutDate")].ToString());
                    model.TotalAmount = decimal.Parse(dr[dr.GetOrdinal("TotalAmount")].ToString());
                    model.CommissionPercent = decimal.Parse(dr[dr.GetOrdinal("CommissionPercent")].ToString());
                    model.CommissionFix = decimal.Parse(dr[dr.GetOrdinal("CommissionFix")].ToString());
                    model.BuyerCId = dr[dr.GetOrdinal("BuyerCId")].ToString();
                    model.ResGuests = GetTravellers(dr[dr.GetOrdinal("Travellers")].ToString());
                    model.Comments = dr[dr.GetOrdinal("Remark")].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取酒店订单
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">查询实体</param>
        /// <returns>酒店订单列表</returns>
        public virtual IList<EyouSoft.Model.HotelStructure.OrderInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo)
        {
            IList<EyouSoft.Model.HotelStructure.OrderInfo> list = new List<EyouSoft.Model.HotelStructure.OrderInfo>();
            #region 构造SQL条件
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            string TableName="tbl_HotelOrder";
            string primaryKey="OrderId";
            string fields = "OrderId,ResOrderId,BuyerUName,BuyerUFullname,ContacterFullname,ContacterMobile,ContacterTelephone,OrderState,CheckState,HotelName,RoomTypeName,CheckInDate,CheckOutDate,TotalAmount,CommissionPercent,CommissionFix,BuyerCId,(select TravellerId,OrderId,TravellerName,TravellerType,Mobile from tbl_HotelTraveller where OrderId=tbl_HotelOrder.OrderId for xml auto,root('Traveller')) as Travellers";
            string StrOrderBy = " CreateDateTime DESC ";
            if (SearchInfo != null)
            {
                if (!string.IsNullOrEmpty(SearchInfo.CompanyId))
                    strWhere.AppendFormat(" and BuyerCId='{0}' ", SearchInfo.CompanyId);
                if (!string.IsNullOrEmpty(SearchInfo.ResOrderId))
                    strWhere.AppendFormat(" and ResOrderId like'%{0}%' ", SearchInfo.ResOrderId);
                if (SearchInfo.OrderType.HasValue)
                    strWhere.AppendFormat(" and OrderType={0} ", (int)SearchInfo.OrderType.Value);
                if (SearchInfo.OrderState.HasValue)
                {
                    switch (SearchInfo.OrderState.Value)
                    { 
                        case EyouSoft.Model.HotelStructure.OrderStateList.处理中:
                            strWhere.Append(" and OrderState in(1,2,7) ");
                            break;
                        case EyouSoft.Model.HotelStructure.OrderStateList.取消:
                            strWhere.Append(" and OrderState in(3,4,6) ");
                            break;
                        case EyouSoft.Model.HotelStructure.OrderStateList.已确认:
                            strWhere.Append(" and OrderState= 0 ");
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(SearchInfo.HotelName))
                    strWhere.AppendFormat(" and HotelName like'%{0}%' ", SearchInfo.HotelName);
                if (!string.IsNullOrEmpty(SearchInfo.CustomerName))
                    strWhere.AppendFormat(" and OrderId in(select OrderId from tbl_HotelTraveller where TravellerName like '%{0}%')", SearchInfo.CustomerName);
                if (SearchInfo.CheckState.HasValue)
                    strWhere.AppendFormat(" and CheckState={0} ", (int)SearchInfo.CheckState.Value);
                if (SearchInfo.CheckOutSDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CheckOutDate)>=0 ", SearchInfo.CheckOutSDate.Value.ToString());
                if (SearchInfo.CheckOutEDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CheckOutDate)<=0 ", SearchInfo.CheckOutEDate.Value.ToString());
                if (SearchInfo.CheckInSDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CheckInDate)>=0 ", SearchInfo.CheckInSDate.Value.ToString());
                if (SearchInfo.CheckInEDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CheckInDate)<=0 ", SearchInfo.CheckInEDate.Value.ToString());
                if (SearchInfo.CreateSDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CreateDateTime)>=0 ", SearchInfo.CreateSDate.Value.ToString());
                if (SearchInfo.CreateEDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,'{0}',CreateDateTime)<=0 ", SearchInfo.CreateEDate.Value.ToString());
                if (!string.IsNullOrEmpty(SearchInfo.BuyerUName))
                    strWhere.AppendFormat(" AND BuyerUName LIKE '%{0}%' ", SearchInfo.BuyerUName);
            }
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, PageSize, PageIndex, ref RecordCount, TableName, primaryKey, fields, strWhere.ToString(), StrOrderBy))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.HotelStructure.OrderInfo model = new EyouSoft.Model.HotelStructure.OrderInfo();
                    model.OrderId = dr[dr.GetOrdinal("OrderId")].ToString();
                    model.ResOrderId = dr[dr.GetOrdinal("ResOrderId")].ToString();
                    model.BuyerUName = dr[dr.GetOrdinal("BuyerUName")].ToString();
                    model.BuyerUFullName = dr[dr.GetOrdinal("BuyerUFullname")].ToString();
                    model.ContacterFullname = dr[dr.GetOrdinal("ContacterFullname")].ToString();
                    model.ContacterMobile = dr[dr.GetOrdinal("ContacterMobile")].ToString();
                    model.ContacterTelephone = dr[dr.GetOrdinal("ContacterTelephone")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("OrderState")))
                        model.ResStatus = (EyouSoft.HotelBI.HBEResStatus)int.Parse(dr[dr.GetOrdinal("OrderState")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckState")))
                        model.CheckState = (EyouSoft.Model.HotelStructure.CheckStateList)int.Parse(dr[dr.GetOrdinal("CheckState")].ToString());
                    model.HotelName = dr[dr.GetOrdinal("HotelName")].ToString();
                    model.RoomTypeName = dr[dr.GetOrdinal("RoomTypeName")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckInDate")))
                        model.CheckInDate =DateTime.Parse(dr[dr.GetOrdinal("CheckInDate")].ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("CheckOutDate")))
                        model.CheckOutDate = DateTime.Parse(dr[dr.GetOrdinal("CheckOutDate")].ToString());
                    model.TotalAmount=decimal.Parse(dr[dr.GetOrdinal("TotalAmount")].ToString());
                    model.CommissionPercent = decimal.Parse(dr[dr.GetOrdinal("CommissionPercent")].ToString());
                    model.CommissionFix = decimal.Parse(dr[dr.GetOrdinal("CommissionFix")].ToString());
                    model.BuyerCId = dr[dr.GetOrdinal("BuyerCId")].ToString();
                    model.ResGuests = GetTravellers(dr[dr.GetOrdinal("Travellers")].ToString());
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #region XMLTo旅客信息
        /// <summary>
        /// 根据XML获取旅客信息
        /// </summary>
        /// <param name="TravellerXML">旅客信息XML字符串</param>
        /// <returns>旅客信息列表</returns>
        private IList<EyouSoft.HotelBI.HBEResGuestInfo> GetTravellers(string TravellerXML)
        {
            if (string.IsNullOrEmpty(TravellerXML))
                return null;
            IList<EyouSoft.HotelBI.HBEResGuestInfo> list = new List<EyouSoft.HotelBI.HBEResGuestInfo>();
            XElement xElement = XElement.Parse(TravellerXML);
            var Travellers = xElement.Elements("tbl_HotelTraveller");
            foreach (var Traveller in Travellers)
            {
                EyouSoft.HotelBI.HBEResGuestInfo model = new EyouSoft.HotelBI.HBEResGuestInfo();
                model.PersonName= EyouSoft.HotelBI.Utils.GetXAttributeValue(Traveller, "TravellerName");
                model.Mobile = EyouSoft.HotelBI.Utils.GetXAttributeValue(Traveller, "Mobile");
                if (!string.IsNullOrEmpty(EyouSoft.HotelBI.Utils.GetXAttributeValue(Traveller, "TravellerType")))
                    model.GuestTypeIndicator = (EyouSoft.HotelBI.HBEGuestTypeIndicator)int.Parse(EyouSoft.HotelBI.Utils.GetXAttributeValue(Traveller, "TravellerType"));
                list.Add(model);
                model = null;
            }
            
            xElement = null;
            return list;
        }
        #endregion

        #endregion
    }
}
