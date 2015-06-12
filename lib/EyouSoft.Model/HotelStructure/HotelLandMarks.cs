using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店系统-地理位置（地标）实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    [Serializable]
    public class HotelLandMarks
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 地理位置（地标）
        /// </summary>
        public string Por { get; set; }
        /// <summary>
        /// 城市三字码
        /// </summary>
        public string CityCode { get; set; }
    }
}
