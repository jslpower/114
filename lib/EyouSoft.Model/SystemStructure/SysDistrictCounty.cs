using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 县区实体
    /// 创建人：郑付杰
    /// 创建时间：2011/10/24
    /// </summary>
    [Serializable]
    public class SysDistrictCounty
    {
        public SysDistrictCounty() { }

        /// <summary>
        /// 县区编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 县区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 城市简拼 
        /// </summary>
        public string HeaderLetter { get; set; }
    }
}
