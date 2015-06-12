using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// IM中用户好友实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMFriendList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMFriendList()
        { }

        #region Model

        private string _uid;
        private string _friendid;
        private string _groupname;
        private DateTime _issuetime;

        /// <summary>
        /// MQ用户编号
        /// </summary>
        public string MQId
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 好友MQ编号
        /// </summary>
        public string FriendMQId
        {
            set { _friendid = value; }
            get { return _friendid; }
        }
        /// <summary>
        /// 好友分组组名
        /// </summary>
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }

        #region 扩展属性

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// MQ显示名称
        /// </summary>
        public string FriendName { get; set; }

        #endregion

        #endregion Model
    }
}
