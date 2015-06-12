using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.ToolStructure
{
    /// <summary>
    /// 消息提醒记录接口
    /// </summary>
    /// Author:张志瑜  2010-10-25
    public interface IMsgTipRecord
    {
        /// <summary>
        /// 是否发送消息提醒
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="sendWay">消息发送途径</param>
        /// <param name="mqID">接收消息用户MQID</param>
        /// <param name="cityId">当前用户所在城市</param>
        /// <returns></returns>
        bool IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType msgType, EyouSoft.Model.ToolStructure.MsgSendWay sendWay, string mqID, int cityId);

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">消息提醒记录实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.ToolStructure.MsgTipRecord model);
    }
}
