using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 顾问团队业务逻辑接口
    /// </summary>
    /// 周文超 2010-07-15
    public interface ICommunityAdvisor
    {
        /// <summary>
        /// 添加顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_INSERT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_INSERT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ContactName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_INSERT_CODE)]
        int AddCommunityAdvisor(Model.CommunityStructure.CommunityAdvisor model);

        /// <summary>
        /// 修改顾问团队
        /// </summary>
        /// <param name="model">顾问团队实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_UPDATE,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ContactName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_UPDATE_CODE)]
        int UpdateCommunityAdvisor(Model.CommunityStructure.CommunityAdvisor model);

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorId">顾问团队ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvisorId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE_CODE)]
        bool DeleteCommunityAdvisor(int AdvisorId);

        /// <summary>
        /// 删除顾问团队
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvisorId"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_DELETE_CODE)]
        bool DeleteCommunityAdvisor(int[] AdvisorIds);

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorId">顾问团队ID</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvisorId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsCheck"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK_CODE)]
        bool SetIsCheck(int AdvisorId, bool IsCheck, int SysOperatorId);

        /// <summary>
        /// 审核顾问团队申请
        /// </summary>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <param name="IsCheck">审核状态</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvisorIds"",""AttributeType"":""array""},{""Index"":1,""Attribute"":""IsCheck"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_CHECK_CODE)]
        bool SetIsCheck(int[] AdvisorIds, bool IsCheck, int SysOperatorId);

        /// <summary>
        /// 设置顾问团队前台是否显示
        /// </summary>
        /// <param name="IsShow">前台是否显示</param>
        /// <param name="SysOperatorId">后台操作用户ID</param>
        /// <param name="AdvisorIds">顾问团队ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_Show_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_Show,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":2,""Attribute"":""AdvisorIds"",""AttributeType"":""array""},{""Index"":0,""Attribute"":""IsShow"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Advisor_Show_CODE)]
        bool SetIsShow(bool IsShow, int SysOperatorId, params int[] AdvisorIds);

        /// <summary>
        /// 获取顾问团队
        /// </summary>
        /// <param name="TopNum">取得顾问团队的数量(小于等于0取所有)</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        IList<Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int TopNum,bool? IsShow);

        /// <summary>
        /// 分页获取顾问团队
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsShow">是否前台显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        IList<Model.CommunityStructure.CommunityAdvisor> GetCommunityAdvisorList(int PageSize, int PageIndex, ref int RecordCount,bool? IsShow);

        /// <summary>
        /// 获取顾问团队实体
        /// </summary>
        /// <param name="CommunityAdvisorId">顾问团队</param>
        /// <returns>获取顾问团队实体</returns>
        Model.CommunityStructure.CommunityAdvisor GetCommunityAdvisor(int CommunityAdvisorId);
    }
}
