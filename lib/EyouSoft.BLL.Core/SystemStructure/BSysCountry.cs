using System;
using System.Collections.Generic;
using System.Linq;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 国家信息业务逻辑
    /// </summary>
    public class BSysCountry : IBLL.SystemStructure.ISysCountry
    {
        private readonly IDAL.SystemStructure.ISysCountry _dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysCountry>();

        /// <summary>
        /// 构造线路区域业务逻辑接口
        /// </summary>
        /// <returns>线路区域业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysCountry CreateInstance()
        {
            IBLL.SystemStructure.ISysCountry op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysCountry>();
            }
            return op;
        }

        /// <summary>
        /// 获取国家信息实体
        /// </summary>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public Model.SystemStructure.MSysCountry GetCountry(int countryId)
        {
            if (countryId <= 0)
                return null;

            var list = GetCountryList();
            if (list == null)
                return null;

            return list.First(tmp => tmp.CountryId == countryId);
        }

        /// <summary>
        /// 获取所有的国家信息
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.MSysCountry> GetCountryList()
        {
            IList<Model.SystemStructure.MSysCountry> list;

            object cachelist = Cache.Facade.EyouSoftCache.GetCache(CacheTag.System.SystemCountry);
            if (cachelist != null)
                list = (IList<Model.SystemStructure.MSysCountry>)cachelist;
            else
            {
                list = _dal.GetCountryList();
                if (list == null)
                    return null;

                Cache.Facade.EyouSoftCache.Add(CacheTag.System.SystemCountry, list);
            }
            return list;
        }

        /// <summary>
        /// 根据地理洲际获取国家信息
        /// </summary>
        /// <param name="continent">地理洲际编号</param>
        /// <returns></returns>
        public IList<Model.SystemStructure.MSysCountry> GetCountryList(params Model.SystemStructure.Continent[] continent)
        {
            if(continent == null || continent.Length < 1)
                return null;

            var list = GetCountryList();
            if (list == null)
                return null;

            return (from c in list where continent.Contains(c.Continent) select c).ToList();
        }

        /// <summary>
        /// 根据国家ID集合获取国家实体集合
        /// </summary>
        /// <param name="countryIds">国家编号</param>
        /// <returns></returns>
        public IList<Model.SystemStructure.MSysCountry> GetCountryList(int[] countryIds)
        {
            if (countryIds == null || countryIds.Length <= 0)
                return null;
            IList<Model.SystemStructure.MSysCountry> list = GetCountryList();
            if (list == null)
                return null;
            return (from c in list where countryIds.Contains(c.CountryId) select c).ToList();
        }
    }
}
