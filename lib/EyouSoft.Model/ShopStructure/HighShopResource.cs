using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：旅游资源推荐
    /// </summary>
    public class HighShopResource
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopResource() { }
        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string ContentText
        {
            get;
            set;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImagePath
        {
            get;
            set;
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            get;
            set;
        }
        /// <summary>
        /// 置顶时间
        /// </summary>
        public DateTime TopTime
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }

}
