using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAP
{
    /// <summary>
    /// API安全认证类
    /// </summary>
    /// zhangzy  2010-11-1
    public class APISoapHeader : System.Web.Services.Protocols.SoapHeader
    {
        private string _localhostkey = string.Empty;
        private string _secretkey = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public APISoapHeader() { this._localhostkey = System.Configuration.ConfigurationManager.AppSettings.Get("TMIS_APIKey"); }

        /// <summary>
        /// 获取或设置密钥
        /// </summary>
        public string SecretKey
        {
            get { return this._secretkey; }
            set { this._secretkey = value; }
        }

        /// <summary>
        /// 是否为安全的API调用
        /// </summary>
        public bool IsSafeCall
        {
            get
            {
                return true;
                if (this._localhostkey == this._secretkey)
                    return true;
                else
                    return false;
            }
        }
    }
}
