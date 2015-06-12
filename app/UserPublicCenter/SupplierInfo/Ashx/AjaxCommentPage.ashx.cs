using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
namespace UserPublicCenter.SupplierInfo.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxCommentPage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            EyouSoft.SSOComponent.Entity.UserInfo SiteUserInfo = null;
            bool IsLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out SiteUserInfo);
            context.Response.ContentType = "text/plain";
            string ImageServerURL = ImageManage.GetImagerServerUrl(1);
            #region 参数生成
            int pageIndex = 1;
            int pageSize = 10;
            int recordCount = 0;
            int ItemIndex = 1;
            int ChildItemIndex = 1;
            string TopicId = string.Empty;
            int TopicType = 1; //回复类型 1：供求 2：嘉宾访谈
            int CurrCityId = 0; //当前销售城市
            StringBuilder StrCommHTML = new StringBuilder();
            if (context.Request.QueryString["pageIndex"] != null && StringValidate.IsInteger(context.Request.QueryString["pageIndex"].ToString()))
            {
                pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());
            }
            if (context.Request.QueryString["pageSize"] != null && StringValidate.IsInteger(context.Request.QueryString["pageSize"].ToString()))
            {
                pageSize = int.Parse(context.Request.QueryString["pageSize"].ToString());
            }
            if (context.Request.QueryString["TopicType"] != null && StringValidate.IsInteger(context.Request.QueryString["TopicType"].ToString()))
            {
                TopicType = int.Parse(context.Request.QueryString["TopicType"].ToString());
            }
            if (context.Request.QueryString["TopicId"] != null)
            {
                TopicId = StringValidate.SafeRequest(context.Request.QueryString["TopicId"].ToString());
            }
            if (context.Request.QueryString["cityid"] != null)
            {
                int.TryParse(context.Request.QueryString["cityid"].ToString(), out CurrCityId);
            }
            #endregion

            #region 生成回复列表
            EyouSoft.Model.CompanyStructure.CompanyType? UnitType =null;
            string GoToUrl=string.Empty;
            EyouSoft.IBLL.CommunityStructure.IExchangeComment ICommentBll = EyouSoft.BLL.CommunityStructure.ExchangeComment.CreateInstance();
            IList<EyouSoft.Model.CommunityStructure.ExchangeComment> list = new List<EyouSoft.Model.CommunityStructure.ExchangeComment>();
            if (TopicType == 1)
            {
                list = ICommentBll.GetSupplyComment(pageSize, pageIndex, ref recordCount, TopicId);
            }
            else
            {
                list = ICommentBll.GetGuestInterview(pageSize, pageIndex, ref recordCount, TopicId);
            }
            StrCommHTML.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border:1px solid #ccc;\">");
            StrCommHTML.Append("<tr>");
            StrCommHTML.AppendFormat("<td style=\"background:url({0}/images/seniorshop/gqplhang.gif) repeat-x; height:29px; font-size:14px; padding-left:10px; text-align:left;\">", ImageServerURL);
            StrCommHTML.AppendFormat("<a name=\"d1\"> </a><strong>网友评论| 共{0}条</strong></td>", recordCount);
            StrCommHTML.Append("</tr><tr><td>");
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeComment parent in list)
                {

                    UnitType = Utils.GetCompanyType(parent.CompanyId);
                    GoToUrl = UnitType.HasValue ? Utils.GetCompanyDomain(parent.CompanyId, UnitType.Value) : "javascript:void(0);";
                    UnitType = null;
                    StrCommHTML.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-bottom:1px dashed #d5d5d5; margin-top:5px;\">");
                    StrCommHTML.Append("<tr>");
                    StrCommHTML.Append("<td width=\"78%\" align=\"left\">");
                    StrCommHTML.AppendFormat("<em style=\"color:#009900; font-size:18px; font-weight:bold;\">{0}</em>楼 ", (pageIndex - 1) * pageSize + ItemIndex);
                    StrCommHTML.AppendFormat("<span class=\"gqlvse\"><strong><a {2} href=\"{0}\" class=\"font12_grean\">{1}</a></strong></span>", GoToUrl, parent.IsAnonymous ? "" : parent.CompanyName,UnitType.HasValue?"target=\"_blank\"":"");
                    StrCommHTML.AppendFormat("<label class=\"gqlvse\"><strong> {0} </strong></label> {1}说： ", parent.IsAnonymous ? "网友" : parent.OperatorName, EyouSoft.Common.Utils.GetMQ(parent.OperatorMQ));
                    StrCommHTML.Append("</td>");
                    //StrCommHTML.AppendFormat("<td width=\"22%\" align=\"right\">{0} {1}</td>", parent.IssueTime.ToString("yyyy-MM-dd HH:mm"), IsLogin ? string.Format("|<a href=\"javascript:void(0);\" onclick=\"gotoComment('{0}',{1});return false\">回复</a> ", parent.ID, (pageIndex - 1) * pageSize + ItemIndex) : "");
                    StrCommHTML.AppendFormat("<td width=\"22%\" align=\"right\">{0} {1}</td>", parent.IssueTime.ToString("yyyy-MM-dd HH:mm"), string.Format("|<a href=\"javascript:void(0);\" onclick=\"gotoComment('{0}',{1});return false\">回复</a> ", parent.ID, (pageIndex - 1) * pageSize + ItemIndex));
                    StrCommHTML.Append("</tr>");
                    StrCommHTML.Append("<tr>");
                    if (parent.IsHasNextLevel)
                    {
                        StrCommHTML.Append("<td colspan=\"2\" align=\"left\" class=\"detaillou\">");
                        IList<EyouSoft.Model.CommunityStructure.ExchangeComment> childList = ICommentBll.GetCommentByCommentId(parent.ID);
                        if (childList != null && childList.Count > 0)
                        {
                            for (int i = 0; i < childList.Count + 1; i++)
                            {
                                StrCommHTML.Append("<div>");
                            }
                            StrCommHTML.AppendFormat("<p><span>{0}</span></p>{1}</div>", ChildItemIndex, StringValidate.TextToHtml(parent.CommentText));
                            ChildItemIndex += 1;
                            foreach (EyouSoft.Model.CommunityStructure.ExchangeComment child in childList)
                            {
                                UnitType = Utils.GetCompanyType(parent.CompanyId);
                                GoToUrl = UnitType.HasValue ? Utils.GetCompanyDomain(parent.CompanyId, UnitType.Value) : "javascript:void(0);";
                                UnitType = null;
                                StrCommHTML.AppendFormat("<p><a  {5} href=\"{0}\">{1}</a>{2}<span>{3}</span></p>{4}</div>"
                                   , GoToUrl, child.IsAnonymous ? "" : child.CompanyName, child.IsAnonymous ? "网友" : child.OperatorName, ChildItemIndex,
                                   StringValidate.TextToHtml(child.CommentText),UnitType.HasValue?"target=\"_blank\"":"");
                                ChildItemIndex += 1;
                            }
                        }
                    }
                    else
                    {
                        StrCommHTML.AppendFormat("<td colspan=\"2\" align=\"left\" style=\"padding:10px; font-size:14px;\">{0}</td>",StringValidate.TextToHtml(parent.CommentText));
                    }
                    StrCommHTML.Append("</td></tr></table>");
                    ItemIndex += 1;
                    ChildItemIndex = 1;
                }
            }
            else
            {
                StrCommHTML.Append("暂无回复！");
            }
            StrCommHTML.Append("</td></tr></table>");
            StrCommHTML.AppendFormat("<input type=\"hidden\" id=\"hIndex\" value=\"{0}\">", pageIndex);
            StrCommHTML.AppendFormat("<input type=\"hidden\" id=\"hSize\" value=\"{0}\">", pageSize);
            StrCommHTML.AppendFormat("<input type=\"hidden\" id=\"hRecordCount\" value=\"{0}\">", recordCount);
            #endregion

            context.Response.Write(StrCommHTML.ToString());
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
