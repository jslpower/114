using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.BLL.NewTourStructure
{
    /// <summary>
    /// 散拼计划
    /// </summary>
    public class BPowderList : EyouSoft.IBLL.NewTourStructure.IPowderList
    {
        #region
        private readonly EyouSoft.IDAL.NewTourStructure.IPowderList dal =
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.NewTourStructure.IPowderList>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewTourStructure.IPowderList CreateInstance()
        {
            IBLL.NewTourStructure.IPowderList op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.NewTourStructure.IPowderList>();
            }
            return op;
        }
        #endregion

        #region 增,删,改
        /// <summary>
        /// 添加散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        public bool AddPowder(MPowderList item)
        {
            bool result = false;
            if (item != null)
            {
                item.TourId = Guid.NewGuid().ToString();
                item.StartCityName = BRoute.GetCityName(item.StartCity);
                item.EndCityName = BRoute.GetCityName(item.EndCity);
                item.ComeBackDate = item.LeaveDate.AddDays(item.Day);
                if (dal.AddPowder(item) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        public bool UpdatePowder(MPowderList item)
        {
            bool result = false;
            if (item != null)
            {
                item.StartCityName = BRoute.GetCityName(item.StartCity);
                item.EndCityName = BRoute.GetCityName(item.EndCity);
                item.ComeBackDate = item.LeaveDate.AddDays(item.Day);
                if (dal.UpdatePowder(item) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 删除散拼计划
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public bool DeletePowder(params string[] tourId)
        {
            bool result = false;
            if (tourId != null && tourId.Length > 0)
            {
                if (dal.DeletePowder(BRoute.ArrayToStr(tourId)) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 批量修改散拼团状态
        /// </summary>
        /// <param name="status">散拼团状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool UpdateStatus(PowderTourStatus status, params string[] tourId)
        {
            bool result = false;
            if (tourId != null && tourId.Length > 0)
            {
                if (dal.UpdateStatus(status, BRoute.ArrayToStr(tourId)) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 批量修改散拼团推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool UpdatePowderRecommend(RecommendType type, params string[] tourId)
        {
            bool result = false;
            if (tourId != null && tourId.Length > 0)
            {
                if (dal.UpdatePowderRecommend(type, BRoute.ArrayToStr(tourId)) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 批量修改散拼计划
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool UpdatePowerList(IList<MPowderList> list)
        {
            bool result = false;
            if (list != null && list.Count > 0)
            {
                if (dal.UpdatePowerList(list) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="leaveDates">出团日期</param>
        /// <param name="registrationDate">报名截止日期</param>
        /// <returns></returns>
        public bool AddBatchPowder(MPowderList item, DateTime[] leaveDates, DateTime[] registrationDate)
        {
            bool result = false;
            if (item != null && leaveDates != null && leaveDates.Length > 0 
                && registrationDate != null && registrationDate.Length > 0 && !string.IsNullOrEmpty(item.Publishers))
            {
                if (dal.AddBatchPowder(item, leaveDates,registrationDate) > 0)
                {
                    result = true;
                }
            }
            return result;

        }

        /// <summary>
        /// 批量修改行程
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public bool UpdateStandardPlan(MPowderList item, params string[] tourId)
        {
            bool result = false;
            if (item != null && tourId != null && tourId.Length > 0)
            {
                if (dal.UpdateStandardPlan(item, tourId) > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 线路区域对应的散拼计划数量
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetCurrentUserTourByAreaStats()
        {
            string companyId = new EyouSoft.Security.Membership.Utility().GetCurrentUserCompanyId();

            if (string.IsNullOrEmpty(companyId)) { return null; }

            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = dal.GetTourByAreaStats(companyId, new EyouSoft.Security.Membership.Utility().GetCurrentUserArea());

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
        /// 到期时期还有1周
        /// </summary>
        /// <returns></returns>
        public int GetExpirePowder()
        {
            string companyId = new EyouSoft.Security.Membership.Utility().GetCurrentUserCompanyId();

            if (string.IsNullOrEmpty(companyId)) { return 0; }

            return dal.GetExpirePowder(companyId);
        }

        /// <summary>
        /// 散拼计划行程单,出团通知书
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>行程单实体</returns>
        public MPowderPrint GetModelPrint(string tourId)
        {
            if (!string.IsNullOrEmpty(tourId))
            {
               return dal.GetModelPrint(tourId);
            }
            return null;
        }

        /// <summary>
        /// 获取散拼计划实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>散拼计划实体</returns>
        public MPowderList GetModel(string tourId)
        {
            if (!string.IsNullOrEmpty(tourId))
            {
                return dal.GetModel(tourId);
            }
            return null;
        }
        /// <summary>
        /// 获取线路散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetList(string routeId)
        {
            if (!string.IsNullOrEmpty(routeId))
            {
                return dal.GetList(routeId);
            }
            return null;
        }
        /// <summary>
        /// 获取公司散拼计划
        /// </summary>
        /// <param name="top">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MPowderList> GetList(int top, string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                top = top < 1 ? 7 : top;
                return dal.GetList(top, companyId);
            }
            return null;
        }
        //// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="tourNo">团号(可为空)</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        public IList<MPowderList> GetList(string routeId, string tourNo, DateTime? startDate, DateTime? endDate)
        {
            if (!string.IsNullOrEmpty(routeId))
            {
                return dal.GetList(routeId, tourNo, startDate, endDate);
            }
            return null;
        }
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号(不可以为空)</param>
        /// <param name="tourNo">团号(可以为空)</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        public IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId, string tourNo,
            DateTime? startDate, DateTime? endDate)
        {
            if (!string.IsNullOrEmpty(routeId))
            {
                return dal.GetList(pageSize, pageCurrent, ref recordCount, routeId, tourNo, startDate, endDate);
            }
            return null;
        }
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号</param>
        /// <returns>散拼计划列表</returns>
        public IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId)
        {
            if (!string.IsNullOrEmpty(routeId))
            {
                return dal.GetList(pageSize, pageCurrent, ref recordCount, routeId);
            }
            return null;
        }
        /// <summary>
        /// 分页获取散拼计划列表(用户后台-组团社)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="AreaType">线路类型</param>
        /// <param name="cityId">商家所在城市</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            Model.SystemStructure.AreaType AreaType,int cityId, MPowderSearch search)
        {
            return dal.GetList(pageSize, pageCurrent, ref recordCount, AreaType, cityId, 2, search);
        }
        /// <summary>
        /// 分页获取散拼计划列表(用户后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MPowderSearch search)
        {
            return dal.GetList(pageSize, pageCurrent, ref recordCount, companyId, 1, search);
        }
        /// <summary>
        /// 分页获取散拼计划列表(运营后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, MPowderSearch search)
        {
            return dal.GetList(pageSize, pageCurrent, ref recordCount, 1, search);
        }
        /// <summary>
        /// 专线商历史团队(过了出发时间的散拼团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public IList<MPowderList> GetHistoryList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MPowderSearch search)
        {
            IList<MPowderList> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetHistoryList(pageSize, pageCurrent, ref recordCount, companyId, 1, search);
            }
            return list;
        }

        /// <summary>
        /// 最新散客订单
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeKey">关键字(可以为空)</param>
        /// <param name="areaId">专线(默认0查询全部)</param>
        /// <param name="startDate">开始日期(出团日期)</param>
        /// <param name="endDate">结束日期(出团日期)</param>
        /// <param name="tourId">散拼团队编号(可以为空)</param>
        /// <returns></returns>
        public IList<MPowderOrder> GetNewPowderOrder(int pageSize, int pageCurrent, ref int recordCount, string companyId,
            string routeKey, int areaId, DateTime? startDate, DateTime? endDate,string tourId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetNewPowderOrder(pageSize, pageCurrent, ref recordCount, companyId, routeKey, areaId, startDate, endDate, tourId);
            }
            return null;
        }
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MPowderList> GetCollectionPowder(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MPowderSearch search)
        {
            IList<MPowderList> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetCollectionPowder(pageSize, pageCurrent, ref recordCount, companyId, 3, search);
            }
            return list;
        }

        /// <summary>
        /// 网店4推荐线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="areaId">线路专线</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShop4RecommendList(int topNum, int areaId, string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                topNum = topNum > 0 ? topNum : 10;

                return dal.GetShopList(topNum, areaId, companyId, 4, search);
            }
            return null;
        }
        /// <summary>
        /// 网店4近期线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="areaId">线路专线</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShop4NearList(int topNum, int areaId, string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                topNum = topNum > 0 ? topNum : 10;

                return dal.GetShopList(topNum, areaId, companyId, 5, search);
            }
            return null;
        }

        /// <summary>
        /// 网店4推荐线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShop4RecommendList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetShopList(pageSize, pageCurrent, ref recordCount, 4, companyId, search);
            }
            return null;
        }
        /// <summary>
        /// 网店4线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MShopRoute> GetShop4NearList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetShopList(pageSize, pageCurrent, ref recordCount, 5, companyId, search);
            }
            return null;
        }

        /// <summary>
        /// 获取散拼计划列表
        /// </summary>
        /// <param name="topNum">top数量（小于等于0取所有）</param>
        /// <param name="search">搜索实体</param>
        /// <param name="orderIndex">排序方式 0/1 出团时间降/升序</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int topNum, MPowderSearch search, int orderIndex)
        {
            return dal.GetList(topNum, search, orderIndex);
        }

        #endregion
    }
}
