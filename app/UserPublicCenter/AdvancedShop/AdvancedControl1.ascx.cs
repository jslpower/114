using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AdvancedShop
{
    public partial class AdvancedControl1 : System.Web.UI.UserControl
    {
        private string _setAgencyId;

        public string SetAgencyId
        {
            get { return _setAgencyId; }
            set { _setAgencyId = value; }
        }
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //初始化获取相关新闻
            NewsListInit();
        }


        #region 高级网店新闻列表
        public void NewsListInit()
        {
            string agencyId = this.SetAgencyId;
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Sdll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model = Sdll.GetModel(agencyId);
            if (model != null)
            {
                IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> list = null;
                int recordCount = 0;
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
                {
                    list = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetList(10, 1, ref recordCount, agencyId, EyouSoft.Model.CompanyStructure.CompanyAfficheType.酒店新闻, null, null, null);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队))
                {
                    list = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetList(10, 1, ref recordCount, agencyId, EyouSoft.Model.CompanyStructure.CompanyAfficheType.车队新闻, null, null, null);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店))
                {
                    list = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetList(10, 1, ref recordCount, agencyId, EyouSoft.Model.CompanyStructure.CompanyAfficheType.购物点新闻, null, null, null);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                {
                    list = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetList(10, 1, ref recordCount, agencyId, EyouSoft.Model.CompanyStructure.CompanyAfficheType.景区新闻, null, null, null);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
                {
                    list = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetList(10, 1, ref recordCount, agencyId, EyouSoft.Model.CompanyStructure.CompanyAfficheType.旅游用品新闻, null, null, null);
                }

                if (list != null && list.Count > 0)
                {
                    this.rptNewsList.DataSource = list;
                    this.rptNewsList.DataBind();
                }
                list = null;
            }

        }
        #endregion
    }
}