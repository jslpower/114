using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CompanyStructure
{
    /// <summary>
    /// 描述:单位经营城市[包括出港城市,销售城市]接口层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public interface ICompanyCity
    {
        /// <summary>
        /// 获得公司所拥有的销售城市列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanySaleCity(string companyId);    
        /// <summary>
        /// 获得公司常用的出港城市列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanyPortCity(string companyId);
        /// <summary>
        /// 设置公司常用的出港城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="items">常用的出港城市列表</param>
        /// <returns></returns>
        [EyouSoft.AOPHandler.CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYPORTCITY_UPDATE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYPORTCITY_UPDATE, LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""companyId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_COMPANYPORTCITY_UPDATE_CODE
            )]
        bool SetCompanyPortCity(string companyId, IList<EyouSoft.Model.SystemStructure.CityBase> items);
    }

    /// <summary>
    /// 描述:单位经营线路区域接口层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public interface ICompanyArea
    {
        /// <summary>
        /// 获得公司所拥有的线路区域列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.AreaBase> GetCompanyArea(string companyId);   
        /// <summary>
        /// 获得用户所拥有的线路区域列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.AreaBase> GetUserArea(string userId);
    }

    /// <summary>
    /// 描述:单位待增加的经营城市[包括出港城市,销售城市]接口层
    /// 创建人：张志瑜 2010-07-14
    /// </summary>
    public interface ICompanyUnCheckedCity
    {
        /// <summary>
        /// 新增公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="cityText">城市名称(多个城市,则城市名称使用逗号分割)</param>
        /// <returns></returns>
        bool AddSaleCity(string companyId, string cityText);
        /// <summary>
        /// 获得公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        string GetSaleCity(string companyId);
        /// <summary>
        /// 删除公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        bool DeleteSaleCity(string companyId);
    }
}
