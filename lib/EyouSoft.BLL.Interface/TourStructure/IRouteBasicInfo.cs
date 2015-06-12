using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.TourStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-28
    /// 描述：线路信息 业务逻辑接口
    /// </summary>
    public interface IRouteBasicInfo
    {
        /// <summary>
        /// 验证是否存在同名线路
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="RouteName">线路名称</param>
        /// <returns></returns>
        bool Exists(string CompanyId, string RouteName);
        /// <summary>
        /// 写入线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_ADD,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.RouteLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""RouteName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]")]
        bool InsertRouteInfo(RouteBasicInfo routeInfo);

        /*
        /// <summary>
        /// 发送线路给专线商
        /// </summary>
        /// <param name="sendRoutes">发送线路信息业务实体</param>
        /// <returns>返回发送成功的总数</returns>
        int SendRouteToWholesalers(IList<RouteSendList> sendRoutes);
        */
        /// <summary>
        /// 获取线路信息实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        RouteBasicInfo GetRouteInfo(string routeId);

        /// <summary>
        /// 分页获取地接线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =0时返回公司所有用户发布的线路 >0时返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <param name="ReleaseType">线路发布类型 =null返回全部</param>
        /// <returns></returns>
        IList<RouteBasicInfo> GetLocaRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName, int[] AreaId, EyouSoft.Model.TourStructure.ReleaseType? ReleaseType);
        /// <summary>
        /// 根据指定条件获取线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =0时返回公司所有用户发布的线路 >0时返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <param name="ReleaseType">线路发布类型 =null返回全部</param>
        /// <returns></returns>
        IList<RouteBasicInfo> GetRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName, int[] AreaId, EyouSoft.Model.TourStructure.ReleaseType? ReleaseType);

        /// <summary>
        /// 更新线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_EDIT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_EDIT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.RouteLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""RouteName"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]")]
        bool UpdateRouteInfo(RouteBasicInfo routeInfo);
        /*
        /// <summary>
        /// 获取专线已接收线路数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetWholesalersAcceptRouteCount(string companyId);

        /// <summary>·
        /// 根据指定条件获取专线已接收的线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当面页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =0时返回公司所有用户发布的线路 >0时返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <returns></returns>
        IList<RouteBasicInfo> GetWholesalersAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName,int[] AreaId);

        /// <summary>
        /// 获取线路的报价详细信息集合
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        IList<RoutePriceDetail> GetRoutePriceDetails(string routeId);

        /// <summary>
        /// 根据指定条件获取专线已接收的线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当面页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <returns></returns>
        IList<WholesalerWaitAcceptRouteInfo> GetWholesalerWaitAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string routeName, int routeDays, string contactName);

        /// <summary>
        /// 批发商确认接收线路
        /// </summary>
        /// <param name="userId">接收线路的用户编号</param>
        /// <param name="waitAcceptIds">待接收信息编号(即发送线路给批发商的发送编号)</param>
        /// <returns></returns>
        int AcceptRoutes(int userId, string[] waitAcceptIds);

        /// <summary>
        /// 获取批发商等待接收的线路数量
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        int GetWholesalerWaitAcceptRouteCount(string companyId);
        */
        /// <summary>
        /// 删除线路信息
        /// </summary>
        /// <param name="routeId">要删除的线路编号</param>
        /// <returns></returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_DELETE_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.RouteLog.LOG_ROUTE_DELETE,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.RouteLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""routeId"",""AttributeType"":""array""}]")]
        bool DeleteRoutes(params string[] routeId);
    }
}
