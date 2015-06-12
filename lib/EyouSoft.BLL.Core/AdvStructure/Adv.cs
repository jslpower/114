using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.AdvStructure
{
    /// <summary>
    /// 广告业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-07-15
    public class Adv:EyouSoft.IBLL.AdvStructure.IAdv
    {
        private readonly EyouSoft.IDAL.AdvStructure.IAdv dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.AdvStructure.IAdv>();

        #region CreateInstance
        /// <summary>
        /// 创建广告业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.AdvStructure.IAdv CreateInstance()
        {
            EyouSoft.IBLL.AdvStructure.IAdv op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.AdvStructure.IAdv>();
            }
            return op;
        }
        #endregion

        #region private members
        /// <summary>
        /// 获取广告投放范围集合
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <param name="range">广告投放范围</param>
        /// <returns></returns>
        private IList<int> GetAdvRelations(int advId, EyouSoft.Model.AdvStructure.AdvRange range)
        {
            IList<int> relations = new List<int>();
            switch (range)
            {
                case EyouSoft.Model.AdvStructure.AdvRange.全国:
                    relations = null; 
                    break;
                case EyouSoft.Model.AdvStructure.AdvRange.全省:
                    relations = dal.GetAdvRelation(advId);

                    IList<EyouSoft.Model.SystemStructure.CityBase> citys = new List<EyouSoft.Model.SystemStructure.CityBase>();
                    foreach (int cityId in relations)
                    {
                        EyouSoft.Model.SystemStructure.CityBase tmp = new EyouSoft.Model.SystemStructure.CityBase();
                        tmp.CityId = cityId;

                        citys.Add(tmp);
                    }

                    citys = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(citys);

                    relations = new List<int>();

                    if (citys != null && citys.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.CityBase city in citys)
                        {
                            if (!relations.Contains(city.ProvinceId))
                            {
                                relations.Add(city.ProvinceId);
                            }
                        }
                    }
                    break;
                default:
                    relations = dal.GetAdvRelation(advId);
                    break;
            }

            return relations;
        }
        #endregion private members

        #region 成员方法
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        public int InsertAdv(EyouSoft.Model.AdvStructure.AdvInfo info)
        {
            if (info == null
                || info.EndDate == DateTime.MinValue
                || info.StartDate == DateTime.MinValue)
            {
                return 0;
            }

            if (info.EndDate != DateTime.MaxValue)
            {
                info.EndDate = info.EndDate.AddDays(1).AddSeconds(-1);
            }

            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, info.Position.ToString());
            EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, DateTime.Now);

            return dal.InsertAdv(info);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="info">广告信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        public int UpdateAdv(EyouSoft.Model.AdvStructure.AdvInfo info)
        {
            if (info == null
                || info.EndDate == DateTime.MinValue
                || info.StartDate == DateTime.MinValue
                || info.AdvId == 0)
            {
                return 0;
            }

            if (info.EndDate != DateTime.MaxValue)
            {
                info.EndDate = info.EndDate.AddDays(1).AddSeconds(-1);
            }

            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, info.Position.ToString());
            EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, DateTime.Now);

            return dal.UpdateAdv(info);
        }
        /// <summary>
        /// 更新单位广告链接
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="AdvLink">广告链接</param>
        /// <returns>1:成功 0:失败</returns>
        public int UpdateAdv(string CompanyId, string AdvLink)
        {
            return dal.UpdateAdv(CompanyId, AdvLink);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public bool DeleteAdv(int advId)
        {
            EyouSoft.Model.AdvStructure.AdvInfo adv = GetAdvInfo(advId);
            if (adv != null)
            {
                string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, adv.Position.ToString());
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, DateTime.Now);
                return dal.DeleteAdv(advId);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否有效(添加时用)
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="range">投放范围</param>
        /// <param name="relation">关联信息(城市或单位类型编号)集合</param>
        /// <returns></returns>
        public bool IsValid(EyouSoft.Model.AdvStructure.AdvPosition position, DateTime startDate, DateTime endDate
            , EyouSoft.Model.AdvStructure.AdvRange range, IList<int> relation)
        {
            return this.IsValid(position, startDate, endDate, range, relation, 0);
        }

        /// <summary>
        /// 是否有效(修改时用)
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="range">投放范围</param>
        /// <param name="relation">关联信息(城市或单位类型编号)集合</param>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public bool IsValid(EyouSoft.Model.AdvStructure.AdvPosition position, DateTime startDate, DateTime endDate
            , EyouSoft.Model.AdvStructure.AdvRange range, IList<int> relation, int advId)
        {
            EyouSoft.Model.AdvStructure.AdvInfo info = new EyouSoft.Model.AdvStructure.AdvInfo();

            info.Position = position;
            info.StartDate = startDate;
            info.EndDate = endDate;

            if (info.EndDate != DateTime.MaxValue)
            {
                info.EndDate = info.EndDate.AddDays(1).AddSeconds(-1);
            }

            info.Range = range;
            info.Relation = relation;
            info.AdvId = advId;

            return dal.IsValid(info);
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public EyouSoft.Model.AdvStructure.AdvInfo GetAdvInfo(int advId)
        {
            EyouSoft.Model.AdvStructure.AdvInfo info = dal.GetAdvInfo(advId);

            if (info != null)
            {
                info.Relation = this.GetAdvRelations(info.AdvId, info.Range);
            }

            return info;
        }

        /// <summary>
        /// 设置广告排序
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <param name="position">广告位置</param>
        /// <param name="relationId">关联编号</param>
        /// <param name="sortId">排序编号</param>
        /// <returns></returns>
        public bool SetAdvSort(int advId, EyouSoft.Model.AdvStructure.AdvPosition position, int relationId, int sortId)
        {
            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, position.ToString());
            EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, DateTime.Now);

            return dal.SetAdvSort(advId, this.GetPositionInfo(position).AdvType, relationId, sortId);
        }

        /*/// <summary>
        /// 获取广告关系集合
        /// </summary>
        /// <param name="advId">广告编号</param>
        /// <returns></returns>
        public IList<int> GetAdvRelation(int advId)
        {
            return dal.GetAdvRelation(advId);
        }*/

        /// <summary>
        /// 获取广告位置信息
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public EyouSoft.Model.AdvStructure.AdvPositionInfo GetPositionInfo(EyouSoft.Model.AdvStructure.AdvPosition position)
        {            
            return dal.GetPositionInfo(position);
        }

        /// <summary>
        /// 按照指定条件获取广告信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="position">广告位置</param>
        /// <param name="relationId">关联编号(城市或单位类型编号)</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="category">广告类别 为null时不做为查询条件</param>
        /// <param name="startDate">有效期起始时间 为null时不做为查询条件</param>
        /// <param name="endDate">有效期截止时间 为null时不做为查询条件</param>
        /// <param name="title">广告标题 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.AdvStructure.AdvPosition position
            , int relationId, string companyName, EyouSoft.Model.AdvStructure.AdvCategory? category, DateTime? startDate, DateTime? endDate,string title)
        {
            if (endDate.HasValue) { endDate = endDate.Value.AddDays(1); }

            IList<EyouSoft.Model.AdvStructure.AdvInfo> advs=dal.GetAdvs(pageSize, pageIndex, ref recordCount, position, this.GetPositionInfo(position).AdvType
                , relationId, companyName, category, startDate, endDate, null, title);

            if (advs != null && advs.Count > 0)
            {
                foreach (EyouSoft.Model.AdvStructure.AdvInfo tmp in advs)
                {
                    tmp.Relation = this.GetAdvRelations(tmp.AdvId, tmp.Range);
                }
            }

            return advs;
        }

        /// <summary>
        /// 获取快到期广告信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="advType">广告投放类型</param>
        /// <param name="relationId">关联编号(城市或单位类型编号)</param>
        /// <param name="catelog">栏目编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.AdvInfo> GetComingExpireAdvs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.AdvStructure.AdvType advType
            , int relationId, EyouSoft.Model.AdvStructure.AdvCatalog? catelog, string companyName)
        {
            DateTime? startDate = DateTime.Today;
            DateTime? endDate = startDate.Value.AddMonths(1);
            return dal.GetAdvs(pageSize, pageIndex, ref recordCount, null, advType,
                relationId, companyName, null, startDate, endDate, catelog, null);
        }

        /// <summary>
        /// 获取指定位置的广告信息集合
        /// </summary>
        /// <param name="relationId">城市或单位类型(MQ)编号</param>
        /// <param name="position">广告位置</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.AdvInfo> GetAdvs(int relationId, EyouSoft.Model.AdvStructure.AdvPosition position)
        {
            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdv, position.ToString(), relationId);

            EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>> list = (EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>>)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);

            object UpdateTime = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, position.ToString()));

            if (UpdateTime == null)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, position.ToString()), DateTime.Now);
            }
            if (list != null && UpdateTime != null && list.UpdateTime > (DateTime)UpdateTime)
            {
                return list.Data;
            }
            else
            {
                list = new EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>>();

                EyouSoft.Model.AdvStructure.AdvPositionInfo positionInfo = this.GetPositionInfo(position);

                IList<EyouSoft.Model.AdvStructure.AdvInfo> advs = dal.GetAdvs(positionInfo.AdvType, relationId, position, DateTime.Today, positionInfo.AdvCount,false);

                int appendItems = positionInfo.AdvCount;

                #region 补平台广告
                if (advs != null && advs.Count > 0)
                {
                    appendItems = positionInfo.AdvCount - advs.Count;
                }
                else
                {
                    advs = new List<EyouSoft.Model.AdvStructure.AdvInfo>();
                }

                if (appendItems > 0)
                {
                    IList<EyouSoft.Model.AdvStructure.AdvInfo> platformAdvs = dal.GetAdvs(positionInfo.AdvType, relationId, position, DateTime.Today, appendItems, true);

                    if (platformAdvs != null && platformAdvs.Count > 0)
                    {
                        //advs = advs.Union(platformAdvs).ToList();
                        foreach (EyouSoft.Model.AdvStructure.AdvInfo advInfo in platformAdvs)
                            advs.Add(advInfo);
                    }
                }
                #endregion

                #region 补空白广告
                if (advs != null && advs.Count > 0)
                {
                    appendItems = positionInfo.AdvCount - advs.Count;
                }
                else
                {
                    advs = new List<EyouSoft.Model.AdvStructure.AdvInfo>();
                }


                EyouSoft.Model.AdvStructure.AdvInfo tmp = new EyouSoft.Model.AdvStructure.AdvInfo();
                tmp.Title = positionInfo.DefaultTitle;
                tmp.ImgPath = positionInfo.DefaultImgPath;
                tmp.RedirectURL = positionInfo.DefaultRedirectURL;

                for (int i = 0; i < appendItems; i++)
                {
                    advs.Add(tmp);
                }

                tmp = null;
                #endregion

                list.Data = advs.ToList();
                list.UpdateTime = DateTime.Now;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, list, DateTime.Today.AddDays(1).AddMinutes(10));
            }

            return list.Data;
        }
        /// <summary>
        /// 获取指定位置的广告信息集合
        /// </summary>
        /// <param name="relationId">城市或单位类型(MQ)编号</param>
        /// <param name="position">广告位置</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.AdvInfo> GetNotFillAdvs(int relationId, EyouSoft.Model.AdvStructure.AdvPosition position)
        {
            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdv, position.ToString(), relationId);

            EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>> list = (EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>>)
                EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);

            object UpdateTime = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, position.ToString()));

            if (UpdateTime == null)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Add(string.Format(EyouSoft.CacheTag.Adv.SystemAdvUpdateKey, position.ToString()), DateTime.Now);
            }
            if (list != null && UpdateTime != null && list.UpdateTime > (DateTime)UpdateTime)
            {
                return list.Data;
            }
            else
            {
                list = new EyouSoft.Cache.Facade.EyouSoftCacheTime<List<EyouSoft.Model.AdvStructure.AdvInfo>>();

                EyouSoft.Model.AdvStructure.AdvPositionInfo positionInfo = this.GetPositionInfo(position);

                IList<EyouSoft.Model.AdvStructure.AdvInfo> advs = dal.GetAdvs(positionInfo.AdvType, relationId, position, DateTime.Today, positionInfo.AdvCount, false);

                int appendItems = positionInfo.AdvCount;

                #region 补平台广告
                if (advs != null && advs.Count > 0)
                {
                    appendItems = positionInfo.AdvCount - advs.Count;
                }
                else
                {
                    advs = new List<EyouSoft.Model.AdvStructure.AdvInfo>();
                }

                if (appendItems > 0)
                {
                    IList<EyouSoft.Model.AdvStructure.AdvInfo> platformAdvs = dal.GetAdvs(positionInfo.AdvType, relationId, position, DateTime.Today, appendItems, true);

                    if (platformAdvs != null && platformAdvs.Count > 0)
                    {
                        //advs = advs.Union(platformAdvs).ToList();
                        foreach (EyouSoft.Model.AdvStructure.AdvInfo advInfo in platformAdvs)
                            advs.Add(advInfo);
                    }
                }
                #endregion

                list.Data = advs.ToList();
                list.UpdateTime = DateTime.Now;

                EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, list, DateTime.Today.AddDays(1).AddMinutes(10));
            }

            return list.Data;
        }
        /// <summary>
        /// 获取广告位置信息集合
        /// </summary>
        /// <param name="catalog">广告栏目</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> GetPositions(EyouSoft.Model.AdvStructure.AdvCatalog catalog)
        {
            string cachename = string.Format(EyouSoft.CacheTag.Adv.SystemAdvPostion, catalog.ToString());
            IList<EyouSoft.Model.AdvStructure.AdvPositionInfo> positions = (IList<EyouSoft.Model.AdvStructure.AdvPositionInfo>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);
            if (positions != null)
            {
                return positions;
            }
            else
            {
                positions = dal.GetPositions(catalog);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, positions);
                return positions;
            }
        }
        #endregion
    }
}
