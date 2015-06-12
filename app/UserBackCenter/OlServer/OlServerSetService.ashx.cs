using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using EyouSoft.Common;
namespace WEB.OlServer
{
    /// <summary>
    /// 在线客服-设置客服
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-25
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlServerSetService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //发起请求的在线用户编号
            string olId = Utils.InputText(context.Request.Form["olid"]);
            //要设置成的客服编号
            string serviceId = Utils.InputText(context.Request.Form["serviceid"]);
            //要设置成的客服名称
            string serviceName = Utils.InputText(context.Request.Form["servicename"]);

            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
            bll.SetService(olId, serviceId, serviceName);

            bll = null;
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
