using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace EyouSoft.Common.ConfigModel
{
    /// <summary>
    /// ImageManage自定义配置节处理程序
    /// </summary>
    public class ImageManageTagHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlNodeList imgServerNodes;
            String aUrl;
            int currentUsedIndex,aIndex;
            List<ImageServer> list = new List<ImageServer>();
            currentUsedIndex = Int32.Parse(section.Attributes.GetNamedItem("currentUsedIndex").Value);
            imgServerNodes = section.SelectNodes("ImageServer");

            foreach (XmlNode imgServerNode in imgServerNodes)
            {
                aIndex = Int32.Parse(imgServerNode.Attributes.GetNamedItem("index").Value);
                aUrl = imgServerNode.Attributes.GetNamedItem("url").Value;

                list.Add(new ImageServer()
                {
                    Index=aIndex,
                    Url = aUrl
                });
            }
            return list;
        }

        #endregion
    }

    public class ImageServer
    {
        public int Index { get; set; }
        public string Url { get; set; }
    }
}
