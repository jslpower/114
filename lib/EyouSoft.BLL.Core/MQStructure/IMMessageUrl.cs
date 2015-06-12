using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// MQ消息提醒中转地址业务逻辑
    /// </summary>
    /// 周文超 2010-06-12
    public class IMMessageUrl : IBLL.MQStructure.IIMMessageUrl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessageUrl() { }

        private readonly IDAL.MQStructure.IIMMessageUrl dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMMessageUrl>();

        /// <summary>
        /// 构造MQ消息提醒中转地址业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMMessageUrl CreateInstance()
        {
            IBLL.MQStructure.IIMMessageUrl op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMMessageUrl>();
            }
            return op;
        }

        #region IIMMessageUrl 成员

        /// <summary>
        /// 表中查询中转地址实体
        /// </summary>
        /// <param name="Id">MQ消息提醒中转地址ID</param>
        /// <returns></returns>
        public EyouSoft.Model.MQStructure.IMMessageUrl GetIm_Message_Url(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return null;

            return dal.GetIm_Message_Url(Id);
        }

        #endregion
    }
}
