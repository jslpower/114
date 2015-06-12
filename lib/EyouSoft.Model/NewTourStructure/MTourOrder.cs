using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// 散拼
/// 创建者：郑付杰
/// 创建时间：2011-12-19
namespace EyouSoft.Model.NewTourStructure
{
    using EyouSoft.Model.CompanyStructure;
    using EyouSoft.Model.TicketStructure;

    /// <summary>
    /// 散拼订单
    /// </summary>
    public class MTourOrder:MTourBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourOrder() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 销售成人价
        /// </summary>
        public decimal PersonalPrice { get; set; }
        /// <summary>
        /// 销售儿童价
        /// </summary>
        public decimal ChildPrice { get; set; }
        /// <summary>
        /// 销售单房差
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 结算成人价
        /// </summary>
        public decimal SettlementAudltPrice { get; set; }
        /// <summary>
        /// 结算儿童价
        /// </summary>
        public decimal SettlementChildrenPrice { get; set; }
        /// <summary>
        /// 销售费用(增减)
        /// </summary>
        public decimal Add { get; set; }
        /// <summary>
        /// 结算费用(增减)
        /// </summary>
        public decimal Reduction { get; set; }
        /// <summary>
        /// 留位时间
        /// </summary>
        public DateTime? SaveDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public PowderOrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }
        /// <summary>
        /// 总销售价
        /// </summary>
        public decimal TotalSalePrice { get; set; }
        /// <summary>
        /// 总结算价
        /// </summary>
        public decimal TotalSettlementPrice { get; set; }
        /// <summary>
        /// 特殊要求说明
        /// </summary>
        public string SpecialDes { get; set; }
        /// <summary>
        /// 操作留言
        /// </summary>
        public string OperationMsg { get; set; }
        /// <summary>
        /// 航班出发时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 航班返回时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 余位
        /// </summary>
        public int MoreThan { get; set; }
        /// <summary>
        /// 领队全陪说明
        /// </summary>
        public string TeamLeaderDec { get; set; }
        /// <summary>
        /// 集合说明
        /// </summary>
        public string SetDec { get; set; }
        /// <summary>
        /// 联系人MQ
        /// </summary>
        public string ContactMQ { get; set; }
        /// <summary>
        /// 联系人QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 出发城市名称
        /// </summary>
        public string StartCityName { get; set; }
        /// <summary>
        /// 散拼团队备注
        /// </summary>
        public string TourNotes { get; set; }
        /// <summary>
        /// 订单游客
        /// </summary>
        public IList<MTourOrderCustomer> Customers { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyType CompanyType { get; set; }
    }

    /// <summary>
    /// 散拼订单游客
    /// </summary>
    public class MTourOrderCustomer
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourOrderCustomer() { }

        /// <summary>
        /// 自增编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 游客姓名
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 护照
        /// </summary>
        public string Passport { get; set; }
        /// <summary>
        /// 其他证件类型
        /// </summary>
        public TicketCardType CertificatesType { get; set; }
        /// <summary>
        /// 其他证件
        /// </summary>
        public string OtherCard { get; set; }
        /// <summary>
        /// 性别(男:0 女:1)
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 游客身份类型
        /// </summary>
        public TicketVistorType CradType { get; set; }
        /// <summary>
        /// 座号
        /// </summary>
        public string SiteNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否保存到常旅客
        /// </summary>
        public bool IsSaveToTicketVistorInfo { get; set; }
        /// <summary>
        /// 公司编号（保存入常旅客用）
        /// </summary>
        public string CompanyId { get; set; }
    }

    /// <summary>
    /// 订单日志
    /// </summary>
    public class MOrderHandleLog : MOrderHandleLogSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public MOrderHandleLog() { }

        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogId { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public OrderSource OrderType { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyType CompanyType { get; set; }
    }

    /// <summary>
    /// 订单操作日志搜索实体
    /// </summary>
    public class MOrderHandleLogSearch
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorName { get; set; }
    }

    #region 订单搜索实体
    /// <summary>
    /// 订单搜索实体
    /// </summary>
    public class MTourOrderSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public MTourOrderSearch() { }
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 订单编号,线路名称/线路名称、联系人姓名、团号、专线商名称/线路名称、订单编号、组团社
        /// 订单编号，线路标题，联系人，手机号码，组团社名，专线商名
        /// </summary>
        public string OrderKey { get; set; }
        /// <summary>
        /// 订单区域
        /// </summary>
        public Model.SystemStructure.AreaType? AreaType { get; set; }
        /// <summary>
        /// 专线
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 出团日期
        /// </summary>
        public string LeaveDateS { get; set; }
        public string LeaveDateE { get; set; }
        /// <summary>
        /// 回团日期
        /// </summary>
        public DateTime ComeBackDate { get; set; }
        /// <summary>
        /// 散拼团队计划
        /// </summary>
        public PowderTourStatus? PowderTourStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public IList<PowderOrderStatus> PowderOrderStatus { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public IList<PaymentStatus> PaymentStatus { get; set; }
        /// <summary>
        /// 专线商
        /// </summary>
        public string Publishers { get; set; }
        /// <summary>
        /// 排序规则
        /// 1：按出发时间排序
        /// 2：按下单时间排序
        /// 3：按订单状态排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// True：降序
        /// False：升序
        /// </summary>
        public bool IsDesc { get; set; }
    }

    #endregion

#region 订单统计实体
    /// <summary>
    /// 订单统计实体
    /// </summary>
    public class MOrderStatic
    {
        /// <summary>
        /// 出团时间段最小
        /// </summary>
        public DateTime LeaveDateMin { get; set; }
        /// <summary>
        /// 出团时间段最大
        /// </summary>
        public DateTime LeaveDateMax { get; set; }
        /// <summary>
        /// 专线名
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 专线名
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 订单量
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 成人数
        /// </summary>
        public int TotalAdult { get; set; }
        /// <summary>
        /// 儿童数
        /// </summary>
        public int TotalChild { get; set; }
        /// <summary>
        /// 销售总额
        /// </summary>
        public decimal TotalSale { get; set; }
        /// <summary>
        /// 结算总额
        /// </summary>
        public decimal TotalSettle { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int TotalPeople { get{return this.TotalAdult + this.TotalChild;}}
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }
        /// <summary>
        /// 线路名
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 专线商名
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 组团社名
        /// </summary>
        public string TravelName { get; set; }
    }
#endregion

#region 订单统计搜索实体
    /// <summary>
    /// 订单统计搜索实体
    /// </summary>
    public class MOrderStaticSearch
    {
        /// <summary>
        /// 专线商/组团社
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyType CompanyTyp { get; set; }
        /// <summary>
        /// 是否详细列表
        /// </summary>
        public bool IsDetail { get; set; }
        /// <summary>
        /// 订单区域
        /// </summary>
        public Model.SystemStructure.AreaType? AreaType { get; set; } 
        /// <summary>
        /// 专线编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 出团时间_开始时间
        /// </summary>
        public DateTime? LeaveDateS { get; set; }
        /// <summary>
        /// 出团时间_结束时间
        /// </summary>
        public DateTime? LeaveDateE { get; set; }
    }
#endregion
}
