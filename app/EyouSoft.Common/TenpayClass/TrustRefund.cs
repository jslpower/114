using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tenpay
{
    /// <summary>
    /// 建立财付通委托退款关系业务
    /// </summary>
    public class TrustRefund
    {
        private string _bargainor_id = string.Empty;
        private string url = "https://www.tenpay.com/cgi-bin/trust/showtrust_refund.cgi?spid=";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bargainor_id">商户ID</param>
        public TrustRefund(string bargainor_id)
        {
            this._bargainor_id = bargainor_id;
        }
        /// <summary>
        /// 创建签约URL
        /// </summary>
        /// <returns></returns>
        public string CreateUrl()
        {
            return url + this._bargainor_id;
        }
    }
}
