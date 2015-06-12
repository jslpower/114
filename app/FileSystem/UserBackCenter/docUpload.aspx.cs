using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EyouSoft.Common;
using EyouSoft.SSOComponent.Entity;

namespace FileSystem.UserBackCenter
{
    public partial class docUpload : System.Web.UI.Page
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
            string fileExt = System.IO.Path.GetExtension(image_upload.FileName);
            string fileName = Utils.GenerateFileName(fileExt);


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




            if (!Directory.Exists(desDirPath))
            {
                Directory.CreateDirectory(desDirPath);
            }
            image_upload.SaveAs(desFilePath);
            Response.Clear();

            Response.StatusCode = 200;

            Response.Write("{fileid:'" + image_upload.FileName + "|" + relativePath + "'}");
            Response.End();
        }
    }
}
