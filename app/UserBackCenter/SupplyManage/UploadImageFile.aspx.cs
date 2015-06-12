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
    /// 供应商后台:图片上传
    /// 罗伏先   2010-07-24
    /// </summary>
    public partial class UploadImageFile : EyouSoft.Common.Control.BackPage
    {
        private string CompanyID = string.Empty;
        protected string CompanyImg = string.Empty;//宣传图片

        protected int ProductInfoID1 = 0;//
        protected int ProductInfoID2 = 0;//
        protected int ProductInfoID3 = 0;//
        protected int ProductInfoID4 = 0;//

        protected string ProductInfoImg1 = string.Empty;//链接1
        protected string ProductInfoImg2 = string.Empty;//链接2
        protected string ProductInfoImg3 = string.Empty;//链接3
        protected string ProductInfoImg4 = string.Empty;//链接4

        protected string ProductInfoLink1 = string.Empty;//链接1
        protected string ProductInfoLink2 = string.Empty;//链接2
        protected string ProductInfoLink3 = string.Empty;//链接3
        protected string ProductInfoLink4 = string.Empty;//链接4
        protected string WebSiteUrl = string.Empty;
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
            WebSiteUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(CompanyID);// Domain.UserPublicCenter + "/AdvancedShop/AdvancedShopDetails.aspx?cid=" + CompanyID;
            if (Utils.GetQueryStringValue("action") == "SupplyImageSave")
            {
                SupplyImageSave();
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
                CompanyImg = model.CompanyImg;
                if (model.ProductInfo != null && model.ProductInfo.Count > 0)
                {
                    for (int i = 0; i < model.ProductInfo.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                ProductInfoID1 = model.ProductInfo[i].ID;
                                this.ProductInfoImg1 = model.ProductInfo[i].ImagePath;
                                this.ProductInfoLink1 = model.ProductInfo[i].ImageLink.Replace(Utils.EmptyLinkCode, "");
                                break;
                            case 1:
                                ProductInfoID2 = model.ProductInfo[i].ID;
                                this.ProductInfoImg2 = model.ProductInfo[i].ImagePath;
                                this.ProductInfoLink2 = model.ProductInfo[i].ImageLink.Replace(Utils.EmptyLinkCode, "");
                                break;
                            case 2:
                                ProductInfoID3 = model.ProductInfo[i].ID;
                                this.ProductInfoImg3 = model.ProductInfo[i].ImagePath;
                                this.ProductInfoLink3 = model.ProductInfo[i].ImageLink.Replace(Utils.EmptyLinkCode, "");
                                break;
                            case 3:
                                ProductInfoID4 = model.ProductInfo[i].ID;
                                this.ProductInfoImg4 = model.ProductInfo[i].ImagePath;
                                this.ProductInfoLink4 = model.ProductInfo[i].ImageLink.Replace(Utils.EmptyLinkCode, "");
                                break;
                        }
                    }
                }
            }
            Ibll = null;
            model = null;
        }
        /// <summary>
        /// 供应商图片上传
        /// </summary>
        private void SupplyImageSave()
        {

            //if (!this.IsSupplyUser)
            //{
            //    Response.Clear();
            //    Response.Write("[{isSuccess:true,ErrorMessage:'对不起，你的公司身份不是属于供应商，没有权限进入供应商网店管理！'}]");
            //    Response.End();
            //    return;
            //}
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("[{isSuccess:false,Message:'对不起，新注册用户还未审核，没有权限进入供应商网店管理！'}]");
                Response.End();
            }
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Ibll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.ProductInfo model = null;
            IList<EyouSoft.Model.CompanyStructure.ProductInfo> ProductLists = new List<EyouSoft.Model.CompanyStructure.ProductInfo>();
            string companyImgPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$Upload_CompanyImg$hidFileName");
            if (string.IsNullOrEmpty(companyImgPath))
            {
                companyImgPath = Utils.GetFormValue("hidUpload_CompanyImg");
            }
            
            for (int i = 1; i <= 4; i++)
            {
                model = new EyouSoft.Model.CompanyStructure.ProductInfo();
                string linkUrl = Utils.GetFormValue("Shop_ProductInfoLink" + i.ToString());
                if (linkUrl != "")
                {
                    model.ImageLink = linkUrl.Contains("http://") ? linkUrl : "http://" + linkUrl;
                }
                else {
                    model.ImageLink = Utils.EmptyLinkCode ;
                }
                model.ImagePath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$Upload_ProductInfo" + i.ToString() + "$hidFileName");
                model.ID =Utils.GetInt(Request.Form["hid_MyOwenerShop_ProductInfoID" + i.ToString()]);
                if (string.IsNullOrEmpty(model.ImagePath))
                {
                    model.ImagePath = Utils.GetFormValue("hid_MyOwenerShop_Upload_ProductInfo"+i.ToString());
                }
                model.ThumbPath = "";
                ProductLists.Add(model);
            }
            bool isTrue = Ibll.SetProduct(CompanyID, companyImgPath,ProductLists);
            if (isTrue)
            {
                InitPage();
                Response.Clear();
                Response.Write("[{isSuccess:true,ErrorMessage:' 图片上传成功！'}]");
                Response.End();
            }
            Ibll = null;
            ProductLists = null;
            model = null;
        }
    }
}
