using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-13
    /// 描述：供求栏目固定文字数据层接口
    /// </summary>
    public interface ISiteTopic
    {
        /// <summary>
        /// 设置供求栏目
        /// </summary>
        /// <param name="fieldKey">供求栏目key</param>
        /// <param name="fieldValue">供求栏目value</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetSiteTopic(string fieldKey, string fieldValue);
        /// <summary>
        /// 获取指定栏目的值
        /// </summary>
        /// <param name="FieldKey">栏目名称(key)</param>
        /// <returns>true:成功 false:失败</returns>
        string GetSiteTopic(string FieldKey);
    }
}
