using System;
using System.Collections.Generic;

namespace EyouSoft.Model.TourStructure
{
    #region TourPriceDetail
    /// <summary>
    /// 团队报价信息业务实体
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourPriceDetail : PriceDetail
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourPriceDetail() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团队报价客户等级报价信息业务实体集合
        /// </summary>
        public IList<TourPriceCustomerLeaveDetail> PriceDetail { get; set; }
    }
    #endregion 

    #region TourPriceCustomerLeaveDetail
    /// <summary>
    /// 团队报价客户等级报价信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public class TourPriceCustomerLeaveDetail : PriceCustomerLeaveDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourPriceCustomerLeaveDetail() { }
    }
    #endregion   
}
