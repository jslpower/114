using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：互动交流询价
    /// </summary>
    public class ExchangeAskPrice
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeAskPrice() { }

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
        /// 询价人公司ID(默认为0)
        /// </summary>
        public string AskCompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 询价人用户ID(默认为0)
        /// </summary>
        public string AskOperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 询价的目标公司ID(默认为0)
        /// </summary>
        public string ToCompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 询价时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }
}
