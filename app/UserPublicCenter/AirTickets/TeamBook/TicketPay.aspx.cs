using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
using AlipayClass;
using tenpay;

namespace UserPublicCenter.AirTickets.TeamBook
{
    public partial class TicketPay : EyouSoft.Common.Control.FrontPage
    {
        protected string companyName;
        protected string contactName;
        protected string address;
        protected string moible;
    
        protected string flightName;//航空公司名
        protected int buyInsCount;//购买保险人数
        protected int buyItineraryCount;//购买行程单人数
        protected EyouSoft.Model.TicketStructure.OrderInfo orderInfo;
        protected EyouSoft.Model.TicketStructure.TicketWholesalersInfo supplierInfo;
        protected IList<EyouSoft.Model.TicketStructure.TicketCompanyAccount> list;//供应商账户信息
        EyouSoft.IBLL.TicketStructure.ITicketOrder orderBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetFormValue("method");
            this.Title = "支付-组团预定/散拼-机票";
            if (method != "InsertAccount")
            {
                this.Master.Naviagtion = AirTicketNavigation.团队预定散拼;
                orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                string orderId = Utils.GetQueryStringValue("orderId");
                orderInfo = orderBll.GetOrderInfoById(orderId);
                EyouSoft.Model.TicketStructure.TicketFlightCompany companyModel = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(orderInfo.FlightId);
               if (companyModel != null)
               {
                   flightName = companyModel.AirportName;
               }

             
                companyName = SiteUserInfo.CompanyName;
                contactName = SiteUserInfo.ContactInfo.ContactName;
                address = "";
                moible = SiteUserInfo.ContactInfo.Mobile;
                buyInsCount = orderInfo.Travellers.Where(i => i.IsBuyIns).Count();//保险数
                buyItineraryCount = orderInfo.Travellers.Where(i => i.IsBuyItinerary).Count();//行程单数
                supplierInfo = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(orderInfo.SupplierCId);
                acl_rptCustomerList.DataSource = orderInfo.Travellers;
                acl_rptCustomerList.DataBind();
                list = EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance().GetTicketCompanyAccountList(orderInfo.SupplierCId);

            }
            else
            {   
                string payWhich=Utils.GetFormValue("paywhich");
                string freightType = Utils.GetFormValue("freightType");
                string orderId = Utils.GetFormValue("orderId");
                string orderNo = Utils.GetFormValue("orderNo");
                string sellCId=Utils.GetFormValue("sellcId");
                string batchNo="";
                decimal total=decimal.Parse(Utils.GetFormValue("total"));
                string companyName = Utils.GetFormValue("companyname");
                string contactName = Utils.GetFormValue("contactname");
                string moible = Utils.GetFormValue("moible");
                string address = Utils.GetFormValue("address");
                string currUId = SiteUserInfo.ID;
                string currCId = SiteUserInfo.CompanyID;
                string theUrl = GetPayUrl(payWhich, orderNo, total,freightType);
                decimal IntoRatio = decimal.Parse(AlipayParameters.TongyeFee);//平台交易费
                orderBll = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance();
                orderBll.UpdateBuyerContact(orderId, companyName, contactName, moible, address);//修改订单联系方式
                EyouSoft.Model.TicketStructure.ItemType? itemType = EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单;//流水金额记录项类型
                IList<EyouSoft.Model.TicketStructure.TicketPay> payList = orderBll.GetPayList(orderId, itemType, orderNo, "");//获取交易记录
                list = EyouSoft.BLL.TicketStructure.TicketCompanyAccount.CreateInstance().GetTicketCompanyAccountList(sellCId);//获取供应商所有账户
                EyouSoft.Model.TicketStructure.TicketAccountType accountType = (EyouSoft.Model.TicketStructure.TicketAccountType)Utils.GetInt(payWhich);//获取支付类型
                 string sellAccount="";//供应商账户
                 EyouSoft.Model.TicketStructure.TicketCompanyAccount accountModel=list.FirstOrDefault(i => i.InterfaceType == accountType);//获取供应商账户实体
                 if (accountModel != null)
                     sellAccount = accountModel.AccountNumber;//赋值供应商账户
                 else
                 {
                     Utils.ResponseMegError();//如果对应接口账户不存在则输出失败
                     return;
                 }
                if(payList==null||payList.Count==0||(payList!=null&&payList.Where(i=>i.PayState==EyouSoft.Model.TicketStructure.PayState.交易完成).Count()<1))//如果交易记录不存在或交易状态不是完成则添加支付前交易记录
                {   
                    
                    if (orderBll.PayBefore(orderId, orderNo,sellAccount,IntoRatio, SiteUserInfo.ID, SiteUserInfo.CompanyID, total,accountType, sellCId, "", out batchNo))
                    {

                        Utils.ResponseMeg(true, theUrl);

                    }
                    else
                    {
                        Utils.ResponseMegError();
                    }
                }

                else if (payList != null && payList.Where(i => i.PayState == EyouSoft.Model.TicketStructure.PayState.交易完成).Count() > 0)
                {
                    Utils.ResponseMeg(false, "你已经支付过，并且交易完成了！");
                }
                else
                {
                    Utils.ResponseMeg(true, theUrl);
                }
               
                
            }

        }


        #region 获取支付链接
        /// <summary>
        /// 获取支付链接
        /// </summary>
        /// <param name="payWhich">支付方式</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="totol">金额</param>
        /// <returns></returns>
        protected string GetPayUrl(string payWhich,string orderNo,decimal total,string freightType)
        {
            string url = "";
            switch (payWhich)//2-支付宝，1-财付通，3-工行，4-建行，5-农行，6-招行，7-其它
            {
                case "2":
                      ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////
                string partner = AlipayParameters.Partner;                                     //合作身份者ID
                string key = AlipayParameters.Key;                         //安全检验码
                string seller_email = AlipayParameters.Seller_mailer;                             //签约支付宝账号或卖家支付宝帐户
                string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
                string notify_url = AlipayParameters.DomainPath + "/AirTickets/alipay/directpay/notify_url.aspx"; //交易过程中服务器通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                string return_url = Domain.UserPublicCenter + "/AirTickets/alipay/directpay/Return_url.aspx"; //付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
                string show_url = Domain.UserPublicCenter + "/AirTickets/alipay/directpay/MyOrder.aspx";                     //网站商品的展示地址，不允许加?id=123这类自定义参数
                string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改
                string antiphishing = "0";                                               //防钓鱼功能开关，'0'表示该功能关闭，'1'表示该功能开启。默认为关闭
                //一旦开启，就无法关闭，根据商家自身网站情况请慎重选择是否开启。
                //申请开通方法：联系我们的客户经理或拨打商户服务电话0571-88158090，帮忙申请开通
                //若要使用防钓鱼功能，建议使用POST方式请求数据

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////
                //必填参数
                string CgsPayInfo = "";
                if (freightType == "1")
                {
                    CgsPayInfo = "航班信息(单程)：" + Utils.GetFormValue("leaveTime") + "/" + Utils.GetFormValue("homeCityName") + "至" + Utils.GetFormValue("destCityName");
                }
                else if (freightType == "2")
                {
                    CgsPayInfo = "航班信息(往返程)：去程" + Utils.GetFormValue("leaveTime") + "/" + Utils.GetFormValue("homeCityName") + "至" + Utils.GetFormValue("destCityName") + ";" + " 回程" + Utils.GetFormValue("returnTime") + "/" + Utils.GetFormValue("destCityName") + "至" + Utils.GetFormValue("homeCityName");
                }
                string out_trade_no = orderNo;//DateTime.Now.ToString("yyyyMMddHHmmss");;  //请与贵网站订单系统中的唯一订单号匹配
                string subject = "同业114机票平台（订单号：" + orderNo + "）";                           //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
                string body = "订单号：" + orderNo + "/" + CgsPayInfo;                           //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
                string total_fee = Utils.GetMoney(total);                              //订单总金额，显示在支付宝收银台里的“应付总额”里


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
                url = aliService.Create_url();
                break;
                case "1":
                //商户号
                string bargainor_id = TenpayParameters.Bargainor_ID;
                //密钥
                string key1 = TenpayParameters.Key;

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
                reqHandler.setParameter("sp_billno", orderNo);				//商家订单号
                reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
                reqHandler.setParameter("return_url", TenpayParameters.DomainPath+"/AirTickets/tenpay/directpay/Return_url.aspx");				//支付通知url
                reqHandler.setParameter("desc", string.Format("同业114机票平台(订单号：{0})", orderNo)); //商品名称
                reqHandler.setParameter("total_fee", Utils.GetMoney(total*100));						//商品金额,以分为单
                reqHandler.setParameter("cs", TenpayParameters.Input_Charset);//设置编码
                //用户的公网ip,测试时填写127.0.0.1,只能支持10分以下交易
                reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);
                //获取请求带参数的url
                 url = reqHandler.getRequestURL();
                 break;
              
                    

            }
            return url;
        }


        #endregion




        //protected string BindAccountType()
        //{

        //    StringBuilder builder = new StringBuilder();

        //    if (list != null && list.Count > 0)
        //    {
        //        foreach (EyouSoft.Model.TicketStructure.TicketCompanyAccount account in list)
        //        {
        //            if (account.IsSign)
        //            {
        //                switch (account.InterfaceType)
        //                {
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.支付宝:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('alipay');\"><img src=\"{0}/images/jipiao/alipay.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.财付通:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('cft');\"><img src=\"{0}/images/jipiao/cft.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.工行:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('cft');\"><img src=\"{0}/images/jipiao/js.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.建行:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('jh');\"><img src=\"{0}/images/jipiao/jh.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.农行:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('nh');\"><img src=\"{0}/images/jipiao/nh.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.招行:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('zh');\"><img src=\"{0}/images/jipiao/zs.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
        //                    case EyouSoft.Model.TicketStructure.TicketAccountType.其他银行:
        //                        builder.AppendFormat("<td align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return InsertAccount('qt');\"><img src=\"{0}/images/jipiao/qt.jpg\" width=\"104\" height=\"32\" /></a></td>", ImageServerUrl);
        //                        break;
                         
        //                }
        //            }
        //        }
        //    }
        //    return builder.ToString();
        //}
    }
}
