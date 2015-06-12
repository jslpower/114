using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 景区Url更改
    /// </summary>
    public class ScenicSpot
    {
        /// <summary>
        /// 景区首页Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public static string ScenicDefalutUrl(int cityId)
        {
            return Domain.UserPublicCenter + "/jingqu_" + cityId.ToString();
        }

        /// <summary>
        /// 景区详细页url(新景区  2011-12-08,景区自增编号)
        /// </summary>
        /// <param name="scenicId">景区自增编号</param>
        /// <returns></returns>
        public static string ScenicDetails(int scenicId)
        {
            return Domain.UserPublicCenter + "/jingquinfo_" + scenicId;
        }

        /// <summary>
        /// 景区详细页url(新景区  2011-12-08)
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public static string ScenicDetails(string scenicId)
        {
            return Domain.UserPublicCenter + "/jingquinfo_" + scenicId;
        }

        /// <summary>
        /// 景区普通网店的url
        /// </summary>
        /// <param name="companyId">景区所属公司编号</param>
        /// <returns></returns>
        public static string ScenicGeneralShop(string companyId)
        {
            return string.Format("{0}/GeneralShop/SightCompany_{1}", Domain.SeniorOnlineShop, companyId);
        }

        /// <summary>
        /// 景区普通网店的url
        /// </summary>
        /// <param name="companyId">景区所属公司编号</param>
        /// <param name="cityId">销售城市编号</param>
        /// <returns></returns>
        public static string ScenicGeneralShop(string companyId, int cityId)
        {
            return string.Format("{0}/GeneralShop/SightCompany_{1}_{2}", Domain.SeniorOnlineShop,
                                 companyId, cityId);
        }

        /// <summary>
        /// 景区高级网店url
        /// </summary>
        /// <param name="companyId">景区所属公司编号</param>
        /// <returns></returns>
        public static string ScenicEShop(string companyId)
        {
            return string.Format("{0}/jingqu_2_{1}", Domain.SeniorOnlineShop, companyId);
        }

        /// <summary>
        /// 景区高级网店url
        /// </summary>
        /// <param name="companyId">景区所属公司编号</param>
        /// <param name="cityId">销售城市编号</param>
        /// <returns></returns>
        public static string ScenicEShop(string companyId, int cityId)
        {
            return string.Format("{0}/jingqu_2_{1}_{2}", Domain.SeniorOnlineShop,
                                 companyId, cityId);
        }

        /// <summary>
        /// 景区详细信息URl
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <returns></returns>
        public static string ScenicDetailsUrl(string companyId)
        {
            return Domain.UserPublicCenter + "/jingqu_1_" + companyId;
        }

        public static string ScenicDetailsUrl(string companyId, int cityId)
        {
            return Domain.UserPublicCenter + "/jingqu_1_" + cityId.ToString() + "_" + companyId;
        }

        ///// <summary>
        ///// 景区地图Url
        ///// </summary>
        ///// <param name="provinceId">省份</param>
        ///// <param name="cityid">城市</param>
        ///// <returns></returns>
        //public static string ScenicListUrl(int provinceId, int cityId)
        //{
        //    return Domain.UserPublicCenter + "/jingqumap_" + cityId.ToString() + "_" + provinceId;
        //}

        /// <summary>
        /// 景区高级网店Url
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <param name="companyId">公司Id</param>
        /// <returns></returns>
        public static string ScenicAdvancedShopUrl(string companyId)
        {
            return Domain.SeniorOnlineShop + "/jingqu_2_" + companyId;
        }
    }
}
