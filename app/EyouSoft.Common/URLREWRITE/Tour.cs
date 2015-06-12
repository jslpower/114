using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 线路频道 URL重写 实用程序，DYZ，2010-12-14
    /// </summary>
    public class Tour
    {
        #region 线路栏目
        public static string GetXianLuUrl(int CityId)
        {
            return Domain.UserPublicCenter + "/xianlu_" + CityId;
        }
        #endregion

        #region DefaultTourList 线路区域
        public static string GetDefTourAreaUrl(int TourAreaID, int CityID)
        {
            return Domain.UserPublicCenter + "/TourList_" + TourAreaID + "_" + CityID;
        }
        /// <summary>
        /// 线路普通网店的url
        /// </summary>
        /// <param name="companyId">景区所属公司编号</param>
        /// <returns></returns>
        public static string TourGeneralShop(string companyId)
        {
            return string.Format("{0}/shop_{1}", Domain.SeniorOnlineShop, companyId);
        }

        // DefaultTourList 主题游
        public static string GetDefTourThemeUrl(int TourAreaID, int FieldId, int CityID)
        {
            return Domain.UserPublicCenter + "/TourList_1_" + TourAreaID + "_" + FieldId + "_" + CityID;
        }

        //首页价格
        public static string GetDefTourPriceUrl(int TourAreaID, int FieldId, int CityID)
        {
            return Domain.UserPublicCenter + "/TourList_2_" + TourAreaID + "_" + FieldId + "_" + CityID;
        }
        //月份
        public static string GetDefTourMonthUrl(int TourAreaID, int FieldId, int CityID)
        {
            return Domain.UserPublicCenter + "/Tourlist_4_" + TourAreaID + "_" + FieldId + "_" + CityID;
        }
        //首页行程天数
        public static string GetDefToruDayUrl(int TourAreaID, int Id, int CityID)
        {
            return Domain.UserPublicCenter + "/TourList_3_" + TourAreaID + "_" + Id + "_" + CityID;
        }
        #endregion

        #region 非首页线路区域的
        //全部线路
        public static string GetTourAreaUrl(int TourAreaID, int CityID)
        {
            return Domain.UserPublicCenter + "/xianlu_1_" + TourAreaID + "_" + CityID;
        }

        //主题游
        public static string GetTourThemeUrl(int TourAreaID, int FieldId, int CityID)
        {
            return Domain.UserPublicCenter + "/xianlu_2_" + TourAreaID + "_" + FieldId + "_" + CityID;
        }

        //价格
        public static string GetTourPriceUrl(int TourAreaID, int FieldId, int CityID)
        {
            return Domain.UserPublicCenter + "/xianlu_3_" + TourAreaID + "_" + FieldId + "_" + CityID;
        }

        //行程天数
        public static string GetToruDayUrl(int TourAreaID, int Id, int CityID)
        {
            return Domain.UserPublicCenter + "/xianlu_4_" + TourAreaID + "_" + Id + "_" + CityID;
        }
        //月份
        public static string GetTourMonthUrl(int TourAreaID, int id, int CityID)
        {
            return Domain.UserPublicCenter + "/xianlu_5_" + TourAreaID + "_" + id + "_" + CityID;
        }

        //线路预订
        public static string GetTourToUrl(string TourId, int CityID)
        {
            return Domain.UserPublicCenter + "/tour_" + TourId + "_" + CityID;
        }

        /// <summary>
        /// 线路预定（使用线路数字Id）
        /// </summary>
        /// <param name="intRouteId">线路数字Id</param>
        /// <param name="cityId">当前销售城市编号</param>
        /// <returns></returns>
        public static string GetTourToUrl(long intRouteId, int cityId)
        {
            return Domain.UserPublicCenter + "/tour_" + intRouteId + "_" + cityId;
        }

        //线路预定(普通网店)
        public static string GetTourToUrl(string TourId, int CityID, string CompanyID)
        {
            return Domain.UserPublicCenter + "/tour_" + TourId + "_" + CityID + "_" + CompanyID;
        }
        #endregion

        #region 往期线路
        public static string GetOldTourUrl(string TourId, int CityID)
        {
            return Domain.UserPublicCenter + "/oldxianlu_" + TourId + "_" + CityID.ToString();
        }
        #endregion

        #region 地接
        public static string GetLocalAgencyListUrl(int provinceId, int cityId)
        {
            return Domain.UserPublicCenter + "/dijie_" + provinceId + "_" + cityId + "";
        }
        public static string GetLocalAgencyUrl(string companyId)
        {
            return Domain.UserPublicCenter + "/dijie_" + companyId + "";
        }
        public static string GetLocalAgencyListUrl(int provinceId, int RequestCityId, int cityId)
        {
            return Domain.UserPublicCenter + "/dijie_" + provinceId + "_" + RequestCityId + "_" + cityId + "";
        }
        public static string GetLocalAgencyListUrl(int provinceId)
        {
            return Domain.UserPublicCenter + "/provinces_" + provinceId + "";
        }

        #endregion

        #region 用户后台 行程单打印
        public static string GetTourPrintUrl(string TourId)
        {
            return Domain.UserBackCenter + "/tour_" + TourId;
        }
        #endregion



    }
}
