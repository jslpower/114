using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// 散拼
/// 创建人：郑付杰
/// 创建时间：2011-12-19
namespace EyouSoft.Model.NewTourStructure
{
    #region 散拼计划
    /// <summary>
    /// 散拼信息
    /// </summary>
    public class MPowderList : MBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public MPowderList() { }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime ComeBackDate { get; set; }
        /// <summary>
        /// 航班出发时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 航班返回时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 报名截止日期
        /// </summary>
        public DateTime RegistrationEndDate { get; set; }
        /// <summary>
        /// 团队人数
        /// </summary>
        public int TourNum { get; set; }
        /// <summary>
        /// 留位
        /// </summary>
        public int SaveNum { get; set; }
        /// <summary>
        /// 余位
        /// </summary>
        public int MoreThan { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public PowderTourStatus PowderTourStatus { get; set; }
        /// <summary>
        /// 成人市场价
        /// </summary>
        public decimal RetailAdultPrice { get; set; }
        /// <summary>
        /// 成人结算价
        /// </summary>
        public decimal SettlementAudltPrice { get; set; }
        /// <summary>
        /// 儿童市场价
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }
        /// <summary>
        /// 儿童结算价
        /// </summary>
        public decimal SettlementChildrenPrice { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 集合说明
        /// </summary>
        public string SetDec { get; set; }
        /// <summary>
        /// 领队全陪说明
        /// </summary>
        public string TeamLeaderDec { get; set; }
        /// <summary>
        /// 团队备注
        /// </summary>
        public string TourNotes { get; set; }
        /// <summary>
        /// 是否限制报名人数
        /// </summary>
        public bool IsLimit { get; set; }
        /// <summary>
        /// 简易版行程
        /// </summary>
        public string StandardStroke { get; set; }
        /// <summary>
        /// 散客版报价
        /// </summary>
        public string FitQuotation { get; set; }
        /// <summary>
        /// 发布IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 订单人数
        /// </summary>
        public int OrderPeopleNum { get; set; }
    }
    #endregion

    #region 散拼行程单
    /// <summary>
    /// 散拼行程单
    /// </summary>
    public class MPowderPrint : MRoutePrint
    {
        /// <summary>
        /// 
        /// </summary>
        public MPowderPrint() { }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 单房差
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 集合说明
        /// </summary>
        public string SetDec { get; set; }
        /// <summary>
        /// 领队全陪说明
        /// </summary>
        public string TeamLeaderDec { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 返团时间
        /// </summary>
        public DateTime ComeBackDate { get; set; }
        /// <summary>
        /// 航班出发时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 航班返回时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 报名截止日期
        /// </summary>
        public DateTime RegistrationEndDate { get; set; }
    }
    #endregion

    #region 最新散客订单

    /// <summary>
    /// 最新散客订单
    /// </summary>
    public class MPowderOrder
    {
        /// <summary>
        /// 
        /// </summary>
        public MPowderOrder() { }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNO { get; set; }
        /// <summary>
        /// 空位
        /// </summary>
        public int MoreThan { get; set; }
        /// <summary>
        /// 几家零售商
        /// </summary>
        public int Retailer { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 成人
        /// </summary>
        public int Audit { get; set; }
        /// <summary>
        /// 儿童
        /// </summary>
        public int Children { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public IList<MTourOrder> Orders { get; set; }
    }

    #endregion

    #region 搜索实体
    /// <summary>
    /// 搜索实体
    /// </summary>
    public class MPowderSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public MPowderSearch() { }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 团号,线路名/团队编号,团队线路名,线路特色/线路名称,线路特色,专线商名
        /// </summary>
        public string TourKey { get; set; }
        /// <summary>
        /// 专线编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 出团时间开始
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 出团时间结束
        /// </summary>
        public DateTime EndLeaveDate { get; set; }
        /// <summary>
        /// 专线商编号
        /// </summary>
        public string Publishers { get; set; }
        /// <summary>
        /// 出发城市编号
        /// </summary>
        public int StartCityId { get; set; }
        /// <summary>
        /// 出发城市名称
        /// </summary>
        public string StartCityName { get; set; }
        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendType? RecommendType { get; set; }
        /// <summary>
        /// 线路类型
        /// </summary>
        public SystemStructure.AreaType? AreaType { get; set; }
        /// <summary>
        /// 主题编号
        /// </summary>
        public int ThemeId { get; set; }
        /// <summary>
        /// 起始价格
        /// </summary>
        public decimal StartPrice { get; set; }
        /// <summary>
        /// 结束价格
        /// </summary>
        public decimal EndPrice { get; set; }
        /// <summary>
        /// 出游天数
        /// </summary>
        public PowderDay? PowderDay { get; set; }

    }
    #endregion
}
