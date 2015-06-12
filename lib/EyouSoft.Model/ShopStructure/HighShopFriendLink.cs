using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ShopStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-01
    /// 描述：友情链接
    /// </summary>
    public class HighShopFriendLink
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public HighShopFriendLink() { }
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
        /// 链接名称
        /// </summary>
        public string LinkName
        {
            get;
            set;
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 排序值
        /// </summary>
        public int SortID
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
