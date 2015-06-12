using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IBLL.NewTourStructure
{
    /// <summary>
    /// 散拼计划接口
    /// 创建者：郑付杰
    /// 创建时间：2011-12-20
    /// </summary>
    public interface IPowderList
    {
        #region 增,删,改
        /// <summary>
        /// 添加散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        bool AddPowder(MPowderList item);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="leaveDates">出团日期</param>
        /// <param name="registrationDate">报名截止日期</param>
        /// <returns></returns>
        bool AddBatchPowder(MPowderList item, DateTime[] leaveDates, DateTime[] registrationDate);/// <summary>
        /// 批量修改行程
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        bool UpdateStandardPlan(MPowderList item, params string[] tourId);
        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        bool UpdatePowder(MPowderList item);
        /// <summary>
        /// 删除散拼计划
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        bool DeletePowder(params string[] tourId);
        /// <summary>
        /// 批量修改散拼团状态
        /// </summary>
        /// <param name="status">散拼团状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        bool UpdateStatus(PowderTourStatus status, params string[] tourId);
        /// <summary>
        /// 批量修改散拼团推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        bool UpdatePowderRecommend(RecommendType type, params string[] tourId);
        /// <summary>
        /// 批量修改散拼计划
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool UpdatePowerList(IList<MPowderList> list);
        #endregion

        #region 查询
        /// <summary>
        /// 到期时期还有1周
        /// </summary>
        /// <returns></returns>
        int GetExpirePowder();
        /// <summary>
        /// 线路区域对应的散拼计划数量
        /// </summary>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetCurrentUserTourByAreaStats();
        /// <summary>
        /// 散拼计划行程单,出团通知书
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>行程单实体</returns>
        MPowderPrint GetModelPrint(string tourId);
        /// <summary>
        /// 获取散拼计划实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>散拼计划实体</returns>
        MPowderList GetModel(string tourId);
        /// <summary>
        /// 获取线路散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        IList<MPowderList> GetList(string routeId);

        /// <summary>
        /// 获取公司散拼计划
        /// </summary>
        /// <param name="top">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MPowderList> GetList(int top, string companyId);
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="tourNo">团号(可为空)</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        IList<MPowderList> GetList(string routeId, string tourNo, DateTime? startDate, DateTime? endDate);
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号</param>
        /// <param name="tourNo">团号</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId, string tourNo,
            DateTime? startDate, DateTime? endDate);
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号</param>
        /// <returns>散拼计划列表</returns>
        IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId);
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
        IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            Model.SystemStructure.AreaType AreaType,int cityId, MPowderSearch search);
        /// <summary>
        /// 分页获取散拼计划列表(用户后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MPowderSearch search);
        /// <summary>
        /// 分页获取散拼计划列表(运营后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,MPowderSearch search);
        /// <summary>
        /// 专线商历史团队(过了出发时间的散拼团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        IList<MPowderList> GetHistoryList(int pageSize, int pageCurrent, ref int recordCount,string companyId, MPowderSearch search);
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
        IList<MPowderOrder> GetNewPowderOrder(int pageSize, int pageCurrent, ref int recordCount, string companyId,
            string routeKey, int areaId, DateTime? startDate, DateTime? endDate, string tourId);
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MPowderList> GetCollectionPowder(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MPowderSearch search);
        /// <summary>
        /// 网店4推荐线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="areaId">线路专线</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShop4RecommendList(int topNum, int areaId, string companyId, MRouteSearch search);
        /// <summary>
        /// 网店4近期线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="areaId">线路专线</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShop4NearList(int topNum, int areaId, string companyId, MRouteSearch search);
        /// <summary>
        /// 网店4推荐线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShop4RecommendList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search);
        /// <summary>
        /// 网店4线路
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MShopRoute> GetShop4NearList(int pageSize, int pageCurrent, ref int recordCount,
             string companyId, MRouteSearch search);

        /// <summary>
        /// 获取散拼计划列表
        /// </summary>
        /// <param name="topNum">top数量（小于等于0取所有）</param>
        /// <param name="search">搜索实体</param>
        /// <param name="orderIndex">排序方式 0/1 出团时间降/升序</param>
        /// <returns>散拼计划列表</returns>
        IList<MPowderList> GetList(int topNum, MPowderSearch search, int orderIndex);

        #endregion
    }
}
