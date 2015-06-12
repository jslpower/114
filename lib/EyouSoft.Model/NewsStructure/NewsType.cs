using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.NewsStructure
{
    /// <summary>
    /// 资讯类别实体
    /// </summary>
    /// 鲁功源 2011-03-31
    public class NewsType
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsType() { }
        #endregion

        #region  属性
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 所属类别编号
        /// </summary>
        public NewsCategory Category
        {
            get;
            set;
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string ClassName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsSystem
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

    #region 资讯大类别枚举
    /// <summary>
    /// 资讯大类别
    /// </summary>
    public enum NewsCategory
    {
        /// <summary>
        /// 行业资讯
        /// </summary>
        行业资讯=1,
        /// <summary>
        /// 同业学堂
        /// </summary>
        同业学堂=2,
        /// <summary>
        /// 用户后台公告
        /// </summary>
        用户后台公告 = 3
    }
    #endregion

}
