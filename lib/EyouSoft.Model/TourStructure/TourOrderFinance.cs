using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TourStructure
{
    /// <summary>
    /// 订单财务业务实体
    /// </summary>
    /// 周文超 2010-06-22
    [Serializable]
    public class TourOrderFinance
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TourOrderFinance() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OrderId">订单编号</param>
        /// <param name="OrderNo">订单号</param>
        /// <param name="TourId">团队编号</param>
        /// <param name="TourNo">团号</param>
        /// <param name="TourType">团队类型</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="LeaveDate">出发日期</param>
        /// <param name="BuyCompanyID">预订人公司ID</param>
        /// <param name="BuyCompanyName">预订人公司名称</param>
        /// <param name="PeopleNumber">订单人数</param>
        /// <param name="ContactName">联系人</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="ContactFax">传真</param>
        /// <param name="OperatorID">操作员ID</param>
        /// <param name="OperatorName">操作员名字</param>
        /// <param name="CompanyID">订单所属公司ID</param>
        /// <param name="SumPrice">订单总金额</param>
        public TourOrderFinance(string OrderId, string OrderNo, string TourId, string TourNo, Model.TourStructure.TourType TourType
            , string RouteName, DateTime LeaveDate, string BuyCompanyID, string BuyCompanyName, int PeopleNumber, string ContactName
            , string ContactTel, string ContactFax, string OperatorID, string OperatorName, string CompanyID, decimal SumPrice)
        {
            this.OrderId = OrderId;
            this.OrderNo = OrderNo;
            this.TourId = TourId;
            this.TourNo = TourNo;
            this.TourType = TourType;
            this.RouteName = RouteName;
            this.LeaveDate = LeaveDate;
            this.BuyCompanyID = BuyCompanyID;
            this.BuyCompanyName = BuyCompanyName;
            this.PeopleNumber = PeopleNumber;
            this.ContactName = ContactName;
            this.ContactTel = ContactTel;
            this.ContactFax = ContactFax;
            this.OperatorID = OperatorID;
            this.OperatorName = OperatorName;
            this.CompanyID = CompanyID;
            this.SumPrice = SumPrice;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OrderId">订单ID</param>
        /// <param name="PeopleNumber">订单人数</param>
        /// <param name="ContactName">联系人</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="ContactFax">传真</param>
        /// <param name="SumPrice">订单总金额</param>
        public TourOrderFinance(string OrderId, int PeopleNumber, string ContactName, string ContactTel, string ContactFax, decimal SumPrice)
        {
            this.OrderId = OrderId;
            this.PeopleNumber = PeopleNumber;
            this.ContactName = ContactName;
            this.ContactTel = ContactTel;
            this.ContactFax = ContactFax;
            this.SumPrice = SumPrice;
        }

        #endregion

        #region Model

        /// <summary>
        /// 订单财务编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 团号
        /// </summary>
        public string TourNo { get; set; }

        /// <summary>
        /// 团队类型
        /// </summary>
        public Model.TourStructure.TourType TourType { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime LeaveDate { get; set; }

        /// <summary>
        /// 预订单位ID
        /// </summary>
        public string BuyCompanyID { get; set; }

        /// <summary>
        /// 预定单位名称
        /// </summary>
        public string BuyCompanyName { get; set; }

        /// <summary>
        /// 订单总人数
        /// </summary>
        public int PeopleNumber { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string ContactFax { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// 操作员名字
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 订单所属公司ID
        /// </summary>
        public string CompanyID { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal SumPrice { get; set; }

        /// <summary>
        /// 交易成功时间
        /// </summary>
        public DateTime SuccessTime { get; set; }

        #endregion
    }
}
