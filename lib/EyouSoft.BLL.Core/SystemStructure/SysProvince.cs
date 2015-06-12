using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 省份 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SysProvince : IBLL.SystemStructure.ISysProvince 
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysProvince() { }

        private readonly IDAL.SystemStructure.ISysProvince dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysProvince>();

        /// <summary>
        /// 构造省份业务逻辑接口
        /// </summary>
        /// <returns>省份业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysProvince CreateInstance()
        {
            IBLL.SystemStructure.ISysProvince op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysProvince>();
            }
            return op;
        }



        #region ISysProvince 成员
        /// <summary>
        /// 获取已开通省份列表
        /// </summary>
        /// <returns>返回省份实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetEnabledList()
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemProvinceEnabled);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysProvince>)cachelist;
            else
            {
                list = dal.GetProvinceList(false);
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemProvinceEnabled, list);
            }
            return list;
        }
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns>返回省份实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList()
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemProvince);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysProvince>)cachelist;
            else
            {
                list = dal.GetProvinceList(true);
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemProvince, list);
            }
            return list;
        }

        /// <summary>
        /// 获取省份基本信息实体
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回省份实体</returns>
        public Model.SystemStructure.ProvinceBase GetProvinceBaseModel(int ProvinceId)
        {
            return null; 
        }

        /// <summary>
        /// 获取省份基本信息实体
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回省份实体</returns>
        public EyouSoft.Model.SystemStructure.SysProvince GetProvinceModel(int ProvinceId)
        {
            if (ProvinceId <= 0)
                return null;
            IList<EyouSoft.Model.SystemStructure.SysProvince> list = GetProvinceList();
            list = list.Where(Item => (Item.ProvinceId == ProvinceId)).ToList();
            if (list == null || list.Count <= 0)
                return null;
            else
                return list[0];
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="ProvinceIds">省份ID集合</param>
        /// <returns>返回省份实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(int[] ProvinceIds)
        {
            if (ProvinceIds == null || ProvinceIds.Length <= 0)
                return null;

            IList<EyouSoft.Model.SystemStructure.SysProvince> List = GetProvinceList();
            return List.Where(Item => (ProvinceIds.Contains(Item.ProvinceId))).ToList();
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="CountryId">国家编号，小于等于0不作为条件</param>
        /// <param name="HeaderLetter">首字母，为空 不作为条件</param>
        /// <param name="AreaId">区域编号，小于等于0不作为条件</param>
        /// <returns>返回省份实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetProvinceList(int CountryId, string HeaderLetter
            , EyouSoft.Model.SystemStructure.ProvinceAreaType? AreaId)
        {
            IList<EyouSoft.Model.SystemStructure.SysProvince> List = GetProvinceList();
            return List.Where(Item => ((CountryId > 0 ? Item.CountryId == CountryId : true) && (string.IsNullOrEmpty(HeaderLetter) ? true : Item.HeaderLetter.Contains(HeaderLetter)) && (AreaId.HasValue ? Item.AreaId == AreaId : true))).ToList();
        }
        /// <summary>
        /// 获取存在资讯信息的所有省份列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysProvince> GetExistsNewsProvinceList()
        {
            return dal.GetExistsNewsProvinceList();
        }
        #endregion
    }
}
