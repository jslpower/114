using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace UserPublicCenter.HomeControl
{
    public partial class Companys : System.Web.UI.UserControl
    {
        protected string companysHtml;//旅行社
        protected string ImageServerPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageServerPath = (this.Page as EyouSoft.Common.Control.FrontPage).ImageServerUrl;
            if (IsShow)
            {
                int CityId = (this.Page as EyouSoft.Common.Control.FrontPage).CityId;
                IList<EyouSoft.Model.AdvStructure.SiteExtendInfo> siteExtendList = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().GetSiteExtendList(CityId, true);
                StringBuilder strBuilder = new StringBuilder();
                if (siteExtendList != null && siteExtendList.Count > 0)
                {
                    foreach (EyouSoft.Model.AdvStructure.SiteExtendInfo model in siteExtendList)
                    {
                        strBuilder.AppendFormat("<li><a title=\"{5}\" href=\"{4}\" style=\"color:{0};font-weight:{2}\" target=\"_blank\">·{1}<em>{3}</em></a></li>", model.Color, EyouSoft.Common.Utils.GetText(model.CompanyName,18), model.IsBold ? "bold" : "", model.ProductNum == 0 ? "" : string.Format("（{0}）", model.ProductNum), EyouSoft.Common.Utils.GetDomainByCompanyId(model.CompanyID,CityId),model.CompanyName);
                    }
                }
                companysHtml = strBuilder.ToString();
            }
        }
        public string CityName;//城市名称
        public int CityId;
        public bool IsShow;//是否显示


    }
}