using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 线路区域 业务逻辑
    /// </summary>
    /// 周文超 2010-05-27
    /// -----------------------
    /// 修改人：鲁功源 2011-04-06
    /// 新增内容：获取存在团队信息的线路区域集合方法GetExistsTourAreas()
    public class SysArea : IBLL.SystemStructure.ISysArea
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysArea() { }

        private readonly IDAL.SystemStructure.ISysArea dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysArea>();

        /// <summary>
        /// 构造线路区域业务逻辑接口
        /// </summary>
        /// <returns>线路区域业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysArea CreateInstance()
        {
            IBLL.SystemStructure.ISysArea op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysArea>();
            }
            return op;
        }

        #region ISysArea 成员

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemArea);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysArea>)cachelist;
            else
            {
                list = dal.GetSysAreaList();
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemArea, list);
            }
            return list;
        }
        /// <summary>
        /// 获取存在线路数据的线路区域集合
        /// </summary>
        /// <returns>返回线路区域实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysArea> GetExistsTourAreas()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.ExistsTourAreas);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysArea>)cachelist;
            else
            {
                list = dal.GetExistsTourAreas();
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.ExistsTourAreas, list, DateTime.Now.AddHours(1));
            }
            return list;
        }
        /// <summary>
        /// 获取线路区域实体
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>线路区域实体</returns>
        public Model.SystemStructure.SysArea GetSysAreaModel(int AreaId)
        {
            if (AreaId <= 0)
                return null;

            IList<EyouSoft.Model.SystemStructure.SysArea> list = GetSysAreaList();
            list = list.Where(Item => (Item.AreaId == AreaId)).ToList();
            if (list == null || list.Count <= 0)
                return null;
            else
                return list[0];
        }

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="RouteType">线路类型</param>
        /// <returns>返回线路区域实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList(Model.SystemStructure.AreaType RouteType)
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> list = GetSysAreaList();
            return list.Where(Item => (Item.RouteType == RouteType)).ToList();
        }

        /// <summary>
        /// 获取线路区域
        /// </summary>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回线路区域实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysArea> GetSysAreaList(int[] AreaIds)
        {
            if (AreaIds == null || AreaIds.Length <= 0)
                return null;

            IList<EyouSoft.Model.SystemStructure.SysArea> list = GetSysAreaList();
            return list.Where(Item => (AreaIds.Contains(Item.AreaId))).ToList();
        }

        /// <summary>
        /// 获得指定线路区域ID的线路区域信息集合
        /// </summary>
        /// <param name="items">线路区域ID集合</param>
        public IList<EyouSoft.Model.SystemStructure.AreaBase> GetAreaList(IList<EyouSoft.Model.SystemStructure.AreaBase> items)
        {
            //从缓存中获取线路区域名称,线路区域类型
            if (items != null && items.Count > 0)
            {
                int count = items.Count;
                EyouSoft.Model.SystemStructure.SysArea areaModel = null;
                for (int index = 0; index < count; index++)
                {
                    if (items[index] != null)
                    {
                        areaModel = this.GetSysAreaModel(items[index].AreaId);
                        if (areaModel != null)
                        {
                            items[index].AreaName = areaModel.AreaName;
                            items[index].RouteType = areaModel.RouteType;
                        }
                    }
                }
                areaModel = null;
            }

            return items;
        }

        /// <summary>
        /// 新增线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysArea(EyouSoft.Model.SystemStructure.SysArea model)
        {
            if (model == null)
                return 0;

            int Result = dal.AddSysArea(model);
            if (Result > 0)
            {
                model.AreaId = Result;
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemArea);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 修改线路区域
        /// </summary>
        /// <param name="model">线路区域实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateSysArea(EyouSoft.Model.SystemStructure.SysArea model)
        {
            if (model == null)
                return 0;

            int Result = dal.UpdateSysArea(model);
            if (Result > 0)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemArea);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemCity);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 删除线路区域(物理删除，有有效计划时删除失败)
        /// </summary>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysArea(int AreaId)
        {
            if (AreaId <= 0)
                return false;

            bool Result = dal.DeleteSysArea(AreaId);
            if (Result)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemArea);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemCity);
            }

            return Result;
        }

        /// <summary>
        /// 获取包含某线路区域的所有分站（销售城市）
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        public IList<Model.SystemStructure.CityBase> GetSalesCityByArea(int areaId)
        {
            if (areaId <= 0)
                return null;

            return dal.GetSalesCityByArea(areaId);
        }

        #endregion

        #region 长短线区域城市关系函数成员

        /// <summary>
        /// 添加长短线区域城市关系
        /// </summary>
        /// <param name="List">长短线区域城市关系实体集合</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysAreaSiteControl(IList<Model.SystemStructure.SysAreaSiteControl> List)
        {
            if (List == null || List.Count <= 0)
                return 0;

            int Result = dal.AddSysAreaSiteControl(List);
            if (Result > 0)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 获取所有的长短线区域城市关系
        /// </summary>
        /// <returns>长短线区域城市关系实体集合</returns>
        public IList<Model.SystemStructure.SysAreaSiteControl> GetSysAreaSiteControl()
        {
            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> list = null;

            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemAreaCatalog);
            if (cachelist != null)
                list = (IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl>)cachelist;
            else
            {
                list = dal.GetSysAreaSiteControl();
                if (list == null)
                    return null;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemAreaCatalog, list);
            }
            return list;
        }

        /// <summary>
        /// 获取某省份的国内长线
        /// </summary>
        /// <param name="ProvinceId">省份ID(小于等于0取所有)</param>
        /// <returns>长短线区域城市关系实体集合</returns>
        public IList<Model.SystemStructure.SysAreaSiteControl> GetLongAreaSiteControl(int ProvinceId)
        {
            IList<Model.SystemStructure.SysAreaSiteControl> List = GetSysAreaSiteControl();
            if (List == null)
                return null;
            else
                return
                    List.Where(
                        Item =>
                        (Item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线 &&
                         (ProvinceId > 0 ? Item.ProvinceId == ProvinceId : true))).ToList();

        }

        /// <summary>
        /// 获取某城市的国内短线
        /// </summary>
        /// <param name="CityId">城市ID(小于等于0取所有)</param>
        /// <returns>长短线区域城市关系实体集合</returns>
        public IList<Model.SystemStructure.SysAreaSiteControl> GetShortAreaSiteControl(int CityId)
        {
            IList<Model.SystemStructure.SysAreaSiteControl> List = GetSysAreaSiteControl();
            if (List == null)
                return null;
            else
                return List.Where(Item => (Item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线 && (CityId > 0 ? Item.CityId == CityId : true))).ToList();
        }

        /// <summary>
        /// 删除国内长线和省份的关系(删除某一省份下的所有)
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteLongAreaSiteControl(int ProvinceId)
        {
            if (ProvinceId <= 0)
                return false;

            bool Result = dal.DeleteSysAreaSiteControl(ProvinceId, -1, null, Model.SystemStructure.AreaType.国内长线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        /// <summary>
        /// 删除国内长线和省份的关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteLongAreaSiteControl(int ProvinceId, int AreaId)
        {
            if (ProvinceId <= 0 || AreaId <= 0)
                return false;

            bool Result = dal.DeleteSysAreaSiteControl(ProvinceId, -1, AreaId.ToString(), Model.SystemStructure.AreaType.国内长线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        /// <summary>
        /// 删除国内长线和省份的关系
        /// </summary>
        /// <param name="ProvinceId">省份ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteLongAreaSiteControl(int ProvinceId, int[] AreaIds)
        {
            if (ProvinceId <= 0 || AreaIds == null || AreaIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AreaIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            bool Result = dal.DeleteSysAreaSiteControl(ProvinceId, -1, strIds, Model.SystemStructure.AreaType.国内长线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteShortAreaSiteControl(int CityId)
        {
            if (CityId <= 0)
                return false;

            bool Result = dal.DeleteSysAreaSiteControl(-1, CityId, null, Model.SystemStructure.AreaType.国内短线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteShortAreaSiteControl(int CityId, int AreaId)
        {
            if (CityId <= 0 || AreaId <= 0)
                return false;

            bool Result = dal.DeleteSysAreaSiteControl(-1, CityId, AreaId.ToString(), Model.SystemStructure.AreaType.国内短线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        /// <summary>
        /// 删除国内短线和城市的关系
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <param name="AreaIds">线路区域ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteShortAreaSiteControl(int CityId, int[] AreaIds)
        {
            if (CityId <= 0 || AreaIds == null || AreaIds.Length <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int i in AreaIds)
            {
                strIds += i.ToString() + ",";
            }
            strIds = strIds.TrimEnd(',');

            bool Result = dal.DeleteSysAreaSiteControl(-1, CityId, strIds, Model.SystemStructure.AreaType.国内短线);
            if (Result)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemAreaCatalog);

            return Result;
        }

        #endregion
    }
}
