using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ消息提醒中转地址业务逻辑接口
    /// </summary>
    /// 周文超 2010-06-12
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
