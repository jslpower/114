using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;
namespace EyouSoft.IBLL.ToolStructure
{
    /// <summary>
    ///  应付账款信息业务层接口
    /// </summary>
    /// 鲁功源 2010-11-09
    public interface IPayments
    {
        /// <summary>
        /// 添加应付账款信息
        /// </summary>
        /// <param name="model">应付账款信息实体</param>
        /// <returns></returns>
        [CommonLogHandler(
           LogTitle = EyouSoft.BusinessLogWriter.CompanyLog.LOG_PAYMENTS_ADD_TITLE,
           LogMessage = EyouSoft.BusinessLogWriter.CompanyLog.LOG_PAYMENTS_ADD,
           LogWriterType = typeof(EyouSoft.BusinessLogWriter.CompanyLog),
           LogAttribute = @"[{""Index"":0,""Attribute"":""CompanyId"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""Id"",""AttributeType"":""class""}]",
           EventCode = EyouSoft.BusinessLogWriter.CompanyLog.LOG_PAYMENTS_ADD_CODE)]
        bool AddPayments(EyouSoft.Model.ToolStructure.Payments model);

        /// <summary>
        /// 根据ID获取对应的应付账款信息
        /// </summary>
        /// <param name="PaymentId">应付账款信息ID</param>
        /// <returns></returns>
        EyouSoft.Model.ToolStructure.Payments GetModel(string PaymentId);

        /// <summary>
        /// 获取应付账款信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="TourNo">団号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="SupperName">供应商名称</param>
        /// <param name="SupperType">供应商类型</param>
        /// <param name="StartLeaveDate">开始出团时间</param>
        /// <param name="EndLeaveDate">结束出团时间</param>
        /// <param name="IsCheck">是否只查询已付</param>
        /// <param name="CompanyId">所属供应商编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.ToolStructure.Payments> GetList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string TourNo, string RouteName, string SupperName, string SupperType, DateTime? StartLeaveDate, DateTime? EndLeaveDate
            , bool IsCheck,string CompanyId);
    }
}
