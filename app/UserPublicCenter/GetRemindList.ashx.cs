using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Collections.Generic;

namespace UserPublicCenter.ashx
{
    /// <summary>
    /// 创建人：张新兵，创建时间：2011-06-17
    /// 创建内容 ：为首页提供异步获取登录下动态信息提醒的接口
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetRemindList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;

            //判断请求来源
            //当前只有来自UserPublic的请求是合法请求

            string referer = request.UrlReferrer.ToString();

            if (string.IsNullOrEmpty(referer))
            {
                response.End();
            }

            if (referer.ToLower().IndexOf(Domain.UserPublicCenter.ToLower()) == -1)
            {
                response.End();
            }

            IOrderedEnumerable<EyouSoft.Model.CommunityStructure.Remind> remindList = null;
            IList<EyouSoft.Model.CommunityStructure.Remind> list = EyouSoft.BLL.CommunityStructure.Remind.CreateInstance().GetList();
            if (list != null && list.Count > 0)
            {
                remindList = list.OrderByDescending(i => i.EventTime);

                foreach (EyouSoft.Model.CommunityStructure.Remind remind in remindList)
                {
                    /*
                     * <li style="margin-top: 0px; ">13:09
                    <font class="c-blue">试试加同业</font>为<a href="http://im.tongye114.com/" target="_blank">同业MQ</a>好友</li>
                     * */
                    response.Write("<li>");
                    response.Write(remind.EventTime.ToString("HH:mm"));
                    response.Write("<font class='c-blue'>&nbsp;");
                    if ((int)remind.TilteType != 10)
                    {
                        response.Write(Utils.GetText(remind.Operator,5));
                    }
                    else
                    {
                        response.Write(Utils.GetText(remind.Operator,7));
                    }
                    response.Write("</font>"); 
                    response.Write(remind.TypeLink);
                    response.Write("</li>");
                }
            }
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
