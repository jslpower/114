using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ推荐批发商
    /// </summary>
    /// 周文超  2010-06-12
    public class IMCommendComapny : IBLL.MQStructure.IIMCommendComapny
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMCommendComapny() { }

        private readonly IDAL.MQStructure.IIMCommendComapny dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMCommendComapny>();

        /// <summary>
        /// 构造MQ推荐批发商接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMCommendComapny CreateInstance()
        {
            IBLL.MQStructure.IIMCommendComapny op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMCommendComapny>();
            }
            return op;
        }

        #region IIMCommendComapny 成员

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">MQ推荐批发商实体</param>
        /// <returns>返回受影响行数</returns>
        public int Add(EyouSoft.Model.MQStructure.IMCommendComapny model)
        {
            if (model == null)
                return 0;

            return dal.Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="Id">MQ推荐批发商ID</param>
        /// <returns>返回操作是否成功</returns>
        public bool Delete(int Id)
        {
            if (Id <= 0)
                return false;

            return dal.Delete(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="OperatorId">推荐人姓名(为null不作条件)</param>
        /// <param name="CompanyId">推荐人公司编号(为null不作条件)</param>
        /// <param name="CompanyName">推荐人公司名称(为null不作条件)</param>
        /// <param name="DescCompanyName">被推荐的公司名称(为null不作条件)</param>
        /// <returns>返回MQ推荐批发商实体集合</returns>
        public IList<EyouSoft.Model.MQStructure.IMCommendComapny> GetList(string OperatorId, string CompanyId, string CompanyName, string DescCompanyName)
        {
            return dal.GetList(OperatorId, CompanyId, CompanyName, DescCompanyName);
        }

        #endregion
    }
}
