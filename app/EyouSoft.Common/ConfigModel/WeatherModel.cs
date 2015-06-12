using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EyouSoft.Common.ConfigModel
{   
    /// <summary>
    /// 天气配置 xuty2011/5
    /// </summary>
    public sealed class WeatherClass
    {   
        /// <summary>
        /// 获取天气配置列表
        /// </summary>
        /// <returns></returns>
        public static XElement GetWeatherList()
        {
            string weatherPath=ConfigClass.GetConfigString("weatherpath");
            XElement xele =(XElement)EyouSoft.Cache.Facade.EyouSoftCache.GetCache("weather");
            if (xele == null)
            {
                xele = XElement.Load(System.Web.HttpContext.Current.Server.MapPath(weatherPath));
                if (xele != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add("weather", xele, DateTime.Now.AddMinutes(20));
                }
            }
            return xele;
        }
    }
    
}
