using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：互动交流询价业务逻辑层
    /// </summary>
    public class ExchangeAskPrice:IExchangeAskPrice
    {
        private readonly IDAL.SystemStructure.IExchangeAskPrice dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.IExchangeAskPrice>();

        #region CreateInstance
        /// <summary>
        /// 创建互动交流询价业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.IExchangeAskPrice CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.IExchangeAskPrice op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.IExchangeAskPrice>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">交流询价实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.SystemStructure.ExchangeAskPrice model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        #endregion
    }
}
