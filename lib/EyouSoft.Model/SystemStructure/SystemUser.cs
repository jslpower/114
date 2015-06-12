using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统用户 实体
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SystemUser
    {
        private PassWord _passwordinfo = new PassWord();
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemUser()
		{}

		#region Model

		private int _id;
		private string _username;
		private string _contactname;
		private string _contacttel;
		private string _contactfax;
		private string _contactmobile;
		private string _permissionlist;
        private bool _isdisable;
		private DateTime _issuetime;

		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
        /// <summary>
        /// 用户密码(在应用层设置时,只需设置其NoEncryptPassword属性)
        /// </summary>
        public PassWord PassWordInfo { get { return this._passwordinfo; } set { this._passwordinfo = value; } }
		/// <summary>
		/// 姓名
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
		/// 手机
		/// </summary>
		public string ContactMobile
		{
			set{ _contactmobile=value;}
			get{return _contactmobile;}
		}

		/// <summary>
		/// 角色权限列表
		/// </summary>
		public string PermissionList
		{
			set{ _permissionlist=value;}
			get{return _permissionlist;}
		}

        /// <summary>
        /// 是否停用 0：启用；1：停用  默认为0
        /// </summary>
        public bool IsDisable
        {
            set { _isdisable = value; }
            get { return _isdisable; }
        }

		/// <summary>
		/// 创建时间(默认为getdate())
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
        }

        #region 扩展属性

        private IList<int> _areaid;

        /// <summary>
        /// 系统用户区域ID
        /// </summary>
        public IList<int> AreaId
        {
            set { _areaid = value; }
            get { return _areaid; }
        }

        /// <summary>
        /// 系统用户所能查看的易诺用户池客户类型
        /// </summary>
        public IList<int> CustomerTypeIds { get; set; }

        #endregion

        #endregion Model
    }

    /// <summary>
    /// 系统用户公司管理区域查询条件实体
    /// </summary>
    /// 张志瑜   2010-7-28
    [Serializable]
    public class QueryParamsSysUserArea
    {
        /// <summary>
        /// 运营后台系统用户所能管理的城市ID列表,默认为null,若=null,或无任何ID,则不作为查询条件
        /// </summary>
        public int[] ManageCity { get; set; }
        /// <summary>
        /// 运营后台系统用户所能管理的公司类型列表,默认为null,若=null,或无任何ID,则不作为查询条件
        /// </summary>
        public List<EyouSoft.Model.CompanyStructure.CompanyType> ManageCompanyType { get; set; }
    }
}
