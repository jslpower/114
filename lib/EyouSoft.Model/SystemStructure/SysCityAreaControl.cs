using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 城市线路区域关系基类
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysCityArea : AreaBase 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCityArea()
		{}

		#region Model

        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 是否首页显示
        /// </summary>
        public bool IsDefaultShow { get; set; }
		
		#endregion Model
    }

    /// <summary>
    /// 城市线路区域关系
    /// </summary>
    /// 周文超 2010-07-08
    [Serializable]
    public class SysCityAreaControl : SysCityArea
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 团队数
        /// </summary>
        public int TourCount { get; set; }
    }
}
