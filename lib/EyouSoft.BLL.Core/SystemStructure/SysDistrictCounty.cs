using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 县区业务层
    /// </summary>
    public class SysDistrictCounty:IBLL.SystemStructure.ISysDistrictCounty
    {

        public SysDistrictCounty() { }

        private readonly IDAL.SystemStructure.ISysDistrictCounty dal = 
            EyouSoft.Component.Factory.ComponentFactory.Create<IDAL.SystemStructure.ISysDistrictCounty>();

        /// <summary>
        /// 创建业务逻辑层接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.SystemStructure.ISysDistrictCounty CreateInstance()
        {
            IBLL.SystemStructure.ISysDistrictCounty op = null;
            if (op == null)
            {
                op  = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.SystemStructure.ISysDistrictCounty>();
            }
            return op;
        }

        #region ISysDistrictCounty 成员
        /// <summary>
        /// 获取所有县区
        /// </summary>
        /// <returns>县区集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> GetDistrictCounty()
        {
            IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> list = null;
            object cacheObject = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemCounty);
            if (cacheObject != null)
            {
                list = (IList<EyouSoft.Model.SystemStructure.SysDistrictCounty>)cacheObject;
            }
            else
            {
                list = dal.GetDistrictCounty();
                if (list != null && list.Count > 0)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemCounty, list);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据城市获取县区
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns>县区编号</returns>
        public IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> GetDistrictCounty(int cityId)
        {
            return (from c in GetDistrictCounty() where c.CityId == cityId select c).ToList();
        }

        /// <summary>
        /// 获取县区实体
        /// </summary>
        /// <param name="districtId">县区编号</param>
        /// <returns>县区实体</returns>
        public Model.SystemStructure.SysDistrictCounty GetModel(int districtId)
        {
            Model.SystemStructure.SysDistrictCounty item = null;
            object cacheObject = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(string.Format(EyouSoft.CacheTag.System.SystemCountyInfo,districtId));
            if (cacheObject != null)
            {
                item = (Model.SystemStructure.SysDistrictCounty)cacheObject;
            }
            else
            {
                item = dal.GetModel(districtId);
                if (item != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.System.SystemCountyInfo, districtId), item);
                }
            }

            return item;
        }

        /// <summary>
        /// 根据城市ID集合获取城市实体集合
        /// </summary>
        /// <param name="districtIds">城市编号集合</param>
        /// <returns>县区编号</returns>
        public IList<Model.SystemStructure.SysDistrictCounty> GetDistrictCountyList(int[] districtIds)
        {
            if (districtIds == null || districtIds.Length <= 0)
                return null;
            IList<Model.SystemStructure.SysDistrictCounty> list = GetDistrictCounty();
            if (list == null)
                return null;
            return (from c in list where districtIds.Contains(c.Id) select c).ToList();
        }

        #endregion
    }
}
