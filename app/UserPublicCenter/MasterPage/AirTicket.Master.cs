using System;
using System.Collections;
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
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserPublicCenter.MasterPage
{
    public partial class AirTicket : System.Web.UI.MasterPage
    {
        protected int _NaviagtionIndex = 0;
        public AirTicketNavigation Naviagtion
        {
            set
            {
                _NaviagtionIndex = (int)value;
            }
        }
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EyouSoft.Common.Control.FrontPage page = this.Page as FrontPage;

            //判断用户是否登录
            if (!page.IsLogin)
            {
                Response.Redirect("/AirTickets/Login.aspx", true);
            }


            page.AddStylesheetInclude(CssManage.GetCssFilePath("body"));
            page.AddStylesheetInclude(CssManage.GetCssFilePath("jipiaostyle"));
            page.AddJavaScriptInclude(JsManage.GetJsFilePath("jquery"), false, false);

            //根据用户的身份 初始化其对应的后台系统地址
            ltrUserName.Text = page.SiteUserInfo.UserName;

            //根据当前用户所属的公司ID ，从公司信息中 获取 用户的登录次数和最后登录时间
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyInfo =
                EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(page.SiteUserInfo.CompanyID);
            if (companyInfo != null)
            {
                ltrLoginCount.Text = companyInfo.StateMore.LoginCount.ToString();
                ltrLastLoginTime.Text = companyInfo.StateMore.LastLoginTime.ToString("yyyy年MM月dd日HH点mm分");//格式2010年8月13日9点19分
            }

            //判断当前用户身份，如果其是机票供应商,则显示 进入机票供应商后台 链接。
            if (page.IsAirTicketSupplyUser)//是
            {
                spanAirGongYinLink.Visible = true;
            }
            else//不是
            {
                spanAirGongYinLink.Visible = false;
            }
        }
    }
}
