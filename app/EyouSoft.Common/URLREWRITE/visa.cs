using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common.URLREWRITE
{
    /// <summary>
    /// 签证
    /// </summary>
    public class visa
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="stateId">国家ID</param>
        public static string GetVisaUrl()
        {
            return Domain.UserPublicCenter + "/visaList";
        }

        /// <summary>
        /// 国家搜索
        /// </summary>
        /// <param name="stateId">国家ID</param>
        public static string GetVisaUrl(string stateId)
        {
            return Domain.UserPublicCenter + "/visaDetail_" + stateId.ToString();
        }
    } 
}
