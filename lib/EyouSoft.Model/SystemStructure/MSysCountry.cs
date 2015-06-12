using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统国家基类
    /// </summary>
    [Serializable]
    public class MCountryBase
    {
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 国家中文名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 国家英文名称
        /// </summary>
        public string EnName { get; set; }
    }

    /// <summary>
    /// 系统国家实体
    /// </summary>
    [Serializable]
    public class MSysCountry : MCountryBase
    {
        /// <summary>
        /// 国家所属洲
        /// </summary>
        public Continent Continent { get; set; }

        /// <summary>
        /// Zones
        /// </summary>
        public int Zones { get; set; }
    }

    /// <summary>
    /// 地理洲际枚举
    /// </summary>
    public enum Continent
    {
        ///<summary>
        /// 亚洲
        ///</summary>
        亚洲 = 1,
        ///<summary>
        /// 欧洲
        ///</summary>
        欧洲 = 2,
        ///<summary>
        /// 非洲
        ///</summary>
        非洲 = 3,
        ///<summary>
        /// 大洋洲
        ///</summary>
        大洋洲 = 4,
        ///<summary>
        /// 北美洲
        ///</summary>
        北美洲 = 5,
        ///<summary>
        /// 中美洲
        ///</summary>
        中美洲 = 6, 
        ///<summary>
        /// 南美洲
        ///</summary>
        南美洲 = 7
    }
}
