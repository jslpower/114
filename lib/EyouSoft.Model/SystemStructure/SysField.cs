using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 系统类型定义基类
    /// </summary>
    /// 周文超 2010-06-22
    [Serializable]
    public class SysFieldBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysFieldBase() { }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public int FieldId { get; set; }
    }

    /// <summary>
    /// 系统类型定义
    /// </summary>
    /// 周文超 2010-06-22
    [Serializable]
    public class SysField : SysFieldBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysField() { }

        /// <summary>
        /// 系统类型定义类型
        /// </summary>
        public SysFieldType FieldType { get; set; }

        /// <summary>
        /// 是否系统预定义类型
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用，默认启用(true)
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    #region 系统名称定义类型枚举

    /// <summary>
    /// 系统类型定义类型
    /// </summary>
    public enum SysFieldType
    {
        /// <summary>
        /// 团队推广状态
        /// </summary>
        推广状态 = 1,
        /// <summary>
        /// 线路主题
        /// </summary>
        线路主题,
        /// <summary>
        /// 公用客户等级
        /// </summary>
        客户等级,
        /// <summary>
        /// 景点主题
        /// </summary>
        景点主题,
        /// <summary>
        /// 周边环境
        /// </summary>
        周边环境

    }

    #endregion
}
