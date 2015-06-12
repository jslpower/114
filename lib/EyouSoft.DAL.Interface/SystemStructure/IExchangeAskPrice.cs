using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：互动交流询价接口
    /// </summary>
    public interface IExchangeAskPrice
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">交流询价实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Add(ExchangeAskPrice model);
        
    }
}
