using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 系统数据统计业务逻辑
    /// </summary>
    /// 创建人:蒋胜蓝  2010-6-23
    public class SummaryCount: ISummaryCount
    {
        private readonly IDAL.SystemStructure.ISummaryCount dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISummaryCount>();

        #region CreateInstance
        /// <summary>
        /// 创建系统数据统计业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.ISummaryCount CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.ISummaryCount op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.ISummaryCount>();
            }
            return op1;
        }
        #endregion

        #region ISummaryCount 成员
        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SummaryCount GetSummary()
        {
            EyouSoft.Model.SystemStructure.SummaryCount model = (EyouSoft.Model.SystemStructure.SummaryCount)EyouSoftCache.GetCache(CacheTag.System.SummaryCount);
            if (model == null)
            {
                model = dal.GetSummary();
                EyouSoftCache.Add(CacheTag.System.SummaryCount, model, DateTime.Today.AddDays(1).AddMinutes(10));
            }
            return model;
        }
         /// <summary>
        /// 更新统计信息基数值
        /// </summary>
        /// <param name="model"></param>
        /// <returns>false:失败 true:成功</returns>
        public  bool SetSummaryCount(EyouSoft.Model.SystemStructure.SummaryCount model)
        {
            if (model == null)
                return false;
            EyouSoftCache.Remove(CacheTag.System.SummaryCount);
            return dal.SetSummaryCount(model);
        }
        #endregion
    }
}
