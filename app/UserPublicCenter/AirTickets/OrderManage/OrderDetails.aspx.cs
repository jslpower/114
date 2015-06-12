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
using EyouSoft.Common.Function;
using System.Collections.Generic;
using AlipayClass;
using tenpay;
namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票订单详细页
    /// 2010-11
    /// 袁惠
    /// </summary>
    public partial class OrderDetails : EyouSoft.Common.Control.FrontPage
    {
        protected string OrderRateInfo = string.Empty;   //运价信息
        /// <summary>
        /// 回程显示的html
        /// </summary>
        protected string BackRunInfo = string.Empty; 

        protected int BuySafetyCount = 0; //购买保险人数
        protected int BuyRoutingCount = 0; //购买行程单人数
        /// <summary>
        /// 支付类型链接Html
        /// </summary>
        protected string PayHtmlText = string.Empty;
        protected string OrderNo = string.Empty;
        protected string SupplierCId = string.Empty; //供应商公司Id
        protected string TotalAmount = string.Empty;
        protected string GysContactMQ = string.Empty;    //供应商MQ
        protected int ShowControType = 0;     //默认只显示采购商信息修改控件
        protected string OrderStateLog = string.Empty;     //保存当前订单状态
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string orderId = Utils.GetQueryStringValue("OrderId");
                string opearType = Utils.GetFormValue("OpearType");
                if (string.IsNullOrEmpty(orderId) && string.IsNullOrEmpty(opearType))
                {
                    Response.Redirect("CurrentOrderList.aspx");
                    return;
                }
                this.Master.Naviagtion = AirTicketNavigation.机票订单管理;
                this.Title = "订单管理-订单详情_机票";
                OrderOperate(opearType);  //订单操作
                LoadBindData(orderId);
                
            }
        }

        #region 初始化加载基本数据
        private void LoadBindData(string orderId) 
        {
            EyouSoft.Model.TicketStructure.OrderInfo orderInfo = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetOrderInfoById(orderId);
            if (orderInfo != null)
            {
                hidOrderId.Value = orderInfo.OrderId;
                #region 航班信息
                EyouSoft.Model.TicketStructure.TicketFlightCompany companyModel = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(orderInfo.FlightId);
                if (companyModel != null)
                {
                    ltr_gysCompanyName.Text = companyModel.AirportName; //航空公司名
                    companyModel = null;
                }
                ltr_gysGoDate.Text = orderInfo.LeaveTime.ToString("yyyy-MM-dd");
                //ltr_gysGoRun.Text = orderInfo.HomeCityName + "(" + orderInfo.ReturnTime.ToString("hh:mm") + ")-->" + orderInfo.DestCityName + "(" + orderInfo.LeaveTime.ToString("hh:mm") + ")"; 
                ltr_gysGoRun.Text = orderInfo.HomeCityName + "-->" + orderInfo.DestCityName; 
                ltr_gyshbType.Text = orderInfo.FreightType.ToString();
                ltr_gyslkType.Text = orderInfo.TravellerType.ToString();
                ltr_gysPlaneNum.Text = orderInfo.LFlightCode; //航班号
                ltr_gysLinkPerson.Text = "";
                ltr_gysdllevel.Text = "";

                ltr_OrderNo.Text = orderInfo.OrderNo;
                ltr_OrderPnr.Text = string.IsNullOrEmpty(orderInfo.PNR) == true ? orderInfo.OPNR : "";
                ltr_IsChangePnr.Text = string.IsNullOrEmpty(orderInfo.PNR) == true ? "" : orderInfo.PNR;
                ltr_gysCompanyName1.Text = orderInfo.SupplierCName;
                ltr_TeamType.Text = orderInfo.RateType.ToString();   //团队类型

                #endregion
                #region 供应商详细信息
                EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo gysInfo = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(orderInfo.SupplierCId);//orderInfo.SupplierCId
                if (gysInfo != null)
                {
                    GysContactMQ = gysInfo.ContactMQ;
                    ltr_gysLinkPerson.Text = gysInfo.ContactName;
                    ltr_gysWorkDate.Text = gysInfo.WorkStartTime + "-" + gysInfo.WorkEndTime;
                    ltr_gysMainPage.Text =string.IsNullOrEmpty(gysInfo.WebSite)==true?"":"<a target=\"_blank\" href=\"http://"+gysInfo.WebSite+"\">http://"+ gysInfo.WebSite+"</a>";               //供应商主页
                    ltr_gysLinkPhone.Text = gysInfo.ContactTel;
                    ltr_gysOutTickets.Text =(gysInfo.SuccessRate*100).ToString("F1")+ "%";
                    ltr_gysdllevel.Text = gysInfo.ProxyLev;
                    ltr_gysBackTicAgvDate.Text ="自愿/非自愿：" +gysInfo.RefundAvgTime.ToString() + "/" + gysInfo.NoRefundAvgTime.ToString();//平均退票时间
                    gysInfo = null;
                }
                ltr_gysRemark.Text = orderInfo.OrderRateInfo.SupplierRemark;

                string CgsPayInfo ="";
                //运价信息
                string strhtml = "<tr><td align=\"center\" height=\"25\">{0}</td><td align=\"center\">￥{1}</td><td align=\"center\"><font color=\"#ff6600\">{2}</font></td><td align=\"center\">{3}</td><td align=\"center\"><span class=\"jiesuanjia\">￥{4}</span></td><td align=\"center\">{5}</td><td align=\"center\">￥{6}</td></tr>";
                //回程显示的html
                string backrunhtml="<td align=\"center\" height=\"25\">回程：</td> <td align=\"left\">{0}</td><td align=\"left\">返回时间：{1}</td><td width=\"12%\" align=\"center\">航班号：{2}</td><td align=\"left\">旅客类型：{3}</td>";
                //单程信息
                OrderRateInfo = string.Format(strhtml, "去程",Utils.GetMoney(orderInfo.OrderRateInfo.LeaveFacePrice),Utils.GetMoney(orderInfo.OrderRateInfo.LeaveDiscount) + "%", orderInfo.OrderRateInfo.LeaveTimeLimit,Utils.GetMoney( orderInfo.OrderRateInfo.LeavePrice), orderInfo.OrderRateInfo.MaxPCount.ToString(),Utils.GetMoney( orderInfo.OrderRateInfo.LFuelPrice) + "/" +Utils.GetMoney( orderInfo.OrderRateInfo.LBuildPrice));               
                if (orderInfo.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                {
                    CgsPayInfo = "航班信息(往返程)：去程" + orderInfo.LeaveTime.ToString("yyyy-MM-dd") + "/" + orderInfo.HomeCityName + "至" + orderInfo.DestCityName + ";" + " 回程" + orderInfo.ReturnTime.ToString("yyyy-MM-dd") + "/" + orderInfo.DestCityName + "至" + orderInfo.HomeCityName ;
                    //航班回程信息
                    OrderRateInfo += string.Format(strhtml, "回程", Utils.GetMoney(orderInfo.OrderRateInfo.ReturnFacePrice), Utils.GetMoney(orderInfo.OrderRateInfo.ReturnDiscount) + "%", orderInfo.OrderRateInfo.ReturnTimeLimit, Utils.GetMoney(orderInfo.OrderRateInfo.ReturnPrice), orderInfo.OrderRateInfo.MaxPCount, Utils.GetMoney(orderInfo.OrderRateInfo.RFuelPrice) + "/" + Utils.GetMoney(orderInfo.OrderRateInfo.RBuildPrice)); 
                    //显示回程信息
                    //string cityinfo = orderInfo.DestCityName + "(" + orderInfo.ReturnTime.ToString("hh:mm") + ")-->" + orderInfo.HomeCityName + "(" + orderInfo.LeaveTime.ToString("hh:mm") + ")";  //回程格式
                    string cityinfo = orderInfo.DestCityName +"-->" + orderInfo.HomeCityName;  //回程格式
                    BackRunInfo = string.Format(backrunhtml, cityinfo, orderInfo.ReturnTime.ToString("yyyy-MM-dd"), orderInfo.RFlightCode, orderInfo.TravellerType.ToString());
                }
                if (orderInfo.FreightType == EyouSoft.Model.TicketStructure.FreightType.单程)
                {
                    CgsPayInfo = "航班信息(单程)：" + orderInfo.LeaveTime.ToString("yyyy-MM-dd") + "/" + orderInfo.HomeCityName + "至" + orderInfo.DestCityName;
                }
                #endregion
                #region 旅客信息
                ltr_perCount.Text = orderInfo.PCount.ToString();
                IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> traveller = orderInfo.Travellers;
                if (traveller != null && traveller.Count > 0)
                {
                    crptPassengersList.DataSource = traveller;
                    crptPassengersList.DataBind();
                }
                BuySafetyCount = orderInfo.Travellers.Where(i => i.IsBuyIns).Count();//保险数
                BuyRoutingCount = orderInfo.Travellers.Where(i => i.IsBuyItinerary).Count();//行程单数

                #endregion

                #region 支付方式和支付金额
                ltr_PassCount.Text = orderInfo.PCount.ToString();
                ltr_TotalPrice.Text =orderInfo.TotalAmount.ToString("0.00");
                #endregion

                #region 采购商联系方式
                txtcgsAddress.Value = orderInfo.BuyerContactAddress;//SiteUserInfo.ContactInfo//联系人地址
                txtcgsComapanyName.Value = orderInfo.BuyerCName;
                txtcgsLinkName.Value = orderInfo.BuyerContactName;
                txtcgsTel.Value = orderInfo.BuyerContactMobile;
                txaExpressRemark.Value = orderInfo.BuyerRemark; //特殊备注
                #endregion
                #region 订单处理状态
                IList<EyouSoft.Model.TicketStructure.TicketOrderLog> orderLogs = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetTicketOrderState(orderInfo.OrderId);
                if (orderLogs.Count > 0 && orderLogs != null)
                {
                    crptOrdState.DataSource = orderLogs;
                    crptOrdState.DataBind();
                    OrderStateLog= orderLogs[orderLogs.Count - 1].State;  //当前订单状态赋值
                    if (OrderStateLog != "申请改期" && OrderStateLog != "申请退票" && OrderStateLog != "申请作废" && OrderStateLog != "申请改签")
                    {
                        OrderStateLog = string.Empty;
                    }
                    foreach (EyouSoft.Model.TicketStructure.TicketOrderLog item in orderLogs)
                    {
                        item.Remark = CutStr(item.Remark, 50);
                    }
                    orderLogs = null;
                }
                #endregion

                switch (orderInfo.OrderState)
                {
                    case EyouSoft.Model.TicketStructure.OrderState.等待审核:
                    case EyouSoft.Model.TicketStructure.OrderState.拒绝审核:
                        divOperateOrder.Visible = false;  //支付操作div
                        divPayType.Visible = false; //支付类型div
                        divSpecRemark.Visible = false; //特殊备注div
                        break;
                    case EyouSoft.Model.TicketStructure.OrderState.出票完成:
                         divPayType.Visible = false; //支付类型div
                         divOperateOrder.Visible = true;
                         #region 绑定退作废控件值
                         sltBlack.Items.Add(new ListItem("-请选择-", "0"));
                         sltBlankOut.Items.Add(new ListItem("-请选择-", "0"));
                         string[] arrback = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.RefundTicketType));
                         if (arrback.Length > 0 && arrback != null)
                         {
                             foreach (string item in arrback)
                             {
                                 if ((EyouSoft.Model.TicketStructure.RefundTicketType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.RefundTicketType), item) == EyouSoft.Model.TicketStructure.RefundTicketType.当日作废)
                                 {
                                     sltBlankOut.Items.Add(new ListItem(item, item));
                                     break;
                                 }
                                 else
                                 {
                                     sltBlack.Items.Add(new ListItem(item, item));
                                 }
                             }
                         }
  
                         #endregion

                         break;
                    case EyouSoft.Model.TicketStructure.OrderState.审核通过:
                         divOperateOrder.Visible = false;
                        //根据当前采购商用户绑定的支付类型显示支付链接
                         ShowCgSPayType(orderInfo.OrderNo, orderInfo.TotalAmount, CgsPayInfo, orderInfo.SupplierCId,orderInfo.OrderId);
                         OrderNo = orderInfo.OrderNo;
                         SupplierCId = orderInfo.SupplierCId;
                         TotalAmount = orderInfo.TotalAmount.ToString();
                         break;
                    case EyouSoft.Model.TicketStructure.OrderState.拒绝出票:
                    case EyouSoft.Model.TicketStructure.OrderState.支付成功:
                         divOperateOrder.Visible = false;  //支付操作div
                         divPayType.Visible = false; //支付类型div
                         break;
                    case EyouSoft.Model.TicketStructure.OrderState.无效订单:
                         divOperateOrder.Visible = false;  //支付操作div
                         divPayType.Visible = false; //支付类型div
                         divSpecRemark.Visible = false; //特殊备注div
                         divcgsInfo.Visible = false;
                         break;
                    default:
               
                        break;
                }
                traveller = null;
                orderInfo = null;
            }
            else
            {
                Response.Redirect("CurrentOrderList.aspx");
                return;
            }
        }
        #endregion
        #region 订单处理操
        /// <param name="opearType">操作类型</param>
        private void OrderOperate(string opearType)
        {
            string OrderNum = Utils.GetFormValue("OrderId");   //订单ID
            bool result = false;     //保存操作结果
            string ErrorStr = string.Empty;
            if(!string.IsNullOrEmpty(opearType))
            {
                if (opearType == "0")        //只能执行采购商信息修改操作
                {
                    string mobile=Utils.GetFormValue("Mobile");
                    if (!Utils.IsMobile(mobile))
                    {
                        Utils.ResponseMeg(false, "采购商手机号码填写错误！");
                        return;
                    }
                    result = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().UpdateBuyerContact(OrderNum, Utils.GetFormValue("CompanyName"), Utils.GetFormValue("LinkName"), mobile, Utils.GetFormValue("Address"));                   
                }
                else if(opearType=="5")         //服务备注
                {
                    result = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().UpdateServiceRemark(OrderNum, Utils.GetFormValue("Content"));
                }
                else if (opearType == "6")      //修改特殊备注
                {
                    result = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().UpdateBuyerRemark(OrderNum, Utils.GetFormValue("Content"));
                }
                else if (opearType == "InsertAccount")  //支付前处理操作
                {
                    string orderId = Utils.GetFormValue("orderId");
                    string orderNo = Utils.GetFormValue("orderNo");
                    string sellCId = Utils.GetFormValue("sellcId");
                    string batchNo = "";
                    string total = Utils.GetFormValue("total");
                    string currUId = SiteUserInfo.ID;
                    string currCId = SiteUserInfo.CompanyID;
                    string payType=Utils.GetFormValue("PayType") ;
                    decimal IntoRatio = decimal.Parse(AlipayParameters.TongyeFee);      //平台交易费
                    EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll =EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();
                    //当前只能判断支付宝接口的帐户
                    IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list = bll.GetTicketCompanyAccountList(sellCId);
                    EyouSoft.Model.TicketStructure.ItemType? itemType = EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单;//流水金额记录项类型
                    IList<EyouSoft.Model.TicketStructure.TicketPay> payList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetPayList(orderId, itemType, orderNo, "");//获取交易记录
                    EyouSoft.Model.TicketStructure.TicketAccountType accountType = (EyouSoft.Model.TicketStructure.TicketAccountType)Utils.GetInt(payType);//获取支付类型
                    string sellAccount = "";//供应商账户
                    EyouSoft.Model.TicketStructure.TicketCompanyAccount accountModel = list.FirstOrDefault(i => i.InterfaceType == accountType);//获取供应商账户实体
                    if (accountModel != null)
                        sellAccount = accountModel.AccountNumber;//赋值供应商账户
                    else
                    {
                        Utils.ResponseMegError();//如果对应接口账户不存在则输出失败
                        return;
                    }
                    if (payList == null || payList.Count == 0 || (payList != null && payList.Where(i => i.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() < 1))
                    {
                        result = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayBefore(orderId, orderNo, sellAccount, IntoRatio, SiteUserInfo.ID, SiteUserInfo.CompanyID, Convert.ToDecimal(total), accountType, sellCId, "", out batchNo);
                    }
                    else if (payList != null && payList.Where(i => i.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() > 0)
                    {
                        Utils.ResponseMeg(false, "你已经支付过，并且交易完成了！");
                        return;
                    }
                    else
                    {
                        result = true;
                    }
                }
                else        //退/作废/改/签操作
                {
                    EyouSoft.Model.TicketStructure.OrderChangeInfo changeinfo = new EyouSoft.Model.TicketStructure.OrderChangeInfo();
                    changeinfo.ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
                    changeinfo.ChangeTime = DateTime.Now;
                    changeinfo.ChangeUId = this.SiteUserInfo.ID;
                    changeinfo.OrderId = OrderNum;
                    changeinfo.ChangeId = Guid.NewGuid().ToString();
                    changeinfo.Travellers = new List<string>();
                    changeinfo.ChangeUFullName = SiteUserInfo.UserName;
                    //IList<string> ids = null;
                    string strids = Utils.GetFormValue("CheckperIds");
                    if (string.IsNullOrEmpty(strids))
                    {
                        Utils.ResponseMeg(false, "请选择旅客！");
                        return;
                    }
                    else
                    {
                        //ids = new List<string>();
                        foreach (string item in StringValidate.Split(strids, ","))
                        {
                            changeinfo.Travellers.Add(item);
                        }
                    }
                    IsTravellerStateSucces(changeinfo.Travellers);   //判断某旅客状态是否为退票成功
                    switch (opearType)
                    {
                        case "1":        //退票                      
                            changeinfo.RefundTicketType = (EyouSoft.Model.TicketStructure.RefundTicketType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.RefundTicketType), Utils.GetFormValue("Content"));
                            changeinfo.ChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.退票;
                            changeinfo.ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
                            break;
                        case "2":    //作废                            
                            changeinfo.RefundTicketType = (EyouSoft.Model.TicketStructure.RefundTicketType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.RefundTicketType), Utils.GetFormValue("Content"));
                            changeinfo.ChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.作废;
                            changeinfo.ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
                            break;
                        case "3":    //改期
                            changeinfo.ChangeRemark = Utils.GetFormValue("Content");
                            changeinfo.ChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.改期;
                            changeinfo.ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
                            break;
                        case "4":    //改签
                            changeinfo.ChangeRemark = Utils.GetFormValue("Content");
                            changeinfo.ChangeType = EyouSoft.Model.TicketStructure.OrderChangeType.改签;
                            changeinfo.ChangeState = EyouSoft.Model.TicketStructure.OrderChangeState.申请;
                            break;
                        default:
                            break;
                    }
                    switch (EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().SetOrderChange(changeinfo))
                    {
                        case 0:
                            result = true;
                            break;
                        case 1:
                            result = false;
                            ErrorStr = "不能申请旅客状态变更!";
                            break;
                        case 2:
                            result = false;
                            break;
                    }                      
                }
                if(result)     //根据操作结果提示
                {
                    Utils.ResponseMeg(true, "操作成功！");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(ErrorStr))
                    {
                        ErrorStr = "操作失败！";
                    }
                    Utils.ResponseMeg(false, ErrorStr);
                    return;
                }
            }
        }
        #endregion   
   
        #region 判断供应商加入的支付圈类型
        /// <param name="OrderNo">订单编号</param>
        /// <param name="planeInfo">航班信息(用户显示订单详细信息)</param>
        /// <param name="TotalAmount">总金额</param>
        /// <param name="SupplierCId">供应商公司编号</param>
        private void ShowCgSPayType(string OrderNo, decimal TotalAmount, string planeInfo, string SupplierCId,string OrderId)
        {
            //判断用户绑定的支付帐户信息 是否已经成功加入支付圈
            EyouSoft.IBLL.TicketStructure.ITicketCompanyAccount bll =
                EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance();

            //当前只能判断支付宝接口的帐户
            IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> accountList =
                bll.GetTicketCompanyAccountList(SupplierCId);
            if (accountList != null && accountList.Count > 0)
            {
                foreach (EyouSoft.Model.TicketStructure.TicketCompanyAccount account in accountList)
                {
                    if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                    {
                        if (account.IsSign)//已经签约
                        {

                            divPayType.Visible = true;
                            #region 支付配置
                            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////
                            string partner = AlipayParameters.Partner;                                     //合作身份者ID
                            string key = AlipayParameters.Key;                         //安全检验码
                            string seller_email = AlipayParameters.Seller_mailer;                             //签约支付宝账号或卖家支付宝帐户
                            string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
                            string notify_url = AlipayParameters.DomainPath + "/AirTickets/alipay/directpay/notify_url.aspx"; //交易过程中服务器通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                            string return_url = Domain.UserPublicCenter + "/AirTickets/alipay/directpay/Return_url.aspx"; //付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                            string show_url = Domain.UserPublicCenter + "/AirTickets/OrderManage/OrderDetails.aspx?OrderId="+OrderId;                     //网站商品的展示地址，不允许加?id=123这类自定义参数
                            string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改
                            string antiphishing = "0";                                               //防钓鱼功能开关，'0'表示该功能关闭，'1'表示该功能开启。默认为关闭
                            //一旦开启，就无法关闭，根据商家自身网站情况请慎重选择是否开启。
                            //申请开通方法：联系我们的客户经理或拨打商户服务电话0571-88158090，帮忙申请开通
                            //若要使用防钓鱼功能，建议使用POST方式请求数据

                            ////////////////////////////////////////////////////////////////////////////////////////////////////

                            ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////
                            //必填参数
                            string out_trade_no = OrderNo;  //请与贵网站订单系统中的唯一订单号匹配
                            string subject = "同业114机票平台（订单号：" + OrderNo + "）";                                    //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                            string body = "订单号：" + OrderNo + "/"+planeInfo;                               //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
                            string total_fee = Utils.GetMoney(TotalAmount);                                    //订单总金额，显示在支付宝收银台里的“应付总额”里


                            //扩展功能参数——网银提前
                            //string paymethod = "bankPay";                                   //默认支付方式，四个值可选：bankPay(网银); cartoon(卡通); directPay(余额); CASH(网点支付)
                            //string defaultbank = "CMB";                                     //默认网银代号，代号列表见http://club.alipay.com/read.php?tid=8681379
                            //zxb 修改 此处暂不需要网银提前功能
                            string paymethod = "directPay";
                            string defaultbank = "";

                            //扩展功能参数——防钓鱼
                            string encrypt_key = "";                                        //防钓鱼时间戳，初始值
                            string exter_invoke_ip = "";                                    //客户端的IP地址，初始值
                            if (antiphishing == "1")
                            {
                                encrypt_key = AlipayFunction.Query_timestamp(partner);
                                exter_invoke_ip = "";                                       //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
                            }

                            //扩展功能参数——其他
                            string extra_common_param = "";                //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
                            string buyer_email = "";			                            //默认买家支付宝账号

                            //扩展功能参数——分润(若要使用，请按照注释要求的格式赋值)
                            string royalty_type = "";                                   //提成类型，该值为固定值：10，不需要修改
                            string royalty_parameters = "";
                            //提成信息集，与需要结合商户网站自身情况动态获取每笔交易的各分润收款账号、各分润金额、各分润说明。最多只能设置10条
                            //提成信息集格式为：收款方Email_1^金额1^备注1|收款方Email_2^金额2^备注2
                            //如：
                            //royalty_type = "10";
                            //royalty_parameters = "111@126.com^0.01^分润备注一|222@126.com^0.01^分润备注二";

                            //扩展功能参数——自定义超时(若要使用，请按照注释要求的格式赋值)
                            //该功能默认不开通，需联系客户经理咨询
                            string it_b_pay = "";  //超时时间，不填默认是15天。八个值可选：1h(1小时),2h(2小时),3h(3小时),1d(1天),3d(3天),7d(7天),15d(15天),1c(当天)

                            /////////////////////////////////////////////////////////////////////////////////////////////////////

                            //构造请求函数
                            AlipayService aliService = new AlipayService(partner, seller_email, return_url, notify_url, show_url, out_trade_no, subject, body, total_fee, paymethod, defaultbank, encrypt_key, exter_invoke_ip, extra_common_param, buyer_email, royalty_type, royalty_parameters, it_b_pay, key, input_charset, sign_type);

                            //GET方式传递
                            string url = aliService.Create_url();
                            PayHtmlText += "<a href=" + url + " onclick=\"return OrderDetails.InsertAccount('2');\"><img border='0' src='" + Domain.ServerComponents + "/images/jipiao/alipay.jpg' /></a>"+ "&nbsp;&nbsp;&nbsp;";

                            #endregion
                        }                    
                    }
                    if (account.InterfaceType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                    {
                        if (account.IsSign) //供应商已经签约财付通
                        {
                            divPayType.Visible = true;
                            #region 财付通支付配置
                            //商户号
                            string bargainor_id =TenpayParameters.Bargainor_ID;
                            //密钥
                            string key1 =TenpayParameters.Key;

                            //当前时间 yyyyMMdd
                            string date = DateTime.Now.ToString("yyyyMMdd");
                            ////生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
                            string strReq = "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);

                            ////商户订单号，不超过32位，财付通只做记录，不保证唯一性
                            //string sp_billno = strReq;

                            // 财付通交易单号，规则为：10位商户号+8位时间（YYYYmmdd)+10位流水号 ,商户根据自己情况调整，只要保证唯一及符合规则就行
                            string transaction_id = bargainor_id + date + strReq;


                            //创建PayRequestHandler实例
                            PayRequestHandler reqHandler = new PayRequestHandler(Context);
                            //初始化
                            reqHandler.init();
                            //设置密钥
                            reqHandler.setKey(key1);
                            //-----------------------------
                            //设置支付参数
                            //-----------------------------
                            reqHandler.setParameter("bargainor_id", bargainor_id);			//商户号
                            reqHandler.setParameter("sp_billno", OrderNo);				//商家订单号
                            reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
                            reqHandler.setParameter("return_url", TenpayParameters.DomainPath + "/AirTickets/tenpay/directpay/Return_url.aspx");				//支付通知url
                            reqHandler.setParameter("desc", string.Format("同业114机票平台(订单号：{0})", OrderNo)); //商品名称
                            reqHandler.setParameter("total_fee", Utils.GetMoney(TotalAmount * 100));						//商品金额,以分为单
                            reqHandler.setParameter("cs", TenpayParameters.Input_Charset);//设置编码
                            //用户的公网ip,测试时填写127.0.0.1,只能支持10分以下交易
                            reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);

                            //获取请求带参数的url
                            string requestUrl = reqHandler.getRequestURL();

                            //Get的实现方式
                            string a_link = "<a  onclick=\"return OrderDetails.InsertAccount('1')\" href=\"" + requestUrl + "\"><img border='0' src='" + Domain.ServerComponents + "/images/jipiao/cft.jpg'  /></a>";
                            PayHtmlText += a_link + "&nbsp;&nbsp;&nbsp;";
                            #endregion
                        }
                    }
                }
            }

        }
        #endregion
        /// <summary>
        /// 获取当前某旅客状态
        /// </summary>
        /// <param name="TState">当前某旅客状态</param>
        /// <returns></returns>
        protected string GetTravellerState(string TState)
        {
            switch ((EyouSoft.Model.TicketStructure.TravellerState)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TravellerState),TState))
            {
                //case EyouSoft.Model.TicketStructure.TravellerState.作废成功:
                //case EyouSoft.Model.TicketStructure.TravellerState.改期成功:
                //case EyouSoft.Model.TicketStructure.TravellerState.改签成功:
                //case EyouSoft.Model.TicketStructure.TravellerState.退票成功:

                //    break;
                case EyouSoft.Model.TicketStructure.TravellerState.拒绝作废:
                case EyouSoft.Model.TicketStructure.TravellerState.拒绝改期:
                case EyouSoft.Model.TicketStructure.TravellerState.拒绝改签:
                case EyouSoft.Model.TicketStructure.TravellerState.拒绝退票:
                case EyouSoft.Model.TicketStructure.TravellerState.正常:
                   TState= EyouSoft.Model.TicketStructure.TravellerState.正常.ToString();
                    break;
            }
            return TState;
        }
        /// <summary>
        /// 判断执行申请退票或者申请作废的旅客是否，已经申请成功过如果已经申请成功就不能申请两者
        /// </summary>
        /// <param name="TreIds">旅客编号</param>
        protected void IsTravellerStateSucces(IList<string>  TreIds)
        {
            string orderNo=Utils.GetFormValue("OrderNo");
            IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellerList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetTravells(orderNo);   //根据订单编号获取改订单旅客集合

            if (travellerList != null && travellerList.Count > 0)
            {
                for (int i = 0; i < travellerList.Count; i++)
                {
                    for (int j = 0; j < TreIds.Count; j++)
                    {
                        if (travellerList[i].TravellerId == TreIds[j])
                        {
                            if (travellerList[i].TravellerState == EyouSoft.Model.TicketStructure.TravellerState.退票成功 || travellerList[i].TravellerState == EyouSoft.Model.TicketStructure.TravellerState.作废成功)
                            {
                                Utils.ResponseMeg(false,"选择的旅客中存在退票成功或者作废成功的旅客，这类旅客不能再执行订单处理操作！");
                                return;
                            }
                        }
                    }
                }
            }
        }
         /// 截取字符串，不限制字符串长度
         /// 
         /// 待截取的字符串
         /// 每行的长度，多于这个长度自动换行
        public string CutStr(string str,int len)
        { 
            string s="";

             for (int i = 0; i < len; i++)
             {
                 int r = i % len;
                 int last = (str.Length / len) * len;
                 if (i != 0 && i <= last)
                 {

                     if (r == 0)
                     {
                         s += str.Substring(i - len, len) + "";
                     }

                 }
                 else if (i > last)
                 {
                     s += str.Substring(i - 1);
                     break;
                 }

             }
            return s;
         }

    }
}
