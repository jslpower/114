using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页精品推荐广告及团队列表
    /// </summary>
    public partial class RecommendList : System.Web.UI.UserControl
    {
        protected int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetRecommendImages();
                GetRecommendList();
            }
        }

        /// <summary>
        /// 获的推荐的图文广告信息
        /// </summary>
        protected void GetRecommendImages()
        {
            FrontPage page = this.Page as FrontPage;
             CityId = page.CityId;
            EyouSoft.Model.AdvStructure.AdvPosition PositionType = EyouSoft.Model.AdvStructure.AdvPosition.首页广告精品推荐图文;

            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, PositionType);
            if (AdvList != null && AdvList.Count > 0)
            {
                this.repImagesList.DataSource = AdvList;
                this.repImagesList.DataBind();
            }
            AdvList = null;
        }

        /// <summary>
        /// 获的推荐的信息列表
        /// </summary>
        protected void GetRecommendList()
        {
            FrontPage page = this.Page as FrontPage;
            int CityId = page.CityId;
            EyouSoft.Model.AdvStructure.AdvPosition PositionType = EyouSoft.Model.AdvStructure.AdvPosition.首页广告精品推荐文字;

            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, PositionType);
            if (AdvList != null && AdvList.Count > 0)
            {
                this.repCommendList.DataSource = AdvList;
                this.repCommendList.DataBind();
            }
            AdvList = null;
        }

        /// <summary>
        /// 获的发布单位的网店链接
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="CompanyName"></param>
        /// <returns></returns>
        protected string GetCompanyUrl(string CompanyId,string CompanyName)
        {
            string goToUrl = "";
            string returnUrl = "";
            if (!string.IsNullOrEmpty(CompanyId))
            {
                 EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model=  EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
                 if (Model != null)
                 {
                     EyouSoft.Model.CompanyStructure.CompanyType[] CompanyTypeItems = Model.CompanyRole.RoleItems;
                     bool isRouteAgency = false;
                     if (CompanyTypeItems != null && CompanyTypeItems.Length > 0)
                     {
                         for (int i = 0; i < CompanyTypeItems.Length; i++)
                         {
                             if (CompanyTypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                             {
                                 isRouteAgency = true;
                                 break;
                             }
                         }
                     }
                     EyouSoft.Model.CompanyStructure.CompanyType Type = Model.CompanyRole.RoleItems[0];
                     if (isRouteAgency)
                     {
                         Type = EyouSoft.Model.CompanyStructure.CompanyType.专线;
                     }

                     goToUrl = EyouSoft.Common.Utils.GetCompanyDomain(CompanyId, Type, CityId);
                     if (goToUrl != "" && goToUrl != EyouSoft.Common.Utils.EmptyLinkCode)
                     {
                         returnUrl = string.Format(" <a class=\"linecom\"  href=\"{0}\" target=\"_blank\" title='{2}'>{1}</a>", EyouSoft.Common.Utils.SiteAdvUrl(goToUrl), EyouSoft.Common.Utils.GetText(CompanyName, 17), CompanyName);
                     }
                 }
                 Model = null;
            }
            return returnUrl;
        }
    }
}