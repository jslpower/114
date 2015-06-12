using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tenpay;
using System.Web;

namespace tenpay
{
    public class trustRefundQuerys : BaseSplitRequestHandler
    {
        /// <summary>
        /// 委托退款业务查询
        /// </summary>
        /// <param name="bargainorId">商家的商户号,由腾讯公司唯一分配</param>
        /// <param name="purchaserId">用户(买方)的财付通账号(QQ或EMAIL)</param>
        /// <param name="key">密匙</param>
        /// <param name="context"></param>
        public trustRefundQuerys(string bargainorId, string purchaserId, string key, HttpContext context)
            : base(context)
        {
            this.setParameter("cmdno", "99");//设置业务参数：委托退款业务查询
            this.setParameter("bargainor_id", bargainorId);		//商户号
            this.setParameter("purchaser_id", purchaserId);//用户(买方)的财付通账号(QQ或EMAIL)
            this.setParameter("type", "1");//类型：1、委托退款，2、委托代扣；
            this.setParameter("return_url", "http://127.0.0.1/");//后台调用，填写为http://127.0.0.1/
            this.setParameter("version", "4");//版本号必须填4 

            this.setGateUrl("https://mch.tenpay.com/cgi-bin/inquire_entrust_relation.cgi");//设置接口调用地址

            this.setKey(key);//设置密匙
        }
        /// <summary>
        /// 是否存在委托退款关系 
        /// </summary>
        /// <returns></returns>
        public bool IsTrustRefund()
        {
            //通信对象
            TenpayHttpClient httpClient = new TenpayHttpClient();

            //应答对象
            ScriptClientResponseHandler resHandler = new ScriptClientResponseHandler();

            //-----------------------------
            //设置通信参数
            //-----------------------------
            //证书必须放在用户下载不到的目录，避免证书被盗取
            httpClient.setCertInfo(this.httpContext.Server.MapPath(TenpayParameters.PfxPath),TenpayParameters.PfxPwd);

            string requestUrl = this.getRequestURL();
            //设置请求内容
            httpClient.setReqContent(requestUrl);
            //设置超时
            httpClient.setTimeOut(10);

            string rescontent = "";
            //后台调用
            if (httpClient.call())
            {
                //获取结果
                rescontent = httpClient.getResContent();

                resHandler.setKey(this.getKey());
                //设置结果参数
                resHandler.setContent(rescontent);

                //判断签名及结果
                if (resHandler.isTenpaySign())
                {
                    if (resHandler.getParameter("status") == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                    //错误时，返回结果未签名。
                    //如包格式错误或未确认结果的，请使用原来订单号重新发起，确认结果，避免多次操作
                    //Response.Write("业务错误信息或签名错误:" + resHandler.getParameter("pay_result") + "," + resHandler.getParameter("pay_info") + "<br>");
                }

            }
            else
            {
                //后台调用通信失败
                //Response.Write("call err:" + httpClient.getErrInfo() + "<br>" + httpClient.getResponseCode() + "<br>");
                //有可能因为网络原因，请求已经处理，但未收到应答。
                return false;
            }

            return false;
        }
    }
}
