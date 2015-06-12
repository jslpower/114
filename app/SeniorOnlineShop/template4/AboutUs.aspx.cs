using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 页面功能：关于我们
    /// 开发时间：2010-11-15  开发人：张志瑜
    /// </summary>
    public partial class AboutUs : EyouSoft.Common.Control.FrontPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //品牌名称
            ltrBrandName.Text = this.Master.CompanyInfo.CompanyBrand;

            //单位名称初始化
            ltrCompanyName.Text = this.Master.CompanyInfo.CompanyName;
            //许可证初始化
            ltrXuKeZheng.Text = this.Master.CompanyInfo.License;
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.Master.CompanyId);
            if (model != null)
            {
                //关于我们初始化
                ltrAboutUs.Text = model.CompanyInfo;
            }
            model = null;
        }
        #endregion
    }
}
