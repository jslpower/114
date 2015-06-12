using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 机票折扣申请
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public class TicketApply
    {
        private TicketFlight _ticketflight = new TicketFlight();

        /// <summary>
        /// 申请的ID编号
        /// </summary>
        public string ApplyId { get; set; }

        /// <summary>
        /// 机票文章ID
        /// </summary>
        public string TicketArticleID { get; set; }

        /// <summary>
        /// 机票文章标题
        /// </summary>
        public string TicketArticleTitle { get; set; }

        /// <summary>
        /// 机票航班信息
        /// </summary>
        public TicketFlight TicketFlight { get { return this._ticketflight; } set { this._ticketflight = value; } }

        /// <summary>
        /// 采购商公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 采购商联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 采购商公司联系电话
        /// </summary>
        public string CompanyTel { get; set; }

        /// <summary>
        /// 采购商公司联系地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 机票折扣申请查询条件
    /// </summary>
    public class QueryTicketApply
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 航程
        /// </summary>
        public EyouSoft.Model.TicketStructure.VoyageType VoyageType { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 出发时间（起）
        /// </summary>
        public DateTime? TakeOffDateStart { get; set; }
        /// <summary>
        /// 出发时间（至）
        /// </summary>
        //public DateTime? TakeOffDateEnd { get; set; }
    }
}
