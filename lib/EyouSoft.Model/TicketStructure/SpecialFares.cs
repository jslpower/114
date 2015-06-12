using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 特价机票实体类 
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class SpecialFares
    {
        public SpecialFares() { }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 特价机票类型
        /// </summary>
        public SpecialFaresType SpecialFaresType { get; set; }
        /// <summary>
        /// 是否跳转至散客票平台
        /// </summary>
        public bool IsJump { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string ContentText { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactWay { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }

    /// <summary>
    /// 特价机票枚举类型
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public enum SpecialFaresType
    {
        /// <summary>
        /// 特价
        /// </summary>
        特价 = 1,
        /// <summary>
        /// 免票
        /// </summary>
        免票 = 2,
        /// <summary>
        /// K位
        /// </summary>
        K位 = 3
    }
}
