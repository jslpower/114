using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ResultStructure
{
    /// <summary>
    /// 操作结果返回值枚举
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public enum ResultInfo
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Succeed = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        Error,

        /// <summary>
        /// 数据已存在
        /// </summary>
        Exists
    }

    /// <summary>
    /// 操作用户信息的操作结果返回值枚举
    /// </summary>
    /// 创建人：张志瑜 2010-07-14
    public enum UserResultInfo
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Succeed = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        Error,

        /// <summary>
        /// 用户名已存在
        /// </summary>
        ExistsUserName,

        /// <summary>
        /// Email已存在
        /// </summary>
        ExistsEmail
    }


    /// <summary>
    /// 审核状态
    /// </summary>
    /// 创建人：张志瑜 2010-06-03
    public enum CheckState
    {
        /// <summary>
        /// 未审核
        /// </summary>
        未审核 = 0,
        /// <summary>
        /// 已通过审核
        /// </summary>
        已通过
    }
}
