using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 常用短语业务实体类
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class TemplateInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TemplateInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="templateId">常用短语编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="categoryId">分类编号</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="content">短语内容</param>
        /// <param name="issueTime">IssueTime</param>
        public TemplateInfo(string templateId, string companyId, string userId, int categoryId, string categoryName, string content, DateTime issueTime)
        {
            this.TemplateId = templateId;
            this.CompanyId = companyId;
            this.UserId = userId;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.Content = content;
            this.IssueTime = issueTime;
        }

        /// <summary>
        /// 常用短语编号
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 短语内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
}
