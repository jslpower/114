using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 团队票实体类
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class GroupTickets
    {
        public GroupTickets() { }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 团队票类型
        /// </summary>
        public GroupType GroupType { get; set; }
        /// <summary>
        /// 起始城市
        /// </summary>
        public string StartCity { get; set; }
        /// <summary>
        /// 终点城市
        /// </summary>
        public string EndCity { get; set; }
        /// <summary>
        /// 乘机人数
        /// </summary>
        public int PelopeCount { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 航空公司类型
        /// </summary>
        public AirlinesType AirlinesType { get; set; }
        /// <summary>
        /// 去程航班号
        /// </summary>
        public string FlightNumber { get; set; }
        /// <summary>
        /// 出发时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 时间范围
        /// </summary>
        public string TimeRange { get; set; }
        /// <summary>
        /// 返程时间
        /// </summary>
        public string RoundTripTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系QQ
        /// </summary>
        public string ContactQQ { get; set; }
        /// <summary>
        /// 希望价格
        /// </summary>
        public decimal HopesPrice { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 乘客信息
        /// </summary>
        public IList<PassengerInformation> PassengerInformationList
        {
            get;
            set;
        }
    }

    #region 团队票枚举类型

    /// <summary>
    /// 团队票枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public enum GroupType
    {
        单程 = 1,
        往返 = 2,
        联程 = 3,
        缺程 = 4
    }
    #endregion
    #region 航空公司枚举类型
    /// <summary>
    /// 航空公司枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public enum AirlinesType
    {
        CA国际航空 = 1,
        东方航空 = 2
    }

    #endregion
}
