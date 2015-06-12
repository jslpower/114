using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{   
    /// <summary>
    /// 静态页面重写的
    /// </summary>
    public  class StaticPage
    {   
        /// <summary>
        /// 开网店url
        /// </summary>
        /// <returns></returns>
        public static string GetCreateShopUrl()
        {
            return Domain.UserPublicCenter + "/CreateShop";
        }
        /// <summary>
        /// 做营销url
        /// </summary>
        /// <returns></returns>
        public static string GetDoMarketUrl()
        {
            return Domain.UserPublicCenter + "/DoMarket";
        }
    }
}
