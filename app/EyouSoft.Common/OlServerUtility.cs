using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EyouSoft.Common
{
    /// <summary>
    /// 在线客服公共类
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-27
    public class OlServerUtility
    {
        /// <summary>
        /// 获取在线客服配置信息
        /// </summary>
        public static EyouSoft.Model.OnLineServer.OlServerConfig GetOlServerConfig()
        {
            object oConfig = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.OlServer.OLSCONFIGCACHENAME);
            EyouSoft.Model.OnLineServer.OlServerConfig config = new EyouSoft.Model.OnLineServer.OlServerConfig();

            if (oConfig == null)
            {
                config.GetUsersInterval = Utils.GetInt(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("OlServerGetUsersInterval"), 5);
                config.GetMessageInterval = Utils.GetInt(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("OlServerGetMessageInterval"), 3);
                config.StayTime = Utils.GetInt(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("OlServerUserStayTime"), 10);
                config.ClearUserOutInterVal = Utils.GetInt(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("OlServerClearUserOutInterVal"), 1);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.OlServer.OLSCONFIGCACHENAME, config);
            }
            else
            {
                config = (EyouSoft.Model.OnLineServer.OlServerConfig)oConfig;
            }

            return config;
        }

        /// <summary>
        /// 清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
        /// </summary>
        public static void ClearUserOut()
        {
            object olServerClearUserOutInterVal = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.OlServer.OLSERVERCLEARUSEROUTINTERVALCACHENAME);

            //缓存信息为null时执行清理过了指定停留时间的在线用户的在线状态为不在线（不含客服人员）
            if (olServerClearUserOutInterVal == null)
            {
                EyouSoft.BLL.OnLineServer.OLServer bll = new EyouSoft.BLL.OnLineServer.OLServer();
                bll.ClearUserOut(GetOlServerConfig().StayTime);
                bll = null;

                EyouSoft.Model.OnLineServer.OlServerConfig config = GetOlServerConfig();

                //设置缓存及缓存时间
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.OlServer.OLSERVERCLEARUSEROUTINTERVALCACHENAME, config.ClearUserOutInterVal, DateTime.Now.AddMinutes(config.ClearUserOutInterVal));
            }

        }

        /// <summary>
        /// 获取用户Cookie信息
        /// </summary>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        public static EyouSoft.Model.OnLineServer.OlServerUserInfo GetOlCookieInfo(bool isService)
        {
            EyouSoft.Model.OnLineServer.OlServerUserInfo info = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
            HttpCookie cookie = null;

            if (isService)
            {
                cookie = HttpContext.Current.Request.Cookies[EyouSoft.CacheTag.OlServer.OLSSERVICECOOKIENAME];
            }
            else
            {
                cookie = HttpContext.Current.Request.Cookies[EyouSoft.CacheTag.OlServer.OLSGUESTCOOKIENAME];
            }

            if (cookie != null)
            {
                info = (EyouSoft.Model.OnLineServer.OlServerUserInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(HttpUtility.UrlDecode(cookie.Value), typeof(EyouSoft.Model.OnLineServer.OlServerUserInfo));
            }
            else
            {
                info = null;
            }

            return info;
        }
    }
}
