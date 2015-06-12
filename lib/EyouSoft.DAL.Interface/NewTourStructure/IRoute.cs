using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IDAL.NewTourStructure
{
    /// <summary>
    /// 线路
    /// 创建者：郑付杰
    /// 创建时间：2011-12-19
    /// </summary>
    public interface IRoute
    {
        #region 增,删,改
        /// <summary>
        /// 添加线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>受影响行数</returns>
        int AddRoute(MRoute item);
        /// <summary>
        /// 批量修改线路上下架状态
        /// </summary>
        /// <param name="status">线路上下架状态</param>
        /// <param name="routes">线路编号</param>
        /// <returns>受影响行数</returns>
        int UpdateRouteStatus(RouteStatus status, string routes);
        /// <summary>
        /// 批量修改线路推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="routes">线路编号</param>
        /// <returns>受影响行数</returns>
        int UpdateRouteRecommend(RecommendType type, string routes);
        /// <summary>
        /// 修改点击量
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns>受影响行数</returns>
        int UpdateClick(string routeId);
        /// <summary>
        /// 修改线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>受影响行数</returns>
        int UpdateRoute(MRoute item);
        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="type">1：更新B2B 2：逻辑删除更改isdelete</param>
        /// <returns>受影响行数</returns>
        int DeleteRoute(string routeId, int type);

        #endregion

        #region 查询
        /// <summary>
        /// 获取打印线路行程单
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        MRoutePrint GetPrintModel(string routeId);
        /// <summary>
        /// 验证公司下线路名称是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称</param>
        /// <returns></returns>
        bool IsExits(string companyId, string routeName);
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns>线路实体</returns>
        MRoute GetModel(string routeId);
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="Id">线路自增编号</param>
        /// <returns>线路实体</returns>
        MRoute GetModel(long Id);
        /// <summary>
        /// 线路首页-最新旅游线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetNewRouteList(int topNum);
        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        IList<MRoute> GetRecommendList(int topNum, RecommendType? type, RouteB2BDisplay? b2BDisplay);
        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        IList<MRoute> GetRecommendList(int topNum, string companyId, RecommendType? type, RouteB2BDisplay? b2BDisplay);

        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MRoute> GetRecommendList(int topNum, MTuiJianRouteSearch search);

        /// <summary>
        /// 线路列表-组团社
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MRoute> GetList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, MRouteSearch search);
        /// <summary>
        /// 大平台线路列表(只显示签约，收费和认证的专线商线路)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetPublicCenterList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, MRouteSearch search);
        /// <summary>
        /// 运营后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetOperationsCenterList(int pageSize, int pageCurrent, ref int RecordCoun,
            int orderByt, int routeKeyType, MRouteSearch search);
        /// <summary>
        /// 用户后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetBackCenterList(int pageSize, int pageCurrent, ref int RecordCount, int orderBy,
            int routeKeyType, string companyId, MRouteSearch search);
        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cityId">城市</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="orderBy">排序规则 1:按推荐类型 2：按出发时间</param>
        /// <returns></returns>
        IList<MShopRoute> GetShopList(int topNum, int cityId, string companyId, int orderBy);
        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShopList(int pageSize, int pageCurrent, ref int recordCount,
            int orderBy, string companyId, MRouteSearch search);
        /// <summary>
        /// 线路区域对应的有效线路（地接社）
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetRouteByAreaStats(string companyId, string userAreas);
        #endregion
    }
}
