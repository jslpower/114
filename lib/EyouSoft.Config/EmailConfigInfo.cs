using System;

namespace EyouSoft.Config
{
	/// <summary>
	/// Email配置信息类
	/// </summary>
	[Serializable]
    public class EmailConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string smtp; //smtp 地址

		private int port = 25; //端口号

		private string sysemail;  //系统邮件地址

		private string username;  //邮件帐号

		private string password;  //邮件密码

        private string fromname; //发件人

        #endregion

        public EmailConfigInfo()
		{
        }

        #region 属性

        /// <summary>
		/// smtp服务器
		/// </summary>
		public string Smtp
		{
			get { return smtp;}
			set { smtp = value;}
		}

		/// <summary>
		/// 端口号
		/// </summary>
		public int Port
		{
			get { return port;}
			set { port = value;}
		}
		

		/// <summary>
		/// 系统Email地址
		/// </summary>
		public string Sysemail
		{
			get { return sysemail;}
			set { sysemail = value;}
		}


		/// <summary>
		/// 用户名
		/// </summary>
		public string Username
		{
			get { return username;}
			set { username = value;}
		}

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get { return password;}
			set { password = value;}
		}

        /// <summary>
        /// 发件人
        /// </summary>
        public string FromName
        {
            get { return fromname; }
            set { fromname = value; }
        }

        #endregion

    }
}
