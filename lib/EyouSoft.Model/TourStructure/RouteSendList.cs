using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：地接社发送线路信息
    /// </summary>
    public class RouteSendList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteSendList() { }

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
        /// 发送方公司ID(默认为0)  单索引
        /// </summary>
        public string FromCompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 发送方用户ID(默认为0)
        /// </summary>
        public string FromOperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 接收方公司ID(默认为0)  单索引
        /// </summary>
        public string ToCompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 线路ID(默认为newid())
        /// </summary>
        public string RouteID
        {
            get;
            set;
        }
        /// <summary>
        /// 接收后的线路ID( 未接收时该值为null, 默认为null)
        /// </summary>
        public string AcceptRouteID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已接收(0:未接收   1:已接收  默认为0)
        /// </summary>
        public bool IsAccept
        {
            get;
            set;
        }
        /// <summary>
        /// 发送时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        #endregion
    }
}
