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
using System.IO;

namespace FileSystem.General
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the width and heigth.
            int width = Utils.GetInt(Request.Form["ImageWidth"], 300);
            int height = Utils.GetInt(Request.Form["ImageHeight"], 300);

            // Get the file data.
            HttpPostedFile image_upload = Request.Files["Filedata"];

            if (image_upload == null || image_upload.ContentLength <= 0)
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

            //generate the file path
            string moduleDir = "general";

            string fileExt = ".jpg";
            string fileName = Utils.GenerateFileName(fileExt, string.Format("{0}-{1}", width, height));
            string path = null;
            string relativePath = string.Empty;
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();

            path = VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.SiteVirtualPath"));
            path = VirtualPathUtility.RemoveTrailingSlash(path);
            relativePath = path + "/" + moduleDir + "/" + year + "/" + month + "/" + fileName;
            string relativeDirPath = path + "/" + moduleDir + "/" + year + "/" + month + "/";
            string desDirPath = Server.MapPath(relativeDirPath);
            string desFilePath = Server.MapPath(relativePath);

            if (!Directory.Exists(desDirPath))
            {
                Directory.CreateDirectory(desDirPath);
            }

            //Thumbnail.CompressionImage(image_upload.InputStream, desFilePath, width, height);
            image_upload.SaveAs(desFilePath);

            Response.Clear();
            Response.StatusCode = 200;
            Response.Write("{fileid:'" + relativePath + "'}");
            Response.End();
        }
    }
}
