using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 客户类型业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class CustomerCategoryInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CustomerCategoryInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="categoryId">类型编号</param>
        /// <param name="categoryName">类型名称</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="issueTime">创建时间</param>
        public CustomerCategoryInfo(int categoryId, string categoryName, string companyId, string userId, DateTime issueTime)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.CompanyId = companyId;
            this.UserId = userId;
            this.IssueTime = issueTime;
        }

        /// <summary>
        /// 类型编号
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }

}
