using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.FinanceManage
{
    /// <summary>
    /// 付款登记
    /// </summary>
    /// 周文超 2010-11-10
    public partial class EnterPayable : EyouSoft.Common.Control.BasePage
    {
        private string ReturnUrl = "/FinanceManage/EnterPayable.aspx?";
        private bool IsHandle = true; //是否有操作权限
        protected bool IsCheck = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsLogin)
            {
                if (!this.CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('对不起，你正在使用的帐号没有相应的权限！');closeWin();");
                    return;
                }
                if (!this.CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理))
                {
                    EnterPayable_trAdd.Visible = false;
                    IsHandle = false;
                    EnterPayable_ckbAddIsCheck.Enabled = false;
                    IsCheck = false;
                }

                if (!IsPostBack)
                {
                    IniaPageData();
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('对不起你还没登陆,请登录！');closeWin();");
            }
        }


        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void IniaPageData()
        {
            string ReceivablesId = Utils.InputText(Request.QueryString["ID"]);
            if (string.IsNullOrEmpty(ReceivablesId))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('参数丢失，请从新打开此窗口！');closeWin();");
                return;
            }
            ReturnUrl = ReturnUrl + "ID=" + ReceivablesId;
            EyouSoft.Model.ToolStructure.Payments model = EyouSoft.BLL.ToolStructure.Payments.CreateInstance().GetModel(ReceivablesId);
            if (model == null)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('未找到您要查看的信息！');closeWin();");
                return;
            }

            decimal YS = model.SumPrice;
            decimal YIS = model.CheckendPrice + model.NoCheckedPrice;
            decimal WS = YS - YIS;
            //未收小于等于0时屏蔽新增按钮
            if (WS <= 0)
            {
                EnterPayable_btnSave.Visible = false;
            }
            EnterPayable_WS.Value = WS.ToString();
            EnterPayable_ltrSumPrice.Text = YS.ToString("C2");//合同金额
            EnterPayable_ltrYS.Text = YIS.ToString("C2");//已收
            EnterPayable_ltrWS.Text = WS.ToString("C2");//未收

            IList<EyouSoft.Model.ToolStructure.FundRegisterInfo> list = EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().GetRegisters(ReceivablesId);
            EnterPayable_rptReceivables.DataSource = list;
            EnterPayable_rptReceivables.DataBind();

            list = null;
            model = null;
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EnterPayable_rptReceivables_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex == -1)
                return;

            DropDownList ddl = (DropDownList)e.Item.FindControl("EnterPayable_ddlEditEnterFS");
            LinkButton lkb = (LinkButton)e.Item.FindControl("EnterPayable_btnEditSave");
            LinkButton lkbdel = (LinkButton)e.Item.FindControl("EnterPayable_lkbDel");
            LinkButton lbkChangeCheck = (LinkButton)e.Item.FindControl("EnterPayable_lkbCheck");
            Panel pan = (Panel)e.Item.FindControl("EnterPayable_tdHandle");
            Panel panEdit = (Panel)e.Item.FindControl("EnterPayable_panEdit");
            if (ddl != null)
            {
                EyouSoft.Model.ToolStructure.FundRegisterInfo dr = (EyouSoft.Model.ToolStructure.FundRegisterInfo)e.Item.DataItem;
                if (ddl.Items.FindByValue(((int)dr.PayType).ToString()) != null)
                    ddl.Items.FindByValue(((int)dr.PayType).ToString()).Selected = true;
            }
            if (lkb != null)
            {
                lkb.Attributes.Add("onclick", "return EnterPayable.getEditValue(" + (e.Item.ItemIndex + 1).ToString() + ",this);");
            }
            if (lkbdel != null)
            {
                lkbdel.Attributes.Add("onclick", "return confirm('确定要删除此项吗？');");
            }
            if (!IsHandle)
            {
                if (pan != null)
                    pan.Visible = false;
                if (panEdit != null)
                    panEdit.Visible = false;
            }
            if (!IsCheck)
            {
                if (lbkChangeCheck != null)
                    lbkChangeCheck.Visible = false;
            }
        }

        /// <summary>
        /// 行命令事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EnterPayable_rptReceivables_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string RegisterId = Utils.InputText(e.CommandArgument.ToString());
            bool IsResult = false;
            if (string.IsNullOrEmpty(RegisterId))
                return;
            switch (e.CommandName.ToLower())
            {
                case "del":
                    IsResult = EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().Delete(RegisterId);
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, IsResult ? "操作成功！" : "操作失败！", ReturnUrl + "ID=" + Request.QueryString["ID"]);
                    break;
                case "nocheck":
                    IsResult = EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().Check(RegisterId, false);
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, IsResult ? "操作成功！" : "操作失败！", ReturnUrl + "ID=" + Request.QueryString["ID"]);
                    break;
                case "edit":
                    System.Text.StringBuilder strErr = null;
                    IsResult = EditReceivables(RegisterId, out strErr);
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, strErr.ToString(), ReturnUrl + "ID=" + Request.QueryString["ID"]);
                    break;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RegisterId">登记信息编号</param>
        /// <returns></returns>
        private bool EditReceivables(string RegisterId, out System.Text.StringBuilder strErr)
        {
            strErr = new System.Text.StringBuilder();
            if (string.IsNullOrEmpty(RegisterId))
            {
                strErr.Append("参数丢失！");
                return false;
            }

            string pam = EnterPayable_hidEditValue.Value;
            if (string.IsNullOrEmpty(pam))
            {
                strErr.Append("参数丢失！");
                return false;
            }

            string[] Pam = pam.Split('^');
            if (Pam == null || Pam.Length <= 0 || Pam.Length != 10)
            {
                strErr.Append("参数丢失！");
                return false;
            }

            DateTime? EnterDate = Utils.GetDateTimeNullable(Pam[0]);
            string ContactName = Utils.InputText(Pam[1]);
            decimal ItemAmount = Utils.GetDecimal(Pam[2], 0);
            decimal OldItemAmount = Utils.GetDecimal(Pam[3], 0);
            int PayType = Utils.GetInt(Pam[4], -1);
            bool IsIsBilling = Pam[5] == "1" ? true : false;
            decimal BillingAmount = Utils.GetDecimal(Pam[6], 0);
            string InvoiceNo = Utils.InputText(Pam[7]);
            string Remark = Utils.InputText(Pam[8]);
            bool IsCheck = Pam[9] == "1" ? true : false;

            if (!EnterDate.HasValue)
                strErr.Append("付款日期必须填写！\\n");
            if (string.IsNullOrEmpty(ContactName))
                strErr.Append("付款人必须填写！\\n");
            if (ItemAmount <= 0)
                strErr.Append("付款金额必须填写！\\n");
            if (PayType < 0)
                strErr.Append("请选择付款方式！\\n");
            if (IsIsBilling)
            {
                if (BillingAmount <= 0)
                    strErr.Append("开票金额必须填写！\\n");
                if (string.IsNullOrEmpty(InvoiceNo))
                    strErr.Append("发票号必须填写！\\n");
            }

            if (!string.IsNullOrEmpty(strErr.ToString()))
                return false;

            if (CheckMoney(ItemAmount, OldItemAmount))
            {
                strErr.Append("付款金额不能大于未付金额！");
                return false;
            }

            EyouSoft.Model.ToolStructure.FundRegisterInfo model = new EyouSoft.Model.ToolStructure.FundRegisterInfo();
            model.ContactName = ContactName;
            model.BillingAmount = BillingAmount;
            model.InvoiceNo = InvoiceNo;
            model.IsBilling = IsIsBilling;
            model.IsChecked = IsCheck;
            model.IssueTime = DateTime.Now;
            model.ItemAmount = ItemAmount;
            model.ItemTime = EnterDate.Value;
            model.OperatorId = SiteUserInfo.ID;
            model.PayType = (EyouSoft.Model.ToolStructure.FundPayType)PayType;
            model.RegisterId = RegisterId;
            model.Remark = Remark;

            bool IsResult = EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().Update(model);
            if (IsResult)
                strErr.Append("修改成功！");
            else
                strErr.Append("修改失败！");
            model = null;

            return IsResult;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EnterPayable_btnSave_Click(object sender, EventArgs e)
        {
            string ReceivablesId = Utils.InputText(Request.QueryString["ID"]);
            if (string.IsNullOrEmpty(ReceivablesId))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('参数丢失，请从新打开此窗口！');closeWin();");
                return;
            }
            ReturnUrl = ReturnUrl + "ID=" + ReceivablesId;
            DateTime? EnterDate = Utils.GetDateTimeNullable(EnterPayable_txtEnterDate.Text.Trim());
            string ContactName = Utils.InputText(EnterPayable_txtEnterPeople.Text.Trim());
            decimal ItemAmount = Utils.GetDecimal(EnterPayable_txtPrice.Text.Trim(), 0);
            int PayType = Utils.GetInt(Request.Form["EnterPayable_ddlEnterFS"].Trim(), -1);
            bool IsIsBilling = EnterPayable_ckbAddIsBilling.Checked;
            decimal BillingAmount = Utils.GetDecimal(EnterPayable_txtBillingPrice.Text.Trim(), 0);
            string InvoiceNo = Utils.InputText(EnterPayable_txtInvoiceNo.Text.Trim());
            string Remark = Utils.InputText(EnterPayable_txtRemark.Text.Trim());
            bool IsCheck = EnterPayable_ckbAddIsCheck.Checked;

            System.Text.StringBuilder strErr = new System.Text.StringBuilder();
            if (!EnterDate.HasValue)
                strErr.Append("付款日期必须填写！\\n");
            if (string.IsNullOrEmpty(ContactName))
                strErr.Append("付款人必须填写！\\n");
            if (ItemAmount <= 0)
                strErr.Append("付款金额必须填写！\\n");
            if (PayType < 0)
                strErr.Append("请选择付款方式！\\n");
            if (IsIsBilling)
            {
                if (BillingAmount <= 0)
                    strErr.Append("开票金额必须填写！\\n");
                if (string.IsNullOrEmpty(InvoiceNo))
                    strErr.Append("发票号必须填写！\\n");
            }

            if (!string.IsNullOrEmpty(strErr.ToString()))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndReturnBack(this, strErr.ToString(), 1);
                return;
            }
            if (CheckMoney(ItemAmount, 0))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndReturnBack(this, "付款金额不能大于未付金额！", 1);
                return;
            }
            EyouSoft.Model.ToolStructure.FundRegisterInfo model = new EyouSoft.Model.ToolStructure.FundRegisterInfo();
            model.ContactName = ContactName;
            model.BillingAmount = BillingAmount;
            model.InvoiceNo = InvoiceNo;
            model.IsBilling = IsIsBilling;
            model.IsChecked = IsCheck;
            model.IssueTime = DateTime.Now;
            model.ItemAmount = ItemAmount;
            model.ItemId = ReceivablesId;
            model.ItemTime = EnterDate.Value;
            model.OperatorId = SiteUserInfo.ID;
            model.PayType = (EyouSoft.Model.ToolStructure.FundPayType)PayType;
            model.RegisterId = Guid.NewGuid().ToString();
            model.RegisterType = EyouSoft.Model.ToolStructure.FundRegisterType.付款;
            model.Remark = Remark;

            bool IsResult = EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().Add(model);
            EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, IsResult ? "添加成功！" : "添加失败！", ReturnUrl);

            model = null;
        }

        /// <summary>
        /// 验证应收、已收、未收关系
        /// </summary>
        /// <returns></returns>
        private bool CheckMoney(decimal CurrPrice, decimal OldPrice)
        {
            bool IsResult = false;
            string ReceivablesId = Utils.InputText(Request.QueryString["ID"]);
            if (string.IsNullOrEmpty(ReceivablesId))
                return true;
            EyouSoft.Model.ToolStructure.Payments model = EyouSoft.BLL.ToolStructure.Payments.CreateInstance().GetModel(ReceivablesId);
            if (model == null)
                return true;

            decimal YS = model.SumPrice;
            decimal YIS = model.CheckendPrice + model.NoCheckedPrice + CurrPrice - OldPrice;

            if (YS < YIS)
                IsResult = true;

            return IsResult;
        }
    }
}
