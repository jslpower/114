using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.Itinerary
{
    /// <summary>
    /// 供应商信息 页面
    /// 创建人：戴银柱
    /// 创建时间： 2010-10-19  
    public partial class SuppliersInfo : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化
                DataInit();
            }

            //页面修改保存时 type = update
            string type = Utils.GetQueryStringValue("type");
            if (type == "Update")
            {
                Response.Clear();
                Response.Write(UpdateData());
                Response.End();
            }
        }

        #region 数据初始化
        /// <summary>
        /// 数据初始化
        /// </summary>
        protected void DataInit()
        {
            EyouSoft.Model.TicketStructure.TicketWholesalersInfo model = EyouSoft.BLL.TicketStructure.TicketSupplierInfo .CreateInstance().GetSupplierInfo(SiteUserInfo.CompanyID);
            if (model != null)
            {
                this.sup_lblSuppliers.Text = SiteUserInfo.CompanyName;
                this.sup_txtContact.Text = model.ContactName;
                this.sup_txtTel.Text = model.ContactTel;
                this.sup_ServicePrice.Text = model.ServicePrice.ToString("0.00");
                this.sup_BeginTime.Text = model.WorkStartTime;
                this.sup_EndTime.Text = model.WorkEndTime;
                this.sup_txtPrice.Text = model.DeliveryPrice.ToString("0.00");
            }
        }
        #endregion

        #region 修改保存
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected string UpdateData()
        {
            string contactName = Utils.GetFormValue(this.sup_txtContact.UniqueID);
            string contactTel = Utils.GetFormValue(this.sup_txtTel.UniqueID);
            decimal servicePrice = Utils.GetDecimal(Utils.GetFormValue(this.sup_ServicePrice.UniqueID));
            string workStartTime = Utils.GetFormValue(this.sup_BeginTime.UniqueID);
            string workEndTime = Utils.GetFormValue(this.sup_EndTime.UniqueID);
            decimal deliveryPrice = Utils.GetDecimal(Utils.GetFormValue(this.sup_txtPrice.UniqueID));
            if (String.IsNullOrEmpty(contactName))
            {
                return "联系人不能为空!";
            }
            if (!Utils.IsMobilePhone(contactTel))
            {
                return "请正确输入手机号码!";
            }
            if (servicePrice == 0)
            {
                //return "请正确输入服务价格!";
            }
            if (String.IsNullOrEmpty(workStartTime))
            {
                return "请输入上班时间!";
            }
            if (String.IsNullOrEmpty(workEndTime))
            {
                return "请输入下班时间!";
            }
            if (deliveryPrice == 0)
            {
                //return "请正确输入快递费!";
            }
             
            EyouSoft.Model.TicketStructure.TicketWholesalersInfo model = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(SiteUserInfo.CompanyID);
            if (model != null)
            {
                model.ContactName = contactName;
                model.ContactTel = contactTel;
                model.ServicePrice = servicePrice;
                model.WorkStartTime = workStartTime;
                model.WorkEndTime = workEndTime;
                model.DeliveryPrice = deliveryPrice;

                bool result = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().UpatetSupplierInfo(model);
                if (result)
                {
                    return "UpdateOk";
                }
                else
                {
                    return "error"; 
                }
            }
            return "error";
        }
        #endregion 
    }
}
