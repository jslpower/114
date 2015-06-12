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
using EyouSoft.Security.Membership;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 出发团队人员汇总名单
    /// 创建者：luofx 时间：2010-6-24
    /// </summary>
    public partial class BookingList : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 线路名称
        /// </summary>
        protected string RouteName = string.Empty;
        /// <summary>
        /// 出团时间
        /// </summary>
        protected string LeaveTime = string.Empty;

        private string SellCompanyID = string.Empty;
        private string TourID = string.Empty;
        private string TourNo = string.Empty;
        private string UserID = string.Empty;
        private string OrderID = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {                
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(Request.Url.ToString(), "对不起，你还未登录获取登录过期，不能查看游客出团名单，请登录！");                                
                return;
            }
            imgbtnToWord.ImageUrl = ImageServerPath + "/images/daochuword.gif";
            if (this.IsLogin)
            {
                SellCompanyID = this.SiteUserInfo.CompanyID;
                UserID = this.SiteUserInfo.ID;
                OrderID = Utils.GetQueryStringValue("OrderID");
                if (!IsPostBack)
                {
                    InitPage();
                }
            }
            else
            {
                UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/RouteAgency/NotStartingTeams.aspx", "对不起，你还未登录或登录过期，请重新登录！");
                return;
            }
            this.btnWord.Attributes.Add("onclick", "getHtml();");
            this.btnToWord.Attributes.Add("onclick", "getHtml();");
            imgbtnToWord.Attributes.Add("onclick", "getHtml();");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            if (Request.QueryString["TourID"] != null && !string.IsNullOrEmpty(Request.QueryString["TourID"]))
            {
                TourID = Utils.InputText(Request.QueryString["TourID"]);
                //获取批发商团队信息集合 
                EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
                IList<EyouSoft.Model.TourStructure.TourOrder> lists = Ibll.GetOrderList(SellCompanyID, null, TourID, TourNo, "", null, null, null);
               if(lists!=null&&lists.Count>0){
                   RouteName = lists[0].RouteName;
                   LeaveTime = lists[0].LeaveDate.ToShortDateString();
                if (!string.IsNullOrEmpty(Request.QueryString["OrderID"]))
                {
                    ltrBookName.Text = "订单人员名单";
                    IList<EyouSoft.Model.TourStructure.TourOrder> bindList = ((List<EyouSoft.Model.TourStructure.TourOrder>)lists).FindAll(item =>
                    {
                        return item.ID == OrderID;
                    });
                    rptBookingList.DataSource = bindList;
                    rptBookingList.DataBind();
                }
                else
                {
                    rptBookingList.DataSource = lists;
                    rptBookingList.DataBind();
                }
               }
                lists = null;
                Ibll = null;
            }
        }
        /// <summary>
        /// 第一层repeater数据绑定时激发嵌套repeater数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_BookingList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptChild = new Repeater();
                rptChild = (Repeater)e.Item.FindControl("rptBookingList2");
                if (rptChild != null)
                {
                    EyouSoft.Model.TourStructure.TourOrder model = (EyouSoft.Model.TourStructure.TourOrder)e.Item.DataItem;
                    if (model != null)
                    {
                        if (string.IsNullOrEmpty(Request.QueryString["OrderID"]))
                        {
                            tr_SpecialContent.Visible = false;
                            rptChild.DataSource = EyouSoft.BLL.TourStructure.TourOrderCustomer.CreateInstance().GetCustomerListByOrderId(model.ID);
                            rptChild.DataBind();
                        }
                        else
                        {
                            ltrSpecialContent.Text=model.SpecialContent;
                            rptChild.DataSource = EyouSoft.BLL.TourStructure.TourOrderCustomer.CreateInstance().GetCustomerListByOrderId(OrderID);
                            rptChild.DataBind();
                        }
                    }
                    model = null;
                }
            }
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
            string saveFileName = HttpUtility.UrlEncode("游客名单.doc");
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
                    <div>{0}
                            <table width=""696"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"">
                                <tr>
                                    <td>
                                        备注：
                                    </td>
                                </tr>
                                 <tr>
                                    <td style=""border: 1px solid #000000; padding: 1px;"">
                                       {1}
                                    </td>
                                </tr>
                            </table>
                    </div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, printHtml, printRemark));

            Response.End();
        }
    }
}
