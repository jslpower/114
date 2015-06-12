using System;
using System.Net;
using System.IO;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.SSOComponent.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystem.SiteOperation
{
    public partial class UploadByUrl1 : System.Web.UI.Page
    {
        //定义允许上传的文件扩展名
        private String fileType = "gif,jpg,jpeg,png,bmp";

        /// <summary>
        /// 描述：根据URL下载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断 是否在运营后台中心登录过
            bool isLogin = EyouSoft.Security.Membership.UserProvider.ValidateMasterUserInFileSystem();
            if (!isLogin)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Write("<msg>请重新登录</msg>");
                Response.End();
            }
            //返回的文件路径
            string fileName = "";
            //请求的URL
            string Url = Utils.GetQueryStringValue("UrlAdd");
            //判断是否要加水印
            bool isAddWater = Utils.GetQueryStringValue("isAddWater") == "1" ? true : false;
            string FileHttpPath = "";
            if (!string.IsNullOrEmpty(Url) && (Url.ToLower().StartsWith("http://")))
            {
                try
                {
                    string tempPath = Url.Remove(0, 7);
                    tempPath = tempPath.Substring(tempPath.IndexOf('/'));
                    //如果是站内链接
                    if (File.Exists(Server.MapPath(tempPath)))
                    {
                        if (isAddWater)
                        {
                            EyouSoft.Common.Function.ImageHelper ImageHel = new EyouSoft.Common.Function.ImageHelper();
                            fileName = System.IO.Path.GetFileName(ImageHel.DrawImage(Server.MapPath(tempPath), Server.MapPath(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("ShuiYinPicPath")), 0.4F, ImagePosition.RigthBottom, Server.MapPath(tempPath)));
                            tempPath = tempPath.Replace(System.IO.Path.GetFileName(tempPath), "");
                            FileHttpPath = Domain.FileSystem + tempPath + fileName;
                        }
                        else
                        {
                            FileHttpPath = Url;
                        }
                    }
                    else
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                        request.Method = "GET";
                        request.KeepAlive = true;
                        request.AllowAutoRedirect = false;
                        request.ContentType = "image/BMP";
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();
                        System.Drawing.Image imageStream = System.Drawing.Image.FromStream(responseStream);
                        string fileExt = string.Empty;
                        string year = DateTime.Now.Year.ToString();
                        string month = DateTime.Now.Month.ToString();
                        string relativePath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.SiteVirtualPath")))
                             + "/NewsCenter/" + year + "/" + month + "/";
                        fileExt = System.IO.Path.GetExtension(Url);
                        fileName = Utils.GenerateFileName(fileExt,System.Guid.NewGuid().ToString());
                        string desFilePath = Server.MapPath(relativePath);
                        if (!Directory.Exists(desFilePath))
                        {
                            Directory.CreateDirectory(desFilePath);
                        }
                        //保存图片
                        imageStream.Save(desFilePath + fileName);
                        if (isAddWater)
                        {
                            EyouSoft.Common.Function.ImageHelper ImageHel = new EyouSoft.Common.Function.ImageHelper();
                            fileName = System.IO.Path.GetFileName(ImageHel.DrawImage(desFilePath + fileName, Server.MapPath(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("ShuiYinPicPath")), 0.4F, ImagePosition.RigthBottom, desFilePath));
                        }
                        FileHttpPath = Domain.FileSystem + relativePath + fileName;
                    }
                }
                catch (Exception ex)
                {
                    FileHttpPath = "";
                }
            }
            Response.Clear();
            Response.StatusCode = 200;
            Response.Write(string.Format("<msg>{0}</msg>", FileHttpPath));
            Response.End();
        }
    }
}
