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
using EyouSoft.Common;

namespace IMFrame.LocalAgency
{
    /// <summary>
    /// 页面功能:地接专线主页面
    /// 修改时间:2010-08-14 修改人:袁惠
    /// </summary>
    public partial class Main : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string RouteListUrl = string.Empty;//我的线路库
        protected string RouteDefaultUrl = string.Empty;//进入后台
        /// <summary>
        /// 标准发布线路 
        /// </summary>
        protected string RouteAddUrl = string.Empty;
        protected bool RoleAdd = false;
        protected bool RoleEdit = false;
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                EyouSoft.Model.CompanyStructure.CompanyType? UnitType = null;
                foreach (EyouSoft.Model.CompanyStructure.CompanyType item in this.SiteUserInfo.CompanyRole.RoleItems)
	            {
                    if (item == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                    {
                        UnitType = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    }
	            }
                if (UnitType == null)
                {
                    Response.Clear();
                    Response.Write("您不是地接社用户不能访问！");
                    Response.End();
                    return;
                }
                //地接发布线路 2012-02-22 修改 方琪
                RouteAddUrl = Domain.UserBackCenter + "/routeagency/routemanage/rmdefault.aspx?RouteSource=2";
                //用户后台  2012-02-22 修改 方琪
                RouteDefaultUrl = Domain.UserBackCenter + "/Default.aspx";
                //地接线路库  2012-02-22 修改 方琪
                RouteListUrl = Domain.UserBackCenter + "/routeagency/routemanage/routeview.aspx?RouteSource=2";
                
                if (this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
                {
                    RoleAdd = true;
                }
                if (this.CheckGrant(EyouSoft.Common.TravelPermission.地接_线路管理))
                {
                    RoleEdit = true;
                }
            }
        }
    }
}
