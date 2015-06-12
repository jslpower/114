using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.ConfigModel;

namespace EyouSoft.Common
{
    /// <summary>
    /// Js文件的管理，缓存，版本管理
    /// </summary>
    public class JsManage
    {
        static JsManage()
        {
            InitJsFileList();
        }

        private static object synchObject = new object();
        private static List<JsFile> _JsFileList = null;
        private static List<JsFile> JsFileList
        {
            get
            {
                if (_JsFileList == null)
                {
                    lock (synchObject)
                    {
                        if (_JsFileList == null)
                        {
                            InitJsFileList();
                        }
                    }
                }

                return _JsFileList;
            }
        }

        private static void InitJsFileList()
        {
            List<JsFile> list = ConfigModel.ConfigClass.GetConfigurationSecion("JSManage") as List<JsFile>;
            if (list != null)
            {
                _JsFileList = list;
            }
        }

        public static string GetJsFilePath(string name)
        {
            return JsFileList.Find(file => file.FileName.Equals(name, StringComparison.OrdinalIgnoreCase)).FilePath;
        }
    }
}
