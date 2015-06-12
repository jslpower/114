using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SMSStructure
{
    /// <summary>
    /// 短信中心-发送短信业务逻辑类接口
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public interface ISendMessage
    {
        /// <summary>
        /// 验证要发送的短信
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <returns></returns>
        EyouSoft.Model.SMSStructure.SendResultInfo ValidateSend(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <returns></returns>
        EyouSoft.Model.SMSStructure.SendResultInfo Send(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo);

        /// <summary>
        /// 根据指定条件获取发送短信历史记录
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <param name="startTime">发送开始时间 为空时不做为查询条件</param>
        /// <param name="finishTime">发送截止时间 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendHistorys(int pageSize, int pageIndex, ref int recordCount, string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime);

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送号码列表
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendDetails(string totalId, string companyId, int sendStatus);

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送短信的内容
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        string GetSendContent(string totalId, string companyId);

        /// <summary>
        /// 根据指定条件获取所有发送短信历史记录
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <param name="startTime">发送开始时间 为空时不做为查询条件</param>
        /// <param name="finishTime">发送截止时间 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetAllSendHistorys(string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime);
        /// <summary>
        /// 发送订单预定短信，返回1成功，其它失败
        /// </summary>
        /// <param name="info">订单短信信息业务实体</param>
        /// <returns></returns>
        int SendOrderSMS(EyouSoft.Model.NewTourStructure.MOrderSmsInfo info);
    }
}
