using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;

namespace EyouSoft.Common.ConfigModel
{
    /// <summary>
    /// CssManage自定义配置节处理程序
    /// </summary>
    public class CssManageTagHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlNodeList cssFileNodes;
            String aName, aVersion, DefaultServerComponents,aPath;
            List<CssFile> list = new List<CssFile>();
            DefaultServerComponents = section.Attributes.GetNamedItem("defaultServerComponents").Value;
            cssFileNodes = section.SelectNodes("CssFile");

            foreach (XmlNode cssFileNode in cssFileNodes)
            {
                aName = cssFileNode.Attributes.GetNamedItem("name").Value;
                aVersion = cssFileNode.Attributes.GetNamedItem("version").Value;
                aPath = cssFileNode.Attributes.GetNamedItem("relativepath").Value;

                list.Add(new CssFile()
                {
                    FileName = aName,
                    ServerComponents = DefaultServerComponents,
                    Version = aVersion,
                    RelativePath= aPath
                });
            }
            return list;
        }

        #endregion
    }

    public class CssFile
    {
        public string FileName { get; set; }
        public string Version { get; set; }
        public string ServerComponents { get; set; }
        public string RelativePath { get; set; }
        public string FilePath
        {
            get
            {
                return string.Format("{0}{1}?v={2}", ServerComponents, RelativePath, Version);
            }
        }
    }
}
