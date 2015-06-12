using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.ScenicManage
{
    /// <summary>
    /// 编辑景区信息
    /// </summary>
    public partial class EditScenicTicket : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 是否修改
        /// </summary>
        protected string IsEdit = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Utils.GetQueryStringValue("action").ToLower();//行为 add、edit
            string ticketId = Utils.GetQueryStringValue("id");//门票编号
            string companyId = Utils.GetQueryStringValue("cid");//公司编号
            string scenicId = Utils.GetQueryStringValue("sid");//景区编号

            if (string.IsNullOrEmpty(action))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicTicket.aspx");
                return;
            }

            if (action == "edit")
            {
                if (string.IsNullOrEmpty(ticketId) || string.IsNullOrEmpty(companyId))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicTicket.aspx");
                    return;
                }

                IsEdit = "1";
            }
            else
            {
                if (string.IsNullOrEmpty(scenicId) || string.IsNullOrEmpty(companyId))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicList.aspx");
                    return;
                }
            }

            if (!IsPostBack)
            {
                InitData();
                if (action == "edit")
                {
                    InitAllSight(companyId);
                    InitTicket(ticketId, companyId);
                }
                else
                {
                    InitSight(scenicId, companyId);
                }
            }
        }

        /// <summary>
        /// 初始化基础数据
        /// </summary>
        private void InitData()
        {
            //初始化支付方式
            rblPayment.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicPayment));
            rblPayment.DataTextField = "Text";
            rblPayment.DataValueField = "Value";
            rblPayment.DataBind();

            //初始化状态、审核状态
            ddlState.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicTicketsStatus));
            ddlState.DataTextField = "Text";
            ddlState.DataValueField = "Value";
            ddlState.DataBind();

            ddlIsCheck.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ExamineStatus));
            ddlIsCheck.DataTextField = "Text";
            ddlIsCheck.DataValueField = "Value";
            ddlIsCheck.DataBind();

            //初始化b2b,b2c 显示位置
            ddlB2B.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicB2BDisplay));
            ddlB2B.DataTextField = "Text";
            ddlB2B.DataValueField = "Value";
            ddlB2B.DataBind();

            ddlB2C.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicB2CDisplay));
            ddlB2C.DataTextField = "Text";
            ddlB2C.DataValueField = "Value";
            ddlB2C.DataBind();

            txtB2BSort.Value = "50";
            txtB2CSort.Value = "50";
        }

        /// <summary>
        /// 初始化所有景区
        /// </summary>
        /// <param name="companyId">公司编号</param>
        private void InitAllSight(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return;

            ddlSight.DataSource = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(companyId);
            ddlSight.DataTextField = "ScenicName";
            ddlSight.DataValueField = "ScenicId";
            ddlSight.DataBind();
        }

        /// <summary>
        /// 初始化门票信息
        /// </summary>
        /// <param name="ticketId">门票编号</param>
        /// <param name="companyId">公司编号</param>
        private void InitTicket(string ticketId, string companyId)
        {
            var model = EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().GetModel(ticketId, companyId);
            if (model == null)
                return;

            if (ddlSight.Items.FindByValue(model.ScenicId) != null)
                ddlSight.Items.FindByValue(model.ScenicId).Selected = true;
            if (rblPayment.Items.FindByValue(Convert.ToInt32(model.Payment).ToString()) != null)
                rblPayment.Items.FindByValue(Convert.ToInt32(model.Payment).ToString()).Selected = true;
            if (ddlState.Items.FindByValue(Convert.ToInt32(model.Status).ToString()) != null)
                ddlState.Items.FindByValue(Convert.ToInt32(model.Status).ToString()).Selected = true;
            if (ddlIsCheck.Items.FindByValue(Convert.ToInt32(model.ExamineStatus).ToString()) != null)
                ddlIsCheck.Items.FindByValue(Convert.ToInt32(model.ExamineStatus).ToString()).Selected = true;
            if (ddlB2B.Items.FindByValue(Convert.ToInt32(model.B2B).ToString()) != null)
                ddlB2B.Items.FindByValue(Convert.ToInt32(model.B2B).ToString()).Selected = true;
            if (ddlB2C.Items.FindByValue(Convert.ToInt32(model.B2C).ToString()) != null)
                ddlB2C.Items.FindByValue(Convert.ToInt32(model.B2C).ToString()).Selected = true;

            txtName.Value = model.TypeName;
            txtMSPrice.Value = model.RetailPrice <= 0 ? string.Empty : model.RetailPrice.ToString("F2");
            hidMSPrice.Value = model.RetailPrice.ToString("F2");
            txtWZPrice.Value = model.WebsitePrices <= 0 ? string.Empty : model.WebsitePrices.ToString("F2");
            hidWZPrice.Value = model.WebsitePrices.ToString("F2");
            txtSCPrice.Value = model.MarketPrice <= 0 ? string.Empty : model.MarketPrice.ToString("F2");
            hidSCPrice.Value = model.MarketPrice.ToString("F2");
            txtTHPrice.Value = model.DistributionPrice <= 0 ? string.Empty : model.DistributionPrice.ToString("F2");
            hidTHPrice.Value = model.DistributionPrice.ToString("F2");
            txtLimit.Value = model.Limit <= 0 ? string.Empty : model.Limit.ToString();
            txtEnName.Value = model.EnName;
            txtStartDate.Value = model.StartTime.HasValue ? model.StartTime.Value.ToShortDateString() : string.Empty;
            txtEndDate.Value = model.EndTime.HasValue ? model.EndTime.Value.ToShortDateString() : string.Empty;
            txtDescription.Value = model.Description;
            txtSaleDescription.Value = model.SaleDescription;
            txtSort.Value = model.CustomOrder <= 0 ? string.Empty : model.CustomOrder.ToString();
            txtB2BSort.Value = model.B2BOrder <= 0 ? string.Empty : model.B2BOrder.ToString();
            txtB2CSort.Value = model.B2COrder <= 0 ? string.Empty : model.B2COrder.ToString();
        }

        /// <summary>
        /// 初始化景区信息
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        private void InitSight(string scenicId, string companyId)
        {
            if (string.IsNullOrEmpty(scenicId) || string.IsNullOrEmpty(companyId))
                return;

            rblPayment.SelectedIndex = 0;

            var model = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(scenicId, companyId);
            if (model == null)
                return;

            ddlSight.Items.Clear();
            ddlSight.Items.Add(new ListItem(model.ScenicName, model.ScenicId));

            if (ddlState.Items.FindByValue(Convert.ToInt32(EyouSoft.Model.ScenicStructure.ScenicTicketsStatus.上架).ToString()) != null)
            {
                ddlState.Items.FindByValue(
                    Convert.ToInt32(EyouSoft.Model.ScenicStructure.ScenicTicketsStatus.上架).ToString()).Selected = true;
            }

            if (ddlIsCheck.Items.FindByValue(Convert.ToInt32(EyouSoft.Model.ScenicStructure.ExamineStatus.已审核).ToString()) != null)
            {
                ddlIsCheck.Items.FindByValue(
                    Convert.ToInt32(EyouSoft.Model.ScenicStructure.ExamineStatus.已审核).ToString()).Selected = true;
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        protected void SaveDate(object sender, EventArgs e)
        {
            string action = Utils.GetQueryStringValue("action").ToLower();//行为 add、edit
            string ticketId = Utils.GetQueryStringValue("id");//门票编号
            string companyId = Utils.GetQueryStringValue("cid");//公司编号
            string scenicId = Utils.GetQueryStringValue("sid");//景区编号
            if (string.IsNullOrEmpty(action))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicTicket.aspx");
                return;
            }

            if (action == "edit")
            {
                if (string.IsNullOrEmpty(ticketId) || string.IsNullOrEmpty(companyId))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicTicket.aspx");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(scenicId) || string.IsNullOrEmpty(companyId))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "参数丢失", "/ScenicManage/ScenicList.aspx");
                    return;
                }
            }

            string strName = Utils.GetFormValue(txtName.UniqueID, 20);
            decimal msPrice = Utils.GetDecimal(Utils.GetFormValue(txtMSPrice.UniqueID));
            decimal yhPrice = Utils.GetDecimal(Utils.GetFormValue(txtWZPrice.UniqueID));
            decimal zdPrice = Utils.GetDecimal(Utils.GetFormValue(txtSCPrice.UniqueID));
            decimal thPrice = Utils.GetDecimal(Utils.GetFormValue(txtTHPrice.UniqueID));
            int payType = Utils.GetInt(Utils.GetFormValue(rblPayment.UniqueID));
            //同业价 < 最低限价 < 优惠价 < 门市价
            string strErr = string.Empty;
            if (string.IsNullOrEmpty(strName))
                strErr += "门票类型名称不能为空 \\n";
            if (msPrice <= 0)
                strErr += "门市价必须为大于0的数字 \\n";
            if (payType <= 0)
                strErr += "支付方式必须选择 \\n";
            if (thPrice > zdPrice)
                strErr += "同业价必须小于等于最低限价 \\n";
            if (zdPrice > yhPrice)
                strErr += "最低限价必须小于等于优惠价 \\n";
            if (yhPrice > msPrice)
                strErr += "优惠价必须小于等于门市价 \\n";


            if (!string.IsNullOrEmpty(strErr))
            {
                EyouSoft.Common.Function.MessageBox.ShowAndReturnBack(this, strErr, 1);
                return;
            }

            var model = new EyouSoft.Model.ScenicStructure.MScenicTickets
                            {
                                TicketsId = ticketId,
                                CompanyId = companyId,
                                B2B =
                                    (EyouSoft.Model.ScenicStructure.ScenicB2BDisplay)
                                    Utils.GetInt(Utils.GetFormValue(ddlB2B.UniqueID)),
                                B2BOrder = Utils.GetInt(Utils.GetFormValue(txtB2BSort.UniqueID), 50),
                                B2C =
                                    (EyouSoft.Model.ScenicStructure.ScenicB2CDisplay)
                                    Utils.GetInt(Utils.GetFormValue(ddlB2C.UniqueID)),
                                B2COrder = Utils.GetInt(Utils.GetFormValue(txtB2CSort.UniqueID), 50),
                                CustomOrder = Utils.GetInt(Utils.GetFormValue(txtSort.UniqueID), 9),
                                Description = Utils.GetFormValue(txtDescription.UniqueID, 500),
                                DistributionPrice = thPrice,
                                EnName = Utils.GetFormValue(txtEnName.UniqueID),
                                EndTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtEndDate.UniqueID)),
                                RetailPrice = msPrice,
                                WebsitePrices = yhPrice,
                                MarketPrice = zdPrice,
                                Limit = Utils.GetInt(Utils.GetFormValue(txtLimit.UniqueID)),
                                StartTime = Utils.GetDateTimeNullable(Utils.GetFormValue(txtStartDate.UniqueID)),
                                SaleDescription = Utils.GetFormValue(txtSaleDescription.UniqueID, 500),
                                ScenicId = Utils.GetFormValue(ddlSight.UniqueID),
                                Payment = (EyouSoft.Model.ScenicStructure.ScenicPayment)payType,
                                Status =
                                    (EyouSoft.Model.ScenicStructure.ScenicTicketsStatus)
                                    Utils.GetInt(Utils.GetFormValue(ddlState.UniqueID)),
                                ExamineStatus =
                                    (EyouSoft.Model.ScenicStructure.ExamineStatus)
                                    Utils.GetInt(Utils.GetFormValue(ddlIsCheck.UniqueID)),
                                TypeName = strName
                            };

            if (model.ExamineStatus == EyouSoft.Model.ScenicStructure.ExamineStatus.已审核)
            {
                model.ExamineOperator = MasterUserInfo.ID;
            }

            if (action == "edit")
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().OperateUpdate(model))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "修改成功", "/ScenicManage/ScenicTicket.aspx");
                    return;
                }
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "修改失败", "/ScenicManage/ScenicTicket.aspx");
                return;
            }
            else
            {
                model.IssueTime = DateTime.Now;

                #region 为发布人赋值

                var qmodel = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
                var userList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(companyId, qmodel);
                if (userList != null && userList.Count > 0)
                {
                    foreach (var t in userList)
                    {
                        if (t != null && t.IsAdmin)
                        {
                            model.Operator = t.ID;
                            break;
                        }
                    }
                }
                //防止程序异常，无实际意义
                if (model.Operator == null)
                    model.Operator = string.Empty;

                #endregion

                if (EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().OperateAdd(model))
                {
                    EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "添加成功", "/ScenicManage/ScenicList.aspx");
                    return;
                }
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "添加失败", "/ScenicManage/ScenicList.aspx");
                return;
            }

        }
    }
}
