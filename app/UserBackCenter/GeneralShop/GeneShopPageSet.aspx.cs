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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.Collections.Generic;
using System.Text;

namespace UserBackCenter.GeneralShop
{
    public partial class GeneShopPageSet : BasePage
    { 
        protected string UserName = "";
        protected int pageSize = 7;
        protected int pageIndex = 1;
        protected string MQHtml = string.Empty;
        protected string ImagePath = "";
        protected string LogoPath = string.Empty;
        protected string UnionLogo = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.IsLogin)
                {
                    EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
                }
                InitSysAreaList();
                InitCompanyInfo();
                GetUnionLogo();
            }
        }
        //加载线路信息
        private void InitSysAreaList()
        {
            int recordCount = 0;
            pageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            IList<EyouSoft.Model.TourStructure.TourInfo> list = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetNormalEshopTours(pageSize, pageIndex, ref recordCount, null, null, this.SiteUserInfo.CompanyID, EyouSoft.Model.TourStructure.TourDisplayType.线路产品);
            if (list != null && list.Count > 0)
            {
                crptLineList.DataSource = list;
                crptLineList.DataBind();
                //分页控件绑定
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.CurrencyPage = pageIndex;
                this.ExportPageInfo1.intRecordCount =recordCount;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.Visible = false;
            }
            else
            {
                crptLineList.EmptyText = "<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无旅游线路信息！</div> ";
                this.ExportPageInfo1.Visible = false;
            }
            list = null;
        }
        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionLogo = EyouSoft.Common.Domain.FileSystem + Model.UnionLog;
            }
            else
            {
                UnionLogo = EyouSoft.Common.Domain.ServerComponents + "/images/UserPublicCenter/indexlogo.gif";
            }
            Model = null;

        }
        //加载公司信息
        private void InitCompanyInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo compDetail = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SiteUserInfo.CompanyID); //公司详细信息
            if (compDetail != null)
            {
                ltr_BrandName.Text = compDetail.CompanyBrand;
                ltr_CompanyName.Text = compDetail.CompanyName;
                ltr_Address.Text = compDetail.CompanyAddress;               
                ltr_LinkPerson.Text = compDetail.ContactInfo.ContactName;
                ltr_Faxs.Text = compDetail.ContactInfo.Fax;
                ltr_Mobile.Text = compDetail.ContactInfo.Mobile;
                ltr_Phone.Text = compDetail.ContactInfo.Tel;
                if (!string.IsNullOrEmpty(compDetail.ContactInfo.MQ))
                {
                    MQHtml = string.Format("<img src=\"{0}/Images/seniorshop/mqonline.gif\" width=\"70\" height=\"18\" />", ImageManage.GetImagerServerUrl(1));
                }
                ImagePath=Utils.GetLineShopImgPath(compDetail.AttachInfo.CompanyImg.ImagePath,6); //宣传图片       
                LogoPath =Utils.GetLineShopImgPath(compDetail.AttachInfo.CompanyLogo.ImagePath,2); //Logo
            }
            compDetail = null;
        }

        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }
    
    }
}
