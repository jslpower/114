using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 团队信息附件
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourAttachInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourAttachInfo()
        { }

        #region Model

        private string _id;
        private string _tourid;
        private string _originalpath;
        private string _currentpath;
        private string _filename;
        private string _operatorid;
        private DateTime _issuetime;

        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 团队ID
        /// </summary>
        public string TourID
        {
            set { _tourid = value; }
            get { return _tourid; }
        }
        /// <summary>
        /// 原始文件路径
        /// </summary>
        public string OriginalPath
        {
            set { _originalpath = value; }
            get { return _originalpath; }
        }
        /// <summary>
        /// 当前文件路径
        /// </summary>
        public string CurrentPath
        {
            set { _currentpath = value; }
            get { return _currentpath; }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorID
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }

        #endregion Model
    }
}
