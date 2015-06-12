using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SeniorOnlineShop.seniorshop
{
    /// <summary>
    /// 页面功能：关于我们
    /// 开发时间：2010-07-13  开发人：杜桂云
    /// </summary>
    public partial class AboutUs : EyouSoft.Common.Control.FrontPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //品牌名称
            ltrBrandName.Text = this.Master.DetailCompanyInfo.CompanyBrand;

            //单位名称初始化
            ltrCompanyName.Text = this.Master.DetailCompanyInfo.CompanyName;
            //许可证初始化
            ltrXuKeZheng.Text = this.Master.DetailCompanyInfo.License;
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.Master.CompanyId);
            if (model != null)
            {
                //关于我们初始化
                ltrAboutUs.Text = model.CompanyInfo;
            }
            model = null;
            Page.Title = "关于我们";
        }
        #endregion
    }
}
