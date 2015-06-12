using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.AdvStructure
{
    /// <summary>
    /// 广告业务逻辑类接口
    /// </summary>
    /// Author:汪奇志 2010-07-15
    public interface IAdv
    {
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_InsertAdv_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_InsertAdv,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_InsertAdv_CODE)]
        int InsertAdv(EyouSoft.Model.AdvStructure.AdvInfo info);

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_UpdateAdv_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_UpdateAdv,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AdvId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_UpdateAdv_CODE)]
        int UpdateAdv(EyouSoft.Model.AdvStructure.AdvInfo info);

        /// <summary>
        /// 更新单位广告链接
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AdvLink">广告链接</param>
        /// <returns>1:成功 0:失败</returns>
        int UpdateAdv(string CompanyId, string AdvLink);

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_DeleteAdv_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_DeleteAdv,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""advId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_DeleteAdv_CODE)]
        bool DeleteAdv(int advId);

        /// <summary>
        /// 是否有效(添加时用)
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="range">投放范围</param>
        /// <param name="relation">关联信息(城市或单位类型编号)集合</param>
        /// <returns></returns>
        bool IsValid(EyouSoft.Model.AdvStructure.AdvPosition position,DateTime startDate,DateTime endDate
            ,EyouSoft.Model.AdvStructure.AdvRange range,IList<int> relation);

        /// <summary>
        /// 是否有效(修改时用)
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="range">投放范围</param>
        /// <param name="relation">关联信息(城市或单位类型编号)集合</param>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        bool IsValid(EyouSoft.Model.AdvStructure.AdvPosition position, DateTime startDate, DateTime endDate
            , EyouSoft.Model.AdvStructure.AdvRange range, IList<int> relation, int advId);

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        EyouSoft.Model.AdvStructure.AdvInfo GetAdvInfo(int advId);

        /// <summary>
        /// 设置广告排序
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <param name="position">广告位置</param>
        /// <param name="relationId">关联编号</param>
        /// <param name="sortId">排序编号</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_SetAdvSort_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_SetAdvSort,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""advId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Adv_SetAdvSort_CODE)]
        bool SetAdvSort(int advId, EyouSoft.Model.AdvStructure.AdvPosition position, int relationId, int sortId);

        /*/// <summary>
        /// 获取广告关系集合
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        IList<int> GetAdvRelation(int advId);*/

        /// <summary>
        /// 获取广告位置信息
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        EyouSoft.Model.AdvStructure.AdvPositionInfo GetPositionInfo(EyouSoft.Model.AdvStructure.AdvPosition position);

        /// <summary>
        /// 按照指定条件获取广告信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="position">广告位置</param>
        /// <param name="relationId">关联编号(城市或单位类型编号)</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="category">广告类别 为null时不做为查询条件</param>
        /// <param name="startDate">有效期起始时间 为null时不做为查询条件</param>
        /// <param name="endDate">有效期截止时间 为null时不做为查询条件</param>
        /// <param name="title">广告标题 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.AdvStructure.AdvPosition position
            , int relationId, string companyName, EyouSoft.Model.AdvStructure.AdvCategory? category, DateTime? startDate, DateTime? endDate,string title);

        /// <summary>
        /// 获取快到期广告信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="advType">广告投放类型</param>
        /// <param name="relationId">关联编号()</param>
        /// <param name="catelog"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.AdvInfo> GetComingExpireAdvs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.AdvStructure.AdvType advType
            , int relationId, EyouSoft.Model.AdvStructure.AdvCatalog? catelog, string companyName);

        /// <summary>
        /// 获取指定位置的广告信息集合
        /// </summary>
        /// <param name="relationId">城市或单位类型(MQ)编号</param>
        /// <param name="position">广告位置</param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(int relationId, EyouSoft.Model.AdvStructure.AdvPosition position);
        /// <summary>
        /// 获取指定位置的广告信息集合
        /// </summary>
        /// <param name="relationId">城市或单位类型(MQ)编号</param>
        /// <param name="position">广告位置</param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.AdvInfo> GetNotFillAdvs(int relationId, EyouSoft.Model.AdvStructure.AdvPosition position);
        /// <summary>
        /// 获取广告位置信息集合
        /// </summary>
        /// <param name="catalog">广告栏目</param>
        /// <returns></returns>
        IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> GetPositions(EyouSoft.Model.AdvStructure.AdvCatalog catalog);
    }
}
