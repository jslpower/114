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
    /// 在线客服用户退出
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-27
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlserverExit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //请求退出的在线用户编号
            string olId = Utils.InputText(context.Request.Form["olid"]);
            //是否客服端发起的请求
            bool isService = Utils.GetInt(context.Request["service"], -1) == 2009 ? true : false;

            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();

            string exitResultString = bll.Exit(olId, isService) ? "1" : "0";

            context.Response.Write(exitResultString);
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
