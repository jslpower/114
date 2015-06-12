using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using EyouSoft.Common;
namespace WEB.OlServer
{
    /// <summary>
    /// 在线客服-保存聊天记录
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-28
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlServerSaveMessages : IHttpHandler
    {
        #region Attributes
        /// <summary>
        /// 保存消息记录的文件名称
        /// </summary>
        private  string fileName = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss")+"_聊天记录.txt";
        #endregion Attributes

        public void ProcessRequest(HttpContext context)
        {
            string saveFileName = HttpUtility.UrlEncode(fileName);
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.Charset = "utf-8";
            context.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}", saveFileName));
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            context.Response.ContentType = "text/plain";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("消息记录\r\n\r\n");
            sb.Append("=================================================\r\n");
            sb.AppendFormat("消息保存时间:{0}\r\n", DateTime.Now);
            sb.Append("=================================================\r\n\r\n");

            string olId = Utils.InputText(context.Request.QueryString["olid"]);
            bool isService = Utils.GetInt(context.Request.QueryString["service"], 0) == 2009 ? true : false;
            EyouSoft.Model.OnLineServer.OlServerUserInfo info = OlServerUtility.GetOlCookieInfo(isService);

            if (info != null && info.OId == olId)
            {
                EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
                IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> items = bll.GetMessages(olId, null);
                bll = null;

                foreach (EyouSoft.Model.OnLineServer.OlServerMessageInfo item in items)
                {
                    if (item.SendId == olId)
                    {
                        sb.AppendFormat("{0} {1} 对 {2} 说：\r\n{3}\r\n\r\n", item.SendTime, "我", item.AcceptName, item.Message);
                    }
                    else
                    {
                        sb.AppendFormat("{0} {1} 对 {2} 说：\r\n{3}\r\n\r\n", item.SendTime, item.SendName, "我", item.Message);
                    }
                }

                items = null;
            }

            context.Response.Write(sb.ToString());
            context.Response.End(); 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
