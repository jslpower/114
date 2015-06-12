using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using EyouSoft.Common;
namespace UserPublicCenter.SupplierInfo.UserControl
{
    public partial class CommonTopicControl : System.Web.UI.UserControl
    {
        #region 列表模板
        // 文字描述表格模板
        private const string SoftManHtmlFormat = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"{0}><tr><td class=\"{1}\"><strong>{5}{2}</strong></td></tr><tr><td{3}>{4}</td></tr></table>";
        private const string ScriptHtmlFormat = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border:1px solid #ccc;\"><tr><td class=\"{0}\" >&nbsp;&nbsp;<strong>{1}</strong></td></tr><tr><td style=\"padding:5px; text-align:left; font-size:14px; line-height:24px;\" {2}>{3}</td></tr></table>";
        private const string PicListHtmlFormat = "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"maintop10\"style=\"border:1px solid #ccc;\"><tr><td class=\"{0}\" ><strong>{1}</strong>{2}</td></tr><tr><td{3}>{4}</td></tr></table>";
        #endregion
        private string ImageServerUrl = ImageManage.GetImagerServerUrl(1);
        private string FileServerUrl = Domain.FileSystem;
        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (TopicType)
            {
                case TopicTypes.软文广告列表:
                    BindSoftManList();
                    break;
                case TopicTypes.图文广告列表:
                    BindPicList();
                    break;
                case TopicTypes.文字描述:
                    EyouSoft.IBLL.CommunityStructure.ISiteTopic ISiteBll = EyouSoft.BLL.CommunityStructure.SiteTopic.CreateInstance();
                    switch (PartType)
                    {
                        case PartTypes.访谈介绍:
                            mainDiv.Controls.Add(new LiteralControl(string.Format(ScriptHtmlFormat, PartCss, PartText, TextCss.Length > 0 ? " class=" + TextCss : "", ISiteBll.GetInterview())));
                            break;
                        case PartTypes.同业交流专区:
                            mainDiv.Controls.Add(new LiteralControl(string.Format(ScriptHtmlFormat, PartCss, PartText, TextCss.Length > 0 ? " class=" + TextCss : "", ISiteBll.GetCommArea())));
                            break;
                        case PartTypes.学堂介绍:
                            mainDiv.Controls.Add(new LiteralControl(string.Format(ScriptHtmlFormat, PartCss, PartText, TextCss.Length > 0 ? " class=" + TextCss : "", ISiteBll.GetSchool())));
                            break;
                    }
                    ISiteBll = null;
                    break;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 资讯大类别
        /// </summary>
        public EyouSoft.Model.CommunityStructure.TopicClass? TopicClass
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯小类别
        /// </summary>
        public EyouSoft.Model.CommunityStructure.TopicAreas? TopicArea
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯类型
        /// </summary>
        public TopicTypes TopicType
        {
            get;
            set;
        }
        /// <summary>
        /// 资讯类型集合
        /// </summary>
        public enum TopicTypes
        {
            /// <summary>
            /// 软文广告列表
            /// </summary>
            软文广告列表 = 1,
            /// <summary>
            /// 图文广告列表
            /// </summary>
            图文广告列表,
            /// <summary>
            /// 文字描述
            /// </summary>
            文字描述
        }
        /// <summary>
        /// 需要返回的记录数
        /// </summary>
        public int TopNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目文字说明
        /// </summary>
        public string PartText
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目样式
        /// </summary>
        public string PartCss
        {
            get;
            set;
        }
        /// <summary>
        /// 文字样式
        /// </summary>
        public string TextCss
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目类型
        /// </summary>
        public PartTypes PartType
        {
            get;
            set;
        }
        /// <summary>
        /// 当前销售城市编号
        /// </summary>
        public int CurrCityId
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目类型集合
        /// </summary>
        public enum PartTypes
        {
            /// <summary>
            /// 资讯
            /// </summary>
            资讯 = 1,
            /// <summary>
            /// 嘉宾访谈
            /// </summary>
            嘉宾访谈,
            /// <summary>
            /// 顾问团队
            /// </summary>
            顾问团队,
            /// <summary>
            /// 学堂介绍
            /// </summary>
            学堂介绍,
            /// <summary>
            /// 访谈介绍
            /// </summary>
            访谈介绍,
            /// <summary>
            /// 同业交流专区
            /// </summary>
            同业交流专区,
            /// <summary>
            /// 新手学堂{资讯中特殊排版}
            /// </summary>
            同业学堂,
            /// <summary>
            /// 嘉宾访谈下资讯
            /// </summary>
            嘉宾访谈下资讯,
            /// <summary>
            /// 同业之星访谈
            /// </summary>
            同业之星访谈,
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 绑定图文列表
        /// </summary>
        private void BindPicList()
        {
            StringBuilder strInfoHtml = new StringBuilder();
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = null;
            EyouSoft.IBLL.CommunityStructure.IInfoArticle InfoBll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
            switch (PartType)
            {
                #region 资讯
                case PartTypes.资讯:
                    list = InfoBll.GetTopNumPicList(TopNumber, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, null, true, null);
                    if (list != null && list.Count > 0)
                    {
                        foreach (EyouSoft.Model.CommunityStructure.InfoArticle model in list)
                        {
                            strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-bottom:1px dashed #ccc;\">");
                            strInfoHtml.Append("<tr>");
                            strInfoHtml.AppendFormat("<td width=\"48%\" height=\"108\"><img src=\"{0}\" width=\"120\" height=\"90\" /></td>", FileServerUrl + model.ImgThumb);
                            strInfoHtml.AppendFormat("<td width=\"52%\" valign=\"top\"><strong><a href=\"/SupplierInfo/{0}?Id={1}\" class=\"heise\" title=\"{3}\">{2}</a></strong><br /><span class=\"huise\">{3}</span><a href=\"/SupplierInfo/{0}?Id={1}\"><span class=\"ff0000\">[详细]</span></a></td>",
                                model.TopicClassId == EyouSoft.Model.CommunityStructure.TopicClass.行业资讯 ? "ArticleInfo.aspx" : "SchoolIntroductionInfo.aspx", model.ID, Utils.GetText(model.ArticleTitle, 7), model.ArticleTitle, Utils.GetText(model.ArticleText, 30));
                            strInfoHtml.Append("</tr>");
                            strInfoHtml.Append("</table>");
                        }
                    }
                    list = null;
                    InfoBll = null;
                    break;
                #endregion

                #region 顾问团队
                case PartTypes.顾问团队:
                    EyouSoft.IBLL.CommunityStructure.ICommunityAdvisor IAdvisorBll = EyouSoft.BLL.CommunityStructure.CommunityAdvisor.CreateInstance();
                    IList<EyouSoft.Model.CommunityStructure.CommunityAdvisor> AdvList = IAdvisorBll.GetCommunityAdvisorList(TopNumber, true);
                    if (AdvList != null && AdvList.Count > 0)
                    {
                        foreach (EyouSoft.Model.CommunityStructure.CommunityAdvisor model in AdvList)
                        {
                            strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-bottom:1px dashed #ccc; margin-top:5px; padding-bottom:2px;\">");
                            strInfoHtml.Append("<tr>");
                            strInfoHtml.AppendFormat("<td width=\"27%\"><img src=\"{0}\" width=\"59\" height=\"60\" /></td>", FileServerUrl + model.ImgPath);
                            strInfoHtml.AppendFormat("<td width=\"73%\" valign=\"top\" align=\"left\"><strong>{0}</strong><br />", model.ContactName);
                            strInfoHtml.AppendFormat("<div style=\"color:#585858;\" title=\"{1}\">{0}</div></td>", Utils.GetText(model.Achieve, 30), model.Achieve);
                            strInfoHtml.Append("</tr>");
                            strInfoHtml.Append("</table>");
                        }
                    }
                    AdvList = null;
                    IAdvisorBll = null;
                    break;
                #endregion

                #region 嘉宾访谈
                case PartTypes.嘉宾访谈:
                    EyouSoft.IBLL.CommunityStructure.IHonoredGuest IGuestBll = EyouSoft.BLL.CommunityStructure.HonoredGuest.CreateInstance();
                    IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GuestList = IGuestBll.GetTopNumList(TopNumber);
                    if (GuestList != null && GuestList.Count > 0)
                    {
                        foreach (EyouSoft.Model.CommunityStructure.HonoredGuest model in GuestList)
                        {
                            strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-bottom:1px dashed #ccc; margin-top:5px; padding-bottom:2px;\">");
                            strInfoHtml.Append("<tr>");
                            strInfoHtml.AppendFormat("<td width=\"30%\"><img src=\"{0}\" width=\"50\" height=\"50\" /></td>", FileServerUrl + model.ImgThumb);
                            strInfoHtml.AppendFormat("<td width=\"70%\" valign=\"top\" style=\"text-align:left\"><strong title=\"{2}\">{0}</strong><br /><div style=\"margin-top:2px; color:#585858;\">{1}<br />", Utils.GetText(model.Title, 13), Utils.GetText(model.Content, 13), model.Title);
                            strInfoHtml.AppendFormat("<span class=\"lanse\"><a href=\"/SupplierInfo/HonoredGuestInfo.aspx?id={0}\">[详细]</a></span></div></td>", model.ID);
                            strInfoHtml.Append("</tr>");
                            strInfoHtml.Append("</table>");
                        }
                    }
                    GuestList = null;
                    break;
                #endregion

                #region 同业之星访谈
                case PartTypes.同业之星访谈:
                    list = InfoBll.GetTopNumPicList(TopNumber, TopicClass, TopicArea, true, null);
                    if (list != null && list.Count > 0)
                    {
                        foreach (EyouSoft.Model.CommunityStructure.InfoArticle model in list)
                        {
                            strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-bottom:1px dashed #ccc; margin-top:5px; padding-bottom:2px;\">");
                            strInfoHtml.Append("<tr>");
                            strInfoHtml.AppendFormat("<td width=\"30%\"><img src=\"{0}\" width=\"50\" height=\"50\" /></td>", FileServerUrl + model.ImgThumb);
                            strInfoHtml.AppendFormat("<td width=\"70%\" valign=\"top\" style=\"text-align:left\"><strong title=\"{2}\">{0}</strong><div style=\"margin-top:2px; color:#585858;\">{1}<br />", Utils.GetText(model.ArticleTitle, 26), "", model.ArticleTitle);
                            strInfoHtml.AppendFormat("<span class=\"lanse\"><a target=\"_blank\" href=\"/SupplierInfo/ArticleInfo.aspx?id={0}\">[详细]</a></span></div></td>", model.ID);
                            strInfoHtml.Append("</tr>");
                            strInfoHtml.Append("</table>");
                        }
                    }
                    list = null;
                    break;
                #endregion
            }
            mainDiv.Controls.Add(new LiteralControl(string.Format(PicListHtmlFormat, PartCss, PartText, PartType == PartTypes.顾问团队 ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"/SupplierInfo/ApplicationTeam.aspx\" target=\"_blank\" class=\"heise12\">访谈申请</a>" : string.Empty, TextCss.Length > 0 ? " class=" + TextCss : "", strInfoHtml.ToString())));
        }
        /// <summary>
        /// 绑定软文资讯
        /// </summary>
        private void BindSoftManList()
        {
            StringBuilder strInfoHtml = new StringBuilder();
            EyouSoft.IBLL.CommunityStructure.IInfoArticle InfoBll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = InfoBll.GetTopNumList(TopNumber, TopicClass, TopicArea, null, string.Empty);
            switch (PartType)
            {
                #region 资讯
                case PartTypes.资讯:
                    if (list != null && list.Count > 0)
                    {
                        strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        strInfoHtml.Append("<tr>");
                        strInfoHtml.Append("<td class=\"xuetang\"><ul>");
                        foreach (EyouSoft.Model.CommunityStructure.InfoArticle model in list)
                        {
                            strInfoHtml.AppendFormat("<li>·<a href=\"{0}\" title=\"{2}\">{1}</a></li>", TopicClass == EyouSoft.Model.CommunityStructure.TopicClass.行业资讯 ? EyouSoft.Common.URLREWRITE.SupplierInfo.ArticleInfo(model.ID) : EyouSoft.Common.URLREWRITE.SupplierInfo.SchoolInfo(model.ID),Utils.GetText(model.ArticleTitle, 18), model.ArticleTitle);
                        }
                        strInfoHtml.Append("</ul></td></tr></table>");
                    }
                    break;
                #endregion

                #region 同业学堂
                case PartTypes.同业学堂:
                    List<EyouSoft.Model.CommunityStructure.TopicClass> TopicList = new List<EyouSoft.Model.CommunityStructure.TopicClass>();
                    TopicList.Add(EyouSoft.Model.CommunityStructure.TopicClass.案例分析);
                    TopicList.Add(EyouSoft.Model.CommunityStructure.TopicClass.导游之家);
                    TopicList.Add(EyouSoft.Model.CommunityStructure.TopicClass.计调指南);
                    TopicList.Add(EyouSoft.Model.CommunityStructure.TopicClass.经验交流);
                    list = InfoBll.GetTopNumListByTopicList(TopNumber, TopicList, null);
                    if (list != null && list.Count > 0)
                    {
                        int i = 0;
                        strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"padding-top:6px;\">");
                        strInfoHtml.Append("<tr>");
                        StringBuilder strInfo = new StringBuilder();
                        foreach (EyouSoft.Model.CommunityStructure.InfoArticle model in list)
                        {
                            i++;
                            strInfo.Append("<li>·<a href=\"" + EyouSoft.Common.URLREWRITE.SupplierInfo.SchoolInfo(model.ID) + "\" title=\"" + model.ArticleTitle + "\">" + Utils.GetText(model.ArticleTitle, 8) + "</a></li>");
                            if (i % 6 == 0 || i == list.Count)
                            {
                                strInfoHtml.AppendFormat("<td width=\"50%\" valign=\"top\" class=\"xuetang\"><ul>{0}</ul></td>", strInfo.ToString());
                                strInfo.Remove(0, strInfo.Length);
                            }
                        }
                        strInfoHtml.Append("</tr></table>");
                    }
                    break;
                #endregion

                #region 嘉宾访谈下资讯
                case PartTypes.嘉宾访谈下资讯:
                    if (list != null && list.Count > 0)
                    {
                        strInfoHtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"padding-top:6px;\">");
                        strInfoHtml.Append("<tr>");
                        strInfoHtml.Append("<td class=\"xuetang\"><ul>");
                        foreach (EyouSoft.Model.CommunityStructure.InfoArticle model in list)
                        {
                            strInfoHtml.AppendFormat("<li>·<a href=\"/SupplierInfo/ArticleInfo.aspx?Id={0}\" title=\"{2}\">{1}</a></li>", model.ID, Utils.GetText(model.ArticleTitle, 15), model.ArticleTitle);
                        }
                        strInfoHtml.Append("</ul></td></tr></table>");
                    }
                    break;
                #endregion
            }
            mainDiv.Controls.Add(new LiteralControl(string.Format(SoftManHtmlFormat, PartType == PartTypes.嘉宾访谈下资讯 ? " class=\"margin10\" style=\"border:1px solid #ccc; text-align:left\"" : "", PartCss, PartText, TextCss.Length > 0 ? " class=" + TextCss : "", strInfoHtml.ToString(),
                PartType == PartTypes.嘉宾访谈下资讯 ? "&nbsp;&nbsp;" : "")));
        }
        #endregion

    }
}