using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.PurchaseRouteShip
{
    public partial class MsgComfirm : EyouSoft.Common.Control.BasePage
    {
        protected string uid = "";
        protected string iframeId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsLogin)
                {
                    Response.Write("登录已过期!");
                    return;
                }

                if (!IsPostBack)
                {

                    string logId = Utils.GetQueryStringValue("logId");
                    iframeId = Utils.GetQueryStringValue("iframeId");
                    if (logId != "")
                    {
                        uid = SiteUserInfo.ID;
                        DataInit(logId);
                    }
                    else
                    {
                        Response.Write("Error:服务器繁忙,请稍后在试!");
                    }
                }
            }


        }

        #region 数据初始化
        /// <summary>
        /// 页面数据初始化
        /// </summary>
        /// <param name="logId"></param>
        protected void DataInit(string logId)
        {
            EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetModel(logId);
            if (model != null)
            {
                this.msg_hide_logId.Value = logId;
                EyouSoft.Model.TicketStructure.TicketFreightPackageInfo packageModel = EyouSoft.BLL.TicketStructure.FreightPackageInfo.CreateInstance().GetModel(model.PackageId);
                if(packageModel!=null)
                {
                    this.lbl_msg_name.Text = packageModel.PackageName;
                    this.lbl_msg_air.Text = packageModel.FlightName;
                    this.lbl_msg_count.Text = model.BuyCount.ToString();
                    this.lbl_msg_begin.Text = model.StartMonth.ToString("yyyy-MM-dd");
                    this.lbl_msg_end.Text = model.EndMonth.ToString("yyyy-MM-dd");
                    this.lbl_msg_price.Text = model.SumPrice.ToString("0.00");
                    switch (model.PackageType)
                    {
                        case EyouSoft.Model.TicketStructure.PackageTypes.常规: this.lbl_msg_type.Text = "常规购买"; break;
                        case EyouSoft.Model.TicketStructure.PackageTypes.套餐: this.lbl_msg_type.Text = "套餐购买"; break;
                        default: this.lbl_msg_type.Text = "促销购买"; break;
                    }

                    if (model.PackageType == EyouSoft.Model.TicketStructure.PackageTypes.常规)
                    {
                        panelHangKong.Visible = false;
                    }
                }
            }
        }
        #endregion 
    }

}
