using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.ToolStructure
{
    /// <summary>
    /// 描述：公司网络硬盘业务接口
    /// </summary>
    /// 创建人：蒋胜蓝 2010-05-13
    public interface ICompanyFileUpload
    {
        /// <summary>
        /// 添加上传
        /// </summary>
        /// <param name="model">文件信息</param>
        /// <returns>操作结果</returns>
        bool Add(EyouSoft.Model.ToolStructure.CompanyFileUpload model);

        /// <summary>
        /// 修改名字
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FileName">文件名</param>
        /// <returns>操作结果</returns>
        bool UpdateName(string FileId, string FileName);

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FolderId">目录编号</param>
        /// <returns>操作结果</returns>
        bool MoveFile(string FileId, string FolderId);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <returns>操作结果</returns>
        bool RemoveFolder(string FileId);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileId">文件编号</param>
        /// <param name="FolderId">目录编号</param>
        /// <returns>操作结果</returns>
        bool RemoveFile(string FileId, string FolderId);

        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFolderList(string CompanyId);

        /// <summary>
        /// 获取目录中文件列表
        /// </summary>
        /// <param name="FolderId">目录编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string FolderId);

        /// <summary>
        /// 获取公司文件列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="FolderId">目录编号（该编号为空就取公司所有文件）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.CompanyFileUpload> GetFileList(string CompanyId, string FolderId);

        /// <summary>
        /// 获取已用空间
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        int GetSumSize(string CompanyId);
    }
}
