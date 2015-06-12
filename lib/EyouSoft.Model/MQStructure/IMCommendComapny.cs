using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// MQ推荐批发商实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMCommendComapny
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMCommendComapny()
		{}

		#region Model

		private int _id;
		private string _operatorid;
		private string _operatorname;
		private string _companyid;
		private string _companyname;
		private string _desccompanyname;
		private string _desccontactname;
		private string _desccontacttel;
		private string _desccontactmobile;
		private string _desccompanyarea;
		private DateTime _issuetime;
		/// <summary>
		/// 编号
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 推荐人编号
		/// </summary>
		public string OperatorId
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 推荐人姓名
		/// </summary>
		public string OperatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
		}
		/// <summary>
		/// 推荐人公司编号
		/// </summary>
		public string CompanyId
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 推荐人公司名称
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 被推荐的公司名称
		/// </summary>
		public string DescCompanyName
		{
			set{ _desccompanyname=value;}
			get{return _desccompanyname;}
		}
		/// <summary>
		/// 被推荐的联系人姓名
		/// </summary>
		public string DescContactName
		{
			set{ _desccontactname=value;}
			get{return _desccontactname;}
		}
		/// <summary>
		/// 被推荐的联系人电话
		/// </summary>
		public string DescContactTel
		{
			set{ _desccontacttel=value;}
			get{return _desccontacttel;}
		}
		/// <summary>
		/// 被推荐的联系人手机
		/// </summary>
		public string DescContactMobile
		{
			set{ _desccontactmobile=value;}
			get{return _desccontactmobile;}
		}
		/// <summary>
		/// 被推荐的公司线路区域
		/// </summary>
		public string DescCompanyArea
		{
			set{ _desccompanyarea=value;}
			get{return _desccompanyarea;}
		}
		/// <summary>
		/// 推荐的时间
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}

		#endregion Model
    }
}
