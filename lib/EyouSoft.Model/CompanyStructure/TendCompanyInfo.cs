using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 经营单位实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-31
    [Serializable]
    public class TendCompanyInfo : CompanyBasicInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TendCompanyInfo() { }

        #region 属性
        /// <summary>
        /// 经营单位编号
        /// </summary>
        public new string ID { get; set; }
        /// <summary>
        /// 经营单位所属公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 经营单位所在区域编号
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 是否审核通过(true:审核通过  false:未通过)
        /// </summary>
        public bool IsEnabled { get; set; }
        #endregion
    }
}
