using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model;
using EyouSoft.IBLL;
using EyouSoft.IDAL;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 订单游客信息业务逻辑
    /// </summary>
    /// 周文超 2010-05-25
    public class TourOrderCustomer : IBLL.TourStructure.ITourOrderCustomer 
    {
        private readonly IDAL.TourStructure.ITourOrderCustomer dalCustomer = ComponentFactory.CreateDAL<IDAL.TourStructure.ITourOrderCustomer>();

        /// <summary>
        /// 构造订单游客信息业务逻辑接口
        /// </summary>
        /// <returns>订单游客信息业务逻辑接口</returns>
        public static IBLL.TourStructure.ITourOrderCustomer CreateInstance()
        {
            IBLL.TourStructure.ITourOrderCustomer op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.TourStructure.ITourOrderCustomer>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrderCustomer()
        { }

        #region ITourOrderCustomer 成员

        /// <summary>
        /// 获取某订单的所有的游客信息
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <returns>返回实体集合</returns>
        public IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByOrderId(string OrderId)
        {
            if (string.IsNullOrEmpty(OrderId))
                return null;

            return dalCustomer.GetCustomerListByOrderId(OrderId);
        }

        /// <summary>
        /// 获取某公司的所有的游客信息(分页)
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数(返回参数)</param>
        /// <param name="OrderIndex">排序方式：0 时间升序；1 时间降序</param>
        /// <param name="CompanyId">游客所属公司ID</param>
        /// <returns>返回游客信息实体集合</returns>
        public IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerListByCompanyId(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, string CompanyId)
        {
            return dalCustomer.GetCustomerListByCompanyId(PageSize, PageIndex, ref RecordCount, OrderIndex, CompanyId);
        }

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
        public IList<EyouSoft.Model.TourStructure.TourOrderCustomer> GetCustomerList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string BuyCompanyID, string SellCompanyName, string CustomerName, bool? Sex, DateTime? StartTime
            , DateTime? EndTime, params EyouSoft.Model.TourStructure.OrderState[] OrderStates)
        {
            if (string.IsNullOrEmpty(BuyCompanyID))
                return null;

            return dalCustomer.GetCustomerList(PageSize, PageIndex, ref RecordCount, OrderIndex, BuyCompanyID, SellCompanyName, CustomerName
                , Sex, StartTime, EndTime, OrderStates);
        }

        #endregion
    }
}
