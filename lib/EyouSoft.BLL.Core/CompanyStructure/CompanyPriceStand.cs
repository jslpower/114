using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 公司价格等级业务逻辑层
    /// </summary>
    /// 张志瑜 2010-06-03
    public class CompanyPriceStand : EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyPriceStand idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyPriceStand>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyPriceStand>();
            }
            return op;
        }

        /// <summary>
        /// 获取指定公司的价格等级列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> GetList(string companyId)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> list = (IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand>)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(
                EyouSoft.CacheTag.Company.CompanyPriceStand + companyId);
            if (list == null)
            {
                list = idal.GetList(companyId);
                EyouSoft.Cache.Facade.EyouSoftCache.Add(
                EyouSoft.CacheTag.Company.CompanyPriceStand + companyId, list, DateTime.Now.AddHours(1));
            }
            return list;
        }
        /// <summary>
        /// 设置公司价格等级信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="items">公用价格等级(系统基础数据)[设置公共价格等级ID和等级名称即可]信息业务实体集合</param>
        /// <returns></returns>
        public bool SetCompanyPriceStand(string companyId, IList<EyouSoft.Model.CompanyStructure.CommonPriceStand> items)
        {
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(
                EyouSoft.CacheTag.Company.CompanyPriceStand + companyId);
            return idal.SetCompanyPriceStand(companyId, items);
        }
        /// <summary>
        /// 判断指定公司,指定公共的价格等级ID有无在(团队/线路价格等级)使用(true:在使用  false:未使用)
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="commonPriceStandId">公共价格等级ID</param>
        /// <returns></returns>
        public bool IsUsing(string companyId, string commonPriceStandId)
        {
            return idal.IsUsing(companyId, commonPriceStandId);
        }
    }
}
