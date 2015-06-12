using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 平台注册头部控件
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class RegisterHead : System.Web.UI.UserControl
    {
        protected string ImageServerPath = "";
        protected string UnionLogo = "";
        protected string DefaultUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            DefaultUrl = EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(page.CityModel);
            if (!Page.IsPostBack)
            {
                GetUnionLogo();
            } 
        }

        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionLogo =EyouSoft.Common.Domain.FileSystem+ Model.UnionLog;
            }
            else
            {
                UnionLogo = ImageServerPath + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;

        }
    }
}