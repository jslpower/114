using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    public static class Hotel
    {
        /// <summary>
        /// 获得酒店栏目的URL
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static string GetHotelBannerUrl(int CityId)
        {
            return Domain.UserPublicCenter + "/hotel_" + CityId.ToString();
        }
        /// <summary>
        /// 获得酒店的详细页URL
        /// </summary>
        /// <param name="hotelCode"></param>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static string GetHotelDetailsUrl(string hotelCode, int CityId)
        {
            return Domain.UserPublicCenter + "/hotel_" + hotelCode + "_" + CityId.ToString();
        }        
    }
}
