using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 国家信息业务逻辑接口
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

        /// <summary>
        /// 根据国家ID集合获取国家实体集合
        /// </summary>
        /// <param name="countryIds">国家编号</param>
        /// <returns></returns>
        IList<Model.SystemStructure.MSysCountry> GetCountryList(int[] countryIds);
    }
}
