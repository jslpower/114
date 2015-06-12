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

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 出团通知书
    /// 创建者：luofx 时间：2010-7-12
    /// </summary>
    public partial class TeamNotice : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string CompanyName = string.Empty;
        protected string TourContact = string.Empty;//联系人
        protected string TourContactTel = string.Empty;//联系人电话
        protected string License = string.Empty;//许可证号
        protected string CompanyAddress = string.Empty;//许可证号
        protected string RouteName = string.Empty;
        protected int TourDays = 0;

        protected string QuickPlanContent = string.Empty;
        protected string ResideContent = string.Empty;//住宿
        protected string SpeciallyNotice = string.Empty;//备注
        protected string GuideContent = string.Empty;//导游
        protected string DinnerContent = string.Empty;//用餐
        protected string TrafficContent = string.Empty;//往返交通?大交通 
        protected string CarContent = string.Empty;//景交?用车
        protected string SightContent = string.Empty; //景点
        protected string IncludeOtherContent = string.Empty; //包含项目:其他说明
        protected string NotContainService = string.Empty;//不包含项目
        //protected string TourCode = string.Empty;
        private string TourID = string.Empty;
        protected string CompanyID = string.Empty;
        private string OrderID = string.Empty;
        protected DateTime LeaveDate = DateTime.Now;//
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Request.Url.ToString(), "对不起，你还未登录不能查看出团通知书，请登录！");
                return;
            }
            OrderID = Utils.GetQueryStringValue("OrderID");
            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                TourID = Utils.InputText(Request.QueryString["TourID"]);
            }
            //if (Request.QueryString["TourCode"] != null && !string.IsNullOrEmpty(Request.QueryString["TourCode"]))
            //{
            //    TourCode = Utils.InputText(Request.QueryString["TourCode"]);
            //}
            if (!IsPostBack)
            {
                imgbtnToWord.ImageUrl = this.ImageServerUrl + "/images/dcprint.gif";
                InitPage();
            }
            this.btnToWord.Attributes.Add("onclick", "getHtml();");
            imgbtnToWord.Attributes.Add("onclick", "getHtml();");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                #region 获取所属公司信息
                if (this.IsLogin)
                {
                    CompanyID = this.SiteUserInfo.CompanyID;
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
                    EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                    EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(model.CompanyID);
                    CompanyName = companyInfoModel.CompanyName;
                    CompanyAddress = companyInfoModel.CompanyAddress;
                    License = companyInfoModel.License;
                    TourContactTel = companyInfoModel.ContactInfo.Tel;
                    TourContact = companyInfoModel.ContactInfo.ContactName;
                    ICompanyInfobll = null;
                    companyInfoModel = null;
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
                    localInfo.Visible = false;
                }
                #endregion

                #region 团队相关信息
                TourDays = model.TourDays;
                RouteName = model.RouteName;
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)//标准发布
                {
                    if (!string.IsNullOrEmpty(model.LeaveTraffic))
                    {
                        ltrTraffic.Text =EyouSoft.Common.Function.StringValidate.TextToHtml(model.LeaveTraffic);
                    }
                    else
                    {
                        Tr_Traffic.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(model.CollectionContect))
                    {
                        ltrCollectionContect.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CollectionContect);
                    }
                    else
                    {
                        CollectionContect.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(model.MeetTourContect))
                    {
                        ltrMeetTourContect.Text =EyouSoft.Common.Function.StringValidate.TextToHtml( model.MeetTourContect);
                    }
                    else
                    {
                        MeetTourContect.Visible = false;
                    }

                    ResideContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.ResideContent);//住宿
                    DinnerContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.DinnerContent);//用餐
                    SightContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.SightContent);//景点
                    CarContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.CarContent);//用车
                    GuideContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.GuideContent);//导游
                    TrafficContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.TrafficContent);//往返交通
                    IncludeOtherContent =EyouSoft.Common.Function.StringValidate.TextToHtml( model.ServiceStandard.IncludeOtherContent);//其它
                    NotContainService = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.IncludeOtherContent);//其它说明
                    //备注
                    SpeciallyNotice = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SpeciallyNotice);

                    rptTeamNotice.DataSource = model.StandardPlan;
                    rptTeamNotice.DataBind();
                }
                else//快速发布
                {
                    pnlQuickPlan.Visible = true;
                    Tr_Traffic.Visible = false;
                    CollectionContect.Visible = false;
                    MeetTourContect.Visible = false;
                    pnlNotQuickPlan.Visible = false;
                    QuickPlanContent = model.QuickPlan;
                }
                #region 盖章信息
                //if(this.SiteUserInfo.CompanyID==model.se)
                if (!string.IsNullOrEmpty(OrderID))
                {
                    string BuyCompanyID = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderModel(OrderID).BuyCompanyID;
                    if (this.SiteUserInfo.CompanyID == BuyCompanyID)
                    {
                        string imgPath = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().GetGroupAdviceSignet(OrderID);
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            imgBuyCompany.Visible = true;
                            imgBuyCompany.ImageUrl = Domain.FileSystem + imgPath;
                            btnCancelStamp.Visible = true;
                            btnToStamp.Visible = false;
                        }
                    }
                    else
                    {
                        this.btnCancelStamp.Visible = false;
                        this.btnToStamp.Visible = false;
                    }
                }
                else
                {//无订单号传入时，盖章隐藏
                    this.btnCancelStamp.Visible = false;
                    this.btnToStamp.Visible = false;
                }
                //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.系统设置_盖章) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    this.btnCancelStamp.Visible = false;
                    this.btnToStamp.Visible = false;
                }
                #endregion
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
            string printHtml = Request.Form["rptPrintHTML"];
            string printRemark = Request.Form["rptRemark"];
            string saveFileName = HttpUtility.UrlEncode("出团通知书.doc");
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
                    <div>
                        {0}                            
                    </div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, printHtml, printRemark));
            Response.End();
        }

        protected void rptTeamNotice_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
        /// <summary>
        /// 盖章 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToStamp_Click(object sender, EventArgs e)
        {
            CompanyID = Utils.GetFormValue("hidCompanyID");
            string imgPath = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(CompanyID).CompanySignet;
            if (string.IsNullOrEmpty(imgPath))
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('对不起，你的公司不前还没有上传公章！'); location.href=location.href;");
                return;
            }

            bool isTrue = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().GroupAdviceToStamp(OrderID, imgPath);
            if (isTrue)
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('盖章成功！'); location.href=location.href;");
            }
        }
        /// <summary>
        /// 取消盖章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelStamp_Click(object sender, EventArgs e)
        {
            bool isTrue = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().CancelGroupAdviceStamp(OrderID);
            if (isTrue)
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('取消盖章成功！'); location.href=location.href;");
            }
        }
    }
}
