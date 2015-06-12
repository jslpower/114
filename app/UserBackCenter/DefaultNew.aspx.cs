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
    /// 新用户后台首页
    /// 2010-06-30
    /// </summary>
    public partial class DefaultNew : EyouSoft.Common.Control.BasePage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/DefaultNew.aspx");
            }
            else
            {
                if (!IsCompanyCheck)//当前公司未通过运营后台审核
                {
                    //显示 【未审核】 提示信息
                    this.tbIsCheck.Visible = true;
                }
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                    //this.userbackdefault.UserInfoModel = SiteUserInfo;
                    this.ltrCompanyName.Text = UserInfoModel.CompanyName;
                }
                else
                {
                    this.ltrCompanyName.Text = "杭州易诺科技公司";
                }
            }
        }

    }
}
