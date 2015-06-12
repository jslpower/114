using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common.Control;
namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页优秀企业展示
    /// </summary>
    public partial class ExcellentCompany : System.Web.UI.UserControl
    {
        protected string strAllCompany = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.GetExcellentCompanyList();
            }
        }

        /// <summary>
        /// 获的优秀企业列表
        /// </summary>
        protected void GetExcellentCompanyList()
        {
            FrontPage page = this.Page as FrontPage;
            int CityId = page.CityId;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告优秀企业展示);
            if (AdvList != null && AdvList.Count > 0)
            {
                int AdvCount = AdvList.Count;
                StringBuilder strAdvList = new StringBuilder();
                foreach (EyouSoft.Model.AdvStructure.AdvInfo item in AdvList)
                {
                    strAdvList.AppendFormat(" <div class=\"logodiv\"><a href='{0}' target=\"_blank\" ><img src=\"{1}\" width=\"100\" height=\"55\" /></a></div>", EyouSoft.Common.Utils.SiteAdvUrl(EyouSoft.Common.Utils.GeneratePublicCenterUrl(item.RedirectURL, CityId)), item.ImgPath != "" ? EyouSoft.Common.Domain.FileSystem + item.ImgPath : EyouSoft.Common.Utils.NoLogoImage100_55);
                }

                strAllCompany = strAdvList.ToString();
            }
        }
    }
}