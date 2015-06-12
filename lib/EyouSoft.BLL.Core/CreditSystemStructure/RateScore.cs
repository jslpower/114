using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;


namespace EyouSoft.BLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-公司的信用分值与次数汇总信息业务逻辑类
    /// </summary>
    public class RateScore : EyouSoft.IBLL.CreditSystemStructure.IRateScore
    {
        private readonly EyouSoft.IDAL.CreditSystemStructure.IRateScore idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CreditSystemStructure.IRateScore>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CreditSystemStructure.IRateScore CreateInstance()
        {
            EyouSoft.IBLL.CreditSystemStructure.IRateScore op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CreditSystemStructure.IRateScore>();
            }
            return op;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public RateScore() { }

        /// <summary>
        /// 获取指定公司的信用分值与次数汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.CreditSystemStructure.RateDetailTotal GetCollectsInfo(string companyId)
        {
            string cacheName = EyouSoft.CacheTag.Company.RateScore + companyId;
            EyouSoft.Model.CreditSystemStructure.RateDetailTotal info = (EyouSoft.Model.CreditSystemStructure.RateDetailTotal)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheName);
            if (info == null)  //缓存中不存在
            {
                info = idal.GetCollectsInfo(companyId, EyouSoft.BLL.CreditSystemStructure.RateConfig.CreateInstance().GetRateConfig().CertificateScore);
                if (info != null)
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheName, info, DateTime.Now.AddDays((double)1));  //存入缓存
            }

            return info;
        }

        /// <summary>
        /// 移除公司诚信分值缓存
        /// </summary>
        /// <param name="companyId">公司ID</param>
        public void RemoveCache(string companyId)
        {
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(EyouSoft.CacheTag.Company.RateScore + companyId);
        }
    }
}
