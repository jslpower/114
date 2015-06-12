using System;
namespace EyouSoft.Common.Function
{
	/// <summary>
	/// Email 的摘要说明。
	/// </summary>
	public class Email
	{
		private string _ToEmail;//收件人
		private string _FromEmail;//发件人
		private string _EmailSubject;//邮件主题
		private string _EmailBody;//邮件内容
		private string _SmtpServer;
		private bool _IsAuth = false;//是否支持服务器验证
		private string _EmailUserName;//服务器验证的用户
		private string _EmailPassword;//服务器验证的用户密码
		public Email()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 模块
		/// <summary>
		/// 设置收件人地址
		/// </summary>
		public string ToEmail
		{
			set{_ToEmail = value;}
			get{return _ToEmail;}
		}
		/// <summary>
		/// 发件人EMAIL
		/// </summary>
		public string FromEmail
		{
			set{_FromEmail = value;}
			get{return _FromEmail;}
		}
		/// <summary>
		/// 邮件主题
		/// </summary>
		public string EmailSubject
		{
			set{_EmailSubject = value;}
			get{return _EmailSubject;}
		}
		/// <summary>
		/// 邮件主体
		/// </summary>
		public string EmailBody
		{
			set{_EmailBody = value;}
			get{return _EmailBody;}
		}
		/// <summary>
		/// 设置是否用户验证
		/// </summary>
		public bool IsAuth
		{
			set{ _IsAuth=value;}
			get{return _IsAuth;}
		}
		/// <summary>
		/// SMTP服务器
		/// </summary>
		public string SmtpServer
		{
			set{_SmtpServer = value;}
			get{return _SmtpServer;}
		}
		/// <summary>
		/// 验证用户名
		/// </summary>
		public string EmailUserName
		{
			set{_EmailUserName = value;}
			get{return _EmailUserName;}
		}
		/// <summary>
		/// 验证密码
		/// </summary>
		public string EmailPassword
		{
			set{_EmailPassword = value;}
			get{return _EmailPassword;}
		}
		#endregion
		#region 函数
		/// <summary>
		/// 发送邮件 
		/// </summary>
		/// <returns>true：发送成功，flase：发送失败</returns>
		public bool SendEmail()
		{
			//创建MailMessage对象
			System.Web.Mail.MailMessage mailMsg = new System.Web.Mail.MailMessage();
			//设置收件人的邮件地址
			mailMsg.To = _ToEmail;
			//设置发送者的邮件地址
			mailMsg.From = _FromEmail;
			//设置邮件主题
			mailMsg.Subject = _EmailSubject;
			//设置邮件内容
			mailMsg.Body = _EmailBody;
			//设置支持服务器验证
			if(_IsAuth == true)
			{
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
				//设置用户名
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",_EmailUserName);
				//设置用户密码
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword",_EmailPassword);
			}
			try
			{
				//设置发送邮件服务器
				System.Web.Mail.SmtpMail.SmtpServer = _SmtpServer;
				//发送邮件
				System.Web.Mail.SmtpMail.Send(mailMsg);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
