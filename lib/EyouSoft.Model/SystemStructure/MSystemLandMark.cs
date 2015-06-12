using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 城市地标
    /// 创建人：郑付杰
    /// 创建时间：2011/11/28
    /// </summary>
    [Serializable]
    public class MSystemLandMark
    {
        public MSystemLandMark() { }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 地标名称
        /// </summary>
        public string Por { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 三字码
        /// </summary>
        public string CityCode { get; set; }
    }
}
