using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 线路信息抽象类
    /// </summary>
    [Serializable]
    public abstract class RouteObject
    {
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }
        /// <summary>
        /// 天数(默认为0)
        /// </summary>
        public int TourDays
        {
            get;
            set;
        }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaId 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// 线路区域类型
        /// </summary>
        public SystemStructure.AreaType AreaType 
        { get; set; }
    }
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：线路基本信息
    /// </summary>
    [Serializable]
    public class RouteBasicInfo : RouteObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteBasicInfo() { }

        #region 属性
        /// <summary>
        /// 线路编号
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司ID(默认为0)   单聚集索引
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID(默认为0)
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 线路负责人
        /// </summary>
        public string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string ContactTel
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人MQ用户名
        /// </summary>
        public string ContactUserName
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人MQID(默认为0)
        /// </summary>
        public string ContactMQID
        {
            get;
            set;
        }        
        /// <summary>
        /// 发布时间(默认为getdate())
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 线路发布类型
        /// </summary>
        public ReleaseType ReleaseType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否已被专线接收过
        /// </summary>
        public bool IsAccept
        {
            get;
            set;
        }
        /// <summary>
        /// 行程是否异常(百分制)
        /// </summary>
        public int StandardPlanError
        {
            get;
            set;
        }
        /// <summary>
        /// 价格是否异常(百分制)
        /// </summary>
        public int TourPriceError
        {
            get;
            set;
        }
        /// <summary>
        /// 出港城市编号
        /// </summary>
        public int LeaveCityId
        {
            get;
            set;
        }
        #endregion

        #region 附加属性
        private IList<RoutePriceDetail> _pricedetails = new List<RoutePriceDetail>(); //价格明细
        private RouteServiceStandard _servicestandard = new RouteServiceStandard();//服务标准
        private IList<RouteStandardPlan> _standardplans = new List<RouteStandardPlan>();//标准版线路行程
        /// <summary>
        /// 报价明细
        /// </summary>
        public IList<RoutePriceDetail> PriceDetails
        {
            get
            {
                return _pricedetails;
            }
            set
            {
                _pricedetails = value;
            }
        }
        /// <summary>
        /// 服务标准
        /// </summary>
        public RouteServiceStandard ServiceStandard
        {
            get
            {
                return _servicestandard;
            }
            set
            {
                _servicestandard = value;
            }
        }
        /// <summary>
        /// 标准版线路行程
        /// </summary>
        public IList<RouteStandardPlan> StandardPlans
        {
            get
            {
                return _standardplans;
            }
            set
            {
                _standardplans = value;
            }
        }
        /// <summary>
        /// 快速版线路行程
        /// </summary>
        public string QuickPlan { get; set; }
        /*
        /// <summary>
        /// 出港城市集合
        /// </summary>
        public IList<int> LeaveCity { get; set; }*/
        /// <summary>
        /// 销售城市集合
        /// </summary>
        public IList<int> SaleCity { get; set; }
        /// <summary>
        /// 线路主题集合
        /// </summary>
        public IList<int> RouteTheme { get; set; }
        #endregion

    }

    #region 批发商待接收的线路信息业务实体 WholesalerWaitAcceptRouteInfo
    /// <summary>
    /// 批发商待接收的线路信息业务实体
    /// </summary>
    /// Author:鲁功源 2010-05-26
    [Serializable]
    public class WholesalerWaitAcceptRouteInfo : RouteBasicInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public WholesalerWaitAcceptRouteInfo() { }

        /// <summary>
        /// 待接收信息编号(即发送线路给批发商的发送编号)
        /// </summary>
        public string WaitAcceptId { get; set; }
    }
    #endregion 批发商待接收的线路信息业务实体 WholesalerWaitAcceptRouteInfo
}
