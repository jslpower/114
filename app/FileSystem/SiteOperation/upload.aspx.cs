using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using EyouSoft.Security.Membership;
using System.IO;

namespace FileSystem.SiteOperation
{
    /// <summary>
    /// 运营后台中心上传页面
    /// </summary>
    public partial class upload : System.Web.UI.Page
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

            //Get the IsGenerateThumbnail
            int geneateThumbnail = Utils.GetInt(Request.Form["IsGenerateThumbnail"]);
            bool isGenerateThumbnail = geneateThumbnail == 1 ? true : false;

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

            //Get the width and heigth.
            int width = Utils.GetInt(Request.Form["ImageWidth"], 1024);
            int height = Utils.GetInt(Request.Form["ImageHeight"], 768);
            int themWidth = Utils.GetInt(Request.Form["ThemImageWidth"], 400);
            int themHeight = Utils.GetInt(Request.Form["ThemImageHeight"], 300);



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

            //is swf.
            bool isSwf = false;
            if (System.IO.Path.GetExtension(image_upload.FileName).Equals(".swf", StringComparison.OrdinalIgnoreCase) == true)
            {
                isSwf = true;
            }

            //generate the file path
            string moduleDir = GetModuleDirName(siteModule);

            string fileExt = isSwf == false ? ".jpg" : ".swf";
            string fileName = Utils.GenerateFileName(fileExt, string.Format("{0}-{1}", width, height));
            string path = null;
            string relativeDirPath = string.Empty;
            string relativePath = string.Empty;
            string themFilePath = string.Empty;
            string themDirPath = string.Empty;
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            if (siteModule != SiteOperationsCenterModule.会员管理)
            {
                path = VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.SiteVirtualPath"));
                path = VirtualPathUtility.RemoveTrailingSlash(path);
                relativePath = path + "/" + moduleDir + "/" + year + "/" + month + "/" + fileName;
                relativeDirPath = path + "/" + moduleDir + "/" + year + "/" + month + "/";

                //缩略图完整路径
                themFilePath = path + "/" + moduleDir + "/" + year + "/" + month + "/them/" + fileName;
                //缩略图文件夹
                themDirPath = path + "/" + moduleDir + "/" + year + "/" + month + "/them/";
            }
            else
            {
                path = VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath"));
                path = VirtualPathUtility.RemoveTrailingSlash(path);
                if (companyId != string.Empty)
                {
                    relativePath = path + "/" + companyId + "/" + year + "/" + month + "/" + fileName;
                    relativeDirPath = path + "/" + companyId + "/" + year + "/" + month + "/";
                    //缩略图完整路径
                    themFilePath = path + "/" + companyId + "/" + year + "/" + month + "/them/" + fileName;
                    //缩略图文件夹
                    themDirPath = path + "/" + companyId + "/" + year + "/" + month + "/them/";
                }
                else
                {
                    relativePath = path + "/" + fileName;
                    relativeDirPath = path + "/";

                    //缩略图完整路径
                    themFilePath = path + "/them/" + fileName;
                    //缩略图文件夹
                    themDirPath = path + "/them/";
                }
            }
            string desFilePath = Server.MapPath(relativePath);
            string desDirFilePath = Server.MapPath(relativeDirPath);

            string desThemFilePath = Server.MapPath(themFilePath);
            string desThemDirPath = Server.MapPath(themDirPath);



            string filename1 = Utils.GenerateFileName(fileExt, string.Format("{0}-{1}", 122, 102));
            string relativePath1 = relativeDirPath + filename1;
            string desFilePath1 = Server.MapPath(relativePath1);

            if (!Directory.Exists(desDirFilePath))
            {
                Directory.CreateDirectory(desDirFilePath);
            }
            if (!Directory.Exists(desThemDirPath))
            {
                Directory.CreateDirectory(desThemDirPath);
            }

            if (!isSwf)
            {
                image_upload.SaveAs(desFilePath);


                System.Drawing.Image img = System.Drawing.Image.FromFile(desFilePath);

                if (img.Width > width || img.Height > height)
                {
                    string cutImgPath = relativeDirPath + "1024_768_" + fileName;
                    EyouSoft.Common.Function.Thumbnail.MakeThumbnail(desFilePath, Server.MapPath(cutImgPath), width, height, "HW");
                    relativePath = cutImgPath;
                    //删除原文件
                    DeleteImg(desFilePath);
                }

                if (isGenerateThumbnail)
                {
                    if (img.Width > themWidth || img.Height > themHeight)
                    {
                        EyouSoft.Common.Function.Thumbnail.MakeThumbnail(desFilePath, desThemFilePath, themWidth, themHeight, "HW");
                    }
                    else
                    {
                        image_upload.SaveAs(desThemFilePath);
                    }
                }

                img.Dispose();
                img = null;
            }
            else
            {
                image_upload.SaveAs(desFilePath);
            }

            Response.Clear();
            Response.StatusCode = 200;
            if (isGenerateThumbnail && !isSwf)
            {
                Response.Write("{fileid:'" + relativePath + "|" + themFilePath + "'}");
            }
            else
            {
                Response.Write("{fileid:'" + relativePath + "'}");
            }
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
