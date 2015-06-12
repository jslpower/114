using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 在线用户 业务逻辑
    /// </summary>
    /// 周文超 2010-05-26
    public class SysOnlineUser : IBLL.SystemStructure.ISysOnlineUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysOnlineUser() { }

        private readonly IDAL.SystemStructure.ISysOnlineUser dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.ISysOnlineUser>();

        /// <summary>
        /// 构造在线用户业务逻辑接口
        /// </summary>
        /// <returns>在线用户业务逻辑接口</returns>
        public static IBLL.SystemStructure.ISysOnlineUser CreateInstance()
        {
            IBLL.SystemStructure.ISysOnlineUser op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.SystemStructure.ISysOnlineUser>();
            }
            return op;
        }



        #region ISysOnlineUser 成员

        /// <summary>
        /// 新增在线用户
        /// </summary>
        /// <param name="model">在线用户实体</param>
        /// <returns>0:Error;1:Success</returns>
        public int AddSysOnlineUser(EyouSoft.Model.SystemStructure.SysOnlineUser model)
        {
            if (model == null)
                return 0;

            int Result = dal.AddSysOnlineUser(model);

            if (Result > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <returns>返回操作是否成功</returns>
        public bool DeleteSysOnlineUser()
        {
            return dal.DeleteSysOnlineUser();
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns>在线用户实体集合</returns>
        public IList<EyouSoft.Model.SystemStructure.SysOnlineUser> GetOnlineUserList()
        {
            return dal.GetOnlineUserList();
        }

        #endregion
    }
}
