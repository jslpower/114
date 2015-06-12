using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店系统-城市信息实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    [Serializable]
    public class HotelCity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 全拼
        /// </summary>
        public string Spelling { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 城市三字码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 是否常用（true:常用，false：不常用）
        /// </summary>
        public bool IsHot { get; set; }
    }
}
