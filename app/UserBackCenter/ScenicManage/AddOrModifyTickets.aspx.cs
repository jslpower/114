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
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common;

namespace UserBackCenter.ScenicManage
{
    public partial class AddOrModifyTickets : EyouSoft.Common.Control.BasePage
    {
             
        protected void Page_Load(object sender, EventArgs e)
        {
            ScenicB2BDisplay? b2b = null;
            ScenicB2CDisplay? b2c = null;
            ScenicPayment? payment = null;
            ScenicTicketsStatus? status = null;
            string scenicId = string.Empty;
            string companyId = this.SiteUserInfo.CompanyID;
            if (!IsPostBack)
            {
                scenicId = Request.QueryString["scenic"];
                string ticketsId = Request.QueryString["tickets"];
                MScenicTickets item = new MScenicTickets();
                #region
                if (!string.IsNullOrEmpty(scenicId))
                {
                    item = BScenicTickets.CreateInstance().GetModel(ticketsId, companyId);
                    if (item != null)
                    {
                        txtTypeName.Value = item.TypeName;
                        txtRetailPrice.Value = item.RetailPrice == 0 ? string.Empty : item.RetailPrice.ToString("F2");
                        txtMarketPrice.Value = item.MarketPrice == 0 ? string.Empty : item.MarketPrice.ToString("F2");
                        txtWebsitePrices.Value = item.WebsitePrices == 0 ? string.Empty : item.WebsitePrices.ToString("F2");
                        txtDistributionPrice.Value = item.DistributionPrice == 0 ? string.Empty : item.DistributionPrice.ToString("F2");
                        txtEnName.Value = item.EnName;
                        hidMSPrice.Value = item.RetailPrice == 0 ? string.Empty : item.RetailPrice.ToString("F2");
                        hidWZPrice.Value = item.MarketPrice == 0 ? string.Empty : item.MarketPrice.ToString("F2");
                        hidSCPrice.Value = item.WebsitePrices == 0 ? string.Empty : item.WebsitePrices.ToString("F2");
                        hidTHPrice.Value =item.DistributionPrice == 0 ? string.Empty : item.DistributionPrice.ToString("F2");
                       
                        txtLimit.Value = item.Limit.ToString();
                        txtStartTime.Value = item.StartTime == null ? string.Empty : Convert.ToDateTime(item.StartTime).ToString("yyyy-MM-dd");
                        txtEndTime.Value = item.EndTime == null ? string.Empty : Convert.ToDateTime(item.EndTime).ToString("yyyy-MM-dd");
                        txtDescription.Value = item.Description;
                        txtSaleDescription.Value = item.SaleDescription;
                        lblExamineStatus.Text = item.ExamineStatus.ToString();
                        txtCustomOrder.Value = item.CustomOrder.ToString();
                        //txtB2BOrder.Value = item.B2BOrder.ToString();
                        //txtB2COrder.Value = item.B2COrder.ToString();
                        //支付方式
                        payment = item.Payment;
                        status = item.Status;
                        b2b = item.B2B;
                        b2c = item.B2C;
                        //景区编号
                        hfId.Value = ticketsId;
                    }
                }
                #endregion

                //景区
                DropScenic(companyId, scenicId);
                RidaoPayment(payment);
                DropStatus(status);
                //DropB2B(b2b);
                //DropB2C(b2c);
            }
        }

        /// <summary>
        /// 绑定dropdownlist
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="scenicId"></param>
        protected void DropScenic(string companyId, string scenicId)
        {
            IList<MScenicArea> list = BScenicArea.CreateInstance().GetList(companyId);
            if (list != null)
            {
                ddlScenic.DataSource = list;
                ddlScenic.DataTextField = "ScenicName";
                ddlScenic.DataValueField = "ScenicId";
                ddlScenic.DataBind();

                ddlScenic.Items.Insert(0, new ListItem("请选择", ""));
                if (!string.IsNullOrEmpty(scenicId))
                {
                    ddlScenic.SelectedValue = scenicId;
                }
            }
        }

        /// <summary>
        /// 绑定支付方式
        /// <param name="payment"></param>
        /// </summary>
        protected void RidaoPayment(ScenicPayment? payment)
        {
            List<EnumObj> list = EnumObj.GetList(typeof(ScenicPayment));
            if (list != null && list.Count > 0)
            {
                rdoPayment.DataSource = list;
                rdoPayment.DataTextField = "Text";
                rdoPayment.DataValueField = "Value";
               
                rdoPayment.DataBind();

                rdoPayment.Items[0].Selected = true;
                if (payment != null)
                {
                    rdoPayment.SelectedValue = ((int)payment).ToString();
                }
            }
        }

        /// <summary>
        /// 绑定状态
        /// </summary>
        /// <param name="status"></param>
        protected void DropStatus(ScenicTicketsStatus? status)
        {
            List<EnumObj> list = EnumObj.GetList(typeof(ScenicTicketsStatus));
            if (list != null && list.Count > 0)
            {
                ddlStatus.DataSource = list;
                ddlStatus.DataTextField = "Text";
                ddlStatus.DataValueField = "Value";
                ddlStatus.DataBind();

                //ddlStatus.Items.Insert(0, new ListItem("请选择", ""));
                if (status != null)
                {
                    ddlStatus.SelectedValue = ((int)status).ToString();
                }
            }
        }

        /// <summary>
        /// 绑定B2B显示方式
        /// </summary>
        /// <param name="b2b"></param>
        protected void DropB2B(ScenicB2BDisplay? b2b)
        {
            //List<EnumObj> list = EnumObj.GetList(typeof(ScenicB2BDisplay));
            //if (list != null && list.Count > 0)
            //{
            //    ddlB2B.DataSource = list;
            //    ddlB2B.DataTextField = "Text";
            //    ddlB2B.DataValueField = "Value";
            //    ddlB2B.DataBind();

            //    ddlB2B.Items.Insert(0, new ListItem("请选择", ""));
            //    if (b2b != null)
            //    {
            //        ddlB2B.SelectedValue = ((int)b2b).ToString();
            //    }
            //}
        }
        /// <summary>
        /// 绑定B2C显示方式
        /// </summary>
        /// <param name="b2c"></param>
        protected void DropB2C(ScenicB2CDisplay? b2c)
        {
            //List<EnumObj> list = EnumObj.GetList(typeof(ScenicB2CDisplay));
            //if (list != null && list.Count > 0)
            //{
            //    ddlB2C.DataSource = list;
            //    ddlB2C.DataTextField = "Text";
            //    ddlB2C.DataValueField = "Value";
            //    ddlB2C.DataBind();
            //    ddlB2C.Items.Insert(0, new ListItem("请选择",""));
            //    if (b2c != null)
            //    {
            //        ddlB2C.SelectedValue = ((int)b2c).ToString();
            //    }
            //}
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            MScenicTickets item = new MScenicTickets()
            {
                ScenicId = Utils.GetFormValue(ddlScenic.UniqueID),
                Status = (ScenicTicketsStatus)Enum.Parse(typeof(ScenicTicketsStatus), Utils.GetFormValue(ddlStatus.UniqueID)),
                //B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), Utils.GetFormValue(ddlB2B.UniqueID)),
                //B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), Utils.GetFormValue(ddlB2C.UniqueID)),
                Description = Utils.GetFormValue(txtDescription.UniqueID),
                CustomOrder = Utils.GetInt(Utils.GetFormValue(txtCustomOrder.UniqueID)),
                CompanyId = SiteUserInfo.CompanyID,
                SaleDescription = Utils.GetFormValue(txtSaleDescription.UniqueID),
                StartTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtStartTime.UniqueID)),
                EndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtEndTime.UniqueID)),
                EnName = Utils.GetFormValue(txtEnName.UniqueID),
                DistributionPrice = Utils.GetDecimal(Utils.GetFormValue(txtDistributionPrice.UniqueID)),
                //B2BOrder = Utils.GetInt(Utils.GetFormValue(txtB2BOrder.UniqueID)),
                //B2COrder = Utils.GetInt(Utils.GetFormValue(txtB2COrder.UniqueID)),
                Operator = SiteUserInfo.ID,
                Payment = (ScenicPayment)Enum.Parse(typeof(ScenicPayment), Utils.GetFormValue(rdoPayment.UniqueID)),
                Limit = Utils.GetInt(Utils.GetFormValue(txtLimit.UniqueID)),
                TypeName = Utils.GetFormValue(txtTypeName.UniqueID),
                WebsitePrices = Utils.GetDecimal(Utils.GetFormValue(txtWebsitePrices.UniqueID)),
                RetailPrice = Utils.GetDecimal(Utils.GetFormValue(txtRetailPrice.UniqueID)),
                MarketPrice = Utils.GetDecimal(Utils.GetFormValue(txtMarketPrice.UniqueID))
            };
            bool result;
            //添加
            if (string.IsNullOrEmpty(hfId.Value.Trim()) && !Utils.GetFormValue(ddlScenic.UniqueID).Equals(hfId.Value.Trim()))
            {
                result = BScenicTickets.CreateInstance().Add(item);
            }
            else //修改
            {
                item.TicketsId = hfId.Value.Trim();
                MScenicTickets ticketsItem = BScenicTickets.CreateInstance().GetModel(item.TicketsId,item.CompanyId);
                item.B2B = ticketsItem.B2B;
                item.B2C = ticketsItem.B2C;
                item.B2BOrder = ticketsItem.B2BOrder;
                item.B2COrder = ticketsItem.B2COrder;
                result = BScenicTickets.CreateInstance().Update(item);
                //if (result)
                //{
                    
                //    //已审核的情况下，修改了价格则要提示
                //    if ((item.DistributionPrice != ticketsItem.DistributionPrice ||
                //    item.WebsitePrices != ticketsItem.WebsitePrices ||
                //    item.RetailPrice != ticketsItem.RetailPrice ||
                //    item.MarketPrice != ticketsItem.MarketPrice) && ticketsItem.ExamineStatus == ExamineStatus.已审核)
                //    {
                //        msg = "修改价格后，请等待网站审核，方可显示";
                //    }
                //}
            }
            if (result)
            {
                Response.Write("");
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('操作成功');parent.Boxy.getIframeDialog('" + Request.QueryString["iframeid"] + "').hide();parent.topTab.url(parent.topTab.activeTabIndex,'/ScenicManage/MyScenice.aspx');");
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('操作失败!');;parent.Boxy.getIframeDialog('" + Request.QueryString["iframeid"] + "').hide();parent.topTab.url(parent.topTab.activeTabIndex,'/ScenicManage/MyScenice.aspx');");
            }
        }
    }
}
