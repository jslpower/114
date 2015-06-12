using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{

    #region 运价信息实体

    /// <summary>
    /// 运价信息
    /// </summary>
    /// 周文超 2010-10-21
    [Serializable]
    public class TicketFreightInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketFreightInfo() { }

        #region Model

        private string _id;
        private EyouSoft.Model.CompanyStructure.Company _company = new EyouSoft.Model.CompanyStructure.Company();
        private string _contactMQ;
        private RateType _ratetype;
        private FreightType _freighttype;
        private ProductType _producttype;
        private PackageTypes _buytype;
        private int _flightid;
        private string _flightname;
        private int _nogadhomecityid;
        private string _nogadhomecityidname;
        private int _nogaddestcityid;
        private string _nogaddestcityname;
        private int _gadhomecityid;
        private string _gadhomecityname;
        private int _gaddestcityid;
        private string _gaddestcityname;
        private bool _isenabled;
        private bool _ischeckpnr;
        private bool _isexpired;
        private DateTime? _freightstartdate;
        private DateTime? _freightenddate;
        private int _maxpcount;
        private string _workstarttime;
        private string _workendtime;
        private string _supplierremark;
        private DateTime? _lastupdatedate;
        private string _freightbuyid;
        private string _operatorid;
        private DateTime _issuetime;

        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 公司基本信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.Company Company
        {
            set { _company = value; }
            get { return _company; }
        }

        /// <summary>
        /// 公司联系MQ
        /// </summary>
        public string ContactMQ
        {
            set { _contactMQ = value; }
            get { return _contactMQ; }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public RateType RateType
        {
            set { _ratetype = value; }
            get { return _ratetype; }
        }

        /// <summary>
        /// 运价类型
        /// </summary>
        public FreightType FreightType
        {
            set { _freighttype = value; }
            get { return _freighttype; }
        }

        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductType ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }

        /// <summary>
        /// 购买类型
        /// </summary>
        public PackageTypes BuyType
        {
            set { _buytype = value; }
            get { return _buytype; }
        }
        /// <summary>
        /// 所属航空编号
        /// </summary>
        public int FlightId
        {
            set { _flightid = value; }
            get { return _flightid; }
        }
        /// <summary>
        /// 所属航空公司名称
        /// </summary>
        public string FlightName
        {
            set { _flightname = value; }
            get { return _flightname; }
        }
        /// <summary>
        /// 非缺口乘始发地编号
        /// </summary>
        public int NoGadHomeCityId
        {
            set { _nogadhomecityid = value; }
            get { return _nogadhomecityid; }
        }
        /// <summary>
        /// 非缺口乘始发地名称
        /// </summary>
        public string NoGadHomeCityIdName
        {
            set { _nogadhomecityidname = value; }
            get { return _nogadhomecityidname; }
        }
        /// <summary>
        /// 非缺口乘目的地编号
        /// </summary>
        public int NoGadDestCityId
        {
            set { _nogaddestcityid = value; }
            get { return _nogaddestcityid; }
        }
        /// <summary>
        /// 非缺口乘目的地名称
        /// </summary>
        public string NoGadDestCityName
        {
            set { _nogaddestcityname = value; }
            get { return _nogaddestcityname; }
        }
        /// <summary>
        /// 缺口乘始发地编号
        /// </summary>
        public int GadHomeCityId
        {
            set { _gadhomecityid = value; }
            get { return _gadhomecityid; }
        }
        /// <summary>
        /// 缺口乘始发地名称
        /// </summary>
        public string GadHomeCityName
        {
            set { _gadhomecityname = value; }
            get { return _gadhomecityname; }
        }
        /// <summary>
        /// 缺口乘目的地编号
        /// </summary>
        public int GadDestCityId
        {
            set { _gaddestcityid = value; }
            get { return _gaddestcityid; }
        }
        /// <summary>
        /// 缺口乘目的地名称
        /// </summary>
        public string GadDestCityName
        {
            set { _gaddestcityname = value; }
            get { return _gaddestcityname; }
        }
        /// <summary>
        /// 是否启用运价
        /// </summary>
        public bool IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// 是否更换PNR
        /// </summary>
        public bool IsCheckPNR
        {
            set { _ischeckpnr = value; }
            get { return _ischeckpnr; }
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired
        {
            set { _isexpired = value; }
            get { return _isexpired; }
        }
        /// <summary>
        /// 运价开始日期
        /// </summary>
        public DateTime? FreightStartDate
        {
            set { _freightstartdate = value; }
            get { return _freightstartdate; }
        }
        /// <summary>
        /// 运价结束时间
        /// </summary>
        public DateTime? FreightEndDate
        {
            set { _freightenddate = value; }
            get { return _freightenddate; }
        }
        /// <summary>
        /// 人数上限
        /// </summary>
        public int MaxPCount
        {
            set { _maxpcount = value; }
            get { return _maxpcount; }
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        public string WorkStartTime
        {
            set { _workstarttime = value; }
            get { return _workstarttime; }
        }
        /// <summary>
        /// 下班时间
        /// </summary>
        public string WorkEndTime
        {
            set { _workendtime = value; }
            get { return _workendtime; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string SupplierRemark
        {
            set { _supplierremark = value; }
            get { return _supplierremark; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdateDate
        {
            set { _lastupdatedate = value; }
            get { return _lastupdatedate; }
        }
        /// <summary>
        /// 套餐购买编号
        /// </summary>
        public string FreightBuyId
        {
            set { _freightbuyid = value; }
            get { return _freightbuyid; }
        }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }

        #endregion Model

        #region 扩展属性

        /// <summary>
        /// 去程独立日期
        /// </summary>
        public DateTime? FromSelfDate { get; set; }

        /// <summary>
        /// 回程独立日期
        /// </summary>
        public DateTime? ToSelfDate { get; set; }

        /// <summary>
        /// 去程适用日期
        /// </summary>
        public string FromForDay { get; set; }

        /// <summary>
        /// 回程适用日期
        /// </summary>
        public string ToForDay { get; set; }

        /// <summary>
        /// 去程参考面价
        /// </summary>
        public decimal FromReferPrice { get; set; }

        /// <summary>
        /// 回程参考面价
        /// </summary>
        public decimal ToReferPrice { get; set; }

        /// <summary>
        /// 去程参考扣率
        /// </summary>
        public decimal FromReferRate { get; set; }

        /// <summary>
        /// 回程参考扣率
        /// </summary>
        public decimal ToReferRate { get; set; }

        /// <summary>
        /// 去程结算价格
        /// </summary>
        public decimal FromSetPrice { get; set; }

        /// <summary>
        /// 回程结算价格
        /// </summary>
        public decimal ToSetPrice { get; set; }

        /// <summary>
        /// 去程燃油费
        /// </summary>
        public decimal FromFuelPrice { get; set; }

        /// <summary>
        /// 回程燃油费
        /// </summary>
        public decimal ToFuelPrice { get; set; }

        /// <summary>
        /// 去程机建费
        /// </summary>
        public decimal FromBuildPrice { get; set; }

        /// <summary>
        /// 回程机建费
        /// </summary>
        public decimal ToBuildPrice { get; set; }

        #endregion
    }

    #endregion

    #region 运价查询实体

    /// <summary>
    /// 运价查询实体
    /// </summary>
    [Serializable]
    public class QueryTicketFreightInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryTicketFreightInfo(string SellCompanyId, FreightType? FreightType, int HomeCity, int DestCity, int PeopleNum, DateTime? FromTime, DateTime? ToTime, int AirportId, TravellerType? TravellerType)
        {
            this.SellCompanyId = SellCompanyId;
            this.FreightType = FreightType;
            this.HomeCity = HomeCity;
            this.DestCity = DestCity;
            this.PeopleNum = PeopleNum;
            this.FromTime = FromTime;
            this.ToTime = ToTime;
            this.AirportId = AirportId;
            this.TravellerType = TravellerType;
        }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SellCompanyId { get; set; }

        /// <summary>
        /// 航班类型
        /// </summary>
        public FreightType? FreightType { get; set; }

        /// <summary>
        /// 出发地
        /// </summary>
        public int HomeCity { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public int DestCity { get; set; }

        /// <summary>
        ///  乘机人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// 航空公司ID
        /// </summary>
        public int AirportId { get; set; }

        /// <summary>
        /// 旅客类型
        /// </summary>
        public TravellerType? TravellerType { get; set; }
    }

    #endregion

    #region 运价列表实体

    /// <summary>
    /// 运价列表实体(采购商端查询后显示)
    /// </summary>
    [Serializable]
    public class TicketFreightInfoList : TicketFlightBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TicketFreightInfoList() { }

        /// <summary>
        /// 供应商在平台购买套餐的总金额（某一航空公司）
        /// </summary>
        public decimal AirportPayPrice { get; set; }

        /// <summary>
        /// 运价信息实体
        /// </summary>
        public IList<TicketFreightInfo> FreightInfoList { get; set; }
    }

    #endregion

    #region 业务类型枚举

    /// <summary>
    /// 业务类型
    /// </summary>
    public enum RateType
    {
        /// <summary>
        /// 散客政策
        /// </summary>
        散客政策 = 1,
        /// <summary>
        /// 特殊代理费
        /// </summary>
        特殊代理费 = 2,
        /// <summary>
        /// 团队散拼
        /// </summary>
        团队散拼 = 3,
        /// <summary>
        /// 特价政策
        /// </summary>
        特价政策 = 4,
        /// <summary>
        /// 国际政策
        /// </summary>
        国际政策 = 5,
        /// <summary>
        /// 航线政策
        /// </summary>
        航线政策 = 6
    }

    #endregion

    #region 运价类型枚举

    /// <summary>
    /// 运价类型
    /// </summary>
    public enum FreightType
    {
        /// <summary>
        /// 单程
        /// </summary>
        单程 = 1,
        /// <summary>
        /// 来回程
        /// </summary>
        来回程 = 2,
        /// <summary>
        /// 缺口程
        /// </summary>
        缺口程 = 3
    }

    #endregion

    #region 产品类型枚举

    /// <summary>
    /// 产品类型
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// 整团
        /// </summary>
        整团 = 1,
        /// <summary>
        /// 散拼
        /// </summary>
        散拼 = 2
    }

    #endregion

}
