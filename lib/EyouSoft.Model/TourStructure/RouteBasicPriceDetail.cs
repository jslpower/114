using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    #region 线路报价信息业务实体 RoutePriceDetail
    /// <summary>
    /// 线路报价信息业务实体
    /// </summary>
    /// Author:鲁功源 2010-05-12
    [Serializable]
    public class RoutePriceDetail:PriceDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public RoutePriceDetail() { }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }

        /// <summary>
        /// 线路报价客户等级报价信息业务实体集合
        /// </summary>
        public IList<RoutePriceCustomerLeaveDetail> PriceDetail { get; set; }
    }
    #endregion

    #region 线路报价客户等级报价信息业务实体 RoutePriceCustomerLeaveDetail
    /// <summary>
    ///  线路报价客户等级报价信息业务实体 
    /// </summary>
    [Serializable]
    public class RoutePriceCustomerLeaveDetail:PriceCustomerLeaveDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public RoutePriceCustomerLeaveDetail() { }
    }
    #endregion

    #region 报价信息业务实体基类 PriceDetail
    /// <summary>
    /// 报价信息业务实体基类
    /// </summary>
    [Serializable]
    public class PriceDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public PriceDetail() { }

        /// <summary>
        /// 报价等级编号
        /// </summary>
        public string PriceStandId { get; set; }
        /// <summary>
        /// 报价等级名称
        /// </summary>
        public string PriceStandName { get; set; }
    }
    #endregion

    #region 报价客户等级报价信息业务实体基类 PriceCustomerLeaveDetail
    /// <summary>
    /// 报价客户等级报价信息业务实体基类
    /// </summary>
    [Serializable]
    public class PriceCustomerLeaveDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PriceCustomerLeaveDetail() { }

        /// <summary>
        /// 客户等级编号
        /// </summary>
        public int CustomerLevelId { get; set; }
        
        /// <summary>
        /// 客户等级类型
        /// </summary>
        public CompanyStructure.CustomerLevelType CustomerLevelType
        {
            get 
            {
                if (this.CustomerLevelId < 3)
                {
                    return (EyouSoft.Model.CompanyStructure.CustomerLevelType)this.CustomerLevelId;
                }
                return EyouSoft.Model.CompanyStructure.CustomerLevelType.其它;
            }
        }
        /// <summary>
        /// 成人价[同行价格]
        /// </summary>
        public decimal AdultPrice { get; set; }
        /// <summary>
        /// 儿童价[门市价格]
        /// </summary>
        public decimal ChildrenPrice { get; set; }

    }
    #endregion   
}
