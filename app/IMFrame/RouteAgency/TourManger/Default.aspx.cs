using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;

namespace IMFrame.RouteAgency
{
    /// <summary>
    /// 页面功能:批发商的团队主页面
    /// 章已泉 2009-9-10
    /// </summary>
    public partial class Default : EyouSoft.ControlCommon.Control.MQPage
    {
        protected bool IsTourPermissions = true; //团队管理权限
        private string UserID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                divData.Visible = false;
                divNoData.Visible = true;
            }

            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理))   //添加权限
            {
                Add.Visible = false;
                Copy.Visible = false;
                Update.Visible = false;
            }
            if (this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理))  //查看所有
            {
                IsTourPermissions = true;
            }
            if (!String.IsNullOrEmpty(Utils.GetQueryStringValue("OperatorId")))
            {
                UserID = Utils.GetQueryStringValue("OperatorId");
            }
            if (!IsPostBack)
            {
                InitData();
            }
            this.Add.HRef = GetDesPlatformUrl(Domain.UserBackCenter + "/routeagency/routemanage/rmdefault.aspx?RouteSource=1");
        }

        private void InitData()
        {
            if (string.IsNullOrEmpty(UserID))
                UserID = SiteUserInfo.ID ?? "0";

            EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(UserID);
            if (companyUserModel != null)
            {
                if (companyUserModel.Area != null && companyUserModel.Area.Count > 0)
                {
                    this.rptAreaList.DataSource = companyUserModel.Area;
                    this.rptAreaList.DataBind();
                }
            }
            else
            {
                this.pnlNoData.Visible = true;
            }
        }

        protected void rptAreaList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                int intRecordCount = 0;
                //找到对应的绑定项里的对应的线路区域编号
                EyouSoft.Model.SystemStructure.AreaBase areaModel = (EyouSoft.Model.SystemStructure.AreaBase)e.Item.DataItem;
                MRouteSearch queryModel = new MRouteSearch();
                queryModel.AreaId = areaModel.AreaId;
                queryModel.RouteSource = RouteSource.专线商添加;
                queryModel.RouteType = areaModel.RouteType;
                IList<MRoute> list = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetBackCenterList(1000000, 1, ref intRecordCount, this.SiteUserInfo.CompanyID, queryModel);
                Repeater rptTourList = (Repeater)e.Item.FindControl("rptTourList");
                if (list != null && list.Count > 0)
                {
                    rptTourList.DataSource = list;
                    rptTourList.DataBind();
                }
            }
        }
    }
}
