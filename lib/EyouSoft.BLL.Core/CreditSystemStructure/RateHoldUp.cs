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
    /// 描述:诚信体系-公司推荐业务逻辑类
    /// </summary>
    public class RateHoldUp : EyouSoft.IBLL.CreditSystemStructure.IRateHoldUp
    {
        private readonly EyouSoft.IDAL.CreditSystemStructure.IRateHoldUp idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CreditSystemStructure.IRateHoldUp>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CreditSystemStructure.IRateHoldUp CreateInstance()
        {
            EyouSoft.IBLL.CreditSystemStructure.IRateHoldUp op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CreditSystemStructure.IRateHoldUp>();
            }
            return op;
        }

        /// <summary>
        /// 是否已经推荐
        /// </summary>
        /// <param name="toCompanyId">推荐的公司编号</param>
        /// <param name="fromCompanyId">推荐人所在公司编号</param>
        /// <returns></returns>
        public bool IsHoldUp(string toCompanyId, string fromCompanyId)
        {
            return idal.IsHoldUp(toCompanyId, fromCompanyId);
        }

        /// <summary>
        /// 获取指定公司的推荐数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetAmount(string companyId)
        {
            return idal.GetAmount(companyId);
        }

        /// <summary>
        /// 获取指定公司的推荐列表
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CreditSystemStructure.RateHoldUp> GetHoldUps(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            return idal.GetHoldUps(pageSize, pageIndex, ref recordCount, companyId);
        }

        /// <summary>
        /// 插入一条推荐信息,同时更新推荐的公司推荐次数与推荐分值
        /// </summary>
        /// <param name="info">公司推荐业务实体</param>
        /// <returns>失败 成功 已推荐过该公司</returns>
        public EyouSoft.Model.ResultStructure.ResultInfo Insert(EyouSoft.Model.CreditSystemStructure.RateHoldUp info)
        {
            EyouSoft.Model.ResultStructure.ResultInfo result = idal.Insert(info);
            if (result == EyouSoft.Model.ResultStructure.ResultInfo.Succeed)  //推荐成功后,则清除缓存
                EyouSoft.BLL.CreditSystemStructure.RateScore.CreateInstance().RemoveCache(info.ToCompanyId);
            return result;
        }
    }
}
