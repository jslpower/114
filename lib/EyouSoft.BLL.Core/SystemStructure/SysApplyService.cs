using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 高级服务申请业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-07-23
    public class SysApplyService:EyouSoft.IBLL.SystemStructure.ISysApplyService
    {
        private readonly EyouSoft.IDAL.SystemStructure.ISysApplyService dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SystemStructure.ISysApplyService>();

        #region CreateInstance
        /// <summary>
        /// 高级服务申请业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.ISysApplyService CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.ISysApplyService op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.ISysApplyService>();
            }
            return op;
        }
        #endregion

        #region private member
        /// <summary>
        /// 获取当前用户(运营后台)用户分管的线路区域 多个线路区域用","间隔
        /// </summary>
        /// <returns></returns>
        private string GetWebMasterArea()
        {
            return new EyouSoft.Security.Membership.Utility().GetCurrentWebMasterArea();
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 高级服务申请
        /// </summary>
        /// <param name="info">高级服务申请信息业务实体</param>
        /// <returns></returns>
        public bool Apply(EyouSoft.Model.SystemStructure.SysApplyServiceInfo info)
        {
            if (!string.IsNullOrEmpty(info.CompanyId) && !this.IsApply(info.CompanyId, info.ApplyServiceType))
            {
                info.ApplyId = Guid.NewGuid().ToString();
                info.ApplyTime = DateTime.Now;
                return dal.Apply(info);
            }

            return false;
        }

        /// <summary>
        /// 判断是否申请高级服务
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public bool IsApply(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            return dal.IsApply(companyId, serviceType);
        }

        /// <summary>
        /// 获取高级服务申请审核状态,返回null未申请
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.ApplyServiceState? GetApplyState(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            return dal.GetApplyState(companyId, serviceType);
        }

        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string applyId)
        {
            return dal.GetApplyInfo(applyId);
        }
        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            return dal.GetApplyInfo(companyId, serviceType);
        }

        /// <summary>
        /// 按当前登录用户分管的区域范围获取高级服务申请信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="applyStartTime">申请起始时间 为null时不做为查询条件</param>
        /// <param name="applyFinishTime">申请截止时间 为null时不做为查询条件</param>
        /// <param name="applyState">审核状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType, int? provinceId, int? cityId, string companyName, DateTime? applyStartTime, DateTime? applyFinishTime
            , EyouSoft.Model.SystemStructure.ApplyServiceState? applyState)
        {
            return dal.GetApplys(pageSize, pageIndex, ref recordCount, serviceType
                , provinceId, cityId, companyName, applyStartTime, applyFinishTime
                , applyState, this.GetWebMasterArea());
        }

        /// <summary>
        /// 按当前登录用户分管的区域范围获取快到期(提前一个月)服务信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="searchStartTime">搜索起始时间 为null时不做为查询条件</param>
        /// <param name="searchFinishTime">搜索截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetComingExpireApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType
            , int? provinceId, int? cityId, string companyName
            , DateTime? searchStartTime, DateTime? searchFinishTime)
        {
            DateTime? expireStartTime = null;
            DateTime? expireFinishTime = null;

            if (!(searchStartTime.HasValue || searchFinishTime.HasValue))//未按时间进行查询时默认查询一个月内到期的
            {
                expireStartTime = DateTime.Today;
                expireFinishTime = expireStartTime.Value.AddMonths(1);
            }

            if (searchFinishTime.HasValue)
            {
                searchFinishTime = searchFinishTime.Value.AddDays(1);
            }

            return dal.GetComingExpireApplys(pageSize, pageIndex, ref recordCount
                , serviceType, expireStartTime, expireFinishTime
                , this.GetWebMasterArea(), provinceId, cityId, companyName, searchStartTime, searchFinishTime);
        }

        /// <summary>
        /// 高级网店审核
        /// </summary>
        /// <param name="info">高级网店申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败 2:域名重复 3:有效期限时间值不对</returns>
        public int EshopChecked(EyouSoft.Model.SystemStructure.EshopCheckInfo info)
        {
            if (string.IsNullOrEmpty(info.ApplyId) 
                || info.ApplyState == EyouSoft.Model.SystemStructure.ApplyServiceState.未审核)
            {
                return 0;
            }
            info.ExpireTime = info.ExpireTime.AddDays(1).AddSeconds(-1);

            if (info.EnableTime > info.ExpireTime
                || info.EnableTime == DateTime.MinValue
                || info.ExpireTime == DateTime.MinValue)
            {
                return 3;
            }

            EyouSoft.Model.SystemStructure.SysApplyServiceInfo apply = GetApplyInfo(info.ApplyId);
            if (apply != null)
            {
                if(!string.IsNullOrEmpty(info.DomainName))
                    EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemDomain + info.DomainName.ToLower());
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.CompanyState + apply.CompanyId);
            }

            return dal.EshopChecked(info);
        }

        /// <summary>
        /// 收费MQ审核
        /// </summary>
        /// <param name="info">收费MQ申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败 2:有效期限时间值不对</returns>
        public int MQChecked(EyouSoft.Model.SystemStructure.MQCheckInfo info)
        {
            if (string.IsNullOrEmpty(info.ApplyId)
                || info.ApplyState == EyouSoft.Model.SystemStructure.ApplyServiceState.未审核)
            {
                return 0;
            }

            info.ExpireTime = info.ExpireTime.AddDays(1).AddSeconds(-1);

            if (info.EnableTime > info.ExpireTime
                || info.EnableTime == DateTime.MinValue
                || info.ExpireTime == DateTime.MinValue)
            {
                return 2;
            }

            EyouSoft.Model.SystemStructure.SysApplyServiceInfo apply = GetApplyInfo(info.ApplyId);
            if (apply != null)
                EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.CompanyState + apply.CompanyId);

            return dal.MQChecked(info);
        }

        /// <summary>
        /// 高级网店服务续费
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <param name="enableTime">续费起始时间</param>
        /// <param name="expireTime">续费到期时间</param>
        /// <param name="operatorId">续费人编号</param>
        /// <param name="renewTime">续费时间</param>
        /// <returns>1:成功 0:失败 2:续费时间不对</returns>
        public int EshopRenewed(string applyId, DateTime enableTime, DateTime expireTime, int operatorId, DateTime renewTime)
        {
            return this.MQRenewed(applyId, enableTime, expireTime, operatorId, renewTime, 0);
        }

        /// <summary>
        /// MQ服务续费
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <param name="enableTime">续费起始时间</param>
        /// <param name="expireTime">续费到期时间</param>
        /// <param name="operatorId">续费人编号</param>
        /// <param name="renewTime">续费时间</param>
        /// <param name="subAccountNumber">子账号数量</param>
        /// <returns>1:成功 0:失败 2:续费时间不对</returns>
        public int MQRenewed(string applyId, DateTime enableTime, DateTime expireTime, int operatorId, DateTime renewTime, int subAccountNumber)
        {
            if (string.IsNullOrEmpty(applyId))
            {
                return 0;
            }

            expireTime = expireTime.AddDays(1).AddSeconds(-1);

            if (enableTime > expireTime
               || expireTime == DateTime.MinValue
               || expireTime == DateTime.MinValue)
            {
                return 2;
            }

            return dal.Renewed(applyId, enableTime, expireTime, operatorId, renewTime, subAccountNumber);
        }

        /// <summary>
        /// 获取高级服务续费信息集合
        /// </summary>
        /// <param name="applyId">高级服务申请编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.ServiceRenewInfo> GetServiceReneweds(string applyId)
        {
            return string.IsNullOrEmpty(applyId) ? null : dal.GetServiceReneweds(applyId);
        }
        #endregion
    }
}
