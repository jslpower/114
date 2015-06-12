using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.ConsoleApp
{
    /// <summary>
    /// 长期未登录短信提醒
    /// </summary>
    public static class NoLoginSms
    {
        public static void Run()
        {
            NoLoginSmsService service = new NoLoginSmsService();
            service.SendUserSms();
        }
    }

    public class NoLoginSmsService : DALBase
    {

        SMSService.Service sms = new EyouSoft.ConsoleApp.SMSService.Service();
        string EmailTemplate = string.Empty;

        public NoLoginSmsService()
        {
            EmailTemplate = System.IO.File.ReadAllText(@"config/SMS.txt", System.Text.Encoding.GetEncoding("gb2312"));
        }

        public void SendUserSms()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("select substring(contactname,1,4) as contactname,ltrim(ContactMobile) as contactmobile from tbl_CompanyUser where mq in ");
            sb.Append("(select im_uid from dbo.im_member where datediff(mm,im_latest_time,getdate())>0 and im_latest_time<>'0000-00-00 00:00:00')");
            sb.Append(" and (ContactMobile<>'' and ISNUMERIC(ContactMobile)=1 and len(ltrim(ContactMobile))=11 and contactname<>'')");
            DataSet ds = new DataSet();
            DbCommand dc = this.SystemStore.GetSqlStringCommand(sb.ToString());
            this.SystemStore.LoadDataSet(dc, ds, "ContactEmail");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    Console.Write(dr["contactname"].ToString() + "\n");
                    SendSMS(dr["contactmobile"].ToString(), EmailTemplate.Replace("{name}", dr["contactname"].ToString()));
                    System.Threading.Thread.Sleep(2000);
                }
                catch { }
            }
            ds.Dispose();
        }

        private void SendSMS(string mobilno, string content)
        {
            sms.SendSms(string.Empty, mobilno, content, "yn2", "851111");
        }
    }
}
