using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.TourStructure
{
    /// <summary>
    /// 订单评价信息业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-25
    public interface IRateOrder
    {
        /// <summary>
        /// 增加一条订单评价信息
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_ADD_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_ADD,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""OrderId"",""AttributeType"":""class""}]", 
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_ADD_CODE)]
        int AddRateOrder(EyouSoft.Model.TourStructure.RateOrder model);

        /// <summary>
        /// 更新一条订单评价信息，评论日期不更新
        /// </summary>
        /// <param name="model">订单评价信息实体</param>
        /// <returns>0:Error;1:Success</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_EDIT_TITLE,
            LogMessage = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_EDIT,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.OrderLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""OrderId"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.OrderLog.LOG_RATEORDER_EDIT_CODE)]
        int UpdateRateOrderById(EyouSoft.Model.TourStructure.RateOrder model);

        /// <summary>
        /// 获取某一公司作为卖家时所有的订单评价数据
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0:IssueTime升序；1:IssueTime降序</param>
        /// <param name="SellCompanyId">卖家公司ID(必须传值)</param>
        /// <param name="RateType">评价类型(小于等于0不作条件)</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TourStructure.RateOrder> GetRateOrderList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string SellCompanyId, int RateType);
    }
}
