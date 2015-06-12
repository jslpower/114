using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：待新增公用价格等级
    /// </summary>
    [Serializable]
    public class CommonPriceStandAdd
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonPriceStandAdd() { }

        #region 属性
        /// <summary>
        /// 列表ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  提交公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 提交人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 报价标准名称
        /// </summary>
        public string PriceStandName { get; set; }
        /// <summary>
        /// 提交时间[不必设置]
        /// </summary>
        public DateTime IssueTime { get; set; }
        #endregion
    }
}
