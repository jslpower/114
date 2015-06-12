using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 线路项目安排基类
    /// </summary>
    [Serializable]
    public class ServiceStandard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceStandard() { }

        #region 属性
        /// <summary>
        /// 住宿
        /// </summary>
        public string ResideContent
        {
            get;
            set;
        }
        /// <summary>
        /// 用餐
        /// </summary>
        public string DinnerContent
        {
            get;
            set;
        }
        /// <summary>
        /// 景点
        /// </summary>
        public string SightContent
        {
            get;
            set;
        }
        /// <summary>
        /// 用车
        /// </summary>
        public string CarContent
        {
            get;
            set;
        }
        /// <summary>
        /// 导游
        /// </summary>
        public string GuideContent
        {
            get;
            set;
        }
        /// <summary>
        /// 往返交通
        /// </summary>
        public string TrafficContent
        {
            get;
            set;
        }
        /// <summary>
        /// 包含项目其它内容
        /// </summary>
        public string IncludeOtherContent
        {
            get;
            set;
        }
        /// <summary>
        /// 不包含项目
        /// </summary>
        public string NotContainService
        {
            get;
            set;
        }

        /*
        /// <summary>
        /// 自费项目
        /// </summary>
        public string ExpenseItem
        {
            get;
            set;
        }
        /// <summary>
        /// 儿童安排(对应原tbl_RouteInfo,tbl_TourList表SpecialInfo字段)
        /// </summary>
        public string ChildrenInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 购物安排(对应原tbl_RouteInfo,tbl_TourList表SightInfo字段)
        /// </summary>
        public string ShoppingInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 赠送项目(对应原tbl_RouteInfo,tbl_TourList表Remark字段)
        /// </summary>
        public string GiftInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string NoticeProceeding
        {
            get;
            set;
        }*/
        /// <summary>
        /// 温馨提醒
        /// </summary>
        public string SpeciallyNotice
        {
            get;
            set;
        }
        #endregion
    }
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：标准发布线路项目安排
    /// </summary>
    [Serializable]
    public class RouteServiceStandard : ServiceStandard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteServiceStandard() { }

        #region 属性
        /// <summary>
        /// 线路基本信息编号
        /// </summary>
        public string RouteBasicID
        {
            get;
            set;
        }
        #endregion
    }

    /// <summary>
    /// 标准发布团队服务标准
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourServiceStandard : ServiceStandard
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public TourServiceStandard() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
    }
}
