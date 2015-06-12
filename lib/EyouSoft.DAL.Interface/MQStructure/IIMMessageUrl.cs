using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.MQStructure
{
    /// <summary>
    /// MQ消息提醒中转地址 数据访问接口
    /// </summary>
    /// 周文超 2010-05-11
    public interface IIMMessageUrl
    {
        /// <summary>
        /// 表中查询中转地址实体
        /// </summary>
        /// <param name="Id">MQ消息提醒中转地址ID</param>
        /// <returns></returns>
        Model.MQStructure.IMMessageUrl GetIm_Message_Url(string Id);
    }
}
