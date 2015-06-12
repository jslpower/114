using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common;

namespace IMFrame.TourAgency.TourOrder
{
    /// <summary>
    /// 页面功能:订单合计汇总页面
    /// </summary>
    public partial class Default : EyouSoft.ControlCommon.Control.MQPage
    {
        public string UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.SSOComponent.Entity.UserInfo operatorModel = this.SiteUserInfo;
            if (!this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Response.Clear();
                Response.Write("对不起，不属于组团商，没有权限操作该页面！");
                Response.End();
            }
            if (CheckGrant(TravelPermission.组团_线路散客订单管理) || CheckGrant(TravelPermission.组团_线路团队订单管理))
                div_ZTOrder.Visible = true;
            if (operatorModel != null)
            {
                CityId = operatorModel.CityId;
                IMTop1.MQLoginId = this.MQLoginId;
                IMTop1.Password = this.Password;
                a_AddSonAccount.HRef = "javascript:void(0)";
                a_SonAccountManage.HRef = "javascript:void(0)";
                //子帐号设置的权限,有权限才可以看到
                if (CheckGrant(TravelPermission.系统设置_子账户管理))
                {
                    this.divUserManage.Visible = true;
                    this.SubAccount1.IsShow = true;
                    this.SubAccount1.CompanyId = operatorModel.CompanyID;
                    this.SubAccount1.OperatorId = operatorModel.ID;
                    this.SubAccount1.FunStringOnChange = "ChangeUserData(this)";

                    a_AddSonAccount.Target = "_blank";
                    a_SonAccountManage.Target = "_blank";
                    a_AddSonAccount.HRef = this.GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + "/SystemSet/SonUserManage.aspx");
                    a_SonAccountManage.HRef = this.GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + "/SystemSet/SonUserManage.aspx");

                }
                else
                {
                    UserId = operatorModel.ID;
                    this.SubAccount1.IsShow = false;
                }
            }
        }
    }
}
