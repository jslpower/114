using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// IM中用户好友业务逻辑接口
    /// </summary>
    /// 周文超 2010-06-12
    public interface IIMFriendList
    {
        /// <summary>
        /// 根据MQ用户ID获取该用户的所有的好友信息
        /// </summary>
        /// <param name="MQId">MQ用户ID</param>
        /// <returns>IM中用户好友实体集合</returns>
        IList<EyouSoft.Model.MQStructure.IMFriendList> GetFriendList(int MQId);

        /// <summary>
        /// 获取好友信息集合
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupName">分组名称 为空时返回不在任何分组里的好友</param>
        /// <returns></returns>
        IList<Model.MQStructure.IMFriendList> GetFriends(int mqId, string groupName);

        /// <summary>
        /// 同步好友信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        bool InStepFriends(int mqId);

        /// <summary>
        /// 获取某个MQ用户的好友MQID集合
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>好友MQID集合</returns>
        List<int> GetMyFriendIdList(int im_uid);
        /// <summary>
        /// 是否允许添加好友
        /// </summary>
        /// <param name="mqId">MQID</param>
        /// <returns></returns>
        bool IsAddFriend(int mqId);
        /// <summary>
        /// 累加好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        void AddFriendCount(int mqId);
    }
}
