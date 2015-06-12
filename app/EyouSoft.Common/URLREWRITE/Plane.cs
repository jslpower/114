using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 机票
    /// </summary>
    public  class Plane
    {
        /// <summary>
        /// 机票咨询URL改写
        /// </summary>
       public static string PlanUrlRewrite(string url)
       {
           return Regex.Replace(url, @"NewsDetailInfo.aspx\?TypeID=(\d+)&NewsID=(\d+)&CityId=(\d+)", "$1_$2_$3");
       }
        /// <summary>
        /// 机票栏目Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
       public static string PlaneDefaultUrl(int cityId)
       {
           return Domain.UserPublicCenter + "/jipiao_" + cityId.ToString();
       }
        /// <summary>
        /// 首页特价机票Url
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public static string SpecialFaresUrl(int id,int cityId)
       {
           return Domain.UserPublicCenter + "/jipiaoinfo_" + id + "_" + cityId;
       }
    }
}
