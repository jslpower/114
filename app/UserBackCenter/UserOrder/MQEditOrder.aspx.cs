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
using System.Text;
using EyouSoft.Common;

namespace UserBackCenter.UserOrder
{
    /// <summary>
    /// MQ订单处理
    /// luofx 2010-7-7
    /// </summary>
    public partial class MQEditOrder : EyouSoft.Common.Control.BasePage
    {
        #region 变量
        protected string BuyCompanyName = string.Empty;
        protected string ContactName = string.Empty;
        protected string ContactTel = string.Empty;
        protected string ContactQQ = string.Empty;
        protected string LeaveDate = string.Empty;
        protected string ContactMQ = string.Empty;
        protected string LeaveTraffic = string.Empty;
        protected string RouteName = string.Empty;
        protected int OrderSource = 0;
        protected int OrderState = 5;
        protected int OrderType = 0;
        protected string TourID = string.Empty;
        private string OrderID = string.Empty;
        protected string CustomerHtml = string.Empty;
        private string UserID = string.Empty;
        /// <summary>
        /// 显示使用-虚拟剩余
        /// </summary>
        protected int ShowRemnantNumber = 0;
        /// <summary>
        /// 计算使用-虚拟剩余
        /// </summary>
        protected int RemnantNumber = 0;
        private string SeatList = string.Empty;
        protected bool isGrant = false;
        protected bool isRouteCompany = false;
        /// <summary>
        /// 判断是否为该专线公司的产品 
        /// </summary>
        protected bool isRouteCompanyProduct = false;
        /// <summary>
        /// 判断是否为该组团公司买的产品
        /// </summary>
        protected bool isTourCompanyProduct = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {

            }
            else if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {

            }
            else if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
            { 
                
            }
            isGrant = true;
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            else
            {
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    isRouteCompany = true;
                }
                UserID = this.SiteUserInfo.ID;
                OrderID = Utils.InputText(Request.QueryString["OrderID"]);
                btnNotEdit.Attributes.Add("onclick", "return EditOrder.confirm('你确定要不受理该订单吗？');");
                btnSuccess.Attributes.Add("onclick", "return EditOrder.confirm('你确定要成功交易该订单吗？');");
                btnCancelLeave.Attributes.Add("onclick", "return EditOrder.confirm('你确定要取消留位吗？');");
                btnCancel.Attributes.Add("onclick", "return EditOrder.confirm('你确定要取消该订单吗？');");
                btnDelete.Attributes.Add("onclick", "return EditOrder.confirm('你确定要删除该订单吗？');");
                if (!IsPostBack)
                {
                    InitPage();
                }
            }
        }
        private void InitPage()
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            EyouSoft.Model.TourStructure.TourOrder Model = new EyouSoft.Model.TourStructure.TourOrder();
            Model = Ibll.GetOrderModel(OrderID);
            List<EyouSoft.Model.TourStructure.TourOrder> lists = new List<EyouSoft.Model.TourStructure.TourOrder>();
            if (Model != null)
            {
                if (string.IsNullOrEmpty(Model.CompanyID))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "noOrderInfo()");
                    return;
                }
                InitButtonSate(Model.OrderState, Model.CompanyID, Model.BuyCompanyID);
                //该产品既不属于专线，也不是组团所买的产品时
                if (!isTourCompanyProduct && !isRouteCompanyProduct)
                {
                    Response.Clear();
                    Response.Write("对不起，该订单你没有修改和查看的权限！");
                    Response.End();
                }
                RouteName = Model.RouteName;
                TourID = Model.TourId;
                lists.Add(Model);
                ShowRemnantNumber = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourInfo(Model.TourId).RealRemnantNumber;
                BuyCompanyName = Model.BuyCompanyName;
                ContactName = Model.ContactName;
                ContactTel = Model.ContactTel;
                ContactQQ = Model.ContactQQ;
                LeaveDate = Model.LeaveDate.ToShortDateString();
                ContactMQ = Model.ContactMQ;
                LeaveTraffic = Model.LeaveTraffic;
                OrderSource = (int)Model.OrderSource;
                OrderState = (int)Model.OrderState;
                //游客
                InitCustomers(Model.TourOrderCustomer);
                this.rptEditOrder.DataSource = lists;
                this.rptEditOrder.DataBind();
                Model = null;
                Ibll = null;
                Model = null;
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "noOrderInfo()");
            }
            lists = null;
        }
        /// <summary>
        /// 初始化Button状态
        /// </summary>
        private void InitButtonSate(EyouSoft.Model.TourStructure.OrderState? OrderState, string SellCompanyID, string BuyCompanyID)
        {
            if (this.SiteUserInfo.CompanyID == SellCompanyID)
            {
                isRouteCompanyProduct = true;
            }
            if (this.SiteUserInfo.CompanyID == BuyCompanyID)
            {
                isTourCompanyProduct = true;
            }
            switch (OrderState)
            {
                case EyouSoft.Model.TourStructure.OrderState.未处理://未处理页面：处理订单
                    if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团) && this.isTourCompanyProduct)
                    {
                        btnUpdate.Visible = true;
                    }
                    else
                    {
                        btnUpdate.Visible = false;
                    }
                    btnSuccess.Visible = false;
                    btnCancelLeave.Visible = false;
                    btnCancel.Visible = false;
                    if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {
                        ChangeOrderSate();
                    }
                    break;
                case EyouSoft.Model.TourStructure.OrderState.处理中://处理中
                    btnUpdate.Visible = false;
                    btnSuccess.Visible = false;
                    btnCancelLeave.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case EyouSoft.Model.TourStructure.OrderState.已留位://已留位
                    btnLaeve.Visible = false;
                    btnNotEdit.Visible = false;
                    btnCancel.Visible = false;
                    btnSuccess.Visible = false;
                    if (!isRouteCompanyProduct && isTourCompanyProduct)
                    {
                        btnUpdate.Visible = false;
                    }
                    break;
                case EyouSoft.Model.TourStructure.OrderState.留位过期://留位过期
                    btnLaeve.Text = "继续留位";
                    btnUpdate.Visible = false;
                    btnNotEdit.Visible = false;
                    btnSuccess.Visible = false;
                    btnCancel.Visible = false;
                    btnCancelLeave.Visible = false;
                    break;
                case EyouSoft.Model.TourStructure.OrderState.不受理://不受理
                    btnNotEdit.Visible = false;
                    btnUpdate.Visible = false;
                    btnSuccess.Visible = false;
                    btnCancelLeave.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case EyouSoft.Model.TourStructure.OrderState.已成交://确认成交
                    btnSave.Visible = false;
                    btnLaeve.Visible = false;
                    btnNotEdit.Visible = false;
                    btnSuccess.Visible = false;
                    btnCancelLeave.Visible = false;
                    if (!isRouteCompanyProduct && isTourCompanyProduct)
                    {
                        btnUpdate.Visible = false;
                    }
                    break;
                default:
                    btnSave.Visible = false;
                    btnLaeve.Visible = false;
                    btnNotEdit.Visible = false;
                    btnUpdate.Visible = false;
                    btnSuccess.Visible = false;
                    btnCancel.Visible = false;
                    btnCancelLeave.Visible = false;
                    btnDelete.Visible = false;
                    break;

            }
            if (OrderState != EyouSoft.Model.TourStructure.OrderState.未处理 && !this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                TrOrderOperatorContainer.Visible = false;
                this.btnUpdate.Visible = false;
            }
        }
        #region 订单操作
        /// <summary>
        /// 修改状态：处理中
        /// </summary>
        private void ChangeOrderSate()
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            bool isTrue = Ibll.SetTourOrderState(OrderID, EyouSoft.Model.TourStructure.OrderState.处理中, DateTime.Now);
            Ibll = null;
        }
        /// <summary>
        /// 确认成交订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.TourStructure.TourOrder model = new EyouSoft.Model.TourStructure.TourOrder();
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            model.TourOrderCustomer = TourOrderCustomer();
            model.SeatList = SeatList.Trim(',');
            if (model.TourOrderCustomer == null || model.TourOrderCustomer.Count == 0)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('抱歉,游客人数必须大于0，将不能保存，请重新填写游客数和游客信息！');");
                InitPage();
                return;
            }
            InputValueToModel(model);
            if (model.PeopleNumber > Utils.GetInt(Request.Form["hidRemnantNumber"], 0))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('抱歉，剩余数不能大于游客数，不能保存，请重新填写游客数和游客信息！');");
                InitPage();
                return;
            }
            model.OrderState = (EyouSoft.Model.TourStructure.OrderState)Utils.GetInt(Utils.GetFormValue("hidOrderState"));
            model.OrderState = EyouSoft.Model.TourStructure.OrderState.已成交;
            model.SaveSeatDate = DateTime.Now;
            int ReturnValue = Ibll.UpdateTourOrder(model);
            model = null;
            Ibll = null;
            if (ReturnValue > 0)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行确认成交操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行确认成交操作失败！");
            }
        }
        /// <summary>
        /// 不受理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNotEdit_Click(object sender, EventArgs e)
        {
            bool isTrue = UpdateOrderState(EyouSoft.Model.TourStructure.OrderState.不受理, DateTime.Now);
            if (isTrue)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行不受理操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行不受理操作失败！");
            }
        }
        /// <summary>
        /// 交易成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSuccess_Click(object sender, EventArgs e)
        {
            bool isTrue = UpdateOrderState(EyouSoft.Model.TourStructure.OrderState.已成交, DateTime.Now);
            if (isTrue)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行交易成功操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行交易成功操作失败！");
            }
        }
        /// <summary>
        /// 取消留位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelLeave_Click(object sender, EventArgs e)
        {
            bool isTrue = UpdateOrderState(EyouSoft.Model.TourStructure.OrderState.不受理, DateTime.Now);
            if (isTrue)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行取消留位操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行取消留位操作失败！");
            }
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            bool isTrue = UpdateOrderState(EyouSoft.Model.TourStructure.OrderState.不受理, DateTime.Now);
            if (isTrue)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行取消订单操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行取消订单操作失败！");
            }
        }
        /// <summary>
        /// 修改订单状态
        /// 公用方法
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool UpdateOrderState(EyouSoft.Model.TourStructure.OrderState state, DateTime NowDate)
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            bool isTrue = Ibll.SetTourOrderState(OrderID, state, NowDate);
            Ibll = null;
            return isTrue;
        }

        /// <summary>
        /// 保存修改订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.TourStructure.TourOrder model = new EyouSoft.Model.TourStructure.TourOrder();
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            model.TourOrderCustomer = TourOrderCustomer();
            model.SeatList = SeatList.Trim(',');
            if (model.TourOrderCustomer == null || model.TourOrderCustomer.Count == 0)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('抱歉,游客人数必须大于0，将不能保存，请重新填写游客数和游客信息！');");
                InitPage();
                return;
            }
            if (model.PeopleNumber > Utils.GetInt(Request.Form["hidRemnantNumber"], 0))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('抱歉，剩余数不能大于游客数，不能保存，请重新填写游客数和游客信息！');");
                InitPage();
                return;
            }
            InputValueToModel(model);
            model.OrderState = (EyouSoft.Model.TourStructure.OrderState)Utils.GetInt(Utils.GetFormValue("hidOrderState"));
            model.SaveSeatDate = DateTime.Now;
            int ReturnValue = Ibll.UpdateTourOrder(model);
            model = null;
            Ibll = null;
            switch (ReturnValue)
            {
                case -1:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('写入订单信息失败！');closeWin()");
                    break;
                case 0:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('订单实体为空！');closeWin()");
                    break;
                default:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('订单保存成功！');closeWin()");
                    break;
            }
        }
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <param name="model"></param>
        private void InputValueToModel(EyouSoft.Model.TourStructure.TourOrder model)
        {
            model.ID = OrderID;
            model.OrderType = Utils.GetInt(Utils.GetFormValue("hidOrderType"));
            model.LastOperatorID = UserID;
            model.SpecialContent = Utils.GetFormValue("SpecialContent");
            model.SumPrice = Utils.GetDecimal(Request.Form["SumPrice"]);
            model.PriceStandId = Utils.GetFormValue("PriceStandId");
            model.PersonalPrice = Utils.GetDecimal(Request.Form["PersonalPrice"]);
            model.ChildPrice = Utils.GetDecimal(Request.Form["ChildPrice"]);
            model.AdultNumber = Utils.GetInt(Request.Form["AdultNumber"]);
            model.ChildNumber = Utils.GetInt(Request.Form["ChildNumber"]);
            model.MarketNumber = Utils.GetInt(Request.Form["MarketNumber"]);
            model.PeopleNumber = model.AdultNumber + model.ChildNumber;
            model.MarketPrice = Utils.GetDecimal(Request.Form["MarketPrice"]);
            model.OtherPrice = Utils.GetDecimal(Request.Form["OtherPrice"]);
            model.OperatorContent = Utils.GetFormValue("OperatorContent");
            model.IssueTime = DateTime.Now;
            model.BuyCompanyName = Utils.GetFormValue("hidBuyCompanyName");
            model.ContactName = Utils.GetFormValue("hidContactName");
            model.ContactTel = Utils.GetFormValue("hidContactTel");
            model.ContactQQ = Utils.GetFormValue("hidContactQQ");
            model.ContactMQ = Utils.GetFormValue("hidContactMQ");
            model.LeaveDate = Utils.GetDateTime(Utils.GetFormValue("hidLeaveDate"));
            model.LeaveTraffic = Utils.GetFormValue("Traffic");
            model.SumPrice = model.AdultNumber * model.PersonalPrice + model.ChildNumber * model.ChildPrice + model.MarketNumber * model.MarketPrice + model.OtherPrice;
            //该字段做订单数据操作处理，并不修改数据库值，传入只做判断作用
            model.OrderSource = EyouSoft.Model.TourStructure.TourOrderOperateType.代客预定;
        }
        /// <summary>
        /// 同意留位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnYes_Click(object sender, EventArgs e)
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            EyouSoft.Model.TourStructure.TourOrder model = new EyouSoft.Model.TourStructure.TourOrder();
            model.TourOrderCustomer = TourOrderCustomer();
            model.SeatList = SeatList.Trim(',');
            InputValueToModel(model);
            model.OrderState = EyouSoft.Model.TourStructure.OrderState.已留位;
            if (Utils.GetDateTime(Utils.GetFormValue("txtEndTime$dateTextBox")) <= DateTime.Now)
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('留位日期不能小于等于当前日期！');");
                return;
            }
            if (model.PeopleNumber > Utils.GetInt(Request.Form["hidRemnantNumber"], 0))
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('抱歉，剩余数不能大于游客数，不能保存，请重新填写游客数和游客信息！');");
                return;
            }
            model.SaveSeatDate = Utils.GetDateTime(Utils.GetFormValue("txtEndTime$dateTextBox"));
            int ReturnValue = Ibll.UpdateTourOrder(model);
            Ibll = null;
            model = null;
            switch (ReturnValue)
            {
                case -1:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('写入订单信息失败！');closeWin()");
                    break;
                case 0:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('订单实体为空！');closeWin()");
                    break;
                default:
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('订单保存成功！');closeWin()");
                    break;
            }
        }
        /// <summary>
        /// 删除无效订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            EyouSoft.IBLL.TourStructure.ITourOrder Ibll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            bool isTrue = Ibll.UpdateIsDelete(OrderID);
            Ibll = null;
            if (isTrue)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "alert('执行删除无效订单操作成功！');closeWin()");
            }
            else
            {
                InitPage();
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "执行删除无效订单操作失败！");
            }
        }

        /// <summary>
        /// 获取游客实体集合
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.TourOrderCustomer> TourOrderCustomer()
        {
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> lists = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();
            EyouSoft.Model.TourStructure.TourOrderCustomer model = null;
            int len = Utils.GetFormValues("VisitorName").Length;
            for (int i = 0; i < len; i++)
            {
                model = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                model.VisitorName = Utils.GetFormValues("VisitorName")[i];
                int cType = (int)Utils.GetInt(Utils.GetFormValues("CradType")[i]);
                switch (cType)
                {
                    case 0:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.请选择证件;
                        break;
                    case 1:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.身份证;
                        break;
                    case 2:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.户口本;
                        break;
                    case 3:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.军官证;
                        break;
                    case 4:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.护照;
                        break;
                    case 5:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.边境通行证;
                        break;
                    case 6:
                        model.CradType = EyouSoft.Model.TourStructure.CradType.其他;
                        break;
                }
                SeatList += Utils.GetFormValues("SeatNumber")[i] + ",";
                model.SiteNo = Utils.GetFormValues("SeatNumber")[i];
                model.CradNumber = Utils.GetFormValues("CradNumber")[i];
                model.ContactTel = Utils.GetFormValues("ContactTel")[i];
                model.Sex = Utils.GetFormValues("Sex")[i] == "0" ? false : true;
                model.VisitorType = Utils.GetFormValues("VisitorType")[i] == "0" ? false : true;
                model.Remark = Utils.GetFormValues("Remark")[i];
                lists.Add(model);
                model = null;
            }
            return lists;
        }
        #endregion
        /// <summary>
        /// 初始化游客信息
        /// </summary>
        /// <param name="CustomerList"></param>
        private void InitCustomers(IList<EyouSoft.Model.TourStructure.TourOrderCustomer> CustomerList)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("");
            if (CustomerList != null)
            {
                for (int i = 0; i < CustomerList.Count; i++)
                {
                    int itemtype = CustomerList[i].VisitorType ? 1 : 0;
                    string itemselected = "selected=\"selected\"";
                    int cardType = 0;
                    cardType = (int)CustomerList[i].CradType;
                    strBuild.AppendFormat("<tr bgcolor=\"#ffffff\" itemtype='{0}'>", itemtype);
                    strBuild.AppendFormat(" <td align=\"middle\" bgcolor=\"#EDF6FF\">{0}</td>", i + 1);
                    strBuild.AppendFormat("<td  align=\"left\" bgcolor=\"#EDF6FF\"><input style=\"width:80px;\" value=\"{0}\" name=\"VisitorName\" /> </td>", CustomerList[i].VisitorName);
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"> <select id=\"CradType\" name=\"CradType\">");//证件类型
                    switch (cardType)
                    {
                        case 0:
                            strBuild.Append("<option value=\"0\" selected=\"selected\">请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        case 1:
                            strBuild.Append("<option value=\"0\">请选择证件</option>");
                            strBuild.Append("<option value=\"1\" selected=\"selected\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        case 2:
                            strBuild.Append("<option value=\"0\">请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\" selected=\"selected\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        case 3:
                            strBuild.Append("<option value=\"0\" >请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\" selected=\"selected\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        case 4:
                            strBuild.Append("<option value=\"0\" >请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\" selected=\"selected\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        case 5:
                            strBuild.Append("<option value=\"0\" >请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\" selected=\"selected\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\">其他</option>");
                            break;
                        default:
                            strBuild.Append("<option value=\"0\" >请选择证件</option>");
                            strBuild.Append("<option value=\"1\">身份证</option>");
                            strBuild.Append("<option value=\"2\">户口本</option>");
                            strBuild.Append("<option value=\"3\">军官证</option>");
                            strBuild.Append("<option value=\"4\">护照</option>");
                            strBuild.Append("<option value=\"5\">边境通行证</option>");
                            strBuild.Append("<option value=\"6\" selected=\"selected\">其他</option>");
                            break;
                    }
                    strBuild.Append("</select></td>");
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"><input name=\"CradNumber\" value=\"{0}\" id=\"CradNumber\" style=\"width:120px;\" /></td>", CustomerList[i].CradNumber);
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"><select id=\"Sex\" name=\"Sex\">");
                    if (CustomerList[i].Sex == true)
                    {
                        strBuild.AppendFormat("<option value=\"1\" {0}>男</option>", itemselected);
                        strBuild.Append("<option value=\"0\">女</option>");
                    }
                    else
                    {
                        strBuild.AppendFormat("<option value=\"0\" {0}>女</option>", itemselected);
                        strBuild.Append("<option value=\"1\">男</option>");
                    }

                    strBuild.Append("</select></td>");
                    //成人,儿童
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\">");
                    if (CustomerList[i].VisitorType)
                    {
                        strBuild.Append("<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"1\" type=\"hidden\" />成人");
                    }
                    else
                    {
                        strBuild.Append("<input id=\"hidVisitorType\" name=\"VisitorType\" value=\"0\" type=\"hidden\" />儿童");
                    }
                    strBuild.Append("</td>");
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"><input name=\"SeatNumber\" value=\"{0}\" id=\"SeatNumber\"  style=\"width:40px;\" /></td>", CustomerList[i].SiteNo);
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"><input name=\"ContactTel\" value=\"{0}\" id=\"ContactTel\"  style=\"width:100px;\" /></td>", CustomerList[i].ContactTel);
                    strBuild.AppendFormat("<td align=\"left\"  bgcolor=\"#EDF6FF\"><input id=\"Remark\" value=\"{0}\" name=\"Remark\"  style=\"width:100px;\" /> </td>", CustomerList[i].Remark);
                    strBuild.Append("</tr>");
                }
                RemnantNumber = CustomerList.Count + ShowRemnantNumber;
            }
            CustomerHtml = strBuild.ToString();
        }
    }
}
