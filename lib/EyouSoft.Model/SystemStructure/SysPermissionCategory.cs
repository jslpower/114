using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统权限类型
    /// </summary>
    public enum PermissionType
    {
        运营后台 = 1,
        大平台
    }
    /// <summary>
    /// 创建人：鲁功源 2010-07-05
    /// 描述：系统权限大类别实体类
    /// </summary>
    public class SysPermissionCategory
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysPermissionCategory() { }

        #region 成员属性
        /// <summary>
        /// 权限大类编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 权限类型编号
        /// </summary>
        public int TypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 权限类别名称
        /// </summary>
        public string CategoryName
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
        /// <summary>
        /// 权限子类别集合
        /// </summary>
        public IList<SysPermissionClass> SysPermissionClass
        {
            get;
            set;
        }
        #endregion
    }
}
