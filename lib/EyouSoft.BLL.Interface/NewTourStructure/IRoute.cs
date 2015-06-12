using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IBLL.NewTourStructure
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
        /// 添加简易版线路(专线商)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        string AddQuickRoute(MRoute item);
        /// <summary>
        /// 添加标准版线路(专线商)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        string AddStandardRoute(MRoute item);
        /// <summary>
        /// 添加简易版线路(地接社)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        string AddGroundQuickRoute(MRoute item);
        /// <summary>
        /// 添加标准版线路(地接社)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        string AddGroundStandardRoute(MRoute item);
        /// <summary>
        /// 批量修改线路上下架状态
        /// </summary>
        /// <param name="status">线路上下架状态</param>
        /// <param name="routes">线路编号</param>
        /// <returns></returns>
        bool UpdateRouteStatus(RouteStatus status, params string[] routes);
        /// <summary>
        /// 批量修改线路推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="routes">线路编号</param>
        /// <returns></returns>
        bool UpdateRouteRecommend(RecommendType type, params string[] routes);
        /// <summary>
        /// 修改点击量
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        bool UpdateClick(string routeId);
        /// <summary>
        /// 修改线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns></returns>
        bool UpdateRoute(MRoute item);
        /// <summary>
        /// 删除线路(用户后台-将b2b显示控制更改为隐藏)
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        bool DeleteBackRoute(string routeId);
        /// <summary>
        /// 删除线路(运营后台)
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        bool DeleteSiteRoute(string routeId);

        #endregion

        #region 查询

        /// <summary>
        /// 线路区域对应的有效线路（地接社）
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetCurrentUserRouteByAreaStats();

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
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MRoute> GetList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search);
        /// <summary>
        /// 大平台线路列表(只显示签约，收费和认证的专线商线路)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetPublicCenterList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search);
        /// <summary>
        /// 运营后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetOperationsCenterList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search);
        /// <summary>
        /// 用户后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        IList<MRoute> GetBackCenterList(int pageSize, int pageCurrent, ref int RecordCount, string companyId, MRouteSearch search);
        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cityId">出发城市</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MShopRoute> GetShopList(int topNum, int cityId, string companyId);
        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShopList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search);
       
        #endregion
    }
}
