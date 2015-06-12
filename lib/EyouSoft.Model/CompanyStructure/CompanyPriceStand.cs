using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-06-01
    /// 描述：公司价格等级实体
    /// </summary>
    [Serializable]
    public class CompanyPriceStand
    {
        /// <summary>
        /// 公司价格等级编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公用价格等级编号
        /// </summary>
        public string CommonPriceStandID { get; set; }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string PriceStandName { get; set; }
        /// <summary>
        /// 是否系统定义
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
