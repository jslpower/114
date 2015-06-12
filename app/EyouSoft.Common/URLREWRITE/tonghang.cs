using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 同行
    /// </summary>
    public class tonghang
    {
        /// <summary>
        /// 同行首页Url
        /// </summary>
        /// <param name="CityId">城市ID</param>
        public static string GetTongHangUrl(string CityId)
        {
            return Domain.UserPublicCenter + "/rival_" + CityId;
        }

        /// <summary>
        /// 同行列表Url
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="proviceId">省份ID</param>
        public static string GetTongHangUrl(string CityId, string proviceId)
        {
            return Domain.UserPublicCenter + "/rival_" + CityId + "_" + proviceId;
        }
        public static string GetTonghangProviceUrl(string ProviceId)
        {
            return Domain.UserPublicCenter + "/province_" + ProviceId;
        }
    }
}
