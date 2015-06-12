using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 城市基类
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class CityBase: ProvinceBase
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }

    /// <summary>
    /// 城市明细类,继承了类CityBase
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysCity : CityBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCity()
        {
            this.CityAreaControls = new List<SysCityArea>();
            this.AreaAdvNum = new Dictionary<int, IList<string>>();
        }

        #region Model    
    
        /// <summary>
        /// 省会城市ID
        /// </summary>
        public int CenterCityId { get; set; }
        /// <summary>
        /// 城市简拼
        /// </summary>
        public string HeaderLetter { get; set; }

        /// <summary>
        /// 线路数量
        /// </summary>
        public int ParentTourCount { get; set; }

        /// <summary>
        /// 散拼团和团队数量总和
        /// </summary>
        public int TourCount { get; set; }

        /// <summary>
        /// 是否出港城市
        /// </summary>
        public bool IsSite { get; set; }

        /// <summary>
        /// 城市二级域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 城市UrlRewrite代码
        /// </summary>
        public string RewriteCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 城市线路区域关系集合
        /// </summary>
        public IList<SysCityArea> CityAreaControls { get; set; }

        /// <summary>
        /// 线路区域下绑定的批发商数量(AreaId,CompanyIds)
        /// 区域ID为键，公司编号集合为值
        /// </summary>
        public Dictionary<int,IList<string>> AreaAdvNum { get; set; }

        #endregion Model
    }

    /// <summary>
    /// 分站,线路区域信息(某个分站信息以及对应所有的线路区域信息)
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class SiteAreaInfo : CityBase
    {
        /// <summary>
        /// 所对应的线路区域列表(若无线路区域信息,则返回null)
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.AreaBase> AreaList { get; set; }
    }

    /// <summary>
    /// 分站,线路区域列表信息(所有分站,以及当前分站对应的所有线路区域信息)
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class SiteAreaList
    {
        /// <summary>
        /// 当前分站ID
        /// </summary>
        public int CurrentSiteId { get; set; }
        /// <summary>
        /// 所有分站信息
        /// </summary>
        public List<CityBase> SiteList { get; set; }
        /// <summary>
        /// 当前分站所对应的线路区域列表(若当前分站为0或无线路区域信息,则返回null)
        /// </summary>
        public List<EyouSoft.Model.SystemStructure.AreaBase> AreaList { get; set; }
    }
}
