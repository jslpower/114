//Author:汪奇志 2010-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.PoolStructure
{
    /// <summary>
    /// 易诺用户池公司信息业务逻辑类接口
    /// </summary>
    public interface ICompany
    {
        /// <summary>
        /// 创建用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        bool Create(EyouSoft.Model.PoolStructure.CompanyInfo info);
        /// <summary>
        /// 更新用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        bool Update(EyouSoft.Model.PoolStructure.CompanyInfo info);
        /// <summary>
        /// 删除用户池公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool Delete(string companyId);
        /// <summary>
        /// 获取公司信息集合，列表以公司信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.PoolStructure.CompanyInfo> GetCompanys(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo);
        /// <summary>
        /// 获取公司信息集合，列表以公司联系人信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.PoolStructure.ContacterInfo> GetCompanysItemByContacter(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo);
        /// <summary>
        /// 验证手机号码是否存在,添加时使用
        /// </summary>
        /// <param name="companyId">公司编号 为null时不做为查询条件</param>
        /// <param name="mobiles">手机号码</param>
        /// <returns>重复的号码信息集合</returns>
        IList<string> ExistsMobiles(string companyId, params string[] mobiles);
        /// <summary>
        /// 验证手机号码是否存在,添加修改都可使用
        /// </summary>
        /// <param name="companyId">公司编号 为null时不做为查询条件</param>
        /// <param name="mobiles">手机号码集合 添加时item.ContacterId设置0，修改时item.ContacterId设置相应的联系人编号</param>
        /// <returns>重复的号码信息集合</returns>
        IList<string> ExistsMobiles(string companyId, IList<EyouSoft.Model.PoolStructure.ContacterInfo> mobiles);
        /// <summary>
        /// 获取用户池公司信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.PoolStructure.CompanyInfo GetCompanyInfo(string companyId);
    }
}
