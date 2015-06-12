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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.SSOComponent.Entity;
using System.IO;

namespace FileSystem.UserBackCenter
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInfo userInfo = null;
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
            if (!isLogin)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'请重新登录'}");
                Response.End();
            }

            //Get the Upload File Type.
            string fileType = Utils.GetFormValue("filetype");

            //Get the width and heigth.
            int width = Utils.GetInt(Request.Form["ImageWidth"], 1024);
            int height = Utils.GetInt(Request.Form["ImageHeight"], 768);
            int themWidth = Utils.GetInt(Request.Form["ThemImageWidth"], 400);
            int themHeight = Utils.GetInt(Request.Form["ThemImageHeight"], 300);
            int geneateThumbnail = Utils.GetInt(Request.Form["IsGenerateThumbnail"]);
            bool isGenerateThumbnail = geneateThumbnail == 1 ? true : false;

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

            //generate the file path.
            string companyName = userInfo.CompanyID;
            string fileExt = string.Empty;
            string fileName = string.Empty;
            if (fileType != "worddoc")
            {
                //判断是否是SWF
                fileExt = System.IO.Path.GetExtension(image_upload.FileName);
                if (!fileExt.Equals(".swf", StringComparison.OrdinalIgnoreCase))
                {
                    fileExt = ".jpg";
                }
                fileName = Utils.GenerateFileName(fileExt);
            }
            else
            {
                fileExt = System.IO.Path.GetExtension(image_upload.FileName);
                fileName = Utils.GenerateFileName(fileExt);
              
            }

            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();

            //原图完整路径
            string relativePath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath")))
                + "/" + companyName + "/" + year + "/" + month + "/" + fileName;
            //原图文件夹
            string relativeDirPath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath")))
                + "/" + companyName + "/" + year + "/" + month + "/";
            string desFilePath = Server.MapPath(relativePath);
            string desDirPath = Server.MapPath(relativeDirPath);


            //缩略图完整路径
            string themFilePath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath")))
                + "/" + companyName + "/" + year + "/" + month + "/them/" + fileName;
            //缩略图文件夹
            string themDirPath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath")))
                + "/" + companyName + "/" + year + "/" + month + "/them/";
            string desThemFilePath = Server.MapPath(themFilePath);
            string desThemDirPath = Server.MapPath(themDirPath);



            if (!Directory.Exists(desDirPath))
            {
                Directory.CreateDirectory(desDirPath);
            }
            if (!Directory.Exists(desThemDirPath))
            {
                Directory.CreateDirectory(desThemDirPath);
            }

            if (fileType != "worddoc")
            {
                image_upload.SaveAs(desFilePath);
            }
            else
            {
                image_upload.SaveAs(desFilePath);
                //doc 文件保存后直接退出
                Response.Clear();
                Response.StatusCode = 200;
                Response.Write("{fileid:'" + relativePath + "'}");
                Response.End();
                return;
            }

            if (!fileExt.Equals(".swf", StringComparison.OrdinalIgnoreCase))
            {

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

            Response.Clear();

            Response.StatusCode = 200;
            if (isGenerateThumbnail && !fileExt.Equals(".swf", StringComparison.OrdinalIgnoreCase))
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
