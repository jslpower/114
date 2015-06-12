using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.Register
{
    /// <summary>
    /// 注册成功跳转页
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class RegisterSuccess : EyouSoft.Common.Control.FrontPage
    {
        //protected string a_AddTourList = EyouSoft.Common.Domain.UserBackCenter + "/RouteManage/Default.aspx"; //专线
        //protected string SupplyUrl = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/Default.aspx"; // 供应商
        protected string domainName = EyouSoft.Common.Domain.UserPublicCenter;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (IsLogin)
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                    if (Model != null)
                    {
                        //EyouSoft.Model.CompanyStructure.CompanyType[] CompanyTypeItems = Model.CompanyRole.RoleItems;
                        //判断当前用户是否是专线用户
                        bool isRouteAgency = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                        //if (CompanyTypeItems != null && CompanyTypeItems.Length > 0)
                        //{
                        //    for (int i = 0; i < CompanyTypeItems.Length; i++)
                        //    {
                        //        if (CompanyTypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                        //        {
                        //            isRouteAgency = true;
                        //            break;
                        //        }
                        //    }
                        //}
                        //获的当前注册用户信息
                        //labRegisterMessage.Text = "欢迎您! " + SiteUserInfo.UserName;

                        string userManageUrl = null;//用户后台URL                         
                        if (Model.CompanyRole.Length > 0 && Model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛))
                        {
                            userManageUrl = EyouSoft.Common.Domain.UserClub + "/usercp.aspx";
                            imgBack.Src = ImageServerPath + "/images/UserPublicCenter/jionicon/btn_03.gif";
                            this.hfPerfect.Visible = false;
                        }
                        else
                        {
                            imgBack.Src=ImageServerPath+"/images/UserPublicCenter/jionicon/btn_01.gif";
                            //进入后台管理的链接
                            //if (IsSupplyUser)//如果是供应商用户【酒店，景区，车队，购物店，旅游用品】
                            //{
                            //    userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/Default.aspx";
                            //}
                            if (IsAirTicketSupplyUser)//如果是机票供应商用户
                            {
                                userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/TicketsCenter/";
                            }
                            else//如果是旅行社用户
                            {
                                userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/";
                            }
                            this.hfPerfect.HRef = "PerfectInfo.aspx";
                        }
                        this.a_GoBackCenter.HRef = userManageUrl;

                        //如果是专线用户，则显示 发布产品的链接
                        //if (isRouteAgency)//是专线用户
                        //{
                        //    //发布产品的链接
                        //    //this.a_AddTourList.HRef
                        //        a_AddTourList= EyouSoft.Common.Domain.UserBackCenter + "/Default.aspx#page%3A%2Frouteagency%2Faddquicktour.aspx";
                        //}
                        //else//不是
                        //{
                        //    //this.a_AddTourList.Visible = false;
                        //}

                        //发布供求信息的链接
                        
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}
