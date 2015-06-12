using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 高级服务申请数据访问接口
    /// </summary>
    /// Author:汪奇志 2010-07-22
    public interface ISysApplyService
    {
        /// <summary>
        /// 高级服务申请
        /// </summary>
        /// <param name="info">高级服务申请信息业务实体</param>
        /// <returns></returns>
        bool Apply(EyouSoft.Model.SystemStructure.SysApplyServiceInfo info);

        /// <summary>
        /// 判断是否申请高级服务
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        bool IsApply(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType);

        /// <summary>
        /// 获取高级服务申请审核状态,返回null未申请
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.ApplyServiceState? GetApplyState(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType);

        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string applyId);

        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType);

        /// <summary>
        /// 获取高级服务申请信息集合
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
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType, int? provinceId, int? cityId, string companyName, DateTime? applyStartTime, DateTime? applyFinishTime
            , EyouSoft.Model.SystemStructure.ApplyServiceState? applyState
            ,string userAreas);

        /// <summary>
        /// 获取快到期服务信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <param name="expireStartTime">到期起始时间 为null时不做为查询条件</param>
        /// <param name="expireFinishTime">到期截止时间 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="searchStartTime">搜索起始时间 为null时不做为查询条件</param>
        /// <param name="searchFinishTime">搜索截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetComingExpireApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType, DateTime? expireStartTime, DateTime? expireFinishTime
            , string userAreas
            , int? provinceId, int? cityId, string companyName
            , DateTime? searchStartTime, DateTime? searchFinishTime);

        /// <summary>
        /// 高级网店审核
        /// </summary>
        /// <param name="info">高级网店申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败 2:域名重复</returns>
        int EshopChecked(EyouSoft.Model.SystemStructure.EshopCheckInfo info);

        /// <summary>
        /// 收费MQ审核
        /// </summary>
        /// <param name="info">收费MQ申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        int MQChecked(EyouSoft.Model.SystemStructure.MQCheckInfo info);

        /// <summary>
        /// 高级服务续费
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <param name="enableTime">续费起始时间</param>
        /// <param name="expireTime">续费到期时间</param>
        /// <param name="operatorId">续费人编号</param>
        /// <param name="renewTime">续费时间</param>
        /// <param name="subAccountNumber">MQ续费时子账号数量 高级网店续费时设置0</param>
        /// <returns>1:成功 0:失败 2:续费时间不对</returns>
        int Renewed(string applyId, DateTime enableTime, DateTime expireTime, int operatorId, DateTime renewTime, int subAccountNumber);

        /// <summary>
        /// 获取高级服务续费信息集合
        /// </summary>
        /// <param name="applyId">高级服务申请编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.SystemStructure.ServiceRenewInfo> GetServiceReneweds(string applyId);
    }
}
