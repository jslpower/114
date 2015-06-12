using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.NewsStructure
{
    /// <summary>
    /// Tag标签与KeyWord关键字实体
    /// </summary>
    /// 鲁功源 2011-03-31
    public class TagKeyInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TagKeyInfo() { }
        #endregion

        #region 属性
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 所属类别
        /// </summary>
        public ItemCategory Category
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            get;
            set;
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string ItemUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion

    }

    #region 项目类别枚举
    /// <summary>
    /// 项目类别枚举
    /// </summary>
    public enum ItemCategory
    { 
        /// <summary>
        /// Tag标签
        /// </summary>
        Tag=1,
        /// <summary>
        /// 关键字
        /// </summary>
        KeyWord=2
    }
    #endregion

}
