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
    /// 团队散客的确认单
    /// 创建者：luofx 时间：2010-7-8
    /// </summary>
    public partial class TourConfirmation : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected string RouteName = string.Empty;
        protected int TourDays = 0;
        protected string AdultPrice = "0";
        protected string ChildrenPrice = "0";
        protected string SumPrice = "0";
        protected int AdultNum = 0;
        protected int ChildrenNum = 0;


        protected string BuyCompanyName = string.Empty;
        protected string BuyCompanyConnectName = string.Empty;
        protected string BuyCompanyTel = string.Empty;
        protected string BuyCompanyFax = string.Empty;

        private string SellCompanyID = string.Empty;
        protected string SellCompanyName = string.Empty;
        protected string SellCompanyConnectName = string.Empty;
        protected string SellCompanyTel = string.Empty;
        protected string SellCompanyFax = string.Empty;

        protected string ComfirmTime = string.Empty;
        protected string QuickPlanContent = string.Empty;
        protected string Remark = string.Empty; //备注

        protected string ResideContent = string.Empty;//住宿
        protected string SpeciallyNotice = string.Empty;//温馨提醒?友情提示
        protected string GuideContent = string.Empty;//导游
        protected string DinnerContent = string.Empty;//用餐
        protected string TrafficContent = string.Empty;//往返交通?大交通 
        protected string CarContent = string.Empty;//景交?用车
        protected string SightContent = string.Empty; //景点
        protected string IncludeOtherContent = string.Empty; //包含项目:其他说明
        protected string NotContainService = string.Empty;//不包含项目

        protected string DoorPrice = string.Empty;//门票 
        protected string SiglePeplePrice = string.Empty;//报价

        private string TourID = string.Empty;
        private string BuyCompanyID = string.Empty;
        private string OrderID = string.Empty;
        /// <summary>
        /// 判断是专线还是组团:默认组团
        /// </summary>
        private bool isTourCompany = true;
        /// <summary>
        /// 当前公司的盖章路径
        /// </summary>
        protected string singnetPath = string.Empty;
        protected string TourCode = string.Empty;
        protected DateTime LeaveDate = DateTime.Now;//
        protected string CompanyID = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Request.Url.ToString(), "对不起，你还未登录或登录过期，不能查看团队确认单，请登录！");
                return;
            }
            OrderID = Utils.GetQueryStringValue("OrderID");
            TourID = Utils.GetQueryStringValue("TourID");
            //1:表示专线端进入;其他表示组团端进入
            isTourCompany = Utils.GetQueryStringValue("type") == "1" ? false : true;
            if (Request.QueryString["TourCode"] != null && !string.IsNullOrEmpty(Request.QueryString["TourCode"]))
            {
                TourCode = Utils.InputText(Request.QueryString["TourCode"]);
            }
            if (!IsPostBack)
            {
                imgbtnToWord.ImageUrl = ImageServerPath + "/images/daochuword.gif";
                InitPage(true);
            }
            this.btnWord.Attributes.Add("onclick", "getHtml();");
            this.btnToWord.Attributes.Add("onclick", "getHtml();");
            imgbtnToWord.Attributes.Add("onclick", "getHtml();");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage(bool isShow)
        {
            #region 根据订单号获取相关信息
            //EyouSoft.Model.TourStructure.TourOrder
            EyouSoft.IBLL.TourStructure.ITourOrder IOrderbll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            EyouSoft.Model.TourStructure.TourOrder OrderModel = IOrderbll.GetOrderModel(OrderID);
            if (OrderModel != null)
            {
                BuyCompanyID = OrderModel.BuyCompanyID;
                SellCompanyID = OrderModel.CompanyID;
                RouteName = OrderModel.RouteName;

                if (!string.IsNullOrEmpty(OrderModel.LeaveTraffic))
                {
                    ltrTraffic.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(OrderModel.LeaveTraffic);
                }
                else
                {
                    Tr_Traffic.Visible = false;
                }

                ComfirmTime = DateTime.Now.ToShortDateString();
                AdultPrice = OrderModel.PersonalPrice.ToString("F2");
                this.ChildrenPrice = OrderModel.ChildPrice.ToString("F2");
                SumPrice = OrderModel.SumPrice.ToString("F2");
                AdultNum = OrderModel.AdultNumber;
                ChildrenNum = OrderModel.ChildNumber;
            }
            else
            {
                return;
            }
            #endregion

            #region 游客信息
            if (OrderModel.TourOrderCustomer != null && OrderModel.TourOrderCustomer.Count > 0)
            {
                IList<EyouSoft.Model.TourStructure.TourOrderCustomer> OrderCustomerList = OrderModel.TourOrderCustomer;
                this.rptCustomers.DataSource = OrderCustomerList;
                this.rptCustomers.DataBind();
                if (rptCustomers.Items.Count < 1)
                {
                    this.tblCustomers.Visible = false;
                }
            }
            #endregion

            #region 专线公司信息
            EyouSoft.Model.CompanyStructure.CompanyInfo SellCompany = new EyouSoft.Model.CompanyStructure.CompanyInfo();
            SellCompany = this.GetCompanyInfo(SellCompanyID);
            if (SellCompany != null)
            {
                txtSellCompanyName.Value = SellCompany.CompanyName;
                txtSellConnectName.Value = SellCompany.ContactInfo.ContactName;
                txtSellConnectFax.Value = SellCompany.ContactInfo.Fax;
                txtSellConnectTel.Value = SellCompany.ContactInfo.Tel;
            }
            #endregion

            #region 组团公司信息
            EyouSoft.Model.CompanyStructure.CompanyInfo BuyCompany = new EyouSoft.Model.CompanyStructure.CompanyInfo();
            BuyCompany = this.GetCompanyInfo(BuyCompanyID);
            if (BuyCompany != null)
            {
                txtBuyCompanyName.Value = BuyCompany.CompanyName;
                txtBuyConnectName.Value = BuyCompany.ContactInfo.ContactName;
                txtBuyConnectFax.Value = BuyCompany.ContactInfo.Fax;
                txtBuyConnectTel.Value = BuyCompany.ContactInfo.Tel;
            }
            #endregion

            if (this.IsLogin)
            {
                CompanyID = this.SiteUserInfo.CompanyID;
                ltrSellCompanyName.Text = this.SiteUserInfo.CompanyName;
            }

            #region 银行帐号信息
            EyouSoft.IBLL.CompanyStructure.IBankAccount IbankBll = EyouSoft.BLL.CompanyStructure.BankAccount.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.BankAccount> bankList = IbankBll.GetList(SellCompanyID);
            if (bankList != null && bankList.Count > 0)
            {
                EyouSoft.Model.CompanyStructure.BankAccount CompanyBankAccount = bankList.FirstOrDefault(BankItem =>
                {
                    return BankItem.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司;
                });
                if (CompanyBankAccount != null)
                {
                    ltrSellCompanyName.Text = CompanyBankAccount.BankAccountName;
                    ltrBuyComapnyBankName.Text = CompanyBankAccount.BankName;
                    ltrBuyComapnyBankNo.Text = CompanyBankAccount.AccountNumber;
                }

                rptBankAccount.DataSource = ((List<EyouSoft.Model.CompanyStructure.BankAccount>)bankList).FindAll(BankItem =>
                {
                    return BankItem.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.个人;
                });
                rptBankAccount.DataBind();
                IbankBll = null;
                bankList = null;
                CompanyBankAccount = null;
            }
            #endregion

            #region 团队信息
            EyouSoft.IBLL.TourStructure.ITour Ibll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.Model.TourStructure.TourInfo model = Ibll.GetTourInfo(TourID);
            if (model != null)
            {
                RouteName = model.RouteName;
                TourDays = model.TourDays;
                LeaveDate = model.LeaveDate;
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)//标准发布
                {
                    if (!string.IsNullOrEmpty(model.MeetTourContect))
                    {
                        ltrMeetTourContect.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.MeetTourContect);
                    }
                    else
                    {
                        tr_MeetTourContect.Visible = false;
                    }
                    if (!string.IsNullOrEmpty(model.CollectionContect))
                    {
                        ltrCollectionContect.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.CollectionContect);
                    }
                    else
                    {
                        tr_CollectionContect.Visible = false;
                    }
                    ResideContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.ResideContent);//住宿
                    DinnerContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.DinnerContent);//用餐
                    SightContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SightContent);//景点
                    CarContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.CarContent);//用车
                    GuideContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.GuideContent);//导游
                    TrafficContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.TrafficContent);//往返交通
                    IncludeOtherContent = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.IncludeOtherContent);//其它
                    NotContainService = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.NotContainService);//其它说明
                    //备注
                    SpeciallyNotice = EyouSoft.Common.Function.StringValidate.TextToHtml(model.ServiceStandard.SpeciallyNotice);
                    //行程信息
                    rptStandardPlan.DataSource = model.StandardPlan;
                    rptStandardPlan.DataBind();
                }
                else//快速发布
                {
                    tr_MeetTourContect.Visible = false;
                    tr_CollectionContect.Visible = false;
                    pnlQuickPlan.Visible = true;
                    pnlNotQuickPlan.Visible = false;
                    QuickPlanContent = model.QuickPlan;
                }

            }
            Ibll = null;
            model = null;
            IOrderbll = null;
            OrderModel = null;
            #endregion

            #region 公司盖章信息
            if (!string.IsNullOrEmpty(OrderID))
            {
                //判断是否属于该订单的专线商或组团社
                if (this.SiteUserInfo.CompanyID == BuyCompanyID || this.SiteUserInfo.CompanyID == SellCompanyID)
                {
                    string[] imgPath = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().GetConfirmationSignet(OrderID);
                    if (imgPath.Length > 0)
                    {
                        imgBuyCompany.ImageUrl = Domain.FileSystem + imgPath[0];
                        if (!string.IsNullOrEmpty(imgPath[0])) imgBuyCompany.Visible = true;
                        if (!string.IsNullOrEmpty(imgPath[0]) && isTourCompany)
                        {
                            this.btnCancelStamp.Visible = true;
                            this.btnToStamp.Visible = false;
                        }
                        if (imgPath.Length > 1)
                        {
                            imgSellCompany.ImageUrl = Domain.FileSystem + imgPath[1];
                            if (!string.IsNullOrEmpty(imgPath[1])) imgSellCompany.Visible = true;
                            if (!string.IsNullOrEmpty(imgPath[1]) && !isTourCompany)
                            {
                                this.btnCancelStamp.Visible = true;
                                this.btnToStamp.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    this.btnCancelStamp.Visible = false;
                    this.btnToStamp.Visible = false;
                }
            }
            else
            {
                this.btnCancelStamp.Visible = false;
                this.btnToStamp.Visible = false;
            }
            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.系统设置_盖章)) 
            //{
            this.btnCancelStamp.Visible = false;
            this.btnToStamp.Visible = false;
            //}
            if (!isShow)
            {
                imgSellCompany.Visible = false;
                imgBuyCompany.Visible = false;
            }
            #endregion
            IOrderbll = null;
            OrderModel = null;
            SellCompany = null;
            BuyCompany = null;
        }
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns></returns>
        private EyouSoft.Model.CompanyStructure.CompanyInfo GetCompanyInfo(string CompanyID)
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(CompanyID);
            ICompanyInfobll = null;
            return companyInfoModel;
        }
        /// <summary>
        /// Word格式打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWord_Click(object sender, EventArgs e)
        {
            string printHtml = Request.Form["PrintHTML"];
            string saveFileName = HttpUtility.UrlEncode("团队确认单.doc");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            string wordCompanyInfo = @"<table width=""696"" border=""0"" id=""tblShowTop"" align=""center"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td align=""left"" width=""60"">
                                    <strong>接收人</strong>
                                </td>
                                <td  sytle=""border-bottom:1px solid #000000;"">
                                    {0}
                                </td>
                                <td  width=""60"">
                                    <strong>姓名</strong></td>
                                <td>
                                   <label sytle=""border:1px solid #000000;"">{1}</label>
                                </td>
                                <td width=""60"">
                                    <strong>传真</strong></td>
                                <td>
                                   <label sytle=""border: 1px solid #000000;"">{2}</label>
                                </td>
                                <td width=""60"">
                                    <strong>电话</strong></td>
                                <td>
                                   {3}
                                </td>
                            </tr>
                            <tr>
                                <td align=""left"">
                                    <strong>发送人</strong></td>
                                <td>
                                   <label sytle=""border: 0px solid black;"">{4}</label>
                                </td>
                                <td>
                                    <strong>姓名</strong></td>
                                <td>
                                   <label sytle=""border: 0px solid black;"">{5}</label>
                                </td>
                                <td>
                                    <strong>传真</strong></td>
                                <td>
                                   <label sytle=""border: 0px solid black;"">{6}</label>
                                </td>
                                <td>
                                    <strong>电话</strong></td>
                                <td>
                                   <label sytle=""border: 0px solid black;"">{7}</label>
                                </td>
                            </tr>
                        </table>";
            wordCompanyInfo = string.Format(wordCompanyInfo, Request.Form[txtBuyCompanyName.UniqueID], Request.Form[txtBuyConnectName.UniqueID],
               Request.Form[this.txtBuyConnectFax.UniqueID], Request.Form[this.txtBuyConnectTel.UniqueID],
               Request.Form[this.txtSellCompanyName.UniqueID], Request.Form[txtSellConnectName.UniqueID], Request.Form[this.txtSellConnectFax.UniqueID], Request.Form[txtSellConnectTel.UniqueID]);
            string wordFooter = @" <table width=""696"" border=""0"" align=""center"" cellpadding=""15"" cellspacing=""0"">
                                        <tr>
                                            <td width=""346"">
                                                组团社盖章：{0}                       
                                                <br />
                                                联系电话：{1}                        
                                                <br />
                                                确认日期： {2}                      
                                            </td>
                                            <td width=""350"">
                                                专线商盖章：{3}                       
                                                <br />
                                                联系电话：{4}                       
                                                <br />
                                                确认日期：{5}                       
                                            </td>
                                        </tr>
                                    </table>";
            wordFooter = string.Format(wordFooter, Request.Form["CompanyName1"], Request.Form["CompanyTel1"], Request.Form["CompanyDate1"]
                , Request.Form["CompanyName2"], Request.Form["CompanyTel2"], Request.Form["CompanyDate2"]);
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
                    <div style='width:696px; margin-left: auto; margin-right: auto;'>
                        {2}
                        {0}
                        {1} 
                        {3}                           
                    </div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, wordCompanyInfo, printHtml, Request.Form["TourCodeHtml"], wordFooter));
            Response.End();
        }

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
        /// <summary>
        /// 盖章 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToStamp_Click(object sender, EventArgs e)
        {
            int singnetType = 2;
            CompanyID = Utils.GetFormValue("hidCompanyID");
            string imgPath = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(CompanyID).CompanySignet;
            if (isTourCompany)
            {
                singnetType = 1;
            }

            if (string.IsNullOrEmpty(imgPath))
            {
                InitPage(false);
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('对不起，你的公司目前还没有上传公章！'); location.href=location.href;");
                return;
            }

            bool isTrue = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().ConfirmationToStamp(OrderID, imgPath, singnetType);
            if (isTrue)
            {
                InitPage(false);
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
            int singnetType = 2;
            if (isTourCompany)
            {
                singnetType = 1;
            }
            bool isTrue = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance().CancelConfirmationStamp(OrderID, singnetType);
            if (isTrue)
            {
                InitPage(false);
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('取消盖章成功！'); location.href=location.href;");
            }
        }
    }
}
