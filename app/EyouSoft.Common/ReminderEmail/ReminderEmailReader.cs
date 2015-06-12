using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Xml;
using System.Web;

namespace EyouSoft.Common.Email
{
    public class ReminderEmailReader
    {
        /// <summary>
        /// 初始化审核通过提醒Email
        /// </summary>
        /// <param name="userName">注册审核通过的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名所对应的邮箱地址</param>
        /// <param name="userPwd">注册审核通过的公司的管理员用户名密码</param>
        /// <returns></returns>
        public static MailMessage LoadRegPassEmail(string userName, string emailAddress,string userPwd)
        {
            XmlDocument doc = null;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            //从缓存中加载，是否有xml数据
            doc = cache["ReminderEmail_RegPassEmail_XmlData"] as XmlDocument;

            //如果缓存中没有数据，从RegPassEmail.xml中的数据，初始化审核通过提醒Email
            if (doc == null)
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\app_data\ReminderEmail\RegPassEmail.xml";
                doc = new XmlDocument();
                doc.Load(fileName);

                //将数据添加到缓存中
                cache.Insert("ReminderEmail_RegPassEmail_XmlData", doc, new System.Web.Caching.CacheDependency(fileName));
            }

            XmlElement el = doc.DocumentElement;
            XmlNode nodeSubject = el.ChildNodes[0].ChildNodes[0];
            XmlNode nodeBody = el.ChildNodes[1].ChildNodes[0];
            XmlNode nodefrom = el.ChildNodes[2].ChildNodes[0];
            XmlNode nodedisplay = el.ChildNodes[3].ChildNodes[0];

            MailMessage mailMsg = new MailMessage();
            mailMsg.Subject = nodeSubject.Value;
            mailMsg.SubjectEncoding = Encoding.GetEncoding("gb2312");
            mailMsg.From = new MailAddress(nodefrom.Value, nodedisplay.Value, Encoding.GetEncoding("UTF-8"));
            mailMsg.To.Clear();
            mailMsg.To.Add(emailAddress);

            string str = nodeBody.Value;

            str = str.Replace("$UserName$", userName);
            str = str.Replace("$PassWord$", userPwd);

            mailMsg.Body = str;
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = Encoding.UTF8;

            return mailMsg;
        }
        /// <summary>
        /// 初始化加好友提醒Email
        /// </summary>
        /// <param name="userName">收到添加好友请求的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名对应的邮箱地址</param>
        /// <param name="userPwd">收到添加好友请求的公司的管理员用户名密码</param>
        /// <param name="friendCompanyName">添加好友请求的公司名称</param>
        /// <returns></returns>
        public static MailMessage LoadAddFriendEmail(string userName, string emailAddress,string userPwd, string friendCompanyName)
        {
            XmlDocument doc = null;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            //从缓存中加载，是否有xml数据
            doc = cache["ReminderEmail_AddFriendEmail_XmlData"] as XmlDocument;

            //如果缓存中没有数据，从RegPassEmail.xml中的数据，初始化审核通过提醒Email
            if (doc == null)
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\app_data\ReminderEmail\AddFriendEmail.xml";
                doc = new XmlDocument();
                doc.Load(fileName);

                //将数据添加到缓存中
                cache.Insert("ReminderEmail_AddFriendEmail_XmlData", doc, new System.Web.Caching.CacheDependency(fileName));
            }

            XmlElement el = doc.DocumentElement;
            XmlNode nodeSubject = el.ChildNodes[0].ChildNodes[0];
            XmlNode nodeBody = el.ChildNodes[1].ChildNodes[0];
            XmlNode nodefrom = el.ChildNodes[2].ChildNodes[0];
            XmlNode nodedisplay = el.ChildNodes[3].ChildNodes[0];

            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress(nodefrom.Value,nodedisplay.Value, Encoding.GetEncoding("UTF-8"));
            mailMsg.To.Clear();
            mailMsg.To.Add(emailAddress);


            string subject = nodeSubject.Value;
            subject = subject.Replace("$FriendCompanyName$", friendCompanyName);
            mailMsg.Subject = subject;
            mailMsg.SubjectEncoding = Encoding.UTF8;

            string str = nodeBody.Value;

            str = str.Replace("$UserName$", userName);
            str = str.Replace("$FriendCompanyName$", friendCompanyName);
            str = str.Replace("$PassWord$", userPwd);

            mailMsg.Body = str;
            mailMsg.IsBodyHtml = true;

            return mailMsg;
        }
        /// <summary>
        /// 根据指定的用户名和邮箱地址，初始化订单消息提醒Email
        /// </summary>
        /// <param name="userName">收到订单的公司的管理员用户名</param>
        /// <param name="emailAddress">用户名对应的邮箱地址</param>
        /// <param name="userPwd">收到添加好友请求的公司的管理员用户名密码</param>
        /// <param name="makeOrderCompanyName">下订单的公司名称</param>
        /// <param name="makeOrderUserName">下订单的用户姓名</param>
        /// <returns></returns>
        public static MailMessage LoadOrderEmail(string userName, string emailAddress, string userPwd, string makeOrderCompanyName, string makeOrderUserName)
        {
            XmlDocument doc = null;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            //从缓存中加载，是否有xml数据
            doc = cache["ReminderEmail_OrderEmail_XmlData"] as XmlDocument;

            //如果缓存中没有数据，从RegPassEmail.xml中的数据，初始化审核通过提醒Email
            if (doc == null)
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\app_data\ReminderEmail\OrderEmail.xml";
                doc = new XmlDocument();
                doc.Load(fileName);

                //将数据添加到缓存中
                cache.Insert("ReminderEmail_OrderEmail_XmlData", doc, new System.Web.Caching.CacheDependency(fileName));
            }

            XmlElement el = doc.DocumentElement;
            XmlNode nodeSubject = el.ChildNodes[0].ChildNodes[0];
            XmlNode nodeBody = el.ChildNodes[1].ChildNodes[0];
            XmlNode nodefrom = el.ChildNodes[2].ChildNodes[0];
            XmlNode nodedisplay = el.ChildNodes[3].ChildNodes[0];

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(nodefrom.Value, nodedisplay.Value, Encoding.GetEncoding("UTF-8"));
            mailMsg.To.Clear();
            mailMsg.To.Add(emailAddress);

            string subject = nodeSubject.Value;
            subject = subject.Replace("$MakeOrderCompanyName$", makeOrderCompanyName);
            subject = subject.Replace("$MakeOrderUserName$", makeOrderUserName);
            mailMsg.Subject = subject;
            mailMsg.SubjectEncoding = Encoding.UTF8;

            string str = nodeBody.Value;

            str = str.Replace("$UserName$", userName);
            str = str.Replace("$MakeOrderCompanyName$", makeOrderCompanyName);
            str = str.Replace("$MakeOrderUserName$", makeOrderUserName);
            str = str.Replace("$PassWord$", userPwd);

            mailMsg.Body = str;
            mailMsg.IsBodyHtml = true;

            return mailMsg;
        }

        /// <summary>
        /// 初始化 同业114用户密码修复 邮件信息
        /// </summary>
        /// <param name="emailAddress">要进行密码修复的用户的邮件地址</param>
        /// <param name="updateFindPwdLink">进行密码修复的链接地址</param>
        /// <param name="forgetPwdLink">[忘记密码]页面地址</param>
        /// <returns></returns>
        public static MailMessage LoadFindPassWordEmail(string emailAddress, string updateFindPwdLink, string forgetPwdLink)
        {
            XmlDocument doc = null;
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            //从缓存中加载，是否有xml数据
            doc = cache["ReminderEmail_FindPasswordEmail_XmlData"] as XmlDocument;

            //如果缓存中没有数据，从RegPassEmail.xml中的数据，初始化审核通过提醒Email
            if (doc == null)
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\app_data\FindPassWordEmail.xml";
                doc = new XmlDocument();
                doc.Load(fileName);

                //将数据添加到缓存中
                cache.Insert("ReminderEmail_FindPasswordEmail_XmlData", doc, new System.Web.Caching.CacheDependency(fileName));
            }

            XmlElement el = doc.DocumentElement;
            XmlNode nodeSubject = el.ChildNodes[0].ChildNodes[0];
            XmlNode nodeBody = el.ChildNodes[1].ChildNodes[0];
            XmlNode nodefrom = el.ChildNodes[2].ChildNodes[0];
            XmlNode nodedisplay = el.ChildNodes[3].ChildNodes[0];

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(nodefrom.Value, nodedisplay.Value, Encoding.GetEncoding("UTF-8"));
            mailMsg.To.Clear();
            mailMsg.To.Add(emailAddress);

            mailMsg.Subject = nodeSubject.Value;
            mailMsg.SubjectEncoding = Encoding.UTF8;

            string str = nodeBody.Value;

            str = str.Replace("$UpdateFindPwdLink$", updateFindPwdLink);
            str = str.Replace("$ForgetPwdLink$", forgetPwdLink);

            mailMsg.Body = str;
            mailMsg.IsBodyHtml = true;

            return mailMsg;
        }

    }
}
