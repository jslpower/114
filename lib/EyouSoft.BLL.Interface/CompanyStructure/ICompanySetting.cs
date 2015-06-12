using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-02
    /// 描述：单位设置接口
    /// </summary>
    public interface ICompanySetting
    {
        ///// <summary>
        ///// 修改单位设置
        ///// </summary>
        ///// <param name="model">单位设置实体</param>
        ///// <returns></returns>
        //[EyouSoft.AOPHandler.CommonLogHandler(
        //    LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_UPDATE_TITLE,
        //    LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
        //    LogAttribute = @"[{""Index"":0,""Attribute"":""CompanyId"",""AttributeType"":""class""}]",
        //    EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_UPDATE_CODE
        //    )]
        //bool Update(EyouSoft.Model.CompanyStructure.CompanySetting model);
        /// <summary>
        /// 获得单位设置实体
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <returns></returns>
        EyouSoft.Model.CompanyStructure.CompanySetting GetModel(string companyid);
        /// <summary>
        /// 更新单位设置--优先展示栏目位置
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="firstMenu">要优先显示的栏目</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_FIRSTMENU_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_FIRSTMENU_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_FIRSTMENU_UPDATE_CODE
            )]
        bool UpdateFirstMenu(string companyId, EyouSoft.Model.CompanyStructure.MenuSection firstMenu);
        /// <summary>
        /// 更新单位设置--订单刷新时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="minute">刷新时间(单位分钟)</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_ORDERREFRESH_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_ORDERREFRESH_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_ORDERREFRESH_UPDATE_CODE
            )]
        bool UpdateOrderRefresh(string companyId, int minute);
        /// <summary>
        /// 更新单位设置--团队自动停收时间
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="day">团队自动停收时间(单位天)</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYSETTING_TOURSTOPTIME_UPDATE_CODE
            )]
        bool UpdateTourStopTime(string companyId, int day);

        /// <summary>
        /// 更新公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="PositionInfo">地理位置信息实体</param>
        /// <returns></returns>
        bool UpdateCompanyPositionInfo(string companyId, EyouSoft.Model.ShopStructure.PositionInfo PositionInfo);

        /// <summary>
        /// 获取公司地理位置信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns>公司地理位置信息实体</returns>
        EyouSoft.Model.ShopStructure.PositionInfo GetCompanyPositionInfo(string companyId);
    }
}
