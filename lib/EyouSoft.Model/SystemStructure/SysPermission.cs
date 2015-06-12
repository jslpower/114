using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-05
    /// 描述：系统权限实体类
    /// </summary>
    public class SysPermission
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermission() { }

        #region 成员属性
        /// <summary>
        /// 权限编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 权限大类编号
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }
        /// <summary>
        /// 权限类别编号
        /// </summary>
        public int ClassId
        {
            get;
            set;
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName
        {
            get;
            set;
        }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用，1：启用；0：禁用
        /// </summary>
        public bool IsEnable
        {
            get;
            set;
        }
        #endregion
    }
}
