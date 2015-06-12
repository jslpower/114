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
using AlipayClass;
using tenpay;

namespace UserBackCenter.TicketsCenter.OrderManage
{
    /// <summary>
    /// 审核、出票、退票、作废、改期、改签、处理拒绝操作
    /// 罗丽娥   2010-10-22
    /// </summary>
    public partial class CancleOrder : EyouSoft.Common.Control.BasePage
    {
        EyouSoft.Model.TicketStructure.OrderInfo orderInfo = null;

        EyouSoft.Model.TicketStructure.OrderChangeInfo orderChangeInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断是否登录
            if (this.IsLogin == false)//没有登录
            {
                //输出提示信息
                Response.Clear();
                Response.Write("请重新登录");
                Response.End();
            }

            string flag = Utils.GetQueryStringValue("flag");//当前请求类型
            if (flag != string.Empty)//请求类型不为空
            {
                //判断请求类型
                if (flag.Equals("save", StringComparison.OrdinalIgnoreCase))//保存数据
                {
                    SaveData();
                }
                else if (flag.Equals("refund", StringComparison.OrdinalIgnoreCase))//检查支付宝退款是否成功
                {
                    IsAlipayRefund();
                }
            }

            //页面初始化
            if (!Page.IsPostBack)
            {
                string OrderId = Utils.GetQueryStringValue("orderid");//订单ID
                this.CancleOrder_hidOrderId.Value = OrderId;//存储订单iD

                //根据订单ID获取订单明细
                EyouSoft.IBLL.TicketStructure.ITicketOrder ibll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                orderInfo = ibll.GetOrderInfoById(OrderId);
                //根据订单ID获取订单最新的变更信息
                orderChangeInfo = ibll.GetLatestChange(OrderId);

                //判断是否有当前订单内容
                if (orderInfo == null)//没有
                {
                    //输出提示信息
                    Response.Clear();
                    Response.Write("当前订单不存在");
                    Response.End();
                }


                EyouSoft.Model.TicketStructure.OrderState? OrderState = null;//请求的订单状态
                EyouSoft.Model.TicketStructure.OrderChangeType? ChangeType = null;//请求的订单变更类型

                //初始化请求订单状态
                int tmpOrderStateid = Utils.GetInt(Request.QueryString["orderstate"],-1);//请求的订单状态ID
                if (tmpOrderStateid != -1)//有效
                {
                    OrderState = (EyouSoft.Model.TicketStructure.OrderState)tmpOrderStateid;
                    this.CancleOrder_hidOrderState.Value = tmpOrderStateid.ToString();
                }

                //初始化请求订单变更类型
                int tmpChangeTypeId = Utils.GetInt(Request.QueryString["changetype"], -1);//请求的订单变更类型ID
                if (tmpChangeTypeId != -1)
                {
                    ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)tmpChangeTypeId;
                    this.CancleOrder_hidChangeType.Value = tmpChangeTypeId.ToString();
                }

                ////请求的订单状态ID是否有效
                //if (tmpOrderStateid != -1)//有效
                //{
                //    //将ID转换为请求的订单状态
                //    OrderState = (EyouSoft.Model.TicketStructure.OrderState)tmpOrderStateid;
                //    this.CancleOrder_hidOrderState.Value = tmpOrderStateid.ToString();

                //    //判断请求的订单状态是否 与订单当前状态是否同步
                //    if (OrderState != orderInfo.OrderState)
                //    {
                //        Response.Clear();
                //        Response.Write("订单状态已经修改");
                //        Response.End();
                //    }
                //}
                //else//无效
                //{
                //    Response.Clear();
                //    Response.Write("参数错误");
                //    Response.End();
                //}

                /*
                * 判断请求的订单处理状态 是否与 订单的当前状态  是否同步
                * */
                if (OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核
                    || OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)//拒绝审核请求，拒绝出票请求
                {
                    if (OrderState != orderInfo.OrderState)
                    {
                        Utils.ResponseNoPermit("当前订单的状态已经被修改");
                    }
                }

                if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)//拒绝 退/废/改/签 请求
                {
                    if (orderChangeInfo == null)
                    {
                        Utils.ResponseNoPermit("当前订单不能【退/废/改/签】处理");
                    }
                    if (ChangeType != orderChangeInfo.ChangeType
                        || orderChangeInfo.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.接受
                        || orderChangeInfo.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.拒绝)
                    {
                        Utils.ResponseNoPermit("当前订单的状态已经被修改");
                    }
                }
            } 
        }

        private bool IsValid(EyouSoft.Model.TicketStructure.OrderInfo orderInfo,
            EyouSoft.Model.TicketStructure.OrderChangeInfo orderChangeInfo,
            EyouSoft.Model.TicketStructure.OrderState? orderState,
            EyouSoft.Model.TicketStructure.OrderChangeType? orderChangeType)
        {
            return false;
        }

        #region 保存
        private void SaveData()
        {
            bool IsResult = false;
            string OrderId = Utils.GetFormValue(this.CancleOrder_hidOrderId.UniqueID);//订单ID
            //根据ID获取订单明细
            EyouSoft.IBLL.TicketStructure.ITicketOrder OrderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            orderInfo = OrderBll.GetOrderInfoById(OrderId);
            //根据订单ID获取订单最新的变更信息
            orderChangeInfo = OrderBll.GetLatestChange(OrderId);

            //判断是否有当前订单内容
            if (orderInfo == null)//没有
            {
                //输出提示信息
                Utils.ResponseMeg(false, "当前订单不存在");
            }

            EyouSoft.Model.TicketStructure.OrderState? OrderState = null;//请求的订单状态
            int tmpOrderStateId = Utils.GetInt(Utils.GetFormValue(this.CancleOrder_hidOrderState.UniqueID), -1);//订单状态ID
            if (tmpOrderStateId != -1)
            {
                OrderState = (EyouSoft.Model.TicketStructure.OrderState)tmpOrderStateId;
            }

            EyouSoft.Model.TicketStructure.OrderChangeType? ChangeType = null;//请求的订单变更类型
            int tmpOrderChangeTypeId = Utils.GetInt(Utils.GetFormValue(this.CancleOrder_hidChangeType.UniqueID), -1);//订单变更类型ID
            if (tmpOrderChangeTypeId !=-1)
            {
                ChangeType = (EyouSoft.Model.TicketStructure.OrderChangeType)tmpOrderChangeTypeId;
            }

            EyouSoft.Model.TicketStructure.OrderInfo OrderModel = orderInfo;//订单明细

            string Remark = Server.UrlDecode(Utils.GetFormValue("txtRemark",250));//备注信息

            /*
             * 根据请求的订单状态，请求的订单变更类型，获取请求类型
             */
            if(OrderState == EyouSoft.Model.TicketStructure.OrderState.等待审核)// 拒绝审核订单请求
            {
                #region 拒绝审核
                //判断当前订单状态与 请求的订单状态是否同步
                if (OrderState == OrderModel.OrderState)
                {
                    IsResult = OrderBll.SupplierNotCheckOrder(OrderId, Remark, SiteUserInfo.ID, SiteUserInfo.CompanyID);
                    Utils.ResponseMeg(IsResult, IsResult ? "订单修改成功" : "订单修改失败，请稍候再试");
                }
                else
                {
                    Utils.ResponseMeg(false, "页面已经过期");
                }
                #endregion
            }
            else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.支付成功)// 拒绝出票订单请求
            {
                #region 拒绝出票
                //判断当前订单状态与 请求的订单状态是否同步
                if (OrderState != OrderModel.OrderState)
                {
                    Utils.ResponseMeg(false, "页面已经过期");
                }
                string BatchNo = string.Empty;
                // 判断是否有写入支付记录
                IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = 
                    OrderBll.GetPayList(OrderModel.OrderId, EyouSoft.Model.TicketStructure.ItemType.平台到采购商_订单, string.Empty, string.Empty);
                if (PayList == null || PayList.Count == 0 || PayList.Where(item => item.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() == 0)
                {
                    // 拒绝出票完成前写入支付明细记录
                    IsResult = OrderBll.NoOutputTicketBefore(OrderId, OrderModel.OrderNo, SiteUserInfo.ID, SiteUserInfo.CompanyID, OrderModel.TotalAmount, OrderModel.PayType, Remark, out BatchNo);
                }

                // 获取当前订单上相关的支付接口信息和账户信息
                #region 获取当前订单上相关的支付接口信息和账户信息
                string AccountNumber = string.Empty;
                string PayNumber = string.Empty;//支付接口返回的交易号
                EyouSoft.Model.TicketStructure.OrderAccountInfo AccountModel = OrderBll.GetOrderAccountInfo(OrderModel.OrderId);
                if (AccountModel != null)
                {
                    AccountNumber = AccountModel.PayAccount;
                    //AccountNumber = "enowalipay1@163.com";
                    PayNumber = AccountModel.PayNumber;
                }
                AccountModel = null;
                #endregion

                #region 退款
                if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.支付宝)
                {
                    #region 支付宝退款
                    string partner = AlipayParameters.Partner;                                     //合作身份者ID
                    string key = AlipayParameters.Key;                         //安全检验码
                    string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
                    string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改

                    string notify_url = AlipayParameters.DomainPath + "/TicketsCenter/alipay/refund/notify3_url.aspx";//通知地址
                    string batch_no = BatchNo;//批次号
                    string refund_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//退款日期
                    string batch_num = "1";//退款数量
                    string detail_data = "";//退款请求数据集

                    //计算支付宝退款金额对应的手续费
                    decimal alipayFee = Refund_Platform_Parameter.ComputeAlipayFee(OrderModel.TotalAmount);
                    if (alipayFee > 0)
                    {
                        Refund_Platform_Parameter parameter = new Refund_Platform_Parameter(
                            PayNumber, OrderModel.TotalAmount.ToString("F2"), Remark,
                            AlipayParameters.Seller_mailer,
                            alipayFee.ToString("F2"),Remark);
                        detail_data = parameter.ToString();
                    }
                    else
                    {
                        Refund_Platform_Parameter parameter = new Refund_Platform_Parameter(PayNumber, OrderModel.TotalAmount.ToString("F2"), Remark);
                        detail_data = parameter.ToString();
                    }
                   

                    RefundNoPwd refund = new RefundNoPwd(partner, key, sign_type, input_charset, notify_url, batch_no, refund_date, batch_num, detail_data);

                    string url = refund.Create_url();

                    CreateSSL ssl = new CreateSSL(url);

                    string responseFromServer = ssl.GetResponse();

                    Distribute_royalty_Result result = new Distribute_royalty_Result(responseFromServer);

                    IsResult = result.IsSuccess;

                    //判断退款请求是否提交成功
                    if (result.IsSuccess == false)//退款请求提交失败
                    {
                        OrderBll.PayAfterCallBack(PayNumber, OrderModel.TotalAmount,
                            EyouSoft.Model.TicketStructure.PayState.未提交到支付接口,
                            EyouSoft.Model.TicketStructure.TicketAccountType.支付宝,
                            string.Empty, result.ErrorCode, OrderModel.OrderNo, DateTime.Now, batch_no);

                        Utils.ResponseMeg(false, "退款请求提交失败，请稍候再试");
                    }
                    else//退款请求提交成功
                    {
                        //因为支付宝的退款成功信息，是通过异步通知的方式通知
                        //返回到客户端后，在客户端需要启用 实时的请求 查询 数据库，查看退款成功或者失败
                        Response.Clear();
                        Response.Write(string.Format("{{success:'1',message:'{0}',paytype:'{1}',batchno:'{2}'}}",
                            "退款请求提交成功，正在退款中...",
                            "2",
                            batch_no));
                        Response.End();
                    }
                    #endregion
                }
                else if (OrderModel.PayType == EyouSoft.Model.TicketStructure.TicketAccountType.财付通)
                {
                    #region 财付通退款
                    //商户号
                    string bargainor_id = TenpayParameters.Bargainor_ID;
                    //密钥
                    string key = TenpayParameters.Key;

                    //创建请求对象
                    BaseSplitRequestHandler reqHandler = new BaseSplitRequestHandler(Context);

                    //通信对象
                    TenpayHttpClient httpClient = new TenpayHttpClient();

                    //应答对象
                    ScriptClientResponseHandler resHandler = new ScriptClientResponseHandler();

                    //-----------------------------
                    //设置请求参数
                    //-----------------------------
                    reqHandler.init();
                    reqHandler.setKey(key);
                    reqHandler.setGateUrl("https://mch.tenpay.com/cgi-bin/refund_b2c_split.cgi");

                    reqHandler.setParameter("cmdno", "93");
                    reqHandler.setParameter("version", "4");
                    reqHandler.setParameter("fee_type", "1");
                    reqHandler.setParameter("bargainor_id", bargainor_id);		//商户号
                    reqHandler.setParameter("sp_billno", OrderModel.OrderNo);				//商家订单号
                    reqHandler.setParameter("transaction_id", PayNumber);	//财付通交易单号
                    reqHandler.setParameter("return_url", "http://127.0.0.1/");			//后台系统调用，必现填写为http://127.0.0.1/
                    reqHandler.setParameter("total_fee", Utils.GetMoney(OrderModel.TotalAmount * 100));
                    //退款ID，同个ID财付通认为是同一个退款,格式为109+10位商户号+8位日期+7位序列号
                    reqHandler.setParameter("refund_id", "109" + bargainor_id + BatchNo);
                    reqHandler.setParameter("refund_fee", Utils.GetMoney(OrderModel.TotalAmount * 100));


                    //-----------------------------
                    //设置通信参数
                    //-----------------------------
                    //证书必须放在用户下载不到的目录，避免证书被盗取
                    httpClient.setCertInfo(Server.MapPath(TenpayParameters.PfxPath), TenpayParameters.PfxPwd);

                    string requestUrl = reqHandler.getRequestURL();
                    //设置请求内容
                    httpClient.setReqContent(requestUrl);
                    //设置超时
                    httpClient.setTimeOut(10);

                    string rescontent = "";

                    IList<EyouSoft.Model.TicketStructure.TicketPay> tmpPayList = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetPayList(string.Empty, EyouSoft.Model.TicketStructure.ItemType.平台到采购商_订单, string.Empty, BatchNo);

                    //后台调用
                    if (httpClient.call())
                    {
                        //获取结果
                        rescontent = httpClient.getResContent();

                        resHandler.setKey(key);
                        //设置结果参数
                        resHandler.setContent(rescontent);

                        //判断签名及结果
                        if (resHandler.isTenpaySign() && resHandler.getParameter("pay_result") == "0")
                        {
                            //取结果参数做业务处理
                            if (tmpPayList != null && tmpPayList.Count > 0)
                            {
                                EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                                // 拒绝出票完成后更新订单状态为‘拒绝出票’，并修改支付明细状态
                                IsResult = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(PayNumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易完成, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, BatchNo);

                                //判断支付记录 修改是否成功
                                if (IsResult)//成功
                                {
                                    Utils.ResponseMeg(true, "款项已经成功退到采购商帐户中，拒绝出票成功");
                                }
                                else//失败
                                {
                                    Utils.ResponseMeg(true, "款项已经成功退到采购商帐户中，订单状态修改失败，请联系客服");
                                }
                                
                                PayModel = null;
                            }
                            tmpPayList = null;
                        }
                        else
                        {
                            string errorCode = resHandler.getParameter("pay_result");
                            //错误时，返回结果未签名。
                            //如包格式错误或未确认结果的，请使用原来订单号重新发起，确认结果，避免多次操作
                            if (tmpPayList != null && tmpPayList.Count > 0)
                            {
                                EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];
                                //修改之前的支付记录 为 交易失败
                                EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(PayNumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, BatchNo);
                                PayModel = null;
                            }
                            IsResult = false;
                            tmpPayList = null;

                            Utils.ResponseMeg(false, "退款失败，修改订单失败（错误码："+errorCode+"）");
                        }

                    }
                    else
                    {
                        //后台调用通信失败
                        if (tmpPayList != null && tmpPayList.Count > 0)
                        {
                            EyouSoft.Model.TicketStructure.TicketPay PayModel = tmpPayList[0];

                            EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().PayAfterCallBack(PayNumber, PayModel.PayPrice, EyouSoft.Model.TicketStructure.PayState.交易失败, PayModel.PayType, string.Empty, PayModel.Remark, PayModel.TradeNo, DateTime.Now, BatchNo);
                            PayModel = null;
                        }
                        IsResult = false;
                        tmpPayList = null;
                        //有可能因为网络原因，请求已经处理，但未收到应答。

                        Utils.ResponseMeg(false, "操作失败，有可能因为网络原因，请求已经处理，但未收到应答。");
                    }

                    #endregion
                }
                #endregion                   
                #endregion
            }
            else if (OrderState == EyouSoft.Model.TicketStructure.OrderState.出票完成)
            {
                if (ChangeType.HasValue == false)
                {
                    Utils.ResponseMeg(false, "参数错误");
                }

                #region 拒绝退票、改签、改期、作废
                string ChangeID = string.Empty;
                EyouSoft.Model.TicketStructure.OrderChangeInfo ChangeModel = orderChangeInfo;
                    //OrderBll.GetLatestChange(OrderModel.OrderId);
                /*
                * 判断请求的订单处理状态 是否与 订单的当前状态  是否同步
                * */
                if (ChangeModel == null)
                {
                    Utils.ResponseNoPermit("当前订单不能进行【退/废/改/签】处理");
                }
                if (ChangeType.Value != ChangeModel.ChangeType
                    || ChangeModel.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.接受
                    || ChangeModel.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.拒绝)
                {
                    Utils.ResponseNoPermit("当前订单的状态已经被修改");
                }
                ChangeID = ChangeModel.ChangeId;
                IsResult = OrderBll.CheckOrderChange(ChangeID, SiteUserInfo.ID, Remark, EyouSoft.Model.TicketStructure.OrderChangeState.拒绝);
                Utils.ResponseMeg(IsResult, IsResult ? "订单修改成功" : "订单修改失败，请稍候再试");
                #endregion
            }
            //return IsResult;
        }
        #endregion

        /// <summary>
        /// 检查支付宝退款是否成功
        /// </summary>
        private void IsAlipayRefund()
        {
            string OrderId = Utils.GetFormValue(this.CancleOrder_hidOrderId.UniqueID);//订单ID
            //根据ID获取订单明细
            EyouSoft.IBLL.TicketStructure.ITicketOrder OrderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
            orderInfo = OrderBll.GetOrderInfoById(OrderId);

            //判断是否有当前订单内容
            if (orderInfo == null)//没有
            {
                //输出提示信息
                Utils.ResponseMeg(false, "当前订单不存在");
            }

            string batchno = Utils.GetQueryStringValue("batchno");//退款批次号

            //根据订单ID,批次号，获取 拒绝出票退款请求 记录
            IList<EyouSoft.Model.TicketStructure.TicketPay> PayList = OrderBll.GetPayList(orderInfo.OrderId, 
                EyouSoft.Model.TicketStructure.ItemType.平台到采购商_订单, string.Empty,batchno);

            EyouSoft.Model.TicketStructure.TicketPay payModel = null;//支付记录

            //判断该请求交易 是否存在
            if (PayList != null && PayList.Count > 0)//存在
            {
                payModel = PayList[0];
                //判断交易 是否成功
                if (payModel.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成)
                {
                    Utils.ResponseMeg(true, "款项已经成功退到采购商帐户中");
                }
                else if (payModel.PayState == EyouSoft.Model.TicketStructure.PayState.交易失败)
                {
                    Utils.ResponseMeg(false, "退款失败（退款错误码："+payModel.Remark+"），请稍候再试");
                }
                else if (payModel.PayState == EyouSoft.Model.TicketStructure.PayState.支付接口正在处理)
                {
                    Response.Clear();
                    Response.Write(string.Format("{{success:'1',message:'支付接口正在处理中...',search:'{0}'}}","1"));
                    Response.End();
                }
                else
                {
                    Utils.ResponseMeg(false, "退款请求提交失败，请稍候再试");
                }
            }
            else//不存在
            {
                Utils.ResponseMeg(false, "退款请求不存在");
            }
        }
    }
}
