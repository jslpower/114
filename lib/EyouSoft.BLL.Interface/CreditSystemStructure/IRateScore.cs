using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-interface fot the RateScore BLL
    /// </summary>
    public interface IRateScore
    {
        /// <summary>
        /// 获取指定公司的信用分值与次数汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        EyouSoft.Model.CreditSystemStructure.RateDetailTotal GetCollectsInfo(string companyId);
        /// <summary>
        /// 移除公司诚信分值缓存
        /// </summary>
        /// <param name="companyId">公司ID</param>
        void RemoveCache(string companyId);
    }
}
