using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IBLL.NewTourStructure
{
    /// <summary>
    /// 订单日志
    /// 创建者：郑付杰
    /// 创建时间：2011-12-20
    /// </summary>
    public interface IOrderHandleLog
    {
        /// <summary>
        /// 订单日志
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        IList<MOrderHandleLog> GetList(string orderId);
    }
}
