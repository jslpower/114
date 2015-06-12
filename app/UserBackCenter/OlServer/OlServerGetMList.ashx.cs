using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using Newtonsoft.Json;
using EyouSoft.Common;

namespace WEB.OlServer
{
    /// <summary>
    /// 获取消息
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-25
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlServerGetMList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //请求发起用户的在线编号
            string olId = Utils.InputText(context.Request.Form["olid"]);
            //最后获取到的消息编号
            DateTime lastPostTime = Utils.GetDateTime(context.Request.Form["lastTime"],DateTime.Now);

            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
            IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> messages = bll.GetMessages(olId, lastPostTime);
            bll.SetLastSendMessageTime(olId, DateTime.Now);
            bll = null;

            context.Response.Write(JsonConvert.SerializeObject(messages));
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
