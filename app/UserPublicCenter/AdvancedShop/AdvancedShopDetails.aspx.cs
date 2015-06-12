using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using UserPublicCenter.HotelManage;
using System.Text;
using System.Text.RegularExpressions;

namespace UserPublicCenter.AdvancedShop
{
    /// <summary>
    /// 高级网店信息
    /// 功能：显示高级网店的详细信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    public partial class AdvancedShopDetails : EyouSoft.Common.Control.FrontPage
    {

        protected string paramType;
        public string MQ = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string agencyId = Request.QueryString["cid"];
                
                if (EyouSoft.Common.Utils.GetString(agencyId, "") != "")
                {
                    DataInit(agencyId);
                    this.AdvancedControl11.SetAgencyId = agencyId;
                    this.AdvancedControl11.CityId = this.CityId;
                }
            }
            
        }

        #region
        /// <summary>
        /// 获得高级网点的实体
        /// </summary>
        public void DataInit(string agencyId)
        {
            EyouSoft.IBLL.CompanyStructure.ISupplierInfo Sdll = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.SupplierInfo model  = Sdll.GetModel(agencyId);
            if (model != null)
            {
                if (!model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
                {
                    Utils.ShowError("对不起，该网店尚未开通高级！", "SupplyError");
                    return;
                }
                this.ImgHead.ImageUrl = Domain.FileSystem + model.CompanyImg;
                this.lblTitle.Text = model.CompanyName;
                this.lblContent.Text =Utils.RemoveHref(model.Remark);
                this.lblUserName.Text = model.ContactInfo.ContactName;
                this.lblTelPhone.Text = model.ContactInfo.Tel;
                this.lblFax.Text = model.ContactInfo.Fax;
                this.lblAddress.Text = model.CompanyAddress;

                //设置地区
                EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(model.CityId);
                    EyouSoft.Model.SystemStructure.SysProvince provModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(model.ProvinceId);

                    if (provModel != null &&cityModel !=null)
                    {
                        this.lblDiQu.Text = "" + provModel.ProvinceName + "&nbsp;&nbsp;" + cityModel.CityName + "";
                    }
                   
                this.lblUrl.Text = model.WebSite;
                this.MQ = model.ContactInfo.MQ;
                this.AdvancedControl11.SetAgencyId = agencyId;

                IList<EyouSoft.Model.CompanyStructure.ProductInfo> list = model.ProductInfo;
                if (list != null && list.Count >0)
                {
                    this.rptContentImgList.DataSource = list.Take(4);
                    this.DataBind();
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                {
                    this.CityAndMenu1.HeadMenuIndex = 4;
                    this.Page.Title = model.CompanyName + "_景区";
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
                {
                    this.CityAndMenu1.HeadMenuIndex = 5;
                    this.Page.Title = model.CompanyName + "_酒店";
                    //星级、周边环境
                    this.lblLeaveTitle.Visible = true;
                    this.lblLeave.Visible = true;
                    this.lblHuanJingTitle.Visible = true;
                    this.lblHuanJing.Visible = true;

                    this.lblLeave.Text = HotelLevel(model.CompanyLevel);
                    if (model.CompanyTag.Count > 0)
                    {
                        this.lblHuanJing.Text = "<a>";
                        for (int i = 0; i < model.CompanyTag.Count; i++)
                        {
                            this.lblHuanJing.Text += model.CompanyTag[i].FieldName + " &nbsp";
                        }
                        this.lblHuanJing.Text += "</a>";
                    }
                    
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队))
                {
                    this.CityAndMenu1.HeadMenuIndex = 6;
                    //this.Page.Title = model.CompanyName + "_车队";
                    //业务优势
                    this.lblRemark.Visible = true;
                    this.lblRemarkTitle.Visible = true;
                    this.lblRemarkTitle.Text = "业务优势：";
                    this.lblRemark.Text = model.ShortRemark;
                    //设置Title.....
                    this.Title = string.Format(EyouSoft.Common.PageTitle.CarDetail_Title, CityModel.CityName, model.CompanyName);
                    AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.CarDetail_Des, CityModel.CityName, model.CompanyName));
                    AddMetaTag("keywords", EyouSoft.Common.PageTitle.CarDetail_Keywords);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
                {
                    this.CityAndMenu1.HeadMenuIndex = 7;
                    //this.Page.Title = model.CompanyName + "_旅游用品店";
                    //主营产品
                    this.lblRemark.Visible = true;
                    this.lblRemarkTitle.Visible = true;
                    this.lblRemarkTitle.Text = "主营产品：";
                    this.lblRemark.Text = model.ShortRemark;
                    //设置Title.....
                    this.Title = string.Format(EyouSoft.Common.PageTitle.TravelDetail_Title, CityModel.CityName, model.CompanyName);
                    AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.TravelDetail_Des, CityModel.CityName, model.CompanyName));
                    AddMetaTag("keywords", EyouSoft.Common.PageTitle.TravelDetail_Keywords);
                }
                if (model.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店))
                {
                    this.CityAndMenu1.HeadMenuIndex = 8;
                    this.Page.Title = model.CompanyName + "_购物店";
                    //主营产品
                    this.lblRemark.Visible = true;
                    this.lblRemarkTitle.Visible = true;
                    this.lblRemarkTitle.Text = "主营产品：";
                    this.lblRemark.Text = model.ShortRemark;
                }

                list = null;
                model = null;
                Sdll = null;
            }
        }
        #endregion

        #region 显示高级网店的星星
        public string HotelLevel(int level)
        {
            EyouSoft.Model.CompanyStructure.HotelLevel HotelLevel = new EyouSoft.Model.CompanyStructure.HotelLevel();

            HotelLevel = (EyouSoft.Model.CompanyStructure.HotelLevel)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), Enum.GetName(typeof(EyouSoft.Model.CompanyStructure.HotelLevel), level));

            int count = 0;
            switch (HotelLevel)
            {
                case EyouSoft.Model.CompanyStructure.HotelLevel.三星以下: break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.准三星: count = 3; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.三星或同级: count = 3; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.准四星: count = 4; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.四星或同级: count = 4; break;
                case EyouSoft.Model.CompanyStructure.HotelLevel.五星或同级: count = 5; break;
                default: break;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append("<img src=\"" + this.ImageServerPath + "/images/UserPublicCenter/xing.gif\" width=\"16\" height=\"16\" style=\"margin-bottom: -3px;\" />");
            }
            if (count == 0)
            {
                sb.Append("三星以下");
            }
            if (sb.ToString() != "")
            {
                return sb.ToString();
            }
            return "";
        }
        #endregion
       
    }
}
