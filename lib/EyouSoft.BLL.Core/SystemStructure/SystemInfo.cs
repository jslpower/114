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
    /// 系统信息 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SystemInfo : IBLL.SystemStructure.ISystemInfo
    {
        private readonly IDAL.SystemStructure.ISystemInfo dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISystemInfo>();
        private readonly IDAL.SystemStructure.ISysAreaContact dalAreaContact = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysAreaContact>();

        /// <summary>
        /// 构造系统信息业务逻辑接口
        /// </summary>
        /// <returns>系统信息业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISystemInfo CreateInstance()
        {
            IBLL.SystemStructure.ISystemInfo op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISystemInfo>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemInfo()
        {

        }


        #region ISystemInfo 成员

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="model">系统信息实体(区域联系人实体集合为null或者count小于等于0时不修改区域联系人)</param>
        /// <returns>-3:添加区域联系人失败;-2:删除区域联系人失败;-1:修改系统信息失败;0:系统实体为空;1:Success</returns>
        public int UpdateSystemInfo(EyouSoft.Model.SystemStructure.SystemInfo model)
        {
            if (model == null)
                return 0;

            int Result = 0;
            using (TransactionScope UpdateTran = new TransactionScope())
            {
                Result = dal.UpdateSystemInfo(model);
                if (Result <= 0)
                    return -1;

                dalAreaContact.DeleteSysAreaContact();
                //if (Result <= 0)
                //return -2;

                Result = dalAreaContact.AddSysAreaContact(model.SysAreaContact);
                //if (Result <= 0)
                //    return -3;

                Result = 1;
                UpdateTran.Complete();
            }
            if (Result == 1)
                //清空缓存
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemInfo);

            return Result;
        }

        /// <summary>
        /// 根据系统ID获取系统信息(同时获取所有的区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        public EyouSoft.Model.SystemStructure.SystemInfo GetSystemInfoModel()
        {
            EyouSoft.Model.SystemStructure.SystemInfo model = GetSystemModel();
            model.SysAreaContact = dalAreaContact.GetAreaContactList();

            return model;
        }

        /// <summary>
        /// 根据系统ID获取系统信息(不含区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        public EyouSoft.Model.SystemStructure.SystemInfo GetSystemModel()
        {
            EyouSoft.Model.SystemStructure.SystemInfo model = new EyouSoft.Model.SystemStructure.SystemInfo();
            object CacheModel = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemInfo);
            if (CacheModel == null)
            {
                model = dal.GetSystemModel();
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemInfo, model);
            }
            else
                model = (EyouSoft.Model.SystemStructure.SystemInfo)CacheModel;

            return model;
        }

        /// <summary>
        /// 设置平台配置，返回1成功，其它失败
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <returns></returns>
        public int SetSysSettings(EyouSoft.Model.SystemStructure.MSysSettingInfo info)
        {
            if (info == null)
            {
                throw new System.Exception("平台配置业务实体is null。");
                //return -1;
            }

            if (info.OrderSmsIsEnable)
            {
                if (string.IsNullOrEmpty(info.OrderSmsCompanyId) 
                    || string.IsNullOrEmpty(info.OrderSmsUserId) 
                    || info.OrderSmsUserId.Length != 36 
                    || info.OrderSmsUserId.Length != 36)
                {
                    throw new System.Exception("订单短信控制-发送短信公司或用户编号填写错误1。");
                    //return -2;
                }

                if (!EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExistsUserId(info.OrderSmsCompanyId, info.OrderSmsUserId))
                {
                    throw new System.Exception("订单短信控制-发送短信公司或用户编号填写错误2。");
                    //return -3;
                }
                
            }

            int dalRetCode = dal.SetSysSettings(info, new string[] { });

            if (dalRetCode == 1)
            {
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SysSetting);

                return 1;
            }

            return dalRetCode;
        }
        /// <summary>
        /// 获取平台配置
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.MSysSettingInfo GetSysSetting()
        {
            EyouSoft.Model.SystemStructure.MSysSettingInfo info = (EyouSoft.Model.SystemStructure.MSysSettingInfo)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SysSetting);

            if (info != null) return info;

            info = dal.GetSysSetting();

            if (info == null)
            {
                info = new EyouSoft.Model.SystemStructure.MSysSettingInfo();
                info.OrderSmsIsEnable = false;
            }

            if (!EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExistsUserId(info.OrderSmsCompanyId, info.OrderSmsUserId))
            {
                info.OrderSmsIsEnable = false;
            }

            EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SysSetting, info);

            return info;
        }
        #endregion
    }
}
