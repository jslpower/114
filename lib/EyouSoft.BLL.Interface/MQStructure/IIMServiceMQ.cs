using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ客服 业务逻辑访问接口
    /// </summary>
    /// 周文超 2010-05-11
    public interface IIMServiceMQ
    {
        /// <summary>
        /// 插入mq客服号码为好友
        /// </summary>
        /// <param name="mqId">用户的MQID</param>
        /// <param name="provinceId">用户所在的省份</param>
        /// <returns></returns>
        bool InsertFriendServiceMQ(int mqId, int provinceId);
    }
}
