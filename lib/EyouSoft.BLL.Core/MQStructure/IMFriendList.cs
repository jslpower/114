using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;
using System.Transactions;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// IM中用户好友业务逻辑
    /// </summary>
    /// 周文超 2010-06-12
    public class IMFriendList : IBLL.MQStructure.IIMFriendList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMFriendList() { }

        private readonly IDAL.MQStructure.IIMFriendList dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMFriendList>();

        /// <summary>
        /// 构造IM中用户好友业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMFriendList CreateInstance()
        {
            IBLL.MQStructure.IIMFriendList op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMFriendList>();
            }
            return op;
        }

        #region IIMFriendList 成员

        /// <summary>
        /// 根据MQ用户ID获取该用户的所有的好友信息
        /// </summary>
        /// <param name="MQId">MQ用户ID</param>
        /// <returns>IM中用户好友实体集合</returns>
        public IList<EyouSoft.Model.MQStructure.IMFriendList> GetFriendList(int MQId)
        {
            if (MQId <= 0)
                return null;

            return dal.GetFriendList(MQId);
        }

        /// <summary>
        /// 获取好友信息集合
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <param name="groupName">分组名称 为空时返回不在任何分组里的好友</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.MQStructure.IMFriendList> GetFriends(int mqId, string groupName)
        {
            if (mqId <= 0)
                return null;

            return dal.GetFriends(mqId, groupName);
        }

        /// <summary>
        /// 同步好友信息
        /// </summary>
        /// <param name="mqId">MQ编号</param>
        /// <returns></returns>
        public bool InStepFriends(int mqId)
        {
            if (mqId <= 0)
                return false;

            bool Result = false;
            //using (TransactionScope Tran = new TransactionScope())
            //{
            Result = dal.InStepFriendGroups(mqId);
            //if (!Result)
            //    return Result;
            Result = dal.InStepFriends(mqId);
            //if (!Result)
            //    return Result;

            //    Tran.Complete();
            //}
            return Result;
        }

        /// <summary>
        /// 获取某个MQ用户的好友MQID集合
        /// </summary>
        /// <param name="im_uid">MQID</param>
        /// <returns>好友MQID集合</returns>
        public List<int> GetMyFriendIdList(int im_uid)
        {
            if (im_uid <= 0)
                return null;

            return dal.GetMyFriendIdList(im_uid);
        }

        /// <summary>
        /// 是否允许添加好友
        /// </summary>
        /// <param name="mqId">MQID</param>
        /// <returns></returns>
        public bool IsAddFriend(int mqId)
        {
            bool isAdd = false;
            //MQ中当天可添加的好友数量
            int todayMaxFriendCout = 0;

            Int32.TryParse(EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("MQConfig", "TodayMaxFriendCout"), out todayMaxFriendCout);
            if (todayMaxFriendCout <= -1)  //不限制
                isAdd = true;
            else if (todayMaxFriendCout > 0)
            {
                int addcount = dal.GetFriendCount(mqId, 1);
                if (addcount < todayMaxFriendCout)
                    isAdd = true;
            }
            return isAdd;
        }

        /// <summary>
        /// 累加好友数量
        /// </summary>
        /// <param name="mqId">MQID</param>
        public void AddFriendCount(int mqId)
        {
            dal.AddFriendCount(mqId);
        }
        #endregion
    }
}
