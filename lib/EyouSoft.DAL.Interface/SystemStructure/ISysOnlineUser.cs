using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 在线用户 数据访问接口
    /// </summary>
    /// 周文超 2010-05-13
    public interface ISysOnlineUser
    {
        /// <summary>
        /// 新增在线用户
        /// </summary>
        /// <param name="model">在线用户实体</param>
        /// <returns>返回受影响行数</returns>
        int AddSysOnlineUser(EyouSoft.Model.SystemStructure.SysOnlineUser model);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <returns>返回操作是否成功</returns>
        bool DeleteSysOnlineUser();

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns>在线用户实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysOnlineUser> GetOnlineUserList();
    }
}
