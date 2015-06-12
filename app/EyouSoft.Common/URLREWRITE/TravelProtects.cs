using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 旅游用品Url改写
    /// </summary>
    public class TravelProtects
    {	
        /// <summary>
        /// 旅游用品首页(列表页)Url
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public static string TravelDefaultUrl(int cityId)
        {
            return Domain.UserPublicCenter + "/products_" + cityId.ToString();
        }

        /// <summary>
        /// 旅游用品普通网店Url
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string TravelDetailsUrl(int cityId, string companyId)
        {
            return Domain.UserPublicCenter + "/products_1_" + cityId.ToString() + "_" + companyId;
        }
        
        /// <summary>
        /// 旅游用品高级网店Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string TravelAdvancedShopUrl(int cityId, string companyId)
        {
            return Domain.UserPublicCenter + "/products_2_" + cityId.ToString() + "_" + companyId;
        }
        /// <summary>
        /// 旅游用品高级网店Url
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string TravelAdvancedShopUrl(string companyId)
        {
            return Domain.UserPublicCenter + "/products_2_" + companyId;
        }
    }
}
