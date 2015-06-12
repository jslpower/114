using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 线路区域基类
    /// </summary>
    /// 创建人:张志瑜   2010-6-4
    [Serializable]
    public class AreaBase
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// 线路区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 线路区域类别
        /// </summary>
        public AreaType RouteType { get; set; }
    }

    /// <summary>
    /// 线路区域明细,继承了类AreaBase
    /// </summary>
    /// 周文超 2010-05-12
    [Serializable]
    public class SysArea : AreaBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysArea()
        { }

        #region Model

        /// <summary>
        /// 线路区域主要游览城市
        /// </summary>
        public IList<SysAreaVisitCity> VisitCity { get; set; }

        #endregion Model
    }

    /// <summary>
    /// 线路区域主要游览城市
    /// </summary>
    [Serializable]
    public class SysAreaVisitCity
    {
        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 县区编号
        /// </summary>
        public int CountyId { get; set; }
    }

    #region 线路区域类型 (enum) AreaType
    /// <summary>
    /// 线路区域类型
    /// </summary>
    /// Author:汪奇志 2010-05-18
    public enum AreaType
    {
        /// <summary>
        /// 国内长线
        /// </summary>
        国内长线 = 0,
        /// <summary>
        /// 国外
        /// </summary>
        国际线,
        /// <summary>
        /// 国内短线
        /// </summary>
        国内短线,
        /// <summary>
        /// 地接线路
        /// </summary>
        地接线路
    }
    #endregion

}
