//Author:汪奇志 2010-10-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;


namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 机票系统-订单数据访问
    /// </summary>
    public class TicketOrder : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.TicketStructure.ITicketOrder
    {
        #region static constants
        //static constants
        private const string SQL_UPDATE_UpdatePrice = "UPDATE tbl_TicketOrder SET TotalAmount=@TotalAmount WHERE OrderId=@OrderId;UPDATE tbl_TicketOrderRate SET LeaveFacePrice=@LeaveFacePrice,LeaveDiscount=@LeaveDiscount,LeavePrice=@LeavePrice,ReturnFacePrice=@ReturnFacePrice,ReturnDiscount=@ReturnDiscount,ReturnPrice=@ReturnPrice,LFuelPrice=@LFuelPrice,LBuildPrice=@LBuildPrice,RFuelPrice=@RFuelPrice,RBuildPrice=@RBuildPrice WHERE OrderId=@OrderId;";
        private const string SQL_UPDATE_UpdatePNR = "UPDATE tbl_TicketOrder SET PNR=@PNR WHERE OrderId=@OrderId";
        private const string SQL_UPDATE_UpdateFlightCode = "UPDATE tbl_TicketOrder SET LFlightCode=@LFlightCode,RFlightCode=@RFlightCode WHERE OrderId=@OrderId";
        private const string SQL_UPDATE_UpdateTraveller = "UPDATE tbl_TicketOrderTraveller SET TicketNumber=@TicketNumber{0} WHERE TravellerId=@TravellerId{0};";
        private const string SQL_SELECT_GetInfoByOrderId = "SELECT * FROM tbl_TicketOrder WHERE OrderId=@OrderId;";
        private const string SQL_SELECT_GetInfoByOrderNo = "SELECT * FROM tbl_TicketOrder WHERE OrderNo=@OrderNo;";
        private const string SQL_SELECT_GetInfo_Rate = "SELECT * FROM tbl_TicketOrderRate WHERE OrderId=@OrderId;";
        private const string SQL_SELECT_GetInfo_Travellers = "SELECT * FROM tbl_TicketOrderTraveller WHERE OrderId=@OrderId;";
        private const string SQL_SELECT_IsAllowApply = "SELECT COUNT(*) FROM tbl_TicketOrderChange WHERE OrderId=@OrderId AND ChangeState=@ChangeState";
        private const string SQL_SELECT_GetHandels = "SELECT *,(SELECT [ContactName] FROM [tbl_CompanyUser] WHERE [Id]=[tbl_TicketHandleList].[UserId]) AS FullName FROM [tbl_TicketHandleList] WHERE [OrderId]=@OrderId ORDER BY [IssueTime]";
        private const string SQL_SELECT_GetChanges = "SELECT *,(SELECT [ContactName] FROM [tbl_CompanyUser] WHERE [Id]=[tbl_TicketOrderChange].[ChangeUId]) AS ChangeUFullName,(SELECT [ContactName] FROM [tbl_CompanyUser] WHERE [Id]=[tbl_TicketOrderChange].[CheckUId]) AS CheckUFullName FROM [tbl_TicketOrderChange] WHERE [OrderId]=@OrderId ORDER BY [ChangeTime]";
        private const string SQL_UPDATE_UpdateServiceNote = "UPDATE [tbl_TicketOrder] SET [ServiceNote]=@ServiceNote WHERE OrderId=@OrderId";
        private const string SQL_UPDATE_SetBalanceAmount = "UPDATE [tbl_TicketOrder] SET BalanceAmount=@Amount WHERE OrderId=@OrderId";
        private const string SQL_UPDATE_UpdateBuyerRemark = "UPDATE [tbl_TicketOrder] SET BuyerRemark=@BuyerRemark WHERE OrderId=@OrderId";
        private const string SQL_INSERT_InsertOrderAccount = "DELETE FROM [tbl_TicketOrderAccount] WHERE [OrderId]=@OrderId;INSERT INTO [tbl_TicketOrderAccount]([OrderId],[PayCompanyId],[PayUserId],[PayType],[PayAccount],[SellCompanyId],[SellAccount],[OrderNo],[Discount],[PayPrice]) VALUES(@OrderId,@PayCompanyId,@PayUserId,@PayType,@PayAccount,@SellCompanyId,@SellAccount,@OrderNo,@Discount,@PayPrice);";
        private const string SQL_SELECT_GetOrderAccountInfo = "SELECT * FROM [tbl_TicketOrderAccount] WHERE OrderId=@OrderId";
        private const string SQL_UPDATE_UpdateBuyerContact = "UPDATE [tbl_TicketOrder] SET BuyerCName=@BuyerCName,BuyerContactName=@BuyerContactName,BuyerContactMobile=@BuyerContactMobile,BuyerContactAddress=@BuyerContactAddress WHERE OrderId=@OrderId";
        private const string SQL_SELECT_GetLatestChange = "SELECT TOP(1) * FROM [tbl_TicketOrderChange] WHERE OrderId=@OrderId ORDER BY ChangeTime DESC";
        private const string SQL_SELECT_GetSupplierHandelStats = "SELECT ISNULL(SUM(A.PCount),0) AS V,A.OrderState,A.RateType FROM tbl_TicketOrder AS A WHERE A.SupplierCId=@SupplierCId AND A.OrderState IN(0,3) GROUP BY A.RateType,A.OrderState;SELECT SUM(B.PeopleNum) AS V,B.ChangeType,A.RateType FROM tbl_TicketOrder AS A INNER JOIN tbl_TicketOrderChange AS B ON A.OrderId=B.OrderId AND B.ChangeState=0 WHERE A.SupplierCId=@SupplierCId GROUP BY A.RateType,B.ChangeType;SELECT SUM(DATEDIFF(mi,B.ChangeTime,B.CheckTime)) AS V,1 AS RefundTicketType,A.RateType FROM tbl_TicketOrder AS A INNER JOIN tbl_TicketOrderChange AS B ON A.OrderId=B.OrderId AND B.ChangeState=2 AND B.ChangeType=0 AND RefundTicketType=1 WHERE A.SupplierCId=@SupplierCId GROUP BY A.RateType;SELECT SUM(DATEDIFF(mi,B.ChangeTime,B.CheckTime)) AS V,0 AS RefundTicketType,A.RateType FROM tbl_TicketOrder AS A INNER JOIN tbl_TicketOrderChange AS B ON A.OrderId=B.OrderId AND B.ChangeState=2 AND B.ChangeType=0 AND RefundTicketType>1 WHERE A.SupplierCId=@SupplierCId GROUP BY A.RateType";
        private const string SQL_UPDATE_SetChangeAmount = "UPDATE tbl_TicketOrderChange SET HandlingFee=@HandlingFee,ChangeAmount=@ChangeAmount,TotalAmount=@TotalAmount WHERE ChangeId=@ChangeId";
        private const string SQL_UPDATE_SetChangeCheckRemark = "UPDATE tbl_TicketOrderChange SET CheckRemark=@CheckRemark WHERE ChangeId=@ChangeId";

        #endregion

        #region constructor
        private Database _db = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public TicketOrder() { this._db = this.TicketStore; }
        #endregion

        #region private members
        /// <summary>
        /// 构造订单旅客信息XML
        /// </summary>
        /// <param name="travellers">旅客信息集合</param>
        /// <returns></returns>
        private string CreateOrderTravellersXML(IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<ROOT>");

            if (travellers != null && travellers.Count > 0)
            {
                foreach (var tmp in travellers)
                {
                    xml.AppendFormat("<Traveller TravellerId=\"{0}\" CertType=\"{1}\" CertNo=\"{2}\" TravellerName=\"{3}\" TravellerType=\"{4}\" Gender=\"{5}\" Telephone=\"{6}\" IsBuyIns=\"{7}\" InsPrice=\"{8}\" IsBuyItinerary=\"{9}\" State=\"{10}\" />"
                        , tmp.TravellerId
                        , (int)tmp.CertType
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.CertNo)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.TravellerName)
                        , (int)tmp.TravellerType
                        , (int)tmp.Gender
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.Telephone)
                        , tmp.IsBuyIns ? "1" : "0"
                        , tmp.InsPrice
                        , tmp.IsBuyItinerary ? "1" : "0"
                        , (int)tmp.TravellerState);
                }
            }

            xml.Append("</ROOT>");

            return xml.ToString();
        }

        /// <summary>
        /// 构造订单变更旅客信息XML
        /// </summary>
        /// <param name="travellers"></param>
        /// <returns></returns>
        private string CreateApplyChangeTravellersXML(IList<string> travellers)
        {
            StringBuilder xml = new StringBuilder();

            xml.Append("<ROOT>");
            if (travellers != null && travellers.Count > 0)
            {
                foreach (string s in travellers)
                {
                    xml.AppendFormat("<Traveller TravellerId=\"{0}\" />", s);
                }
            }
            xml.Append("</ROOT>");

            return xml.ToString();
        }

        /// <summary>
        /// 获取订单信息业务实体
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private EyouSoft.Model.TicketStructure.OrderInfo GetInfo(DbCommand cmd, Database db)
        {
            EyouSoft.Model.TicketStructure.OrderInfo order = null;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    order = new EyouSoft.Model.TicketStructure.OrderInfo();
                    order.BalanceAmount = rdr.GetDecimal(rdr.GetOrdinal("BalanceAmount"));
                    order.BuyerCId = rdr.GetString(rdr.GetOrdinal("BuyerCId"));
                    order.BuyerCName = rdr["BuyerCName"].ToString();
                    order.BuyerContactAddress = rdr["BuyerContactAddress"].ToString();
                    order.BuyerContactMobile = rdr["BuyerContactMobile"].ToString();
                    order.BuyerContactMQ = rdr["BuyerContactMQ"].ToString();
                    order.BuyerContactName = rdr["BuyerContactName"].ToString();
                    order.BuyerRemark = rdr["BuyerRemark"].ToString();
                    order.BuyerUId = rdr.GetString(rdr.GetOrdinal("BuyerUId"));
                    order.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    order.ElapsedTime = rdr.GetInt32(rdr.GetOrdinal("ElapsedTime"));
                    order.EMSPrice = rdr.GetDecimal(rdr.GetOrdinal("EMSPrice"));
                    order.FlightId = rdr.GetInt32(rdr.GetOrdinal("FlightId"));
                    order.FreightType = (EyouSoft.Model.TicketStructure.FreightType)rdr.GetByte(rdr.GetOrdinal("FreightType"));
                    order.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    order.ItineraryPrice = rdr.GetDecimal(rdr.GetOrdinal("ItineraryPrice"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LeaveTime")))
                        order.LeaveTime = rdr.GetDateTime(rdr.GetOrdinal("LeaveTime"));
                    order.LFlightCode = rdr["LFlightCode"].ToString();
                    order.OPNR = rdr["OPNR"].ToString();
                    order.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    order.OrderNo = rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    order.OrderState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(rdr.GetOrdinal("OrderState"));
                    order.OrderTime = rdr.GetDateTime(rdr.GetOrdinal("OrderTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("PayTime")))
                        order.PayTime = rdr.GetDateTime(rdr.GetOrdinal("PayTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("PayType")))
                        order.PayType = (EyouSoft.Model.TicketStructure.TicketAccountType)rdr.GetByte(rdr.GetOrdinal("PayType"));
                    order.PCount = rdr.GetInt32(rdr.GetOrdinal("PCount"));
                    order.PNR = rdr["PNR"].ToString();
                    order.RateType = (EyouSoft.Model.TicketStructure.RateType)rdr.GetByte(rdr.GetOrdinal("RateType"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ReturnTime")))
                        order.ReturnTime = rdr.GetDateTime(rdr.GetOrdinal("ReturnTime"));
                    order.RFlightCode = rdr["RFlightCode"].ToString();
                    order.ServiceNote = rdr["ServiceNote"].ToString();
                    order.SupplierCId = rdr.GetString(rdr.GetOrdinal("SupplierCId"));
                    order.SupplierCName = rdr["SupplierCName"].ToString();
                    order.SupplierUName = rdr["SupplierUName"].ToString();
                    order.TotalAmount = rdr.GetDecimal(rdr.GetOrdinal("TotalAmount"));
                    order.TravellerType = (EyouSoft.Model.TicketStructure.TravellerType)rdr.GetByte(rdr.GetOrdinal("TravellerType"));                    
                }

                return order;
            }
        }

        /// <summary>
        /// 获取订单运价信息业务实体
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private EyouSoft.Model.TicketStructure.OrderRateInfo GetInfoRate(string orderId)
        {
            EyouSoft.Model.TicketStructure.OrderRateInfo rate = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetInfo_Rate);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    rate = new EyouSoft.Model.TicketStructure.OrderRateInfo();

                    rate.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    rate.FlightId = rdr.GetInt32(rdr.GetOrdinal("FlightId"));
                    rate.FreightType = (EyouSoft.Model.TicketStructure.FreightType)rdr.GetByte(rdr.GetOrdinal("FreightType"));
                    rate.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    rate.LBuildPrice = rdr.GetDecimal(rdr.GetOrdinal("LBuildPrice"));
                    rate.LeaveDiscount = decimal.Parse(rdr["LeaveDiscount"].ToString());//rdr.GetDecimal(rdr.GetOrdinal("LeaveDiscount"));
                    rate.LeaveFacePrice = rdr.GetDecimal(rdr.GetOrdinal("LeaveFacePrice"));
                    rate.LeavePrice = rdr.GetDecimal(rdr.GetOrdinal("LeavePrice"));
                    rate.LeaveTimeLimit = rdr["LeaveTimeLimit"].ToString();
                    rate.LFuelPrice = rdr.GetDecimal(rdr.GetOrdinal("LFuelPrice"));
                    rate.MaxPCount = rdr.GetInt32(rdr.GetOrdinal("MaxPCount"));
                    rate.RBuildPrice = rdr.GetDecimal(rdr.GetOrdinal("RBuildPrice"));
                    rate.ReturnDiscount = decimal.Parse(rdr["ReturnDiscount"].ToString()); //rdr.GetDecimal(rdr.GetOrdinal("ReturnDiscount"));
                    rate.ReturnFacePrice = rdr.GetDecimal(rdr.GetOrdinal("ReturnFacePrice"));
                    rate.ReturnPrice = rdr.GetDecimal(rdr.GetOrdinal("ReturnPrice"));
                    rate.ReturnTimeLimit = rdr["ReturnTimeLimit"].ToString();
                    rate.RFuelPrice = rdr.GetDecimal(rdr.GetOrdinal("RFuelPrice"));
                    rate.SupplierRemark = rdr["SupplierRemark"].ToString();
                }
            }

            return rate;
        }

        /// <summary>
        /// 获取订单旅客信息集合
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> GetInfoTravellers(string orderId)
        {
            IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers = new List<EyouSoft.Model.TicketStructure.OrderTravellerInfo>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetInfo_Travellers);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    travellers.Add(new EyouSoft.Model.TicketStructure.OrderTravellerInfo()
                    {
                        CertNo = rdr.GetString(rdr.GetOrdinal("CertNo")),
                        CertType = (EyouSoft.Model.TicketStructure.TicketCardType)rdr.GetByte(rdr.GetOrdinal("CertType")),
                        Gender = rdr.IsDBNull(rdr.GetOrdinal("Gender")) ? EyouSoft.Model.CompanyStructure.Sex.未知 : (EyouSoft.Model.CompanyStructure.Sex)int.Parse(rdr.GetString(rdr.GetOrdinal("Gender"))),
                        InsPrice = rdr.IsDBNull(rdr.GetOrdinal("InsPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("InsPrice")),
                        IsBuyIns = rdr.GetString(rdr.GetOrdinal("IsBuyIns")) == "1" ? true : false,
                        IsBuyItinerary = rdr.GetString(rdr.GetOrdinal("IsBuyItinerary")) == "1" ? true : false,
                        Telephone = rdr["Telephone"].ToString(),
                        TicketNumber = rdr["TicketNumber"].ToString(),
                        TravellerId = rdr.GetString(rdr.GetOrdinal("TravellerId")),
                        TravellerName = rdr["TravellerName"].ToString(),
                        TravellerState = (EyouSoft.Model.TicketStructure.TravellerState)rdr.GetByte(rdr.GetOrdinal("State")),
                        TravellerType = (EyouSoft.Model.TicketStructure.TicketVistorType)rdr.GetByte(rdr.GetOrdinal("TravellerType"))
                    });
                }
            }

            return travellers;
        }

        /// <summary>
        /// 根据订单查询条件实体生成订单查询where条件（前面要有其他条件）
        /// </summary>
        /// <param name="searchInfo">订单查询条件实体</param>
        /// <param name="strChangeFile"></param>
        /// <returns>订单查询where条件</returns>
        private string GetOrderSearchSqlWhere(EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo, out string strChangeFile)
        {
            strChangeFile = " 0 as ChangePeopleNum ";
            StringBuilder strWhere = new StringBuilder("");
            if (searchInfo != null)
            {
                if (!string.IsNullOrEmpty(searchInfo.OrderId))
                    strWhere.AppendFormat(" and OrderId = '{0}' ", searchInfo.OrderId);
                if (!string.IsNullOrEmpty(searchInfo.OrderNo))
                    strWhere.AppendFormat(" and OrderNo = '{0}' ", searchInfo.OrderNo);
                if (searchInfo.HomeCityId.HasValue)
                    strWhere.AppendFormat(" and HomeCityId = {0} ", searchInfo.HomeCityId);
                if (searchInfo.DestCityId.HasValue)
                    strWhere.AppendFormat(" and DestCityId = {0} ", searchInfo.DestCityId);
                if (searchInfo.RateType.HasValue)
                    strWhere.AppendFormat(" and RateType = {0} ", (int)searchInfo.RateType);
                if (searchInfo.OrderStartTime.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,OrderTime,'{0}') <= 0 ", searchInfo.OrderStartTime);
                if (searchInfo.OrderFinishTime.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,OrderTime,'{0}') >= 0 ", searchInfo.OrderFinishTime);
                if (searchInfo.FixedDate.HasValue)
                    strWhere.AppendFormat(" and datediff(dd,OrderTime,'{0}') = 0 ", searchInfo.FixedDate);
                if (searchInfo.OrderState.HasValue)
                    strWhere.AppendFormat(" and OrderState = {0} ", (int)searchInfo.OrderState);
                if (searchInfo.OrderChangeType.HasValue || searchInfo.OrderChangeState.HasValue)
                {
                    strWhere.Append(" and exists (select 1 from tbl_TicketOrderChange where tbl_TicketOrderChange.OrderId = tbl_TicketOrder.OrderId ");
                    if (searchInfo.OrderChangeType.HasValue)
                        strWhere.AppendFormat(" and ChangeType = {0} ", (int)searchInfo.OrderChangeType);
                    if (searchInfo.OrderChangeType.HasValue)
                        strWhere.AppendFormat(" and ChangeState = {0} ", (int)searchInfo.OrderChangeState);

                    strWhere.Append(" ) ");
                }
                if (!string.IsNullOrEmpty(searchInfo.PNR))
                    strWhere.AppendFormat(" and PNR = '{0}' ", searchInfo.PNR);
                if (!string.IsNullOrEmpty(searchInfo.BuyerCId))
                    strWhere.AppendFormat(" and BuyerCId = '{0}' ", searchInfo.BuyerCId);
                if (!string.IsNullOrEmpty(searchInfo.SupplierCId))
                {
                    if (searchInfo.SupplierCId != "14ab1cc4-055e-47b2-87b6-eb51c17a2921")//某家供应商能查看所有订单(暂时保留功能)
                        strWhere.AppendFormat(" and SupplierCId = '{0}' ", searchInfo.SupplierCId);
                }
                if (searchInfo.FlightId.HasValue)
                    strWhere.AppendFormat(" and FlightId = {0} ", searchInfo.FlightId);
                if (!string.IsNullOrEmpty(searchInfo.TicketNumber) || !string.IsNullOrEmpty(searchInfo.TravellerName))
                {
                    strWhere.Append(" and exists (select 1 from tbl_TicketOrderTraveller where tbl_TicketOrderTraveller.OrderId = tbl_TicketOrder.OrderId ");
                    if (!string.IsNullOrEmpty(searchInfo.TicketNumber))
                        strWhere.AppendFormat(" and TicketNumber = '{0}' ", searchInfo.TicketNumber);
                    if (!string.IsNullOrEmpty(searchInfo.TravellerName))
                        strWhere.AppendFormat(" AND TravellerName LIKE '%{0}%' ", searchInfo.TravellerName);

                    strWhere.Append(" ) ");
                }

                if (searchInfo.OrderChangeType.HasValue || searchInfo.OrderChangeState.HasValue)
                    strChangeFile = " (select PeopleNum from tbl_TicketOrderChange where tbl_TicketOrderChange.OrderId = tbl_TicketOrder.OrderId and tbl_TicketOrderChange.ChangeState = 0) as ChangePeopleNum ";
            }

            return strWhere.ToString();
        }

        /// <summary>
        /// 获取默认的供应商订单处理统计信息
        /// </summary>
        /// <returns></returns>
        private IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> GetDefaultSupplierHandelStats()
        {
            IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> stats = new Dictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>>();
            var orderStatTypes = Enum.GetValues(typeof(EyouSoft.Model.TicketStructure.OrderStatType));
            var rateTypes = Enum.GetValues(typeof(EyouSoft.Model.TicketStructure.RateType));

            foreach (var orderStatType in orderStatTypes)
            {
                var stat = new Dictionary<EyouSoft.Model.TicketStructure.RateType, string>();
                foreach (var rateType in rateTypes)
                {
                    stat.Add((EyouSoft.Model.TicketStructure.RateType)rateType, "0");
                }
                stats.Add((EyouSoft.Model.TicketStructure.OrderStatType)orderStatType, stat);
            }

            return stats;
        }

        /// <summary>
        /// 根据订单旅客信息集合FOR XML AUTO, ROOT('root')后的字符串获取旅客信息集合
        /// </summary>
        /// <param name="s">FOR XML AUTO后的字符串</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> GetTravellersByXml(string s)
        {
            IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers = new List<EyouSoft.Model.TicketStructure.OrderTravellerInfo>();

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(s);

            System.Xml.XmlNodeList xnl=xd.GetElementsByTagName("tbl_TicketOrderTraveller");

            if (xnl != null)
            {
                foreach (System.Xml.XmlNode xn in xnl)
                {
                    EyouSoft.Model.TicketStructure.OrderTravellerInfo traveller = new EyouSoft.Model.TicketStructure.OrderTravellerInfo();

                    traveller.CertNo = xn.Attributes["CertNo"].Value;
                    traveller.CertType = (EyouSoft.Model.TicketStructure.TicketCardType)int.Parse(xn.Attributes["CertType"].Value);
                    traveller.Gender = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(xn.Attributes["Gender"].Value);
                    traveller.InsPrice = decimal.Parse(xn.Attributes["InsPrice"].Value);
                    traveller.IsBuyIns = xn.Attributes["IsBuyIns"].Value == "1" ? true : false;
                    traveller.IsBuyItinerary = xn.Attributes["IsBuyItinerary"].Value == "1" ? true : false;
                    traveller.Telephone = xn.Attributes["Telephone"].Value;
                    traveller.TicketNumber = xn.Attributes["TicketNumber"] != null ? xn.Attributes["TicketNumber"].Value : "";
                    traveller.TravellerId = xn.Attributes["TravellerId"].Value;
                    traveller.TravellerName = xn.Attributes["TravellerName"].Value;
                    traveller.TravellerState = (EyouSoft.Model.TicketStructure.TravellerState)int.Parse(xn.Attributes["State"].Value);
                    traveller.TravellerType = (EyouSoft.Model.TicketStructure.TicketVistorType)int.Parse(xn.Attributes["TravellerType"].Value);

                    travellers.Add(traveller);
                }
            }

            return travellers;
        }

        /// <summary>
        /// decimal parse
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public decimal decimalParse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            return decimal.Parse(s);
        }

        /// <summary>
        /// int parse
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int intParse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            return int.Parse(s);
        }
        #endregion

        #region IOrder 成员
        /// <summary>
        /// 创建订单信息
        /// </summary>
        /// <param name="orderInfo">订单信息业务实体</param>
        /// <returns>0:失败 1:成功</returns>
        public virtual int Create(EyouSoft.Model.TicketStructure.OrderInfo orderInfo)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_TicketOrder_Create");
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderInfo.OrderId);
            this._db.AddOutParameter(cmd, "OrderNo", DbType.String, 50);
            this._db.AddInParameter(cmd, "RateType", DbType.Byte, orderInfo.RateType);
            this._db.AddInParameter(cmd, "LeaveTime", DbType.DateTime, orderInfo.LeaveTime);
            this._db.AddInParameter(cmd, "ReturnTime", DbType.DateTime, orderInfo.ReturnTime);
            this._db.AddInParameter(cmd, "TravellerType", DbType.Byte, orderInfo.TravellerType);
            this._db.AddInParameter(cmd, "SupplierCId", DbType.AnsiStringFixedLength, orderInfo.SupplierCId);
            this._db.AddInParameter(cmd, "SupplierCName", DbType.String, orderInfo.SupplierCName);
            this._db.AddInParameter(cmd, "SupplierUName", DbType.String, orderInfo.SupplierUName);
            this._db.AddInParameter(cmd, "BuyerCId", DbType.AnsiStringFixedLength, orderInfo.BuyerCId);
            this._db.AddInParameter(cmd, "BuyerCName", DbType.String, orderInfo.BuyerCName);
            this._db.AddInParameter(cmd, "BuyerUId", DbType.AnsiStringFixedLength, orderInfo.BuyerUId);
            this._db.AddInParameter(cmd, "BuyerContactName", DbType.String, orderInfo.BuyerContactName);
            this._db.AddInParameter(cmd, "BuyerContactMobile", DbType.String, orderInfo.BuyerContactMobile);
            this._db.AddInParameter(cmd, "BuyerContactAddress", DbType.String, orderInfo.BuyerContactAddress);
            this._db.AddInParameter(cmd, "BuyerContactMQ", DbType.String, orderInfo.BuyerContactMQ);
            this._db.AddInParameter(cmd, "BuyerRemark", DbType.String, orderInfo.BuyerRemark);
            this._db.AddInParameter(cmd, "FlightId", DbType.Int32, orderInfo.FlightId);
            this._db.AddInParameter(cmd, "HomeCityId", DbType.Int32, orderInfo.HomeCityId);
            this._db.AddInParameter(cmd, "DestCityId", DbType.Int32, orderInfo.DestCityId);
            this._db.AddInParameter(cmd, "LFlightCode", DbType.String, orderInfo.LFlightCode);
            this._db.AddInParameter(cmd, "RFlightCode", DbType.String, orderInfo.RFlightCode);
            this._db.AddInParameter(cmd, "ItineraryPrice", DbType.Decimal, orderInfo.ItineraryPrice);
            this._db.AddInParameter(cmd, "EMSPrice", DbType.Decimal, orderInfo.EMSPrice);
            this._db.AddInParameter(cmd, "TotalAmount", DbType.Decimal, orderInfo.TotalAmount);
            this._db.AddInParameter(cmd, "PCount", DbType.Int32, orderInfo.PCount);
            this._db.AddInParameter(cmd, "OrderState", DbType.Byte, orderInfo.OrderState);
            this._db.AddInParameter(cmd, "OrderTime", DbType.DateTime, orderInfo.OrderTime);
            this._db.AddInParameter(cmd, "FreightType", DbType.Byte, orderInfo.OrderRateInfo.FreightType);
            this._db.AddInParameter(cmd, "LeaveFacePrice", DbType.Decimal, orderInfo.OrderRateInfo.LeaveFacePrice);
            this._db.AddInParameter(cmd, "LeaveDiscount", DbType.Decimal, orderInfo.OrderRateInfo.LeaveDiscount);
            this._db.AddInParameter(cmd, "LeaveTimeLimit", DbType.String, orderInfo.OrderRateInfo.LeaveTimeLimit);
            this._db.AddInParameter(cmd, "LeavePrice", DbType.Decimal, orderInfo.OrderRateInfo.LeavePrice);
            this._db.AddInParameter(cmd, "ReturnFacePrice", DbType.Decimal, orderInfo.OrderRateInfo.ReturnFacePrice);
            this._db.AddInParameter(cmd, "ReturnDiscount", DbType.Decimal, orderInfo.OrderRateInfo.ReturnDiscount);
            this._db.AddInParameter(cmd, "ReturnTimeLimit", DbType.String, orderInfo.OrderRateInfo.ReturnTimeLimit);
            this._db.AddInParameter(cmd, "ReturnPrice", DbType.Decimal, orderInfo.OrderRateInfo.ReturnPrice);
            this._db.AddInParameter(cmd, "LFuelPrice", DbType.Decimal, orderInfo.OrderRateInfo.LFuelPrice);
            this._db.AddInParameter(cmd, "LBuildPrice", DbType.Decimal, orderInfo.OrderRateInfo.LBuildPrice);
            this._db.AddInParameter(cmd, "RFuelPrice", DbType.Decimal, orderInfo.OrderRateInfo.RFuelPrice);
            this._db.AddInParameter(cmd, "RBuildPrice", DbType.Decimal, orderInfo.OrderRateInfo.RBuildPrice);
            this._db.AddInParameter(cmd, "MaxPCount", DbType.Int32, orderInfo.OrderRateInfo.MaxPCount);
            this._db.AddInParameter(cmd, "SupplierRemark", DbType.String, orderInfo.OrderRateInfo.SupplierRemark);
            this._db.AddInParameter(cmd, "Travellers", DbType.String, this.CreateOrderTravellersXML(orderInfo.Travellers));
            this._db.AddInParameter(cmd, "HandleId", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            this._db.AddInParameter(cmd, "OPNR", DbType.String, orderInfo.OPNR);

            DbHelper.RunProcedure(cmd, this._db);

            int createResult = Convert.ToInt32(this._db.GetParameterValue(cmd, "Result"));

            if (createResult == 1)
                orderInfo.OrderNo = this._db.GetParameterValue(cmd, "OrderNo").ToString();

            return createResult;
        }

        /// <summary>
        /// 更新订单信息(价格)
        /// </summary>
        /// <param name="orderInfo">订单信息业务实体</param>
        /// <returns></returns>
        public virtual bool UpdatePrice(EyouSoft.Model.TicketStructure.OrderInfo orderInfo)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdatePrice);
            this._db.AddInParameter(cmd, "TotalAmount", DbType.Decimal, orderInfo.TotalAmount);            
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderInfo.OrderId);
            this._db.AddInParameter(cmd, "LeaveFacePrice", DbType.Decimal, orderInfo.OrderRateInfo.LeaveFacePrice);
            this._db.AddInParameter(cmd, "LeaveDiscount", DbType.Decimal, orderInfo.OrderRateInfo.LeaveDiscount);
            this._db.AddInParameter(cmd, "LeavePrice", DbType.Decimal, orderInfo.OrderRateInfo.LeavePrice);
            this._db.AddInParameter(cmd, "ReturnFacePrice", DbType.Decimal, orderInfo.OrderRateInfo.ReturnFacePrice);
            this._db.AddInParameter(cmd, "ReturnDiscount", DbType.Decimal, orderInfo.OrderRateInfo.ReturnDiscount);
            this._db.AddInParameter(cmd, "ReturnPrice", DbType.Decimal, orderInfo.OrderRateInfo.ReturnPrice);
            this._db.AddInParameter(cmd, "LFuelPrice", DbType.Decimal, orderInfo.OrderRateInfo.LFuelPrice);
            this._db.AddInParameter(cmd, "LBuildPrice", DbType.Decimal, orderInfo.OrderRateInfo.LBuildPrice);
            this._db.AddInParameter(cmd, "RFuelPrice", DbType.Decimal, orderInfo.OrderRateInfo.RFuelPrice);
            this._db.AddInParameter(cmd, "RBuildPrice", DbType.Decimal, orderInfo.OrderRateInfo.RBuildPrice);

            return DbHelper.ExecuteSql(cmd, this._db) == 0 ? false : true;
        }

        /// <summary>
        /// 更新订单信息(PNR)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="PNR">PNR</param>
        /// <returns></returns>
        public virtual bool UpdatePNR(string orderId, string PNR)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdatePNR);
            this._db.AddInParameter(cmd, "PNR", DbType.String, PNR);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 0 ? false : true;
        }

        /// <summary>
        /// 更新订单信息(航班号)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="lFlightCode">去程航班号</param>
        /// <param name="rFlightCode">回程航班号</param>
        /// <returns></returns>
        public virtual bool UpdateFlightCode(string orderId, string lFlightCode, string rFlightCode)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdateFlightCode);
            this._db.AddInParameter(cmd, "LFlightCode", DbType.String, lFlightCode);
            this._db.AddInParameter(cmd, "RFlightCode", DbType.String, rFlightCode);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 0 ? false : true;
        }

        /// <summary>
        /// 更新订单旅客信息(票号)
        /// </summary>
        /// <param name="travellers">旅客信息集合</param>
        /// <returns></returns>
        public virtual bool UpdateTraveller(IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers)
        {
            DbCommand cmd = this._db.GetSqlStringCommand("SELECT 1;");
            StringBuilder cmdText = new StringBuilder();
            var i = 0;

            foreach (var traveller in travellers)
            {
                cmdText.AppendFormat(SQL_UPDATE_UpdateTraveller, i.ToString());
                this._db.AddInParameter(cmd, string.Format("TicketNumber{0}", i.ToString()), DbType.String, traveller.TicketNumber);
                this._db.AddInParameter(cmd, string.Format("TravellerId{0}", i.ToString()), DbType.String, traveller.TravellerId);
                i++;
            }

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, this._db) == 0 ? false : true;
        }

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="handelCId">操作人公司编号</param>
        /// <param name="handelUId">操作人用户编号</param>
        /// <param name="handelRemark">备注信息</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payType">支付接口类型</param>
        /// <param name="payAccount">支付账户</param>
        /// <param name="payTradeNo">支付接口返回的交易号</param>
        /// <returns></returns>
        public virtual bool SetState(string orderId, EyouSoft.Model.TicketStructure.OrderState orderState, string handelCId, string handelUId
            , string handelRemark, DateTime? payTime, EyouSoft.Model.TicketStructure.TicketAccountType? payType, string payAccount,string payTradeNo)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_TicketOrder_SetState");
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            this._db.AddInParameter(cmd, "OrderState", DbType.Byte, orderState);
            this._db.AddInParameter(cmd, "HandelCId", DbType.AnsiStringFixedLength, handelCId);
            this._db.AddInParameter(cmd, "HandelUId", DbType.AnsiStringFixedLength, handelUId);
            this._db.AddInParameter(cmd, "HandelRemark", DbType.String, handelRemark);
            this._db.AddInParameter(cmd, "PayTime", DbType.DateTime, payTime);
            this._db.AddInParameter(cmd, "PayType", DbType.Byte, payType);
            this._db.AddInParameter(cmd, "PayAccount", DbType.String, payAccount);
            this._db.AddInParameter(cmd, "HandleId", DbType.String, Guid.NewGuid().ToString());
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            this._db.AddInParameter(cmd, "PayTradeNo", DbType.String, payTradeNo);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 按订单编号获取订单信息业务实体
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.OrderInfo GetInfo(string orderId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetInfoByOrderId);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            EyouSoft.Model.TicketStructure.OrderInfo order = GetInfo(cmd, this._db);

            if (order != null)
            {
                order.OrderRateInfo = this.GetInfoRate(order.OrderId);
                order.Travellers = this.GetInfoTravellers(order.OrderId);
            }

            return order;
        }

        /// <summary>
        /// 按订单号获取订单信息业务实体
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.OrderInfo GetInfoByOrderNo(string orderNo)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetInfoByOrderNo);
            this._db.AddInParameter(cmd, "OrderNo", DbType.AnsiString, orderNo);

            EyouSoft.Model.TicketStructure.OrderInfo order = GetInfo(cmd, this._db);

            if (order != null)
            {
                order.OrderRateInfo = this.GetInfoRate(order.OrderId);
                order.Travellers = this.GetInfoTravellers(order.OrderId);
            }

            return order;
        }

        /// <summary>
        /// 根据指定条件获取订单信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrders(EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = new List<EyouSoft.Model.TicketStructure.OrderInfo>();

            #region 拼接Sql

            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            string strChangeFile = string.Empty;
            strWhere.Append(this.GetOrderSearchSqlWhere(searchInfo, out strChangeFile));
            string strFiles = " OrderId,OrderNo,PCount,LeaveTime,ReturnTime,SupplierCId,SupplierCName,SupplierUName,HomeCityId,DestCityId,LFlightCode,RFlightCode,OrderTime,OrderState,BuyerCId,(select SuccessRate from tbl_TicketWholesalersInfo where tbl_TicketWholesalersInfo.CompanyId = tbl_TicketOrder.SupplierCId) as SuccessRate," + strChangeFile;
            string strOrder = " OrderTime desc ";

            #endregion

            #region 实体赋值

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, "tbl_TicketOrder", "OrderId", strFiles, strWhere.ToString(), strOrder))
            {
                EyouSoft.Model.TicketStructure.OrderInfo order = null;

                while (rdr.Read())
                {
                    order = new EyouSoft.Model.TicketStructure.OrderInfo();

                    order.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    order.OrderNo = rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    order.PCount = rdr.GetInt32(rdr.GetOrdinal("PCount"));
                    order.LeaveTime = rdr.GetDateTime(rdr.GetOrdinal("LeaveTime"));
                    order.ReturnTime = rdr.GetDateTime(rdr.GetOrdinal("ReturnTime"));
                    order.SupplierCId = rdr.GetString(rdr.GetOrdinal("SupplierCId"));
                    order.SupplierCName = rdr["SupplierCName"].ToString();
                    order.SupplierUName = rdr["SupplierUName"].ToString();
                    order.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    order.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    order.LFlightCode = rdr["LFlightCode"].ToString();
                    order.RFlightCode = rdr["RFlightCode"].ToString();
                    order.OrderTime = rdr.GetDateTime(rdr.GetOrdinal("OrderTime"));
                    order.OrderState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(rdr.GetOrdinal("OrderState"));
                    order.BuyerCId = rdr.GetString(rdr.GetOrdinal("BuyerCId"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SuccessRate")))
                        order.SuccessRate = decimal.Parse(rdr["SuccessRate"].ToString());
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChangePeopleNum")))
                        order.ChangePeopleNum = int.Parse(rdr["ChangePeopleNum"].ToString());

                    list.Add(order);
                }
            }

            #endregion

            return list;
        }

        /// <summary>
        /// 根据指定条件获取订单信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrders(EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TicketStructure.OrderInfo> list = new List<EyouSoft.Model.TicketStructure.OrderInfo>();

            StringBuilder strSql = new StringBuilder();
            string strChangeFile = string.Empty;
            string strWhere = this.GetOrderSearchSqlWhere(searchInfo, out strChangeFile);
            strSql.AppendFormat(" select OrderId,PNR,OPNR,OrderNo,PCount,LeaveTime,ReturnTime,SupplierCId,SupplierCName,SupplierUName,HomeCityId,DestCityId,LFlightCode,RFlightCode,OrderTime,OrderState,BuyerCId,(select SuccessRate from tbl_TicketWholesalersInfo where tbl_TicketWholesalersInfo.CompanyId = tbl_TicketOrder.SupplierCId) as SuccessRate,{0} from tbl_TicketOrder where 1 = 1 {1} ", strChangeFile, strWhere);

            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());

            #region 实体赋值

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._db))
            {
                EyouSoft.Model.TicketStructure.OrderInfo order = null;

                while (rdr.Read())
                {
                    order = new EyouSoft.Model.TicketStructure.OrderInfo();

                    order.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    order.OrderNo = rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    order.PCount = rdr.GetInt32(rdr.GetOrdinal("PCount"));
                    order.LeaveTime = rdr.GetDateTime(rdr.GetOrdinal("LeaveTime"));
                    order.ReturnTime = rdr.GetDateTime(rdr.GetOrdinal("ReturnTime"));
                    order.SupplierCId = rdr.GetString(rdr.GetOrdinal("SupplierCId"));
                    order.SupplierCName = rdr["SupplierCName"].ToString();
                    order.SupplierUName = rdr["SupplierUName"].ToString();
                    order.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    order.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    order.LFlightCode = rdr["LFlightCode"].ToString();
                    order.RFlightCode = rdr["RFlightCode"].ToString();
                    order.OrderTime = rdr.GetDateTime(rdr.GetOrdinal("OrderTime"));
                    order.OrderState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(rdr.GetOrdinal("OrderState"));
                    order.BuyerCId = rdr.GetString(rdr.GetOrdinal("BuyerCId"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SuccessRate")))
                        order.SuccessRate = decimal.Parse(rdr["SuccessRate"].ToString());
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ChangePeopleNum")))
                        order.ChangePeopleNum = int.Parse(rdr["ChangePeopleNum"].ToString());
                    order.PNR = rdr["PNR"].ToString();
                    order.OPNR = rdr["OPNR"].ToString();

                    list.Add(order);
                }
            }

            #endregion

            return list;
        }

        /// <summary>
        /// 是否允许旅客状态变更申请(是否有未审核的旅客状态变更申请)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual bool IsAllowApply(string orderId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_IsAllowApply);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);
            this._db.AddInParameter(cmd, "ChangeState", DbType.Byte, EyouSoft.Model.TicketStructure.OrderChangeState.申请);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0) > 0 ? false : true;
                }
            }

            return false;
        }

        /// <summary>
        /// 订单旅客状态变更申请
        /// </summary>
        /// <param name="changeInfo">旅客状态变更信息业务实体</param>
        /// <returns></returns>
        public virtual bool ApplyChange(EyouSoft.Model.TicketStructure.OrderChangeInfo changeInfo)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_TicketOrder_ApplyChange");
            this._db.AddInParameter(cmd, "ChangeId", DbType.AnsiStringFixedLength, changeInfo.ChangeId);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, changeInfo.OrderId);
            this._db.AddInParameter(cmd, "ChangeState", DbType.Byte, changeInfo.ChangeState);
            this._db.AddInParameter(cmd, "RefundTicketType", DbType.Byte, changeInfo.RefundTicketType);
            this._db.AddInParameter(cmd, "ChangeType", DbType.Byte, changeInfo.ChangeType);
            this._db.AddInParameter(cmd, "ChangeRemark", DbType.String, changeInfo.ChangeRemark);
            this._db.AddInParameter(cmd, "ChangeUId", DbType.AnsiStringFixedLength, changeInfo.ChangeUId);
            this._db.AddInParameter(cmd, "Travellers", DbType.String, this.CreateApplyChangeTravellersXML(changeInfo.Travellers));
            this._db.AddInParameter(cmd, "TravellerState", DbType.Byte, changeInfo.TravellerState);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 订单旅客状态变更审核
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="orderChangeState">审核状态</param>
        /// <param name="checkUId">审核人用户编号</param>
        /// <param name="checkTime">审核时间</param>
        /// <param name="checkRemark">备注</param>
        /// <returns></returns>
        public virtual bool CheckChange(string changeId, EyouSoft.Model.TicketStructure.OrderChangeState orderChangeState, string checkUId, DateTime checkTime, string checkRemark)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_TicketOrder_CheckChange");
            this._db.AddInParameter(cmd, "ChangeId", DbType.AnsiStringFixedLength, changeId);
            this._db.AddInParameter(cmd, "ChangeState", DbType.Byte, orderChangeState);
            this._db.AddInParameter(cmd, "CheckUId", DbType.AnsiStringFixedLength, checkUId);
            this._db.AddInParameter(cmd, "CheckTime", DbType.DateTime, checkTime);
            this._db.AddInParameter(cmd, "CheckRemark", DbType.String, checkRemark);
            this._db.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, this._db);

            return Convert.ToInt32(this._db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获取订单操作信息集合
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderHandleInfo> GetHandels(string orderId)
        {
            IList<EyouSoft.Model.TicketStructure.OrderHandleInfo> handles = new List<EyouSoft.Model.TicketStructure.OrderHandleInfo>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetHandels);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    handles.Add(new EyouSoft.Model.TicketStructure.OrderHandleInfo()
                    {
                        CurrState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(rdr.GetOrdinal("CurrOrderState")),
                        HandelCId = rdr.GetString(rdr.GetOrdinal("CompanyId")),
                        HandelId = rdr.GetString(rdr.GetOrdinal("ID")),
                        HandelTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        HandelUId = rdr.GetString(rdr.GetOrdinal("UserId")),
                        HandleRemark = rdr["HandleRemark"].ToString(),
                        OrderId = orderId,
                        PrevState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(rdr.GetOrdinal("PrevOrderState")),
                        HandelUFullName=rdr["FullName"].ToString()
                    });
                }
            }

            return handles;
        }

        /// <summary>
        /// 获取订单旅客状态变更信息集合
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderChangeInfo> GetChanges(string orderId)
        {
            IList<EyouSoft.Model.TicketStructure.OrderChangeInfo> changes = new List<EyouSoft.Model.TicketStructure.OrderChangeInfo>();
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetChanges);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    changes.Add(new EyouSoft.Model.TicketStructure.OrderChangeInfo()
                    {
                        ChangeId = rdr.GetString(rdr.GetOrdinal("ChangeId")),
                        ChangeRemark = rdr["ChangeRemark"].ToString(),
                        ChangeState = (EyouSoft.Model.TicketStructure.OrderChangeState)rdr.GetByte(rdr.GetOrdinal("ChangeState")),
                        ChangeTime = rdr.GetDateTime(rdr.GetOrdinal("ChangeTime")),
                        ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)rdr.GetByte(rdr.GetOrdinal("ChangeType")),
                        ChangeUId = rdr.GetString(rdr.GetOrdinal("ChangeUId")),
                        CheckRemark = rdr["CheckRemark"].ToString(),
                        CheckTime = rdr.IsDBNull(rdr.GetOrdinal("CheckTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("CheckTime")),
                        CheckUId = rdr["CheckUId"].ToString(),
                        OrderId = orderId,
                        RefundTicketType = (EyouSoft.Model.TicketStructure.RefundTicketType)rdr.GetByte(rdr.GetOrdinal("RefundTicketType")),
                        PCount = rdr.GetInt32(rdr.GetOrdinal("PeopleNum")),
                        ChangeUFullName=rdr["ChangeUFullName"].ToString(),
                        CheckUFullName=rdr["CheckUFullName"].ToString()
                    });
                }
            }

            return changes;
        }

        /// <summary>
        /// 供应商获取采购分析信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> GetBuyerAnalysis(EyouSoft.Model.TicketStructure.BuyerAnalysisSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> list = new List<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo>();

            #region 查询语句
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT GETDATE() AS StatTime ");
            cmdText.Append(" ,A.TicketTotalCount,A.TotalAmount,A.BuyerCName,A.BuyerRecentOrderTime ");
            cmdText.Append(" ,B.CompanyId AS BuyerCId,B.Id AS BuyerUId,B.UserName AS BuyerUName,B.ContactName AS BuyerContactName ");
            cmdText.Append(" ,B.IssueTime AS BuyerRegisterTime,B.ContactMobile AS BuyerContactMobile,B.MQ AS BuyerMQ,B.ContactSex AS BuyerContactGender ");
            cmdText.Append(" FROM ( ");
            cmdText.Append(" SELECT BuyerUId ,MAX(OrderTime) AS BuyerRecentOrderTime,SUM(PCount) AS TicketTotalCount ");
            cmdText.Append(" ,SUM(TotalAmount) AS TotalAmount  ");
            cmdText.Append(" ,MAX(BuyerCName) AS BuyerCName  ");
            cmdText.Append(" FROM tbl_TicketOrder WHERE OrderState=5 {0} GROUP BY BuyerUId {1} ");
            cmdText.Append(" ) AS A ");
            cmdText.Append(" INNER JOIN tbl_CompanyUser AS B ON A.BuyerUId=B.Id {2} ");
            #endregion

            #region 查询条件设置
            #region 供应商编号
            StringBuilder strSupplierWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(searchInfo.SupplierCId))
                strSupplierWhere.AppendFormat(" AND SupplierCId='{0}' ", searchInfo.SupplierCId);
            #endregion

            #region 交易时间
            if (searchInfo.OrderStartTime.HasValue)
                strSupplierWhere.AppendFormat(" AND DATEDIFF(dd,'{0}',OrderTime)>-1 ", searchInfo.OrderStartTime.Value.ToString());
            if (searchInfo.OrderFinishTime.HasValue)
                strSupplierWhere.AppendFormat(" AND DATEDIFF(dd,OrderTime,'{0}')>-1 ", searchInfo.OrderFinishTime.Value.ToString());
            #endregion

            #region having条件 【成功购买机票数/成功购买金额数】
            StringBuilder strHavingWhere = new StringBuilder();
            //成功出票数
            if (searchInfo.TicketTotalCount.HasValue)
                strHavingWhere.AppendFormat(" HAVING SUM(PCount)={0} ", searchInfo.TicketTotalCount.Value);
            //总金额
            if (searchInfo.TicketTotalAmount.HasValue)
            {
                if (strHavingWhere.Length == 0)
                    strHavingWhere.AppendFormat(" HAVING SUM(TotalAmount)={0} ", searchInfo.TicketTotalAmount.Value);
                else
                    strHavingWhere.AppendFormat(" AND SUM(TotalAmount)={0} ", searchInfo.TicketTotalAmount.Value);
            }
            #endregion

            #region 采购商用户名
            StringBuilder strUserNameWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(searchInfo.BuyerUName))
                strUserNameWhere.AppendFormat(" WHERE B.UserName ='{0}' ", searchInfo.BuyerUName);
            #endregion

            #endregion

            DbCommand cmd = this._db.GetSqlStringCommand(string.Format(cmdText.ToString()
                , strSupplierWhere.Length > 0 ? strSupplierWhere.ToString() : string.Empty
                , strHavingWhere.Length > 0 ? strHavingWhere.ToString() : string.Empty
                , strUserNameWhere.Length > 0 ? strUserNameWhere.ToString() : string.Empty));

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TicketStructure.BuyerAnalysisInfo model = new EyouSoft.Model.TicketStructure.BuyerAnalysisInfo();
                    model.BuyerCId = rdr["BuyerCId"].ToString();
                    model.BuyerUId = rdr["BuyerUId"].ToString();
                    model.BuyerUName = rdr["BuyerUName"].ToString();
                    model.BuyerContactName = rdr["BuyerContactName"].ToString();
                    model.BuyerCName = rdr["BuyerCName"].ToString();
                    model.BuyerRegisterTime = rdr.IsDBNull(rdr.GetOrdinal("BuyerRegisterTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("BuyerRegisterTime"));
                    model.BuyerRecentOrderTime = rdr.IsDBNull(rdr.GetOrdinal("BuyerRecentOrderTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("BuyerRecentOrderTime"));
                    model.BuyerContactMobile = rdr["BuyerContactMobile"].ToString();
                    model.BuyerMQ = rdr["BuyerMQ"].ToString();
                    model.TicketTotalCount = rdr.IsDBNull(rdr.GetOrdinal("TicketTotalCount")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("TicketTotalCount"));
                    model.TicketTotalAmount = rdr.IsDBNull(rdr.GetOrdinal("TotalAmount")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TotalAmount"));
                    model.StatTime = rdr.GetDateTime(rdr.GetOrdinal("StatTime"));
                    model.BuyerContactGender = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(rdr.GetString(rdr.GetOrdinal("BuyerContactGender")));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        /// <summary>
        /// 获取供应商订单统计信息集合
        /// </summary>
        /// <param name="supplierCId">供应商编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderStats(string supplierCId, EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo)
        {
            #region command text
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT ");
            cmdText.Append(" A.OrderId,A.OrderNo,A.BuyerCName,A.PCount,A.HomeCityId,A.DestCityId,A.LeaveTime,A.ReturnTime,A.FreightType,A.PayTime,A.TotalAmount,A.ElapsedTime ");
            cmdText.Append(" FROM [tbl_TicketOrder] AS A WHERE 1=1 ");
            cmdText.AppendFormat(" AND A.SupplierCId='{0}' ", supplierCId);

            if (searchInfo != null)
            {
                if (!string.IsNullOrEmpty(searchInfo.OrderNo))
                {
                    cmdText.AppendFormat(" AND A.OrderNo='{0}' ", searchInfo.OrderNo);
                }

                if (searchInfo.HomeCityId.HasValue)
                {
                    cmdText.AppendFormat(" AND A.HomeCityId={0} ", searchInfo.HomeCityId.Value);
                }

                if (searchInfo.DestCityId.HasValue)
                {
                    cmdText.AppendFormat(" AND A.DestCityId={0} ", searchInfo.DestCityId.Value);
                }

                if (searchInfo.OrderStartTime.HasValue)
                {
                    cmdText.AppendFormat(" AND A.OrderTime>='{0}' ", searchInfo.OrderStartTime.Value);
                }

                if (searchInfo.OrderFinishTime.HasValue)
                {
                    cmdText.AppendFormat(" AND A.OrderTime<='{0}' ", searchInfo.OrderFinishTime.Value);
                }

                if (searchInfo.OrderState.HasValue)
                {
                    cmdText.AppendFormat(" AND A.OrderState={0} ", (int)searchInfo.OrderState.Value);
                }

                if (searchInfo.OrderChangeType.HasValue || searchInfo.OrderChangeState.HasValue)
                {
                    cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_TicketOrderChange AS B WHERE B.OrderId=A.OrderId ");
                    if (searchInfo.OrderChangeType.HasValue)
                    {
                        cmdText.AppendFormat(" AND B.ChangeType={0} ", (int)searchInfo.OrderChangeType.Value);
                    }

                    if (searchInfo.OrderChangeState.HasValue)
                    {
                        cmdText.AppendFormat(" AND B.ChangeState={0} ", (int)searchInfo.OrderChangeState.Value);
                    }
                    cmdText.Append(" ) ");
                }
            }
            #endregion

            IList<EyouSoft.Model.TicketStructure.OrderInfo> stats = new List<EyouSoft.Model.TicketStructure.OrderInfo>();
            DbCommand cmd = this._db.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TicketStructure.OrderInfo()
                    {
                        OrderId = rdr.GetString(rdr.GetOrdinal("OrderId")),
                        OrderNo = rdr.GetString(rdr.GetOrdinal("OrderNo")),
                        BuyerCName = rdr["BuyerCName"].ToString(),
                        PCount = rdr.GetInt32(rdr.GetOrdinal("PCount")),
                        HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId")),
                        DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId")),
                        LeaveTime = rdr.GetDateTime(rdr.GetOrdinal("LeaveTime")),
                        ReturnTime = rdr.IsDBNull(rdr.GetOrdinal("ReturnTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("ReturnTime")),
                        FreightType = (EyouSoft.Model.TicketStructure.FreightType)rdr.GetByte(rdr.GetOrdinal("FreightType")),
                        PayTime = rdr.IsDBNull(rdr.GetOrdinal("PayTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("PayTime")),
                        TotalAmount = rdr.GetDecimal(rdr.GetOrdinal("TotalAmount")),
                        ElapsedTime = rdr.GetInt32(rdr.GetOrdinal("ElapsedTime"))
                    });
                }
            }

            return stats;
        }

        /// <summary>
        /// 更新订单采购商联系方式
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="buyerCName">公司名称</param>
        /// <param name="buyerContactName">联系人</param>
        /// <param name="buyerContactMobile">联系手机</param>
        /// <param name="buyerContactAddress">联系地址</param>
        /// <returns></returns>
        public virtual bool UpdateBuyerContact(string orderId, string buyerCName, string buyerContactName, string buyerContactMobile, string buyerContactAddress)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdateBuyerContact);
            this._db.AddInParameter(cmd, "BuyerCName", DbType.String, buyerCName);
            this._db.AddInParameter(cmd, "BuyerContactName", DbType.String, buyerContactName);
            this._db.AddInParameter(cmd, "BuyerContactMobile", DbType.String, buyerContactMobile);
            this._db.AddInParameter(cmd, "BuyerContactAddress", DbType.String, buyerContactAddress);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取供应商订单处理统计信息
        /// </summary>
        /// <param name="supplierCId">供应商编号</param>
        /// <returns></returns>
        public virtual IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> GetSupplierHandelStats(string supplierCId)
        {
            var stats = GetDefaultSupplierHandelStats();

            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetSupplierHandelStats);
            this._db.AddInParameter(cmd, "SupplierCId", DbType.String, supplierCId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                //订单
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        EyouSoft.Model.TicketStructure.OrderState orderState = (EyouSoft.Model.TicketStructure.OrderState)rdr.GetByte(1);
                        EyouSoft.Model.TicketStructure.OrderStatType orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待审核;

                        switch (orderState)
                        {
                            case EyouSoft.Model.TicketStructure.OrderState.等待审核: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待审核; break;
                            case EyouSoft.Model.TicketStructure.OrderState.支付成功: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待处理; break;
                        }

                        stats[orderStatType][(EyouSoft.Model.TicketStructure.RateType)rdr.GetByte(2)] = rdr.GetInt32(0).ToString();
                    }
                }

                //变更
                rdr.NextResult();
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        EyouSoft.Model.TicketStructure.OrderChangeType orderChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)rdr.GetByte(1);
                        EyouSoft.Model.TicketStructure.OrderStatType orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待退票;

                        switch (orderChangeType)
                        {
                            case EyouSoft.Model.TicketStructure.OrderChangeType.改期: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待改期; break;
                            case EyouSoft.Model.TicketStructure.OrderChangeType.改签: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待改签; break;
                            case EyouSoft.Model.TicketStructure.OrderChangeType.退票: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待退票; break;
                            case EyouSoft.Model.TicketStructure.OrderChangeType.作废: orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.待作废; break;
                        }

                        stats[orderStatType][(EyouSoft.Model.TicketStructure.RateType)rdr.GetByte(2)] = rdr.GetInt32(0).ToString();
                    }
                }

                //自愿
                rdr.NextResult();
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        EyouSoft.Model.TicketStructure.OrderStatType orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.自愿;

                        stats[orderStatType][(EyouSoft.Model.TicketStructure.RateType)rdr.GetByte(2)] = ((double)rdr.GetInt32(0) / 60 / 24).ToString();
                    }
                }

                //非自愿
                rdr.NextResult();
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        EyouSoft.Model.TicketStructure.OrderStatType orderStatType = EyouSoft.Model.TicketStructure.OrderStatType.非自愿;

                        stats[orderStatType][(EyouSoft.Model.TicketStructure.RateType)rdr.GetByte(2)] = ((double)rdr.GetInt32(0) / 60 / 24).ToString();
                    }
                }
            }

            return stats;
        }

        /// <summary>
        /// 更新订单服务备注
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="note">服务备注</param>
        /// <returns></returns>
        public virtual bool UpdateServiceNote(string orderId, string note)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdateServiceNote);
            this._db.AddInParameter(cmd, "ServiceNote", DbType.String, note);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 设置供应商收款金额
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="amount">供应商收款金额</param>
        /// <returns></returns>
        public virtual bool SetBalanceAmount(string orderId, decimal amount)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_SetBalanceAmount);
            this._db.AddInParameter(cmd, "Amount", DbType.Decimal, amount);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 修改订单采购商备注(下单时特殊要求备注)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="buyerRemark">备注</param>
        /// <returns></returns>
        public virtual bool UpdateBuyerRemark(string orderId, string buyerRemark)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_UpdateBuyerRemark);
            this._db.AddInParameter(cmd, "BuyerRemark", DbType.String, buyerRemark);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 写入订单账户信息(注：采购商订单支付时一定调用)
        /// </summary>
        /// <param name="orderAccountInfo">订单账户信息业务实体</param>
        /// <returns></returns>
        public virtual bool InsertOrderAccount(EyouSoft.Model.TicketStructure.OrderAccountInfo orderAccountInfo)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_INSERT_InsertOrderAccount);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderAccountInfo.OrderId);
            this._db.AddInParameter(cmd, "PayCompanyId", DbType.AnsiStringFixedLength, orderAccountInfo.PayCompanyId);
            this._db.AddInParameter(cmd, "PayUserId", DbType.AnsiStringFixedLength, orderAccountInfo.PayUserId);
            this._db.AddInParameter(cmd, "PayType", DbType.Byte, orderAccountInfo.PayType);
            this._db.AddInParameter(cmd, "PayAccount", DbType.String, orderAccountInfo.PayAccount);
            this._db.AddInParameter(cmd, "SellCompanyId", DbType.AnsiStringFixedLength, orderAccountInfo.SellCompanyId);
            this._db.AddInParameter(cmd, "SellAccount", DbType.String, orderAccountInfo.SellAccount);
            this._db.AddInParameter(cmd, "OrderNo", DbType.String, orderAccountInfo.OrderNo);
            this._db.AddInParameter(cmd, "Discount", DbType.Decimal, orderAccountInfo.Discount);
            this._db.AddInParameter(cmd, "PayPrice", DbType.Decimal, orderAccountInfo.PayPrice);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取订单账户信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.OrderAccountInfo GetOrderAccountInfo(string orderId)
        {
            EyouSoft.Model.TicketStructure.OrderAccountInfo orderAccountInfo = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetOrderAccountInfo);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    orderAccountInfo = new EyouSoft.Model.TicketStructure.OrderAccountInfo()
                    {
                        OrderId = orderId,
                        PayAccount = rdr["PayAccount"].ToString(),
                        PayCompanyId = rdr.GetString(rdr.GetOrdinal("PayCompanyId")),
                        PayNumber = rdr["PayNumber"].ToString(),
                        PayType = (EyouSoft.Model.TicketStructure.TicketAccountType)rdr.GetByte(rdr.GetOrdinal("PayType")),
                        PayUserId = rdr.GetString(rdr.GetOrdinal("PayUserId")),
                        SellAccount = rdr.GetString(rdr.GetOrdinal("SellAccount")),
                        SellCompanyId = rdr.GetString(rdr.GetOrdinal("SellCompanyId")),
                        Discount = decimal.Parse(rdr["Discount"].ToString()),
                        OrderNo=rdr.GetString(rdr.GetOrdinal("OrderNo"))
                    };
                }
            }

            return orderAccountInfo;
        }

        /// <summary>
        /// 获取最新的订单变更信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.OrderChangeInfo GetLatestChange(string orderId)
        {
            EyouSoft.Model.TicketStructure.OrderChangeInfo change = null;
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetLatestChange);
            this._db.AddInParameter(cmd, "OrderId", DbType.AnsiStringFixedLength, orderId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    change = new EyouSoft.Model.TicketStructure.OrderChangeInfo()
                    {
                        ChangeId = rdr.GetString(rdr.GetOrdinal("ChangeId")),
                        ChangeRemark = rdr["ChangeRemark"].ToString(),
                        ChangeState = (EyouSoft.Model.TicketStructure.OrderChangeState)rdr.GetByte(rdr.GetOrdinal("ChangeState")),
                        ChangeTime = rdr.GetDateTime(rdr.GetOrdinal("ChangeTime")),
                        ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)rdr.GetByte(rdr.GetOrdinal("ChangeType")),
                        ChangeUId = rdr.GetString(rdr.GetOrdinal("ChangeUId")),
                        CheckRemark = rdr["CheckRemark"].ToString(),
                        CheckTime = rdr.IsDBNull(rdr.GetOrdinal("CheckTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("CheckTime")),
                        CheckUId = rdr["CheckUId"].ToString(),
                        OrderId = orderId,
                        RefundTicketType = (EyouSoft.Model.TicketStructure.RefundTicketType)rdr.GetByte(rdr.GetOrdinal("RefundTicketType")),
                        PCount = rdr.GetInt32(rdr.GetOrdinal("PeopleNum"))
                    };
                }
            }

            return change;
        }

        /// <summary>
        /// 采购商获取订单报表
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.OrderInfo> GetBuyerOrderReports(string buyerCId, DateTime? startTime, DateTime? finishTime)
        {
            IList<EyouSoft.Model.TicketStructure.OrderInfo> orders = new List<EyouSoft.Model.TicketStructure.OrderInfo>();
            StringBuilder cmdText = new StringBuilder();

            #region command text
            cmdText.Append(" SELECT ");
            cmdText.Append(" OrderId,OrderNo,OPNR,PNR,HomeCityId,DestCityId,FreightType,FlightId,LFlightCode,RFlightCode,TotalAmount,LeaveTime,ReturnTime,PayType,PayTime,BalanceAmount,RefundAmount,RefundTotalAmount,PCount,LastOperatorName ");
            cmdText.Append(" ,LeaveFacePrice,LeaveDiscount,LeavePrice,ReturnFacePrice,ReturnDiscount,ReturnPrice,LFuelPrice,LBuildPrice,RFuelPrice,RBuildPrice ");
            cmdText.Append(" ,Travellers ");
            cmdText.Append(" FROM view_TicketOrder_BuyerOrderReports ");
            cmdText.AppendFormat(" WHERE BuyerCId='{0}' ", buyerCId);
            if (startTime.HasValue)
            {
                cmdText.AppendFormat(" AND OrderTime>='{0}' ", startTime.Value);
            }
            if (finishTime.HasValue)
            {
                cmdText.AppendFormat(" AND OrderTime<='{0}' ", finishTime.Value);
            }
            #endregion
            
            DbCommand cmd = this._db.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TicketStructure.OrderInfo order = new EyouSoft.Model.TicketStructure.OrderInfo();

                    order.OrderId = rdr.GetString(rdr.GetOrdinal("OrderId"));
                    order.OrderNo = rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    order.PNR = rdr["PNR"].ToString();
                    order.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    order.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    order.FreightType = (EyouSoft.Model.TicketStructure.FreightType)rdr.GetByte(rdr.GetOrdinal("FreightType"));
                    order.FlightId = rdr.GetInt32(rdr.GetOrdinal("FlightId"));
                    order.LFlightCode = rdr["LFlightCode"].ToString();
                    order.RFlightCode = rdr["RFlightCode"].ToString();
                    order.TotalAmount = rdr.GetDecimal(rdr.GetOrdinal("TotalAmount"));
                    order.LeaveTime = rdr.GetDateTime(rdr.GetOrdinal("LeaveTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ReturnTime")))
                        order.ReturnTime = rdr.GetDateTime(rdr.GetOrdinal("ReturnTime"));
                    order.PayType = (EyouSoft.Model.TicketStructure.TicketAccountType)rdr.GetByte(rdr.GetOrdinal("PayType"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("PayTime")))
                        order.PayTime = rdr.GetDateTime(rdr.GetOrdinal("PayTime"));
                    order.BalanceAmount = rdr.GetDecimal(rdr.GetOrdinal("BalanceAmount"));
                    order.RefundAmount = rdr.GetDecimal(rdr.GetOrdinal("RefundAmount"));
                    order.RefundTotalAmount = rdr.GetDecimal(rdr.GetOrdinal("RefundTotalAmount"));
                    order.PCount = rdr.GetInt32(rdr.GetOrdinal("PCount"));
                    order.LastOperatorName = rdr["LastOperatorName"].ToString();

                    EyouSoft.Model.TicketStructure.OrderRateInfo rate = new EyouSoft.Model.TicketStructure.OrderRateInfo();
                    rate.DestCityId = rdr.GetInt32(rdr.GetOrdinal("DestCityId"));
                    rate.FlightId = rdr.GetInt32(rdr.GetOrdinal("FlightId"));
                    rate.FreightType = (EyouSoft.Model.TicketStructure.FreightType)rdr.GetByte(rdr.GetOrdinal("FreightType"));
                    rate.HomeCityId = rdr.GetInt32(rdr.GetOrdinal("HomeCityId"));
                    rate.LBuildPrice = rdr.GetDecimal(rdr.GetOrdinal("LBuildPrice"));
                    rate.LeaveDiscount = decimal.Parse(rdr["LeaveDiscount"].ToString());
                    rate.LeaveFacePrice = rdr.GetDecimal(rdr.GetOrdinal("LeaveFacePrice"));
                    rate.LeavePrice = rdr.GetDecimal(rdr.GetOrdinal("LeavePrice"));
                    rate.LFuelPrice = rdr.GetDecimal(rdr.GetOrdinal("LFuelPrice"));
                    rate.RBuildPrice = rdr.GetDecimal(rdr.GetOrdinal("RBuildPrice"));
                    rate.ReturnDiscount = decimal.Parse(rdr["ReturnDiscount"].ToString());
                    rate.ReturnFacePrice = rdr.GetDecimal(rdr.GetOrdinal("ReturnFacePrice"));
                    rate.ReturnPrice = rdr.GetDecimal(rdr.GetOrdinal("ReturnPrice"));
                    order.OrderRateInfo = rate;

                    order.Travellers = this.GetTravellersByXml(rdr["Travellers"].ToString());

                    orders.Add(order);
                }
            }

            return orders;
        }

        /// <summary>
        /// 设置变更金额
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="handlingFee">手续费</param>
        /// <param name="totalAmount">供应商变更总金额</param>
        /// <param name="changeAmount">变更总金额</param>
        /// <returns></returns>
        public virtual bool SetChangeAmount(string changeId, decimal handlingFee, decimal changeAmount, decimal totalAmount)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_SetChangeAmount);
            this._db.AddInParameter(cmd, "HandlingFee", DbType.Decimal, handlingFee);
            this._db.AddInParameter(cmd, "ChangeAmount", DbType.Decimal, changeAmount);
            this._db.AddInParameter(cmd, "TotalAmount", DbType.Decimal, totalAmount);
            this._db.AddInParameter(cmd, "ChangeId", DbType.AnsiStringFixedLength, changeId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 采购商获取订单报表(合计)
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo GetBuyerOrderReportsTotal(string buyerCId, DateTime? startTime, DateTime? finishTime)
        {
            EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo report = new EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo();
            StringBuilder cmdText = new StringBuilder();

            #region command text
            cmdText.Append(" SELECT ");

            cmdText.Append(" SUM(LeaveFacePrice*PCount) AS LFacePrice ");
            cmdText.Append(" ,SUM(ReturnFacePrice*PCount) AS RFacePrice ");
            cmdText.Append(" ,SUM(LFuelPrice*PCount) AS LFuelPrice ");
            cmdText.Append(" ,SUM(RFuelPrice*PCount) AS RFuelPrice ");
            cmdText.Append(" ,SUM(LBuildPrice*PCount) AS LBuildPrice ");
            cmdText.Append(" ,SUM(RBuildPrice*PCount) AS RBuildPrice ");
            cmdText.Append(" ,SUM(PCount) AS PCount ");
            cmdText.Append(" ,SUM(TotalAmount) AS TotalAmount ");
            cmdText.Append(" ,SUM(BalanceAmount) AS BalanceAmount ");
            cmdText.Append(" ,SUM(RefundAmount) AS RefundAmount ");
            cmdText.Append(" ,SUM(RefundTotalAmount) AS RefundTotalAmount ");
            cmdText.Append(" ,SUM(LeaveFacePrice*LeaveDiscount*PCount) AS LAgencyAmount ");
            cmdText.Append(" ,SUM(ReturnFacePrice*ReturnDiscount*PCount) AS RAgencyAmount ");

            cmdText.Append(" FROM view_TicketOrder_BuyerOrderReports ");
            cmdText.AppendFormat(" WHERE BuyerCId='{0}' ", buyerCId);
            if (startTime.HasValue)
            {
                cmdText.AppendFormat(" AND OrderTime>='{0}' ", startTime.Value);
            }
            if (finishTime.HasValue)
            {
                cmdText.AppendFormat(" AND OrderTime<='{0}' ", finishTime.Value);
            }
            #endregion

            DbCommand cmd = this._db.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    report.BalanceAmount = this.decimalParse(rdr["BalanceAmount"].ToString());
                    report.LAgencyAmount = this.decimalParse(rdr["LAgencyAmount"].ToString());
                    report.LBuildPrice = this.decimalParse(rdr["LBuildPrice"].ToString());
                    report.LFacePrice = this.decimalParse(rdr["LFacePrice"].ToString());
                    report.LFuelPrice = this.decimalParse(rdr["LFuelPrice"].ToString());
                    report.Pcount = this.intParse(rdr["Pcount"].ToString());
                    report.RAgencyAmount = this.decimalParse(rdr["RAgencyAmount"].ToString());
                    report.RBuildPrice = this.decimalParse(rdr["RBuildPrice"].ToString());
                    report.RefundAmount = this.decimalParse(rdr["RefundAmount"].ToString());
                    report.RefundTotalAmount = this.decimalParse(rdr["RefundTotalAmount"].ToString());
                    report.RFacePrice = this.decimalParse(rdr["RFacePrice"].ToString());
                    report.RFuelPrice = this.decimalParse(rdr["RFuelPrice"].ToString());
                    report.TotalAmount = this.decimalParse(rdr["TotalAmount"].ToString());
                }
            }

            return report;
        }

        /// <summary>
        /// 设置订单旅客变更审核备注
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public virtual bool SetChangeCheckRemark(string changeId, string remark)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_UPDATE_SetChangeCheckRemark);
            this._db.AddInParameter(cmd, "CheckRemark", DbType.String, remark);
            this._db.AddInParameter(cmd, "ChangeId", DbType.AnsiStringFixedLength, changeId);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }
        #endregion
    }
}
