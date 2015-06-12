using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    #region 产品意见反馈类型
    /// <summary>
    /// 产品意见反馈类型
    /// </summary>
    public enum ProductSuggestionType
    {
        /// <summary>
        /// 商铺
        /// </summary>
        商铺 = 1,
        /// <summary>
        /// 平台
        /// </summary>
        平台,
        /// <summary>
        /// 个人中心
        /// </summary> 
        个人中心,
        /// <summary>
        /// MQ
        /// </summary>
        MQ,
        /// <summary>
        /// 个人中心报价标准
        /// </summary> 
        个人中心报价标准
    }
    #endregion

    #region 产品意见反馈信息业务实体
    /// <summary>
    /// 产品意见反馈信息业务实体
    /// </summary>
    /// 创建人：鲁功源 2010-06-01
    public class ProductSuggestionInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductSuggestionInfo() { }

        /// <summary>
        /// 意见编号
        /// </summary>
        public string SuggestionId{ get; set; }
        /// <summary>
        /// 意见反馈类型
        /// </summary>
        public ProductSuggestionType SuggestionType { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 联系MQ
        /// </summary>
        public string MQ { get; set; }
        /// <summary>
        /// 意见内容
        /// </summary>
        public string ContentText { get;set;}
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    #endregion 
}
