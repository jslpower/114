using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.CommunityStructure;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求规则数据访问层接口
    /// </summary>
    /// 创建人：mk 2011-5-10
    public interface IDSupplyDemandRule
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供求规则实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(List<MSupplyDemandRule> list);

        /// <summary>
        /// 获取供求规则实体集合
        /// </summary>
        /// <returns>供求规则实体集合</returns>
        IList<MSupplyDemandRule> GetList();
    }
}
