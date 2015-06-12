using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 应收账款信息
    /// </summary>
    /// 周文超 2010-11-9
    [Serializable]
    public class Receivables
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Receivables() { }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 添加用户编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 团队ID
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }
        /// <summary>
        /// 团队所属订单号[手动添加时=""]
        /// </summary>
        public string TourOrderId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 组团社
        /// </summary>
        public string RetailersName { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 下单人MQ
        /// </summary>
        public string OperatorMQ { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 已审核已收
        /// </summary>
        public decimal CheckendPrice { get; set; }

        /// <summary>
        /// 未审核已收
        /// </summary>
        public decimal NoCheckPrice { get; set; }

        /// <summary>
        /// 清算时间
        /// </summary>
        public DateTime? ClearTime { get; set; }
    }
}
