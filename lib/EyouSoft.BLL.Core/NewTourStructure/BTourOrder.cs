using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace EyouSoft.BLL.NewTourStructure
{
    using EyouSoft.Model.CompanyStructure;
    using EyouSoft.Model.NewTourStructure;
    using EyouSoft.SSOComponent.Entity;

    /// <summary>
    /// 散拼订单
    /// </summary>
    public class BTourOrder : EyouSoft.IBLL.NewTourStructure.ITourOrder
    {
        private readonly EyouSoft.IDAL.NewTourStructure.ITourOrder _dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.NewTourStructure.ITourOrder>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <returns></returns>
        public static IBLL.NewTourStructure.ITourOrder CreateInstance()
        {
            IBLL.NewTourStructure.ITourOrder op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.NewTourStructure.ITourOrder>();
            }
            return op;
        }

        #region ITourOrder 成员
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="mark">前缀</param>
        /// <returns></returns>
        public string CreateOrderNo(string mark)
        {
            return this._dal.CreateOrderNo(mark);
        }

        /// <summary>
        /// 添加、修改散拼订单
        /// </summary>
        /// <param name="item">散拼订单实体（OrderId=null时添加否则修改；修改判断用 组团社Travel有值为组团社修改否则专线商修改）</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdTourOrder(EyouSoft.Model.NewTourStructure.MTourOrder item)
        {
            bool result = false;
            bool isAdd;
            if (item != null && (!string.IsNullOrEmpty(item.Travel) || (!string.IsNullOrEmpty(item.Publishers))))
            {
                if (string.IsNullOrEmpty(item.OrderId))
                {
                    item.OrderId = Guid.NewGuid().ToString();
                    //item.OrderNo = this.CreateOrderNo("X");
                    item.OrderStatus = PowderOrderStatus.专线商待处理;
                    isAdd = true;
                }
                else
                {
                    isAdd = false;
                }
                result = this._dal.AddOrUpdTourOrder(isAdd, item);

                if (result)
                {
                    #region 获取登录用户信息
                    var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                    var logWriterId = string.Empty;
                    var logWriterName = string.Empty;
                    var logWriterCompanyId = string.Empty;
                    if (userInfo == null)
                    {
                        userInfo = new EyouSoft.Security.Membership.UserProvider().GetMQUser();
                    }

                    if (userInfo == null)
                    {
                        var masterUser = new EyouSoft.Security.Membership.UserProvider().GetMaster();
                        if (masterUser != null)
                        {
                            logWriterId = masterUser.ID.ToString();
                            logWriterName = masterUser.ContactName;
                        }
                    }
                    else
                    {
                        logWriterId = userInfo.ID;
                        logWriterName = userInfo.ContactInfo.ContactName;
                        logWriterCompanyId = userInfo.CompanyID;
                    }
                    #endregion

                    if (isAdd)
                    {
                        //写日志
                        this.AddOrderHandleLog(
                            new MOrderHandleLog
                            {
                                LogId = Guid.NewGuid().ToString(),
                                OrderId = item.OrderId,
                                OrderType = OrderSource.线路散拼订单,
                                OrderNo = item.OrderNo,
                                OperatorId = logWriterId,
                                OperatorName = logWriterName,
                                CompanyId = string.IsNullOrEmpty(item.Travel) ? item.Publishers : item.Travel,
                                Remark = "订单接受预订", //"订单修改",
                                IssueTime = DateTime.Now
                            });


                        //发MQ发送消息
                        EyouSoft.BLL.MQStructure.IMMessage.CreateInstance().AddMessageToWholesalersByNewOrder(item.OrderId, 1);
                    }
                    else
                    {
                        //写日志
                        this.AddOrderHandleLog(
                            new MOrderHandleLog
                            {
                                LogId = Guid.NewGuid().ToString(),
                                OrderId = item.OrderId,
                                OrderType = OrderSource.线路散拼订单,
                                OrderNo = item.OrderNo,
                                OperatorId = logWriterId,
                                OperatorName = logWriterName,
                                CompanyId = logWriterCompanyId,
                                Remark = "订单修改",
                                IssueTime = DateTime.Now
                            });
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 根据订单编号修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="saveDate">留位时间</param>
        /// <returns></returns>
        public bool SetOrderStatus(string orderId, PowderOrderStatus status, DateTime? saveDate)
        {
            bool isOk = false;
            if (!string.IsNullOrEmpty(orderId))
            {
                isOk = this._dal.SetOrderStatus(orderId, status, saveDate);
                #region 登录用户信息
                var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                var logWriterId = string.Empty;
                var logWriterName = string.Empty;
                var logWriterCompanyId = string.Empty;
                if (userInfo == null)
                {
                    userInfo = new EyouSoft.Security.Membership.UserProvider().GetMQUser();
                }

                if (userInfo == null)
                {
                    var masterUser = new EyouSoft.Security.Membership.UserProvider().GetMaster();
                    if (masterUser != null)
                    {
                        logWriterId = masterUser.ID.ToString();
                        logWriterName = masterUser.ContactName;
                    }
                }
                else
                {
                    logWriterId = userInfo.ID;
                    logWriterName = userInfo.ContactInfo.ContactName;
                    logWriterCompanyId = userInfo.CompanyID;
                }
                #endregion
                if (isOk)
                {
                    string orderNo = this.GetModel(orderId).OrderNo;
                    this.AddOrderHandleLog(
                        new MOrderHandleLog
                        {
                            LogId = Guid.NewGuid().ToString(),
                            OrderId = orderId,
                            OrderType = OrderSource.线路散拼订单,
                            OrderNo = orderNo,
                            OperatorId = logWriterId,
                            OperatorName = logWriterName,
                            CompanyId = logWriterCompanyId,
                            Remark = "订单状态修改为" + status.ToString(),
                            IssueTime = DateTime.Now
                        });

                }
                if (isOk)
                {
                    if (status == PowderOrderStatus.专线商已确定)
                    {
                        EyouSoft.BLL.MQStructure.IMMessage.CreateInstance().AddMessageToRetailersByOrder(orderId, 1);
                    }
                }
            }
            return isOk;
        }
        /// <summary>
        /// 根据订单编号修改支付状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">支付编号</param>
        /// <returns></returns>
        public bool SetPaymentStatus(string orderId, PaymentStatus status)
        {
            var isOk = false;
            if (!string.IsNullOrEmpty(orderId))
            {
                isOk = this._dal.SetPaymentStatus(orderId, status);
                #region 登录用户信息
                var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                var logWriterId = string.Empty;
                var logWriterName = string.Empty;
                var logWriterCompanyId = string.Empty;
                if (userInfo == null)
                {
                    userInfo = new EyouSoft.Security.Membership.UserProvider().GetMQUser();
                }

                if (userInfo == null)
                {
                    var masterUser = new EyouSoft.Security.Membership.UserProvider().GetMaster();
                    if (masterUser != null)
                    {
                        logWriterId = masterUser.ID.ToString();
                        logWriterName = masterUser.ContactName;
                    }
                }
                else
                {
                    logWriterId = userInfo.ID;
                    logWriterName = userInfo.ContactInfo.ContactName;
                    logWriterCompanyId = userInfo.CompanyID;
                }
                #endregion
                if (isOk)
                {
                    string orderNo = this.GetModel(orderId).OrderNo;
                    this.AddOrderHandleLog(
                        new MOrderHandleLog
                        {
                            LogId = Guid.NewGuid().ToString(),
                            OrderId = orderId,
                            OrderType = OrderSource.线路散拼订单,
                            OrderNo = orderNo,
                            OperatorId = logWriterId,
                            OperatorName = logWriterName,
                            CompanyId = logWriterCompanyId,
                            Remark = "订单支付状态修改为" + status.ToString(),
                            IssueTime = DateTime.Now
                        });
                }
            }
            return isOk;
        }
        /// <summary>
        /// 根据订单编号删除订单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public bool DelTourOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return false;
            }
            string OrderNo = this.GetModel(orderId).OrderNo;
            var logWriterCompanyId = string.Empty;
            var isOk = this._dal.DelTourOrder(orderId);
            if (isOk)
            {
                var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                var logWriterId = string.Empty;
                var logWriterName = string.Empty;
                if (userInfo == null)
                {
                    userInfo = new EyouSoft.Security.Membership.UserProvider().GetMQUser();
                }

                if (userInfo == null)
                {
                    var masterUser = new EyouSoft.Security.Membership.UserProvider().GetMaster();
                    if (masterUser != null)
                    {
                        logWriterId = masterUser.ID.ToString();
                        logWriterName = masterUser.ContactName;
                    }
                }
                else
                {
                    logWriterId = userInfo.ID;
                    logWriterName = userInfo.ContactInfo.ContactName;
                    logWriterCompanyId = userInfo.CompanyID;
                }
                this.AddOrderHandleLog(
                    new MOrderHandleLog
                    {
                        LogId = Guid.NewGuid().ToString(),
                        OrderId = orderId,
                        OrderType = OrderSource.线路散拼订单,
                        OrderNo = OrderNo,
                        OperatorId = logWriterId,
                        OperatorName = logWriterName,
                        CompanyId = logWriterCompanyId,
                        Remark = "删除订单",
                        IssueTime = DateTime.Now
                    });
            }
            return isOk;
        }

        /// <summary>
        /// 添加订单日志
        /// </summary>
        /// <param name="m">订单日志实体</param>
        public void AddOrderHandleLog(MOrderHandleLog m)
        {
            this._dal.AddOrderHandleLog(m);
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单实体</returns>
        public EyouSoft.Model.NewTourStructure.MTourOrder GetModel(string orderId)
        {
            return this._dal.GetModel(orderId);
        }

        /// <summary>
        /// 专线商-最新散客订单(有效期内的散拼订单)
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="orderNo">订单号</param>
        /// <returns>订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourOrder> GetPublishersNewList(string tourId, string orderNo)
        {
            return this._dal.GetPublishersNewList(tourId, orderNo);
        }

        /// <summary>
        /// 专线商-所有散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">专线商</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourOrder> GetPublishersAllList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourOrderSearch search)
        {
            if (search != null)
            {
                return this._dal.GetPublishersAllList(pageSize, pageCurrent, ref recordCount, companyId, search);
            }
            return null;
        }

        /// <summary>
        /// 组团社-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">组团社</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourOrder> GetTravelList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourOrderSearch search)
        {
            return this._dal.GetTravelList(pageSize, pageCurrent, ref recordCount, companyId, search);
        }

        /// <summary>
        /// 运营后台-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散客订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourOrder> GetList(int pageSize, int pageCurrent, ref int recordCount, EyouSoft.Model.NewTourStructure.MTourOrderSearch search)
        {
            return this._dal.GetList(pageSize, pageCurrent, ref recordCount, search);
        }
        /// <summary>
        /// 根据搜索实体获取订单统计列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单统计实体列表</returns>
        public IList<MOrderStatic> GetOrderStaticLst(int pageSize, int pageCurrent, ref int recordCount, MOrderStaticSearch search)
        {
            return search == null ? null : this._dal.GetOrderStaticLst(pageSize, pageCurrent, ref recordCount, search);
        }
        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <returns>订单游客列表</returns>
        public IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp)
        {
            return string.IsNullOrEmpty(tourId) && string.IsNullOrEmpty(companyId) && (companyTyp == CompanyType.专线 || companyTyp == CompanyType.组团) ? null : this._dal.GetOrderCustomerByTourId(tourId, companyId, companyTyp);
        }

        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <param name="orderStatus">订单状态数组</param>
        /// <returns>订单游客列表</returns>
        public IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp,
                                                           params PowderOrderStatus?[] orderStatus)
        {
            return string.IsNullOrEmpty(tourId) && string.IsNullOrEmpty(companyId) && (companyTyp == CompanyType.专线 || companyTyp == CompanyType.组团) ? null : this._dal.GetOrderCustomerByTourId(tourId, companyId, companyTyp, orderStatus);
        }

        /// <summary>
        /// 根据搜索实体获取不带分页订单操作日志列表
        /// </summary>
        /// <param name="search">订单搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        public IList<MOrderHandleLog> GetOrderHandleLogLst(MOrderHandleLogSearch search)
        {
            return this._dal.GetOrderHandleLogLst(search);
        }
        /// <summary>
        /// 根据搜索实体获取带分页订单操作日志列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        public IList<MOrderHandleLog> GetOrderHandleLogLst(int pageSize, int pageCurrent, ref int recordCount, MOrderHandleLogSearch search)
        {
            return this._dal.GetOrderHandleLogLst(pageSize, pageCurrent, ref recordCount, search);
        }

        #endregion
    }
}
