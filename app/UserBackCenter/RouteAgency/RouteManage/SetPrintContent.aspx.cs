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

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 线路行程单打印
    /// 罗丽娥   2010-07-02
    /// </summary>
    public partial class SetPrintContent : EyouSoft.Common.Control.FrontPage
    {
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        private string CompanyID = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
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
                string RouteID = string.Empty;
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
                    this.lblContactFax.Text = model.ContactInfo.Fax;
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

                        str.Append("<tr>");
                        str.AppendFormat("<td width=\"120\" align=\"right\" bgcolor=\"#eeeeee\"> {0}：", priceName);
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
                            str.Append("<td width=\"26\" bgcolor=\"#eeeeee\"><img src=\"" + ImageServerPath + "/images/deleiption.gif\" width=\"13\" height=\"13\" onclick=\"printConfig.deletePriceInfo(this);\" /></td>");
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
                        this.plhItinerary.Visible = false;
                        this.plhPlan.Visible = true;
                        this.pnlServeice.Visible = false;
                        this.pnlRemark.Visible = false;
                        this.ltrTourPlan1.Text = "<tr><td align=\"left\" bgcolor=\"#E8FFFF\" style=\"line-height: 15px;\">" + model.QuickPlan + "</td></tr>";
                    }
                    else
                    {
                        //this.pnlPlan.Visible = false;
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
                            strPlan.Append("<td width=\"61\" rowspan=\"2\" align=\"center\" bgcolor=\"#E8FFFF\" style=\"line-height: 15px;\">");
                            strPlan.AppendFormat("<strong>第{0}天&nbsp;<br /></strong><br /></td>", standardPlanModel.PlanDay.ToString().ToUpper());
                            strPlan.AppendFormat("<td width=\"649\" align=\"left\" valign=\"bottom\" bgcolor=\"#E8FFFF\"><img src=\"" + ImageServerPath + "/images/xing.gif\" width=\"8\" height=\"11\" />行：{0},{1}[{2}]", standardPlanModel.PlanInterval, standardPlanModel.Vehicle, standardPlanModel.TrafficNumber);
                            strPlan.AppendFormat("<img src=\"" + ImageServerPath + "/images/zhu.gif\" width=\"15\" height=\"11\" />住：{0}", standardPlanModel.House);
                            strPlan.AppendFormat("<img src=\"" + ImageServerPath + "/images/chi.gif\" width=\"10\" height=\"11\" />餐：{0}</td>", standardPlanModel.Dinner);
                            strPlan.Append("</tr>");
                            strPlan.Append("<tr>");
                            strPlan.AppendFormat("<td align=\"left\" class=\"padding30\" style=\"height:25px;\">{0}</td>", StringValidate.TextToHtml(standardPlanModel.PlanContent));
                            strPlan.Append("</tr>");
                        }
                        standardPlanList = null;
                        this.ltrTourPlan.Text = strPlan.ToString();
                        strPlan = null;
                    }
                    else
                    {
                        //this.pnlPlan.Visible = false;
                    }
                }
                #endregion

                #region 包含项目及服务标准
                EyouSoft.Model.TourStructure.RouteServiceStandard ServiceStandard = model.ServiceStandard;
                StringBuilder strService = new StringBuilder();
                if (ServiceStandard != null)
                {
                    if (!String.IsNullOrEmpty(ServiceStandard.ResideContent))
                        strService.AppendFormat("住宿：{0}；", StringValidate.TextToHtml(ServiceStandard.ResideContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.DinnerContent))
                        strService.AppendFormat("用餐：{0}；", StringValidate.TextToHtml(ServiceStandard.DinnerContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.SightContent))
                        strService.AppendFormat("景点：{0}；", StringValidate.TextToHtml(ServiceStandard.SightContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.CarContent))
                        strService.AppendFormat("用车：{0}；", StringValidate.TextToHtml(ServiceStandard.CarContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.GuideContent))
                        strService.AppendFormat("导游：{0}；>", StringValidate.TextToHtml(ServiceStandard.GuideContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.TrafficContent))
                        strService.AppendFormat("往返交通：{0}；", StringValidate.TextToHtml(ServiceStandard.TrafficContent));
                    if (!String.IsNullOrEmpty(ServiceStandard.IncludeOtherContent))
                        strService.AppendFormat("其它：{0}；", StringValidate.TextToHtml(ServiceStandard.IncludeOtherContent));
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
            model = null;
            bll = null;
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

            Response.Write(string.Format(wordTemplate,printHtml));

            Response.End();
        }
    }
}
