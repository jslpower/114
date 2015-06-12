using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.IDAL.NewTourStructure
{
    using EyouSoft.Model.CompanyStructure;

    /// <summary>
    /// 散拼订单
    /// 创建者：郑知远
    /// 创建时间：2011-12-20
    /// </summary>
    public interface ITourOrder
    {
        #region 增,删,改
        /// <summary>
        /// 添加、修改散拼订单
        /// </summary>
        /// <param name="item">散拼订单实体</param>
        /// <returns></returns>
        bool AddOrUpdTourOrder(bool isAdd,MTourOrder item);

        /// <summary>
        /// 添加订单日志
        /// </summary>
        /// <param name="m">订单日志实体</param>
        void AddOrderHandleLog(MOrderHandleLog m);

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        string CreateOrderNo(string mark);
        /// <summary>
        /// 根据订单编号修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="saveDate">留位时间</param>
        /// <returns></returns>
        bool SetOrderStatus(string orderId, PowderOrderStatus status, DateTime? saveDate);
        /// <summary>
        /// 根据订单编号修改支付状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">支付编号</param>
        /// <returns></returns>
        bool SetPaymentStatus(string orderId, PaymentStatus status);
        /// <summary>
        /// 根据订单编号删除订单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        bool DelTourOrder(string orderId);
        #endregion

        #region 查询
        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单实体</returns>
        MTourOrder GetModel(string orderId);
        /// <summary>
        /// 专线商-最新散客订单(有效期内的散拼订单)
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="orderNo">订单号</param>
        /// <returns>订单集合</returns>
        IList<MTourOrder> GetPublishersNewList(string tourId, string orderNo);
        /// <summary>
        /// 专线商-所有散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">专线商</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        IList<MTourOrder> GetPublishersAllList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MTourOrderSearch search);
        /// <summary>
        /// 组团社-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">组团社</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        IList<MTourOrder> GetTravelList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, MTourOrderSearch search);
        /// <summary>
        /// 运营后台-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散客订单集合</returns>
        IList<MTourOrder> GetList(int pageSize, int pageCurrent, ref int recordCount, MTourOrderSearch search);
        /// <summary>
        /// 根据搜索实体获取订单统计列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单统计实体列表</returns>
        IList<MOrderStatic> GetOrderStaticLst(int pageSize, int pageCurrent, ref int recordCount, MOrderStaticSearch search);
        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <returns>订单游客列表</returns>
        IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp);

        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <param name="orderStatus">订单状态数组</param>
        /// <returns>订单游客列表</returns>
        IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp,
                                                           params PowderOrderStatus?[] orderStatus);

        /// <summary>
        /// 根据搜索实体获取不带分页订单操作日志列表
        /// </summary>
        /// <param name="search">订单搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        IList<MOrderHandleLog> GetOrderHandleLogLst(MOrderHandleLogSearch search);
        /// <summary>
        /// 根据搜索实体获取带分页订单操作日志列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        IList<MOrderHandleLog> GetOrderHandleLogLst(int pageSize, int pageCurrent, ref int recordCount, MOrderHandleLogSearch search);

        #endregion
    }
}
