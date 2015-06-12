using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure.CompanyFileType
{
    /// <summary>
    /// 公司网络硬盘上传类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// 目录
        /// </summary>
        Folder
    }
}
namespace EyouSoft.Model.ToolStructure
{
    using EyouSoft.Model.ToolStructure.CompanyFileType;
    /// <summary>
    /// 创建人：蒋胜蓝 2010-05-12
    /// 描述：公司网络硬盘文件信息
    /// </summary>
    [Serializable]
    public class CompanyFileUpload
    {
        #region 属性
        
        /// <summary>
        /// 编号
        /// </summary>
        public string FileId
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get;
            set;
        }
        /// <summary>
        /// 上传类型(0:文件，1:目录)
        /// </summary>
        public FileType FileType
        {
            get;
            set;
        }
        /// <summary>
        /// 文件图标
        /// </summary>
        public string FileIco
        {
            get;
            set;
        }
        /// <summary>
        /// 文件容量(字节)
        /// </summary>
        public int FileSize
        {
            get;
            set;
        }
        /// <summary>
        /// 文件真实地址
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }
        /// <summary>
        /// 上级目录编号
        /// </summary>
        public string ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }
}