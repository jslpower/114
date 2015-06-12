using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 长短线区域城市关系
    /// </summary>
    /// 周文超 2010-07-09
    [Serializable]
    public class SysAreaSiteControl : AreaBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysAreaSiteControl() { }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }

    }
}
