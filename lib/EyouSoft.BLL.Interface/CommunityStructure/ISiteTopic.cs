using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-13
    /// 描述：供求栏目固定文字业务层接口
    /// </summary>
    public interface ISiteTopic
    {
        /// <summary>
        /// 设置访谈内容
        /// </summary>
        /// <param name="Content">访谈内容</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetInterview(string Content);
        /// <summary>
        /// 设置同业交流专区
        /// </summary>
        /// <param name="Content">同业交流</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetCommArea(string Content);
        /// <summary>
        /// 设置学堂介绍
        /// </summary>
        /// <param name="Content">学堂介绍</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetSchool(string Content);
        /// <summary>
        /// 获取访谈内容
        /// </summary>
        /// <returns>不存在返回“”</returns>
        string GetInterview();
        /// <summary>
        /// 获取同业交流专区内容
        /// </summary>
        /// <returns>不存在返回“”</returns>
        string GetCommArea();
        /// <summary>
        /// 获取同业学堂介绍
        /// </summary>
        /// <returns>不存在返回“”</returns>
        string GetSchool();
    }
}
