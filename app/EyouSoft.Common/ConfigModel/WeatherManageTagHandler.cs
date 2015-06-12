using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace EyouSoft.Common.ConfigModel
{
   public  class WeatherManageTagHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlNodeList weatherNodes;
          
            List<weather> list = new List<weather>();

            weatherNodes = section.SelectNodes("Weather");

            foreach (XmlNode wFileNode in weatherNodes)
            {
                list.Add(new weather()
                {
                    condition = wFileNode.Attributes.GetNamedItem("condition").Value,
                    icon = wFileNode.Attributes.GetNamedItem("icon").Value
                });
            }
            return list;
        }

        #endregion
   }

        public class weather
        {
            public string condition;
            public string icon;
        }

        public sealed class WeatherManage
        {
            static WeatherManage()
            {
                InitWeatherList();
            }

        private static object synchObject = new object();
        private static List<weather> _weatherList = null;
        public static List<weather> WeatherList
        {
            get
            {
                if (_weatherList == null || _weatherList.Count==0)
                {
                    lock (synchObject)
                    {
                        if (_weatherList == null)
                        {
                            InitWeatherList();
                        }
                    }
                }

                return _weatherList;
            }
        }

        private static void InitWeatherList()
        {
            List<weather> list = ConfigModel.ConfigClass.GetConfigurationSecion("WeatherManage") as List<weather>;
            if (list != null)
            {
                _weatherList = list;
            }
        }
       
    }
}
