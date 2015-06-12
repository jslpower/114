using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;
using AlipayClass;
using tenpay;
using System.Web.UI;

namespace UserBackCenter.TicketsCenter.PurchaseRouteShip
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    public class PurchasePay : IHttpHandler
    {
        private EyouSoft.Model.CompanyStructure.CompanyInfo companyModel = null;
        private EyouSoft.Model.TicketStructure.TicketFreightPackageInfo packageModel = null;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string logId = context.Request.QueryString["logId"];
            if (!String.IsNullOrEmpty(logId))
            {
                EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().GetModel(logId);
                string url = "";
                /*
                 常规购买：套餐项目－常规  类型－团队  运价数－19 开始10.1－10.3
套餐购买：套餐项目－华东区  类型－团队  航空公司－CA 始发地－上海 目的地－所有 开始10.1－10.3
促销购买：套餐项目－华东区  类型－团队  航空公司－CA 始发地－上海 目的地－北京 开始10.1－10.3
                */
                string batch_no = "";
                companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(model.CompanyId);
                string bankType = context.Request.QueryString["bankType"];
                if (companyModel == null || String.IsNullOrEmpty(context.Request.QueryString["uid"]) || String.IsNullOrEmpty(bankType))
                {
                    context.Response.Write("error");
                    return;
                }
                switch (bankType)
                {
                    case "ZFB":
                        switch (model.PackageType)
                        {
                            case EyouSoft.Model.TicketStructure.PackageTypes.常规: url = this.GetPayUrlByZFB(model.OrderNo, string.Format("常规购买：套餐项目－{0}、类型－团队、运价数－{1}、开始{2}－{3}", model.PackageName, model.BuyCount.ToString(), model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), "暂无", Utils.GetDecimal((model.SumPrice * 100).ToString("0")) / 100); break;
                            case EyouSoft.Model.TicketStructure.PackageTypes.套餐: url = this.GetPayUrlByZFB(model.OrderNo, string.Format("套餐购买：套餐项目－{0}、类型－团队、航空公司－{1}、始发地－{2}、目的地－{3}、开始{4}－{5}", model.PackageName, model.FlightName, model.HomeCityName, model.DestCityNames, model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), "暂无", Utils.GetDecimal((model.SumPrice * 100).ToString("0")) / 100); break;
                            default: url = this.GetPayUrlByZFB(model.OrderNo, string.Format("促销购买：套餐项目－{0}、类型－团队  航空公司－{1}、始发地－{2}、目的地－{3}、开始{4}－{5}", model.PackageName, model.FlightName, model.HomeCityName, model.DestCityNames, model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), "暂无", Utils.GetDecimal((model.SumPrice * 100).ToString("0")) / 100); break;
                        }

                        EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().AddTicketPay(model.Id, model.OrderNo, context.Request.QueryString["uid"], companyModel.ID, Utils.GetDecimal((model.SumPrice * 100).ToString("0")) / 100, EyouSoft.Model.TicketStructure.TicketAccountType.支付宝, out batch_no); break;
                    case "CFT":
                        switch (model.PackageType)
                        {
                            case EyouSoft.Model.TicketStructure.PackageTypes.常规: url = this.GetPayUrlByCFT(model.OrderNo, string.Format("常规购买：套餐项目-{0}、类型-团队、运价数-{1}、开始{2}-{3}", model.PackageName, model.BuyCount.ToString(), model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), Utils.GetDecimal((model.SumPrice * 100).ToString("0")), context); break;
                            case EyouSoft.Model.TicketStructure.PackageTypes.套餐: url = this.GetPayUrlByCFT(model.OrderNo, string.Format("套餐购买：套餐项目-{0}、类型-团队、航空公司-{1}、始发地－{2}、目的地－{3}、开始{4}－{5}", model.PackageName, model.FlightName, model.HomeCityName, model.DestCityNames, model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), Utils.GetDecimal((model.SumPrice * 100).ToString("0")), context); break;
                            default: url = this.GetPayUrlByCFT(model.OrderNo, string.Format("促销购买：套餐项目－{0}、类型－团队、航空公司－{1}、始发地－{2}、目的地－{3}、开始{4}－{5}", model.PackageName, model.FlightName, model.HomeCityName, model.DestCityNames, model.StartMonth.ToString("yyyy.MM"), model.EndMonth.ToString("yyyy.MM")), Utils.GetDecimal((model.SumPrice * 100).ToString("0")), context); break;
                        }
                        EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().AddTicketPay(model.Id, model.OrderNo, context.Request.QueryString["uid"], companyModel.ID, Utils.GetDecimal((model.SumPrice * 100).ToString("0")) / 100, EyouSoft.Model.TicketStructure.TicketAccountType.财付通, out batch_no);
                        break;
                    default: context.Response.Write("error"); return;
                }


                if (url != "")
                {
                    context.Response.Write(url);
                }
                else
                {
                    context.Response.Write("error");
                }
                return;
            }

            //购买数
            int buyCount = Utils.GetInt(context.Request.QueryString["buyCount"]);
            //公司ID
            string companyId = context.Request.QueryString["companyId"];
            //开始时间
            DateTime? startMonth = Utils.GetDateTimeNullable(context.Request.QueryString["SartDateTime"]);
            //套餐编号
            int packageId = Utils.GetInt(context.Request.QueryString["packageId"]);
            //购买时间类型
            string type = Utils.GetQueryStringValue("type");
            //购买类型: cg = 常规  cs = 套餐 + 促销
            string p = context.Request.QueryString["p"];
            //购买运价类型 2 = 套餐  3 = 促销
            string packageType = context.Request.QueryString["packageType"];

            if (startMonth == null)
            {
                context.Response.Write("error");
                return;
            }

            if (p != null)
            {
                //获得公司信息
                companyModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(companyId);
                if (companyModel == null)
                {
                    context.Response.Write("error");
                    return;
                }
                //获取购买运价信息
                packageModel = EyouSoft.BLL.TicketStructure.FreightPackageInfo.CreateInstance().GetModel(packageId);
                if (packageModel == null)
                {
                    context.Response.Write("error");
                    return;
                }
                decimal onePrice = 0.00M;
                //结束时间计算
                DateTime endTime = DateTime.Now;
                DateTime startDate = Convert.ToDateTime(startMonth);
                //如果开始日期是本月
                if (Convert.ToDateTime(startMonth).Month == DateTime.Now.Month)
                {
                    //计算当月的剩余天数的比例
                    int today = Convert.ToDateTime(startMonth).Day;

                    decimal ratio = Decimal.Parse((DateTime.DaysInMonth(startDate.Year, startDate.Month) - today + 1).ToString("0.00")) / Decimal.Parse(Convert.ToDecimal(DateTime.DaysInMonth(startDate.Year, startDate.Month)).ToString("0.00"));
                    switch (type)
                    {
                        case "1":
                            //单价 = 当月剩余天数比率 * 每月价格
                            onePrice = packageModel.MonthPrice * ratio;
                            endTime = Convert.ToDateTime(startDate.Year + "-" + startDate.Month + "-" + DateTime.DaysInMonth(startDate.Year, startDate.Month));
                            break;
                        case "2":
                            //单价 = 当月剩余天数比率 * 每月价格 + 其它两月的价格
                            onePrice = ratio * packageModel.QuarterPrice / 3 + packageModel.QuarterPrice / 3 * 2;
                            endTime = Convert.ToDateTime(startDate.AddMonths(2).Year + "-" + startDate.AddMonths(2).Month + "-" + DateTime.DaysInMonth(startDate.AddMonths(2).Year, startDate.AddMonths(2).Month));
                            break;
                        default:
                            //单价 = 当月剩余天数比率 * 每月价格 + 其它五月的价格
                            onePrice = ratio * packageModel.HalfYearPrice / 6 + packageModel.HalfYearPrice / 6 * 5;
                            endTime = Convert.ToDateTime(startDate.AddMonths(5).Year + "-" + startDate.AddMonths(5).Month + "-" + DateTime.DaysInMonth(startDate.AddMonths(5).Year, startDate.AddMonths(5).Month));
                            break;
                    }
                }
                //如果开始日期是大于本月
                else
                {
                    //设置开始日期为每月 1 号
                    startDate = Convert.ToDateTime(startDate.Year + "-" + startDate.Month + "-01");

                    switch (type)
                    {
                        case "1":
                            //单价 = 当月剩余天数比率 * 每月价格
                            onePrice = packageModel.MonthPrice;
                            endTime = Convert.ToDateTime(startDate.Year + "-" + startDate.Month + "-" + DateTime.DaysInMonth(startDate.Year, startDate.Month));
                            break;
                        case "2":
                            //单价 = 当月剩余天数比率 * 每月价格 + 其它两月的价格
                            onePrice = packageModel.QuarterPrice;
                            endTime = Convert.ToDateTime(startDate.AddMonths(2).Year + "-" + startDate.AddMonths(2).Month + "-" + DateTime.DaysInMonth(startDate.AddMonths(2).Year, startDate.AddMonths(2).Month));
                            break;
                        default:
                            //单价 = 当月剩余天数比率 * 每月价格 + 其它五月的价格
                            onePrice = packageModel.HalfYearPrice;
                            endTime = Convert.ToDateTime(startDate.AddMonths(5).Year + "-" + startDate.AddMonths(5).Month + "-" + DateTime.DaysInMonth(startDate.AddMonths(5).Year, startDate.AddMonths(5).Month));
                            break;
                    }
                }

                if (p == "cg")
                {
                    if (buyCount == 0 || string.IsNullOrEmpty(companyId) || packageId == 0 || type == "" || string.IsNullOrEmpty(p))
                    {
                        context.Response.Write("error");
                        return;
                    }

                    //获得支付总金额
                    decimal sumPrice = onePrice * buyCount;
                    EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = new EyouSoft.Model.TicketStructure.TicketFreightBuyLog();
                    model.BuyCount = buyCount;
                    model.BuyTime = DateTime.Now;
                    model.CompanyId = companyId;
                    model.EndMonth = endTime;
                    model.FlightId = packageModel.FlightId;

                    EyouSoft.Model.TicketStructure.TicketFlightCompany flightCompanyModel = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(packageModel.FlightId);

                    if (flightCompanyModel != null)
                    {
                        model.FlightName = flightCompanyModel.AirportName;
                    }

                    if (string.IsNullOrEmpty(model.FlightName)) model.FlightName = string.Empty;

                    model.OperatorId = companyModel.ID;
                    model.PackageId = packageId;

                    EyouSoft.Model.TicketStructure.TicketFreightPackageInfo ticketFreightModel = EyouSoft.BLL.TicketStructure.FreightPackageInfo.CreateInstance().GetModel(packageId);

                    if (ticketFreightModel != null)
                    {
                        model.PackageName = ticketFreightModel.PackageName;
                    }
                    model.PackageType = EyouSoft.Model.TicketStructure.PackageTypes.常规;

                    model.PayState = false;
                    model.RateType = EyouSoft.Model.TicketStructure.RateType.团队散拼;
                    model.StartMonth = Convert.ToDateTime(startMonth);
                    model.SumPrice = Utils.GetDecimal((sumPrice * 100).ToString("0")) / 100;

                    bool result = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().Add(model);
                    if (result)
                    {
                        context.Response.Write(model.Id);
                    }
                    else
                    {
                        context.Response.Write("error");
                    }

                }

                if (p == "cs")
                {

                    if (string.IsNullOrEmpty(companyId) || startMonth == null || packageId == 0 || type == "" || string.IsNullOrEmpty(p))
                    {
                        context.Response.Write("error");
                        return;
                    }

                    //获得支付总金额
                    decimal sumPrice = onePrice;
                    EyouSoft.Model.TicketStructure.TicketFreightBuyLog model = new EyouSoft.Model.TicketStructure.TicketFreightBuyLog();
                    model.BuyCount = buyCount;
                    model.BuyTime = DateTime.Now;
                    model.CompanyId = companyId;
                    model.EndMonth = endTime;
                    model.FlightId = packageModel.FlightId;
                    if (EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(packageModel.FlightId) != null)
                    {
                        model.FlightName = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompany(packageModel.FlightId).AirportName;
                    }
                    model.OperatorId = companyModel.ID;
                    model.PackageId = packageId;
                    model.PackageName = EyouSoft.BLL.TicketStructure.FreightPackageInfo.CreateInstance().GetModel(packageId).PackageName;
                    if (packageType == "2")
                    {
                        model.PackageType = EyouSoft.Model.TicketStructure.PackageTypes.套餐;
                    }
                    else
                    {
                        model.PackageType = EyouSoft.Model.TicketStructure.PackageTypes.促销;
                    }
                    model.PayState = false;
                    model.RateType = EyouSoft.Model.TicketStructure.RateType.团队散拼;
                    model.StartMonth = Convert.ToDateTime(startMonth);
                    model.SumPrice = Utils.GetDecimal((sumPrice * 100).ToString("0")) / 100; 
                    model.HomeCityId = packageModel.HomeCityId;
                    model.HomeCityName = GetCityNameById(packageModel.HomeCityId.ToString());
                    model.DestCityIds = packageModel.DestCityIds;
                    model.DestCityNames = GetCityNameById(packageModel.DestCityIds).Replace('、', ',');

                    bool result = EyouSoft.BLL.TicketStructure.FreightBuyLog.CreateInstance().Add(model);
                    if (result)
                    {
                        context.Response.Write(model.Id);
                    }
                    else
                    {
                        context.Response.Write("error");
                    }
                }
            }
        }

        /// <summary>
        /// 支付宝生成接口URL
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="subject_in">订单名称</param>
        /// <param name="body_in">订单描述</param>
        /// <param name="price_in">金额</param>
        /// <returns></returns>
        public string GetPayUrlByZFB(string orderNo, string subject_in, string body_in, decimal price_in)
        {
            ///////////////////////以下参数是需要设置的相关配置参数，设置后不会更改的///////////////////////////
            string partner = AlipayParameters.Partner;                                     //合作身份者ID
            string key = AlipayParameters.Key;                         //安全检验码
            string seller_email = AlipayParameters.Seller_mailer;                             //签约支付宝账号或卖家支付宝帐户
            string input_charset = AlipayParameters.Input_Charset;                                          //字符编码格式 目前支持 gbk 或 utf-8
            string notify_url = AlipayParameters.DomainPath + "/TicketsCenter/alipay/repurchase/notify_url.aspx"; //交易过程中服务器通知的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            string return_url = Domain.UserBackCenter + "/TicketsCenter/alipay/repurchase/Return_url.aspx"; //付完款后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            string show_url = "";//Domain.UserBackCenter + "/TicketsCenter/alipay/repurchase/MyOrder.aspx";                     //网站商品的展示地址，不允许加?id=123这类自定义参数
            string sign_type = AlipayParameters.SignType;                                                //加密方式 不需修改
            string antiphishing = "0";                                               //防钓鱼功能开关，'0'表示该功能关闭，'1'表示该功能开启。默认为关闭
            //一旦开启，就无法关闭，根据商家自身网站情况请慎重选择是否开启。
            //申请开通方法：联系我们的客户经理或拨打商户服务电话0571-88158090，帮忙申请开通
            //若要使用防钓鱼功能，建议使用POST方式请求数据

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////以下参数是需要通过下单时的订单数据传入进来获得////////////////////////////////
            //必填参数
            string out_trade_no = orderNo;  //请与贵网站订单系统中的唯一订单号匹配
            string subject = subject_in;                                   //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string body = body_in;                                  //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string total_fee = price_in.ToString("F2");                                  //订单总金额，显示在支付宝收银台里的“应付总额”里


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
            return aliService.Create_url();
        }


        /// <summary>
        /// 财付通生成URL
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="subject_in"></param>
        /// <param name="price_in"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetPayUrlByCFT(string orderNo, string subject_in, decimal price_in, HttpContext context)
        {
            string url = "";
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
            PayRequestHandler reqHandler = new PayRequestHandler(context);
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
            reqHandler.setParameter("return_url", TenpayParameters.DomainPath + "/TicketsCenter/tenpay/Return_url.aspx");				//支付通知url
            reqHandler.setParameter("desc", subject_in); //商品名称
            reqHandler.setParameter("total_fee", price_in.ToString());						//商品金额,以分为单
            reqHandler.setParameter("cs", TenpayParameters.Input_Charset);
            //用户的公网ip,测试时填写127.0.0.1,只能支持10分以下交易
            reqHandler.setParameter("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);
            //获取请求带参数的url
            url = reqHandler.getRequestURL();

            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetCityNameById(string id)
        {
            string str = "";
            string[] list = id.Split(',');
            EyouSoft.IBLL.TicketStructure.ITicketSeattle cityIbll = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance();
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    EyouSoft.Model.TicketStructure.TicketSeattle model = cityIbll.GetTicketSeattleById(Convert.ToInt32(list[i]));
                    if (model != null)
                    {
                        str += model.Seattle + "、";
                    }
                }
            }
            str = str.TrimEnd('、');
            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
