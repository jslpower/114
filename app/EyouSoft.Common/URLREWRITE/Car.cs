using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 车队Url改写
    /// </summary>
    public class Car
    {
        /// <summary>
        /// 车队首页Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public static string CarDefaultUrl(int cityId)
        {
            return Domain.UserPublicCenter + "/bus_" + cityId.ToString();
        }

        /// <summary>
        /// 车队普通网店Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string CardGeneralCarShopUrl(int cityId, string companyId)
        {
            return Domain.UserPublicCenter + "/bus_1_" + cityId.ToString() + "_" + companyId;
        }
       
        /// <summary>
        /// 车队高级网店
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string CarAdvancedShopUrl(int cityId, string companyId)
        {
            return Domain.UserPublicCenter + "/bus_2_" + cityId.ToString() + "_" + companyId;
        }

        /// <summary>
        /// 车队高级网店
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public static string CarAdvancedShopUrl(string companyId)
        {
            return Domain.UserPublicCenter + "/bus_2_" + companyId;
        }
    }
}
