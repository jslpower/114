using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：互动交流询价业务逻辑接口
    /// </summary>
    public interface IExchangeAskPrice
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">询价实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(ExchangeAskPrice model);
    }
}
