using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 机票供应商扩展信息接口
    /// </summary>
    public interface ITicketWholesalersInfo
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">供应商扩展信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.TicketStructure.TicketWholesalersInfo model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">供应商扩展信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.TicketStructure.TicketWholesalersInfo model);
        /// <summary>
        /// 获取供应商扩展信息实体
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>供应商扩展信息实体</returns>
        EyouSoft.Model.TicketStructure.TicketWholesalersInfo GetModel(string CompanyId);
        /// <summary>
        /// 设置行程单服务价格
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <param name="ServicePrice">行程单服务价格</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetServicePrice(string CompanyId, decimal ServicePrice);
        /// <summary>
        /// 设置指定供应商的分成比例
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <param name="IntoRatio">分成比例</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetIntoRatio(string CompanyId, decimal IntoRatio);
        /// <summary>
        /// 获取指定公司的出票成功率
        /// </summary>
        /// <param name="CompanyId">供应商编号</param>
        /// <returns>出票成功率</returns>
        decimal GetSuccessRate(string CompanyId);
    }
}
