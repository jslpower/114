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
using EyouSoft.Common;
namespace SeniorOnlineShop.shop
{
    public partial class LineDescription :EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitCompanyInfo();
                this.Title = "专线介绍";
                line.HRef = Utils.GenerateShopPageUrl2("/shop", Master.CompanyId);
                info.HRef = Utils.GenerateShopPageUrl2("/LineDescription", Master.CompanyId);
            }           
        }
        protected void InitCompanyInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo info = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(Master.CompanyId);
            if (info != null)
            {
                ltr_Address.Text = info.CompanyAddress;
                ltr_CompanyInfo.Text = info.Remark;
                ltr_CompanyName.Text = info.CompanyName;
                ltr_License.Text = info.License;
                ltr_Mobile.Text = info.ContactInfo.Mobile;
                ltr_Person.Text = info.ContactInfo.ContactName;
                ltr_Phone.Text = info.ContactInfo.Tel;                
            }
            info = null;
        }
    }
}
