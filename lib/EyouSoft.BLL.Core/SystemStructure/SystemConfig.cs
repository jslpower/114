using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 系统设置 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SystemConfig : IBLL.SystemStructure.ISystemConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemConfig() { }

        private readonly IDAL.SystemStructure.ISystemConfig dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISystemConfig>();

        /// <summary>
        /// 构造系统设置业务逻辑接口
        /// </summary>
        /// <returns>系统设置业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISystemConfig CreateInstance()
        {
            IBLL.SystemStructure.ISystemConfig op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISystemConfig>();
            }
            return op;
        }



        #region ISystemConfig 成员

        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="model">系统设置实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateSystemConfig(EyouSoft.Model.SystemStructure.SystemConfig model)
        {
            if (model == null)
                return 0;

            int Result = 0;
            Result =  dal.UpdateSystemConfig(model);
            if (Result > 0)
            {
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemSetting);

                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 获取系统设置实体
        /// </summary>
        /// <returns>返回系统设置实体</returns>
        public Model.SystemStructure.SystemConfig GetSystemConfig()
        {
            Model.SystemStructure.SystemConfig model = null;

            object CacheModel = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemSetting);
            if (CacheModel != null)
                model = (Model.SystemStructure.SystemConfig)CacheModel;
            else
                model = dal.GetSystemConfig();

            return model;
        }

        #endregion
    }
}
