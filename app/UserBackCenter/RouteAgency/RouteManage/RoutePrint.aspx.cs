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
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 行程单
    /// 罗丽娥   2010-07-01
    /// </summary>
    public partial class RoutePrint : EyouSoft.Common.Control.FrontPage
    {
        protected string tmpRouteName = string.Empty;
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        private string CompanyID = "0";
        protected string RouteID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AddStylesheetInclude(CssManage.GetCssFilePath("main"));
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                    CompanyID = UserInfoModel.CompanyID;
                }
            }

            if (!Page.IsPostBack)
            {
                // 根据当前登录人获取当前公司信息


                if (!String.IsNullOrEmpty(Request.QueryString["RouteID"]))
                {
                    RouteID = Utils.InputText(Request.QueryString["RouteID"]);
                }
                else
                {
                    //MessageBox.ShowAndClose(this, "未找到您要查看的线路信息");
                    //return;
                }

                InitBasicInfo();
                InitRouteInfo(RouteID);

            }
        }

        #region 初始化基础信息
        /// <summary>
        /// 初始化基础信息
        /// </summary>
        private void InitBasicInfo()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo bll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = bll.GetModel(CompanyID);
            if (model != null)
            {
                this.lblCompanyName.Text = model.CompanyName;
                this.lblLicense.Text = model.License;
                this.lblAddress.Text = model.CompanyAddress;
                if (model.ContactInfo != null)
                {
                    this.lblContactName.Text = model.ContactInfo.ContactName;
                    this.lblContactTel.Text = model.ContactInfo.Tel;
                }
            }
            model = null;
            bll = null;
        }
        #endregion

        #region 初始化线路信息
        /// <summary>
        /// 初始化线路信息
        /// </summary>
        private void InitRouteInfo(string RouteID)
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            EyouSoft.Model.TourStructure.RouteBasicInfo model = bll.GetRouteInfo(RouteID);
            if (model != null)
            {
                this.Page.Title = model.RouteName;
                this.lblRouteName.Text = model.RouteName;
                this.lblTourDays.Text = model.TourDays.ToString();

                #region 报价信息
                IList<EyouSoft.Model.TourStructure.RoutePriceDetail> priceList = model.PriceDetails;
                if (priceList != null && priceList.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    foreach (EyouSoft.Model.TourStructure.RoutePriceDetail priceModel in priceList)
                    {
                        string priceName = string.Empty;
                        IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> priceStandList = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance().GetList(UserInfoModel.CompanyID);
                        str.Append("<tr>");
                        if (priceStandList != null && priceStandList.Count > 0)
                        {
                            foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand priceStandModel in priceStandList)
                            {
                                if (priceStandModel.ID == priceModel.PriceStandId)
                                {
                                    priceName = priceStandModel.PriceStandName;
                                }
                            }
                        }
                        priceStandList = null;

                        str.AppendFormat("<td width=\"110\" align=\"right\" bgcolor=\"#eeeeee\"> {0}：", priceName);
                        IList<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail> detailList = priceModel.PriceDetail;
                        if (detailList != null && detailList.Count > 0)
                        {
                            foreach (EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail detailModel in detailList)
                            {
                                if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.门市)
                                {
                                    str.Append("成人价格：</td>");
                                    str.AppendFormat("<td width=\"155\" bgcolor=\"#eeeeee\"><input name=\"txtPeoplePrice\" type=\"text\" value=\"{0}\" id=\"txtPeoplePrice\" class=\"bottow_side2\" style=\"width:45px;\" />元／人 </td>", detailModel.AdultPrice.ToString("F0"));
                                    str.Append("<td width=\"92\" align=\"right\" bgcolor=\"#eeeeee\"> 儿童价格：</td>");
                                    str.AppendFormat("<td width=\"143\" bgcolor=\"#eeeeee\"><input name=\"txtChildPrice\" type=\"text\" value=\"{0}\" id=\"txtChildPrice\" class=\"bottow_side2\" style=\"width:45px;\" />元／人</td>", detailModel.ChildrenPrice.ToString("F0"));
                                }
                                else if (detailModel.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差)
                                {
                                    str.Append("<td width=\"83\" align=\"right\" bgcolor=\"#eeeeee\">单房差：</td>");
                                    str.AppendFormat("<td width=\"163\" bgcolor=\"#eeeeee\"><input name=\"txtChildPrice\" type=\"text\" value=\"{0}\" id=\"txtChildPrice\" class=\"bottow_side2\" style=\"width:45px;\" />元／人</td>", detailModel.ChildrenPrice.ToString("F0"));
                                }
                            }
                        }
                        str.Append("</tr>");
                        detailList = null;
                    }
                    priceList = null;
                    this.ltrPriceDetail.Text = str.ToString();
                    str = null;
                }
                #endregion

                #region 行程安排
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
                {
                    if (!String.IsNullOrEmpty(model.QuickPlan))
                    {
                        this.ltrTourPlan.Text = "<tr><td align=\"left\" style=\"border-bottom:1px dashed #cccccc;\">" + model.QuickPlan + "</td></tr>";
                    }
                    else
                    {
                        this.pnlPlan.Visible = false;
                    }
                }
                else
                {
                    IList<EyouSoft.Model.TourStructure.RouteStandardPlan> standardPlanList = model.StandardPlans;
                    if (standardPlanList != null && standardPlanList.Count > 0)
                    {
                        StringBuilder strPlan = new StringBuilder();
                        foreach (EyouSoft.Model.TourStructure.RouteStandardPlan standardPlanModel in standardPlanList)
                        {
                            strPlan.Append("<tr>");
                            strPlan.Append("<td style=\"border-bottom:1px dashed #cccccc;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");
                            strPlan.Append("<tr>");
                            strPlan.AppendFormat("<td width=\"30%\" height=\"20\" align=\"left\">第{0}天&nbsp;</td>", standardPlanModel.PlanDay.ToString().ToUpper());
                            strPlan.AppendFormat("<td width=\"30%\" align=\"left\" class=\"zi\">行：{0}[{1}]</td>", standardPlanModel.Vehicle, standardPlanModel.TrafficNumber);
                            strPlan.AppendFormat("<td width=\"19%\" align=\"left\" class=\"red\">住：{0}</td>", standardPlanModel.House);
                            strPlan.AppendFormat("<td width=\"21%\" align=\"left\" class=\"lv\">餐：{0}</td>", standardPlanModel.Dinner);
                            strPlan.Append("</tr>");
                            strPlan.Append("<tr>");
                            strPlan.AppendFormat("<td colspan=\"4\" align=\"left\" class=\"padding30\">{0}</td>", StringValidate.TextToHtml(standardPlanModel.PlanContent));
                            strPlan.Append("</tr></table></td></tr>");
                        }
                        standardPlanList = null;
                        this.ltrTourPlan.Text = strPlan.ToString();
                        strPlan = null;
                    }
                    else
                    {
                        this.pnlPlan.Visible = false;
                    }
                }
                #endregion

                #region 包含项目及服务标准
                EyouSoft.Model.TourStructure.RouteServiceStandard ServiceStandard = model.ServiceStandard;
                StringBuilder strService = new StringBuilder();
                if (ServiceStandard != null)
                {
                    if (!String.IsNullOrEmpty(ServiceStandard.ResideContent))
                        strService.AppendFormat("住宿：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.ResideContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.DinnerContent))
                        strService.AppendFormat("用餐：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.DinnerContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.SightContent))
                        strService.AppendFormat("景点：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.SightContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.CarContent))
                        strService.AppendFormat("用车：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.CarContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.GuideContent))
                        strService.AppendFormat("导游：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.GuideContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.TrafficContent))
                        strService.AppendFormat("往返交通：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.TrafficContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.IncludeOtherContent))
                        strService.AppendFormat("其它：{0}；<br/>", StringValidate.TextToHtml(ServiceStandard.IncludeOtherContent));
                    this.ltrContainContent.Text = strService.ToString();
                    this.ltrStandardService.Text = StringValidate.TextToHtml(ServiceStandard.NotContainService);
                    this.ltrRemark.Text = StringValidate.TextToHtml(ServiceStandard.SpeciallyNotice);
                }
                if (strService.ToString() == string.Empty && (ServiceStandard.NotContainService == string.Empty || ServiceStandard.NotContainService == null) && (ServiceStandard.SpeciallyNotice == string.Empty || ServiceStandard.SpeciallyNotice == null))
                {
                    this.pnlServeice.Visible = false;
                }
                ServiceStandard = null;
                #endregion
            }
        }
        #endregion

        protected void btnWordPrint_Click(object sender, EventArgs e)
        {
            string printHtml = Request.Form["txtPrintHTML"];
            string saveFileName = HttpUtility.UrlEncode("行程单.doc");
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
                    table {{border-collapse:collapse;width:710px;}}
                    td {{font-size: 12px; line-height:18px;color: #000000;  }}	
                    .headertitle {{font-family:""黑体""; font-size:25px; line-height:120%; font-weight:bold;}}
                    </style>
                </head>
                <body>
                    <div id=""divPrintPreview"">{0}</div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, printHtml));

            Response.End();
        }
    }
}
