using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using EyouSoft.Common;
namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 获取谷歌天气
    /// </summary>
    public class GetWeather : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            GetWeatherHtml();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取当前天气
        /// </summary>
        public void GetWeatherHtml()
        {

            string cityName = Utils.GetQueryStringValue("cityname");
            string weatherHtml = "";
            try
            {
                string str = Utils.GetDefaultWebResult("http://www.google.com/ig/api?hl=zh-cn&weather=" + cityName);
                XElement root = XElement.Parse(str);
                XElement current = root.Element("weather").Element("current_conditions");
                string condition = current.Element("condition").Attribute("data").Value;
                List<EyouSoft.Common.ConfigModel.weather> configWeathers = EyouSoft.Common.ConfigModel.WeatherManage.WeatherList;
                EyouSoft.Common.ConfigModel.weather configWeather=null;
                string icon = "";
                if(configWeathers!=null&&configWeathers.Count>0)
                {
                    configWeather=configWeathers.FirstOrDefault(i=>i.condition==condition);
                }
                if (configWeather != null)
                {
                    icon =Domain.ServerComponents+configWeather.icon;
                }
                else
                {
                    string tmp = current.Element("icon").Attribute("data").Value.Replace("40", "30");
                    if (tmp.IndexOf("http://") == -1)
                    {
                        tmp = "http://www.google.com" + tmp;
                    }
                    icon = tmp;
                }
                weatherHtml = string.Format("<img width=\"26px\" height=\"21px\" src=\"{0}\" alt=\"{2}\" title=\"{2}\" />&nbsp;<span style=\"font-size:15px;font-weight:bold;\">{1}℃</span> ", icon, current.Element("temp_c").Attribute("data").Value, condition + " " + current.Element("wind_condition").Attribute("data").Value);
            }
            catch
            {

            }
            Utils.Show(weatherHtml);
         }
    }
}
