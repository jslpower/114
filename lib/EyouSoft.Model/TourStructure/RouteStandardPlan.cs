using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 标准发布线路行程
    /// </summary>
    [Serializable]
    public class RouteStandardPlan : StandardPlan
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteStandardPlan()
        { }
        /// <summary>
        /// 线路基本信息编号
        /// </summary>
        public string RouteBasicID
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 标准发布团队行程
    /// </summary>
    /// 周文超 2010-05-13
    [Serializable]
    public class TourStandardPlan : StandardPlan
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TourStandardPlan()
        { }

        /// <summary>
        /// 团队基本信息编号
        /// </summary>
        public string TourBasicID
        {
            set;
            get;
        }
    }

    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：发布线路行程基类
    /// </summary>
    [Serializable]
    public class StandardPlan
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public StandardPlan() { }

        #region 属性   
        /*
        /// <summary>
        /// 行程表ID
        /// </summary>
        public string PlanID
        {
            get;
            set;
        }*/
        /// <summary>
        /// 行程区间
        /// </summary>
        public string PlanInterval
        {
            get;
            set;
        }
        /// <summary>
        /// 交通工具
        /// </summary>
        public string Vehicle
        {
            get;
            set;
        }
        /// <summary>
        /// 班次
        /// </summary>
        public string TrafficNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 住宿
        /// </summary>
        public string House
        {
            get;
            set;
        }
        /// <summary>
        /// 用餐
        /// </summary>
        public string Dinner
        {
            get;
            set;
        }
        /// <summary>
        /// 行程内容
        /// </summary>
        public string PlanContent
        {
            get;
            set;
        }
        /// <summary>
        /// 第几天行程(从1开始,为1,2,3...)
        /// </summary>
        public int PlanDay
        {
            get;
            set;
        }
        #endregion
    }
}
