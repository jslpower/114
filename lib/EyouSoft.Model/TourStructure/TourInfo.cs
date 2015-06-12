using System;
using System.Collections.Generic;

namespace EyouSoft.Model.TourStructure
{
    #region 团队线路发布类型 (enum) ReleaseType
    /// <summary>
    /// 团队线路发布类型
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public enum ReleaseType
    {
        /// <summary>
        /// 标准发布
        /// </summary>
        Standard = 0,
        /// <summary>
        /// 快速发布
        /// </summary>
        Quick
    }
    #endregion.

    #region 团队状态 (enum) TourState
    /// <summary>
    /// 团队状态
    /// </summary>
    /// Author:汪奇志 2001-05-18
    public enum TourState
    {
        /// <summary>
        /// 手动停收
        /// </summary>
        手动停收 = 0,
        /// <summary>
        /// 收客
        /// </summary>
        收客=1,
        /// <summary>
        /// 手动客满
        /// </summary>
        手动客满=2,
        /// <summary>
        /// 自动停收
        /// </summary>
        自动停收=3,
        /// <summary>
        /// 自动客满
        /// </summary>
        自动客满=4
    }
    #endregion

    #region 搜索时团队状态 (enum) SearchTourState
    /// <summary>
    /// 团队状态
    /// </summary>
    /// Author:汪奇志 2001-05-18
    public enum SearchTourState
    {
        /// <summary>
        /// 全部
        /// </summary>
        全部 = -1,
        /// <summary>
        /// 停收(手动、自动)
        /// </summary>
        停收,
        /// <summary>
        /// 收客
        /// </summary>
        收客,
        /// <summary>
        /// 客满(手动、自动)
        /// </summary>
        客满
    }
    #endregion

    #region 团队类型 (enum) TourType
    /// <summary>
    /// 团队类型
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public enum TourType
    {
        /// <summary>
        /// 组团团队
        /// </summary>
        组团团队 = 1,
        /// <summary>
        /// 组团散拼
        /// </summary>
        组团散拼
    }
    #endregion
    
    #region 团队推广状态 (enum) TourSpreadState
    /// <summary>
    /// 团队推广状态
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public enum TourSpreadState
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 推荐精品
        /// </summary>
        推荐精品,
        /// <summary>
        /// 促销
        /// </summary>
        促销,
        /// <summary>
        /// 最新
        /// </summary>
        最新,
        /// <summary>
        /// 品质
        /// </summary>
        品质,
        /// <summary>
        /// 纯玩
        /// </summary>
        纯玩
    }
    #endregion    

    #region 团队展示类型 (enum) TourDisplayType
    /// <summary>
    /// 团队展示类型
    /// </summary>
    /// Author:2010-06-01 汪奇志
    public enum TourDisplayType
    {
        /// <summary>
        /// 按线路产品展示
        /// </summary>
        线路产品=0,
        /// <summary>
        /// 按出团日期展示
        /// </summary>
        出团日期
    }
    #endregion

    #region 查看团队类型 (enum) VisitTourType
    /// <summary>
    /// 查看团队类型
    /// </summary>
    public enum VisitTourType
    {
        /// <summary>
        /// 查看
        /// </summary>
        查看=0,
        /// <summary>
        /// 预订
        /// </summary>
        预订
    }
    #endregion

    #region /**/
    /*
    #region 获取团队时的身份类型 (enum) GetTourIdentityType
    /// <summary>
    /// 获取团队时的身份类型
    /// </summary>
    /// Author:汪奇志 2010-06-01
    public enum GetTourIdentityType
    {
        /// <summary>
        /// 批发商含经营单位
        /// </summary>
        批发商含经营单位=0,
        /// <summary>
        /// 批发商不含经营单位
        /// </summary>
        批发商不含经营单位,
        /// <summary>
        /// 经营单位
        /// </summary>
        经营单位
    }
    #endregion*/
    #endregion

    #region 团队基本信息业务实体 (class) TourBasicInfo
    /// <summary>
    /// 团队信息抽象类
    /// </summary>
    [Serializable]
    public abstract class TourObject : RouteObject
    {
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime ComeBackDate { get; set; }
    }
    /// <summary>
    /// 团队基本信息业务实体
    /// </summary>
    /// Author:汪奇志 2001-05-18
    [Serializable]
    public class TourBasicInfo : TourObject
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourBasicInfo() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /*
        /// <summary>
        /// 经营单位编号
        /// </summary>
        public string UnitCompanyId { get; set; }*/
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }       
        /// <summary>
        /// 团队状态
        /// </summary>
        public TourState TourState { get; set; }
        /// <summary>
        /// 团队类型
        /// </summary>
        public TourType TourType { get; set; }
        /// <summary>
        /// 团队推广状态
        /// </summary>
        public TourSpreadState TourSpreadState { get; set; }
        /// <summary>
        /// 团队推广状态[推荐、促销、最新、品质、纯玩 etc.]名称
        /// </summary>
        public string TourSpreadStateName { get; set; }
        /// <summary>
        /// 团队推广说明
        /// </summary>
        public string TourSpreadDescription { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 模板团编号
        /// </summary>
        public string ParentTourID { get; set; }
        /// <summary>
        /// 计划总人数
        /// </summary>
        public int PlanPeopleCount { get; set; }
        /// <summary>
        /// 虚拟剩余人数
        /// </summary>
        public int RemnantNumber { get; set; }
        /// <summary>
        /// 门市成人价
        /// </summary>
        public decimal RetailAdultPrice { get; set; }
        /// <summary>
        /// 门市儿童价
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }
        /// <summary>
        /// 单房差结算价
        /// </summary>
        public decimal RoomDiffSettlementPrice { get; set; }
        /// <summary>
        /// 同行成人价
        /// </summary>
        public decimal TravelAdultPrice { get; set; }
        /// <summary>
        /// 同行儿童价
        /// </summary>
        public decimal TravelChildrenPrice { get; set; }
        /// <summary>
        /// 单房差门市价
        /// </summary>
        public decimal RoomDiffRetailPrice { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Clicks { get; set; }
        /// <summary>
        /// 团队发布类型
        /// </summary>
        public ReleaseType ReleaseType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最近出团数
        /// </summary>
        public int RecentLeaveCount { get; set; }
        /// <summary>
        /// 已占座位
        /// </summary>
        public string OccupySeat { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 团队负责人
        /// </summary>
        public string TourContact { get; set; }
        /// <summary>
        /// 团队联系人电话
        /// </summary>
        public string TourContactTel { get; set; }
        /// <summary>
        /// 团队负责人用户名
        /// </summary>
        public string TourContacUserName { get; set; }
        /// <summary>
        /// 团队负责人MQ
        /// </summary>
        public string TourContacMQ { get; set; }
        /// <summary>
        /// 行程是否异常(百分制)
        /// </summary>
        public int StandardPlanError { get; set; }
        /// <summary>
        /// 价格是否异常(百分制)
        /// </summary>
        public int TourPriceError { get; set; }
        /// <summary>
        /// 实收成人数
        /// </summary>
        public int CollectAdultNumber { get; set; }
        /// <summary>
        /// 实收儿童数
        /// </summary>
        public int CollectChildrenNumber { get; set; }
        /// <summary>
        /// 预留成人数
        /// </summary>
        public int AllowanceAdultNumber { get; set; }
        /// <summary>
        /// 预留儿童数
        /// </summary>
        public int AllowanceChildrenNumber { get; set; }
        /// <summary>
        /// 未处理成人数
        /// </summary>
        public int UntreatedAdultNumber { get; set; }
        /// <summary>
        /// 未处理儿童数
        /// </summary>
        public int UntreatedChildrenNumber { get; set; }
        /// <summary>
        /// 留位过期成人数
        /// </summary>
        public int OverdueAdultNumber { get; set; }
        /// <summary>
        /// 留位过期儿童数
        /// </summary>
        public int OverdueChildrenNumber { get; set; }
        /// <summary>
        /// 不受理成人数
        /// </summary>
        public int DismissAdultNumber { get; set; }
        /// <summary>
        /// 不受理儿童数
        /// </summary>
        public int DismissChildrenNumber { get; set; }
        /// <summary>
        /// 所有订单已采购座位数量(实际订单人数)
        /// </summary>
        public int BuySeatNumber { get; set; }
        /// <summary>
        /// 真实剩余人数
        /// </summary>
        public int RealRemnantNumber { get { return this.PlanPeopleCount - this.BuySeatNumber; } }

        #region 新版资讯附加属性
        /// <summary>
        /// 出港城市编号
        /// </summary>
        public int LeaveCityId { get; set; }
        /// <summary>
        /// 出港城市名称
        /// </summary>
        public string LeaveCityName { get; set; }
        #endregion
    }
    #endregion


    #region 团队信息业务实体 (class) TourInfo
    /// <summary>
    /// 团队信息业务实体
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourInfo:TourBasicInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourInfo() { }
        
        /// <summary>
        /// 交通安排
        /// </summary>
        public string LeaveTraffic { get; set; }
        /*/// <summary>
        /// 返程交通
        /// </summary>
        public string BackTraffic { get; set; }*/
        /// <summary>
        /// 送团人
        /// </summary>
        public string SendContactName { get; set; }
        /// <summary>
        /// 送团电话
        /// </summary>
        public string SendContactTel { get; set; }
        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string UrgentContactName { get; set; }
        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string UrgentContactTel { get; set; }        
        /*/// <summary>
        /// 出港城市集合
        /// </summary>
        public IList<int> LeaveCity { get; set; }*/
        /// <summary>
        /// 出港城市
        /// </summary>
        public int LeaveCity { get; set; }
        /// <summary>
        /// 销售城市集合
        /// </summary>
        public IList<int> SaleCity { get; set; }
        /// <summary>
        /// 线路主题集合
        /// </summary>
        public IList<int> RouteTheme { get; set; }
        /// <summary>
        /// 停收天数
        /// </summary>
        public int AutoOffDays { get; set; }        
        /// <summary>
        /// 接团方式
        /// </summary>
        public string MeetTourContect { get; set; }
        /// <summary>
        /// 集合方式
        /// </summary>
        public string CollectionContect { get; set; }        
        /// <summary>
        /// 所属公司是否审核
        /// </summary>
        public bool IsCompanyCheck { get; set; }
        /// <summary>
        /// 快速发布团队的行程内容
        /// </summary>
        public string QuickPlan { get; set; }        
        /// <summary>
        /// 团队报价明细
        /// </summary>
        public IList<TourPriceDetail> TourPriceDetail { get; set; }
        /// <summary>
        /// 标准发布的行程内容
        /// </summary>
        public IList<TourStandardPlan> StandardPlan { get; set; }
        /// <summary>
        /// 标准发布的团队服务标准信息
        /// </summary>
        public TourServiceStandard ServiceStandard { get; set; }
        /// <summary>
        /// 子团信息集合
        /// </summary>
        public IList<ChildrenTourInfo> Childrens { get; set; }
        /// <summary>
        /// 团队地接社信息集合
        /// </summary>
        public IList<TourLocalityInfo> LocalTravelAgency { get; set; }
    }
    #endregion 
    
    #region 自动团号信息业务实体 (class) AutoTourCodeInfo
    /// <summary>
    /// 自动团号信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public class AutoTourCodeInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AutoTourCodeInfo() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="tourCode">自动团号</param>
        /// <param name="leaveDate">出团日期</param>
        public AutoTourCodeInfo(string tourCode, DateTime leaveDate)
        {
            this.TourCode = tourCode;
            this.LeaveDate = leaveDate;
        }

        /// <summary>
        /// 自动团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveDate { get; set; }
    }
    #endregion

    #region 模板团的子团信息业务实体 (class) ChildrenTourInfo
    /// <summary>
    /// 模板团的子团信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public class ChildrenTourInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ChildrenTourInfo() { }

        /*
        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="childrenId">子团编号</param>
        /// <param name="tourCode">团号</param>
        /// <param name="leaveDate">出团日期</param>
        /// <param name="tourState">团队状态</param>
        /// <param name="travelAdultPrice">同行成人价</param>
        /// <param name="travelChildrenPrice">同行儿童价</param>
        /// <param name="retailAdultPrice">门市成人价</param>
        /// <param name="retailChildrenPrice">门市儿童价</param>
        /// <param name="roomDiffSettlementPrice">单房差结算价</param>
        /// <param name="roomDiffRetailPrice">单房差门市价</param>
        public ChildrenTourInfo(string childrenId, string tourCode, DateTime leaveDate,TourState tourState
            , decimal travelAdultPrice, decimal travelChildrenPrice, decimal retailAdultPrice, decimal retailChildrenPrice
            , decimal roomDiffSettlementPrice, decimal roomDiffRetailPrice, int remnantNumber)
        {
            this.ChildrenId = childrenId;
            this.TourCode = tourCode;
            this.LeaveDate = leaveDate;
            this.TourState = tourState;
            this.TravelAdultPrice = travelAdultPrice;
            this.TravelChildrenPrice = travelChildrenPrice;
            this.RetailAdultPrice = retailAdultPrice;
            this.RetailChildrenPrice = retailChildrenPrice;
            this.RoomDiffSettlementPrice = roomDiffSettlementPrice;
            this.RoomDiffRetailPrice = roomDiffRetailPrice;
            this.RemnantNumber = remnantNumber;

        }*/

        /// <summary>
        /// 子团编号
        /// </summary>
        public string ChildrenId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveDate { get; set; }
        /// <summary>
        /// 团队状态
        /// </summary>
        public TourState TourState { get; set; }              
        /// <summary>
        /// 同行成人价
        /// </summary>
        public decimal TravelAdultPrice { get; set; }
        /// <summary>
        /// 同行儿童价
        /// </summary>
        public decimal TravelChildrenPrice { get; set; }
        /// <summary>
        /// 门市成人价
        /// </summary>
        public decimal RetailAdultPrice { get; set; }
        /// <summary>
        /// 门市儿童价
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }  
        /// <summary>
        /// 单房差结算价
        /// </summary>
        public decimal RoomDiffSettlementPrice { get; set; }
        /// <summary>
        /// 单房差门市价
        /// </summary>
        public decimal RoomDiffRetailPrice { get; set; }
        /// <summary>
        /// 虚拟剩余人数
        /// </summary>
        public int RemnantNumber { get; set; }
        /// <summary>
        /// 计划人数
        /// </summary>
        public int PlanPeopleCount { get; set; }
        /// <summary>
        /// 已采购座位数量(实际订单人数)
        /// </summary>
        public int BuySeatNumber { get; set; }
        /// <summary>
        /// 真实剩余人数
        /// </summary>
        public int RealRemnantNumber { get { return this.PlanPeopleCount - this.BuySeatNumber; } }
    }
    #endregion

    #region 按线路区域分组模板团信息业务实体 (class) TemplateTourInfo
    /// <summary>
    /// 按线路区域分组模板团信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public class TemplateTourInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TemplateTourInfo() { }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 模板团数量
        /// </summary>
        public int TourNumber { get; set; }
        /// <summary>
        /// 模板团信息集合
        /// </summary>
        public IList<TourBasicInfo> TemplateTour { get; set; }
    }
    #endregion

    #region 团队浏览记录信息业务实体 (class) TourVisitInfo
    /// <summary>
    /// 团队浏览记录信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-24
    public class TourVisitInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourVisitInfo() { }

        /// <summary>
        /// 浏览记录编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 浏览的团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 发布团队的公司编号
        /// </summary>
        public string VisitedCompanyId { get; set; }
        /// <summary>
        /// 被访问公司名称(发布团队的公司名称)
        /// </summary>
        public string VisitedCompanyName { get; set; }
        /// <summary>
        /// 浏览者IP
        /// </summary>
        public string ClientIP { get; set; }
        /// <summary>
        /// 浏览人用户编号
        /// </summary>
        public string ClientUserId { get; set; }
        /// <summary>
        /// 浏览人姓名
        /// </summary>
        public string ClientUserContactName { get; set; }
        /// <summary>
        /// 浏览人公司名称
        /// </summary>
        public string ClientCompanyName { get; set; }
        /// <summary>
        /// 浏览人公司编号
        /// </summary>
        public string ClientCompanyId { get; set; }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime VisitedTime { get; set; }
        /// <summary>
        /// 访问的团的团号
        /// </summary>
        public string VisitTourCode { get; set; }
        /// <summary>
        /// 访问的团的线路名称
        /// </summary>
        public string VisitTourRouteName { get; set; }
        /// <summary>
        /// 访问者联系电话
        /// </summary>
        public string ClientUserContactTelephone { get; set; }
        /// <summary>
        /// 访问者联系手机
        /// </summary>
        public string ClientUserContactMobile { get; set; }
        /// <summary>
        /// 访问者联系QQ
        /// </summary>
        public string ClinetUserContactQQ { get; set; }
        /// <summary>
        /// 查看团队类型
        /// </summary>
        public VisitTourType VisitTourType { get; set; }

    }
    #endregion

    #region 按线路区域统计(团队、批发商等)信息业务实体 (class) AreaStatInfo
    /// <summary>
    /// 按线路区域统计(团队、线路、批发商等)信息业务实体
    /// </summary>
    public class AreaStatInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AreaStatInfo() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="areaid">线路区域编号</param>
        /// <param name="areaName">线路区域名称</param>
        /// <param name="Number">数量</param>
        public AreaStatInfo(int areaid, string areaName, int Number)
        {
            this.AreaId = areaid;
            this.AreaName = areaName;
            this.Number = Number;
        }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
    #endregion

    #region 拥有订单的团队信息业务实体 (class) HavingOrderTourInfo
    /// <summary>
    /// 拥有订单的团队信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-28
    public class HavingOrderTourInfo:TourBasicInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public HavingOrderTourInfo() { }
        
        /// <summary>
        /// 已采购零售商数量
        /// </summary>
        public int BuyCompanyNumber { get; set; }
    }
    #endregion

    #region 按线路区域类型统计(团队等)信息业务实体 (class) AreaTypeStatInfo
    /// <summary>
    /// 按线路区域类型统计(团队等)信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-06-01
    public class AreaTypeStatInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AreaTypeStatInfo() { }

        /// <summary>
        /// 全部
        /// </summary>
        public int All
        {
            get { return this.Long + this.Short + this.Exit; }
        }
        /// <summary>
        /// 国内长线
        /// </summary>
        public int Long { get; set; }
        /// <summary>
        /// 国内短线
        /// </summary>
        public int Short { get; set; }
        /// <summary>
        /// 国际线
        /// </summary>
        public int Exit { get; set; }
    }
    #endregion

    #region 团队按订单统计信息业务实体 (class) TourStatByOrderInfo
    /// <summary>
    /// 团队按订单统计信息业务实体
    /// </summary>
    /// Author:汪奇志 201-06-03
    public class TourStatByOrderInfo:TourBasicInfo
    {
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNumber { get; set; }
        /// <summary>
        /// 总收入
        /// </summary>
        public decimal TotalIncome { get; set; }
        /// <summary>
        /// 成交订单数
        /// </summary>
        public int OrdainNum { get; set; }
        /// <summary>
        /// 留位订单数
        /// </summary>
        public int SaveSeatNum { get; set; }
        /// <summary>
        /// 留位过期订单数
        /// </summary>
        public int SaveSeatExpiredNum { get; set; }
        /// <summary>
        /// 不受理订单数
        /// </summary>
        public int NotAcceptedNum { get; set; }
    }
    #endregion

    #region 访问历史记录统计信息业务实体 (class) VisitedHistoryStatInfo
    /// <summary>
    /// 访问历史记录统计信息业务实体
    /// </summary>
    public class VisitedHistoryStatInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public VisitedHistoryStatInfo() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="visitedCompanyId">被访问的公司编号</param>
        /// <param name="visitedCompanyName">被访问的公司名称</param>
        /// <param name="visitedTime">访问次数</param>
        public VisitedHistoryStatInfo(string visitedCompanyId, string visitedCompanyName, int visitedTime)
        {
            this.VisitedCompanyId = visitedCompanyId;
            this.VisitedCompanyName = visitedCompanyName;
            this.VisitedTime = visitedTime;
        }

        /// <summary>
        /// 被访问的公司编号
        /// </summary>
        public string VisitedCompanyId { get; set; }
        /// <summary>
        /// 被访问的公司名称
        /// </summary>
        public string VisitedCompanyName { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int VisitedTime { get; set; }
    }
    #endregion

    #region 团队搜索类型 (enum) TourSearchType
    /// <summary>
    /// 团队搜索类型
    /// </summary>
    /// Author:汪奇志 2010-06-24
    public enum TourSearchType
    {
        /// <summary>
        /// 无
        /// </summary>
        None=0,
        /// <summary>
        /// 按主题
        /// </summary>
        Theme,
        /// <summary>
        /// 按价格区间
        /// </summary>
        Price,
        /// <summary>
        /// 按团队天数
        /// </summary>
        TourDay,
        /// <summary>
        /// 按公司
        /// </summary>
        Company,
        /// <summary>
        /// 其它
        /// </summary>
        Other
    }
    #endregion

    #region 团队搜索条件业务实体 (class) TourSearchInfo
    /// <summary>
    /// 团队搜索条件业务实体
    /// </summary>
    /// Author:汪奇志 201-06-24
    public class TourSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourSearchInfo() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="cityId">城市编号</param>
        public TourSearchInfo(int cityId)
        {
            this.CityId = cityId;
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int? AreaId { get; set; }
        /// <summary>
        /// 线路主题编号
        /// </summary>
        public int ThemeId { get; set; }
        /// <summary>
        /// 团队天数搜索类型 小于0:≤指定天数  等于0：=指定天数 大于0:≥指定天数
        /// </summary>
        public int TourDaysType { get; set; }
        /// <summary>
        /// 价格区间起始值 null→-∞
        /// </summary>
        public int? MinPrice { get; set; }
        /// <summary>
        /// 价格区间截止值 null→+∞
        /// </summary>
        public int? MaxPrice { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 出团起始时间
        /// </summary>
        public DateTime? StartLeaveDate { get; set; }
        /// <summary>
        /// 出团截止时间
        /// </summary>
        public DateTime? FinishLeaveDate { get; set; }
        /// <summary>
        /// 团队天数
        /// </summary>
        public int? TourDays { get; set; }
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; }
        /// <summary>
        /// 是否都是已经出团的团队(模板团下所有团队均已出团)
        /// </summary>
        public bool IsAllHistory { get; set; }
    }
    #endregion

    #region MQ订单提醒的团队信息业务实体 (class) MQRemindHavingOrderTourInfo
    /// <summary>
    /// MQ订单提醒的团队信息业务实体
    /// </summary>
    public class MQRemindHavingOrderTourInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MQRemindHavingOrderTourInfo() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public DateTime LeaveDate { get; set; }

        /// <summary>
        /// 采购零售商数量
        /// </summary>
        public int BuyCompanyNumber { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNumber { get; set; }
        /// <summary>
        /// 采购坐位数量
        /// </summary>
        public int BuySeatNumber { get; set; }
        /*/// <summary>
        /// 计划人数
        /// </summary>
        public int PlanPeopleCount { get; set; }
        /// <summary>
        /// 订单实际人数
        /// </summary>
        public int OrderPeopleNumber { get; set; }
        /// <summary>
        /// 实际剩余人数
        /// </summary>
        public int RealRemnantNumber { get { return this.PlanPeopleCount - this.OrderPeopleNumber; } }*/
        /// <summary>
        /// 实际剩余人数
        /// </summary>
        public int RealRemnantNumber { get; set; }
    }
    #endregion
}
