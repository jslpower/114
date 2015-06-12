using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HomeControl
{   
    /// <summary>
    /// 金牌企业
    /// </summary>
    public partial class GoldCompany : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsShow)
            {
                rptGoldCompany.DataSource = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页金牌企业展示广告);
                rptGoldCompany.DataBind();
            }
        }
        public int CityId;
        public bool IsShow;

    }
}