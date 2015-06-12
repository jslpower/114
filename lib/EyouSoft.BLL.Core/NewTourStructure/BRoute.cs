using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.BLL.NewTourStructure
{
    /// <summary>
    /// 线路
    /// 创建者：郑付杰
    /// 创建时间：2011-12-22
    /// </summary>
    public class BRoute : EyouSoft.IBLL.NewTourStructure.IRoute
    {
        private readonly EyouSoft.IDAL.NewTourStructure.IRoute dal =
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.NewTourStructure.IRoute>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewTourStructure.IRoute CreateInstance()
        {
            IBLL.NewTourStructure.IRoute op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.NewTourStructure.IRoute>();
            }
            return op;
        }

        #region 增,删,改
        /// <summary>
        /// 添加简易版线路(专线商)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns></returns>
        public string AddQuickRoute(MRoute item)
        {
            string routeId = string.Empty;
            if (item != null)
            {
                item.RouteId = Guid.NewGuid().ToString();
                item.RouteSource = RouteSource.专线商添加;
                item.StartCityName = GetCityName(item.StartCity);
                item.EndCityName = GetCityName(item.EndCity);
                //item.B2BOrder = 50;
                //item.B2COrder = 50;
                item.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Quick;
                if (dal.AddRoute(item) > 0)
                    routeId = item.RouteId;
            }
            return routeId;
        }
        /// <summary>
        /// 添加标准版线路(专线商)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns></returns>
        public string AddStandardRoute(MRoute item)
        {
            string routeId = string.Empty;
            if (item != null)
            {
                item.RouteId = Guid.NewGuid().ToString();
                item.RouteSource = RouteSource.专线商添加;
                item.StartCityName = GetCityName(item.StartCity);
                item.EndCityName = GetCityName(item.EndCity);
                //item.B2BOrder = 50;
                //item.B2COrder = 50;
                item.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Standard;
                if (dal.AddRoute(item) > 0)
                    routeId = item.RouteId;
            }
            return routeId;
        }
        /// <summary>
        /// 添加简易版线路(地接社)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        public string AddGroundQuickRoute(MRoute item)
        {
            string routeId = string.Empty;
            if (item != null)
            {
                item.RouteId = Guid.NewGuid().ToString();
                item.RouteSource = RouteSource.地接社添加;
                item.StartCityName = GetCityName(item.StartCity);
                item.EndCityName = GetCityName(item.EndCity);
                //item.B2BOrder = 50;
                //item.B2COrder = 50;
                item.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Quick;
                if (dal.AddRoute(item) > 0)
                    routeId = item.RouteId;
            }
            return routeId;
        }
        /// <summary>
        /// 添加标准版线路(地接社)
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>线路编号</returns>
        public string AddGroundStandardRoute(MRoute item)
        {
            string routeId = string.Empty;
            if (item != null)
            {
                item.RouteId = Guid.NewGuid().ToString();
                item.RouteSource = RouteSource.地接社添加;
                item.StartCityName = GetCityName(item.StartCity);
                item.EndCityName = GetCityName(item.EndCity);
                //item.B2BOrder = 50;
                //item.B2COrder = 50;
                item.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Standard;
                if (dal.AddRoute(item) > 0)
                    routeId = item.RouteId;
            }
            return routeId;
        }

        /// <summary>
        /// 批量修改线路上下架状态
        /// </summary>
        /// <param name="status">线路上下架状态</param>
        /// <param name="routes">线路编号</param>
        /// <returns></returns>
        public bool UpdateRouteStatus(RouteStatus status, params string[] routes)
        {
            bool result = false;
            if (routes.Length > 0)
            {
                if (dal.UpdateRouteStatus(status, ArrayToStr(routes)) > 0)
                    result = true;
            }
            return result;
        }
        /// <summary>
        /// 批量修改线路推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="routes">线路编号</param>
        /// <returns></returns>
        public bool UpdateRouteRecommend(RecommendType type, params string[] routes)
        {
            bool result = false;
            if (routes.Length > 0)
            {
                if (dal.UpdateRouteRecommend(type, ArrayToStr(routes)) > 0)
                    result = true;
            }
            return result;
        }
        /// <summary>
        /// 修改点击量
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public bool UpdateClick(string routeId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(routeId))
            {
                if (dal.UpdateClick(routeId) > 0)
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// 修改线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns></returns>
        public bool UpdateRoute(MRoute item)
        {
            bool result = false;
            if (item != null)
            {
                item.StartCityName = GetCityName(item.StartCity);
                item.EndCityName = GetCityName(item.EndCity);
                if (dal.UpdateRoute(item) > 0)
                    result = true;
            }
            return result;
        }
        /// <summary>
        /// 删除线路(用户后台-将b2b显示控制更改为隐藏)
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public bool DeleteBackRoute(string routeId)
        {
            bool reuslt = false;
            if (!string.IsNullOrEmpty(routeId))
            {
                if (dal.DeleteRoute(routeId, 1) > 0)
                    reuslt = true;
            }
            return reuslt;
        }
        /// <summary>
        /// 删除线路(运营后台)
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public bool DeleteSiteRoute(string routeId)
        {
            bool reuslt = false;
            if (!string.IsNullOrEmpty(routeId))
            {
                if (dal.DeleteRoute(routeId, 2) > 0)
                    reuslt = true;
            }
            return reuslt;
        }

        #endregion

        #region 查询
        /// <summary>
        /// 线路区域对应的有效线路（地接社）
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetCurrentUserRouteByAreaStats()
        {
            string companyId = new EyouSoft.Security.Membership.Utility().GetCurrentUserCompanyId();

            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = dal.GetRouteByAreaStats(companyId, new EyouSoft.Security.Membership.Utility().GetCurrentUserArea());

            if (stats != null && stats.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.AreaStatInfo tmp in stats)
                {
                    EyouSoft.Model.SystemStructure.SysArea areaInfo = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(tmp.AreaId);

                    if (areaInfo != null)
                    {
                        tmp.AreaName = areaInfo.AreaName;
                    }
                }
            }

            return stats;
        }
        /// <summary>
        /// 获取打印线路行程单
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public MRoutePrint GetPrintModel(string routeId)
        {
            MRoutePrint item = null;
            if (!string.IsNullOrEmpty(routeId))
            {
                item = dal.GetPrintModel(routeId);
            }
            return item;
        }
        /// <summary>
        /// 验证公司下线路名称是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称</param>
        /// <returns></returns>
        public bool IsExits(string companyId, string routeName)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(routeName))
            {
                result = dal.IsExits(companyId, routeName);
            }
            return result;
        }
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns>线路实体</returns>
        public MRoute GetModel(string routeId)
        {
            MRoute item = null;
            if (!string.IsNullOrEmpty(routeId))
            {
                item = dal.GetModel(routeId);
            }
            return item;
        }
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="Id">线路自增编号</param>
        /// <returns>线路实体</returns>
        public virtual MRoute GetModel(long Id)
        {
            MRoute item = null;
            if (Id > 0)
            {
                item = dal.GetModel(Id);
            }
            return item;
        }
        /// <summary>
        /// 线路首页-最新旅游线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <returns>线路集合</returns>
        public IList<MRoute> GetNewRouteList(int topNum)
        {
            topNum = topNum < 0 || topNum > 20 ? 10 : topNum;
            return dal.GetNewRouteList(topNum);
        }

        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        public IList<MRoute> GetRecommendList(int topNum, RecommendType? type, RouteB2BDisplay? b2BDisplay)
        {
            topNum = topNum < 0 || topNum > 20 ? 10 : topNum;
            return dal.GetRecommendList(topNum, type, b2BDisplay);
        }
        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        public IList<MRoute> GetRecommendList(int topNum, string companyId, RecommendType? type, RouteB2BDisplay? b2BDisplay)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                topNum = topNum < 0 || topNum > 20 ? 10 : topNum;
                return dal.GetRecommendList(topNum, companyId, type, b2BDisplay);
            }
            return null;
        }

        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRoute> GetRecommendList(int topNum, MTuiJianRouteSearch search)
        {
            return dal.GetRecommendList(topNum, search);
        }

        /// <summary>
        /// 线路列表-组团社
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MRoute> GetList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search)
        {
            return dal.GetList(pageSize, pageCurrent, ref RecordCount, 3, 1, search);
        }
        /// <summary>
        /// 大平台线路列表(只显示签约，收费和认证的专线商线路)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public IList<MRoute> GetPublicCenterList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search)
        {
            return dal.GetPublicCenterList(pageSize, pageCurrent, ref RecordCount, 1, 2, search);
        }
        /// <summary>
        /// 运营后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public IList<MRoute> GetOperationsCenterList(int pageSize, int pageCurrent, ref int RecordCount, MRouteSearch search)
        {
            return dal.GetOperationsCenterList(pageSize, pageCurrent, ref RecordCount, 2, 1, search);
        }
        /// <summary>
        /// 用户后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public IList<MRoute> GetBackCenterList(int pageSize, int pageCurrent, ref int RecordCount, string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetBackCenterList(pageSize, pageCurrent, ref RecordCount, 2, 3, companyId, search);
            }
            return null;
        }

        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cityId">出发城市</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShopList(int topNum, int cityId, string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                topNum = topNum > 0 ? topNum : 10;

                return dal.GetShopList(topNum, cityId, companyId, 0);
            }
            return null;
        }
        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShopList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetShopList(pageSize, pageCurrent, ref recordCount, 2, companyId, search);
            }
            return null;
        }

        #endregion

        #region internal
        /// <summary>
        /// 将数组线路编号转换成字符串
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>
        internal static string ArrayToStr(params string[] routes)
        {
            StringBuilder str = new StringBuilder();
            foreach (string s in routes)
            {
                str.AppendFormat("{0},", s);
            }
            return str.ToString().Substring(0, str.ToString().Length - 1);
        }
        /// <summary>
        /// 获取城市名称
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        internal static string GetCityName(int cityId)
        {
            string cityName = string.Empty;
            if (cityId > 0)
            {
                BLL.SystemStructure.SysCity bll = new EyouSoft.BLL.SystemStructure.SysCity();
                Model.SystemStructure.SysCity item = bll.GetSysCityModel(cityId);
                if (item != null)
                {
                    cityName = item.CityName;
                }
            }
            return cityName;
        }
        #endregion

    }
}
