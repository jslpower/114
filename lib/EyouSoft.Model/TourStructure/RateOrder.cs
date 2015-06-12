using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 订单评价信息
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class RateOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RateOrder()
		{}

		#region Model

		private string _id;
		private string _buycompanyid;
		private string _buycompanyname;
		private string _buyuserid;
		private string _buyusercontactname;
		private string _sellcompanyid;
        private RateType _ratetype;
		private decimal _servicequality;
		private decimal _pricequality;
		private decimal _travelplan;
		private decimal _agreelevel;
		private decimal _ratescore;
		private string _ratecontent;
		private string _orderid;
		private string _tourid;
		private string _routename;
		private decimal _settlepeopleprice;
		private decimal _settlechildprice;
		private DateTime _issuetime;
		private DateTime _expiretime;
		private bool _issystemrate;

		/// <summary>
		/// 评价ID 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 买家公司ID
		/// </summary>
		public string BuyCompanyId
		{
			set{ _buycompanyid=value;}
			get{return _buycompanyid;}
		}
		/// <summary>
		/// 买家公司名称
		/// </summary>
		public string BuyCompanyName
		{
			set{ _buycompanyname=value;}
			get{return _buycompanyname;}
		}
		/// <summary>
		/// 买家用户ID
		/// </summary>
		public string BuyUserId
		{
			set{ _buyuserid=value;}
			get{return _buyuserid;}
		}
		/// <summary>
		/// 买家用户联系人名称
		/// </summary>
		public string BuyUserContactName
		{
			set{ _buyusercontactname=value;}
			get{return _buyusercontactname;}
		}
		/// <summary>
		/// 卖家公司ID
		/// </summary>
		public string SellCompanyId
		{
			set{ _sellcompanyid=value;}
			get{return _sellcompanyid;}
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
		/// 服务品质(星星数量由1-5颗)   默认为0
		/// </summary>
		public decimal ServiceQuality
		{
			set{ _servicequality=value;}
			get{return _servicequality;}
		}
		/// <summary>
		/// 性价比(星星数量由1-5颗)   默认为0
		/// </summary>
		public decimal PriceQuality
		{
			set{ _pricequality=value;}
			get{return _pricequality;}
		}
		/// <summary>
		/// 旅游内容安排(星星数量由1-5颗)   默认为0
		/// </summary>
		public decimal TravelPlan
		{
			set{ _travelplan=value;}
			get{return _travelplan;}
		}
		/// <summary>
		/// 满意度(分值由0-100分)   默认为0
		/// </summary>
		public decimal AgreeLevel
		{
			set{ _agreelevel=value;}
			get{return _agreelevel;}
		}
		/// <summary>
		/// 当次评价的总得分  默认为0
		/// </summary>
		public decimal RateScore
		{
			set{ _ratescore=value;}
			get{return _ratescore;}
		}
		/// <summary>
		/// 评价内容
		/// </summary>
		public string RateContent
		{
			set{ _ratecontent=value;}
			get{return _ratecontent;}
		}
		/// <summary>
		/// 订单ID  
		/// </summary>
		public string OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 团队ID 
		/// </summary>
		public string TourId
		{
			set{ _tourid=value;}
			get{return _tourid;}
		}
		/// <summary>
		/// 线路名称
		/// </summary>
		public string RouteName
		{
			set{ _routename=value;}
			get{return _routename;}
		}
		/// <summary>
		/// 成人价  默认值为0
		/// </summary>
		public decimal SettlePeoplePrice
		{
			set{ _settlepeopleprice=value;}
			get{return _settlepeopleprice;}
		}
		/// <summary>
		/// 儿童价   默认值为0
		/// </summary>
		public decimal SettleChildPrice
		{
			set{ _settlechildprice=value;}
			get{return _settlechildprice;}
		}
		/// <summary>
		/// 评价日期  默认值为getdate()
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}
		/// <summary>
		/// 评价到期日期(在第1次进行评价的时候就要写入[取对应订单中的ExpireTime时间])  默认值getdate()
		/// </summary>
		public DateTime ExpireTime
		{
			set{ _expiretime=value;}
			get{return _expiretime;}
		}
		/// <summary>
		/// 是否为系统默认评价  (0:否[用户评价]  1:是[系统评价]  默认为0)
		/// </summary>
		public bool IsSystemRate
		{
			set{ _issystemrate=value;}
			get{return _issystemrate;}
		}

		#endregion Model
    }
}
