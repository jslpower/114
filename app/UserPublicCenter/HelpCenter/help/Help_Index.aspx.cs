using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;

namespace UserPublicCenter.HelpCenter.help
{
    public partial class Help_Index : EyouSoft.Common.Control.FrontPage
    {
        protected string UnionLogo = "";
        protected int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FrontPage page = this.Page as FrontPage;
                CityId = page.CityId;
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
                UnionLogo = EyouSoft.Common.Domain.FileSystem + Model.UnionLog;
            }
            else
            {
                UnionLogo = EyouSoft.Common.Domain.ServerComponents + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;

        }
    }
}
