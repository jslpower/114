using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.AdvStructure;

namespace UserPublicCenter.HomeControl
{
    public partial class RecommendCompany : System.Web.UI.UserControl
    {
        protected int NewJoinCount = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            int CityId = (this.Page as EyouSoft.Common.Control.FrontPage).CityId;
            //绑定推荐企业
           
                //CompanyId  CompanyName 
            rptrecommendC.DataSource = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页推荐企业).Take(6);
                rptrecommendC.DataBind();
          
            //最新加入
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> companyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetTopNumNewCompanys(4, null);
            if (companyList != null && companyList.Count > 0)
            {
                NewJoinCount = companyList.Count;
                rptNewCompany.DataSource = companyList;
                rptNewCompany.DataBind();
            }
            else
            {
                this.emptydata.Visible = true;
            }
           
        }
        protected int CityId;
    }
}