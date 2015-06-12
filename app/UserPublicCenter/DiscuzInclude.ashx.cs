using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using EyouSoft.Common;

namespace UserPublicCenter
{
    /// <summary>
    /// 同业社区首页调用此程序生成“同业之星访谈”
    /// </summary>
    /// 周文超 2010-09-17
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DiscuzInclude : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string callback = Utils.InputText(context.Request.QueryString["callback"]);
            System.Text.StringBuilder strHtml = new System.Text.StringBuilder();
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = list = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetTopNumPicList(1, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, EyouSoft.Model.CommunityStructure.TopicAreas.同业之星, true, null);
            if (list != null && list.Count > 0)
            {
                strHtml.Append("<table class='margin10' cellspacing='0' cellpadding='0' width='100%' border='0'>");
                strHtml.Append("<tr><td class='hangleft'><strong>同业之星访谈</strong></td></tr>");
                strHtml.Append("<tr><td class='hanglk'><table cellspacing='0' cellpadding='0' width='100%' border='0'>");
                strHtml.Append("<tr><td width='40%'>");
                strHtml.AppendFormat("<a target='_blank' href='{0}/SupplierInfo/ArticleInfo.aspx?id={1}'><img height='80' src='{2}' width='80' border='0'></a>", Domain.UserPublicCenter, list[0].ID, Domain.FileSystem + list[0].ImgThumb);
                strHtml.Append("</td>");
                strHtml.Append("<td valign='top' width='60%'>");
                strHtml.AppendFormat("<a target='_blank' href='{0}/SupplierInfo/ArticleInfo.aspx?id={1}'><strong>{2}</strong><br></a>", Domain.UserPublicCenter, list[0].ID, Utils.GetText2(list[0].ArticleTitle, 9, false));
                strHtml.AppendFormat("<div style='margin-top: 5px; color: #585858; line-height: 120%'>{0} <span class='lanse'><a target='_blank' href='{1}/SupplierInfo/ArticleInfo.aspx?id={2}'>[详细]</a></span></div>", Utils.GetText2(list[0].ArticleText, 35, true), Domain.UserPublicCenter, list[0].ID);
                strHtml.Append("</td></tr></table></td></tr></table>");
            }
            list = null;
            var obj = new { html = strHtml.ToString() };
            context.Response.Write(";" + callback + "(" + Newtonsoft.Json.JsonConvert.SerializeObject(obj) + ")");
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
