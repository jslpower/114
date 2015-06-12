using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店-团队定制实体
    /// </summary>
    /// 鲁功源  2010-12-01
    [Serializable]
    public class HotelTourCustoms
    {
        #region 构造函数
        public HotelTourCustoms() { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 酒店编号
        /// </summary>
        public string HotelCode
        {
            get;
            set;
        }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName
        {
            get;
            set;
        }
        /// <summary>
        /// 酒店星级[枚举]
        /// </summary>
        public EyouSoft.HotelBI.HotelRankEnum HotelStar
        {
            get;
            set;
        }
        /// <summary>
        /// 城市三字码
        /// </summary>
        public string CityCode
        {
            get;
            set;
        }
        /// <summary>
        /// 地理位置要求
        /// </summary>
        public string LocationAsk
        {
            get;
            set;
        }
        /// <summary>
        /// 房间要求
        /// </summary>
        public string RoomAsk
        {
            get;
            set;
        }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime? LiveStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 离店时间
        /// </summary>
        public DateTime? LiveEndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 房间数
        /// </summary>
        public int RoomCount
        {
            get;
            set;
        }
        /// <summary>
        /// 入住人数
        /// </summary>
        public int PeopleCount
        {
            get;
            set;
        }
        /// <summary>
        /// 团队预算最小值
        /// </summary>
        public decimal BudgetMin
        {
            get;
            set;
        }
        /// <summary>
        /// 团队预算最大值
        /// </summary>
        public decimal BudgetMax
        {
            get;
            set;
        }
        /// <summary>
        /// 支付方式[枚举]
        /// </summary>
        public EyouSoft.HotelBI.HBEPaymentType Payment
        {
            get;
            set;
        }
        /// <summary>
        /// 宾客类型[枚举]
        /// </summary>
        public EyouSoft.HotelBI.HBEGuestTypeIndicator? GuestType
        {
            get;
            set;
        }
        /// <summary>
        /// 团队类型[枚举]
        /// </summary>
        public TourTypeList? TourType
        {
            get;
            set;
        }
        /// <summary>
        /// 其他要求
        /// </summary>
        public string OtherRemark
        {
            get;
            set;
        }
        /// <summary>
        /// 记录添加时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 处理状态[枚举]
        /// </summary>
        public EyouSoft.Model.HotelStructure.OrderStateList TreatState
        {
            get;
            set;
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? TreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 处理人编号
        /// </summary>
        public int OperatorId
        {
            get;
            set;
        }

        /// <summary>
        /// 采购商联系人信息
        /// </summary>
        public CompanyStructure.ContactPersonInfo Contact { get; set; }

        #endregion

    }

    #region 枚举定义
    /// <summary>
    /// 团队类型
    /// </summary>
    public enum TourTypeList
    {
        /// <summary>
        /// 会议团
        /// </summary>
        会议团 = 1,
        /// <summary>
        /// 旅游团
        /// </summary>
        旅游团 = 2,
        /// <summary>
        /// 其他
        /// </summary>
        其他
    }
    #endregion

    #region 团队定制搜索实体
    /// <summary>
    /// 团队定制搜索实体
    /// </summary>
    [Serializable]
    public class SearchTourCustomsInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SearchTourCustomsInfo() { }
        #endregion

        #region 属性

        /// <summary>
        /// 采购商编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName
        {
            get;
            set;
        }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode
        {
            get;
            set;
        }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode
        {
            get;
            set;
        }
        /// <summary>
        /// 入住开始时间
        /// </summary>
        public DateTime? CheckInSDate
        {
            get;
            set;
        }
        /// <summary>
        /// 入住结束时间
        /// </summary>
        public DateTime? CheckInEDate
        {
            get;
            set;
        }
        /// <summary>
        /// 离店开始时间
        /// </summary>
        public DateTime? CheckOutSDate
        {
            get;
            set;
        }
        /// <summary>
        /// 离店结束时间
        /// </summary>
        public DateTime? CheckOutEDate
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// 酒店-团队定制订单回复实体
    /// </summary>
    /// zhangzy  2010-12-10
    [Serializable]
    public class HotelTourCustomsAsk
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 酒店-团队定制订单ID
        /// </summary>
        public int TourOrderID { get; set; }
        /// <summary>
        /// 回复人姓名
        /// </summary>
        public string AskName { get; set; }
        /// <summary>
        /// 回复时间
        /// </summary>
        public string AskTime { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string AskContent { get; set; }
    }

}
