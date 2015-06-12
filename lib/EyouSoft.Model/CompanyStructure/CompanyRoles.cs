using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间：2010-05-11
    /// 描述：基础数据-公司角色表实体类
    /// </summary>
    public class CompanyUserRoles
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyUserRoles() { }

        #region 属性
        /// <summary>
        /// 角色编号(默认为newid())
        /// </summary>
       public string ID
       {
            get;
            set;
       }
       /// <summary>
       /// 角色名称
       /// </summary>
       public string RoleName
       {
            get;
            set;
       }
       /// <summary>
       /// 角色权限列表
       /// </summary>
       public string PermissionList
       {
           get;
           set;
       }
       /// <summary>
       /// 是否管理员角色(0:非管理员角色,1:管理员角色  默认为0)
       /// </summary>
       public bool IsAdminRole
       {
           get;
           set;
       }
       /// <summary>
       /// 创建角色的操作员公司ID(默认为0)   单索引
       /// </summary>
       public string CompanyID
       {
           get;
           set;
       }
       /// <summary>
       /// 创建角色的操作员用户ID(默认为0)
       /// </summary>
       public string OperatorID
       {
           get;
           set;
       }
        /* 
       /// <summary>
       /// 管理区域
       /// </summary>
       public string AreaList
       {
           get;
           set;
       } 
       */
       /// <summary>
       /// 创建时间(默认为getdate())
       /// </summary>
       public DateTime IssueTime
       {
           get;
           set;
       }
       #endregion
    }
}
