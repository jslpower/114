using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using EyouSoft.ControlCommon.Control;
using EyouSoft.Common;

namespace IMFrame.WEB.IM.RouteAgency
{
    /// <summary>
    /// MQ  线路页面
    /// </summary>
    /// 修改人:陈志仁   修改时间:2010-07-12 修改内容:企业MQ修改
    /// 修改人:戴银柱   修改时间:2010-08-14 修改内容:底层修改
    public partial class Main : MQPage
    {

        protected string OperatorId = "";
        protected string isShow = "none";
        private string CompanyId = "";

        protected int MQId = 0;
        protected string MD5Password = "";
        protected string parsUserName = "";
        protected string parsMD5Password = "";
        public StringBuilder strUserManageHTML = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {

            int MQVIPLevelID = 0;
            if (!IsPostBack)
            {
                if (SiteUserInfo != null)
                {
                    if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {

                    }
                    else
                    {
                        //如果是组团则直接跳转到组团页面
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            Response.Redirect("/TourAgency/TourManger/TourAreaList.aspx");
                        }
                        else
                            //如果是地接则跳转至地接的页面
                            if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                            {
                                Response.Redirect("/LocalAgency/Main.aspx");
                            }
                            else
                            {
                                //如果是供应商用户则跳转
                                Response.Redirect("/Business/Tongye114.html");
                            }

                    }
                    //对用户控件赋值
                    this.IMTop1.MQLoginId = Utils.GetInt(SiteUserInfo.ContactInfo.MQ);
                    this.IMTop1.Password = SiteUserInfo.PassWordInfo.MD5Password;
                    //当前user Id
                    OperatorId = SiteUserInfo.ID;
                    //当前用户的公司ID
                    CompanyId = SiteUserInfo.CompanyID;
                }
                //判断是否有子账户管理权限
                if (CheckGrant(EyouSoft.Common.TravelPermission.系统设置_子账户管理))
                {
                    this.SubAccount1.IsShow = true;
                    this.SubAccount1.CompanyId = CompanyId;

                    strUserManageHTML.AppendFormat("<a href='{0}' target='_blank'>子账号管理</a>", GetDesPlatformUrl(Domain.UserBackCenter + "/SystemSet/SonUserManage.aspx"));
                    strUserManageHTML.Append("&nbsp;");
                    strUserManageHTML.AppendFormat("<a href='{0}' target='_blank'>添加子账号</a>", GetDesPlatformUrl(Domain.UserBackCenter + "/SystemSet/SonUserManage.aspx"));
                }
                else
                {
                    this.SubAccount1.IsShow = false;
                }
                if (!CheckGrant(TravelPermission.专线_线路散客订单管理) && !CheckGrant(TravelPermission.专线_线路团队订单管理))
                {
                    plnOrder.Visible = false;
                }
            }
        }
    }
}

