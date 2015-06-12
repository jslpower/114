using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// MQ消息提醒中转地址实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMMessageUrl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessageUrl()
		{}

		#region Model
		private string _redirecturl = "";
		private int _uid = 0;
        private string _md5pw = "";

		/// <summary>
		/// 跳转到的URL
		/// </summary>
		public string RedirectUrl
		{
			set{ _redirecturl=value;}
			get{return _redirecturl;}
		}
		/// <summary>
        /// 用户MQID
		/// </summary>
		public int MQID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
        /// <summary>
        /// MD5加密密码
        /// </summary>
        public string MD5Pw
        {
            set { _md5pw = value; }
            get { return _md5pw; }
        }
		#endregion Model
    }
}
