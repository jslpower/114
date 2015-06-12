using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model.MQStructure;
using EyouSoft.Common.Mail;

namespace EyouSoft.ConsoleApp
{
    /// <summary>
    /// 长期未登录邮件提醒
    /// </summary>
    public static class NoLoginMail
    {
        public static void Run()
        {
            NoLoginMailService service = new NoLoginMailService();
            service.SendUserMail();
        }
    }
    
    public class NoLoginMailService : DALBase
    {
        SysMailMessage ESM = new SysMailMessage();
        EyouSoft.Config.EmailConfigInfo config = null;
        string EmailTemplate = string.Empty;

        public NoLoginMailService()
        {
            config = EyouSoft.Config.EmailConfigs.GetConfig();
            EmailTemplate = System.IO.File.ReadAllText(@"config/email.txt", System.Text.Encoding.GetEncoding("gb2312"));
        }

        public void SendUserMail()
        {
            DataSet ds = new DataSet();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("select UserName,MQ,ContactEmail,Password from tbl_CompanyUser where ContactEmail<>'' and datediff(month,LastLoginTime,getdate())>0");
            this.SystemStore.LoadDataSet(dc, ds, "ContactEmail");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    Console.Write(dr["UserName"].ToString() + "\n");
                    SendMail("allenjsl@qq.com", dr["UserName"].ToString(), dr["MQ"].ToString(), dr["Password"].ToString());
                    System.Threading.Thread.Sleep(2000);
                }
                catch { }
            }
            ds.Dispose();
            //System.IO.StreamReader dr = new System.IO.StreamReader(@"C:\邮箱汇总.txt");
            //while (dr.Peek() > 0)
            //{
            //    try
            //    {
            //        string email = dr.ReadLine();
            //        Console.Write(email + "\n");
            //        SendMail(email.Trim(), "test", "");
            //        System.Threading.Thread.Sleep(2000);
            //    }
            //    catch { }
            //}
        }

        private void SendMail(string email, string username, string mq, string password)
        {
            //ESM..Priority = "Normal";
            ESM.AddRecipient(email);
            ESM.MailDomainPort = config.Port;
            ESM.From = config.Sysemail;//设定发件人地址(必须填写)
            ESM.FromName = config.FromName;
            ESM.Html = true;//设定正文是否HTML格式。
            ESM.Subject = "你的客户给你留下了新信息呦，很久没在同业MQ上看到你了！";
            ESM.Body = "" + EmailTemplate.Replace("{username}", username).Replace("{passwd}", password) + "";
            ESM.MailDomain = config.Smtp;
            //也可将将SMTP信息一次设置完成。写成
            ESM.MailServerUserName = config.Username;
            ESM.MailServerPassWord = config.Password;
            ESM.Send();
        }
    }
}
