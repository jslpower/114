using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 查看公司销售城市
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class CompanySaleCity : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!Page.IsPostBack)
           {
               this.GetCompanySaleCity(EyouSoft.Common.Utils.InputText(Request.QueryString["CompanyId"]));
           }
        }

        /// <summary>
        /// 获的公司所属销售城市
        /// </summary>
        protected void GetCompanySaleCity(string CompanyId)
        {
            StringBuilder strAllSaleCity = new StringBuilder();

            IList<EyouSoft.Model.SystemStructure.CityBase> SaleList = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanySaleCity(CompanyId);

            if (SaleList != null && SaleList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.CityBase item in SaleList)
                {
                    strAllSaleCity.AppendFormat("{0}&nbsp;", item.CityName);
                }
            }
            SaleList = null;

            if (string.IsNullOrEmpty(strAllSaleCity.ToString()))
            {
                strAllSaleCity.Append("暂无销售城市");
            }
            this.labSaleCityList.Text = strAllSaleCity.ToString();
        }
    }
}
