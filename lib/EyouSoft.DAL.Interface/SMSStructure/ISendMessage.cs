using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SMSStructure
{
    /// <summary>
    /// 短信中心-发送短信数据访问类接口
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public interface ISendMessage
    {
        /// <summary>
        /// 写入短信发送明细及统计信息，并更新账户余额
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <param name="sendDetailsInfo">短信发送明细</param>
        /// <param name="sendResultInfo">发送短信/验证发送结果业务实体</param>
        /// <returns></returns>
        bool InsertSendInfo(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo, IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendDetailsInfo, EyouSoft.Model.SMSStructure.SendResultInfo sendResultInfo);

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
        /// 写入定时发送短信任务计划
        /// </summary>
        /// <param name="planInfo">任务计划信息业务实体</param>
        /// <returns></returns>
        bool InsertSendPlan(EyouSoft.Model.SMSStructure.SendPlanInfo planInfo);
    }
}
