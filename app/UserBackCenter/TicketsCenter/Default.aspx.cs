using System;
using System.Collections;
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
using System.Collections.Generic;
using AlipayClass;
using EyouSoft.Common.Function;
using tenpay;

namespace UserBackCenter.TicketsCenter
{
    /// <summary>
    /// 机票供应商后台首页，张新兵，20101019
    /// </summary>
    public partial class Default :EyouSoft.Common.Control.BasePage
    {
        public IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> diclist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string requestType = Request.QueryString["type"] != null ? Request.QueryString["type"].ToString().ToLower() : "";
            if (requestType == "save")
            {
                Save();
            }
            // 判断用户是否登录，如果没有登录跳转到登录页面
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/TicketsCenter/Default.aspx");
                return;
            }

            //判断用户所在公司，在运营后台是否通过了审核
            if (!IsCompanyCheck)//没有通过审核
            {
                Utils.ShowError("对不起，您还没通过审核，不能进入机票供应商后台!", "ticketscenter");
                return;
            }

            //判断用户是否是机票供应商用户
            if (!IsAirTicketSupplyUser)//不是
            {
                Utils.ShowError("对不起，您不是机票供应商用户，不能进入机票供应商后台!", "ticketscenter");
                return;
            }

            //判断用户绑定的支付帐户信息 是否已经成功加入支付圈
            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll =
                EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();

            //当前只能判断支付宝接口和财付通接口的帐户
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> accountList =
                bll.GetTicketCompanyAccountList(SiteUserInfo.CompanyID);
            if (accountList != null && accountList.Count > 0)
            {
                foreach (EyouSoft.Model.TicketStructure.TicketCompanyAccount account in accountList)
                {
                    if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                    {
                        if (account.IsSign)//已经签约
                        {
                            continue;
                        }
                        //通过 支付宝查询是否签约接口，获取该帐户是否已经签约
                        Query_Customer queryCustomer = new Query_Customer(
                            AlipayParameters.Partner,
                            AlipayParameters.Key,
                            AlipayParameters.SignType,
                            AlipayParameters.Input_Charset,
                            account.AccountNumber);
                        Query_Customer_Result queryCustomerResult = queryCustomer.GetResult();
                        if (queryCustomerResult.IsSuccess_Refund_Charge)//已经签约
                        {
                            //将成功签约的状态保存到数据库中
                            account.IsSign = true;
                            bll.SetIsSign(account.CompanyId, account.InterfaceType, true);
                        }
                    }
                    else if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                    {
                        if (account.IsSign)//已经建立委托退款关系
                        {
                            continue;
                        }
                        //通过 财付通查询是否建立委托退款关系接口，获取该帐户是否已经建立委托关系
                        trustRefundQuerys querys = new trustRefundQuerys(
                            TenpayParameters.Bargainor_ID,
                            account.AccountNumber,
                            TenpayParameters.Key,
                            Context);
                        bool isTrust = querys.IsTrustRefund();
                        if (isTrust == true)//已经建立关系
                        {
                            //将成功建立关系的状态保存到数据库中
                            account.IsSign = true;
                            bll.SetIsSign(account.CompanyId, account.InterfaceType, true);
                        }
                    }
                }
            }

            //初始化订单管理列表
            diclist = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetSupplierHandelStats(SiteUserInfo.CompanyID);

            //初始化公司信息
            ltrCompanyName.Text = SiteUserInfo.CompanyName;
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo CompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfoModel = CompanyInfo.GetModel(SiteUserInfo.CompanyID);
            if (CompanyInfoModel != null)
            {
                if (!String.IsNullOrEmpty(CompanyInfoModel.AttachInfo.CompanyLogo.ImagePath))
                {
                    this.imgLogo.Src = Domain.FileSystem + CompanyInfoModel.AttachInfo.CompanyLogo.ImagePath;
                }
                else
                {
                    this.imgLogo.Src = ImageServerUrl +"/images/logo.gif";
                }
            }
            ltrUserName.Text = SiteUserInfo.UserName;

            //初始化  现有运价数 ,平台用户数
            int gongYingUserCount = 0;
            EyouSoft.Model.SystemStructure.SummaryCount scount =
                EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (scount != null)
            {
                gongYingUserCount = scount.TicketCompanyVirtual;
            }
            ltrGongYingCompanyCount.Text = gongYingUserCount.ToString();

            //初始化用户资料
            EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo isupplierInfoBll =
                EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance();
            EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo allInfo =
                isupplierInfoBll.GetSupplierInfo(SiteUserInfo.CompanyID);
            if (allInfo != null)
            {
                Default_ContactPeople.Value = allInfo.ContactName;//联系人
                Default_Phone.Value = allInfo.ContactTel;//联系电话
                ltrUserLevel.Text = allInfo.ProxyLev;//代理级别
                ltrTicketSuccess.Text = (allInfo.SuccessRate * 100).ToString("F1")+"%";//出票成功率
                Default_ICP.Value = allInfo.ICPNumber;//ICP备案号
                Default_Url.Value = allInfo.WebSite ;//网址

                string licenceImg = allInfo.AttachInfo.BusinessCertif.LicenceImg;//营业执照
                //判断是否有上传了营业执照
                if (!string.IsNullOrEmpty(licenceImg))//有
                {
                    //显示 查看图片链接
                    ltrFile1.Text = string.Format("<a href='{0}' target='_blank'>查看</a>", Domain.FileSystem + licenceImg);
                }
                else
                {
                    ltrFile1.Text = "暂无营业执照";
                }

                string bronze = allInfo.AttachInfo.Bronze;//铜牌
                //判断是否有上传了铜牌
                if (!string.IsNullOrEmpty(bronze))//有
                {
                    //显示 查看图片链接
                    ltrFile2.Text = string.Format("<a href='{0}' target='_blank'>查看</a>", Domain.FileSystem + bronze);
                }
                else
                {
                    ltrFile2.Text = "暂无铜牌图片";
                }

                string tradeAward = allInfo.AttachInfo.TradeAward;//行业奖项
                //判断是否有上传了行业奖项
                if (!string.IsNullOrEmpty(tradeAward))//有
                {
                    //显示 查看图片链接
                    ltrFile3.Text = string.Format("<a href='{0}' target='_blank'>查看</a>", Domain.FileSystem + tradeAward);
                }
                else
                {
                    ltrFile3.Text = "暂无行业奖项图片";
                }

                Default_Remark.Value = allInfo.CompanyRemark;//备注
            }
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        protected void Save()
        {
            //如果没有登录则返回没有登录的信息
            if (!IsLogin)
            {
                Response.Clear();
                Response.Write("{Islogin:false}");
                Response.End();
            }

            //获取用户信息，需设置每一个信息在服务端能接受的最大长度值。
            string contactPeople = Utils.GetFormValue("Default_ContactPeople");//联系人
            string phone = Utils.GetFormValue("Default_Phone");//联系电话
            string jcp = Utils.GetFormValue("Default_ICP");//ICP备案号
            string url = Utils.GetFormValue("Default_Url");//网址
            string filepath1 = Utils.GetFormValue("fileUploadControl1$hidFileName");//营业执照
            string filepath2 = Utils.GetFormValue("fileUploadControl2$hidFileName");//铜牌图片
            string filepath3 = Utils.GetFormValue("fileUploadControl3$hidFileName");//行业奖项
            string remark = Utils.GetFormValue("Default_Remark",250);//备注

            //验证数据有效性
            string errMsg = string.Empty;//错误信息
            //联系人是否为空
            if (contactPeople == string.Empty)//为空
            {
                errMsg += "联系人不能为空\n";
            }
            //联系电话是否为空
            if (phone == string.Empty)//为空
            {
                errMsg += "联系电话不能为空\n";
            }
            else//不为空
            {
                //验证联系电话是否有效
                if (!Utils.IsPhone(phone))//无效
                {
                    errMsg += "联系电话格式不正确";
                }
            }
            //ICP备案号不能为空
            if (jcp == string.Empty)//为空
            {
                errMsg += "ICP备案号不能为空\n";
            }
            //网址不能为空
            if (url == string.Empty)//为空
            {
                errMsg += "网址不能为空\n";
            }

            //判断 是否 错误信息
            if (errMsg != string.Empty)//有
            {
                //返回错误信息
                Utils.ResponseMeg(false, errMsg);
                return;
            }

            //保存到数据库中
            EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo bll =
                EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance();
            EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo allInfo =
                bll.GetSupplierInfo(SiteUserInfo.CompanyID);

            allInfo.ContactName = contactPeople;
            allInfo.ContactTel = phone;
            allInfo.ICPNumber = jcp;
            allInfo.WebSite = url;
            if (filepath1 != string.Empty)//营业执照
            {
                allInfo.AttachInfo.BusinessCertif.LicenceImg = filepath1;
            }
            if (filepath2 != string.Empty)//铜牌
            {
                allInfo.AttachInfo.Bronze = filepath2;
            }
            if (filepath3 != string.Empty)//行业奖项
            {
                allInfo.AttachInfo.TradeAward = filepath3;
            }
            allInfo.CompanyRemark = remark;

            bool result = bll.UpatetSupplierDetailsInfo(allInfo);

            if (result)
            {
                Utils.ResponseMeg(true, "保存成功");
            }
            else
            {
                Utils.ResponseMeg(false, "保存失败,请稍后再试");
            }
        }
    }
}
