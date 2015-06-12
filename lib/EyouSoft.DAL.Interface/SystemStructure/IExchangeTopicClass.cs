using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
namespace EyouSoft.IDAL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-24
    /// 描述：互动交流栏目接口
    /// </summary>
    public interface IExchangeTopicClass
    {
        /// <summary>
        /// 获取互动交流栏目列表
        /// </summary>
        /// <returns></returns>
        IList<ExchangeTopicClass> GetList();
    }
}
