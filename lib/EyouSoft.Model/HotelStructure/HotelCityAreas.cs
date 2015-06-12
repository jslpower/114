using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店系统-城市区域实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    [Serializable]
    public class HotelCityAreas
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// 城市三字码
        /// </summary>
        public string CityCode { get; set; }
    }
}
