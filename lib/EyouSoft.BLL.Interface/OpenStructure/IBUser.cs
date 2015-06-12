using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.OpenStructure
{
    /// <summary>
    /// 各平台用户对应关系业务逻辑接口
    /// </summary>
    /// 周文超 2011-04-02
    public interface IBUser
    {
        /// <summary>
        /// 添加各平台用户对应关系
        /// </summary>
        /// <param name="model">各平台用户对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        int AddMUser(Model.OpenStructure.MUserInfo model);

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemUserId">系统用户编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformUserId">平台用户编号，大平台（114平台）时赋值</param>
        /// <returns></returns>
        IList<Model.OpenStructure.MUserInfo> GetMUserList(int SystemUserId, int SystemType, string PlatformUserId);

         /// <summary>
        /// 根据条件获取平台用户的系统权限
        /// </summary>
        /// <param name="SystemType">系统类型</param>
        /// <param name="SystemUserId">系统用户编号</param>
        /// <returns></returns>
        int[] GetUserPermission(int SystemType, int SystemUserId);
    }
}
