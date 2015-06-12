using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{

    #region 订单实体

    /// <summary>
    /// 团队订单实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class TourOrder : TourObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrder()
		{}

		#region Model

		private string _id;
		private string _tourid;
		private int _ordertype;
        private OrderState _orderstate;
		private string _buycompanyid;
		private string _buycompanyname;
		private string _contactname;
		private string _contacttel;
		private string _contactfax;
		private string _contactmq;
		private string _contactqq;
		private string _operatorid;
		private string _companyid;
		private string _pricestandid;
		private decimal _personalprice;
		private decimal _childprice;
		private decimal _marketprice;
		private int _adultnumber;
		private int _childnumber;
		private int _marketnumber;
		private int _peoplenumber;
		private decimal _otherprice;
		private DateTime _saveseatdate;
		private string _operatorcontent;
		private string _specialcontent;
		private decimal _sumprice;
		private string _seatlist;
		private DateTime _lastdate;
		private string _lastoperatorid;
		private bool _isdelete;
		private DateTime _issuetime;
		private string _tourcompanyname;
		private string _tourcompanyid;
        private Model.TourStructure.TourType _tourtype;
		private DateTime _successtime;
		private DateTime _expiretime;
        private RateType _ratetype;
		private int _orderscore;
        private TourOrderOperateType _ordersource;

		/// <summary>
		/// 主键ID
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

		/// <summary>
		/// 团队编号
		/// </summary>
		public string TourId
		{
			set{ _tourid=value;}
			get{return _tourid;}
		}

        ///// <summary>
        ///// 出港城市ID
        ///// </summary>
        //public int SiteId { get; set; }

		/// <summary>
		/// 订单类型(1位数) 0:预定 1:购买
		/// </summary>
		public int OrderType
		{
			set{ _ordertype=value;}
			get{return _ordertype;}
		}
		/// <summary>
		/// 订单状态
		/// </summary>
        public OrderState OrderState
		{
			set{ _orderstate=value;}
			get{return _orderstate;}
		}
		/// <summary>
		/// 预订单位ID
		/// </summary>
		public string BuyCompanyID
		{
			set{ _buycompanyid=value;}
			get{return _buycompanyid;}
		}
		/// <summary>
		/// 预订单位名称
		/// </summary>
		public string BuyCompanyName
		{
			set{ _buycompanyname=value;}
			get{return _buycompanyname;}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string ContactName
		{
			set{ _contactname=value;}
			get{return _contactname;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string ContactTel
		{
			set{ _contacttel=value;}
			get{return _contacttel;}
		}
		/// <summary>
		/// 传真
		/// </summary>
		public string ContactFax
		{
			set{ _contactfax=value;}
			get{return _contactfax;}
		}
		/// <summary>
		/// MQ
		/// </summary>
		public string ContactMQ
		{
			set{ _contactmq=value;}
			get{return _contactmq;}
		}
		/// <summary>
		/// QQ
		/// </summary>
		public string ContactQQ
		{
			set{ _contactqq=value;}
			get{return _contactqq;}
		}
		/// <summary>
		/// 操作员ID
		/// </summary>
		public string OperatorID
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName { get; set; }

		/// <summary>
		/// 所属公司ID
		/// </summary>
		public string CompanyID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 价格等级编号
		/// </summary>
		public string PriceStandId
		{
			set{ _pricestandid=value;}
			get{return _pricestandid;}
		}
		/// <summary>
		/// 成人价
		/// </summary>
		public decimal PersonalPrice
		{
			set{ _personalprice=value;}
			get{return _personalprice;}
		}
		/// <summary>
		/// 儿童价
		/// </summary>
		public decimal ChildPrice
		{
			set{ _childprice=value;}
			get{return _childprice;}
		}
		/// <summary>
		/// 单房差
		/// </summary>
		public decimal MarketPrice
		{
			set{ _marketprice=value;}
			get{return _marketprice;}
		}
		/// <summary>
		/// 成人数
		/// </summary>
		public int AdultNumber
		{
			set{ _adultnumber=value;}
			get{return _adultnumber;}
		}
		/// <summary>
		/// 儿童数
		/// </summary>
		public int ChildNumber
		{
			set{ _childnumber=value;}
			get{return _childnumber;}
		}
		/// <summary>
		/// 单房差数
		/// </summary>
		public int MarketNumber
		{
			set{ _marketnumber=value;}
			get{return _marketnumber;}
		}
		/// <summary>
		/// 总人数
		/// </summary>
		public int PeopleNumber
		{
			set{ _peoplenumber=value;}
			get{return _peoplenumber;}
		}
		/// <summary>
		/// 其它费用
		/// </summary>
		public decimal OtherPrice
		{
			set{ _otherprice=value;}
			get{return _otherprice;}
		}
		/// <summary>
		/// 留位时间
		/// </summary>
		public DateTime SaveSeatDate
		{
			set{ _saveseatdate=value;}
			get{return _saveseatdate;}
		}
		/// <summary>
		/// 操作留言
		/// </summary>
		public string OperatorContent
		{
			set{ _operatorcontent=value;}
			get{return _operatorcontent;}
		}

        /// <summary>
        /// 出发交通（交通安排）
        /// </summary>
        public string LeaveTraffic { get; set; }

		/// <summary>
		/// 特别要求
		/// </summary>
		public string SpecialContent
		{
			set{ _specialcontent=value;}
			get{return _specialcontent;}
		}
		/// <summary>
		/// 总金额
		/// </summary>
		public decimal SumPrice
		{
			set{ _sumprice=value;}
			get{return _sumprice;}
		}
		/// <summary>
		/// 座位号多个座位，号分隔
		/// </summary>
		public string SeatList
		{
			set{ _seatlist=value;}
			get{return _seatlist;}
		}
		/// <summary>
		/// 最后修改时间 默认getdate()
		/// </summary>
		public DateTime LastDate
		{
			set{ _lastdate=value;}
			get{return _lastdate;}
		}
		/// <summary>
		/// 最后操作人ID
		/// </summary>
		public string LastOperatorID
		{
			set{ _lastoperatorid=value;}
			get{return _lastoperatorid;}
		}
		/// <summary>
		/// 删除状态 0:未删除 1:已删除 默认0
		/// </summary>
		public bool IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}
		/// <summary>
		/// 专线公司名称
		/// </summary>
		public string TourCompanyName
		{
			set{ _tourcompanyname=value;}
			get{return _tourcompanyname;}
		}
		/// <summary>
		/// 专线编号
		/// </summary>
		public string TourCompanyId
		{
			set{ _tourcompanyid=value;}
			get{return _tourcompanyid;}
		}
		/// <summary>
		/// 团队类型
		/// </summary>
        public Model.TourStructure.TourType TourType
		{
            set { _tourtype = value; }
            get { return _tourtype; }
		}
		/// <summary>
		/// 交易成功时间
		/// </summary>
		public DateTime SuccessTime
		{
			set{ _successtime=value;}
			get{return _successtime;}
		}
		/// <summary>
		/// 评价到期日期
		/// </summary>
		public DateTime ExpireTime
		{
			set{ _expiretime=value;}
			get{return _expiretime;}
		}
		/// <summary>
		/// 评价类型
		/// </summary>
        public RateType RateType
		{
			set{ _ratetype=value;}
			get{return _ratetype;}
		}
		/// <summary>
		/// 交易分值
		/// </summary>
		public int OrderScore
		{
			set{ _orderscore=value;}
			get{return _orderscore;}
		}
		/// <summary>
		/// 订单来源 默认：0 代客预定：1
		/// </summary>
        public TourOrderOperateType OrderSource
		{
			set{ _ordersource=value;}
			get{return _ordersource;}
        }

        #region 扩展属性

        private IList<TourOrderCustomer> _tourordercustomer;

        /// <summary>
        /// 订单游客信息
        /// </summary>
        public IList<TourOrderCustomer> TourOrderCustomer
        {
            set { _tourordercustomer = value; }
            get { return _tourordercustomer; }
        }

        #endregion

        #endregion Model
    }

    #endregion

    #region 订单统计信息实体

    /// <summary>
    /// 订单统计信息实体(已成交客户)
    /// </summary>
    /// 周文超 2010-06-02
    [Serializable]
    public class OrderStatistics
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderStatistics() { }

        private string _companyid;
        private string _companyname;
        private string _contactname;
        private string _contacttel;
        private OrderState? _orderstate;
        private int _ordainnum;
        private int _ordainpeoplenum;
        private decimal _totalmoney;
        private int _saveseatexpirednum;
        private int _notacceptednum;

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName
        {
            set { _contactname = value; }
            get { return _contactname; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel
        {
            set { _contacttel = value; }
            get { return _contacttel; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderState? OrderState
        {
            set { _orderstate = value; }
            get { return _orderstate; }
        }
        /// <summary>
        /// 预定成功次数
        /// </summary>
        public int OrdainNum
        {
            set { _ordainnum = value; }
            get { return _ordainnum; }
        }
        /// <summary>
        /// 预定人数
        /// </summary>
        public int OrdainPeopleNum
        {
            set { _ordainpeoplenum = value; }
            get { return _ordainpeoplenum; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalMoney
        {
            set { _totalmoney = value; }
            get { return _totalmoney; }
        }
        /// <summary>
        /// 留位过期次数
        /// </summary>
        public int SaveSeatExpiredNum
        {
            set { _saveseatexpirednum = value; }
            get { return _saveseatexpirednum; }
        }
        /// <summary>
        /// 不受理次数
        /// </summary>
        public int NotAcceptedNum
        {
            set { _notacceptednum = value; }
            get { return _notacceptednum; }
        }
    }

    #endregion

    #region 公司登录统计实体

    /// <summary>
    /// 公司登录统计实体
    /// </summary>
    [Serializable]
    public class RetailLoginStatistics
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RetailLoginStatistics() { }

        private string _companyid;
        private string _companyname;
        private int _visitnum;
        private int _loginnum;

        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 访问数量
        /// </summary>
        public int VisitNum
        {
            set { _visitnum = value; }
            get { return _visitnum; }
        }
        /// <summary>
        ///  登录次数
        /// </summary>
        public int LoginNum
        {
            set { _loginnum = value; }
            get { return _loginnum; }
        }
    }

    #endregion

    #region 批发商行为分析实体

    /// <summary>
    /// 批发商行为分析实体
    /// </summary>
    [Serializable]
    public class WholesalersStatistics
    {
        private string _companyid;
        private string _companyname;
        private IList<Model.TourStructure.AreaStatInfo> _areastatinfo;
        private int _ordainnum;
        private int _saveseatnum;
        private int _saveseatexpirednum;
        private int _notacceptednum;
        private int _loginnum;
        private int _visitednum;

        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 线路区域及有效产品数
        /// </summary>
        public IList<Model.TourStructure.AreaStatInfo> AreaStatinfo
        {
            set { _areastatinfo = value; }
            get { return _areastatinfo; }
        }
        /// <summary>
        /// 总有效产品数
        /// </summary>
        public int SumTourNum
        {
            get
            {
                int num = 0;
                if (AreaStatinfo == null || AreaStatinfo.Count <= 0)
                    return 0;
                foreach (Model.TourStructure.AreaStatInfo model in this.AreaStatinfo)
                {
                    if(model != null)
                        num += model.Number;
                }
                return num;
            }
        }
        /// <summary>
        /// 成交订单数
        /// </summary>
        public int OrdainNum
        {
            set { _ordainnum = value; }
            get { return _ordainnum; }
        }
        /// <summary>
        /// 留位订单数
        /// </summary>
        public int SaveSeatNum
        {
            set { _saveseatnum = value; }
            get { return _saveseatnum; }
        }
        /// <summary>
        /// 留位过期订单数
        /// </summary>
        public int SaveSeatExpiredNum
        {
            set { _saveseatexpirednum = value; }
            get { return _saveseatexpirednum; }
        }
        /// <summary>
        /// 不受理订单数
        /// </summary>
        public int NotAcceptedNum
        {
            set { _notacceptednum = value; }
            get { return _notacceptednum; }
        }
        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginNum
        {
            set { _loginnum = value; }
            get { return _loginnum; }
        }
        /// <summary>
        /// 被查看记录数
        /// </summary>
        public int VisitedNum
        {
            set { _visitednum = value; }
            get { return _visitednum; }
        }
    }

    #endregion

    #region 组团订单统计实体

    /// <summary>
    /// 组团订单统计实体
    /// </summary>
    [Serializable]
    public class AreaAndOrderNum
    {
        /// <summary>
        /// 线路区域ID
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 处理中订单数
        /// </summary>
        public int TreatNum { get; set; }
        /// <summary>
        /// 未处理订单数
        /// </summary>
        public int UntreatedNum { get; set; }
        /// <summary>
        /// 已成交订单数
        /// </summary>
        public int OrdainNum { get; set; }
        /// <summary>
        /// 已留位订单数
        /// </summary>
        public int SaveSeatNum { get; set; }
        /// <summary>
        /// 留位已到期订单数
        /// </summary>
        public int SaveSeatExpiredNum { get; set; }
        /// <summary>
        /// 未受理订单数
        /// </summary>
        public int NotAcceptedNum { get; set; }
        /// <summary>
        /// 订单总数
        /// </summary>
        public int AllOrderNum
        {
            get { return this.TreatNum + this.UntreatedNum + this.OrdainNum + this.SaveSeatNum + this.SaveSeatExpiredNum + this.NotAcceptedNum; }
        }
    }

    #endregion

    #region 统计每月每个订单状态的订单数量的实体

    /// <summary>
    /// 统计每月每个订单状态的订单数量的实体
    /// </summary>
    [Serializable]
    public class TourOrderStatisticsByMonth
    {
        /// <summary>
        /// 当前年月
        /// </summary>
        public string CurrYearAndMonth { get; set; }
        /// <summary>
        /// 未处理订单数
        /// </summary>
        public int UntreatedNum { get; set; }
        /// <summary>
        /// 处理中订单数
        /// </summary>
        public int TreatNum { get; set; }
        /// <summary>
        /// 已留位订单数
        /// </summary>
        public int SaveSeatNum { get; set; }
        /// <summary>
        /// 留位过期订单数
        /// </summary>
        public int SaveSeatExpiredNum { get; set; }
        /// <summary>
        /// 不受理订单数
        /// </summary>
        public int NotAcceptedNum { get; set; }
        /// <summary>
        /// 已成交订单数
        /// </summary>
        public int OrdainNum { get; set; }

    }

    #endregion

    #region 订单状态枚举

    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderState
    { 
        /// <summary>
        /// 未处理
        /// </summary>
        未处理 = 0,
        /// <summary>
        /// 处理中
        /// </summary>
        处理中=1,
        /// <summary>
        /// 已留位
        /// </summary>
        已留位=2,
        /// <summary>
        /// 留位过期
        /// </summary>
        留位过期=3,
        /// <summary>
        /// 不受理
        /// </summary>
        不受理=4,
        /// <summary>
        /// 已成交
        /// </summary>
        已成交=5
    }

    #endregion

    #region 订单交易类型枚举

    /// <summary>
    /// 订单交易类型
    /// </summary>
    public enum TourOrderOperateType
    {
        /// <summary>
        /// 组团社下单
        /// </summary>
        组团社下单 = 0,
        /// <summary>
        /// 代客预定
        /// </summary>
        代客预定
    }

    #endregion

    #region 订单交易评价类型枚举

    /// <summary>
    /// 订单交易评价类型
    /// </summary>
    public enum RateType
    {
        /// <summary>
        /// 未评价
        /// </summary>
        未评价 = 0,
        /// <summary>
        /// 好评
        /// </summary>
        好评 = 1,
        /// <summary>
        /// 中评
        /// </summary>
        中评 = 2,
        /// <summary>
        /// 差评
        /// </summary>
        差评 = 3
    }

    #endregion
}
