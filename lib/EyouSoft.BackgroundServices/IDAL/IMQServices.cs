using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.BackgroundServices
{
    public interface IMQServices
    {
        /// <summary>
        /// 发送短线
        /// </summary>
        /// <param name="srcMq">发送人</param>
        /// <param name="dstMq">接收人</param>
        void SendMessage(int srcMq, int dstMq);

        /// <summary>
        /// 获得当天最近的MQ消息
        /// </summary>
        /// <param name="count">要获取的MQ消息条数,若为0,则表示查询所有</param>
        /// <returns></returns>
        IList<EyouSoft.Model.MQStructure.IMMessage> GetTodayLastMessage(int count);
    }
}
