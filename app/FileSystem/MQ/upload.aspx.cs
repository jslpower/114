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
using System.IO;

namespace FileSystem.MQ
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int mqLoginId = Utils.GetInt(Request.Form["mqid"]);
            string pwd = Utils.GetFormValue("mqpwd");

            EyouSoft.Model.CompanyStructure.CompanyUser companyUser = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(mqLoginId);

            if (companyUser == null||companyUser.PassWordInfo.MD5Password!=pwd)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Write("无效的用户信息");
                Response.End();
            }

            //Get the Upload File Type.
            string fileType = Utils.GetFormValue("filetype");

            //Get the width and heigth.
            int width = Utils.GetInt(Request.Form["ImageWidth"], 300);
            int height = Utils.GetInt(Request.Form["ImageHeight"],300);

            // Get the file data.
			HttpPostedFile image_upload = Request.Files["Filedata"];

            if (image_upload==null||image_upload.ContentLength <= 0)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件为空'}");
                Response.End();
            }
            else if (image_upload.ContentLength > 1048576)
            {
                Response.Clear();
                Response.StatusCode = 200;

                Response.Write("{error:'上传的文件超过了指定的大小'}");
                Response.End();
            }

            //generate the file path.
            string companyName = companyUser.CompanyID;
            string fileExt = string.Empty;
            string fileName = string.Empty;
            if (fileType != "worddoc")
            {
                fileExt = ".jpg";
                fileName = Utils.GenerateFileName(fileExt);
            }
            else
            {
                fileExt = System.IO.Path.GetExtension(image_upload.FileName);
                fileName = Utils.GenerateFileName(fileExt);
            }
            
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();


            string relativePath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath"))) 
                + "/" + companyName + "/" +year+"/"+month+"/"+ fileName;
            string relativeDirPath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath")))
                + "/" + companyName + "/" + year + "/" + month + "/";
            string desFilePath = Server.MapPath(relativePath);
            string desDirPath = Server.MapPath(relativeDirPath);

            if (!Directory.Exists(desDirPath))
            {
                Directory.CreateDirectory(desDirPath);
            }

            if (fileType != "worddoc")
            {
                //Thumbnail.CompressionImage(image_upload.InputStream, desFilePath, width, height);
                image_upload.SaveAs(desFilePath);
            }
            else
            {
                image_upload.SaveAs(desFilePath);
            }

            Response.Clear();

            Response.StatusCode = 200;
            Response.Write("{fileid:'" + relativePath + "'}");
            Response.End();
        }
    }
}
