using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 客户域名 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SysCompanyDomain : IBLL.SystemStructure.ISysCompanyDomain
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysCompanyDomain() { }

        private readonly IDAL.SystemStructure.ISysCompanyDomain dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysCompanyDomain>();

        /// <summary>
        /// 构造客户域名业务逻辑接口
        /// </summary>
        /// <returns>客户域名业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysCompanyDomain CreateInstance()
        {
            IBLL.SystemStructure.ISysCompanyDomain op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysCompanyDomain>();
            }
            return op;
        }



        #region ISysCompanyDomain 成员

        /// <summary>
        /// 域名是否存在
        /// </summary>
        /// <param name="DomainName">域名</param>
        /// <returns></returns>
        public bool IsExist(string DomainName)
        {
            return dal.IsExist(DomainName);
        }

        /// <summary>
        /// 新增客户域名
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model)
        {
            if (model == null)
                return 0;

            int Result = dal.AddSysCompanyDomain(model);
            if (Result > 0)
            {
                RemoveCache();
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 修改客户域名(只更新公司类型，公司名称，域名，跳转地址)
        /// </summary>
        /// <param name="model">客户域名实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int UpdateSysCompanyDomain(EyouSoft.Model.SystemStructure.SysCompanyDomain model)
        {
            if (model == null)
                return 0;

            int Result = dal.UpdateSysCompanyDomain(model);
            if (Result > 0)
            {
                RemoveCache();
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="DomainId">域名编号</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回操作是否成功</returns>
        public bool SetDisabled(int DomainId, bool IsDisabled)
        {
            int Result = dal.SetDisabled(DomainId, IsDisabled);
            if (Result > 0)
            {
                RemoveCache();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 设置域名状态
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="IsDisabled">状态</param>
        /// <returns>返回受影响的行数</returns>
        public bool SetDisabled(string CompanyId, DomainType domainType, bool IsDisabled)
        {
            int Result = dal.SetDisabled(CompanyId, domainType, IsDisabled);
            if (Result > 0)
            {
                RemoveCache();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 删除客户域名
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysCompanyDomain(int SysCompanyDomainId)
        {
            if (SysCompanyDomainId <= 0)
                return false;

            bool Result = dal.DeleteSysCompanyDomain(SysCompanyDomainId);
            if (Result)
                RemoveCache();
            return Result;
        }

        /// <summary>
        /// 根据公司ID删除客户域名
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysCompanyDomain(string CompanyId)
        {
            if (string.IsNullOrEmpty(CompanyId))
                return false;

            bool Result = dal.DeleteSysCompanyDomain(CompanyId);
            if (Result)
                RemoveCache();
            return Result;
        }

        /// <summary>
        /// 根据ID获取客户域名信息
        /// </summary>
        /// <param name="SysCompanyDomainId">客户域名ID</param>
        /// <returns>返回客户域名实体</returns>
        public EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(int SysCompanyDomainId)
        {
            if (SysCompanyDomainId <= 0)
                return null;

            return dal.GetSysCompanyDomain(SysCompanyDomainId);
        }

        /// <summary>
        /// 根据域名获取客户域名信息
        /// </summary>
        /// <param name="domainType">域名类型</param>
        /// <param name="DomainName">域名</param>
        /// <returns>返回客户域名实体</returns>
        public EyouSoft.Model.SystemStructure.SysCompanyDomain GetSysCompanyDomain(DomainType domainType, string DomainName)
        {
            string cachename= EyouSoft.CacheTag.System.SystemDomain + DomainName.ToLower();
            if (string.IsNullOrEmpty(DomainName))
                return null;
            EyouSoft.Model.SystemStructure.SysCompanyDomain model = (EyouSoft.Model.SystemStructure.SysCompanyDomain)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cachename);
            if (model == null)
            {
                model = dal.GetSysCompanyDomain(domainType, DomainName);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(cachename, model, DateTime.Today.AddDays(1));
            }
            return model;
        }

        /// <summary>
        /// 分页获取客户域名信息集合
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0为时间升序，1为时间降序</param>
        /// <returns>返回客户域名实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex)
        {
            return dal.GetDomainList(PageSize, PageIndex, ref RecordCount, OrderIndex);
        }

        /// <summary>
        /// 根据公司ID获取客户域名实体集合
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns>客户域名实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetDomainList(params string[] CompanyId)
        {
            if (CompanyId == null && CompanyId.Length < 0)
                return null;

            return GetAllList().Where(item => CompanyId.Contains(item.CompanyId)).ToList();
        }

        private IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> GetAllList()
        {
            IList<EyouSoft.Model.SystemStructure.SysCompanyDomain> list = null;
            object cachelist = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.System.SystemDomain);
            if (cachelist != null)
            {
                list = (IList<EyouSoft.Model.SystemStructure.SysCompanyDomain>)cachelist;
            }
            else
            {
                list = dal.GetDomainList("");
                EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SystemDomain, list, DateTime.Now.AddDays(1));
            }
            return list;
        }

        /// <summary>
        /// 更新域名跳转URL
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="domainType">域名类型</param>
        /// <param name="gotoUrl">跳转URL</param>
        /// <returns></returns>
        public bool UpdateGotoUrl(string companyId, DomainType domainType, string gotoUrl)
        {
            bool result = dal.UpdateGotoUrl(companyId, domainType, gotoUrl);

            if(result)
                RemoveCache();

            return result;
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 移除客户域名缓存
        /// </summary>
        private void RemoveCache()
        {
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.System.SystemDomain);
        }

        #endregion
    }
}
