using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// 
/// 创建人：郑付杰
/// 创建时间：2011-12-19
namespace EyouSoft.Model.NewTourStructure
{
    /// <summary>
    /// 团队计划,团队订单
    /// </summary>
    public class MTourList : MTourBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourList() { }

        /// <summary>
        /// 出发日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 航班出发日期
        /// </summary>
        public string SLeaveDate { get; set; }
        /// <summary>
        /// 航班回程日期
        /// </summary>
        public string ComeBackDate { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public TourStatus TourStatus { get; set; }
        /// <summary>
        /// 团队金额
        /// </summary>
        public decimal TourPrice { get; set; }
        /// <summary>
        /// 团队订单状态
        /// </summary>
        public TourOrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 团队订单来源
        /// </summary>
        public RouteSource OrderSource { get; set; }

        #region 线路信息 
        /// <summary>
        /// 出发城市
        /// </summary>
        public string StartCityName { get; set; }
        /// <summary>
        /// 返回城市
        /// </summary>
        public string EndCityName { get; set; }
        /// <summary>
        /// 出发大交通
        /// </summary>
        public TrafficType StartTraffic { get; set; }
        /// <summary>
        /// 返程大交通
        /// </summary>
        public TrafficType EndTraffic { get; set; }
        /// <summary>
        /// 最少成团人数
        /// </summary>
        public int GroupNum { get; set; }
        /// <summary>
        /// 线路订金 成人(国际游)
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 线路订金 儿童(国际游)
        /// </summary>
        public decimal ChildrenPrice { get; set; }
        /// <summary>
        /// 销售商须知(只有组团社能看到)
        /// </summary>
        public string VendorsNotes { get; set; }
        #endregion

        #region 公司信息
        /// <summary>
        /// 专线商编号
        /// </summary>
        public string Business { get; set; }
        /// <summary>
        /// 专线商名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 专线商地接社QQ
        /// </summary>
        public string BusinessQQ { get; set; }
        /// <summary>
        /// 专线商地接社MQ
        /// </summary>
        public string BusinessMQ { get; set; }
        /// <summary>
        /// 组团社QQ
        /// </summary>
        public string TravelQQ { get; set; }
        /// <summary>
        /// 组团社MQ
        /// </summary>
        public string TravelMQ { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyStructure.CompanyType CompanyType { get; set; }
        #endregion
    }

    /// <summary>
    /// 团队订单,散拼订单共有属性
    /// </summary>
    public abstract class MTourBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourBaseInfo() { }
        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 专线商
        /// </summary>
        public string Publishers { get; set; }
        /// <summary>
        /// 专线商名称
        /// </summary>
        public string PublishersName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 线路名称(b2b)
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 住宿天
        /// </summary>
        public int DayNum { get; set; }
        /// <summary>
        /// 住宿晚
        /// </summary>
        public int LateNum { get; set; }
        /// <summary>
        /// 组团社编号
        /// </summary>
        public string Travel { get; set; }
        /// <summary>
        /// 组团社名称
        /// </summary>
        public string TravelName { get; set; }
        /// <summary>
        /// 组团社联系人
        /// </summary>
        public string TravelContact { get; set; }
        /// <summary>
        /// 组团社联系人电话
        /// </summary>
        public string TravelTel { get; set; }
        /// <summary>
        /// 组团社备注
        /// </summary>
        public string TravelNotes { get; set; }
        /// <summary>
        /// 专线商备注
        /// </summary>
        public string BusinessNotes { get; set; }
        /// <summary>
        /// 游客联系人
        /// </summary>
        public string VisitorContact { get; set; }
        /// <summary>
        /// 游客联系人电话
        /// </summary>
        public string VisitorTel { get; set; }
        /// <summary>
        /// 游客备注
        /// </summary>
        public string VisitorNotes { get; set; }
        /// <summary>
        /// 预定人数
        /// </summary>
        public int ScheduleNum { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int AdultNum { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int ChildrenNum { get; set; }
        /// <summary>
        /// 单房差数量
        /// </summary>
        public int SingleRoomNum { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 下单人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 下单人名称
        /// </summary>
        public string OperatorName { get; set; }
    }

    #region 搜索实体
    /// <summary>
    /// 团队计划,订单搜索实体
    /// </summary>
    public class MTourListSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourListSearch() { }

        /// <summary>
        /// 线路名，组团社/线路名，游客，专线商名称/线路名，组团社，专线商
        /// </summary>
        public string TourKey { get; set; }
        /// <summary>
        /// 出团开始日期
        /// </summary>
        public DateTime? SLeaveDate { get; set; }
        /// <summary>
        /// 出团结束日期
        /// </summary>
        public DateTime? ELeaveDate { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public TourStatus? TourStatus { get; set; }
        /// <summary>
        /// 团队订单状态
        /// </summary>
        public TourOrderStatus? TourOrderStatus { get; set; }
        /// <summary>
        /// 线路区域类型
        /// </summary>
        public SystemStructure.AreaType? AreaType { get; set; }

        /// <summary>
        /// 团队订单状态查询集合
        /// </summary>
        public TourOrderStatus?[] OrderStatus { get; set; }
    }
    #endregion
}
