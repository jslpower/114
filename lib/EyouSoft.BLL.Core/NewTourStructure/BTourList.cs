using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using System.Transactions;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.BLL.NewTourStructure
{
    /// <summary>
    /// 团队计划,订单业务逻辑
    /// 修改记录：
    /// 1、2011-12-20 曹胡生 创建
    /// </summary>
    public class BTourList : EyouSoft.IBLL.NewTourStructure.ITourList
    {
        #region 反射数据接口

        private readonly IDAL.NewTourStructure.ITourList dal = ComponentFactory.CreateDAL<IDAL.NewTourStructure.ITourList>();

        /// <summary>
        /// 构造团队计划,订单业务逻辑接口
        /// </summary>
        /// <returns>团队计划,订单业务逻辑接口</returns>
        public static IBLL.NewTourStructure.ITourList CreateInstance()
        {
            IBLL.NewTourStructure.ITourList op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.NewTourStructure.ITourList>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BTourList()
        {

        }

        #endregion

        #region ITourList 成员

        /// <summary>
        /// 添加团队订单(单团预订)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddTourList(EyouSoft.Model.NewTourStructure.MTourList item)
        {
            bool result = false;
            if (item != null)
            {
                item.TourId = System.Guid.NewGuid().ToString();
                //item.OrderNo = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().CreateOrderNo("X");
                if (item.StartDate == DateTime.MinValue || item.StartDate == DateTime.MaxValue)
                    return false;
                item.IssueTime = DateTime.Now;

                result = dal.AddTourList(item);
                if (result)
                {
                    var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();
                    EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrderHandleLog(
                       new EyouSoft.Model.NewTourStructure.MOrderHandleLog()
                       {
                           CompanyId = item.Travel,
                           IssueTime = item.IssueTime,
                           LogId = System.Guid.NewGuid().ToString(),
                           OperatorId = item.OperatorId,
                           OperatorName = userInfo.ContactInfo.ContactName,
                           OrderId = item.TourId,
                           OrderNo = item.OrderNo,
                           OrderType = EyouSoft.Model.NewTourStructure.OrderSource.线路团队订单,
                           Remark = "订单接受预订"
                       }
                    );
                }
                if (result)
                {
                    //发MQ发送消息
                    EyouSoft.BLL.MQStructure.IMMessage.CreateInstance().AddMessageToWholesalersByNewOrder(item.TourId, 2);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取团队计划,订单实体
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <returns>团队计划,订单实体</returns>
        public EyouSoft.Model.NewTourStructure.MTourList GetModel(string TourId)
        {
            if (!string.IsNullOrEmpty(TourId))
            {
                return dal.GetModel(TourId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得专线商订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetZXList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetList(pageSize, pageCurrent, ref recordCount, companyId, EyouSoft.Model.NewTourStructure.RouteSource.专线商添加, search);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得地接社订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetDJList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetList(pageSize, pageCurrent, ref recordCount, companyId, EyouSoft.Model.NewTourStructure.RouteSource.地接社添加, search);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 团队订单(组团社)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetZTList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetList(pageSize, pageCurrent, ref recordCount, companyId, search);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 团队订单-运营后台
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetList(int pageSize, int pageCurrent, ref int recordCount, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            return dal.GetList(pageSize, pageCurrent, ref recordCount, search);
        }

        /// <summary>
        /// 团队订单状态更改
        /// </summary>
        /// <param name="Status">更改的状态</param> 
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool TourOrderStatusChange(EyouSoft.Model.NewTourStructure.TourOrderStatus Status, string TourId)
        {
            bool result = false;
            result = dal.TourOrderStatusChange(Status, TourId);
            if (result)
            {
                #region 获取登录用户信息
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
                }
                #endregion

                string orderNo = this.GetModel(TourId).OrderNo;
                EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrderHandleLog(new EyouSoft.Model.NewTourStructure.MOrderHandleLog()
                {
                    LogId = Guid.NewGuid().ToString(),
                    OrderId = TourId,
                    OperatorName = logWriterName,
                    OperatorId = logWriterId,
                    OrderType = EyouSoft.Model.NewTourStructure.OrderSource.线路团队订单,
                    IssueTime = DateTime.Now,
                    OrderNo = orderNo,
                    CompanyId = this.GetModel(TourId).Travel,
                    Remark = "订单状态修改为" + Status.ToString(),
                });
            }
            return result;
        }

        /// <summary>
        /// 运营前台 单团订单 专线或地接修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        public bool OrderModifyZXDJ(EyouSoft.Model.NewTourStructure.MTourList model)
        {
            bool result = false;
            if (model != null)
            {
                result = dal.OrderModifyZXDJ(model);
                if (result)
                {
                    var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();

                    EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrderHandleLog(new EyouSoft.Model.NewTourStructure.MOrderHandleLog()
                    {
                        LogId = Guid.NewGuid().ToString(),
                        OrderId = model.TourId,
                        OperatorName = userInfo.ContactInfo.ContactName,
                        OperatorId = userInfo.ID,
                        OrderType = EyouSoft.Model.NewTourStructure.OrderSource.线路团队订单,
                        IssueTime = DateTime.Now,
                        OrderNo = model.OrderNo,
                        CompanyId = model.Travel,
                        Remark = "订单修改",
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 运营前台 单团订单 组团修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        public bool OrderModifyZT(EyouSoft.Model.NewTourStructure.MTourList model)
        {
            bool result = false;
            if (model != null)
            {
                result = dal.OrderModifyZT(model);
                if (result)
                {
                    var userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();

                    EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().AddOrderHandleLog(new EyouSoft.Model.NewTourStructure.MOrderHandleLog()
                    {
                        LogId = Guid.NewGuid().ToString(),
                        OrderId = model.TourId,
                        OperatorName = userInfo.ContactInfo.ContactName,
                        OperatorId = userInfo.ID,
                        OrderType = EyouSoft.Model.NewTourStructure.OrderSource.线路团队订单,
                        IssueTime = DateTime.Now,
                        OrderNo = model.OrderNo,
                        CompanyId = model.Travel,
                        Remark = "订单修改",
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// 专线商订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public Dictionary<string, int> GetOrderBusinessCount(string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetStatisticsCount(companyId, 1);
            }
            return null;
        }
        /// <summary>
        /// 地接社订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public Dictionary<string, int> GetOrderGroundCount(string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetStatisticsCount(companyId, 2);
            }
            return null;
        }
        /// <summary>
        /// 组团社订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public Dictionary<string, int> GetOrderTravelCount(string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                return dal.GetStatisticsCount(companyId, 3);
            }
            return null;
        }
        #endregion
    }
}
