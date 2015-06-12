using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 城市 业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-26
    public interface ISysCity
    {
        #region 城市函数

        /// <summary>
        /// 根据城市ID获取城市实体(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>系统城市实体</returns>
        Model.SystemStructure.SysCity GetSysCityModel(int CityId);

        /// <summary>
        /// 根据城市域名获取城市实体(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="DomainName">城市域名</param>
        /// <returns>系统城市实体</returns>
        Model.SystemStructure.SysCity GetSysCityModel(string DomainName);

         /// <summary>
        /// 获取所有的销售城市
        /// </summary>
        /// <param name="IsSite">是否出港城市(为true取既是销售也是出港的城市)</param>
        /// <param name="ProvinceId">省份ID集合</param>
        /// <returns>城市实体集合</returns>
        IList<Model.SystemStructure.SysCity> GetSaleCity(bool? IsSite, params int[] ProvinceId);

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns>返回城市实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList();

        /// <summary>
        /// 根据城市ID集合获取城市实体集合
        /// </summary>
        /// <param name="CityIds">城市ID集合</param>
        /// <returns>城市实体集合</returns>
        IList<Model.SystemStructure.SysCity> GetSysCityList(int[] CityIds);

        /// <summary>
        /// 根据省份ID集合获取城市实体集合
        /// </summary>
        /// <param name="ProvinceIds">省份ID集合</param>
        /// <returns>城市实体集合</returns>
        IList<Model.SystemStructure.SysCity> GetProvinceCityList(int[] ProvinceIds);

        /// <summary>
        /// 根据省份ID获取城市实体集合
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="IsSite">是否出港城市(为null取该省下所有城市)</param>
        /// <returns>城市实体集合</returns>
        IList<Model.SystemStructure.SysCity> GetSysCityList(int ProvinceId, bool? IsSite);
        /// <summary>
        /// 获得指定城市ID的城市信息集合
        /// </summary>
        /// <param name="items">城市ID集合</param>
        IList<EyouSoft.Model.SystemStructure.CityBase> GetCityList(IList<EyouSoft.Model.SystemStructure.CityBase> items);
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="ProvinceId">省份ID，小于等于0不作条件</param>
        /// <param name="CenterCityId">省会城市ID，小于等于0不作条件</param>
        /// <param name="HeaderLetter">首字母，为null或者空不作条件</param>
        /// <param name="IsSite">是否出港城市(为null不作条件)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <returns>返回城市实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int ProvinceId, int CenterCityId, string HeaderLetter, bool? IsSite
            , bool? IsEnabled);

        /// <summary>
        /// 获取城市列表(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1：城市首字母升/降序；2/3：城市ID升/降序；)</param>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否停用</param>
        /// <returns>城市列表</returns>
        IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int ProvinceId, int CityId, bool? IsEnabled);

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsEnabled_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsEnabled,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsEnabled"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsEnabled_CODE)]
        bool SetIsEnabled(int CityId, bool IsEnabled);

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""IsSite"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite_CODE)]
        bool SetIsSite(int CityId, bool IsSite);

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityIds">城市ID集合</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityIds"",""AttributeType"":""array""},{""Index"":1,""Attribute"":""IsSite"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysCity_SetIsSite_CODE)]
        bool SetIsSite(int[] CityIds, bool IsSite);

        #endregion

        #region 城市线路区域关系函数

        /// <summary>
        /// 添加城市线路区域关系
        /// </summary>
        /// <param name="List">城市线路区域关系集合</param>
        /// <param name="CityId">城市ID(小于等于0则取集合中的)</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_ADD,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":1,""Attribute"":""CityId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_ADD_CODE)]
        int AddSysSiteAreaControl(IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List, int CityId);

        /// <summary>
        /// 修改城市线路区域关系(先删后加模式)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="List">城市线路区域关系集合</param>
        /// <returns>-1:添加新关系失败;0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_EDIT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_EDIT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_EDIT_CODE)]
        int UpdateSysSiteAreaControl(int CityId, IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List);

        /// <summary>
        /// 根据城市ID删除城市线路区域关系(该城市下所有线路区域)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_DelByCity_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_DelByCity,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_DelByCity_CODE)]
        bool DeleteSysSiteAreaControl(int CityId);

        /// <summary>
        /// 删除城市线路区域关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_Delete_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_Delete,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""AreaIds"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_CityArea_Delete_CODE)]
        bool DeleteSysSiteAreaControl(int CityId, int[] AreaIds);

        #endregion

        #region 系统IP函数

        /// <summary>
        /// 根据客户端Ip地址返回城市基类实体
        /// </summary>
        /// <param name="IpAddress">客户端Ip地址</param>
        /// <returns>城市基类实体</returns>
        Model.SystemStructure.CityBase GetClientCityByIp(string IpAddress);

        #endregion

        #region 城市缓存清除
        /// <summary>
        /// 单个城市缓存清除
        /// </summary>
        /// <param name="CityId">城市编号</param>
        void ClearCache(int CityId);
        #endregion
    }
}
