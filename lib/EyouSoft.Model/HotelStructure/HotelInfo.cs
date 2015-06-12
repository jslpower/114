using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    //Author:周文超 2010-12-02

    #region 酒店基类

    /// <summary>
    /// 酒店基类
    /// </summary>
    /// 周文超 2010-12-01
    [Serializable]
    public class HotelBase
    {
        private AppCode _appCode = AppCode.SOHOTO;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelBase() { }

        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 酒店系统接口类型
        /// </summary>
        public AppCode AppCode
        {
            get { return _appCode; }
            set { _appCode = value; }
        }
        /// <summary>
        /// 酒店编号
        /// </summary>
        public string HotelId { get; set; }
    }

    #endregion

    #region 酒店信息实体

    /// <summary>
    /// 酒店实体
    /// </summary>
    [Serializable]
    public class HotelInfo : HotelBase
    {
        /// <summary>
        /// 开业时间 
        /// </summary>
        public string Opendate { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// 装修时间
        /// </summary>
        public string Fitment { get; set; }

        /// <summary>
        /// 房间数量
        /// </summary>
        public int RoomQuantity { get; set; }

        /// <summary>
        /// 酒店电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 酒店简要描述
        /// </summary>
        public string ShortDesc { get; set; }

        /// <summary>
        /// 酒店详细描述
        /// </summary>
        public string LongDesc { get; set; }

        /// <summary>
        /// 行政区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 酒店地理位置
        /// </summary>
        public HotelPositionInfo HotelPosition { get; set; }

        /// <summary>
        /// 酒店星级
        /// </summary>
        public EyouSoft.HotelBI.HotelRankEnum Rank { get; set; }

        /// <summary>
        /// 酒店图片信息集合
        /// </summary>
        public IList<HotelImagesInfo> HotelImages { get; set; }

        /// <summary>
        /// 房型信息
        /// </summary>
        public IList<RoomTypeInfo> RoomTypeList { get; set; }

        /// <summary>
        /// 查询价格下限
        /// </summary>
        public decimal MinRate { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 接口返回的XML（测试用，测试结束后要删除此属性）
        /// </summary>
        public string InterfaceResponseXML { get; set; }
    }

    #endregion

    #region 酒店地理位置

    /// <summary>
    /// 酒店地理位置
    /// </summary>
    [Serializable]
    public class HotelPositionInfo
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 酒店地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 地标位置
        /// </summary>
        public string POR { get; set; }
    }

    #endregion

    #region 酒店图片信息实体

    /// <summary>
    /// 酒店图片信息实体
    /// </summary>
    [Serializable]
    public class HotelImagesInfo
    {
        /// <summary>
        /// 图片类型
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 图片地址（无域名）
        /// </summary>
        public string ImageURL { get; set; }
    }

    #endregion

    #region 酒店系统房型信息业务实体

    /// <summary>
    /// 酒店系统房型信息业务实体
    /// </summary>
    [Serializable]
    public class RoomTypeInfo
    {
        /// <summary>
        /// 价格计划代码
        /// </summary>
        public string RatePlanCode { get; set; }
        /// <summary>
        /// 房型代码
        /// </summary>
        public string RoomTypeCode { get; set; }
        /// <summary>
        /// 房型名称
        /// </summary>
        public string RoomTypeName { get; set; }
        /// <summary>
        /// 房型种类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 床型
        /// </summary>
        public string BedType { get; set; }
        /// <summary>
        /// 吸烟
        /// </summary>
        public bool NoSmoking { get; set; }
        /// <summary>
        /// 房型描述
        /// </summary>
        public string RoomDescription { get; set; }
        /// <summary>
        /// 房型价格信息
        /// </summary>
        public RoomRateInfo RoomRate { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 房型编号
        /// </summary>
        public string RoomTypeId { get; set; }
        /// <summary>
        /// 能否上网
        /// </summary>
        public bool IsInternet { get; set; }
    }

    #endregion

    #region 房型价格信息

    /// <summary>
    /// 房型价格信息
    /// </summary>
    [Serializable]
    public class RoomRateInfo
    {
        /// <summary>
        /// 入住起始时间 yy-mm-dd
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 入住截止时间 yy-mm-dd
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public EyouSoft.HotelBI.HBEPaymentType Payment { get; set; }
        
        /// <summary>
        /// 能否上网
        /// </summary>
        public bool Internet { get; set; }

        /// <summary>
        /// 房型数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 房型最大入住人数
        /// </summary>
        public int MaxGuestNum { get; set; }

        /// <summary>
        /// 总销售价
        /// </summary>
        public decimal AmountPrice { get; set; }

        /// <summary>
        /// 酒店是否含available的房型
        /// </summary>
        public RoomState AvailabilityStatus { get; set; }

        /// <summary>
        /// 房型价格明细信息
        /// </summary>
        public IList<RateInfo> RateInfos { get; set; }

        /// <summary>
        /// 预定规则和要求信息(暂时放在酒店下面，等知道酒店接口的数据结构后在做调整)
        /// </summary>
        public BookPolicy BookPolicy { get; set; }
    }

    #endregion

    #region 房型价格明细信息

    /// <summary>
    /// 房型价格明细信息
    /// </summary>
    [Serializable]
    public class RateInfo
    {
        /// <summary>
        /// 当天日期
        /// </summary>
        public DateTime CurrData { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal AmountPrice { get; set; }

        /// <summary>
        /// 门市价
        /// </summary>
        public decimal DisplayPrice { get; set; }

        /// <summary>
        /// 服务费固定值
        /// </summary>
        public decimal FeeFix { get; set; }

        /// <summary>
        /// 服务费百分比 不返佣
        /// </summary>
        public decimal FeePercent { get; set; }

        /// <summary>
        /// 服务费，不返佣，可由amountPrice和feePercent、feeFix计算出来
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// 返佣类型
        /// </summary>
        public EyouSoft.HotelBI.HBECommissionType CommissionType { get; set; }

        /// <summary>
        /// 返佣固定金额
        /// </summary>
        public decimal Fix { get; set; }

        /// <summary>
        /// 返佣百分比
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// 返佣佣金税率
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// 返佣价
        /// </summary>
        public decimal CommissionAmount 
        { 
            get
            {
                return AmountPrice - Fee;
            } 
        }

        /// <summary>
        /// 税前价格(返佣价)，此属性为缓存数据后增加
        /// </summary>
        public decimal AmountBeforeTax { get; set; }

        /// <summary>
        /// 返佣金额
        /// </summary>
        public decimal CommissionPrice
        {
            get
            {
                //返佣价 = 销售价 - 服务费
                //返佣金额 = （销售价 - 服务费） * 返佣比例
                decimal tmp = 0;
                switch (CommissionType)
                {
                    case EyouSoft.HotelBI.HBECommissionType.NUL: //无返佣；返佣金额为0
                        tmp = 0;
                        break;
                    case EyouSoft.HotelBI.HBECommissionType.PCT://返佣金额 = 返佣价 * 返佣比例
                        tmp = CommissionAmount * Percent;
                        break;
                    case EyouSoft.HotelBI.HBECommissionType.FIX://返佣金额 = 返佣固定金额
                        tmp = Fix;
                        break;
                }
                return tmp;
            }
        }

        /// <summary>
        /// 结算价
        /// </summary>
        public decimal BalancePrice
        {
            get 
            {
                //返佣价 = 销售价 - 服务费
                //返佣金额 = （销售价 - 服务费） * 返佣比例
                //结算价＝返佣价--返佣价×返佣比例
                return CommissionAmount - CommissionPrice;
            }
        }

        /// <summary>
        /// 含早餐数
        /// </summary>
        public int FreeMeal { get; set; }

        /// <summary>
        /// 额外收费项目集合
        /// </summary>
        public IList<ExcessiveFee> ExcessiveFees { get; set; }
    }

    #endregion

    #region 额外收费项目实体

    /// <summary>
    /// 额外收费项目实体
    /// </summary>
    [Serializable]
    public class ExcessiveFee
    {
        /// <summary>
        /// 附加费代码
        /// </summary>
        public string FeeCode { get; set; }

        /// <summary>
        /// 附加费名称
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// 收费频率
        /// </summary>
        public string ChargeFrequence { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 收费单元
        /// </summary>
        public string ChargeUnit { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }

    #endregion

    #region 预定规则和要求信息实体

    /// <summary>
    /// 预定规则和要求信息实体
    /// </summary>
    [Serializable]
    public class BookPolicy
    {
        /// <summary>
        /// 政策代码
        /// </summary>
        public string PolicyCode { get; set; }

        /// <summary>
        /// 政策类型
        /// </summary>
        public string PolicyType { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 简要描述
        /// </summary>
        public string ShortDesc { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string LongDesc { get; set; }
    }

    #endregion

    #region  查询酒店返回的分页信息实体

    /// <summary>
    /// 酒店查询返回分页信息实体
    /// </summary>
    [Serializable]
    public class RespPageInfo
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageNum { get; set; }
        /// <summary>
        /// 总酒店数
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
    }

    #endregion

    #region 平台前台酒店查询实体

    ///// <summary>
    ///// 平台前台酒店查询实体
    ///// </summary>
    //[Serializable]
    //public class SystemSearchHotelInfo
    //{
    //    /// <summary>
    //    /// 城市三字码
    //    /// </summary>
    //    public string CityCode { get; set; }

    //    /// <summary>
    //    /// 行政区
    //    /// </summary>
    //    public string District { get; set; }

    //    /// <summary>
    //    /// 酒店地理位置
    //    /// </summary>
    //    public HotelPositionInfo HotelPositionInfo { get; set; }

    //    /// <summary>
    //    /// 入住日期
    //    /// </summary>
    //    public DateTime? CheckInDate { get; set; }

    //    /// <summary>
    //    /// 离店日期
    //    /// </summary>
    //    public DateTime? CheckOutDate { get; set; }

    //    /// <summary>
    //    /// 酒店中文名
    //    /// </summary>
    //    public string HotelCNName { get; set; }

    //    /// <summary>
    //    /// 酒店英文名
    //    /// </summary>
    //    public string HotelENName { get; set; }

    //    /// <summary>
    //    /// 价格范围上限
    //    /// </summary>
    //    public decimal PriceUpper { get; set; }

    //    /// <summary>
    //    /// 价格范围下限
    //    /// </summary>
    //    public decimal PriceLower { get; set; }

    //    /// <summary>
    //    /// 酒店星级
    //    /// </summary>
    //    public EyouSoft.HotelBI.HotelRankEnum? Rank { get; set; }

    //    /// <summary>
    //    /// 支付方式
    //    /// </summary>
    //    public EyouSoft.HotelBI.HBEPaymentType? Payment { get; set; }

    //    /// <summary>
    //    /// 是否即时确认
    //    /// </summary>
    //    public bool IsAvail { get; set; }

    //    /// <summary>
    //    /// 是否接机
    //    /// </summary>
    //    public bool IsAirReception { get; set; }

    //    /// <summary>
    //    /// 优惠服务
    //    /// </summary>
    //    public bool IsPreference { get; set; }

    //    /// <summary>
    //    /// 是否上网
    //    /// </summary>
    //    public bool IsInternet { get; set; }

    //    /// <summary>
    //    /// 是否团队
    //    /// </summary>
    //    public bool IsTour { get; set; }

    //    /// <summary>
    //    /// 装修时间
    //    /// </summary>
    //    public string Fitment { get; set; }

    //    /// <summary>
    //    /// 特殊房型
    //    /// </summary>
    //    public string Category { get; set; }

    //    /// <summary>
    //    /// 床型
    //    /// </summary>
    //    public string BedType { get; set; }
    //}

    #endregion

    #region 酒店房态枚举

    /// <summary>
    /// 酒店是否含available的房型
    /// </summary>
    public enum RoomState
    {
        /// <summary>
        /// 可申请
        /// </summary>
        onRequest = 0,
        /// <summary>
        /// 即时确认
        /// </summary>
        avail = 1,
        /// <summary>
        /// 不可用
        /// </summary>
        noavail = 2
    }

    #endregion

    #region 酒店系统接口类型
    /// <summary>
    /// 酒店系统接口类型
    /// </summary>
    public enum AppCode
    {
        /// <summary>
        /// 中国航信HotelBE数据接口
        /// </summary>
        SOHOTO=0
    }
    #endregion

    #region 本地缓存酒店查询信息业务实体
    /// <summary>
    /// 本地缓存酒店查询信息业务实体
    /// </summary>
    /// Author:汪奇志 2011-05-13
    public class MLocalHotelSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLocalHotelSearchInfo() { }

        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 入住日期
        /// </summary>
        public DateTime CheckInDate { get; set; }
        /// <summary>
        /// 离店日期
        /// </summary>
        public DateTime CheckOutDate { get; set; }
        /// <summary>
        /// 酒店星级
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 酒店名称(中)
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 酒店名称(英)
        /// </summary>
        public string HotelNameEn { get; set; }
        /// <summary>
        /// 床型
        /// </summary>
        public string BedType { get; set; }
        /// <summary>
        /// 装修时间
        /// </summary>
        public string Fitment { get; set; }
        /// <summary>
        /// 特殊房型
        /// </summary>
        public string SpecialRoomName { get; set; }
        /// <summary>
        /// 上网服务
        /// </summary>
        public bool? IsInternet { get; set; }
        /// <summary>
        /// 行政区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 地标名称
        /// </summary>
        public string LandMarkName { get; set; }

        /// <summary>
        /// 价格计划查询开始时间
        /// </summary>
        public DateTime RatePlanStartDate
        {
            get
            {
                DateTime today = DateTime.Today;

                //2011-09-22 还原注释，根据入住时间取价格
                if (this.CheckInDate != DateTime.MinValue && this.CheckInDate < today.AddMonths(2))
                {
                    return this.CheckInDate;
                }

                return today;
            }
        }

        private EyouSoft.HotelBI.HotelOrderBy _OrderBy = EyouSoft.HotelBI.HotelOrderBy.Default;
        /// <summary>
        /// 排序方式
        /// </summary>
        public EyouSoft.HotelBI.HotelOrderBy OrderBy
        {
            get { return _OrderBy; }
            set { _OrderBy = value; }
        }
        /// <summary>
        /// 价格查询上限
        /// </summary>
        public decimal? PriceMaxRate { get; set; }
        /// <summary>
        /// 价格查询下限
        /// </summary>
        public decimal? PriceMinRate { get; set; }
    }
    #endregion
}
