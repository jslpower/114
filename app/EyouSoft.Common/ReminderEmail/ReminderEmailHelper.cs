using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace EyouSoft.Common.Email
{
    public class ReminderEmailHelper
    {
        /// <summary>
        /// 发送提醒用户审核通过的电子邮件
        /// </summary>
        /// <param name="userName">注册审核通过的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名所对应的邮箱地址</param>
        /// <param name="userPwd">注册审核通过的公司的管理员用户名密码</param>
        /// <returns></returns>
        public static bool SendRegPassEmail(string userName, string emailAddress,string userPwd)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            MailMessage msg = ReminderEmailReader.LoadRegPassEmail(userName, emailAddress,userPwd);
            SmtpClient smtp = CreateSmtpClient();
            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (System.Exception ex)
            {
                //log ex，后期应对发送失败的邮件进行记录，以便对邮件发送程序进行优化
                return false;
            }
        }
        /// <summary>
        /// 发送加好友提醒Email
        /// </summary>
        /// <param name="userName">收到添加好友请求的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名对应的邮箱地址</param>
        /// <param name="userPwd">收到添加好友请求的公司的管理员用户名密码</param>
        /// <param name="friendCompanyName">添加好友请求的公司名称</param>
        /// <returns></returns>
        public static bool SendAddFriendEmail(string userName, string emailAddress,string userPwd ,string friendCompanyName)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            MailMessage msg = ReminderEmailReader.LoadAddFriendEmail(userName, emailAddress,userPwd,friendCompanyName);
            SmtpClient smtp = CreateSmtpClient();

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (System.Exception ex)
            {
                //log ex，后期应对发送失败的邮件进行记录，以便对邮件发送程序进行优化
                return false;
            }
        }
        /// <summary>
        /// 发送订单消息提醒Email
        /// </summary>
        /// <param name="userName">收到订单的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名对应的邮箱地址</param>
        /// <param name="userPwd">收到添加好友请求的公司的管理员用户名密码</param>
        /// <param name="makeOrderCompanyName">下订单的公司名称</param>
        /// <param name="makeOrderUserName">下订单的用户姓名</param>
        /// <returns></returns>
        public static bool SendOrderEmail(string userName, string emailAddress, string userPwd, string makeOrderCompanyName, string makeOrderUserName)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            MailMessage msg = ReminderEmailReader.LoadOrderEmail(userName, emailAddress, userPwd, makeOrderCompanyName, makeOrderUserName);
            SmtpClient smtp = CreateSmtpClient();

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (System.Exception ex)
            {
                //log ex，后期应对发送失败的邮件进行记录，以便对邮件发送程序进行优化
                return false;
            }
        }

        /// <summary>
        /// 发送 同业114用户密码修复 邮件
        /// </summary>
        /// <param name="emailAddress">要进行密码修复的用户的邮件地址</param>
        /// <param name="updateFindPwdLink">进行密码修复的链接地址</param>
        /// <param name="forgetPwdLink">[忘记密码]页面地址</param>
        /// <returns></returns>
        public static bool SendUpdatePasswordEmail(string emailAddress, string updateFindPwdLink, string forgetPwdLink)
        {
            MailMessage msg = ReminderEmailReader.LoadFindPassWordEmail(emailAddress,updateFindPwdLink,forgetPwdLink);
            SmtpClient smtp = CreateSmtpClient();

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (System.Exception ex)
            {
                //log ex，后期应对发送失败的邮件进行记录，以便对邮件发送程序进行优化
                return false;
            }
        }

        private static SmtpClient CreateSmtpClient()
        {
            SmtpClient smtp = new SmtpClient();
            bool IsEnableSSL = false;
            bool.TryParse(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("IsEnableSSL"), out IsEnableSSL);
            smtp.EnableSsl = IsEnableSSL;

            return smtp;
        }
    }
}
