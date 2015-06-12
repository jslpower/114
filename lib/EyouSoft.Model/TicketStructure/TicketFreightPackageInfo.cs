using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间:2010-10-21
    /// 描述:运价套餐信息实体
    /// </summary>
    [Serializable]
    public class TicketFreightPackageInfo
    {
        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
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
        /// 目的地编号集合(以,分隔)
        /// </summary>
        public string DestCityIds
        {
            get;
            set;
        }
        /// <summary>
        /// 月价格
        /// </summary>
        public decimal MonthPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 季价格
        /// </summary>
        public decimal QuarterPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 半年价格
        /// </summary>
        public decimal HalfYearPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 年价格
        /// </summary>
        public decimal YearPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted
        {
            get;
            set;
        }
        #endregion
    }

    #region 套餐类型枚举

    public enum PackageTypes
    { 
        /// <summary>
        /// 常规
        /// </summary>
        常规=1,
        /// <summary>
        /// 套餐
        /// </summary>
        套餐,
        /// <summary>
        /// 促销
        /// </summary>
        促销
    }

    #endregion
}
