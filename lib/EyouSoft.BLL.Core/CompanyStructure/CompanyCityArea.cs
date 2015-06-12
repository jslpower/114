using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    #region 单位经营城市[包括出港城市,销售城市]
    /// <summary>
    /// 描述:单位经营城市[包括出港城市,销售城市]业务逻辑层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public class CompanyCity : EyouSoft.IBLL.CompanyStructure.ICompanyCity
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyCity idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyCity>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyCity CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyCity op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyCity>();
            }
            return op;
        }
        /// <summary>
        /// 获得公司所拥有的销售城市列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanySaleCity(string companyId)
        {
            string cachename = EyouSoft.CacheTag.Company.CompanyCity + companyId;
            IList<EyouSoft.Model.SystemStructure.CityBase> list = (IList<EyouSoft.Model.SystemStructure.CityBase>)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);
            if (list == null)
            {
                list = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(idal.GetCompanySaleCity(companyId));
                if (list != null)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, list, DateTime.Now.AddHours(1));
            }
            return list;
        }
        /// <summary>
        /// 获得公司常用的出港城市列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.CityBase> GetCompanyPortCity(string companyId)
        {
            string cachename = EyouSoft.CacheTag.Company.CompanySite + companyId;

            EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.SystemStructure.CityBase>> list =
                (EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.SystemStructure.CityBase>>)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);
                        
            object UpdateTime = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SysSiteUpdateKey);
            if (UpdateTime == null)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SysSiteUpdateKey, DateTime.Now);
            }
            if (list != null && UpdateTime != null && list.UpdateTime > (DateTime)UpdateTime)
            {
                return list.Data;
            }
            else
            {
                list = new EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.SystemStructure.CityBase>>();
                list.Data = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(idal.GetCompanyPortCity(companyId)).ToList();
                list.UpdateTime = DateTime.Now;
                if (list != null)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, list, DateTime.Now.AddHours(1));
            }
            return list.Data;
        }
        /// <summary>
        /// 设置公司常用的出港城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="items">常用的出港城市列表</param>
        /// <returns></returns>
        public bool SetCompanyPortCity(string companyId, IList<EyouSoft.Model.SystemStructure.CityBase> items)
        {
            RemoveCompanySiteCache(companyId);
            return idal.SetCompanyPortCity(companyId, items);
        }
        /// <summary>
        /// 移除公司销售城市缓存
        /// </summary>
        /// <param name="companyId">公司ID</param>
        private void RemoveCompanySiteCache(string companyId)
        {
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.CompanySite + companyId);
        }        
    }
    #endregion 单位经营城市[包括出港城市,销售城市]

    #region 单位线路区域
    /// <summary>
    /// 描述:单位线路区域业务逻辑层
    /// 创建人：张志瑜 2010-07-08
    /// </summary>
    public class CompanyArea : EyouSoft.IBLL.CompanyStructure.ICompanyArea
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyArea idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyArea>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyArea CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyArea op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyArea>();
            }
            return op;
        }
        /// <summary>
        /// 获得公司所拥有的线路区域列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.AreaBase> GetCompanyArea(string companyId)
        {
            return EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetAreaList(idal.GetCompanyArea(companyId));
        }
        /// <summary>
        /// 获得用户所拥有的线路区域列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.AreaBase> GetUserArea(string userId)
        {
            return EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetAreaList(idal.GetUserArea(userId));
        }        
    }
    #endregion 单位线路区域

    #region 单位待增加的经营城市[包括出港城市,销售城市]
    /// <summary>
    /// 描述:单位待增加的经营城市[包括出港城市,销售城市]业务逻辑层
    /// 创建人：张志瑜 2010-07-14
    /// </summary>
    public class CompanyUnCheckedCity : EyouSoft.IBLL.CompanyStructure.ICompanyUnCheckedCity
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyUnCheckedCity idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyUnCheckedCity>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyUnCheckedCity CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyUnCheckedCity op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyUnCheckedCity>();
            }
            return op;
        }

        /// <summary>
        /// 新增公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="cityText">城市名称(多个城市,则城市名称使用逗号分割)</param>
        /// <returns></returns>
        public bool AddSaleCity(string companyId, string cityText)
        {
            return idal.AddSaleCity(companyId, cityText);
        }
        /// <summary>
        /// 获得公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public string GetSaleCity(string companyId)
        {
            return idal.GetSaleCity(companyId);
        }
        /// <summary>
        /// 删除公司待增加的销售城市
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public bool DeleteSaleCity(string companyId)
        {
            return idal.DeleteSaleCity(companyId);
        }
    }
    #endregion 单位待增加的经营城市[包括出港城市,销售城市]
}
