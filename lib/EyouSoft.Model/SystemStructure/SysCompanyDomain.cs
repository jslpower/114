using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 客户域名
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysCompanyDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCompanyDomain()
		{}

		#region Model

		private int _id;
		private string _companyid;
        private CompanyStructure.CompanyType _companytype;
		private string _companyname;
		private string _domain;
        private DomainType _domaintype;
		private string _gotourl;
		private DateTime _issuetime;
        private bool _isdisabled;

		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 公司编号
		/// </summary>
		public string CompanyId
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 公司类型
		/// </summary>
        public CompanyStructure.CompanyType CompanyType
		{
			set{ _companytype=value;}
			get{return _companytype;}
		}
		/// <summary>
		/// 公司名称
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 域名
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}

        /// <summary>
        /// 域名类型
        /// </summary>
        public DomainType DomainType
        {
            set { _domaintype = value; }
            get { return _domaintype; }
        }

		/// <summary>
		/// 跳转地址
		/// </summary>
		public string GoToUrl
		{
			set{ _gotourl=value;}
			get{return _gotourl;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}
        /// <summary>
        /// 是否禁用 0:否 1:是
        /// </summary>
        public bool IsDisabled
        {
            set { _isdisabled = value; }
            get { return _isdisabled; }
        }

		#endregion Model
    }

    #region enum域名类型

    /// <summary>
    /// 域名类型
    /// </summary>
    public enum DomainType
    {
        /// <summary>
        /// 网店域名
        /// </summary>
        网店域名 = 1,
        /// <summary>
        /// 软件域名
        /// </summary>
        软件域名
    }

    #endregion
}
