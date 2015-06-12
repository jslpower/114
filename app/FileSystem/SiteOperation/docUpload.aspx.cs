using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Security.Membership;
using EyouSoft.Common;
using System.IO;

namespace FileSystem.SiteOperation
{
    public partial class docUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断 是否在运营后台中心登录过
            bool isLogin = UserProvider.ValidateMasterUserInFileSystem();

            if (!isLogin)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'请重新登录'}");
                Response.End();
            }


            //Get the ToCompanyId
            string companyId = Utils.GetFormValue("ToCompanyId");

            //Get the SiteModule
            int module = Utils.GetInt(Request.Form["Module"]);
            //Convert the module To SiteOperationsCenterModule type.
            SiteOperationsCenterModule siteModule = (SiteOperationsCenterModule)module;
            bool isExists = false;
            foreach (SiteOperationsCenterModule m in Enum.GetValues(typeof(SiteOperationsCenterModule)))
            {
                if (m == siteModule)
                {
                    isExists = true;
                    break;
                }
            }
            if (isExists == false)
            {
                siteModule = SiteOperationsCenterModule.平台管理;
            }

            // Get the file data.
            HttpPostedFile image_upload = Request.Files["Filedata"];

            if (image_upload == null || image_upload.ContentLength <= 0)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件为空'}");
                Response.End();
            }
            else if (image_upload.ContentLength > 2097152)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件超过了指定的大小'}");
                Response.End();
            }


            //generate the file path
            string moduleDir = GetModuleDirName(siteModule);

            string fileExt = System.IO.Path.GetExtension(image_upload.FileName);
            string fileName = Utils.GenerateFileName(fileExt);
            string path = null;
            string relativeDirPath = string.Empty;
            string relativePath = string.Empty;

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            if (siteModule != SiteOperationsCenterModule.会员管理)
            {
                path = VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.SiteVirtualPath"));
                path = VirtualPathUtility.RemoveTrailingSlash(path);
                relativePath = path + "/" + moduleDir + "/" + year + "/" + month + "/" + fileName;
                relativeDirPath = path + "/" + moduleDir + "/" + year + "/" + month + "/";
            }
            else
            {
                path = VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath"));
                path = VirtualPathUtility.RemoveTrailingSlash(path);
                if (companyId != string.Empty)
                {
                    relativePath = path + "/" + companyId + "/" + year + "/" + month + "/" + fileName;
                    relativeDirPath = path + "/" + companyId + "/" + year + "/" + month + "/";

                }
                else
                {
                    relativePath = path + "/" + fileName;
                    relativeDirPath = path + "/";
                }
            }
            string desFilePath = Server.MapPath(relativePath);
            string desDirFilePath = Server.MapPath(relativeDirPath);

            if (!Directory.Exists(desDirFilePath))
            {
                Directory.CreateDirectory(desDirFilePath);
            }
            image_upload.SaveAs(desFilePath);
            Response.Clear();
            Response.StatusCode = 200;

            Response.Write("{fileid:'" + image_upload.FileName + "|" + relativePath + "'}");
            Response.End();
        }

        /// <summary>
        /// 根据模块返回对应的模块目录名称。
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        private string GetModuleDirName(SiteOperationsCenterModule module)
        {
            string dirName = "";
            switch (module)
            {
                case SiteOperationsCenterModule.平台管理:
                    dirName = "PlatformManagement";
                    break;
                case SiteOperationsCenterModule.新闻中心:
                    dirName = "NewsCenter";
                    break;
                case SiteOperationsCenterModule.供求管理:
                    dirName = "SupplierManage";
                    break;
                case SiteOperationsCenterModule.广告管理:
                    dirName = "Management";
                    break;
                default:
                    dirName = "";
                    break;
            }

            return dirName;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strPath">文件磁盘路径</param>
        private void DeleteImg(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
                return;

            var file = new EyouSoft.Common.Function.FileDirectory();
            file.FileDelete(strPath);
            file = null;
        }
    }
}
