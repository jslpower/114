using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统信息 实体
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SystemInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemInfo()
		{}

		#region Model

		private int _id;
		private string _systemname;
		private string _contactname;
		private string _contacttel;
		private string _contactmobile;
		private string _qq1;
		private string _qq2;
		private string _qq3;
		private string _qq4;
		private string _qq5;
		private string _msn;
		private string _systemremark;
		private string _agencylog;
		private string _unionlog;
		private string _allright;

		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 系统名称
		/// </summary>
		public string SystemName
		{
			set{ _systemname=value;}
			get{return _systemname;}
		}
		/// <summary>
		/// 系统联系人
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
		/// 手机
		/// </summary>
		public string ContactMobile
		{
			set{ _contactmobile=value;}
			get{return _contactmobile;}
		}
		/// <summary>
		/// QQ1
		/// </summary>
		public string QQ1
		{
			set{ _qq1=value;}
			get{return _qq1;}
		}
		/// <summary>
		/// QQ2
		/// </summary>
		public string QQ2
		{
			set{ _qq2=value;}
			get{return _qq2;}
		}
		/// <summary>
		/// QQ3
		/// </summary>
		public string QQ3
		{
			set{ _qq3=value;}
			get{return _qq3;}
		}
		/// <summary>
		/// QQ4
		/// </summary>
		public string QQ4
		{
			set{ _qq4=value;}
			get{return _qq4;}
		}
		/// <summary>
		/// QQ5
		/// </summary>
		public string QQ5
		{
			set{ _qq5=value;}
			get{return _qq5;}
		}
		/// <summary>
		/// Msn
		/// </summary>
		public string Msn
		{
			set{ _msn=value;}
			get{return _msn;}
		}

        /// <summary>
        /// 系统MQ
        /// </summary>
        public string MQ { get; set; }

		/// <summary>
		/// 系统介绍
		/// </summary>
		public string SystemRemark
		{
			set{ _systemremark=value;}
			get{return _systemremark;}
		}
		/// <summary>
		/// 零售商端Log
		/// </summary>
		public string AgencyLog
		{
			set{ _agencylog=value;}
			get{return _agencylog;}
		}
		/// <summary>
		/// 系统图标
		/// </summary>
		public string UnionLog
		{
			set{ _unionlog=value;}
			get{return _unionlog;}
		}
		/// <summary>
		/// 首页版权
		/// </summary>
		public string AllRight
		{
			set{ _allright=value;}
			get{return _allright;}
        }

        #region 扩展属性

        private IList<SysAreaContact> _sysareacontact;

        /// <summary>
        /// 区域联系人
        /// </summary>
        public IList<SysAreaContact> SysAreaContact
        {
            set { _sysareacontact = value; }
            get { return _sysareacontact; }
        }

        #endregion

        #endregion Model
    }
}
