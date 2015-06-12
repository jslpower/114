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
namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 代客预订，获取预订公司联系信息
    /// </summary>
    public partial class AjaxBookingTourCompanyInfo :EyouSoft.Common.Control.BasePage
    {
        //预订人姓名
        protected string ManName = string.Empty;
        //联系电话
        protected string ConnectTel = string.Empty;
        //传真
        protected string ConnectFax = string.Empty;
        //手机
        protected string ConnectMobilePhone = string.Empty;
        private string CompanyID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.IsLogin)
                {
                    if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("TourCompanyID")))
                    {
                        CompanyID = Utils.GetQueryStringValue("TourCompanyID");
                        EyouSoft.IBLL.CompanyStructure.ICompanyInfo Ibll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                        EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = new EyouSoft.Model.CompanyStructure.CompanyDetailInfo();
                        model = Ibll.GetModel(CompanyID);
                        ManName = model.ContactInfo.ContactName;
                        ConnectTel = model.ContactInfo.Tel;
                        ConnectFax = model.ContactInfo.Fax;
                        ConnectMobilePhone = model.ContactInfo.Mobile;
                        model = null;
                        Ibll = null;
                    }
                }
                else
                {

                }
            }
        }
    }
}
