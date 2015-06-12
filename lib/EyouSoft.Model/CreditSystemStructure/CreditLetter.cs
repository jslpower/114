using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-20
    /// 描述:诚信体系-服务承诺书业务实体
    /// </summary>
    public class CreditLetter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CreditLetter() { }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 联盟编号
        /// </summary>
        public int UnionId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 承诺书文件路径
        /// </summary>
        public string PromiseFilePath { get; set; }
        /// <summary>
        /// 状态 0:未审核 1:通过审核 2:未通过审核 3:失效
        /// </summary>
        public int CheckedState { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 公司许可证号
        /// </summary>
        public string LicenseNumber { get; set; }
    }
}
