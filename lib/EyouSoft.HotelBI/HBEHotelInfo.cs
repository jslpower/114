using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.HotelBI
{
    #region 单酒店查询

    /// <summary>
    /// 单酒店查询实体
    /// </summary>
    [Serializable]
    public class SingleSeach
    {
        #region 查询实体

        private AvailReqTypeEnum _AvailReqType = AvailReqTypeEnum.noneStatics;

        /// <summary>
        /// 查询完整的酒店信息还是只查询价格、房态、政策等动态信息
        /// </summary>
        public AvailReqTypeEnum AvailReqType { get { return _AvailReqType; } set { _AvailReqType = value; } }

        /// <summary>
        /// 入住时间 yyyy-mm-dd 必填
        /// </summary>
        public string CheckInDate { get; set; }

        /// <summary>
        /// 离店时间 yyyy-mm-dd 必填
        /// </summary>
        public string CheckOutDate { get; set; }

        /// <summary>
        /// 要查询的价格计划代码
        /// </summary>
        public string RatePlanCode { get; set; }

        /// <summary>
        /// 要查询的价格计划类型
        /// </summary>
        public string RatePlanType { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string VendorCode { get; set; }

        #region 价格查询

        /// <summary>
        /// 查询的价格上限 Num(8,2)
        /// </summary>
        public decimal? PriceMaxRate { get; set; }
        /// <summary>
        /// 查询的价格下限 Num(8,2)
        /// </summary>
        public decimal? PriceMinRate { get; set; }

        #endregion

        #region 酒店地理位置查询

        #region 经纬度查询

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        #endregion

        /// <summary>
        /// 地理位置(地标)
        /// </summary>
        public string LandMark { get; set; }
        /// <summary>
        /// 到中心点距离 接口预留
        /// </summary>
        public string Radius { get; set; }

        #endregion

        /// <summary>
        /// 酒店电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }

        /// <summary>
        /// 装修时间
        /// </summary>
        public string Fitment { get; set; }

        /// <summary>
        /// 开业时间
        /// </summary>
        public string OpenDate { get; set; }

        /// <summary>
        /// 景观 如：海景、山景
        /// </summary>
        public string RoomView { get; set; }

        /// <summary>
        /// 房型代码
        /// </summary>
        public string RoomTypeCode { get; set; }

        private BoolEnum _Internet = BoolEnum.none;

        /// <summary>
        /// 上网 Y/N
        /// </summary>
        public BoolEnum Internet { get { return _Internet; } set { _Internet = value; } }

        private BoolEnum _NoSmoking = BoolEnum.none;

        /// <summary>
        /// 吸烟 Y/N
        /// </summary>
        public BoolEnum NoSmoking { get { return _NoSmoking; } set { _NoSmoking = value; } }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomQuantity { get; set; }

        /// <summary>
        /// 床型(如：双床，大床，三床，大/双)
        /// </summary>
        public string BedType { get; set; }

        private QualityLevelEnum _QualityLevel = QualityLevelEnum.None;

        /// <summary>
        /// 装修级别
        /// </summary>
        public QualityLevelEnum QualityLevel { get { return _QualityLevel; } set { _QualityLevel = value; } }

        /// <summary>
        /// 房间最大容纳人数
        /// </summary>
        public int RoomTypeGuestMaxNum { get; set; }

        private HotelRankEnum _HotelRank = HotelRankEnum._00;

        /// <summary>
        /// 酒店星级 特殊处理 因为枚举命名不能以数字开头,所以结果要过滤 下划线(_)
        /// </summary>
        public HotelRankEnum HotelRank { get { return _HotelRank; } set { _HotelRank = value; } }

        #endregion

        /// <summary>
        /// 单酒店查询XML
        /// </summary>
        public string SingleRequestXML
        {
            get
            {
                return Utils.CreateRequestXML(Utils.TH_HotelSingleAvailRQ, GetSingleRequestXML());
            }
        }

        #region 私有方法

        /// <summary>
        /// 构造单酒店查询XML
        /// </summary>
        /// <returns>单酒店查询XML</returns>
        private string GetSingleRequestXML()
        {
            StringBuilder NodeInfo = new StringBuilder();

            #region Build XML

            NodeInfo.Append("<HotelAvailRQ>");
            NodeInfo.Append("<HotelAvailCriteria AvailReqType=\"" + Enum.GetName(typeof(AvailReqTypeEnum), AvailReqType) + "\">");
            NodeInfo.Append("<StayDateRange Duration=\"\"  CheckOutDate=\"" + CheckOutDate + "\" CheckInDate=\"" + CheckInDate + "\"/>");
            NodeInfo.Append("<RatePlanCandidates>");
            NodeInfo.Append("<RatePlanCandidate");
            if (!String.IsNullOrEmpty(RatePlanCode))
                NodeInfo.Append(" RatePlanCode=\"" + RatePlanCode + "\"");
            if (!String.IsNullOrEmpty(RatePlanType))
                NodeInfo.Append(" RatePlanType=\"" + RatePlanType + "\"");
            NodeInfo.Append(">");
            //指定供应商 开始
            if (!String.IsNullOrEmpty(VendorCode))
            {
                NodeInfo.Append("<VendorsIncluded>");
                NodeInfo.Append("<Vendor VendorCode=\"" + VendorCode + "\"/>");
                NodeInfo.Append("</VendorsIncluded>");
            }
            //指定供应商 结束
            NodeInfo.Append("</RatePlanCandidate>");
            NodeInfo.Append("</RatePlanCandidates>");
            //价格
            NodeInfo.Append("<RateRange");
            if (PriceMaxRate.HasValue)
                NodeInfo.Append(" MaxRate=\"" + PriceMaxRate + "\"");
            if (PriceMinRate.HasValue)
                NodeInfo.Append(" MinRate=\"" + PriceMinRate + "\"");
            NodeInfo.Append("/>");
            NodeInfo.Append("<HotelSearchCriteria>");
            //经纬度
            NodeInfo.Append("<Position Longitude=\"" + Longitude + "\" Latitude=\"" + Latitude + "\" />");
            //地理位置（地标）
            if (!String.IsNullOrEmpty(LandMark))
                NodeInfo.Append("<LandMark>" + LandMark + "</LandMark>");
            if (!String.IsNullOrEmpty(PhoneNumber))
                NodeInfo.Append("<PhoneNumber>" + PhoneNumber + "</PhoneNumber>");
            if (!String.IsNullOrEmpty(Radius))
                NodeInfo.Append("<Radius>" + Radius + "</adius>");
            NodeInfo.Append("<HotelRef");
            if (!String.IsNullOrEmpty(HotelCode))
                NodeInfo.Append(" HotelCode=\"" + HotelCode + "\"");
            NodeInfo.Append(" />");
            if (!String.IsNullOrEmpty(Fitment))
                NodeInfo.Append("<Fitment>" + Fitment + "</Fitment>");
            if (!String.IsNullOrEmpty(OpenDate))
                NodeInfo.Append("<OpenDate>" + OpenDate + "</OpenDate>");
            NodeInfo.Append("<RoomStayCandidates>");
            NodeInfo.Append("<RoomStayCandidate");
            if (!String.IsNullOrEmpty(RoomView))
                NodeInfo.Append(" RoomView=\"" + RoomView + "\"");
            if (Internet != BoolEnum.none)
                NodeInfo.Append(" Internet=\"" + Enum.GetName(typeof(BoolEnum), Internet) + "\"");
            if (NoSmoking != BoolEnum.none)
                NodeInfo.Append(" NoSmoking=\"" + Enum.GetName(typeof(BoolEnum), NoSmoking) + "\"");
            if (RoomQuantity > 0)
                NodeInfo.Append(" Quantity=\"" + RoomQuantity.ToString() + "\"");
            if (!String.IsNullOrEmpty(BedType))
                NodeInfo.Append(" BedType=\"" + BedType + "\"");
            if (!String.IsNullOrEmpty(RoomTypeCode))
                NodeInfo.Append(" RoomTypeCode=\"" + RoomTypeCode + "\"");
            NodeInfo.Append(">");
            NodeInfo.Append("<RoomAmenity");
            if (QualityLevel != QualityLevelEnum.None)
                NodeInfo.Append(" QualityLevel=\"" + Enum.GetName(typeof(QualityLevelEnum), QualityLevel) + "\"");
            if (RoomTypeGuestMaxNum > 0)
                NodeInfo.Append(" RoomTypeGuestMaxNum=\"" + RoomTypeGuestMaxNum.ToString() + "\"");
            NodeInfo.Append(" />");
            NodeInfo.Append("</RoomStayCandidate>");
            NodeInfo.Append("</RoomStayCandidates>");
            //星级
            if (HotelRank != HotelRankEnum._00)
                NodeInfo.Append("<Rank>" + Enum.GetName(typeof(HotelRankEnum), HotelRank).Substring(1) + "</Rank>");
            NodeInfo.Append("</HotelSearchCriteria>");
            NodeInfo.Append("</HotelAvailCriteria>");
            NodeInfo.Append("</HotelAvailRQ>");

            #endregion

            return NodeInfo.ToString();
        }

        #endregion
    }

    #endregion

    #region 多酒店查询

    /// <summary>
    /// 多酒店查询条件
    /// </summary>
    /// 注 有些 实体属性声明为 private 是接口预留,并注明接口预留
    [Serializable]
    public class MultipleSeach : SingleSeach
    {
        #region 查询实体

        private bool _ConfirmRightNowIndicator = false;

        /// <summary>
        /// 查立即确认的房型 True/False(默认值)
        /// </summary>
        public bool ConfirmRightNowIndicator { get { return _ConfirmRightNowIndicator; } set { _ConfirmRightNowIndicator = value; } }

        private string _Payment = "T";
        /// <summary>
        /// 支付方式 （前台现付：T，代收代付：S，预付：Y） 目前只支持 前台现付,不提供SET方式
        /// </summary>
        public string Payment { get { return _Payment; } }

        #region 供应商 接口预留

        private string _IsDefinedChannel = "Y";

        /// <summary>
        /// 选择只授权给该渠道的价格计划 Y
        /// </summary>
        public string IsDefinedChannel { get { return _IsDefinedChannel; } }

        private string _IsDefinedVendor = "Y";

        /// <summary>
        /// 选择只授权给该渠道的价格计划 Y
        /// </summary>
        public string IsDefinedVendor { get { return _IsDefinedVendor; } }

        #endregion

        #region 价格查询

        private string _PriceCurrency = "CNY";

        /// <summary>
        /// 货币类型 目前只有 人民币（CNY），如扩充请参考国际货币编码
        /// </summary>
        public string PriceCurrency { get { return _PriceCurrency; } set { _PriceCurrency = value; } }

        #endregion

        #region 酒店地理位置查询

        #region 经纬度查询

        /// <summary>
        /// 经度上限
        /// </summary>
        public string MaximumLongitude { get; set; }
        /// <summary>
        /// 经度下限
        /// </summary>
        public string MinimumLongitude { get; set; }
        /// <summary>
        /// 纬度上限
        /// </summary>
        public string MaximumLatitude { get; set; }
        /// <summary>
        /// 纬度下限
        /// </summary>
        public string MinimumLatitude { get; set; }

        #endregion

        /// <summary>
        /// 城市代码    必填
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 行政区
        /// </summary>
        public string District { get; set; }

        #endregion

        private BoolEnum _AirReception = BoolEnum.none;

        /// <summary>
        /// 是否接机
        /// </summary>
        public BoolEnum AirReception { get { return _AirReception; } set { _AirReception = value; } }

        /// <summary>
        /// 酒店品牌名称
        /// </summary>
        public string HotelBrandName { get; set; }

        /// <summary>
        /// 连锁酒店集团代码
        /// </summary>
        public string HotelChainCode { get; set; }

        /// <summary>
        /// 连锁酒店集团名称
        /// </summary>
        public string HotelChainName { get; set; }

        /// <summary>
        /// 酒店品牌代码
        /// </summary>
        public string HotelBrandCode { get; set; }

        /// <summary>
        /// 酒店中文名称
        /// </summary>
        public string HotelChineseName { get; set; }

        /// <summary>
        /// 酒店英文名称
        /// </summary>
        public string HotelEnglishName { get; set; }

        /// <summary>
        /// 房间名称(房型,如：蜜月，海景，山景，标准，商务)
        /// </summary>
        public string RoomName { get; set; }

        #region 查询页码条件实体类

        private int _NumOfEachPage = 8;

        /// <summary>
        /// 每页要求记录数
        /// </summary>
        public int NumOfEachPage { get { return _NumOfEachPage; } set { _NumOfEachPage = value; } }

        private int _PageNo = 1;

        /// <summary>
        /// 申请页数
        /// </summary>
        public int PageNo { get { return _PageNo; } set { _PageNo = value; } }

        private bool _IsPageView = true;

        /// <summary>
        /// 是否需要分页(如果不填写，默认查询第一页，每页10条信息。如填Y，以上两项应填入具体值；不分页须填入N)
        /// </summary>
        public bool IsPageView { get { return _IsPageView; } set { _IsPageView = value; } }

        #endregion

        private bool _RoomTypeDetailShowed = false;

        /// <summary>
        /// 是否展示房型具体内容（是：Y，如果展示，查询时间将会增加）
        /// </summary>
        public bool RoomTypeDetailShowed { get { return _RoomTypeDetailShowed; } set { _RoomTypeDetailShowed = value; } }

        private HotelOrderBy _OrderBy = HotelOrderBy.Default;

        /// <summary>
        /// 排序方式
        /// </summary>
        public HotelOrderBy OrderBy { get { return _OrderBy; } set { _OrderBy = value; } }

        #endregion

        /// <summary>
        /// 多酒店查询XML
        /// </summary>
        public string MultiRequestXML
        {
            get
            {
                return Utils.CreateRequestXML(Utils.TH_HotelMultiAvailRQ, GetMultiRequestXML());
            }
        }

        #region 私有方法

        /// <summary>
        /// 构造多酒店查询XML
        /// </summary>
        /// <returns>多酒店查询XML</returns>
        private string GetMultiRequestXML()
        {
            StringBuilder NodeInfo = new StringBuilder();

            #region Build XML

            NodeInfo.Append("<HotelAvailRQ>");
            NodeInfo.Append("<HotelAvailCriteria AvailReqType=\"" + Enum.GetName(typeof(AvailReqTypeEnum), AvailReqType) + "\">");
            NodeInfo.Append("<ConfirmRightNowIndicator>" + Utils.BoolToYesOrNo(ConfirmRightNowIndicator) + "</ConfirmRightNowIndicator>");
            NodeInfo.Append("<StayDateRange Duration=\"\"  CheckOutDate=\"" + CheckOutDate + "\" CheckInDate=\"" + CheckInDate + "\"/>");
            NodeInfo.Append("<RatePlanCandidates>");
            NodeInfo.Append("<RatePlanCandidate RatePlanCode=\"" + RatePlanCode + "\" RatePlanType=\"" + RatePlanType + "\" Payment=\"" + Payment + "\">");
            //指定供应商 开始
            if (!String.IsNullOrEmpty(VendorCode))
            {
                NodeInfo.Append("<VendorsIncluded>");
                NodeInfo.Append("<Vendor VendorCode=\"" + VendorCode + "\"/>");
                NodeInfo.Append("</VendorsIncluded>");
                NodeInfo.Append("<IsDefinedChannel>" + IsDefinedChannel + "</IsDefinedChannel>");
                NodeInfo.Append("<IsDefinedVendor>" + IsDefinedVendor + "</IsDefinedVendor>");
            }
            //指定供应商 结束
            NodeInfo.Append("</RatePlanCandidate>");
            NodeInfo.Append("</RatePlanCandidates>");
            //价格
            NodeInfo.Append("<RateRange MaxRate=\"" + (PriceMaxRate.HasValue ? PriceMaxRate.ToString() : string.Empty) + "\" Currency=\"" + PriceCurrency + "\" MinRate=\"" + (PriceMinRate.HasValue ? PriceMinRate.ToString() : string.Empty) + "\"/>");
            NodeInfo.Append("<HotelSearchCriteria>");
            //经纬度
            NodeInfo.Append("<Position LongitudeFrom=\"" + MinimumLongitude + "\" LatitudeFrom=\"" + MinimumLatitude + "\" Longitude=\"" + Longitude + "\" LongitudeTo=\"" + MaximumLongitude + "\" Latitude=\"" + Latitude + "\" LatitudeTo=\"" + MaximumLatitude + "\"/>");
            //地理位置（地标）
            if (!String.IsNullOrEmpty(LandMark))
                NodeInfo.Append("<LandMark>" + LandMark + "</LandMark>");
            if (!String.IsNullOrEmpty(PhoneNumber))
                NodeInfo.Append("<PhoneNumber>" + PhoneNumber + "</PhoneNumber>");
            if (!String.IsNullOrEmpty(Radius))
                NodeInfo.Append("<Radius>" + Radius + "</Radius>");
            NodeInfo.Append("<HotelRef");
            if (!String.IsNullOrEmpty(District))
                NodeInfo.Append(" District=\"" + District + "\"");
            NodeInfo.Append(" CityCode=\"" + CityCode + "\"");
            if (!String.IsNullOrEmpty(HotelBrandName))
                NodeInfo.Append(" HotelBrandName=\"" + HotelBrandName + "\"");
            if (AirReception != BoolEnum.none)
                NodeInfo.Append(" AirReception=\"" + Enum.GetName(typeof(BoolEnum), AirReception) + "\"");
            if (!String.IsNullOrEmpty(HotelChainCode))
                NodeInfo.Append(" HotelChainCode=\"" + HotelChainCode + "\"");
            if (!String.IsNullOrEmpty(HotelCode))
                NodeInfo.Append(" HotelCode=\"" + HotelCode + "\"");
            if (!String.IsNullOrEmpty(HotelEnglishName))
                NodeInfo.Append(" HotelEnglishName=\"" + HotelEnglishName + "\"");
            if (!String.IsNullOrEmpty(HotelBrandCode))
                NodeInfo.Append(" HotelBrandCode=\"" + HotelBrandCode + "\"");
            if (!String.IsNullOrEmpty(HotelChineseName))
                NodeInfo.Append(" HotelChineseName=\"" + HotelChineseName + "\"");
            if (!String.IsNullOrEmpty(HotelChainName))
                NodeInfo.Append(" HotelChainName=\"" + HotelChainName + "\"");
            NodeInfo.Append(" />");
            if (!String.IsNullOrEmpty(Fitment))
                NodeInfo.Append("<Fitment>" + Fitment + "</Fitment>");
            if (!String.IsNullOrEmpty(OpenDate))
                NodeInfo.Append("<OpenDate>" + OpenDate + "</OpenDate>");
            NodeInfo.Append("<RoomStayCandidates>");
            NodeInfo.Append("<RoomStayCandidate");
            if (!String.IsNullOrEmpty(RoomView))
                NodeInfo.Append(" RoomView=\"" + RoomView + "\"");
            if (!String.IsNullOrEmpty(RoomName))
                NodeInfo.Append(" RoomName=\"" + RoomName + "\"");
            if (Internet != BoolEnum.none)
                NodeInfo.Append(" Internet=\"" + Enum.GetName(typeof(BoolEnum), Internet) + "\"");
            if (NoSmoking != BoolEnum.none)
                NodeInfo.Append(" NoSmoking=\"" + Enum.GetName(typeof(BoolEnum), NoSmoking) + "\"");
            if (RoomQuantity > 0)
                NodeInfo.Append(" Quantity=\"" + RoomQuantity.ToString() + "\"");
            if (!String.IsNullOrEmpty(BedType))
                NodeInfo.Append(" BedType=\"" + BedType + "\"");
            NodeInfo.Append(">");
            NodeInfo.Append("<RoomAmenity");
            if (QualityLevel != QualityLevelEnum.None)
                NodeInfo.Append(" QualityLevel=\"" + Enum.GetName(typeof(QualityLevelEnum), QualityLevel) + "\"");
            if (RoomTypeGuestMaxNum > 0)
                NodeInfo.Append(" RoomTypeGuestMaxNum=\"" + RoomTypeGuestMaxNum.ToString() + "\"");
            NodeInfo.Append(" />");
            NodeInfo.Append("</RoomStayCandidate>");
            NodeInfo.Append("</RoomStayCandidates>");
            //星级
            if (HotelRank != HotelRankEnum._00)
                NodeInfo.Append("<Rank>" + Enum.GetName(typeof(HotelRankEnum), HotelRank).Substring(1) + "</Rank>");
            NodeInfo.Append("</HotelSearchCriteria>");
            //分页信息
            NodeInfo.Append("<ReqPageInfo>");
            NodeInfo.Append("<ReqNumOfEachPage>" + NumOfEachPage.ToString() + "</ReqNumOfEachPage>");
            NodeInfo.Append("<ReqPageNo>" + PageNo.ToString() + "</ReqPageNo>");
            NodeInfo.Append("<IsPageView>" + Utils.BoolToYesOrNo(IsPageView) + "</IsPageView>");
            NodeInfo.Append("</ReqPageInfo>");
            //是否显示房型
            NodeInfo.Append("<RoomTypeDetailShowed>" + Utils.BoolToYesOrNo(RoomTypeDetailShowed) + "</RoomTypeDetailShowed>");
            //排序
            if (OrderBy != HotelOrderBy.Default)
                NodeInfo.Append("<OrderBy>" + Enum.GetName(typeof(HotelOrderBy), OrderBy) + "</OrderBy>");
            NodeInfo.Append("</HotelAvailCriteria>");
            NodeInfo.Append("</HotelAvailRQ>");

            #endregion

            return NodeInfo.ToString();
        }

        #endregion
    }

    #endregion
}
