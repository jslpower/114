using System;
using System.Collections.Generic;
using System.IO;

namespace EyouSoft.Common.Function
{
    /// <summary>
    /// 文件上传类
    /// </summary>
    public class UploadFile
    {
        private string _UploadFileExt = ".gif,.bmp,.png,.jpg,.jpeg";
        private int _UpFolderSize = 1024;//KB

        public UploadFile()
        {
        }
        /// <summary>
        /// 释放空间
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }
        ~UploadFile()
        {
            Dispose(false);
        }
        #region model
        /// <summary>
        /// 支持的文件后缀
        /// </summary>
        public string UploadFileExt
        {
            set { _UploadFileExt = value; }
            get { return _UploadFileExt; }
        }
        /// <summary>
        /// 支持的文件大小 以KB为单位
        /// </summary>
        public int UpFolderSize
        {
            set
            {
                _UpFolderSize = value;
            }
            get
            {
                return _UpFolderSize;
            }
        }
        /// <summary>
        /// 取得索引上传文件的文件名
        /// </summary>
        /// <param name="fileIndex">文件索引</param>
        /// <returns></returns>
        public string FileName(int fileIndex)
        {
            string fileName;
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (fileIndex < 0 || fileIndex >= files.Count)
            {
                files = null;
                return "";
            }
            System.Web.HttpPostedFile file = files[fileIndex];
            fileName = System.IO.Path.GetFileName(file.FileName);
            file = null;
            files = null;
            return fileName;
        }
        /// <summary>
        /// 按时间加随机数生成文件名
        /// </summary>
        /// <param name="fileIndex">文件索引</param>
        /// <returns></returns>
        public string TimeFileName(int fileIndex)
        {
            int RandNum;
            string fileName;
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (fileIndex < 0 || fileIndex >= files.Count)
            {
                files = null;
                return "";
            }
            System.Web.HttpPostedFile file = files[fileIndex];
            Random rnd = new Random();
            RandNum = rnd.Next(1, 99);//生成一个99以内的随机数
            fileName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + RandNum.ToString() + System.IO.Path.GetExtension(file.FileName);
            file = null;
            files = null;
            return fileName;
        }
        /// <summary>
        /// 按时间加随机数生成文件名
        /// </summary>
        /// <param name="FileExt">文件后缀名</param>
        /// <returns></returns>
        public string TimeFileName(string FileExt)
        {
            int RandNum;
            string PhotoName;
            Random rnd = new Random();
            RandNum = rnd.Next(1, 99);//生成一个99以内的随机数
            PhotoName = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + RandNum.ToString() + FileExt;
            return PhotoName;
        }
        /// <summary>
        /// 返回当前日期的名称
        /// </summary>
        /// <returns></returns>
        public string DateDirectory()
        {
            string FolderName;
            FolderName = DateTime.Now.ToString("yyyyMMdd");
            return FolderName;
        }
        #endregion

        #region 文件处理函数

        /// <summary>
        /// 检查文件后缀名
        /// </summary>
        /// <returns></returns>
        public bool CheckFileExt()
        {
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //处理多文件
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                foreach (string FileExt in strFileExt)
                {
                    if (file.FileName != null && file.FileName != String.Empty)
                    {
                        if (FileExt.ToLower().Trim() == System.IO.Path.GetExtension(file.FileName).ToLower().Trim())
                        {
                            return true;
                        }
                    }
                }
            }
            files = null;
            return false;
        }
        /// <summary>
        /// 批量检验文件大小
        /// </summary>
        /// <returns></returns>
        public bool CheckFileSize()
        {
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //处理多文件
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                if (file.ContentLength > _UpFolderSize * 1024)
                {
                    return false;
                }
            }
            files = null;
            return true;
        }
        /// <summary>
        /// 上传的文件数
        /// </summary>
        /// <returns></returns>
        public int FilesCount()
        {
            return System.Web.HttpContext.Current.Request.Files.Count;
        }
        /// <summary>
        /// 覆盖保存文件
        /// </summary>
        /// <param name="fileIndex">文件对象索引</param>
        /// <param name="AbsoluteFileName">文件保存路径及文件名</param>
        /// <returns>
        ///		0:上传文件成功
        ///		1:上传的文件超过指定大小
        ///		2:上传的文件格式不在指定的后缀名列表中
        ///		3:上传的文件没有后缀名
        ///		4:保存文件出错
        ///		5:文件对象索引错误
        ///</returns>
        public int Save(int fileIndex, string AbsoluteFileName)
        {
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (fileIndex < 0 || fileIndex >= files.Count)
            {
                files = null;
                return 5;
            }
            System.Web.HttpPostedFile file = files[fileIndex];
            //判断文件大小
            if (file.ContentLength > _UpFolderSize * 1024)
            {
                return 1;
            }
            //检验后缀名
            if (file.FileName != String.Empty)
            {
                if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
                    return 2;
            }
            else
            {
                return 3;
            }
            //保存文件
            try
            {
                //FileDelete(AbsoluteFileName);
                file.SaveAs(AbsoluteFileName);
            }
            catch
            {
                return 4;
            }
            return 0;
        }
        /// <summary>
        /// 批量保存文件 同名文件自动覆盖
        /// </summary>
        /// <param name="AbsoluteFilePath">文件保存路径</param>
        /// <returns>
        ///		0:上传文件成功
        ///		1:上传的文件超过指定大小
        ///		2:上传的文件格式不在指定的后缀名列表中
        ///		3:上传的文件没有后缀名
        ///		4:保存文件出错
        ///		5:没有发现上传的文件
        ///</returns>
        public int FilesSave(string AbsoluteFilePath)
        {
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (files.Count < 1)
            {
                files = null;
                return 5;//没有发现上传文件;
            }
            //处理多文件
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                //判断文件大小
                if (file.ContentLength > _UpFolderSize * 1024)
                {
                    return 1;
                }
                //检验后缀名
                if (file.FileName != String.Empty)
                {
                    if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
                        return 2;
                    //保存文件
                    try
                    {
                        if (AbsoluteFilePath.LastIndexOf("\\") == AbsoluteFilePath.Length)
                        {
                            CreateDirectory(AbsoluteFilePath);
                            file.SaveAs(AbsoluteFilePath + System.IO.Path.GetFileName(file.FileName));
                        }
                        else
                        {
                            CreateDirectory(AbsoluteFilePath);
                            file.SaveAs(AbsoluteFilePath + "\\" + System.IO.Path.GetFileName(file.FileName));
                        }
                    }
                    //catch(Exception e1)
                    catch
                    {
                        //throw new Exception(AbsoluteFilePath + file.FileName);
                        return 4;
                    }
                }
            }
            files = null;
            return 0;
        }
        /// <summary>
        /// 批量保存文件
        /// </summary>
        /// <param name="AbsoluteFilePath">文件保存路径</param>
        /// <returns>
        ///		0:上传文件成功
        ///		1:上传的文件超过指定大小
        ///		2:上传的文件格式不在指定的后缀名列表中
        ///		3:上传的文件没有后缀名
        ///		4:保存文件出错
        ///		5:没有发现上传的文件
        ///</returns>
        public int FilesNewNameSave(string AbsoluteFilePath)
        {
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string tmpFileExt = _UploadFileExt;
            string[] strFileExt = tmpFileExt.Split(',');
            if (files.Count < 1)
            {
                files = null;
                return 5;//没有发现上传文件;
            }
            //处理多文件
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i];
                //判断文件大小
                if (file.ContentLength > _UpFolderSize * 1024)
                {
                    return 1;
                }
                //检验后缀名,无后缀名的不做处理
                if (file.FileName != String.Empty)
                {
                    if (IsStringExists(System.IO.Path.GetExtension(file.FileName).ToLower().Trim(), strFileExt) == false)
                        return 2;
                    //保存文件
                    try
                    {
                        if (AbsoluteFilePath.LastIndexOf("\\") == AbsoluteFilePath.Length)
                        {
                            CreateDirectory(AbsoluteFilePath);
                            file.SaveAs(AbsoluteFilePath + TimeFileName(System.IO.Path.GetExtension(file.FileName)));
                        }
                        else
                        {
                            CreateDirectory(AbsoluteFilePath);
                            file.SaveAs(AbsoluteFilePath + "\\" + TimeFileName(System.IO.Path.GetExtension(file.FileName)));
                        }
                    }
                    //catch(Exception e1)
                    catch
                    {
                        //throw new Exception(e1.Source + e1.Message);
                        return 4;
                    }
                }
            }
            files = null;
            return 0;
        }       

        #endregion

        #region 内部函数
        /// <summary>
        /// 检测字符串是否是数组中的一项
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private static bool IsStringExists(string inputData, string[] arrData)
        {
            if (null == inputData || string.Empty == inputData)
            {
                return false;
            }
            foreach (string tmpStr in arrData)
            {
                if (inputData == tmpStr)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="AbsoluteFilePath"></param>
        /// <returns></returns>
        private bool FileDelete(string AbsoluteFilePath)
        {
            try
            {
                FileInfo objFile = new FileInfo(AbsoluteFilePath);
                if (objFile.Exists)//如果存在
                {
                    //删除文件.
                    objFile.Delete();
                    return true;
                }

            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 建立目录
        /// </summary>
        /// <param name="DirectoryName">目录名</param>
        /// <returns>返回数字,0:目录建立成功, 1:目录已存在,2:目录建立失败</returns>
        private int CreateDirectory(string DirectoryName)
        {
            try
            {
                if (!Directory.Exists(DirectoryName))
                {
                    Directory.CreateDirectory(DirectoryName);
                    return 0;
                }
                else
                {

                    return 1;
                }
            }
            catch
            {
                return 2;
            }
        }
        #endregion
    }
}
