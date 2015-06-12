using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using EyouSoft.Common;
using Newtonsoft.Json;

namespace WEB.OlServer
{
    /// <summary>
    /// 在线客服-获取在线用户信息
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-24
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OlServerGetUList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //返回的JSON字符串
            string usersInfoJsonString = "[]";

            //请求发起用户的在线编号
            string olId = Utils.InputText(context.Request["olid"]);
            //是否客服端发起的请求
            bool isService = Utils.GetInt(context.Request["service"], -1) == 2009 ? true : false;

            //清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
            OlServerUtility.ClearUserOut();

            EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
            System.Collections.Generic.IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> users = bll.GetOlUsers(olId, isService);

            usersInfoJsonString = JsonConvert.SerializeObject(users); 
            users = null;

            context.Response.Write(usersInfoJsonString);
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
