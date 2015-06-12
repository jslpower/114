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
    public partial class TourPrintPage : System.Web.UI.Page
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected string CompanyName = string.Empty;
        protected string TourContact = string.Empty;//联系人
        protected string TourContactTel = string.Empty;//联系人电话
        protected string License = string.Empty;//许可证号
        protected string TourContactFax = string.Empty;
        protected string CompanyAddress = string.Empty;//许可证号
        protected string RouteName = string.Empty;//线路名称
        protected string QuickPlanContent = string.Empty;
        protected string TourCode = string.Empty;
        protected int TourDays = 0;
        protected string TourID = string.Empty;

        private string TemplateTourID = string.Empty;
        protected DateTime LeaveDate = DateTime.Now;//
        protected DateTime BackDate = DateTime.Now;//

        protected string DoorPrice = string.Empty;//门票 
        protected string SiglePeplePrice = string.Empty;//报价
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //TourID = Utils.GetQueryStringValue("TourID");
            //if (!IsPostBack)
            //{
            //    InitPage();
            //}
        }
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                TemplateTourID = model.ParentTourID;                
                #region 获取所属公司信息
                EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(model.CompanyID);
                CompanyName = companyInfoModel.CompanyName;
                CompanyAddress = companyInfoModel.CompanyAddress;
                License = companyInfoModel.License;
                TourContactFax = companyInfoModel.ContactInfo.Fax;
                ICompanyInfobll = null;
                companyInfoModel = null;
                #endregion

                #region 获取地接社信息
                if (model.LocalTravelAgency != null && model.LocalTravelAgency.Count > 0)
                {
                    //EyouSoft.Model.TourStructure.TourLocalityInfo localMode = ((List<EyouSoft.Model.TourStructure.TourLocalityInfo>)model.LocalTravelAgency).First(LocalAgency =>
                    //{
                    //    return LocalAgency.TourId == TourID;
                    //});
                    //if (localMode != null)
                    //{
                    ltrLocalCompanyName.Text = model.LocalTravelAgency[0].LocalCompanyName;
                    ltrLicenseNumber.Text = model.LocalTravelAgency[0].LicenseNumber;
                    //}
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
                if (((int)model.ReleaseType) == 0)//标注发布
                {
                    ltrTraffic.Text = model.LeaveTraffic;
                    ltrMeetTourContect.Text = model.MeetTourContect;
                    ltrCollectionContect.Text = model.CollectionContect;
                    //行程信息及相关
                    rptStandardPlan.DataSource = model.StandardPlan;
                    rptStandardPlan.DataBind();
                    //服务标准及说明
                    List<EyouSoft.Model.TourStructure.ServiceStandard> serviceList = new List<EyouSoft.Model.TourStructure.ServiceStandard>();
                    if (model.ServiceStandard != null)
                    {
                        serviceList.Add(model.ServiceStandard);
                        this.rptServiceStandard.DataSource = serviceList;
                        this.rptServiceStandard.DataBind();
                    }
                }
                else//快速发布
                {
                    pnlServiceStandard.Visible = false;
                    Tr_CollectionContect.Visible = false;
                    
                    Tr_Traffic.Visible = false;
                    QuickPlanContent = model.QuickPlan;
                }
                #endregion
            }
            Ibll = null;
            model = null;
        }
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
