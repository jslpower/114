using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;


namespace EyouSoft.BLL.ToolStructure
{
    /// <summary>
    /// 描述：公司网络硬盘业务
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-13
    public class CompanyFileUpload : EyouSoft.IBLL.ToolStructure.ICompanyFileUpload
    {
        IDAL.ToolStructure.ICompanyFileUpload DAL = ComponentFactory.CreateDAL<IDAL.ToolStructure.ICompanyFileUpload>();
        /// <summary>
        /// 创建IBLL对象
        /// </summary>
        /// <returns></returns>
        public static IBLL.ToolStructure.ICompanyFileUpload CreateInstance()
        {
            IBLL.ToolStructure.ICompanyFileUpload op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.ToolStructure.ICompanyFileUpload>();
            }
            return op;
        }

        /// <summary>
        /// 添加上传
        /// </summary>
        /// <param name="model">文件信息</param>
        /// <returns>操作结果</returns>
        public bool Add(EyouSoft.Model.ToolStructure.CompanyFileUpload model)
        {
            return this.DAL.Add(model)>0;
        }
        /// <summary>
        /// 修改名字
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FileName">文件名</param>
        /// <returns>操作结果</returns>
        public bool UpdateName(string FileId, string FileName)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FolderId">目录编号</param>
        /// <returns>操作结果</returns>
        public bool MoveFile(string FileId, string FolderId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <returns>操作结果</returns>
        public bool RemoveFolder(string FileId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FolderId">目录编号</param>
        /// <returns>操作结果</returns>
        public bool RemoveFile(string FileId, string FolderId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFolderList(string CompanyId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 获取目录中文件列表
        /// </summary>
        /// <param name="FolderId">目录编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string FolderId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 获取公司文件列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FolderId">目录编号（该编号为空就取公司所有文件）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string CompanyId, string FolderId)
        {
            throw new NotImplementedException("未实现的接口");
        }

        /// <summary>
        /// 获取已用空间
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public int GetSumSize(string CompanyId)
        {
            throw new NotImplementedException("未实现的接口");
        }
    }
}
