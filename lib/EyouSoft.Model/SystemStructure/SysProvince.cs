using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 省份基类
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class ProvinceBase
    {
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }
    }
    /// <summary>
    /// 省份,分站列表信息(所有省份,以及当前省份对应的所有分站信息)
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class ProvinceSiteList
    {
        /// <summary>
        /// 当前省份ID
        /// </summary>
        public int CurrentProvinceId { get; set; }
        /// <summary>
        /// 所有省份信息
        /// </summary>
        public List<ProvinceBase> ProvinceList { get; set; }
        /// <summary>
        /// 当前省份所对应的分站列表(若当前省份为0或无分站信息,则返回null)
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.CityBase> SiteList { get; set; }
    }

    /// <summary>
    /// 省份,分站,线路区域列表信息(所有省份,以及当前省份对应的所有分站信息,当前分站对应的所有线路区域信息),继承了类ProvinceSiteList
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class ProvinceSiteAreaList : ProvinceSiteList
    {
        /// <summary>
        /// 当前分站ID
        /// </summary>
        public int CurrentSiteId { get; set; }
        /// <summary>
        /// 当前分站所对应的线路区域列表(若当前分站为0或无线路区域信息,则返回null)
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.AreaBase> AreaList { get; set; }
    }

    /// <summary>
    /// 省份,城市列表信息(所有省份,以及当前省份对应的所有城市信息)
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class ProvinceCityList
    {
        /// <summary>
        /// 当前省份ID
        /// </summary>
        public int CurrentProvinceId { get; set; }
        /// <summary>
        /// 所有省份信息
        /// </summary>
        public List<ProvinceBase> ProvinceList { get; set; }
        /// <summary>
        /// 当前省份所对应的城市列表(若当前省份为0或无城市信息,则返回null)
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.CityBase> CityList { get; set; }
    }


    /// <summary>
    /// 省份明细类,继承了类ProvinceBase
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysProvince : ProvinceBase
    {
        #region Model
        /// <summary>
        /// 省份简拼
        /// </summary>
        public string HeaderLetter { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// 地区类型
        /// </summary>
        public ProvinceAreaType AreaId { get; set; }

        #endregion Model
    }

    /// <summary>
    /// 省份所属区域枚举
    /// </summary>
    public enum ProvinceAreaType
    {
        /// <summary>
        /// 未知
        /// </summary>
        未知 = 0,
        /// <summary>
        /// 直辖市
        /// </summary>
        直辖市 = 1,
        /// <summary>
        /// 华北_东北区
        /// </summary>
        华北_东北区 = 2,
        /// <summary>
        /// 华东区
        /// </summary>
        华东区 = 3,
        /// <summary>
        /// 华南_华中区
        /// </summary>
        华南_华中区 = 4,
        /// <summary>
        /// 西北_西南区
        /// </summary>
        西北_西南区 = 5,
        /// <summary>
        /// 港澳台地区
        /// </summary>
        港澳台地区 = 6
    }
}
