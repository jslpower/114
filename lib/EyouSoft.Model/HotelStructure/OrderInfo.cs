//Author:汪奇志 2010-12-01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    #region 酒店系统订单类别
    /// <summary>
    /// 酒店系统订单类别
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 国内现付
        /// </summary>
        国内现付
    }
    #endregion

    #region 酒店订单审核状态枚举
    /// <summary>
    /// 酒店订单审核状态
    /// </summary>
    public enum CheckStateList
    { 
        /// <summary>
        /// 待审结
        /// </summary>
        待审结=1,
        /// <summary>
        /// 在入住
        /// </summary>
        在入住=2,
        /// <summary>
        /// 入住正常
        /// </summary>
        入住正常=3,
        /// <summary>
        /// LESSSHOW
        /// </summary>
        LESSSHOW=4,
        /// <summary>
        /// 延住
        /// </summary>
        延住=5,
        /// <summary>
        /// NOWSHOW
        /// </summary>
        NOWSHOW=6
    }
    #endregion

    #region 酒店订单状态枚举
    /// <summary>
    /// 酒店订单状态
    /// </summary>
    public enum OrderStateList
    { 
        /// <summary>
        /// 已确认
        /// </summary>
        已确认=0,
        /// <summary>
        /// 处理中
        /// </summary>
        处理中=1,
        /// <summary>
        /// 取消
        /// </summary>
        取消=2,
        /// <summary>
        /// NOWSHOW
        /// </summary>
        NOWSHOW=3

    }
    #endregion

    #region 酒店系统订单信息业务实体
    /// <summary>
    /// 酒店系统订单信息业务实体
    /// </summary>
    public class OrderInfo : EyouSoft.HotelBI.HBEOrderInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OrderInfo() { }

        /// <summary>
        /// 订单编号(平台)
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单类别
        /// </summary>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 采购公司编号
        /// </summary>
        public string BuyerCId { get; set; }
        /// <summary>
        /// 采购公司名称
        /// </summary>
        public string BuyerCName { get; set; }
        /// <summary>
        /// 采购用户编号
        /// </summary>
        public string BuyerUId { get; set; }
        /// <summary>
        /// 采购用户名
        /// </summary>
        public string BuyerUName { get; set; }
        /// <summary>
        /// 采购用户姓名
        /// </summary>
        public string BuyerUFullName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public CheckStateList CheckState { get; set; }
        /// <summary>
        /// 订单状态(平台)
        /// </summary>
        public OrderStateList OrderState
        {
            get {
                switch (this.ResStatus)
                { 
                    case EyouSoft.HotelBI.HBEResStatus.CAN:
                    case EyouSoft.HotelBI.HBEResStatus.XXX:
                    case EyouSoft.HotelBI.HBEResStatus.HAC:
                        return OrderStateList.取消;
                        break;
                    case EyouSoft.HotelBI.HBEResStatus.CON:
                        return OrderStateList.已确认;
                        break;
                    case EyouSoft.HotelBI.HBEResStatus.MOD:
                    case EyouSoft.HotelBI.HBEResStatus.RCM:
                    case EyouSoft.HotelBI.HBEResStatus.RES:
                        return OrderStateList.处理中;
                        break;
                }
                return OrderStateList.处理中;
            }
        }
        /// <summary>
        /// 价格清单
        /// </summary>
        public IList<RateInfo> Rates { get; set; }
    }
    #endregion

    #region 酒店订单查询实体
    /// <summary>
    /// 酒店订单查询实体
    /// </summary>
    [Serializable]
    public class SearchOrderInfo
    {
        /// <summary>
        /// 订单号(航旅通接口)
        /// </summary>
        public string ResOrderId { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public EyouSoft.Model.HotelStructure.OrderType? OrderType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public EyouSoft.Model.HotelStructure.OrderStateList? OrderState { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 旅客名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public EyouSoft.Model.HotelStructure.CheckStateList? CheckState { get; set; }
        /// <summary>
        /// 入住开始时间
        /// </summary>
        public DateTime? CheckInSDate { get; set; }
        /// <summary>
        /// 入住结束时间
        /// </summary>
        public DateTime? CheckInEDate { get; set; }
        /// <summary>
        /// 离店开始时间
        /// </summary>
        public DateTime? CheckOutSDate { get; set; }
        /// <summary>
        /// 离店结束时间
        /// </summary>
        public DateTime? CheckOutEDate { get; set; }
        /// <summary>
        /// 预定开始时间
        /// </summary>
        public DateTime? CreateSDate { get; set; }
        /// <summary>
        /// 预定结束时间
        /// </summary>
        public DateTime? CreateEDate { get; set; }
        /// <summary>
        /// 采购商公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 采购用户名
        /// </summary>
        public string BuyerUName { get; set; }
    }
    #endregion
}
