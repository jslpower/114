using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 客户域名 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ISysCompanyDomain
    {
        /// <summary>
        /// 域名是否存在
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        bool IsExist(string DomainName);
        /// <summary>
        /// 新增客户域名
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>返回受影响的行数</returns>
        int AddSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model);

        /// <summary>
        /// 修改客户域名(只更新公司类型，公司名称，域名，跳转地址)
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>返回受影响的行数</returns>
        int UpdateSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model);

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="DomainId">域名编号</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回受影响的行数</returns>
        int SetDisabled(int DomainId, bool IsDisabled);

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回受影响的行数</returns>
        int SetDisabled(string CompanyId, DomainType domainType, bool IsDisabled);

        /// <summary>
        /// 删除客户域名
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysCompanyDomain(int SysCompanyDomainId);

        /// <summary>
        /// 根据公司ID删除客户域名
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysCompanyDomain(string CompanyId);

        /// <summary>
        /// 根据ID获取客户域名信息
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回客户域名实体</returns>
        EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(int SysCompanyDomainId);

        /// <summary>
        /// 根据域名获取客户域名信息
        /// </summary>
        /// <param name="domainType">域名类型</param>
        /// <param name="DomainName">域名</param>
        /// <returns>返回客户域名实体</returns>
        EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(DomainType domainType, string DomainName);

        /// <summary>
        /// 分页获取客户域名信息集合
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0为时间升序，1为时间降序</param>
        /// <returns>返回客户域名实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex);

        /// <summary>
        /// 根据公司ID获取客户域名实体集合
        /// </summary>
        /// <param name="CompanyId">公司ID集合</param>
        /// <returns>客户域名实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(string CompanyId);

        /// <summary>
        /// 更新域名跳转URL
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="gotoUrl">跳转URL</param>
        /// <returns></returns>
        bool UpdateGotoUrl(string companyId, DomainType domainType, string gotoUrl);
    }
}
