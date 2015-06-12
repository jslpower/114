using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 团队留言
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourMessage()
        { }

        #region Model

        private string _id;
        private string _tourid;
        private string _clientip;
        private string _clientuserid;
        private string _contect;
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
        /// 浏览IP
        /// </summary>
        public string ClientIP
        {
            set { _clientip = value; }
            get { return _clientip; }
        }
        /// <summary>
        /// 浏览人ID
        /// </summary>
        public string ClientUserID
        {
            set { _clientuserid = value; }
            get { return _clientuserid; }
        }
        /// <summary>
        /// 客户留言内容
        /// </summary>
        public string Contect
        {
            set { _contect = value; }
            get { return _contect; }
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
