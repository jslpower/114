using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 机票平台-国籍基础信息IBLL
    /// </summary>
    /// 创建人：luofx 2010-11-8
    public interface ITicketNationInfo
    {
        /// <summary>
        /// 获取国家信息列表
        /// </summary>
        /// <param name="CountryName">国家名称（为空时：查询所有；不为空时，按国家名称筛选）</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketNationInfo> GetList(string CountryName);
    }
}
