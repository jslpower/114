using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 景区门票预定详细页
    /// 功能：显示景区门票预定详细页
    /// 创建人：lxh
    /// 创建时间： 2011-10-29 
    /// </summary>
    public partial class SceniceInfo : EyouSoft.Common.Control.BasePage
    {
        protected string timeHtml = string.Empty;
        protected string MQSrc = string.Empty;
        protected string MqContent = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断用户是否登录，如果没有登录跳转到登录页面，如果有登录，初始化用户对象UserInfoModel
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
            }
            if (!IsPostBack)
            {
                //景区编号
                string Id = Utils.GetQueryStringValue("SceniceId");
                if (Id != null)
                {
                    EyouSoft.Model.ScenicStructure.MScenicArea Area = new EyouSoft.BLL.ScenicStructure.BScenicArea().GetModel(Id);

                    if (Area != null)
                    {
                        //景区联系人 联系电话 MQ
                        EyouSoft.Model.CompanyStructure.CompanyUser userinfo = new EyouSoft.BLL.CompanyStructure.CompanyUser().GetModel(Area.ContactOperator);
                        if (userinfo != null)
                        {
                            if (userinfo.ContactInfo != null)
                            {
                                this.litContact.Text = userinfo.ContactInfo.ContactName;
                                this.litContactTel.Text = userinfo.ContactInfo.Mobile;
                                if (!string.IsNullOrEmpty(userinfo.ContactInfo.MQ))
                                    this.LitContactMQ.Text = Utils.GetMQ(userinfo.ContactInfo.MQ);
                            }
                        }

                        if (Area.Company != null)
                        {
                            //门票编号
                            string ticketId = Utils.GetQueryStringValue("TicketId");

                            if (ticketId != "" && ticketId != null)
                            {
                                EyouSoft.Model.ScenicStructure.MScenicTickets Tickets = new EyouSoft.BLL.ScenicStructure.BScenicTickets().GetModel(ticketId, Area.Company.ID);

                                if (Tickets != null)
                                {
                                    if (Utils.GetQueryStringValue("ScenicName") != "")
                                    {
                                        this.litSceniceName.Text = Utils.GetQueryStringValue("ScenicName");
                                    }

                                    this.litSceniceType.Text = Tickets.TypeName;
                                    this.litMSPrice.Text = Utils.FilterEndOfTheZeroDecimal(Tickets.RetailPrice);
                                    this.litYHPrices.Text = Utils.FilterEndOfTheZeroDecimal(Tickets.WebsitePrices);
                                    this.litMinSCPrice.Text = Utils.FilterEndOfTheZeroDecimal(Tickets.MarketPrice);
                                    this.litTHPrice.Text = Utils.FilterEndOfTheZeroDecimal(Tickets.DistributionPrice);
                                    this.litMinlimit.Text = Tickets.Limit.ToString();
                                    //票价有效时间段
                                    timeHtml = DateTimeStr(Tickets.StartTime, Tickets.EndTime);
                                    this.litNotes.Text = Tickets.SaleDescription;
                                    this.litexplain.Text = Tickets.Description;

                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 取得有效时间
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        protected string DateTimeStr(DateTime? start, DateTime? end)
        {
            string str = string.Empty;

            if (start == null || end == null)
            {
                str = "长期有效";
            }
            else
            {
                if (Convert.ToDateTime(start).ToString("yyyy-MM-dd").Equals("0001-01-01")
                || Convert.ToDateTime(end).ToString("yyyy-MM-dd").Equals("0001-01-01"))
                {
                    str = "长期有效";
                }
                else
                {
                    str = Convert.ToDateTime(start).ToString("yyyy-MM-dd") + "至" + Convert.ToDateTime(end).ToString("yyyy-MM-dd");
                }
            }
            return str;
        }
    }
}
