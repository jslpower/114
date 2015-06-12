using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TourStructure
{
    /// <summary>
    /// 订单财务信息数据访问接口
    /// </summary>
    /// 周文超 2010-06-22
    public interface ITourOrderFinance
    {
        /// <summary>
        /// 新增订单财务信息
        /// </summary>
        /// <param name="model">订单财务业务实体</param>
        /// <returns>返回受印象的行数</returns>
        int AddTourOrderFinance(Model.TourStructure.TourOrderFinance model);

        /// <summary>
        /// 修改订单财务信息(只修改钱、联系人、订单人数信息)
        /// </summary>
        /// <param name="model">订单财务业务实体</param>
        /// <returns>返回受印象的行数</returns>
        int UpdateTourOrderFinance(Model.TourStructure.TourOrderFinance model);

        /// <summary>
        /// 设置订单财务信息的订单成交时间
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="SuccessTime">成交时间</param>
        /// <returns>返回操作是否成功</returns>
        bool SetSuccessTime(string OrderId, DateTime SuccessTime);
    }
}
