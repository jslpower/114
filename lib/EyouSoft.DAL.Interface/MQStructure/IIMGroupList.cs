﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// IM中用户自定义组数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
    public interface IIMGroupList
    {
        /// <summary>
        /// 获取某MQ用户的好友分组信息
        /// </summary>
        /// <param name="mqId">MQ用户ID</param>
        /// <returns>IM中用户自定义组实体</returns>
        IList<Model.MQStructure.IMGroupList> GetFriendGroups(int mqId);
    }
}
