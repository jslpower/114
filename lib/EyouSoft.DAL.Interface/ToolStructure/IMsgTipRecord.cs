using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ToolStructure
{
    /// <summary>
    /// 消息提醒记录接口
    /// </summary>
    /// Author:张志瑜  2010-10-21
    public interface IMsgTipRecord
    {
        /// <summary>
        /// 是否连续发送
        /// </summary>
        /// <param name="mqID">MQ号</param>
        /// <param name="validNum">连续数</param>
        /// <returns></returns>
        bool IsValid(string mqID, int validNum);

        /// <summary>
        /// 是否被禁用
        /// </summary>
        /// <param name="mqID">MQ号</param>
        /// <returns></returns>
        bool IsValid(string mqID);

        /// <summary>
        /// 获取当天短信总条数
        /// </summary>
        /// <returns></returns>
        int GetTodayCount();

        /// <summary>
        /// 获取短信剩余条数
        /// </summary>
        /// <returns></returns>
        int GetSmsRemain();

        /// <summary>
        /// 新增禁用记录
        /// </summary>
        /// <param name="mqID">MQ</param>
        /// <param name="EnableTime">启用时间</param>
        /// <returns></returns>
        bool Add(string mqID, DateTime EnableTime);

        /// <summary>
        /// 获取当天提醒次数
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="sendWay">消息发送途径</param>
        /// <param name="mqID">接收消息用户MQID</param>
        /// <returns></returns>
        int GetTodayCount(EyouSoft.Model.ToolStructure.MsgType msgType, EyouSoft.Model.ToolStructure.MsgSendWay sendWay, string mqID);

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">消息提醒记录实体</param>
        /// <returns></returns>
        bool Add(EyouSoft.Model.ToolStructure.MsgTipRecord model);
    }
}
