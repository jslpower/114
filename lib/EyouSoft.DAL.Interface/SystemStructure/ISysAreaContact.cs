using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：区域联系人接口
    /// </summary>
    public interface ISysAreaContact
    {
        /// <summary>
        /// 添加区域联系人信息
        /// </summary>
        /// <param name="List">区域联系人信息集合</param>
        /// <returns>返回受影响行数</returns>
        int AddSysAreaContact(IList<EyouSoft.Model.SystemStructure.SysAreaContact> List);

        /// <summary>
        /// 删除区域联系人信息
        /// </summary>
        /// <param name="AreaContactId">区域联系人信息ID</param>
        /// <returns>受影响行数</returns>
        int DeleteSysAreaContact(int AreaContactId);

        /// <summary>
        /// 删除所有区域联系人
        /// </summary>
        /// <returns>受影响行数</returns>
        int DeleteSysAreaContact();
        
        /// <summary>
        /// 获取区域联系人信息
        /// </summary>
        /// <returns>区域联系人信息实体集合</returns>
        IList<EyouSoft.Model.SystemStructure.SysAreaContact> GetAreaContactList();
       
    }
}
