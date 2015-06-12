using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.TourStructure;


namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：线路信息业务逻辑层
    /// </summary>
    public class RouteBasicInfo:IRouteBasicInfo
    {
        private readonly EyouSoft.IDAL.TourStructure.IRouteBasicInfo dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.IRouteBasicInfo>();

        #region CreateInstance
        /// <summary>
        /// 创建线路信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TourStructure.IRouteBasicInfo CreateInstance()
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.TourStructure.IRouteBasicInfo>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 验证是否存在同名线路
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="RouteName">线路名称</param>
        /// <returns></returns>
        public bool Exists(string CompanyId, string RouteName)
        {
            if (string.IsNullOrEmpty(RouteName) || string.IsNullOrEmpty(CompanyId))
                return false;
            return dal.Exists(CompanyId, RouteName);
        }
        /// <summary>
        /// 写入标准发布线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool InsertRouteInfo(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            bool insertResult = false;

            if (routeInfo == null) { return insertResult; }

            routeInfo.ID = Guid.NewGuid().ToString();

            if (routeInfo.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
            {
                insertResult = dal.InsertRouteInfoByQuick(routeInfo);
            }
            else
            {
                insertResult = dal.InsertRouteInfoByStandard(routeInfo);
            }

            return insertResult;
        }

        /// <summary>
        /// 发送线路给专线商
        /// </summary>
        /// <param name="sendRoutes">发送线路信息业务实体</param>
        /// <returns>返回发送成功的总数</returns>
        public int SendRouteToWholesalers(IList<EyouSoft.Model.TourStructure.RouteSendList> sendRoutes)
        {
            if (sendRoutes == null || sendRoutes.Count == 0)
                return 0;
            return dal.SendRouteToWholesalers(sendRoutes);
        }
        /// <summary>
        /// 获取线路信息实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TourStructure.RouteBasicInfo GetRouteInfo(string routeId)
        {
            if (string.IsNullOrEmpty(routeId))
                return null;
            return dal.GetRouteInfo(routeId);
        }
        /// <summary>
        /// 根据指定条件获取线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =""时返回公司所有用户发布的线路,否则返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <param name="ReleaseType">线路发布类型 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.RouteBasicInfo> GetRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName, int[] AreaId, EyouSoft.Model.TourStructure.ReleaseType? ReleaseType)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> rotues = dal.GetRoutes(pageSize, pageIndex, ref recordCount, companyId, userId, routeName, routeDays, contactName, AreaId, ReleaseType, null);
            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> stands = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance().GetList(companyId);
            
            if (rotues != null && rotues.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.RouteBasicInfo tmp in rotues)
                {
                    if (tmp.PriceDetails == null || tmp.PriceDetails.Count < 1) continue;

                    foreach (EyouSoft.Model.TourStructure.RoutePriceDetail tmpPrice in tmp.PriceDetails)
                    {
                        foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand tmpStand in stands)
                        {
                            if (tmpPrice.PriceStandId == tmpStand.ID)
                            {
                                tmpPrice.PriceStandName = tmpStand.PriceStandName;
                                break;
                            }
                        }
                    }
                }
            }

            return rotues;
        }
        /// <summary>
        /// 分页获取地接线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =""时返回公司所有用户发布的线路，否则返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <param name="ReleaseType">线路发布类型 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.RouteBasicInfo> GetLocaRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName, int[] AreaId, EyouSoft.Model.TourStructure.ReleaseType? ReleaseType)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> rotues = dal.GetRoutes(pageSize, pageIndex, ref recordCount, companyId, userId, routeName, routeDays, contactName, AreaId, ReleaseType, EyouSoft.Model.SystemStructure.AreaType.地接线路);
            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> stands = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance().GetList(companyId);

            if (rotues != null && rotues.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.RouteBasicInfo tmp in rotues)
                {
                    if (tmp.PriceDetails == null || tmp.PriceDetails.Count < 1) continue;

                    foreach (EyouSoft.Model.TourStructure.RoutePriceDetail tmpPrice in tmp.PriceDetails)
                    {
                        foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand tmpStand in stands)
                        {
                            if (tmpPrice.PriceStandId == tmpStand.ID)
                            {
                                tmpPrice.PriceStandName = tmpStand.PriceStandName;
                                break;
                            }
                        }
                    }
                }
            }

            return rotues;
        }
        /// <summary>
        /// 更新标准发布的线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool UpdateRouteInfo(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            bool updateResult = false;

            if (routeInfo == null||string.IsNullOrEmpty(routeInfo.ID)) return updateResult;

            if (routeInfo.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
            {
                updateResult = dal.UpdateRouteInfoByQuick(routeInfo);
            }
            else
            {
                updateResult = dal.UpdateRouteInfoByStandard(routeInfo);
            }

            return updateResult;
        }

        /// <summary>
        /// 获取专线已接收线路数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public int GetWholesalersAcceptRouteCount(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;
            return dal.GetWholesalersAcceptRouteCount(companyId);
        }
        /// <summary>
        /// 根据指定条件获取专线已接收的线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当面页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =""时返回公司所有用户发布的线路,否则返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.RouteBasicInfo> GetWholesalersAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName,int[] AreaId)
        {
            return dal.GetWholesalersAcceptRoutes(pageSize, pageIndex, ref recordCount, companyId, userId, routeName, routeDays, contactName,AreaId);
        }
        /// <summary>
        /// 获取线路的报价详细信息集合
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.RoutePriceDetail> GetRoutePriceDetails(string routeId)
        {
            if (string.IsNullOrEmpty(routeId)) { return null; }
            string companyId;
            IList<EyouSoft.Model.TourStructure.RoutePriceDetail> details = dal.GetRoutePriceDetails(routeId,out companyId);

            IList<EyouSoft.Model.CompanyStructure.CompanyPriceStand> stands = EyouSoft.BLL.CompanyStructure.CompanyPriceStand.CreateInstance().GetList(companyId);

            if (stands != null && stands.Count > 0 && details != null && details.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.RoutePriceDetail tmpPrice in details)
                {
                    foreach (EyouSoft.Model.CompanyStructure.CompanyPriceStand tmpStand in stands)
                    {
                        if (tmpPrice.PriceStandId == tmpStand.ID)
                        {
                            tmpPrice.PriceStandName = tmpStand.PriceStandName;
                            break;
                        }
                    }
                }
            }

            return details; 
        }

        /// <summary>
        /// 获取批发商待接收线路集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称</param>
        /// <param name="routeDays">行程天数</param>
        /// <param name="contactName">联系人</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo> GetWholesalerWaitAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string routeName, int routeDays, string contactName)
        {
            return dal.GetWholesalerWaitAcceptRoutes(pageSize, pageIndex, ref recordCount, companyId, routeName, routeDays, contactName);
        }
        /// <summary>
        /// 批发商确认接收线路
        /// </summary>
        /// <param name="userId">接收线路的用户编号</param>
        /// <param name="waitAcceptIds">待接收信息编号(即发送线路给批发商的发送编号)</param>
        /// <returns></returns>
        public int AcceptRoutes(int userId, string[] waitAcceptIds)
        {
            return dal.AcceptRoutes(userId, waitAcceptIds);
        }

        /// <summary>
        /// 获取批发商等待接收的线路数量
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int GetWholesalerWaitAcceptRouteCount(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
                return 0;
            return dal.GetWholesalerWaitAcceptRouteCount(companyId);
        }
        /// <summary>
        /// 删除线路信息
        /// </summary>
        /// <param name="routeId">要删除的线路编号</param>
        /// <returns></returns>
        public bool DeleteRoutes(params string[] routeId)
        {
            bool tmp = false;

            if (routeId.Length < 1) { return tmp; }

            EyouSoft.SSOComponent.Entity.UserInfo userInfo = EyouSoft.Security.Membership.UserProvider.GetUser();

            if (userInfo == null) { return tmp; }

            tmp= dal.DeleteByVirtual(userInfo.CompanyID, true, routeId);

            userInfo = null;

            return tmp;
        }
        #endregion
    }
}
