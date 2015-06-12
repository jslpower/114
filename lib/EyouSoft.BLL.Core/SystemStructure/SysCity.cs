using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;
using System.Transactions;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 城市 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SysCity : IBLL.SystemStructure.ISysCity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCity() { }

        private readonly IDAL.SystemStructure.ISysCity dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysCity>();

        /// <summary>
        /// 构造城市业务逻辑接口 
        /// </summary>
        /// <returns>城市业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysCity CreateInstance()
        {
            IBLL.SystemStructure.ISysCity op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysCity>();
            }
            return op;
        }

        #region 城市函数

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <returns>返回城市实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList()
        {
            IList<EyouSoft.Model.SystemStructure.SysCity> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemCity);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysCity>)cachelist;
            else
            {
                list = dal.GetCityList();
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemCity, list);
            }
            return list;
        }

        /// <summary>
        /// 根据城市ID获取城市实体(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>系统城市实体</returns>
        public Model.SystemStructure.SysCity GetSysCityModel(int CityId)
        {
            if (CityId <= 0)
                return null;

            Model.SystemStructure.SysCity city = (Model.SystemStructure.SysCity)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(
                string.Format(EyouSoft.CacheTag.System.SystemCityInfo, CityId));

            if (city != null)
            {
                return city;
            }
            else
            {

                city = dal.GetModel(CityId,string.Empty);
                if (city != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.System.SystemCityInfo, CityId), city, DateTime.Now.AddHours(1));
                }
                return city;
            }
        }

        /// <summary>
        /// 根据城市域名获取城市实体(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="DomainName">城市域名</param>
        /// <returns>系统城市实体</returns>
        public Model.SystemStructure.SysCity GetSysCityModel(string DomainName)
        {
            if (string.IsNullOrEmpty(DomainName))
                return null;

            Model.SystemStructure.SysCity city = (Model.SystemStructure.SysCity)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(
                string.Format(EyouSoft.CacheTag.System.SystemCityInfo, DomainName.ToLower()));

            if (city != null)
            {
                return city;
            }
            else
            {

                city = dal.GetModel(0, DomainName.ToLower());
                if (city != null)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.System.SystemCityInfo, DomainName.ToLower()), city, DateTime.Now.AddHours(1));
                }
                return city;
            }
        }

        /// <summary>
        /// 根据城市ID集合获取城市实体集合
        /// </summary>
        /// <param name="CityIds">城市ID集合(小于等于0获取所有城市)</param>
        /// <returns>城市实体集合</returns>
        public IList<Model.SystemStructure.SysCity> GetSysCityList(int[] CityIds)
        {
            if (CityIds == null || CityIds.Length <= 0)
                return null;
            IList<EyouSoft.Model.SystemStructure.SysCity> list = GetCityList();
            if (list == null)
                return null;
            return (from c in list where CityIds.Contains(c.CityId) select c).ToList();
        }

        /// <summary>
        /// 获取所有的销售城市
        /// </summary>
        /// <param name="IsSite">是否出港城市(为true取既是销售也是出港的城市)</param>
        /// <param name="ProvinceId">省份ID集合</param>
        /// <returns>城市实体集合</returns>
        public IList<Model.SystemStructure.SysCity> GetSaleCity(bool? IsSite, params int[] ProvinceId)
        {
            IList<EyouSoft.Model.SystemStructure.SysCity> list = GetCityList();
            if (list == null)
                return null;

            return list.Where(Item => ((IsSite.HasValue ? (bool)IsSite : true) && (ProvinceId != null && ProvinceId.Length > 0 ? ProvinceId.Contains(Item.ProvinceId) : true))).ToList();
        }
        
        /// <summary>
        /// 根据省份ID集合获取城市实体集合
        /// </summary>
        /// <param name="ProvinceIds">省份ID集合</param>
        /// <returns>城市实体集合</returns>
        public IList<Model.SystemStructure.SysCity> GetProvinceCityList(int[] ProvinceIds)
        {
            if (ProvinceIds == null || ProvinceIds.Length <= 0)
                return null;
            IList<EyouSoft.Model.SystemStructure.SysCity> list = GetCityList();
            if (list == null)
                return null;
            return (from c in list where ProvinceIds.Contains(c.ProvinceId) select c).ToList();
        }

        /// <summary>
        /// 获得指定城市ID的城市信息集合
        /// </summary>
        /// <param name="items">城市ID集合</param>
        public IList<EyouSoft.Model.SystemStructure.CityBase> GetCityList(IList<EyouSoft.Model.SystemStructure.CityBase> items)
        {
            //从缓存中获取省份ID,省份名称,城市名称
            if (items != null && items.Count > 0)
            {
                int count = items.Count;
                EyouSoft.Model.SystemStructure.SysCity cityModel = null;
                for (int index = 0; index < count; index++)
                {
                    cityModel = GetSysCityModel(items[index].CityId);
                    if (cityModel != null)
                    {
                        items[index].CityName = cityModel.CityName;
                        items[index].ProvinceId = cityModel.ProvinceId;
                        items[index].ProvinceName = cityModel.ProvinceName;
                    }
                }
                cityModel = null;
            }

            return items;
        }

        /// <summary>
        /// 根据省份ID获取城市实体集合
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="IsSite">是否出港城市(为null取该省下所有城市)</param>
        /// <returns>城市实体集合</returns>
        public IList<Model.SystemStructure.SysCity> GetSysCityList(int ProvinceId, bool? IsSite)
        {
            if (ProvinceId > 0)
                return GetCityList(ProvinceId, -1, null, IsSite, null);
            else
                return null;
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="ProvinceId">省份ID，小于等于0不作条件</param>
        /// <param name="CenterCityId">省会城市ID，小于等于0不作条件</param>
        /// <param name="HeaderLetter">首字母，为null或者空不作条件</param>
        /// <param name="IsSite">是否出港城市(为null不作条件)</param>
        /// <param name="IsEnabled">是否启用(为null不作条件)</param>
        /// <returns>返回城市实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int ProvinceId, int CenterCityId, string HeaderLetter
            , bool? IsSite, bool? IsEnabled)
        {
            IList<EyouSoft.Model.SystemStructure.SysCity> list = GetCityList();
            if (list == null)
                return null;

            list = list.Where(Item => ((ProvinceId > 0 ? Item.ProvinceId == ProvinceId : true) && (CenterCityId > 0 ? Item.CenterCityId == CenterCityId : true) && (string.IsNullOrEmpty(HeaderLetter) ? true : Item.HeaderLetter.Contains(HeaderLetter)) && (IsSite == null ? true : Item.IsSite == (bool)IsSite) && (IsEnabled == null ? true : Item.IsEnabled == (bool)IsEnabled))).ToList();

            return list;
        }

        /// <summary>
        /// 获取城市列表(含该城市下的的线路区域集合以及线路区域公司广告集合)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引(0/1：城市首字母升/降序；2/3：城市ID升/降序；)</param>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否停用</param>
        /// <returns>城市列表</returns>
        public IList<EyouSoft.Model.SystemStructure.SysCity> GetCityList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, int ProvinceId, int CityId, bool? IsEnabled)
        {
            return dal.GetCityList(PageSize, PageIndex, ref RecordCount, OrderIndex, ProvinceId, CityId, IsEnabled);
        }

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsEnabled">是否启用</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsEnabled(int CityId, bool IsEnabled)
        {
            if (CityId <= 0)
                return false;

            bool IsSuccess = dal.SetIsEnabled(CityId, IsEnabled);
            if (IsSuccess)
            {
                ClearCache(CityId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemCity);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemProvince);
            }

            return IsSuccess;
        }

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsSite(int CityId, bool IsSite)
        {
            if (CityId <= 0)
                return false;

            bool IsSuccess = dal.SetIsSite(CityId.ToString(), IsSite);
            if (IsSuccess)
            {
                ClearCache(CityId);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemCity);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SysSiteUpdateKey, DateTime.Now);
            }
            return IsSuccess;
        }

        /// <summary>
        /// 设置是否出港城市
        /// </summary>
        /// <param name="CityIds">城市ID集合</param>
        /// <param name="IsSite">是否出港</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetIsSite(int[] CityIds, bool IsSite)
        {
            if (CityIds == null || CityIds.Length <= 0)
                return false;
            string strIds = string.Empty;
            foreach (int i in CityIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            bool IsSuccess = dal.SetIsSite(strIds, IsSite);
            if (IsSuccess)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemCity);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SysSiteUpdateKey, DateTime.Now);
            }

            return IsSuccess;
        }

        #endregion

        #region 城市线路区域关系函数

        /// <summary>
        /// 添加城市线路区域关系
        /// </summary>
        /// <param name="List">城市线路区域关系集合</param>
        /// <param name="CityId">城市ID(小于等于0则取集合中的)</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysSiteAreaControl(IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List, int CityId)
        {
            if (List == null || List.Count <= 0)
                return 0;

            int Result = dal.AddSysSiteAreaControl(List, CityId);
            if (Result > 0)
            {
                ClearCache(CityId);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 修改城市线路区域关系(先删后加模式)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="List">城市线路区域关系集合</param>
        /// <returns>-1:添加新关系失败;0:Error;1:Success</returns>
        public int UpdateSysSiteAreaControl(int CityId, IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> List)
        {
            if (CityId <= 0)
                return 0;

            int iRow = 0;
            using (TransactionScope UpdateTran = new TransactionScope())
            {
                DeleteSysSiteAreaControl(CityId);

                if (List != null && List.Count > 0)
                {
                    iRow = AddSysSiteAreaControl(List, CityId);
                    if (iRow <= 0)
                        return -2;
                }

                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(EyouSoft.CacheTag.System.SystemCityInfo, CityId));
                iRow = 1;
                UpdateTran.Complete();
            }
            return iRow;
        }

        /// <summary>
        /// 根据城市ID删除城市线路区域关系(该城市下所有线路区域)
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysSiteAreaControl(int CityId)
        {
            if (CityId <= 0)
                return false;

            bool IsSuccess = dal.DelSysSiteAreaControl(CityId, null);
            if (IsSuccess)
            {
                ClearCache(CityId);
            }

            return IsSuccess;
        }

        /// <summary>
        /// 删除城市线路区域关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysSiteAreaControl(int CityId, int[] AreaIds)
        {
            if (CityId <= 0 || AreaIds == null || AreaIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AreaIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            bool IsSuccess = false;
            IsSuccess = dal.DelSysSiteAreaControl(CityId, strIds);
            if (IsSuccess)
            {
                ClearCache(CityId);
            }

            return IsSuccess;
        }

        #endregion

        #region 系统IP函数

        /// <summary>
        /// 根据客户端Ip地址返回城市基类实体
        /// </summary>
        /// <param name="IpAddress">客户端Ip地址</param>
        /// <returns>城市基类实体</returns>
        public Model.SystemStructure.CityBase GetClientCityByIp(string IpAddress)
        {
            if (string.IsNullOrEmpty(IpAddress))
                return null;

            return dal.GetClientCityByIp(IpAddress);
        }

        #endregion
        #region 城市缓存清除
        /// <summary>
        /// 单个城市缓存清除
        /// </summary>
        /// <param name="CityId">城市编号</param>
        public void ClearCache(int CityId)
        {
            Model.SystemStructure.SysCity city = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (city != null)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(EyouSoft.CacheTag.System.SystemCityInfo, city.DomainName.ToLower()));
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(string.Format(EyouSoft.CacheTag.System.SystemCityInfo, CityId));
            }
        }
        #endregion
    }
}
