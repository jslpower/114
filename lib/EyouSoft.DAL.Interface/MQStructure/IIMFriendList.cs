using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// IM中用户好友数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
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
        /// 同步好友分组信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        bool InStepFriendGroups(int mqId);

        /// <summary>
        /// 获取某个MQ用户的好友MQID集合
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>好友MQID集合</returns>
        List<int> GetMyFriendIdList(int im_uid);
        /// <summary>
        /// 获得已添加的MQ好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        /// <param name="findType">查找类型: 0全部好友  1:当天所添加的好友</param>
        /// <returns></returns>
        int GetFriendCount(int mqId, int findType);
        /// <summary>
        /// 累加好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        void AddFriendCount(int mqId);
    }
}
