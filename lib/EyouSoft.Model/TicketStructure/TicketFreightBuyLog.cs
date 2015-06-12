using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间:2010-10-21
    /// 描述:运价套餐购买记录实体
    /// </summary>
    [Serializable]
    public class TicketFreightBuyLog
    {
        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// 购买公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐编号
        /// </summary>
        public int PackageId
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐名称
        /// </summary>
        public string PackageName
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐类型
        /// </summary>
        public PackageTypes PackageType
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public RateType RateType
        {
            get;
            set;
        }
        /// <summary>
        /// 航空公司编号
        /// </summary>
        public int FlightId
        {
            get;
            set;
        }
        /// <summary>
        /// 航空公司名称
        /// </summary>
        public string FlightName
        {
            get;
            set;
        }
        /// <summary>
        /// 始发地编号
        /// </summary>
        public int HomeCityId
        {
            get;
            set;
        }
        /// <summary>
        /// 始发地名称
        /// </summary>
        public string HomeCityName
        {
            get;
            set;
        }
        /// <summary>
        /// 目的地编号集合(以,分隔)
        /// </summary>
        public string DestCityIds
        {
            get;
            set;
        }
        /// <summary>
        /// 目的地名称集合(以,分隔)
        /// </summary>
        public string DestCityNames
        {
            get;
            set;
        }
        /// <summary>
        /// 购买总数
        /// </summary>
        public int BuyCount
        {
            get;
            set;
        }
        /// <summary>
        /// 可用数
        /// </summary>
        public int AvailableCount
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal SumPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 支付状态(0:未付款 1：已付款)
        /// </summary>
        public bool PayState
        {
            get;
            set;
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime
        {
            get;
            set;
        }
        /// <summary>
        /// 目的地是否选择了全部城市
        /// </summary>
        public bool IsSelectAllCity
        {
            get 
            {
                return string.IsNullOrEmpty(DestCityIds);
            }
        }
        #endregion
    }

    #region 可用的运价套餐信息实体
    /// <summary>
    /// 可用的运价套餐信息实体
    /// </summary>
    public class AvailablePackInfo
    {
        #region 属性
        /// <summary>
        /// 购买编号
        /// </summary>
        public string BuyId
        {
            get;
            set;
        }
        /// <summary>
        /// [添加时：可用数 添加后：已使用条数]
        /// </summary>
        public int AvailableNum
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐类型[a、添加运价时-套餐类型不等于"常规"时，不需要更新可用数 b、运价添加完成后修改可用条数时不需要传递该属性]
        /// </summary>
        public PackageTypes PackType
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 运价套餐购买记录统计实体
    public class PackBuyLogStatistics
    {
        #region 属性
        /// <summary>
        /// 常规-已用数
        /// </summary>
        public int GeneralUsedCount
        {
            get;
            set;
        }
        /// <summary>
        /// 常规-可用数
        /// </summary>
        public int GeneralAvailableCount
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐-已用数
        /// </summary>
        public int PackageUsedCount
        {
            get;
            set;
        }
        /// <summary>
        /// 套餐-可用数
        /// </summary>
        public int PackageAvailableCount
        {
            get;
            set;
        }
        /// <summary>
        /// 促销-已用数
        /// </summary>
        public int SaleUsedCount
        {
            get;
            set;
        }
        /// <summary>
        /// 促销-可用数
        /// </summary>
        public int SaleAvailableCount
        {
            get;
            set;
        }
        #endregion
    }
    #endregion
}
