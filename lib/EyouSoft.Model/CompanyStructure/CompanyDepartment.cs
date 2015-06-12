using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-05-11
    /// 描述：基础数据-公司部门实体类
    /// </summary>
    public class CompanyDepartment
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyDepartment() { }

        #region 属性
        /// <summary>
        /// 部门编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        #endregion
    }
}
