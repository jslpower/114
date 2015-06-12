using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace EyouSoft.Common.ConfigModel
{
    /// <summary>
    /// JSManageer自定义配置节处理程序
    /// </summary>
    public class JSManageTagHandler:IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler 成员

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            XmlNodeList jsFileNodes;
            String aName, aVersion,DefaultServerComponents,aPath;
            List<JsFile> list = new List<JsFile>();
            DefaultServerComponents = section.Attributes.GetNamedItem("defaultServerComponents").Value;
            jsFileNodes = section.SelectNodes("JSFile");

            foreach (XmlNode jsFileNode in jsFileNodes)
            {
                aName = jsFileNode.Attributes.GetNamedItem("name").Value;
                aVersion = jsFileNode.Attributes.GetNamedItem("version").Value;
                aPath = jsFileNode.Attributes.GetNamedItem("relativepath").Value;

                list.Add(new JsFile()
                {
                    FileName = aName,
                    ServerComponents = DefaultServerComponents,
                    Version =aVersion,
                    RelativePath = aPath
                });
            }
            return list;
        }

        #endregion
    }

    public class JsFile
    {
        public string FileName { get; set; }
        public string Version { get; set; }
        public string ServerComponents { get; set; }
        public string RelativePath{get;set;}
        public string FilePath
        {
            get
            {
                return string.Format("{0}{1}?v={2}", ServerComponents, RelativePath, Version);
            }
        }
    }
}
