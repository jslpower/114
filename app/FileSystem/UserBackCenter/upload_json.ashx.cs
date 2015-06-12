using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using EyouSoft.Common;
using EyouSoft.SSOComponent.Entity;
namespace FileSystem.UserBackCenter
{
   
   /// <summary>
   /// 处理编辑器的上传图片返回json格式信息
   /// xuty 2011/3/10
   /// </summary>
    public class upload_json : IHttpHandler
    {
	    //定义允许上传的文件扩展名
	    private String fileTypes = "gif,jpg,jpeg,png,bmp";
	    //最大文件大小
        private int maxSize = 1048576;

	    private HttpContext context;
        public void ProcessRequest(HttpContext context)
        {  

            this.context = context;
            context.Response.ContentType = "text/html";
            //登录账户信息
            UserInfo userInfo = null;

            #region 判断是否登录
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out userInfo);
            if (!isLogin)
            {
                context.Response.Clear();
                context.Response.StatusCode = 200;
                showError("请重新登录！");
            }
            #endregion

            #region 验证文件
            HttpPostedFile imgFile = context.Request.Files["imgFile"];
            if (imgFile == null || imgFile.ContentLength<=0)
            {
                showError("请选择文件。");
            }
            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();
          

            if (imgFile.ContentLength > maxSize)
            {
                showError("上传文件大小不能超过1M。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。");
            }
            #endregion

            #region 上传文件返回路径
            //新的文件名
            fileName = Utils.GenerateFileName(Path.GetExtension(fileName));
            //新的部分路径
            string dirPart = string.Format("/{0}/{1}/{2}/",userInfo.CompanyID,DateTime.Now.Year,DateTime.Now.Month);

            string relativeDirPath = VirtualPathUtility.RemoveTrailingSlash(VirtualPathUtility.ToAbsolute(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("FileSystem.CompanyVirtualPath"))) + dirPart;
            string relativePath = relativeDirPath + fileName;
            //文件完整路径
            string desFilePath = context.Server.MapPath(relativePath);
            //所在文件夹路径
            string desDirPath = context.Server.MapPath(relativeDirPath);
            //验证路径是否存在
            if (!Directory.Exists(desDirPath))
            {
                Directory.CreateDirectory(desDirPath);
            }
            imgFile.SaveAs(desFilePath);
            Hashtable hash = new Hashtable();
            hash["error"] = 0;
         
            hash["url"] =Domain.FileSystem+ relativePath;

            context.Response.Write(String.Format("<html><head><script type='text/javascript'>window.name='{0}';</script></head><body></body></html>",JsonConvert.SerializeObject(hash)));
            context.Response.End();
            #endregion
        }

        #region 输出错误消息
        private void showError(string message)
        {
            context.Response.ContentType = "text/html";
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
          
            context.Response.Write(String.Format("<html><head><script type='text/javascript'>window.name='{0}';</script></head><body></body></html>",JsonConvert.SerializeObject(hash)));
            context.Response.End();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
  }

       
   
}
