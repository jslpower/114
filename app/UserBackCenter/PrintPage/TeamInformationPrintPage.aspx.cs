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

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 团队行程信息打印单
    /// 创建者：luofx 时间：2010-7-10
    /// ----------------------------------
    /// 修改时间：2011-11-20 修改内容：WORD导出生成文件
    /// </summary>
    public partial class TeamInformationPrintPage : EyouSoft.Common.Control.BasePage
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
        protected string OrderIDTourLinkParam = string.Empty;

        private string TemplateTourID = string.Empty;
        protected DateTime LeaveDate = DateTime.Now;//
        protected DateTime BackDate = DateTime.Now;//

        protected string ResideContent = string.Empty;//住宿
        protected string GuideContent = string.Empty;//导游
        protected string DinnerContent = string.Empty;//用餐
        protected string TrafficContent = string.Empty;//往返交通?大交通 
        protected string CarContent = string.Empty;//景交?用车
        protected string SightContent = string.Empty; //景点
        protected string IncludeOtherContent = string.Empty; //包含项目:其他说明

        protected string NotContainService = string.Empty;//其他说明
        protected string SpeciallyNotice = string.Empty;//备注
        /// <summary>
        /// 是否含有地接社
        /// </summary>
        protected bool isNotLocalCompany = true;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {                        
            TourID = Utils.GetQueryStringValue("TourID");
            if (!string.IsNullOrEmpty(Request.QueryString["OrderID"])) {
                OrderIDTourLinkParam = "&OrderID=" + Request.QueryString["OrderID"];
            }
            if (!IsPostBack)
            {
                InitPage();
            }
            this.lkbWord.Attributes.Add("onclick", "getHtml();");
            this.lkbWord2.Attributes.Add("onclick", "getHtml();");
        }
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                TemplateTourID = model.ParentTourID;
                #region 新增浏览记录
                string currentCompanyId = string.Empty;
                if (this.IsLogin)
                {
                    currentCompanyId = SiteUserInfo.CompanyID;
                }
                if (model.CompanyID != currentCompanyId && !string.IsNullOrEmpty(currentCompanyId))
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo TourModel = new EyouSoft.Model.TourStructure.TourVisitInfo();
                    TourModel.TourId = TourID;
                    TourModel.VisitedCompanyId = model.CompanyID;
                    TourModel.VisitedCompanyName = model.CompanyName;
                    TourModel.VisitedTime = DateTime.Today;
                    TourModel.VisitTourCode = model.TourNo;
                    TourModel.VisitTourRouteName = model.RouteName;
                    if (IsLogin)
                    {
                        TourModel.ClientCompanyId = SiteUserInfo.CompanyID;
                        TourModel.ClientCompanyName = SiteUserInfo.CompanyName;
                        TourModel.ClientUserContactMobile = SiteUserInfo.ContactInfo.Mobile;
                        TourModel.ClientUserContactName = SiteUserInfo.ContactInfo.ContactName;
                        TourModel.ClientUserContactTelephone = SiteUserInfo.ContactInfo.Tel;
                        TourModel.ClientUserId = SiteUserInfo.ID;
                        TourModel.ClinetUserContactQQ = SiteUserInfo.ContactInfo.QQ;
                    }
                    else
                    {
                        TourModel.ClientCompanyId = "";
                    }
                    EyouSoft.BLL.TourStructure.Tour.CreateInstance().InsertTourVisitedInfo(TourModel);
                    TourModel = null;
                }
                else if (!IsLogin)
                {
                    EyouSoft.BLL.TourStructure.Tour.CreateInstance().IncreaseClicks(TourID);
                }
                #endregion

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
                rptTourLocalityInfo.DataSource = model.LocalTravelAgency;
                rptTourLocalityInfo.DataBind();
                if (rptTourLocalityInfo.Items.Count < 1)
                {
                    isNotLocalCompany = false;
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
                    if (!string.IsNullOrEmpty(model.LeaveTraffic))
                    {
                        ltrTraffic.Text = EyouSoft.Common.Function.StringValidate.TextToHtml( model.LeaveTraffic);
                    }
                    else
                    {
                        Tr_Traffic.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(model.MeetTourContect))
                    {
                        ltrMeetTourContect.Text = EyouSoft.Common.Function.StringValidate.TextToHtml( model.MeetTourContect);
                    }
                    else
                    {
                        Tr_MeetTourContect.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(model.CollectionContect))
                    {
                        ltrCollectionContect.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CollectionContect);
                    }
                    else
                    {
                        Tr_CollectionContect.Visible = false;
                    }

                    //行程信息及相关
                    rptStandardPlan.DataSource = model.StandardPlan;
                    rptStandardPlan.DataBind();
                    //服务标准及说明                    
                    if (model.ServiceStandard != null)
                    {
                        ResideContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.ResideContent);//住宿
                        DinnerContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.DinnerContent);//用餐
                        SightContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SightContent);//景点
                        CarContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.CarContent);//用车
                        GuideContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.GuideContent);//导游
                        TrafficContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.TrafficContent);//往返交通
                        IncludeOtherContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.IncludeOtherContent);//其它                        
                        if (!string.IsNullOrEmpty(model.ServiceStandard.NotContainService))
                        {
                            NotContainService = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.NotContainService);
                        }
                        else
                        {
                            Tr_NotContainService.Visible = false;
                        }
                        if (!string.IsNullOrEmpty(model.ServiceStandard.SpeciallyNotice))
                        {
                            SpeciallyNotice = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SpeciallyNotice);
                        }
                        else
                        {
                            Tr_SpeciallyNotice.Visible = false;
                        }
                    }
                }
                else//快速发布
                {
                    pnlServiceStandard.Visible = false;
                    Tr_CollectionContect.Visible = false;
                    Tr_MeetTourContect.Visible = false;
                    Tr_Traffic.Visible = false;
                    QuickPlanContent = model.QuickPlan;
                }
                #endregion

            }
            Ibll = null;
            model = null;
        }
        /// <summary>
        /// Word格式打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWord_Click(object sender, EventArgs e)
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
                                    PrintMiddleHtml += "<tr><td width=\"99\" align=\"left\" bgcolor=\"#eeeeee\">" + item.PriceStandName + "：</td>";
                                    PrintMiddleHtml += "<td width=\"155\" bgcolor=\"#eeeeee\">成人价格：<span>" + childItem.AdultPrice + "</span>元／人";
                                    PrintMiddleHtml += " <td width=\"92\" align=\"right\" bgcolor=\"#eeeeee\">儿童价格： </td>";
                                    PrintMiddleHtml += "<td width=\"143\" bgcolor=\"#eeeeee\">" + childItem.ChildrenPrice + "元／人</td>";
                                    break;
                                case EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差:
                                    PrintMiddleHtml += "<td width=\"83\" align=\"right\" bgcolor=\"#eeeeee\">单房差：</td>";
                                    PrintMiddleHtml += "<td width=\"163\" bgcolor=\"#eeeeee\">" + childItem.AdultPrice + "元／人</td></tr>";
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
                    table {{border-collapse:collapse;width:710px;}}
                    td {{font-size: 12px; line-height:18px;color: #000000;  }}	
                    .headertitle {{font-family:""黑体""; font-size:25px; line-height:120%; font-weight:bold;}}
                    </style>
                </head>
                <body>
                    <div id=""divPrintPreview"">{0}{1}{2}</div>
                </body>
                </html>";

            string file = string.Format(wordTemplate, PrintTopHtml, PrintMiddleHtml, PrintFooterHtml);
            string filename = "/doctmp/" + DateTime.Now.ToFileTime().ToString() + ".doc";

            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath(filename));
            sw.Write(file);
            sw.Close();

            Response.Redirect(filename);
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
    /// <summary>
    /// 转换价格等级数据，方便处理
    /// </summary>
    public class CPriceDetail
    {
        /// <summary>
        /// 价格等级名称
        /// </summary>
        public string PriceStandName { get; set; }
        /// <summary>
        /// 成人价
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价
        /// </summary>
        public decimal ChildrenPrice { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal SingleRoom { get; set; }
        /// <summary>
        /// 0：同行，1：门市
        /// </summary>
        public int CustomerLevelType { get; set; }
    }
}
