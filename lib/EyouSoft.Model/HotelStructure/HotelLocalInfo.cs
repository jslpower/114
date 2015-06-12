using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店系统-首页酒店信息实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelLocalInfo
    {
        /// <summary>
        /// 主键Id(guid)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 接口酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 酒店介绍
        /// </summary>
        public string ShortDesc { get; set; }
        /// <summary>
        ///  酒店星级
        /// </summary>
        public EyouSoft.HotelBI.HotelRankEnum Rank { get; set; }
        /// <summary>
        /// 促销价
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 酒店所在城市三字码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 酒店图片（图片路径）
        /// </summary>
        public string HotelImg { get; set; }
        /// <summary>
        /// 显示类型 
        /// </summary>
        public HotelShowType ShowType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    /// <summary>
    /// 酒店系统首页板块-显示类型
    /// </summary>
    public enum HotelShowType
    {
        /// <summary>
        /// 最新加入酒店
        /// </summary>
        最新加入酒店 = 0,
        /// <summary>
        /// 促销酒店
        /// </summary>
        促销酒店 = 1,
        /// <summary>
        /// 特推酒店
        /// </summary>
        特推酒店 = 2,
        /// <summary>
        /// 明星酒店推荐
        /// </summary>
        明星酒店推荐 = 3
    }
}
