using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.MQStructure
{
    /// <summary>
    /// IM中用户自定义组业务逻辑
    /// </summary>
    /// 周文超 2010-06-12
    public class IMGroupList : IBLL.MQStructure.IIMGroupList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMGroupList() { }

        private readonly IDAL.MQStructure.IIMGroupList dal = ComponentFactory.CreateDAL<IDAL.MQStructure.IIMGroupList>();

        /// <summary>
        /// 构造IM中用户自定义组业务逻辑接口
        /// </summary>
        /// <returns></returns>
        public static IBLL.MQStructure.IIMGroupList CreateInstance()
        {
            IBLL.MQStructure.IIMGroupList op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.MQStructure.IIMGroupList>();
            }
            return op;
        }


        #region IIMGroupList 成员

        /// <summary>
        /// 获取某MQ用户的好友分组信息
        /// </summary>
        /// <param name="mqId">MQ用户ID</param>
        /// <returns>IM中用户自定义组实体</returns>
        public IList<EyouSoft.Model.MQStructure.IMGroupList> GetFriendGroups(int mqId)
        {
            if (mqId <= 0)
                return null;

            return dal.GetFriendGroups(mqId);
        }

        #endregion
    }
}
