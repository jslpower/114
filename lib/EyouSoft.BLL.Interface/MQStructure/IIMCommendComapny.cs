using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.MQStructure
{
    /// <summary>
    /// MQ推荐批发商
    /// </summary>
    /// 周文超  2010-06-12
    public interface IIMCommendComapny
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ推荐批发商实体</param>
        /// <returns>返回受影响行数</returns>
        int Add(Model.MQStructure.IMCommendComapny model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">MQ推荐批发商ID</param>
        /// <returns>返回操作是否成功</returns>
        bool Delete(int Id);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="OperatorId">推荐人姓名(为null不作条件)</param>
        /// <param name="CompanyId">推荐人公司编号(为null不作条件)</param>
        /// <param name="CompanyName">推荐人公司名称(为null不作条件)</param>
        /// <param name="DescCompanyName">被推荐的公司名称(为null不作条件)</param>
        /// <returns>返回MQ推荐批发商实体集合</returns>
        IList<Model.MQStructure.IMCommendComapny> GetList(string OperatorId, string CompanyId, string CompanyName, string DescCompanyName);
    }
}
