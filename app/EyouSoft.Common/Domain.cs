using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.ConfigModel;

namespace EyouSoft.Common
{
    /// <summary>
    /// 域名的处理，URL地址的生成
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// 高级网店域名
        /// </summary>
        public static readonly string SeniorOnlineShop = ConfigClass.GetConfigString("Domain", "SeniorOnlineShop");
        /// <summary>
        /// 运营后台中心域名
        /// </summary>
        public static readonly string SiteOperationsCenter = ConfigClass.GetConfigString("Domain", "SiteOperationsCenter");
        /// <summary>
        /// 客户后台中心域名
        /// </summary>
        public static readonly string UserBackCenter = ConfigClass.GetConfigString("Domain", "UserBackCenter");
        /// <summary>
        /// 同业114前台域名
        /// </summary>
        public static readonly string UserPublicCenter = ConfigClass.GetConfigString("Domain", "UserPublicCenter");
        /// <summary>
        /// 文件上传服务器域名
        /// </summary>
        public static readonly string FileSystem = ConfigClass.GetConfigString("Domain", "FileSystem");
        /// <summary>
        /// 单点登录验证中心域名
        /// </summary>
        public static readonly string PassportCenter = ConfigClass.GetConfigString("Domain", "PassportCenter");
        /// <summary>
        /// 静态组件域名
        /// </summary>
        public static readonly string ServerComponents = ConfigClass.GetConfigString("Domain", "ServerComponents");
        /// <summary>
        /// 机票查询口地址
        /// </summary>
        public static readonly string TicketSearchUrl = ConfigClass.GetConfigString("appSettings", "TicketInterfaceUrl");
        /// <summary>
        /// 机票 用户默认分润组GUID
        /// </summary>
        public static readonly string TicketDefaultGroupGUID = ConfigClass.GetConfigString("appSettings", "TicketDefaultGroupGUID");
        /// <summary>
        /// IMFrame项目的域名
        /// </summary>
        public static readonly string IMFrame = ConfigClass.GetConfigString("Domain", "IMFrame");

        /// <summary>
        /// 同业社区域名
        /// </summary>
        public static readonly string UserClub = ConfigClass.GetConfigString("Domain", "UserClub");
    }
}
