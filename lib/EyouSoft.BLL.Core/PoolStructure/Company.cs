//Author:汪奇志 2010-11-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.PoolStructure
{
    /// <summary>
    /// 易诺用户池公司信息业务逻辑类接口
    /// </summary>
    public class Company:EyouSoft.IBLL.PoolStructure.ICompany
    {
        private readonly EyouSoft.IDAL.PoolStructure.ICompany dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.PoolStructure.ICompany>();

        #region CreateInstance
        /// <summary>
        /// 创建易诺用户池公司信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.PoolStructure.ICompany CreateInstance()
        {
            EyouSoft.IBLL.PoolStructure.ICompany op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.PoolStructure.ICompany>();
            }
            return op;
        }
        #endregion        

        #region ICompany 成员
        /// <summary>
        /// 创建用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        public bool Create(EyouSoft.Model.PoolStructure.CompanyInfo info)
        {
            info.CompanyId = Guid.NewGuid().ToString();

            return dal.Create(info);
        }
        /// <summary>
        /// 更新用户池公司信息
        /// </summary>
        /// <param name="info">用户池公司信息业务实体</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.PoolStructure.CompanyInfo info)
        {
            if (string.IsNullOrEmpty(info.CompanyId)) return false;

            return dal.Update(info);
        }
        /// <summary>
        /// 删除用户池公司信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool Delete(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return false;

            return dal.Delete(companyId, true);
        }
        /// <summary>
        /// 获取公司信息集合，列表以公司信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.PoolStructure.CompanyInfo> GetCompanys(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo)
        {
            return dal.GetCompanys(pageSize, pageIndex, ref recordCount, searchInfo);
        }
        /// <summary>
        /// 获取公司信息集合，列表以公司联系人信息为行集
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.PoolStructure.ContacterInfo> GetCompanysItemByContacter(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.PoolStructure.CompanySearchInfo searchInfo)
        {
            return dal.GetCompanysItemByContacter(pageSize, pageIndex, ref recordCount, searchInfo);
        }
        /// <summary>
        /// 验证手机号码是否存在
        /// </summary>
        /// <param name="companyId">公司编号 为null时不做为查询条件</param>
        /// <param name="mobiles">手机号码</param>
        /// <returns>重复的号码信息集合</returns>
        public IList<string> ExistsMobiles(string companyId, params string[] mobiles)
        {
            IList<EyouSoft.Model.PoolStructure.ContacterInfo> tmpMobiles = new List<EyouSoft.Model.PoolStructure.ContacterInfo>();

            if (mobiles != null && mobiles.Length > 0)
            {
                foreach (string mobile in mobiles)
                {
                    tmpMobiles.Add(new EyouSoft.Model.PoolStructure.ContacterInfo() { ContacterId = 0, Mobile = mobile });
                }
            }

            return this.ExistsMobiles(companyId, tmpMobiles);
        }

        /// <summary>
        /// 验证手机号码是否存在,添加修改都可使用
        /// </summary>
        /// <param name="companyId">公司编号 为null时不做为查询条件</param>
        /// <param name="mobiles">手机号码集合 添加时item.ContacterId设置0，修改时item.ContacterId设置相应的联系人编号</param>
        /// <returns>重复的号码信息集合</returns>
        public IList<string> ExistsMobiles(string companyId, IList<EyouSoft.Model.PoolStructure.ContacterInfo> mobiles)
        {
            IList<string> existsMobiles = new List<string>();
            
            if (mobiles != null && mobiles.Count > 0)
            {
                var iEnumerable = mobiles.GroupBy(i => i.Mobile);

                foreach (var iGrouping in iEnumerable)
                {
                    if (iGrouping.Count() > 1) existsMobiles.Add(iGrouping.Key);
                }
            }

            if (existsMobiles.Count > 0) return existsMobiles;           

            return dal.ExistsMobiles(companyId, mobiles);
        }

        /// <summary>
        /// 获取用户池公司信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.PoolStructure.CompanyInfo GetCompanyInfo(string companyId)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetCompanyInfo(companyId);
        }

        #endregion
    }
}
