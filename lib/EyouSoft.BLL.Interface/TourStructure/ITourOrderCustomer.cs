using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TourStructure
{
    /// <summary>
    /// 订单游客信息业务逻辑接口
    /// </summary>
    /// 周文超 2010-05-25
    public interface ITourOrderCustomer
    {
        /// <summary>
        /// 获取某订单的所有的游客信息
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByOrderId(string OrderId);

        /// <summary>
        /// 获取某公司的所有的游客信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数(返回参数)</param>
        /// <param name="OrderIndex">排序方式：0 时间升序；1 时间降序</param>
        /// <param name="CompanyId">游客所属公司ID</param>
        /// <returns>返回游客信息实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByCompanyId(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string CompanyId);

        /// <summary>
        /// 获取游客信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="BuyCompanyID">零售商公司ID(买家)(必须传值)</param>
        /// <param name="SellCompanyName">专线商公司名称(卖家)(为null不作条件)</param>
        /// <param name="CustomerName">游客姓名(为null不作条件)</param>
        /// <param name="Sex">游客性别(为null不作条件)</param>
        /// <param name="StartTime">下单开始时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="EndTime">下单结束时间(订单的IssueTime)(为null不作条件)</param>
        /// <param name="OrderStates">订单状态</param>
        /// <returns>返回实体集合</returns>
        IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex
            , string BuyCompanyID, string SellCompanyName, string CustomerName, bool? Sex, DateTime? StartTime, DateTime? EndTime
            , params EyouSoft.Model.TourStructure.OrderState[] OrderStates);
    }
}
