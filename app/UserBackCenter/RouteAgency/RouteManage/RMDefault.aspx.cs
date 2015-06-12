using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 线路库首页
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-15
    public partial class RMDefault : BackPage
    {
        /// <summary>
        /// 显示国内，显示国际，显示周边
        /// </summary>
        protected bool showGN = false, showGJ = false, showZB = false;
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource routeSource;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 权限判断
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }

            if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.专线商添加)
            {
                if (!CheckGrant(TravelPermission.专线_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.地接社添加)
            {
                if (!CheckGrant(TravelPermission.地接_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            #endregion
            BindLineArea();
        }
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void BindLineArea()
        {
            //用户Id
            string UserID = string.Empty; ;
            if (SiteUserInfo != null)
            {
                EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = SiteUserInfo;

                UserID = UserInfoModel.ID ?? "0";
            }
            ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(UserID);
            if (companyUserModel != null && companyUserModel.Area != null && companyUserModel.Area.Count > 0)
            {
                //国内
                List<AreaBase> gn = companyUserModel.Area.Where(ty => ty.RouteType == AreaType.国内长线).ToList();
                //国际
                List<AreaBase> gj = companyUserModel.Area.Where(ty => ty.RouteType == AreaType.国际线).ToList();
                //周边
                List<AreaBase> zb = companyUserModel.Area.Where(ty => ty.RouteType == AreaType.地接线路 || ty.RouteType == AreaType.国内短线).ToList();

                if (gn != null && gn.Count > 0)
                {
                    showGN = true;
                    rpt_gn.DataSource = gn;
                    rpt_gn.DataBind();
                }
                if (gj != null && gj.Count > 0)
                {
                    showGJ = true;
                    rpt_gj.DataSource = gj;
                    rpt_gj.DataBind();
                }
                if (zb != null && zb.Count > 0)
                {
                    showZB = true;
                    rpt_zb.DataSource = zb;
                    rpt_zb.DataBind();
                }
                routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1);
            }
        }
    }
}
