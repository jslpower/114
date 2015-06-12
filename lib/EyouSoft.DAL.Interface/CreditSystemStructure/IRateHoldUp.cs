using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-Interface fot the RateHoldUp DAL
    /// </summary>
    public interface IRateHoldUp
    {
        /// <summary>
        /// 是否已经推荐
        /// </summary>
        /// <param name="toCompanyId">推荐的公司编号</param>
        /// <param name="fromCompanyId">推荐人所在公司编号</param>
        /// <returns></returns>
        bool IsHoldUp(string toCompanyId, string fromCompanyId);

        /// <summary>
        /// 获取指定公司的推荐数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        int GetAmount(string companyId);

        /// <summary>
        /// 获取指定公司的推荐列表
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CreditSystemStructure.RateHoldUp> GetHoldUps(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 插入一条推荐信息,同时更新推荐的公司推荐次数与推荐分值 
        /// </summary>
        /// <param name="info">公司推荐业务实体</param>
        /// <returns>失败 成功 已推荐过该公司</returns>
        EyouSoft.Model.ResultStructure.ResultInfo Insert(EyouSoft.Model.CreditSystemStructure.RateHoldUp info);
    }
}
