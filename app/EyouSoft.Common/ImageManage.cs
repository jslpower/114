using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.ConfigModel;

namespace EyouSoft.Common
{
    /// <summary>
    /// 静态图片和动态图片的路径管理
    /// </summary>
    public class ImageManage
    {
        static ImageManage()
        {
            InitImageServerList();
        }

        private static object synchObject = new object();
        private static List<ImageServer> _ImageServerList = null;
        private static List<ImageServer> ImageServerList
        {
            get
            {
                if (_ImageServerList == null)
                {
                    lock (synchObject)
                    {
                        if (_ImageServerList == null)
                        {
                            InitImageServerList();
                        }
                    }
                }

                return _ImageServerList;
            }
        }

        private static void InitImageServerList()
        {
            List<ImageServer> list = ConfigModel.ConfigClass.GetConfigurationSecion("ImageManage") as List<ImageServer>;
            if (list != null)
            {
                _ImageServerList = list;
            }
        }

        public static string GetImagerServerUrl(int index)
        {
            return ImageServerList.Find(imageServer => imageServer.Index == index).Url;
        }
    }
}
