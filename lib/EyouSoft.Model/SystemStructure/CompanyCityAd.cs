using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 单位城市广告关系
    /// </summary>
    /// 周文超 2010-07-08
    [Serializable]
    public class CompanyCityAd
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyCityAd() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        public CompanyCityAd(string CompanyId, int CityId, int AreaId)
        {
            this.CompanyId = CompanyId;
            this.CityId = CityId;
            this.AreaId = AreaId;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId { get; set; }
    }
}
