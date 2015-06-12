using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.ConfigModel;

namespace EyouSoft.Common
{
    /// <summary>
    /// Css文件的管理，缓存，版本管理
    /// </summary>
    public class CssManage
    {
        static CssManage()
        {
            InitCssFileList();
        }

        private static object synchObject = new object();
        private static List<CssFile> _CssFileList = null;
        private static List<CssFile> CssFileList
        {
            get
            {
                if (_CssFileList == null)
                {
                    lock (synchObject)
                    {
                        if (_CssFileList == null)
                        {
                            InitCssFileList();
                        }
                    }
                }

                return _CssFileList;
            }
        }

        private static void InitCssFileList()
        {
            List<CssFile> list = ConfigModel.ConfigClass.GetConfigurationSecion("CssManage") as List<CssFile>;
            if (list != null)
            {
                _CssFileList = list;
            }
        }

        public static string GetCssFilePath(string name)
        {
            return CssFileList.Find(file => file.FileName.Equals(name, StringComparison.OrdinalIgnoreCase)).FilePath;
        }
    }
}
