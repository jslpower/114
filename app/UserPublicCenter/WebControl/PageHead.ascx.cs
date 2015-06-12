using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 平台头部控件
    /// </summary>
    public partial class PageHead : System.Web.UI.UserControl
    {
        protected string strMessage = "";
        protected string UrlByProposal = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("head2011"));
            EyouSoft.Common.Utils.AddJavaScriptInclude(EyouSoft.Common.JsManage.GetJsFilePath("jquery1.4"), false, false);
            if (!Page.IsPostBack)
            {  
                
                GetHeadMessage();
            }
        }
        /// <summary>
        /// 根据登录状态获的头部显示信息
        /// </summary>
        protected void GetHeadMessage()
        {
            string URL = Request.ServerVariables["SCRIPT_NAME"];
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                URL += "?" + Request.QueryString;
            }
         

            FrontPage page = this.Page as FrontPage;

            //CityId = page.CityId;

            UrlByProposal=EyouSoft.Common.Utils.GetUrlByProposal(3, page.CityId);

            //已经登录
            if (page.IsLogin)
            {
                string strSupplyManage = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/Default.aspx";
                string userManageUrl = null;//用户后台URL
                //进入后台管理的链接

                if (page.IsAirTicketSupplyUser)//如果是机票供应商用户
                {
                    userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/TicketsCenter/";
                }
                else if (page.IsTemporaryUser)//如果是随便逛逛用户
                {
                    userManageUrl = EyouSoft.Common.Domain.UserClub + "/usercp.aspx";
                }
                else//如果是旅行社用户
                {
                    userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/";
                }

                strMessage = string.Format("欢迎您！<a href='{0}' >{1}</a>&nbsp;&nbsp;<a href='{2}'>退出</a> ", userManageUrl, page.SiteUserInfo.UserName, EyouSoft.Common.Utils.GetLogoutUrl(Server.UrlEncode(URL)));
            }
            else//没有登录
            {
                string strUrl = Request.ServerVariables["SCRIPT_NAME"];
                if (!string.IsNullOrEmpty( Request.QueryString.ToString() ))
                {
                    strUrl += "?" + Request.QueryString;
                }
                else {
                    strUrl += "?CityId=" + page.CityId;
                }
                strMessage = string.Format("您好，欢迎来同业114！<a href=\"{1}/Register/Login.aspx?returnurl={0}\">请登录</a> <a href=\"{1}/Register/CompanyUserRegister.aspx\" class=\"ff0000\">免费注册</a>", Server.UrlEncode(strUrl),Domain.UserPublicCenter);
            }
        }
    }
}