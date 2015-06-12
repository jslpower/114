using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.BackgroundServices
{
    /// <summary>
    /// 短信服务接口
    /// </summary>
    /// Author:张志瑜 2010-09-21
    public interface ISMSServices
    {
        /// <summary>
        /// 获得要发送的短信
        /// </summary>
        /// <returns></returns>
        Queue<EyouSoft.Model.SMSStructure.SendPlanInfo> Get();
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sms">待发送的短信实体</param>
        void Send(EyouSoft.Model.SMSStructure.SendPlanInfo sms);
    }
}
