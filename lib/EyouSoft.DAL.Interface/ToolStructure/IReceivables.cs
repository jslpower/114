using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.ToolStructure
{
    /// <summary>
    /// 应收账款信息数据层接口
    /// </summary>
    /// 周文超 2010-11-09
    public interface IReceivables
    {
        /// <summary>
        /// 添加应收账款信息
        /// </summary>
        /// <param name="model">应收账款信息实体</param>
        /// <returns></returns>
        bool AddReceivables(EyouSoft.Model.ToolStructure.Receivables model);

        /// <summary>
        /// 根据ID获取对应的应收账款信息
        /// </summary>
        /// <param name="ReceivablesId">应收账款信息ID</param>
        /// <returns></returns>
        EyouSoft.Model.ToolStructure.Receivables GetModel(string ReceivablesId);

        /// <summary>
        /// 获取应收（已收）账款信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="TourNo">団号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="OperatorName">下单人</param>
        /// <param name="RetailersName">组团社名称</param>
        /// <param name="StartLeaveDate">开始出团时间</param>
        /// <param name="EndLeaveDate">结束出团时间</param>
        /// <param name="IsCheck">是否只查询已收</param>
        /// <param name="CompanyId">所属供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.Receivables> GetList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string TourNo, string RouteName, string OperatorName, string RetailersName, DateTime? StartLeaveDate, DateTime? EndLeaveDate
            , bool IsCheck, string CompanyId);
    }
}
