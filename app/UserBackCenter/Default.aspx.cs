using System;
using System.Collections;
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
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter
{
    /// <summary>
    /// 用户后台首页
    /// 罗丽娥   2010-06-30
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.BasePage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected string FirstMenu = string.Empty;
        protected bool IsRouteAgency = false;//是否是专线
        protected bool IsTourAgency = false;//是否是组团
        protected bool IsLocalAgency = false;//是否是地接
        protected bool IsSightAgency = false;  //是否景区
        protected bool IsOther = false;
        protected bool IsOpenHighShop = false;//是否开通了高级网店

        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断用户是否登录，如果没有登录跳转到登录页面，如果有登录，初始化用户对象UserInfoModel
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (!IsCompanyCheck)//当前公司未通过运营后台审核
                {
                    //显示 【未审核】 提示信息
                    this.tbIsCheck.Visible = true;

                    //公司没有通过审核，隐藏我的景区网店
                    hrefSightShop.Visible = false;
                }
                if (SiteUserInfo != null)
                {
                    this.userbackdefault.UserInfoModel = SiteUserInfo;
                    UserInfoModel = SiteUserInfo;
                }
            }

            if (!Page.IsPostBack)
            {
                bool myShopFlag = this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区);
                //判断是否显示[我的网店]
                this.divMyShop.Visible = myShopFlag;
                if (myShopFlag)
                {
                    IsOpenHighShop = Utils.IsOpenHighShop(UserInfoModel.CompanyID);//当前公司是否开通了高级网店
                    if (IsOpenHighShop)
                    {
                        this.simpleShop.Visible = false;//普通网店 
                        this.heightShop.Visible = true;//高级网店
                        this.applyShop.Visible = false;//申请高级网店
                    }
                    else
                    {
                        this.simpleShop.Visible = true;
                        this.heightShop.Visible = false;
                        this.applyShop.Visible = true;
                    }

                    //景区网店链接设置
                    if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                    {
                        this.simpleShop.HRef = "/generalshop/sightshop/default.aspx"; //普通网店 
                        this.heightShop.HRef = "/eshop/sightshop/sightshopdefault.aspx";//高级网店
                    }
                }
                else
                {
                    this.simpleShop.Visible = false;
                    this.heightShop.Visible = false;
                    this.applyShop.Visible = false;
                }
                //判断是否显示[我的网店-同业资讯管理]  组团不能发布同业资讯
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接) || this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    this.tongYeInfo.Visible = true;
                }
                else
                {
                    this.tongYeInfo.Visible = false;
                }

                //判断是否显示[申请易游通]和[易游通管理]
                //旅业先锋.楼 2012-02-03  9:45:16 易游通管理就是链接到 http://www.gocn.cn/
                bool isOpenEyou = true;//是否开通了易游通（底层还未实现此判断）1033(郑付杰) 2012-02-03 09:56:51 方法有问题，你们同意整理合到时候再发我吧。
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团) && isOpenEyou)
                {
                    this.eyouManager.Visible = true;
                }
                InitBasicInfo();
            }

            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("change", StringComparison.OrdinalIgnoreCase))
                {
                    string type = Utils.InputText(Request.QueryString["type"]);
                    Response.Clear();
                    Response.Write(ChangeFirstMenu(type));
                    Response.End();
                }
            }
            a_GQXX.Visible = CheckGrant(TravelPermission.营销工具_供求信息);
        }

        #region 初始化基础信息
        /// <summary>
        /// 初始化基础信息
        /// </summary>
        private void InitBasicInfo()
        {
            if (UserInfoModel != null)
            {
                this.ltrCompanyName.Text = UserInfoModel.CompanyName;
            }
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo CompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfoModel = CompanyInfo.GetModel(UserInfoModel.CompanyID);
            if (CompanyInfoModel != null)
            {
                if (!String.IsNullOrEmpty(CompanyInfoModel.AttachInfo.CompanyLogo.ImagePath))
                    this.imgLogo.Src = Domain.FileSystem + CompanyInfoModel.AttachInfo.CompanyLogo.ImagePath;
                else
                    this.imgLogo.Src = ImageServerPath + "/images/logo.gif";
            }

            EyouSoft.IBLL.CompanyStructure.ICompanySetting CSetBll = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanySetting CSetModel = CSetBll.GetModel(UserInfoModel.CompanyID);
            if (CSetModel != null)
            {
                switch (CSetModel.FirstMenu)
                {
                    case EyouSoft.Model.CompanyStructure.MenuSection.专线服务:
                        FirstMenu = "0";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.组团服务:
                        FirstMenu = "1";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.地接服务:
                        FirstMenu = "2";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.景区服务:
                        FirstMenu = "3";
                        break;
                    default:
                        FirstMenu = "1";
                        break;
                }
            }

            #region 专线和组团显示控制
            EyouSoft.Model.CompanyStructure.CompanyRole RoleModel = UserInfoModel.CompanyRole;
            EyouSoft.Model.CompanyStructure.CompanyType[] enumType = RoleModel.RoleItems;

            if (enumType.Length > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyType type in enumType)
                {
                    if (type == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                    {
                        IsRouteAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.组团
                        || type == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
                    {
                        IsTourAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                    {
                        IsLocalAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {
                        IsSightAgency = true;
                    }
                    else
                    {
                        IsOther = true;
                    }
                }
                if (IsRouteAgency)   // 仅为专线
                {
                    this.div_RouteAgency.Visible = true;
                    //景区门票后台搜索页
                    divSceniceList.Visible = true;
                }
                if (IsTourAgency)     // 仅为组团
                {
                    this.div_TourAgency.Visible = true;
                    //景区门票后台搜索页
                    divSceniceList.Visible = true;
                }
                if (IsLocalAgency)
                {   // 地接
                    this.div_LocalAgency.Visible = true;
                    //景区门票后台搜索页
                    divSceniceList.Visible = true;
                }
                if (!IsRouteAgency && !IsTourAgency && IsLocalAgency)
                {
                    //景区门票后台搜索页
                    divSceniceList.Visible = true;
                }
                if (IsSightAgency)
                {
                    div_SightAgency.Visible = true;
                    divHotelCenter.Visible = false;
                    //景区门票后台搜索页
                    divSceniceList.Visible = true;
                    //我的网店链接地址 
                    hrefSightShop.HRef = IsOpenHighShop
                                             ? "/EShop/SightShop/SightShopDefault.aspx"
                                             : "/GeneralShop/SightShop/Default.aspx";
                }

                #region ZhouWenChao 2011-03-07  注释，原因是酒店用户进入不了后台

                //if (IsOther)
                //{
                //    Utils.ShowError("对不起，您不是旅行社账户!", "");
                //    return;
                //}

                #endregion
            }
            #endregion
        }
        #endregion

        private bool ChangeFirstMenu(string type)
        {
            EyouSoft.IBLL.CompanyStructure.ICompanySetting bll = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance();
            EyouSoft.Model.CompanyStructure.MenuSection menu = EyouSoft.Model.CompanyStructure.MenuSection.专线服务;

            switch (type)
            {
                case "0":
                    menu = EyouSoft.Model.CompanyStructure.MenuSection.专线服务;
                    break;
                case "1":
                    menu = EyouSoft.Model.CompanyStructure.MenuSection.组团服务;
                    break;
                case "2":
                    menu = EyouSoft.Model.CompanyStructure.MenuSection.地接服务;
                    break;
                case "3":
                    menu = EyouSoft.Model.CompanyStructure.MenuSection.景区服务;
                    break;
                default:
                    menu = EyouSoft.Model.CompanyStructure.MenuSection.专线服务;
                    break;
            }

            return bll.UpdateFirstMenu(UserInfoModel.CompanyID, menu);
        }
    }
}
