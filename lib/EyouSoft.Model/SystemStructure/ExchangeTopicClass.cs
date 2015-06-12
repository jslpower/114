using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：互动交流栏目类别
    /// </summary>
    public class ExchangeTopicClass
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeTopicClass() { }

        #region 属性
        /// <summary>
        /// 互动类别编号
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        ///  互动类别名称
        /// </summary>
        public string TopicClassName
        {
            get;
            set;
        }
        /// <summary>
        /// 栏目排序(默认为0,升序排列)
        /// </summary>
        public byte SortId
        {
            get;
            set;
        }
        /// <summary>
        /// 类别样式名
        /// </summary>
        public string TopicCssClass
        {
            get;
            set;
        }
        #endregion
    }
}
