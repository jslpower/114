//Author:汪奇志 2010-10-21

using System;
using System.Collections.Generic;

namespace EyouSoft.Model.TicketStructure
{
    #region  机票系统旅客类型
    /// <summary>
    /// 机票系统旅客类型
    /// </summary>
    public enum TravellerType
    {
        /// <summary>
        /// 内宾
        /// </summary>
        内宾 = 0,
        /// <summary>
        /// 外宾
        /// </summary>
        外宾
    }
    #endregion

    #region 机票系统订单状态
    /// <summary>
    /// 机票系统订单状态
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// 等待审核
        /// </summary>
        等待审核 = 0,
        /// <summary>
        /// 拒绝审核
        /// </summary>
        拒绝审核,
        /// <summary>
        /// 审核通过
        /// </summary>
        审核通过,
        /// <summary>
        /// 支付成功
        /// </summary>
        支付成功,
        /// <summary>
        /// 拒绝出票
        /// </summary>
        拒绝出票,
        /// <summary>
        /// 出票完成
        /// </summary>
        出票完成,
        /// <summary>
        /// 无效订单
        /// </summary>
        无效订单
    }
    #endregion

    #region 机票系统旅客状态
    /// <summary>
    /// 机票系统旅客状态
    /// </summary>
    public enum TravellerState
    {
        /// <summary>
        /// 正常
        /// </summary>
        正常 = 0,
        /// <summary>
        /// 申请退票
        /// </summary>
        申请退票,
        /// <summary>
        /// 拒绝退票
        /// </summary>
        拒绝退票,
        /// <summary>
        /// 退票成功
        /// </summary>
        退票成功,
        /// <summary>
        /// 申请作废
        /// </summary>
        申请作废,
        /// <summary>
        /// 拒绝作废
        /// </summary>
        拒绝作废,
        /// <summary>
        /// 作废成功
        /// </summary>
        作废成功,
        /// <summary>
        /// 申请改期
        /// </summary>
        申请改期,
        /// <summary>
        /// 拒绝改期
        /// </summary>
        拒绝改期,
        /// <summary>
        /// 改期成功
        /// </summary>
        改期成功,
        /// <summary>
        /// 申请改签
        /// </summary>
        申请改签,
        /// <summary>
        /// 拒绝改签
        /// </summary>
        拒绝改签,
        /// <summary>
        /// 改签成功
        /// </summary>
        改签成功
    }
    #endregion

    #region 机票系统订单变更类型
    /// <summary>
    /// 机票系统订单变更类型
    /// </summary>
    public enum OrderChangeType
    {
        /// <summary>
        /// 退票
        /// </summary>
        退票 = 0,
        /// <summary>
        /// 作废
        /// </summary>
        作废,
        /// <summary>
        /// 改期
        /// </summary>
        改期,
        /// <summary>
        /// 改签
        /// </summary>
        改签
    }
    #endregion

    #region 机票系统订单变更状态
    /// <summary>
    /// 机票系统订单变更状态
    /// </summary>
    public enum OrderChangeState
    {
        /// <summary>
        /// 申请变更
        /// </summary>
        申请 = 0,
        /// <summary>
        /// 拒绝变更
        /// </summary>
        拒绝,
        /// <summary>
        /// 接受变更
        /// </summary>
        接受
    }
    #endregion

    #region 退票、作废类型
    /// <summary>
    /// 退票、作废类型
    /// </summary>
    public enum RefundTicketType
    {
        /// <summary>
        /// 自愿退票
        /// </summary>
        自愿退票 = 1,
        /// <summary>
        /// 航班延误申请全退
        /// </summary>
        航班延误申请全退,
        /// <summary>
        /// 航班取消申请全退
        /// </summary>
        航班取消申请全退,
        /// <summary>
        /// 航班提前申请全退
        /// </summary>
        航班提前申请全退,
        /// <summary>
        /// 备降 
        /// </summary>
        备降,
        /// <summary>
        /// 降舱
        /// </summary>
        降舱,
        /// <summary>
        /// 病退
        /// </summary>
        病退,
        /// <summary>
        /// 升舱换开申请全退
        /// </summary>
        升舱换开申请全退,
        /// <summary>
        /// 当日作废
        /// </summary>
        当日作废
    }
    #endregion

    #region 订单统计类型
    /// <summary>
    /// 订单统计类型
    /// </summary>
    public enum OrderStatType
    {
        /// <summary>
        /// 待审核(张)
        /// </summary>
        待审核 = 0,
        /// <summary>
        /// 待处理(张)
        /// </summary>
        待处理,
        /// <summary>
        /// 待退票(张)
        /// </summary>
        待退票,
        /// <summary>
        /// 自愿(天)
        /// </summary>
        自愿,
        /// <summary>
        /// 待作废(张)
        /// </summary>
        待作废,
        /// <summary>
        /// 待改期(张)
        /// </summary>
        待改期,
        /// <summary>
        /// 待改签(张)
        /// </summary>
        待改签,
        /// <summary>
        /// 非自愿(天)
        /// </summary>
        非自愿
    }
    #endregion

    #region 机票系统订单信息业务实体
    /// <summary>
    /// 机票系统订单信息业务实体
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderInfo() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public RateType RateType { get; set; }
        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime LeaveTime { get; set; }
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ReturnTime { get; set; }
        /// <summary>
        /// 旅客类型
        /// </summary>
        public TravellerType TravellerType { get; set; }
        /// <summary>
        /// 供应商公司编号
        /// </summary>
        public string SupplierCId { get; set; }
        /// <summary>
        /// 供应商公司名称
        /// </summary>
        public string SupplierCName { get; set; }
        /// <summary>
        /// 供应商用户名
        /// </summary>
        public string SupplierUName { get; set; }
        /// <summary>
        /// 采购商公司编号
        /// </summary>
        public string BuyerCId { get; set; }
        /// <summary>
        /// 采购商公司名称
        /// </summary>
        public string BuyerCName { get; set; }
        /// <summary>
        /// 采购人用户编号
        /// </summary>
        public string BuyerUId { get; set; }
        /// <summary>
        /// 采购商联系人
        /// </summary>
        public string BuyerContactName { get; set; }
        /// <summary>
        /// 采购商联系手机
        /// </summary>
        public string BuyerContactMobile { get; set; }
        /// <summary>
        /// 采购商联系地址
        /// </summary>
        public string BuyerContactAddress { get; set; }
        /// <summary>
        /// 采购商联系MQ
        /// </summary>
        public string BuyerContactMQ { get; set; }
        /// <summary>
        /// 采购商备注(下单时特殊要求备注)
        /// </summary>
        public string BuyerRemark { get; set; }
        /// <summary>
        /// 服务备注
        /// </summary>
        public string ServiceNote { get; set; }
        /// <summary>
        /// 航空公司编号
        /// </summary>
        public int FlightId { get; set; }
        /// <summary>
        /// 出发地编号
        /// </summary>
        public int HomeCityId { get; set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        public string HomeCityName { get; set; }
        /// <summary>
        /// 目的地编号
        /// </summary>
        public int DestCityId { get; set; }
        /// <summary>
        /// 目的地名称
        /// </summary>
        public string DestCityName { get; set; }
        /// <summary>
        /// 去程航班号
        /// </summary>
        public string LFlightCode { get; set; }
        /// <summary>
        /// 回程航班号
        /// </summary>
        public string RFlightCode { get; set; }
        /// <summary>
        /// 原PNR
        /// </summary>
        public string OPNR { get; set; }
        /// <summary>
        /// 更换PNR
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        /// 行程单价格
        /// </summary>
        public decimal ItineraryPrice { get; set; }
        /// <summary>
        /// 快递费
        /// </summary>
        public decimal EMSPrice { get; set; }
        /// <summary>
        /// 总金额(下单金额)
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 供应商收款总金额
        /// </summary>
        public decimal BalanceAmount { get; set; }
        /// <summary>
        /// 供应商退款总金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 退款总金额(采购商收款金额)
        /// </summary>
        public decimal RefundTotalAmount { get; set; }
        /// <summary>
        /// 出票耗时(分钟)
        /// </summary>
        public int ElapsedTime { get; set; }
        /// <summary>
        /// 旅客人数
        /// </summary>
        public int PCount { get; set; }
        /// <summary>
        /// 订单状态 下单时直接支付时设置状态为审核通过
        /// </summary>
        public OrderState OrderState { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 支付接口类型
        /// </summary>
        public TicketAccountType PayType { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 订单运价信息
        /// </summary>
        public OrderRateInfo OrderRateInfo { get; set; }
        /// <summary>
        /// 旅客信息集合
        /// </summary>
        public IList<OrderTravellerInfo> Travellers { get; set; }

        /// <summary>
        /// 订单供应商出票成功率
        /// </summary>
        public decimal SuccessRate { get; set; }
        /// <summary>
        /// 运价类型
        /// </summary>
        public FreightType FreightType { get; set; }

        /// <summary>
        /// 订单当前变更人数
        /// </summary>
        public int ChangePeopleNum { get; set; }
        /// <summary>
        /// 最后操作人姓名
        /// </summary>
        public string LastOperatorName { get; set; }

    }
    #endregion

    #region 机票系统订单运价信息业务实体
    /// <summary>
    /// 机票系统订单运价信息业务实体
    /// </summary>
    public class OrderRateInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderRateInfo() { }

        /// <summary>
        /// 航空公司编号
        /// </summary>
        public int FlightId { get; set; }
        /// <summary>
        /// 运价类型:单程、往返程 etc.
        /// </summary>
        public FreightType FreightType { get; set; }
        /// <summary>
        /// 出发地编号
        /// </summary>
        public int HomeCityId { get; set; }
        /// <summary>
        /// 出发地名称
        /// </summary>
        public string HomeCityName { get; set; }
        /// <summary>
        /// 目的地编号
        /// </summary>
        public int DestCityId { get; set; }
        /// <summary>
        /// 目的地名称
        /// </summary>
        public string DestCityName { get; set; }
        /// <summary>
        /// 去程面价
        /// </summary>
        public decimal LeaveFacePrice { get; set; }
        /// <summary>
        /// 去程扣率
        /// </summary>
        public decimal LeaveDiscount { get; set; }
        /// <summary>
        /// 去程运价有效期
        /// </summary>
        public string LeaveTimeLimit { get; set; }
        /// <summary>
        /// 去程结算价
        /// </summary>
        public decimal LeavePrice { get; set; }
        /// <summary>
        /// 回程面价
        /// </summary>
        public decimal ReturnFacePrice { get; set; }
        /// <summary>
        /// 回程扣率
        /// </summary>
        public decimal ReturnDiscount { get; set; }
        /// <summary>
        /// 回程运价有效期
        /// </summary>
        public string ReturnTimeLimit { get; set; }
        /// <summary>
        /// 回程结算价
        /// </summary>
        public decimal ReturnPrice { get; set; }
        /// <summary>
        /// 去程燃油费
        /// </summary>
        public decimal LFuelPrice { get; set; }
        /// <summary>
        /// 去程机建费
        /// </summary>
        public decimal LBuildPrice { get; set; }
        /// <summary>
        /// 回程燃油费
        /// </summary>
        public decimal RFuelPrice { get; set; }
        /// <summary>
        /// 回程机建费
        /// </summary>
        public decimal RBuildPrice { get; set; }
        /// <summary>
        /// 人数上限
        /// </summary>
        public int MaxPCount { get; set; }
        /// <summary>
        /// 供应商备注
        /// </summary>
        public string SupplierRemark { get; set; }
    }
    #endregion

    #region 机票系统订单旅客状态变更信息业务实体
    /// <summary>
    /// 机票系统订单旅客状态变更信息业务实体
    /// </summary>
    public class OrderChangeInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderChangeInfo() { }

        /// <summary>
        /// 变更编号
        /// </summary>
        public string ChangeId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 变更状态
        /// </summary>
        public OrderChangeState ChangeState { get; set; }
        /// <summary>
        /// 通票、作废类型
        /// </summary>
        public RefundTicketType RefundTicketType { get; set; }
        /// <summary>
        /// 变更类型
        /// </summary>
        public OrderChangeType ChangeType { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 变更申请备注
        /// </summary>
        public string ChangeRemark { get; set; }
        /// <summary>
        /// 变更申请人编号
        /// </summary>
        public string ChangeUId { get; set; }
        /// <summary>
        /// 变更处理人编号
        /// </summary>
        public string CheckUId { get; set; }
        /// <summary>
        /// 变更申请人姓名
        /// </summary>
        public string ChangeUFullName { get; set; }
        /// <summary>
        /// 变更处理人姓名
        /// </summary>
        public string CheckUFullName { get; set; }
        /// <summary>
        /// 变更处理备注
        /// </summary>
        public string CheckRemark { get; set; }
        /// <summary>
        /// 变更处理时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 变更旅客信息集合
        /// </summary>
        public IList<string> Travellers { get; set; }
        /// <summary>
        /// 旅客状态
        /// </summary>
        public TravellerState TravellerState
        {
            get
            {
                return GetTravellerState(this.ChangeType, this.ChangeState);
            }
        }
        /// <summary>
        /// 变更人数
        /// </summary>
        public int PCount { get; set; }

        /// <summary>
        /// 根据订单变更类型及订单变更状态获取旅客状态
        /// </summary>
        /// <param name="changeType">订单变更类型</param>
        /// <param name="changeState">订单变更状态</param>
        /// <returns></returns>
        private static TravellerState GetTravellerState(OrderChangeType changeType, OrderChangeState changeState)
        {
            TravellerState travellerState = TravellerState.正常;

            switch (changeType)
            {
                case OrderChangeType.退票:
                    switch (changeState)
                    {
                        case OrderChangeState.申请: travellerState = TravellerState.申请退票; break;
                        case OrderChangeState.接受: travellerState = TravellerState.退票成功; break;
                        case OrderChangeState.拒绝: travellerState = TravellerState.拒绝退票; break;
                    }
                    break;
                case OrderChangeType.作废:
                    switch (changeState)
                    {
                        case OrderChangeState.申请: travellerState = TravellerState.申请作废; break;
                        case OrderChangeState.接受: travellerState = TravellerState.作废成功; break;
                        case OrderChangeState.拒绝: travellerState = TravellerState.拒绝作废; break;
                    }
                    break;
                case OrderChangeType.改期:
                    switch (changeState)
                    {
                        case OrderChangeState.申请: travellerState = TravellerState.申请改期; break;
                        case OrderChangeState.接受: travellerState = TravellerState.改期成功; break;
                        case OrderChangeState.拒绝: travellerState = TravellerState.拒绝改期; break;
                    }
                    break;
                case OrderChangeType.改签:
                    switch (changeState)
                    {
                        case OrderChangeState.申请: travellerState = TravellerState.申请改签; break;
                        case OrderChangeState.接受: travellerState = TravellerState.改签成功; break;
                        case OrderChangeState.拒绝: travellerState = TravellerState.拒绝改签; break;
                    }
                    break;
            }

            return travellerState;
        }
    }
    #endregion

    #region 机票系统订单旅客信息业务实体
    /// <summary>
    /// 机票系统旅客基本信息业务实体
    /// </summary>
    public class TravellerBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TravellerBaseInfo() { }

        /// <summary>
        /// 旅客编号
        /// </summary>
        public string TravellerId { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public TicketCardType CertType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertNo { get; set; }
        /// <summary>
        /// 旅客性别
        /// </summary>
        public EyouSoft.Model.CompanyStructure.Sex Gender { get; set; }
        /// <summary>
        /// 旅客联系电话
        /// </summary>
        public string Telephone { get; set; }
    }

    /// <summary>
    /// 机票系统订单旅客信息业务实体
    /// </summary>
    public class OrderTravellerInfo : TravellerBaseInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderTravellerInfo() { }

        /// <summary>
        /// 旅客姓名
        /// </summary>
        public string TravellerName { get; set; }
        /// <summary>
        /// 是否购买保险
        /// </summary>
        public bool IsBuyIns { get; set; }
        /// <summary>
        /// 保险价格
        /// </summary>
        public decimal InsPrice { get; set; }
        /// <summary>
        /// 是否购买行程单
        /// </summary>
        public bool IsBuyItinerary { get; set; }
        /// <summary>
        /// 旅客状态
        /// </summary>
        public TravellerState TravellerState { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNumber { get; set; }
        /// <summary>
        /// 旅客类别(按成人儿童分类)
        /// </summary>
        public TicketVistorType TravellerType { get; set; }
    }
    #endregion

    #region 机票系统订单账户信息业务实体
    /// <summary>
    /// 机票系统订单账户信息业务实体
    /// </summary>
    [Serializable]
    public class OrderAccountInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderAccountInfo() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 采购公司编号
        /// </summary>
        public string PayCompanyId { get; set; }
        /// <summary>
        /// 采购用户编号
        /// </summary>
        public string PayUserId { get; set; }
        /// <summary>
        /// 采购账号
        /// </summary>
        public string PayAccount { get; set; }
        /// <summary>
        /// 支付接口类型
        /// </summary>
        public Model.TicketStructure.TicketAccountType PayType { get; set; }
        /// <summary>
        /// 供应商公司编号
        /// </summary>
        public string SellCompanyId { get; set; }
        /// <summary>
        /// 供应商公司账号
        /// </summary>
        public string SellAccount { get; set; }
        /// <summary>
        /// 支付接口返回的交易号
        /// </summary>
        public string PayNumber { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 供应商费率
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 支付总金额
        /// </summary>
        public decimal PayPrice { get; set; }
    }

    #endregion

    #region 订单查询提交信息业务实体
    /// <summary>
    /// 订单查询提交信息业务实体
    /// </summary>
    public class OrderSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderSearchInfo() { }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 出发地编号
        /// </summary>
        public int? HomeCityId { get; set; }
        /// <summary>
        /// 目的地编号
        /// </summary>
        public int? DestCityId { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public RateType? RateType { get; set; }
        /// <summary>
        /// 下单开始日期
        /// </summary>
        public DateTime? OrderStartTime { get; set; }
        /// <summary>
        /// 下单结束日期
        /// </summary>
        public DateTime? OrderFinishTime { get; set; }
        /// <summary>
        /// 固定日期
        /// </summary>
        public DateTime? FixedDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderState? OrderState { get; set; }
        /// <summary>
        /// 订单变更类型
        /// </summary>
        public OrderChangeType? OrderChangeType { get; set; }
        /// <summary>
        /// 订单变更状态
        /// </summary>
        public OrderChangeState? OrderChangeState { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNumber { get; set; }
        /// <summary>
        /// 旅客姓名
        /// </summary>
        public string TravellerName { get; set; }
        /// <summary>
        /// PNR
        /// </summary>
        public string PNR { get; set; }
        /// <summary>
        ///  采购商公司编号
        /// </summary>
        public string BuyerCId { get; set; }
        /// <summary>
        /// 供应商公司编号
        /// </summary>
        public string SupplierCId { get; set; }
        /// <summary>
        /// 航空公司编号
        /// </summary>
        public int? FlightId { get; set; }
    }
    #endregion

    #region 采购分析查询提交信息业务实体
    /// <summary>
    /// 采购分析查询提交信息业务实体
    /// </summary>
    public class BuyerAnalysisSearchInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public BuyerAnalysisSearchInfo() { }

        /// <summary>
        /// 采购商用户名
        /// </summary>
        public string BuyerUName { get; set; }
        /// <summary>
        /// 成功购买机票数
        /// </summary>
        public int? TicketTotalCount { get; set; }
        /// <summary>
        /// 成功购买金额数
        /// </summary>
        public decimal? TicketTotalAmount { get; set; }
        /// <summary>
        /// 起始交易日期
        /// </summary>
        public DateTime? OrderStartTime { get; set; }
        /// <summary>
        /// 结束交易日期
        /// </summary>
        public DateTime? OrderFinishTime { get; set; }
        /// <summary>
        /// 供应商公司编号
        /// </summary>
        public string SupplierCId { get; set; }
    }
    #endregion

    #region 采购分析信息业务实体
    /// <summary>
    /// 采购分析信息业务实体
    /// </summary>
    public class BuyerAnalysisInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public BuyerAnalysisInfo() { }

        /// <summary>
        /// 采购商用户编号
        /// </summary>
        public string BuyerUId { get; set; }
        /// <summary>
        /// 采购商用户名
        /// </summary>
        public string BuyerUName { get; set; }
        /// <summary>
        /// 采购商用户姓名 
        /// </summary>
        public string BuyerContactName { get; set; }
        /// <summary>
        /// 采购商公司编号
        /// </summary>
        public string BuyerCId { get; set; }
        /// <summary>
        /// 采购商公司名称
        /// </summary>
        public string BuyerCName { get; set; }
        /// <summary>
        /// 采购商注册时间
        /// </summary>
        public DateTime BuyerRegisterTime { get; set; }
        /// <summary>
        /// 采购商最近交易时间
        /// </summary>
        public DateTime BuyerRecentOrderTime { get; set; }
        /// <summary>
        /// 采购商联系手机
        /// </summary>
        public string BuyerContactMobile { get; set; }
        /// <summary>
        /// 采购商联系MQ
        /// </summary>
        public string BuyerMQ { get; set; }
        /// <summary>
        /// 采购商购买机票数（张）
        /// </summary>
        public int TicketTotalCount { get; set; }
        /// <summary>
        /// 采购商购买机票总金额（元）
        /// </summary>
        public decimal TicketTotalAmount { get; set; }
        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatTime { get; set; }
        /// <summary>
        /// 采购商联系人性别
        /// </summary>
        public EyouSoft.Model.CompanyStructure.Sex BuyerContactGender { get; set; }
    }
    #endregion

    #region 机票系统订单操作信息业务实体
    /// <summary>
    /// 机票系统订单操作信息业务实体
    /// </summary>
    public class OrderHandleInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderHandleInfo() { }

        /// <summary>
        /// 操作编号
        /// </summary>
        public string HandelId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 操作人公司编号
        /// </summary>
        public string HandelCId { get; set; }
        /// <summary>
        /// 操作用户编号
        /// </summary>
        public string HandelUId { get; set; }
        /// <summary>
        /// 操作用户姓名
        /// </summary>
        public string HandelUFullName { get; set; }
        /// <summary>
        /// 操作前状态
        /// </summary>
        public OrderState PrevState { get; set; }
        /// <summary>
        /// 操作后状态
        /// </summary>
        public OrderState CurrState { get; set; }
        /// <summary>
        /// 操作说明
        /// </summary>
        public string HandleRemark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime HandelTime { get; set; }
    }
    #endregion

    #region 机票系统采购商端 订单报表合计 信息业务实体
    /// <summary>
    /// 机票系统采购商端 订单报表合计 信息业务实体
    /// </summary>
    public class BuyerOrderReportsTotalInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public BuyerOrderReportsTotalInfo() { }

        /// <summary>
        /// 去程面价
        /// </summary>
        public decimal LFacePrice { get; set; }
        /// <summary>
        /// 回程面价
        /// </summary>
        public decimal RFacePrice { get; set; }
        /// <summary>
        /// 去程燃油费
        /// </summary>
        public decimal LFuelPrice { get; set; }
        /// <summary>
        /// 回程燃油费
        /// </summary>
        public decimal RFuelPrice { get; set; }
        /// <summary>
        /// 去程机建费
        /// </summary>
        public decimal LBuildPrice { get; set; }        
        /// <summary>
        /// 回程机建费
        /// </summary>
        public decimal RBuildPrice { get; set; }
        /// <summary>
        /// 旅客人数
        /// </summary>
        public int Pcount { get; set; }
        /// <summary>
        /// 总金额(下单金额)
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 供应商收款金额
        /// </summary>
        public decimal BalanceAmount { get; set; }
        /// <summary>
        /// 供应商退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 退款总金额(采购商收款金额)
        /// </summary>
        public decimal RefundTotalAmount { get; set; }
        /// <summary>
        /// 去程代理费
        /// </summary>
        public decimal LAgencyAmount { get; set; }
        /// <summary>
        /// 回程代理费
        /// </summary>
        public decimal RAgencyAmount { get; set; }

        /// <summary>
        /// 合计票面价
        /// </summary>
        public decimal TotalFacePrice
        {
            get { return this.LFacePrice + this.RFacePrice; }
        }
        /// <summary>
        /// 合计燃油费
        /// </summary>
        public decimal TotalFuelPrice
        {
            get { return this.LFuelPrice + this.RFuelPrice; }
        }
        /// <summary>
        /// 合计机建费
        /// </summary>
        public decimal TotalBuidPrice
        {
            get { return this.LBuildPrice + this.RBuildPrice; }
        }
        /// <summary>
        /// 合计代理费
        /// </summary>
        public decimal TotalAgencyPrice
        {
            get { return this.LAgencyAmount + this.RAgencyAmount; }
        }

    }
    #endregion
}
