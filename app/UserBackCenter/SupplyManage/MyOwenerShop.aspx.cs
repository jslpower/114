using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Security.Membership;

namespace UserBackCenter.SupplyManage
{
    /// <summary>
    /// 供应商后台:高级网店管理
    /// 罗伏先   2010-07-22
    /// </summary>
    public partial class MyOwenerShop : EyouSoft.Common.Control.BackPage
    {
        private string CompanyID = string.Empty;
        protected string CompanyName = string.Empty;
        protected string Remark = string.Empty;
        protected string ShortRemark = string.Empty;
        protected string Mobile = string.Empty;
        protected string Tel = string.Empty;
        protected string Fax = string.Empty;
        protected string Email = string.Empty;
        protected string Address = string.Empty;
        protected string ConnectName = string.Empty;
        protected string WebSite = string.Empty;
        protected string cbl_CompanyTagHtml = string.Empty;

        protected string CompanyImg = string.Empty;//宣传图片
        protected string LogImg = string.Empty;
        private EyouSoft.Model.CompanyStructure.CompanyType? companyType = null;
        /// <summary>
        /// 高级网店地址
        /// </summary>
        protected string WebSiteUrl = string.Empty;
        protected string ShortRmarkIntroduce = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.SupplierInfo model = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyID);
            if (!model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                Response.Clear();
                Response.Write("对不起，你没有权限操作供应商高级网店后台，如有需要请申请开通！");
                Response.End();
            }
            model = null;
            getUserCompanyType();
            WebSiteUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(CompanyID);// Domain.UserPublicCenter + "/AdvancedShop/AdvancedShopDetails.aspx?cid=" + CompanyID;
            if (Utils.GetQueryStringValue("action") == "CompanyInfo")
            {
                SupplyInfoSave();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model = Ibll.GetModel(CompanyID);
            if (model != null)
            {
                CompanyName = model.CompanyName;
                Remark = model.Remark;
                ConnectName = model.ContactInfo.ContactName;
                Mobile = model.ContactInfo.Mobile;
                Tel = model.ContactInfo.Tel;
                Fax = model.ContactInfo.Fax;
                Email = model.ContactInfo.Email;
                Address = model.CompanyAddress;
                ProvinceAndCityList1.SetProvinceId = model.ProvinceId;                
                ProvinceAndCityList1.SetCityId = model.CityId;
                WebSite = model.WebSite;
                CompanyImg = model.CompanyImg;
                LogImg = model.CompanyImgThumb;
                if (companyType != EyouSoft.Model.CompanyStructure.CompanyType.酒店 || companyType != EyouSoft.Model.CompanyStructure.CompanyType.景区)
                {
                    ShortRemark = model.ShortRemark;
                }
            }
            //EyouSoft.Model.CompanyStructure.HotelLevel 酒店星级
            #region 酒店：周边环境+星级
            if (companyType == EyouSoft.Model.CompanyStructure.CompanyType.酒店)
            {
                string[] HotelLevelName = Enum.GetNames(typeof(EyouSoft.Model.CompanyStructure.HotelLevel));
                rbl_HotelLevel.DataSource = HotelLevelName;
                rbl_HotelLevel.DataBind();
                rbl_HotelLevel.Items.FindByText(Enum.GetName(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), model.CompanyLevel)).Selected = true;                
                ((List<EyouSoft.Model.SystemStructure.SysFieldBase>)EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetHotelArea()).ForEach(delegate(EyouSoft.Model.SystemStructure.SysFieldBase item)
                {
                    string strChecked = "";
                    if (model.CompanyTag != null && model.CompanyTag.Count > 0)
                    {
                        int ReturnCount = ((List<EyouSoft.Model.CompanyStructure.CompanyTag>)model.CompanyTag).Count(delegate(EyouSoft.Model.CompanyStructure.CompanyTag sysItem)
                        {
                            return sysItem.FieldId == item.FieldId;
                        });
                        if (ReturnCount > 0) strChecked = "checked=\"checked\"";
                    }
                    cbl_CompanyTagHtml += string.Format("<input id=\"cb{0}\" type=\"checkbox\" name=\"cbCompanyTag\" value=\"{0}\" {2} /><label for=\"cb{0}\">{1}</label><input name=\"hidCompanyTag{0}\" value=\"{1}\" type=\"hidden\" />", item.FieldId, item.FieldName, strChecked);
                });
            }
            #endregion
            Ibll = null;
            model = null;
        }
        private void getUserCompanyType()
        {
            if (this.SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                switch (this.SiteUserInfo.CompanyRole.RoleItems[0])
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        tr_ShortRemark.Visible = false;
                        companyType = EyouSoft.Model.CompanyStructure.CompanyType.景区;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        tr_CompanyTag.Visible = true;
                        tr_CompanyLevel.Visible = true;
                        tr_ShortRemark.Visible = false;
                        companyType = EyouSoft.Model.CompanyStructure.CompanyType.酒店;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        ShortRmarkIntroduce = "主营产品";
                        companyType = EyouSoft.Model.CompanyStructure.CompanyType.购物店;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        ShortRmarkIntroduce = "主营产品";
                        companyType = EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店;
                        break;
                    default:
                        companyType = EyouSoft.Model.CompanyStructure.CompanyType.车队;
                        ShortRmarkIntroduce = "主要优势";
                        break;
                }
            }
        }
        /// <summary>
        /// 供应商单位信息保存
        /// </summary>
        private void SupplyInfoSave()
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,Message:'对不起，新注册用户还未审核，没有权限进入供应商网店管理！'}]");
                Response.End();
            }
            bool isTrue = false;
            string webSite = string.Empty;
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model = new EyouSoft.Model.CompanyStructure.SupplierInfo();
            IList<EyouSoft.Model.CompanyStructure.CompanyTag> lists = new List<EyouSoft.Model.CompanyStructure.CompanyTag>();
            EyouSoft.Model.CompanyStructure.CompanyRole RoleModel = new EyouSoft.Model.CompanyStructure.CompanyRole();
            model.ID = CompanyID;
            model.CompanyName =this.SiteUserInfo.CompanyName ;//Utils.GetFormValue("MyOwenerShop_CompanyName");
            model.CompanyAddress = Utils.GetFormValue("MyOwenerShop_ConnectAddress");
            string Remark = Server.UrlDecode(Request.Form["MyOwenerShop_Remark"]);
            model.Remark = Utils.EditInputText(Server.UrlDecode(Remark.Substring(0, Remark.Length > 4000 ? 4000 : Remark.Length)));

            model.ContactInfo.Fax = Utils.GetFormValue("MyOwenerShop_ConnectFax");
            model.ContactInfo.Tel = Utils.GetFormValue("MyOwenerShop_ConnectTel");
            model.ContactInfo.Email = Utils.GetFormValue("MyOwenerShop_ConnectEmail");
            model.ContactInfo.Mobile = Utils.GetFormValue("MyOwenerShop_Mobile");
            model.ContactInfo.ContactName = Utils.GetFormValue("MyOwenerShop_ConnectName");
            model.ProvinceId = Utils.GetInt(Request.Form["ctl00$ContentPlaceHolder1$ProvinceAndCityList1$ddl_ProvinceList"]);
            model.CityId = Utils.GetInt(Request.Form["ctl00$ContentPlaceHolder1$ProvinceAndCityList1$ddl_CityList"]);
            webSite = Utils.GetFormValue("MyOwenerShop_WebSite");
            if (webSite != "")
            {
                model.WebSite = (webSite != "" && webSite.Contains("http://")) ? webSite : "http://" + webSite;

            }
            else {
                model.WebSite = webSite;
            }
            model.CompanyImgThumb = Utils.GetFormValue("ctl00$ContentPlaceHolder1$Upload_LogoImg$hidFileName");
            
            model.ShortRemark = Utils.GetFormValue("MyOwenerShop_ShortRemark");
            if (string.IsNullOrEmpty(model.CompanyImgThumb))
            {
                model.CompanyImgThumb = Utils.GetFormValue("hidLogoImg");
            }
            if (companyType == EyouSoft.Model.CompanyStructure.CompanyType.酒店)
            {
                EyouSoft.Model.CompanyStructure.CompanyTag sysModel;
                string[] strCompanyTag = Utils.GetFormValues("cbCompanyTag");
                for (int i = 0; i < strCompanyTag.Length; i++)
                {
                    sysModel = new EyouSoft.Model.CompanyStructure.CompanyTag();
                    sysModel.FieldId = Utils.GetInt(strCompanyTag[i]);
                    sysModel.FieldName = Utils.GetFormValue("hidCompanyTag" + strCompanyTag[i]);
                    lists.Add(sysModel);
                    sysModel = null;
                }
                EyouSoft.Model.CompanyStructure.HotelLevel level = (EyouSoft.Model.CompanyStructure.HotelLevel)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), Utils.GetFormValue("ctl00$ContentPlaceHolder1$rbl_HotelLevel"));
                model.CompanyLevel = (int)level;
                model.CompanyTag = lists;               
            }
            isTrue = Ibll.Update(model);
            Ibll = null;
            model = null;
            lists = null;
            if (isTrue)
            {
                Response.Clear();
                Response.Write("1");
                Response.End();
            }
        }
    }
}
