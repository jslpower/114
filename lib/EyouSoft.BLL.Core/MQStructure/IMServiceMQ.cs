using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ客服业务逻辑
    /// </summary>
    /// 周文超 2010-07-09
    public class IMServiceMQ : IBLL.MQStructure.IIMServiceMQ
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMServiceMQ() { }

        private readonly IDAL.MQStructure.IIMServiceMQ dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMServiceMQ>();

        /// <summary>
        /// 构造MQ消息提醒中转地址业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMServiceMQ CreateInstance()
        {
            IBLL.MQStructure.IIMServiceMQ op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMServiceMQ>();
            }
            return op;
        }



        #region IIMServiceMQ 成员

        /// <summary>
        /// 插入mq客服号码为好友
        /// </summary>
        /// <param name="mqId">用户的MQID</param>
        /// <param name="provinceId">用户所在的省份</param>
        /// <returns></returns>
        public bool InsertFriendServiceMQ(int mqId, int provinceId)
        {
            if (mqId <= 0 || provinceId <= 0)
                return false;

            return dal.InsertFriendServiceMQ(mqId, provinceId);
        }

        #endregion
    }
}
