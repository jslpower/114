using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.BLL.ScenicStructure
{
    /// <summary>
    /// 景区主题
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class BScenicTheme:IBLL.ScenicStructure.IScenicTheme
    {
        private readonly IDAL.ScenicStructure.IScenicTheme dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.ScenicStructure.IScenicTheme>();

        public static IBLL.ScenicStructure.IScenicTheme CreateInstance()
        {
            IBLL.ScenicStructure.IScenicTheme op = null;
            if (op == null)
            {
                op = Component.Factory.ComponentFactory.Create<IBLL.ScenicStructure.IScenicTheme>();
            }
            return op;
        }

        #region IScenicTheme 成员
        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(MScenicTheme item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Add(item);
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.ScenicTheme);
            }
            return result;
        }
        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(MScenicTheme item)
        {
            bool result = false;
            if (item != null)
            {
                result = dal.Update(item);
                if (result)
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.ScenicTheme);
                }
            }
            return result;
        }
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public bool Delete(int themeId)
        {
            bool result = dal.Delete(themeId);
            if (result)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.ScenicTheme);
            }
            return result;
        }
        /// <summary>
        /// 获取主题
        /// </summary>
        /// <returns></returns>
        public IList<MScenicTheme> GetList()
        {
            IList<MScenicTheme> list = null;
            object objCache = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.ScenicTheme);

            if (objCache != null)
            {
                list = (IList<MScenicTheme>)objCache;
            }
            else
            {
                list = dal.GetList();
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.ScenicTheme, list);
            }
            return list;
        }

        #endregion
    }
}
