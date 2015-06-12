using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 供求信息
    /// </summary>
    public class SupplierInfo
    {
        /// <summary>
        /// 供求频道URL重写
        /// </summary>
        /// <param name="cityId">不使用</param>
        /// <returns></returns>
        public static string InfoDefaultUrlWrite(int cityId)
        {
            return string.Format("{0}/info",Domain.UserPublicCenter);
        }
        /// <summary>
        /// 供求信息明细页面URL重写
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="cityId">不使用</param>
        /// <returns></returns>
        public static string InfoUrlWrite(string infoId, int cityId)
        {
            return string.Format("{0}/info_{1}", Domain.UserPublicCenter, infoId);
        }

        /// <summary>
        /// 供求信息列表
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="cityId">不使用</param>
        /// <returns></returns>
        public static string InfoListWrite(int pId, int cityId)
        {
            return string.Format("{0}/info_list_{1}", Domain.UserPublicCenter, pId);
        }

        /// <summary>
        /// 供求URL重写
        /// </summary>
        /// <param name="key"></param>
        /// <param name="stime"></param>
        /// <param name="stag"></param>
        /// <param name="stype"></param>
        /// <param name="proid"></param>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static string GetSuplierUrl(string key, int stime, int stag, int stype, int proid, int cat, int cityid)
        {
            string s = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                s = string.Format("/SupplierInfo/ExchangeList.aspx?stime={0}&stag={1}&stype={2}&sProvid={3}&scat={4}&keyword={5}",
                                    stime, stag, stype, proid, cat, key);
            }
            else
            {
                s = string.Format("/info_list_{0}_{1}_{2}_{3}_{4}",
                                    stime, stag, stype, proid, cat);
            }
            return s;
        }
        /// <summary>
        /// 同业学堂
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string SchoolInfo(string id)
        {
            return EyouSoft.Common.Domain.UserPublicCenter + "/schoolinfo_" + id;
        }

        public static string ArticleInfo(string id)
        {
            return EyouSoft.Common.Domain.UserPublicCenter + "/articleinfo_" + id;
        }
    }
}
