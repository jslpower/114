using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.HotelBI
{
    #region 酒店系统订单状态
    /// <summary>
    /// 酒店系统订单状态
    /// </summary>
    public enum HBEResStatus
    {
        /// <summary>
        /// 确认单
        /// </summary>
        CON,
        /// <summary>
        /// 申请单
        /// </summary>
        RES,
        /// <summary>
        /// 修改单
        /// </summary>
        MOD,
        /// <summary>
        /// 取消单
        /// </summary>
        CAN,
        /// <summary>
        /// 拒绝单
        /// </summary>
        HAC,
        /// <summary>
        /// 测试
        /// </summary>
        TST,
        /// <summary>
        /// 客户取消
        /// </summary>
        XXX,
        /// <summary>
        /// 房间确认
        /// </summary>
        RCM
    }
    #endregion

    #region 酒店系统支付方式
    /// <summary>
    /// 酒店系统支付方式
    /// </summary>
    public enum HBEPaymentType
    {
        /// <summary>
        /// 前台现付
        /// </summary>
        T,
        /// <summary>
        /// 代收代付
        /// </summary>
        S,
        /// <summary>
        /// 预付
        /// </summary>
        Y
    }
    #endregion

    #region 酒店系统佣金类型
    /// <summary>
    /// 酒店系统佣金类型
    /// </summary>
    public enum HBECommissionType
    {
        /// <summary>
        /// 无返佣
        /// </summary>
        NUL,
        /// <summary>
        /// 百分比
        /// </summary>
        PCT,
        /// <summary>
        /// 固定返佣
        /// </summary>
        FIX
    }
    #endregion

    #region 酒店系统旅客类型
    /// <summary>
    /// 酒店系统旅客类型
    /// </summary>
    public enum HBEGuestTypeIndicator
    {
        /// <summary>
        /// 内宾
        /// </summary>
        D,
        /// <summary>
        /// 外宾
        /// </summary>
        F
    }
    #endregion

    #region *
    /*#region 酒店系统订单异步通知订单状态
    /// <summary>
    /// 酒店系统订单异步通知订单状态
    /// </summary>
    public enum HBEOrderHintState
    {
        /// <summary>
        /// 确认
        /// </summary>
        SS=0,
        /// <summary>
        /// 确认
        /// </summary>
        ST,
        /// <summary>
        /// 取消
        /// </summary>
        XX,
        /// <summary>
        /// 取消
        /// </summary>
        XS,
        /// <summary>
        /// 取消
        /// </summary>
        ZF,
        /// <summary>
        /// 满房
        /// </summary>
        HX,
        /// <summary>
        /// 测试
        /// </summary>
        TS,
        /// <summary>
        /// 处理中
        /// </summary>
        NN,
        /// <summary>
        /// 处理中
        /// </summary>
        NS,
        /// <summary>
        /// 处理中
        /// </summary>
        SP,
        /// <summary>
        /// 处理中
        /// </summary>
        MF,
        /// <summary>
        /// 不在统计范围
        /// </summary>
        BA,
        /// <summary>
        /// 不在统计范围
        /// </summary>
        FX,
        /// <summary>
        /// 不在统计范围
        /// </summary>
        HT,
        /// <summary>
        /// 不在统计范围
        /// </summary>
        SN
    }
    #endregion*/
    #endregion

    #region 酒店系统订单信息业务实体
    /// <summary>
    /// 酒店系统订单信息业务实体
    /// </summary>
    public class HBEOrderInfo
    {
        /// <summary>
        /// 国家代码
        /// </summary>
        private string _CountryCode = "CN";

        #region 平台客服联系信息
        /// <summary>
        /// 订单联系人姓名(平台客服)
        /// </summary>
        private const string _SContacterFullname = "廖婷婷";
        /// <summary>
        /// 订单联系人手机(平台客服)
        /// </summary>
        private const string _SContacterMobile = "15988896919";
        /// <summary>
        /// 订单联系人电话(平台客服)
        /// </summary>
        private const string _SContacterTelephone = "0571-56893761";
        #endregion

        /// <summary>
        /// default constructor
        /// </summary>
        public HBEOrderInfo() { }
        /// <summary>
        /// 预定时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 订单状态(航旅通接口)
        /// </summary>
        public HBEResStatus ResStatus { get; set; }
        /// <summary>
        /// 订单号(航旅通接口)
        /// </summary>
        public string ResOrderId { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 房型代码
        /// </summary>
        public string RoomTypeCode { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string RoomTypeName { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime CheckInDate { get; set; }
        /// <summary>
        /// 离店时间
        /// </summary>
        public DateTime CheckOutDate { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// 价格计划代码
        /// </summary>
        public string RatePlanCode { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public HBEPaymentType PaymentType { get; set; }
        /// <summary>
        /// 间夜数
        /// </summary>
        public int RoomNight
        {
            get { return ((TimeSpan)(this.CheckOutDate - this.CheckInDate)).Days; }
        }
        /// <summary>
        /// 总房价
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 特殊要求
        /// </summary>
        public string SpecialRequest { get; set; }
        /// <summary>
        /// 返佣类型
        /// </summary>
        public HBECommissionType CommissionType { get; set; }
        /// <summary>
        /// 固定返佣
        /// </summary>
        public decimal CommissionFix { get; set; }
        /// <summary>
        /// 佣金比例
        /// </summary>
        public decimal CommissionPercent { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContacterFullname { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContacterMobile { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContacterTelephone { get; set; }
        /// <summary>
        /// 是否手机联系
        /// </summary>
        public bool IsMobileContact { get; set; }        
        /// <summary>
        /// 房间数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 到店最早时间 格式：HHMM(1800 表示为下午六点)
        /// </summary>
        public string ArriveEarlyTime { get; set; }
        /// <summary>
        /// 到店最晚时间 格式：HHMM(1800 表示为下午六点)
        /// </summary>
        public string ArriveLateTime { get; set; }
        /// <summary>
        /// 请求返回指令
        /// </summary>
        public string ResponseXML { get; set; }
        /// <summary>
        /// 旅客信息集合 索引0的联系人姓名联系人电话必须填写
        /// </summary>
        public IList<HBEResGuestInfo> ResGuests { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 总佣金
        /// </summary>
        public decimal TotalCommission { get; set; }

        /// <summary>
        /// 国家代码 默认值：CN
        /// </summary>
        public string CountryCode
        {
            get { return this._CountryCode; }
            set { this._CountryCode = value; }
        }
        /// <summary>
        /// 预订请求XML指令
        /// </summary>
        public string RequestXML
        {
            get
            {
                return Utils.CreateRequestXML(Utils.TH_HotelResRQ, this.CreateHotelResRQ());
            }
        }

        #region private members
        /// <summary>
        /// 获取旅客姓名 多个旅客各姓名ujf将用","隔开
        /// </summary>
        /// <returns></returns>
        private string GetResGuestsName()
        {
            StringBuilder s = new StringBuilder();

            s.Append(this.ResGuests[0].PersonName);

            for (int i = 1; i < this.ResGuests.Count; i++)
            {
                s.AppendFormat(",{0}", this.ResGuests[i].PersonName);
            }

            return s.ToString();
        }

        /// <summary>
        /// Create HotelAvailRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateHotelResRQ()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<HotelResRQ>");
            s.Append("<HotelReservations>");
            s.AppendFormat("<HotelReservation CreateDateTime=\"{0}\" CreateOrigResIndicator=\"\">", this.CreateDateTime);
            s.Append(this.CreateRoomStays());
            s.Append(this.CreateServices());
            s.Append(this.CreateResGuests());
            s.Append(this.CreateResGlobalInfo());
            s.Append(this.CreateContactorInfo());
            s.Append("</HotelReservation>");
            s.Append("</HotelReservations>");
            s.Append("</HotelResRQ>");
            return s.ToString();
        }

        #region CreateRoomStays
        /// <summary>
        /// Create RoomStays XML
        /// </summary>
        /// <returns></returns>
        private string CreateRoomStays()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<RoomStays>");
            s.Append("<RoomStay>");

            s.Append(this.CreateBasicProperty());
            s.Append(this.CreateRoomRates());
            s.AppendFormat("<TimeSpan StartDate=\"{0}\" EndDate=\"{1}\"/>", this.CheckInDate.ToString("yyyy-MM-dd"), this.CheckOutDate.ToString("yyyy-MM-dd"));
            s.AppendFormat("<SpecialRequest>{0}</SpecialRequest>", this.SpecialRequest);
            s.AppendFormat("<Comments>{0}</Comments>", this.Comments);
            s.AppendFormat("<ArriveEarlyTime>{0}</ArriveEarlyTime>", this.ArriveEarlyTime);
            s.AppendFormat("<ArriveLateTime>{0}</ArriveLateTime>", this.ArriveLateTime);
            s.AppendFormat("<AvailabilityStatus>{0}</AvailabilityStatus>", string.Empty);//可用状态
            s.AppendFormat("<VendorComments>{0}</VendorComments>", string.Empty);//供应商出里订单后返回的备注
            s.AppendFormat("<TotalCommission>{0}</TotalCommission>", this.TotalCommission);//总佣金，对于前台现付的订单，体现总佣金 --问题
            s.AppendFormat("<TotalFee></TotalFee>");//总附加费
            s.AppendFormat("<TotalAmount>{0}</TotalAmount>", this.TotalAmount);
            s.AppendFormat("<PaymentStatus>{0}</PaymentStatus>", string.Empty);//支付状态
            s.AppendFormat("<GuaranteeStatus>{0}</GuaranteeStatus>", string.Empty);//担保状态

            s.Append("</RoomStay>");
            s.Append("</RoomStays>");
            return s.ToString();
        }

        /// <summary>
        /// Create BasicProperty XML
        /// </summary>
        /// <returns></returns>
        private string CreateBasicProperty()
        {
            return string.Format(@"<BasicProperty HotelCode=""{0}"" ChainCode=""{1}"" HotelName=""{2}"" CityName=""{3}"" CityCode=""{4}"" ChainName=""{5}"" BrandCode=""{6}"" CountryCode=""{7}"" BrandName=""{8}"" ></BasicProperty>", this.HotelCode
                , string.Empty//连锁酒店代码
                , this.HotelName
                , this.CityName
                , this.CityCode
                , string.Empty//连锁酒店
                , string.Empty//品牌代码
                , this.CountryCode
                , string.Empty/*品牌名称*/);
        }

        /// <summary>
        /// Create RoomRates XML
        /// </summary>
        /// <returns></returns>
        private string CreateRoomRates()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<RoomRates>");
            s.Append(this.CreateRoomRate());
            s.Append("</RoomRates>");
            return s.ToString();
        }
        /// <summary>
        /// Create RoomRate XML
        /// </summary>
        /// <returns></returns>
        private string CreateRoomRate()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<RoomRate RoomTypeName=\"{0}\" RatePlanCode=\"{1}\" ConfirmRightNowIndicator=\"{2}\" RoomTypeCode=\"{3}\" Payment=\"{4}\" StartDate=\"{5}\" RoomTypeDesc=\"{6}\" HotelCode=\"{7}\" EndDate=\"{8}\" VendorCode=\"{9}\" BedType=\"{10}\" feeFix=\"{11}\" feePercent=\"{12}\" fee=\"{13}\" RatePlanName=\"{14}\" VendorName=\"{15}\">", this.RoomTypeName
                , this.RatePlanCode
                , string.Empty//能否立即确认的标识
                , this.RoomTypeCode
                , this.PaymentType.ToString()
                , this.CheckInDate.ToString("yyyy-MM-dd")
                , string.Empty//房型描述
                , this.HotelCode
                , this.CheckOutDate.ToString("yyyy-MM-dd")
                , this.VendorCode
                , string.Empty//床型
                , string.Empty//服务费固定值
                , string.Empty//服务费百分比 不返佣
                , string.Empty//服务费，不返佣，可由amountPrice和feePercent、feeFix计算出来
                , string.Empty//RatePlanName
                , this.VendorName);
            s.Append(this.CreateRates());
            s.AppendFormat("<Quantity>{0}</Quantity>", this.Quantity);
            s.AppendFormat("</RoomRate>");
            return s.ToString();
        }
        /// <summary>
        /// Create Rates XML
        /// </summary>
        /// <returns></returns>
        private string CreateRates()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<Rates>");
            s.Append("<Rate></Rate>");
            s.Append("</Rates>");
            return s.ToString();
        }
        #endregion

        /// <summary>
        /// Create Services XML
        /// </summary>
        /// <returns></returns>
        private string CreateServices()
        {
            StringBuilder s = new StringBuilder();

            return s.ToString();
        }
        /// <summary>
        /// Create ResGuests XML
        /// </summary>
        /// <returns></returns>
        private string CreateResGuests()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<ResGuests>");

            for (int i = 0; i < this.ResGuests.Count; i++)
            {
                s.AppendFormat(@"
<ResGuest>
	<GuestProfile>
		<PersonName>{0}</PersonName>
		<Mobile>{1}</Mobile>
		<Telephone>{2}</Telephone>
		<Fax></Fax>
		<Email></Email>
		<Gender></Gender>
		<Address></Address>
		<CountryCode>{3}</CountryCode>
		<NamePrefix></NamePrefix>
	</GuestProfile>
	<GuestIndex>{4}</GuestIndex>
	<Type>{5}</Type>
	<RoomNumber>{6}</RoomNumber>
	<IsMobileContact>{7}</IsMobileContact>
</ResGuest>", this.ResGuests[i].PersonName
            , this.ResGuests[i].Mobile
            , this.ResGuests[i].Telephone
            , this.ResGuests[i].GuestTypeIndicator == HBEGuestTypeIndicator.D ? "CN" : ""
            , (i + 1).ToString()//旅客编号
            , this.ResGuests[i].Type
            , (i + 1).ToString()//房号
            , Utils.BoolToYesOrNo(this.ResGuests[i].IsMobileContact));
            }
 
            s.Append("</ResGuests>");
            return s.ToString();
        }
        /// <summary>
        /// Create ResGlobalInfo XML
        /// </summary>
        /// <returns></returns>
        private string CreateResGlobalInfo()
        {
            StringBuilder s = new StringBuilder();

            return string.Format(@"
<ResGlobalInfo>
    <GuestCount>{0}</GuestCount>
    <TimeSpan StartDate=""{1}"" EndDate=""{2}""/>
    <ArriveEarlyTime>{3}</ArriveEarlyTime>
    <ArriveLateTime>{4}</ArriveLateTime>
    <Payment>{5}</Payment>
    <Guarantee></Guarantee>
</ResGlobalInfo>", this.Quantity
                 , this.CheckInDate.ToString("yyyy-MM-dd")
                 , this.CheckOutDate.ToString("yyyy-MM-dd")
                 , this.ArriveEarlyTime
                 , this.ArriveLateTime
                 , this.PaymentType.ToString());
        }
        /// <summary>
        /// Create ContactorInfo XML
        /// </summary>
        /// <returns></returns>
        private string CreateContactorInfo()
        {
            return string.Format(@"
<ContactorInfo>
    <Profile>
        <PersonName>{0}</PersonName>
        <Mobile>{1}</Mobile>
        <Telephone>{2}</Telephone>
        <Fax></Fax>
        <Email></Email>
        <Gender></Gender>
        <Address></Address>
        <CountryCode></CountryCode>
        <NamePrefix></NamePrefix>
    </Profile>
    <IsMobileContact>{3}</IsMobileContact>
    <IsEmailContact></IsEmailContact>
    <IsTelephoneContact></IsTelephoneContact>
    <IsFaxContact></IsFaxContact>
</ContactorInfo>", _SContacterFullname//this.ContacterFullname
                 , _SContacterMobile//this.ContacterMobile
                 , _SContacterTelephone//this.ContacterTelephone
                 , Utils.BoolToYesOrNo(this.IsMobileContact));
        }
        #endregion
    }
    #endregion

    #region 酒店系统订单旅客信息业务实体
    /// <summary>
    /// 酒店系统订单旅客信息业务实体
    /// </summary>
    public class HBEResGuestInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public HBEResGuestInfo() { }

        /// <summary>
        /// 旅客姓名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 旅客手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 旅客电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 是否手机联系
        /// </summary>
        public bool IsMobileContact { get; set; }
        /// <summary>
        /// 旅客类型
        /// </summary>
        public HBEGuestTypeIndicator GuestTypeIndicator { get; set; }
        /// <summary>
        /// 客人，值必须为"gue" 
        /// </summary>
        public string Type { get { return "gue"; } }
    }
    #endregion

    #region 酒店系统取消订单信息业务实体
    /// <summary>
    /// 酒店系统取消订单信息业务实体
    /// </summary>
    public class HBECancelOrderInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public HBECancelOrderInfo() { }

        /// <summary>
        /// 订单号(航旅通接口)
        /// </summary>
        public string ResOrderId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// 请求返回指令
        /// </summary>
        public string ResponseXML { get; set; }

        /// <summary>
        /// 取消请求XML指令
        /// </summary>
        public string RequestXML
        {
            get
            {
                StringBuilder s = new StringBuilder();                
                s.Append("<HotelCancelRQ>");
                s.AppendFormat("<CancelReason>{0}</CancelReason>", this.CancelReason);
                s.Append("<HotelReservations>");
                s.AppendFormat("<HotelReservation ResOrderID=\"{0}\"></HotelReservation>",this.ResOrderId);
                s.Append("</HotelReservations>");
                s.Append("</HotelCancelRQ>");

                return Utils.CreateRequestXML(Utils.TH_HotelCancelRQ, s.ToString());
            }
        }
    }
    #endregion

    #region 酒店系统单个订单详细信息查询信息业务实体
    /// <summary>
    /// 酒店系统单个订单详细信息查询信息业务实体
    /// </summary>
    public class HBESingleOrderSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public HBESingleOrderSearchInfo() { }

        /// <summary>
        /// 订单号(航旅通接口)
        /// </summary>
        public string ResOrderId { get; set; }
        /// <summary>
        /// 请求返回指令
        /// </summary>
        public string ResponseXML { get; set; }

        /// <summary>
        /// 取消请求XML指令
        /// </summary>
        public string RequestXML
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append("<HotelResDetailSearchRQ>");
                s.Append("<HotelResSearchCriteria>");
                s.AppendFormat("<ResID>{0}</ResID>", this.ResOrderId);
                s.Append("</HotelResSearchCriteria>");
                s.Append("</HotelResDetailSearchRQ>");

                return Utils.CreateRequestXML(Utils.TH_HotelResDetailSearchRQ, s.ToString());
            }
        }
    }
    #endregion
}
