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
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserBackCenter.PrintPage
{
    public partial class TourInfoPrintPage : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected string CompanyName = string.Empty;
        protected string TourContact = string.Empty;//联系人
        protected string TourContactTel = string.Empty;//联系人电话
        protected string License = string.Empty;//许可证号        
        protected string CompanyAddress = string.Empty;//许可证号
        protected string RouteName = string.Empty;//线路名称
        protected string QuickPlanContent = string.Empty;
        protected string TourCode = string.Empty;
        protected int TourDays = 0;
        protected string TourID = string.Empty;

        private string TemplateTourID = string.Empty;
        protected DateTime LeaveDate = DateTime.Now;//
        protected DateTime BackDate = DateTime.Now;//
        protected string NotContainService = string.Empty;
        protected string SpeciallyNotice = string.Empty;
        protected string Traffic = string.Empty;
        protected string MeetTourContect = string.Empty;
        protected string CollectionContect = string.Empty;
        protected bool HasLocalCompanyInfo = true;
        protected bool isStadardType = true;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TourID = Utils.GetQueryStringValue("TourID");
            imgbtnToWord.ImageUrl = this.ImageServerUrl + "/images/dcprint.gif";
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                TemplateTourID = model.ParentTourID;
                #region 获取所属公司信息
                if (this.IsLogin)
                {
                    TourContactTel = this.SiteUserInfo.ContactInfo.Tel;
                    TourContact = this.SiteUserInfo.ContactInfo.ContactName;
                    EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                    EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(this.SiteUserInfo.CompanyID);
                    CompanyName = companyInfoModel.CompanyName;
                    CompanyAddress = companyInfoModel.CompanyAddress;
                    License = companyInfoModel.License;
                    ICompanyInfobll = null;
                    companyInfoModel = null;

                }
                else
                {
                    tbl_Header.Visible = false;
                    //EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                    //EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(model.CompanyID);
                    //CompanyName = companyInfoModel.CompanyName;
                    //CompanyAddress = companyInfoModel.CompanyAddress;
                    //License = companyInfoModel.License;
                    //TourContactTel = companyInfoModel.ContactInfo.Tel;
                    //TourContact = companyInfoModel.ContactInfo.ContactName;
                    //ICompanyInfobll = null;
                    //companyInfoModel = null;
                }
                #endregion

                #region 获取地接社信息
                if (model.LocalTravelAgency != null && model.LocalTravelAgency.Count > 0)
                {
                    rptTourLocalityInfo.DataSource = model.LocalTravelAgency;
                    rptTourLocalityInfo.DataBind();
                }
                else
                {
                    HasLocalCompanyInfo = false;
                }

                #endregion

                #region 价格等级信息
                IList<EyouSoft.Model.TourStructure.TourPriceDetail> PriceLists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail(TemplateTourID);
                List<UserBackCenter.RouteAgency.CompanyPriceDetail> newLists = new List<UserBackCenter.RouteAgency.CompanyPriceDetail>();
                UserBackCenter.RouteAgency.CompanyPriceDetail cpModel = null;
                if (PriceLists != null && PriceLists.Count > 0)
                {
                    ((List<EyouSoft.Model.TourStructure.TourPriceDetail>)PriceLists).ForEach(item =>
                    {
                        cpModel = new UserBackCenter.RouteAgency.CompanyPriceDetail();
                        cpModel.PriceStandName = item.PriceStandName;
                        ((List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)item.PriceDetail).ForEach(childItem =>
                        {
                            switch (childItem.CustomerLevelType)
                            {
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.同行:
                                    cpModel.AdultPrice1 = childItem.AdultPrice;
                                    cpModel.ChildrenPrice1 = childItem.ChildrenPrice;
                                    break;
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.门市:
                                    cpModel.AdultPrice2 = childItem.AdultPrice;
                                    cpModel.ChildrenPrice2 = childItem.ChildrenPrice;
                                    break;
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                    cpModel.SingleRoom1 = childItem.AdultPrice;
                                    cpModel.SingleRoom2 = childItem.ChildrenPrice;
                                    break;
                            }
                        });
                        newLists.Add(cpModel);
                        cpModel = null;
                    });
                    this.rptTourPriceDetail.DataSource = newLists;
                    this.rptTourPriceDetail.DataBind();
                    newLists = null;
                }
                PriceLists = null;
                #endregion

                #region 团队相关信息
                RouteName = model.RouteName;
                LeaveDate = model.LeaveDate;
                BackDate = model.ComeBackDate;
                TourDays = model.TourDays;
                TourCode = model.TourNo;
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)//标准发布
                {
                    Traffic = EyouSoft.Common.Function.StringValidate.TextToHtml( model.LeaveTraffic.Trim());
                    MeetTourContect = EyouSoft.Common.Function.StringValidate.TextToHtml(model.MeetTourContect.Trim());
                    CollectionContect =  EyouSoft.Common.Function.StringValidate.TextToHtml(model.CollectionContect.Trim());
                    //行程信息及相关
                    rptStandardPlan.DataSource = model.StandardPlan;
                    rptStandardPlan.DataBind();
                    //服务标准及说明
                    List<EyouSoft.Model.TourStructure.ServiceStandard> serviceList = new List<EyouSoft.Model.TourStructure.ServiceStandard>();
                    if (model.ServiceStandard != null)
                    {
                        serviceList.Add(model.ServiceStandard);
                        NotContainService = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.NotContainService);
                        SpeciallyNotice = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SpeciallyNotice);
                        this.rptServiceStandard.DataSource = serviceList;
                        this.rptServiceStandard.DataBind();
                    }
                }
                else//快速发布
                {
                    isStadardType = false;
                    pnlQuickPlan.Visible = true;
                    QuickPlanContent = model.QuickPlan;
                }
                #endregion
            }
            Ibll = null;
            model = null;
        }
        protected void btnWordPrint_Click(object sender, EventArgs e)
        {
            string PrintTopHtml = Request.Form["hidPrintTopHTML"];
            string PrintFooterHtml = Request.Form["hidPrintFooterHTML"];
            string PrintMiddleHtml = string.Empty;
            string saveFileName = HttpUtility.UrlEncode("行程单.doc");
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                this.TemplateTourID = model.ParentTourID;
                #region 价格等级信息
                IList<EyouSoft.Model.TourStructure.TourPriceDetail> PriceLists = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail(TemplateTourID);
                List<UserBackCenter.RouteAgency.CompanyPriceDetail> newLists = new List<UserBackCenter.RouteAgency.CompanyPriceDetail>();
                if (PriceLists != null && PriceLists.Count > 0)
                {
                    PrintMiddleHtml += "<table width=\"720\" border=\"0\" align=\"center\" cellspacing=\"1\" bgcolor=\"#FFFFFF\">";
                    ((List<EyouSoft.Model.TourStructure.TourPriceDetail>)PriceLists).ForEach(item =>
                    {
                        ((List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>)item.PriceDetail).ForEach(childItem =>
                        {
                            switch (childItem.CustomerLevelType)
                            {
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.门市:
                                    PrintMiddleHtml += "<tr><td width=\"70\" align=\"left\" bgcolor=\"#eeeeee\">" + item.PriceStandName + "：</td>";
                                    PrintMiddleHtml += "<td  bgcolor=\"#eeeeee\">成人价：<span class=\"bottow_side2\">" + childItem.AdultPrice.ToString("F2") + "</span>元／人";
                                    PrintMiddleHtml += " <td  align=\"right\" bgcolor=\"#eeeeee\">儿童价： ";
                                    PrintMiddleHtml += "<span class=\"bottow_side2\">" + childItem.ChildrenPrice.ToString("F2") + "</span>元／人</td>";
                                    break;
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                    PrintMiddleHtml += "<td align=\"right\" bgcolor=\"#eeeeee\">单房差：";
                                    PrintMiddleHtml += "<span class=\"bottow_side2\">" + childItem.AdultPrice.ToString("F2") + "</span>元／人</td></tr>";
                                    break;
                            }
                        });
                    });
                    newLists = null;
                    PrintMiddleHtml += "</table>";
                }
                PriceLists = null;
                #endregion

            }
            model = null;
            Ibll = null;
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string wordTemplate = @"
                <html>
                <head>
                    <title>打印预览</title>
                    <style type=""text/css"">
                    body {{color:#000000;font-size:12px;font-family:""宋体"";background:#fff; margin:0px;}}
                    img {{border: thin none;}}
                    table {{border-collapse:collapse;width:720px;}}                    	
                    .headertitle {{font-family:""黑体""; font-size:25px; line-height:120%; font-weight:bold;}}
                    .bottow_side2{{background: #EEEEEE none repeat scroll 0 0;
                                border-color: -moz-use-text-color -moz-use-text-color #000000;
                                border-style: none none solid;
                                border-width: 0 0 1px;
                        }}
                    </style>
                </head>
                <body>
                    <div id=""divPrintPreview"">{0}{1}{2}</div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, PrintTopHtml, PrintMiddleHtml, PrintFooterHtml));
            Response.End();
        }

        /// <summary>
        /// 行程信息转化：
        /// 转化行程当天日期，星期几
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptStandardPlan_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                EyouSoft.Model.TourStructure.TourStandardPlan model = (EyouSoft.Model.TourStructure.TourStandardPlan)e.Item.DataItem;
                Literal ltrPlanDate = (Literal)e.Item.FindControl("ltrPlanDate");
                if (ltrPlanDate != null)
                    ltrPlanDate.Text = LeaveDate.AddDays(model.PlanDay - 1).ToShortDateString();
                Literal ltrWeekDay = (Literal)e.Item.FindControl("ltrWeekDay");
                if (ltrWeekDay != null)
                    ltrWeekDay.Text = EyouSoft.Common.Utils.ConvertWeekDayToChinese(LeaveDate.AddDays(model.PlanDay - 1));

            }
        }
    }
}
