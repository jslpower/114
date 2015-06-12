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
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace SeniorOnlineShop.usercontrol
{
    public partial class ShopHead : System.Web.UI.UserControl
    {
        protected string strMessage = string.Empty;
        protected string tongyeurl = Domain.UserPublicCenter + "/Default.aspx";
        protected string idealurl = "#";
        protected string helpurl = Utils.HelpCenterUrl;
        protected string addtongurl = Domain.UserPublicCenter + "/Register/CompanyUserRegister.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetHeadMessage();
            }
        }
        protected void GetHeadMessage()
        {
            FrontPage basepage = this.Page as FrontPage;
            idealurl = Utils.GetUrlByProposal(1, basepage.CityId);
            //未进行用户身份验证
            if (basepage.IsLogin)
            {
                string currentUrl = HttpContext.Current.Request.Url.ToString();
                string gooutUrl = "";
                if (currentUrl.IndexOf("seniorshop") > 0)
                {
                    gooutUrl = EyouSoft.Common.Utils.GetLogoutUrl(Domain.SeniorOnlineShop + "/seniorshop/Default.aspx");
                }
                else
                {
                    gooutUrl = EyouSoft.Common.Utils.GetLogoutUrl(Domain.SeniorOnlineShop + "/shop/ShopDefault.aspx");
                }
                if (basepage.IsLogin)
                {
                    string strSupplyManage = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/Default.aspx";
                    string userManageUrl = null;//用户后台URL
                    //进入后台管理的链接
                    if (basepage.IsAirTicketSupplyUser)//如果是机票供应商用户
                    {
                        userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/TicketsCenter/";
                    }
                    else if (basepage.IsTemporaryUser)//如果是随便逛逛用户
                    {
                        userManageUrl = EyouSoft.Common.Domain.UserClub + "/usercp.aspx";
                    }
                    else//如果是旅行社用户
                    {
                        userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/";
                    }
                    strMessage = "<li class=\"daleft\">欢迎您，&nbsp;<a target=\"_blank\" href=\"" + userManageUrl + "\" >" + basepage.SiteUserInfo.UserName + "</a>&nbsp;&nbsp;<a href='" + gooutUrl + "'>退出</a> </li>";
                }
            }
            else
            {
                strMessage = "<li class=\"daleft\"><a href=\"" + Domain.UserPublicCenter + "/Register/Login.aspx?returnurl=" + HttpContext.Current.Request.Url.ToString() + "\">请登录</a>&nbsp;&nbsp;<a target=\"_blank\" href=\"" + Domain.UserPublicCenter + "/Register/CompanyUserRegister.aspx\" class=\"ff0000\">免费注册</a></li>";
            }
        }
    }
}