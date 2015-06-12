using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 系统国家数据接口
    /// </summary>
    public interface ISysCountry
    {
        /// <summary>
        /// 获取国家信息实体
        /// </summary>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        Model.SystemStructure.MSysCountry GetCountry(int countryId);

        /// <summary>
        /// 获取所有的国家信息
        /// </summary>
        /// <returns></returns>
        IList<Model.SystemStructure.MSysCountry> GetCountryList();

        /// <summary>
        /// 根据地理洲际获取国家信息
        /// </summary>
        /// <param name="continent">地理洲际编号</param>
        /// <returns></returns>
        IList<Model.SystemStructure.MSysCountry> GetCountryList(params Model.SystemStructure.Continent[] continent);
    }
}
