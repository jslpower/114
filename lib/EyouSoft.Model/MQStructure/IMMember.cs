using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// MQ用户信息实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMMember
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMember()
		{}

		#region Model

		private int _im_uid;
		private string _im_password;
		private int _im_status;
        private DateTime _im_latest_time;
		private string _im_displayname;
		private string _im_username;
		private string _bs_uid;

		/// <summary>
		/// MQ编号
		/// </summary>
		public int im_uid
		{
			set{ _im_uid=value;}
			get{return _im_uid;}
		}
		/// <summary>
		/// MD5密码
		/// </summary>
		public string im_password
		{
			set{ _im_password=value;}
			get{return _im_password;}
		}
		/// <summary>
		/// 在线状态
		/// </summary>
		public int im_status
		{
			set{ _im_status=value;}
			get{return _im_status;}
		}
		/// <summary>
		/// MQ最后登录时间
		/// </summary>
		public DateTime im_latest_time
		{
			set{ _im_latest_time=value;}
			get{return _im_latest_time;}
		}
		/// <summary>
		/// MQ显示名称
		/// </summary>
		public string im_displayname
		{
			set{ _im_displayname=value;}
			get{return _im_displayname;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string im_username
		{
			set{ _im_username=value;}
			get{return _im_username;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public string bs_uid
		{
			set{ _bs_uid=value;}
			get{return _bs_uid;}
		}
        /// <summary>
        /// 登陆方式的标识   true:程序登录,false:客户登陆
        /// </summary>
        public bool IsAutoLogin
        {
            set;
            get;
        }
		#endregion Model
    }

    #region MQ查找好友实体

    /// <summary>
    /// MQ用户实体(含简单的公司信息及平台用户信息，MQ查找好友实体)
    /// </summary>
    [Serializable]
    public class IMMemberAndUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMemberAndUser() { }

        /// <summary>
        /// MQID
        /// </summary>
        public int MQId { get; set; }

        /// <summary>
        /// MQ用户名
        /// </summary>
        public string MQUserName { get; set; }
        /// <summary>
        /// MQ显示名称(昵称)
        /// </summary>
        public string MQDisplayName { get; set; }

        /// <summary>
        /// 平台用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public Model.CompanyStructure.BusinessProperties CompanyType { get; set; }

        /// <summary>
        /// 公司所在城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 公司所在城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 公司所在省份编号
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// MQ在线状态
        /// </summary>
        public int MQStatus { get; set; }
    }

    #endregion

    #region MQ好友查找方式枚举

    /// <summary>
    /// MQ好友查找方式枚举
    /// </summary>
    public enum MQFriendSearchType
    {
        /// <summary>
        /// 精确查找
        /// </summary>
        精确查找 = 0,
        /// <summary>
        /// 找专线商
        /// </summary>
        找专线商 = 1,
        /// <summary>
        /// 找组团社
        /// </summary>
        找组团社 = 2,
        /// <summary>
        /// 找地接社
        /// </summary>
        找地接社 = 3,
        /// <summary>
        /// 找其他企业
        /// </summary>
        找其他企业 = 4
    }

    #endregion
}
