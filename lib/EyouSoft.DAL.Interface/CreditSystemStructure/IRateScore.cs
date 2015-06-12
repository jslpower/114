using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-05-21
    /// 描述:诚信体系-interface fot the RateScore DAL
    /// </summary>
    public interface IRateScore
    {
        /// <summary>
        /// 获取指定公司的信用分值与次数汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="certificateScore">1份证书所获得的分值</param>
        /// <returns></returns>
        EyouSoft.Model.CreditSystemStructure.RateDetailTotal GetCollectsInfo(string companyId, double certificateScore);
    }
}
