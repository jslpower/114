using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-15
    /// 描述：供求浏览记录实体类
    /// </summary>
    [Serializable]
    public class ExchangeVisited : ExchangeBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeVisited() { }

        #region 属性
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }
}
