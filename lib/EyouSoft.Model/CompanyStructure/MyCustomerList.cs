using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：张志瑜 2010-05-27
    /// 描述：我的客户
    /// </summary>
    [Serializable]
    public class MyCustomer : EyouSoft.Model.CompanyStructure.CompanyInfo
    {
        #region 属性
        /// <summary>
        /// 我的客户列表编号
        /// </summary>
        public new string ID { get; set; }
        /// <summary>
        /// 当前登录系统的用户的公司编号
        /// </summary>
        public string CurrentCompanyID { get; set; }
        /// <summary>
        /// 当前登录系统的用户编号
        /// </summary>
        public string CurrentOperatorID { get; set; }
        /// <summary>
        /// 客户公司编号
        /// </summary>
        public string CustomerCompanyID { get; set; }
        #endregion
    }
}
