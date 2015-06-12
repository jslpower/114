using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 线路区域 业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-27
    /// -----------------------
    /// 修改人：鲁功源 2011-04-06
    /// 新增内容：获取存在团队信息的线路区域集合方法GetExistsTourAreas()
    public interface ISysArea
    {
        #region 线路区域函数

        /// <summary>
        /// 获取线路区域实体
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>线路区域实体</returns>
        Model.SystemStructure.SysArea GetSysAreaModel(int AreaId);

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList();

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="RouteType">线路类型</param>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList(Model.SystemStructure.AreaType RouteType);

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList(int[] AreaIds);
        /// <summary>
        /// 获得指定线路区域ID的线路区域信息集合
        /// </summary>
        /// <param name="items">线路区域ID集合</param>
        IList<EyouSoft.Model.SystemStructure.AreaBase> GetAreaList(IList<EyouSoft.Model.SystemStructure.AreaBase> items);
        /// <summary>
        /// 新增线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Add_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AreaId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Add_CODE)]
        int AddSysArea(EyouSoft.Model.SystemStructure.SysArea model);

        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AreaId"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Edit_CODE)]
        int UpdateSysArea(EyouSoft.Model.SystemStructure.SysArea model);

        /// <summary>
        /// 删除线路区域(物理删除，有有效计划时删除失败)
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Del_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Del,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AreaId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_SysArea_Del_CODE)]
        bool DeleteSysArea(int AreaId);
        /// <summary>
        /// 获取存在线路数据的线路区域集合
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysArea> GetExistsTourAreas();

        /// <summary>
        /// 获取包含某线路区域的所有分站（销售城市）
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        IList<Model.SystemStructure.CityBase> GetSalesCityByArea(int areaId);

        #endregion

        #region 长短线区域城市关系函数成员

        /// <summary>
        /// 添加长短线区域城市关系
        /// </summary>
        /// <param name="List">长短线区域城市关系实体集合</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_AddTitle,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_Add_CODE)]
        int AddSysAreaSiteControl(IList<Model.SystemStructure.SysAreaSiteControl> List);

        /// <summary>
        /// 获取所有的长短线区域城市关系
        /// </summary>
        /// <returns>长短线区域城市关系实体集合</returns>
        IList<Model.SystemStructure.SysAreaSiteControl> GetSysAreaSiteControl();

        /// <summary>
        /// 获取某省份的国内长线
        /// </summary>
        /// <param name="ProvinceId">省份ID(小于等于0取所有)</param>
        /// <returns>长短线区域城市关系实体集合</returns>
        IList<Model.SystemStructure.SysAreaSiteControl> GetLongAreaSiteControl(int ProvinceId);

        /// <summary>
        /// 获取某城市的国内短线
        /// </summary>
        /// <param name="CityId">城市ID(小于等于0取所有)</param>
        /// <returns>长短线区域城市关系实体集合</returns>
        IList<Model.SystemStructure.SysAreaSiteControl> GetShortAreaSiteControl(int CityId);

        /// <summary>
        /// 删除国内长线和省份的关系(删除某一省份下的所有)
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ProvinceId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPId_CODE)]
        bool DeleteLongAreaSiteControl(int ProvinceId);

        /// <summary>
        /// 删除国内长线和省份的关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ProvinceId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""AreaId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId_CODE)]
        bool DeleteLongAreaSiteControl(int ProvinceId, int AreaId);

        /// <summary>
        /// 删除国内长线和省份的关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""ProvinceId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""AreaIds"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByPIdAndAId_CODE)]
        bool DeleteLongAreaSiteControl(int ProvinceId, int[] AreaIds);

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCId_CODE)]
        bool DeleteShortAreaSiteControl(int CityId);

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""AreaId"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId_CODE)]
        bool DeleteShortAreaSiteControl(int CityId, int AreaId);

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""CityId"",""AttributeType"":""val""},{""Index"":1,""Attribute"":""AreaIds"",""AttributeType"":""array""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_AreaSite_DelByCIdAndAId_CODE)]
        bool DeleteShortAreaSiteControl(int CityId, int[] AreaIds);

        #endregion
    }
}
