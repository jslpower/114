using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL.CommunityStructure;
using EyouSoft.IDAL.CommunityStructure;
using EyouSoft.Component.Factory;
using EyouSoft.Model.CommunityStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 二次整改供求规则业务逻辑层
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class BSupplyDemandRule : IBSupplyDemandRule
    {
        private readonly IDSupplyDemandRule dal = ComponentFactory.CreateDAL<IDSupplyDemandRule>();

        #region CreateInstance

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IBSupplyDemandRule CreateInstance()
        {
            IBSupplyDemandRule op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBSupplyDemandRule>();
            }
            return op;
        }

        #endregion

        #region IBSupplyDemandRule 成员

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(List<MSupplyDemandRule> list)
        {
            if (list == null || list.Count <= 0)
                return false;
            return dal.Add(list);
        }

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <returns></returns>
        public IList<MSupplyDemandRule> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
