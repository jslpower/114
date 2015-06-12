using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 机票平台-国籍基础信息实体类
    /// </summary>
    /// 创建人：luofx 2010-11-3
    [Serializable]
    public class TicketNationInfo
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int NationId { get; set; }
        /// <summary>
        /// 国籍代号
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName { get; set; }
    }
}
