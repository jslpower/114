using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 应付账款
    /// </summary>
    /// 鲁功原  2010-11-9
    [Serializable]
    public class Payments
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Payments()
		{}

		#region Model

		private string _id;
		private string _companyid;
		private string _tourid;
		private string _tourno;
		private string _routename;
		private DateTime? _leavedate;
		private string _companyname;
		private string _companytype;
		private int _pcount;
		private decimal _sumprice;
		private decimal _checkendprice;
		private decimal _nocheckedprice;
		private DateTime _cleartime;
		private string _operatorid;
		private DateTime _issuetime;
		/// <summary>
		/// 编号
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 所属公司
		/// </summary>
		public string CompanyId
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 团队编号
		/// </summary>
		public string TourId
		{
			set{ _tourid=value;}
			get{return _tourid;}
		}
		/// <summary>
		/// 团号
		/// </summary>
		public string TourNo
		{
			set{ _tourno=value;}
			get{return _tourno;}
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
		/// 出团时间
		/// </summary>
		public DateTime? LeaveDate
		{
			set{ _leavedate=value;}
			get{return _leavedate;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 供应商类型
		/// </summary>
		public string CompanyType
		{
			set{ _companytype=value;}
			get{return _companytype;}
		}
		/// <summary>
		/// 人数
		/// </summary>
		public int PCount
		{
			set{ _pcount=value;}
			get{return _pcount;}
		}
		/// <summary>
		/// 应付金额
		/// </summary>
		public decimal SumPrice
		{
			set{ _sumprice=value;}
			get{return _sumprice;}
		}
		/// <summary>
		/// 已审核已付金额
		/// </summary>
		public decimal CheckendPrice
		{
			set{ _checkendprice=value;}
			get{return _checkendprice;}
		}
		/// <summary>
		/// 未审核已付金额
		/// </summary>
		public decimal NoCheckedPrice
		{
			set{ _nocheckedprice=value;}
			get{return _nocheckedprice;}
		}
		/// <summary>
		/// 结算时间
		/// </summary>
		public DateTime ClearTime
		{
			set{ _cleartime=value;}
			get{return _cleartime;}
		}
		/// <summary>
		/// 操作人编号
		/// </summary>
		public string OperatorId
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}

		#endregion Model
    }
}
