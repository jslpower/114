using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 机场信息实体类
    /// </summary>
    /// 创建人：luofx 2010-10-21
    [Serializable]
    public class TicketSeattle
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int SeattleId { get; set; }
        /// <summary>
        /// 机场名称
        /// </summary>
        public string Seattle { get; set; }
        /// <summary>
        /// 机场三字码
        /// </summary>
        public string LKE { get; set; }
        /// <summary>
        /// 机场全拼
        /// </summary>
        public string LongName { get; set; }
        /// <summary>
        /// 机场简拼
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 是否热门（true:热门,false:不热门）
        /// </summary>
        public bool IsHot { get; set; }
    }
}
