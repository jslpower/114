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
    /// 描述：互动交流栏目业务逻辑层
    /// </summary>
    public class ExchangeTopicClass:IExchangeTopicClass
    {
        private readonly IDAL.SystemStructure.IExchangeTopicClass dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.IExchangeTopicClass>();

        #region CreateInstance
        /// <summary>
        /// 创建互动交流栏目业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.IExchangeTopicClass CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.IExchangeTopicClass op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.IExchangeTopicClass>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 获取互动交流栏目列表
        /// </summary>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.ExchangeTopicClass> GetList()
        {
            return dal.GetList();
        }
        #endregion
    }
}
