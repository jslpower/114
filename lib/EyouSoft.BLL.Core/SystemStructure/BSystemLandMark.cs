using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 城市地标
    /// 创建者：郑付杰
    /// 创建时间：2011-11-28
    /// </summary>
    public class BSystemLandMark:EyouSoft.IBLL.SystemStructure.ISystemLandMark
    {
        private readonly IDAL.SystemStructure.ISystemLandMark dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.SystemStructure.ISystemLandMark>();

        public static IBLL.SystemStructure.ISystemLandMark CreateInstance()
        {
            IBLL.SystemStructure.ISystemLandMark op = null;
            if (op == null)
            {
                op = Component.Factory.ComponentFactory.Create<IBLL.SystemStructure.ISystemLandMark>();
            }
            return op;
        }

        #region ISystemStructure 成员

        /// <summary>
        /// 获取所有地标
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public IList<MSystemLandMark> GetList()
        {
            IList<MSystemLandMark> list = null;

            object objCache = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemLankMarks);
            if (objCache != null)
            {
                list = (IList<MSystemLandMark>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemLankMarks);
            }
            else
            {
                list = dal.GetList();
                if (list != null && list.Count > 0)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemLankMarks, list);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取所有地标
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public IList<MSystemLandMark> GetList(int cityId)
        {
            IList<MSystemLandMark> list = null;
            object objCache = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemLankMarks);
            if (objCache == null)
            {
                list = GetList();
            }
            else
            {
                list = (IList<MSystemLandMark>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemLankMarks);
            }

            return (from c in list where c.CityId == cityId select c).ToList();
        }

        #endregion
    }
}
