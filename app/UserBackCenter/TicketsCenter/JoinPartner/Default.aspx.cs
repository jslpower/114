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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using AlipayClass;
using tenpay;

namespace UserBackCenter.TicketsCenter.JoinPartner
{
    public partial class Default : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"] != null 
                ? Request.QueryString["type"].ToString() : string.Empty;//请求类型
            //判断当前的请求类型
            if (type == "issign")//查询用户是否已经加入支付圈
            {
                IsSign();
                return;
            }
            else if (type == "bindaccount")//绑定支付帐户信息
            {
                BindAccount();
                return;
            }
        }

        /// <summary>
        /// 绑定支付帐户信息
        /// </summary>
        protected void BindAccount()
        {
            string account = Utils.GetQueryStringValue("account");//支付帐户信息
            int accountType = Utils.GetInt(Request.QueryString["accountType"]);//接口类型
            string currentUserCompanyId = SiteUserInfo.CompanyID;//当前用户的公司ID

            //判断接口类型是否有效
            if (accountType < 1 || accountType > 7)//无效
            {
                Utils.ResponseMeg(false, "无效的接口类型");
                return;
            }

            //判断支付帐户信息是否为空
            if (account == string.Empty)//为空
            {
                Utils.ResponseMeg(false, "支付帐户信息不能为空");
                return;
            }

            EyouSoft.Model.TicketStructure.TicketAccountType ticketAccountType =
                (EyouSoft.Model.TicketStructure.TicketAccountType)accountType;

            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll =
                EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();

            bool result = bll.AddTicketCompanyAccount(new EyouSoft.Model.TicketStructure.TicketCompanyAccount()
            {
                AccountNumber= account,
                CompanyId = currentUserCompanyId,
                InterfaceType = ticketAccountType,
                IsSign = false
            });

            if (result)
            {
                string qianyueUrl = GetQianYueUrl(ticketAccountType, account);//获取签约URL

                Response.Clear();
                Response.Write("{isBind:true,isSign:false,success:'1',accountNumber:'" + account + "',qianyueurl:'" + qianyueUrl + "'}");
                Response.End();
            }
            else
            {
                Utils.ResponseMeg(false, "绑定失败,请稍候再试！");
            }
        }

        /// <summary>
        /// 查询用户是否已经加入支付圈
        /// </summary>
        protected void IsSign()
        {
            int accountType = Utils.GetInt(Request.QueryString["accountType"]);//接口类型
            string currentUserCompanyId = SiteUserInfo.CompanyID;//当前用户的公司ID
            
            //判断接口类型是否有效
            if (accountType <1 || accountType >7)//无效
            {
                Utils.ResponseMeg(false, "无效的接口类型");
                return;
            }

            //接口类型
            EyouSoft.Model.TicketStructure.TicketAccountType ticketAccountType =
                (EyouSoft.Model.TicketStructure.TicketAccountType)accountType;

            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll = 
                EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();

            EyouSoft.Model.TicketStructure.TicketCompanyAccount model =
                bll.GetModel(currentUserCompanyId, ticketAccountType);

            //判断是否已经绑定过当前接口类型的帐户信息
            if (model == null)//没有绑定过
            {
                Response.Clear();
                Response.Write("{isBind:false}");
                Response.End();
            }

            Response.Clear();
            if (!model.IsSign)//没有进行过签约
            {
                string qianyueUrl = GetQianYueUrl(ticketAccountType, model.AccountNumber);//获取用于签约的URL

                Response.Write("{isBind:true,isSign:" + model.IsSign.ToString().ToLower() + ",accountNumber:'" + model.AccountNumber + "',qianyueurl:'"+Server.UrlEncode(qianyueUrl)+"'}");

            }
            else//进行过签约
            {
                Response.Write("{isBind:true,isSign:true,accountNumber:'"+model.AccountNumber+"'}");
            }

            
            Response.End();
        }

        /// <summary>
        /// 根据支付接口，生成相应的签约URL
        /// </summary>
        /// <param name="type">支付接口类型</param>
        /// <param name="accountNumber">支付帐户</param>
        /// <returns></returns>
        private string GetQianYueUrl(EyouSoft.Model.TicketStructure.TicketAccountType type,string accountNumber)
        {
            string url = string.Empty;

            switch (type)
            {
                case EyouSoft.Model.TicketStructure.TicketAccountType.财付通:
                    TrustRefund trust = new TrustRefund(TenpayParameters.Bargainor_ID);
                    url = trust.CreateUrl();
                    break;
                case EyouSoft.Model.TicketStructure.TicketAccountType.支付宝:
                    QianYue qianyue = new QianYue(AlipayParameters.Partner, AlipayParameters.SignType, AlipayParameters.Key, AlipayParameters.Input_Charset, accountNumber);
                    url = qianyue.Create_url();
                    break;
            }

            return url;
        }
    }
}
