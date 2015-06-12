using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ScenicStructure
{
    /// <summary>
    /// 景区门票
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    [Serializable]
    public class MScenicTickets:MScenicBase
    {
        public MScenicTickets() { }
        /// <summary>
        /// 门票编号
        /// </summary>
        public string TicketsId { get; set; }
        /// <summary>
        /// 门票类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 门市价
        /// </summary>
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// 网站优惠价
        /// </summary>
        public decimal WebsitePrices { get; set; }
        /// <summary>
        /// 市场限制最低价
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 同行分销价
        /// </summary>
        public decimal DistributionPrice { get; set; }
        /// <summary>
        /// 最少限制（张/套）
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public ScenicPayment Payment { get; set; }
        /// <summary>
        /// 票价有效时间_始
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 票价游戏时间_止
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 门票说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 同业销售须知 （只有同业分销商能看到）
        /// </summary>
        public string SaleDescription { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ExamineStatus ExamineStatus { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ScenicTicketsStatus Status { get; set; }
        /// <summary>
        /// 自定义排序
        /// </summary>
        public int CustomOrder { get; set; }
        /// <summary>
        /// 发布公司
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }

    /// <summary>
    /// B2B前端-特价门票实体
    /// </summary>
    [Serializable]
    public class MScenicTicketsSale
    {
        public MScenicTicketsSale() { }
        /// <summary>
        /// 景区自增编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 景区编号
        /// </summary>
        public string ScenicId { get; set; }
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 门票编号
        /// </summary>
        public string TicketsId { get; set; }
        /// <summary>
        /// 门票类型名称
        /// </summary>
        public string TicketsName { get; set; }
        /// <summary>
        /// 门市价
        /// </summary>
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// 网站优惠价
        /// </summary>
        public decimal WebsitePrices { get; set; }
        /// <summary>
        /// 景区形象图片
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 景区形象缩略图
        /// </summary>
        public string ThumbAddress { get; set; }
    }

    /// <summary>
    /// B2B前端-特价门票搜索实体
    /// </summary>
    [Serializable]
    public class MSearchScenicTicketsSale
    {
        /// <summary>
        /// 省份
        /// </summary>
        public int? ProvinceId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public int? CountyId { get; set; }
        /// <summary>
        /// B2B显示控制
        /// </summary>
        public ScenicB2BDisplay? B2B { get; set; }
        /// <summary>
        /// B2C显示控制
        /// </summary>
        public ScenicB2CDisplay? B2C { get; set; }
    }

    /// <summary>
    /// 景区门票搜索实体
    /// </summary>
    [Serializable]
    public class MSearchScenicTickets
    {
        public MSearchScenicTickets(){}
        /// <summary>
        /// 景区编号
        /// </summary>
        public string ScenicId { get; set; }
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 门票类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ScenicTicketsStatus? Status { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public ExamineStatus? ExamineStatus { get; set; }
        /// <summary>
        /// B2B显示控制
        /// </summary>
        public ScenicB2BDisplay? B2B { get; set; }
        /// <summary>
        /// B2C显示控制
        /// </summary>
        public ScenicB2CDisplay? B2C { get; set; }
        /// <summary>
        /// 门票类型名称
        /// </summary>
        public string TicketsName { get; set; }

    }
}
