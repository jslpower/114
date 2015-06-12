using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    /// <summary>
    /// IM中用户自定义组实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMGroupList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMGroupList()
		{}

		#region Model

		/// <summary>
		/// MQ用户编号
		/// </summary>
        public string MQId
        {
            set;
            get;
        }
		/// <summary>
		/// MQ好友组名
		/// </summary>
        public string GroupName
        {
            set;
            get;
        }
		/// <summary>
		/// 组好友数量
		/// </summary>
        public int FriendCount
        {
            set;
            get;
        }
		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime IssueTime
        {
            set;
            get;
        }

        #region 扩展属性

        /// <summary>
        /// 在线人数
        /// </summary>
        public int OnlineFriendCount { get; set; }
        /// <summary>
        /// 不在线人数
        /// </summary>
        public int OfflineFriendCount { get { return this.FriendCount - this.OnlineFriendCount; } }

        #endregion

        #endregion Model
    }
}
