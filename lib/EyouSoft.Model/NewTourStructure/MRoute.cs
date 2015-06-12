using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

///
/// 创建人：郑付杰
/// 创建时间：2011-12-16

namespace EyouSoft.Model.NewTourStructure
{
    using System.Diagnostics.CodeAnalysis;

    #region 线路信息

    /// <summary>
    /// 线路
    /// </summary>
    public class MRoute : MBaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public MRoute()
        {
        }

        /// <summary>
        /// 自增编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 线路发布类型
        /// </summary>
        public TourStructure.ReleaseType ReleaseType { get; set; }

        /// <summary>
        /// 行程(简易版)
        /// </summary>
        public string FastPlan { get; set; }

        /// <summary>
        /// 报价(散客版)
        /// </summary>
        public string FitQuotation { get; set; }

        /// <summary>
        /// B2C线路别名
        /// </summary>
        public string B2CRouteName { get; set; }

        /// <summary>
        /// 上下架状态
        /// </summary>
        public RouteStatus RouteStatus { get; set; }

        /// <summary>
        /// 团队参考价格
        /// </summary>
        public decimal IndependentGroupPrice { get; set; }

        /// <summary>
        /// 线路来源
        /// </summary>
        public RouteSource RouteSource { get; set; }

        /// <summary>
        /// 散客报名无需成团，铁定发团
        /// </summary>
        public bool IsCertain { get; set; }

        /// <summary>
        /// 提前几天报名
        /// </summary>
        public int AdvanceDayRegistration { get; set; }

        /// <summary>
        /// 最少成团人数
        /// </summary>
        public int GroupNum { get; set; }

        /// <summary>
        /// 前台详细成人价格
        /// </summary>
        public decimal PublicAuditPrice { get; set; }

        #region 线路统计属性

        /// <summary>
        /// 成人市场价
        /// </summary>
        public decimal RetailAdultPrice { get; set; }

        /// <summary>
        /// 儿童市场价
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }

        /// <summary>
        /// 团队班次文字描述(列表)
        /// </summary>
        public string TeamPlanDes { get; set; }

        #endregion

        /// <summary>
        /// 公司类型
        /// </summary>
        public CompanyStructure.CompanyType CompanyType { get; set; }
    }

    #endregion

    #region 线路打印行程单实体

    /// <summary>
    /// 线路打印行程单
    /// </summary>
    public class MRoutePrint
    {
        /// <summary>
        /// 
        /// </summary>
        public MRoutePrint()
        {
        }

        /// <summary>
        /// 发布公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 公司联系人
        /// </summary>
        public string CompanyContact { get; set; }

        /// <summary>
        /// 发布公司联系MQ
        /// </summary>
        public string CompanyContactMq { get; set; }

        /// <summary>
        /// 公司logo
        /// </summary>
        public string CompanyLogo { get; set; }

        /// <summary>
        /// 专线名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// 出发城市
        /// </summary>
        public string StartCityName { get; set; }

        /// <summary>
        /// 返程城市
        /// </summary>
        public string EndCityName { get; set; }

        /// <summary>
        /// 出发交通
        /// </summary>
        public TrafficType StartTraffic { get; set; }

        /// <summary>
        /// 返程交通
        /// </summary>
        public TrafficType EndTraffic { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 夜数
        /// </summary>
        public int Late { get; set; }

        /// <summary>
        /// 提前几天报名
        /// </summary>
        public int AdvanceDayRegistration { get; set; }

        /// <summary>
        /// 浏览城市
        /// </summary>
        public string BrowseCity { get; set; }

        /// <summary>
        /// 签证城市 
        /// </summary>
        public string VisaCity { get; set; }

        /// <summary>
        /// 成人最高市场价格
        /// </summary>
        public decimal RetailAdultPrice { get; set; }

        /// <summary>
        /// 儿童最高市场价格
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }

        /// <summary>
        /// 成人最低市场价格
        /// </summary>
        public decimal MinRetailAdultPrice { get; set; }

        /// <summary>
        /// 儿童最低市场价格
        /// </summary>
        public decimal MinRetailChildrenPrice { get; set; }

        /// <summary>
        /// 最小成团人数
        /// </summary>
        public int GroupNum { get; set; }

        /// <summary>
        /// 团队参考价格
        /// </summary>
        public decimal ReferencePrice { get; set; }

        /// <summary>
        /// 线路订金成人
        /// </summary>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// 线路订金儿童
        /// </summary>
        public decimal ChildrenPrice { get; set; }

        /// <summary>
        /// 出团计划
        /// </summary>
        public ArrayList TeamPlanDes { get; set; }

        /// <summary>
        /// 线路特色
        /// </summary>
        public string Characteristic { get; set; }

        /// <summary>
        /// 发布版本
        /// </summary>
        public EyouSoft.Model.TourStructure.ReleaseType ReleaseType { get; set; }

        /// <summary>
        /// 快速行程
        /// </summary>
        public string FastPlan { get; set; }

        /// <summary>
        /// 报价(散客版)
        /// </summary>
        public string FitQuotation { get; set; }

        /// <summary>
        /// 行程
        /// </summary>
        public IList<MStandardPlan> StandardPlan { get; set; }

        /// <summary>
        /// 服务标准
        /// </summary>
        public MServiceStandard ServiceStandard { get; set; }

        /// <summary>
        /// 公司帐号
        /// </summary>
        public string CompanyAccount { get; set; }

        /// <summary>
        /// 个人帐号
        /// </summary>
        public ArrayList PersonalAccount { get; set; }

        /// <summary>
        /// 支付宝帐号
        /// </summary>
        public string AlipayAccount { get; set; }

        /// <summary>
        /// 线路类型
        /// </summary>
        public SystemStructure.AreaType RouteType { get; set; }

        /// <summary>
        /// 线路添加来源
        /// </summary>
        public RouteSource RouteSource { get; set; }
    }

    #endregion

    #region  线路.散拼共有属性

    /// <summary>
    /// 线路.散拼共有属性
    /// </summary>
    public abstract class MBaseInfo
    {
        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }

        /// <summary>
        /// 线路名称(b2b)
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 线路类型
        /// </summary>
        public SystemStructure.AreaType RouteType { get; set; }

        /// <summary>
        /// 专线类型
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 专线名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 线路特色
        /// </summary>
        public string Characteristic { get; set; }

        /// <summary>
        /// 线路配图
        /// </summary>
        public string RouteImg { get; set; }

        /// <summary>
        /// 400*300缩略图
        /// </summary>
        public string RouteImg1 { get; set; }

        /// <summary>
        /// 1024*768缩略图
        /// </summary>
        public string RouteImg2 { get; set; }

        /// <summary>
        /// 出发城市
        /// </summary>
        public int StartCity { get; set; }

        /// <summary>
        /// 出发城市名称
        /// </summary>
        public string StartCityName { get; set; }

        /// <summary>
        /// 返回城市
        /// </summary>
        public int EndCity { get; set; }

        /// <summary>
        /// 返回城市名称
        /// </summary>
        public string EndCityName { get; set; }

        /// <summary>
        /// 出发大交通
        /// </summary>
        public TrafficType StartTraffic { get; set; }

        /// <summary>
        /// 返程大交通
        /// </summary>
        public TrafficType EndTraffic { get; set; }

        /// <summary>
        /// 住宿(天)
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 住宿(晚)
        /// </summary>
        public int Late { get; set; }

        /// <summary>
        /// 成人线路订金(国际游)
        /// </summary>
        public decimal AdultPrice { get; set; }

        /// <summary>
        /// 儿童线路订金(国际游)
        /// </summary>
        public decimal ChildrenPrice { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendType RecommendType { get; set; }

        /// <summary>
        /// B2B显示控制
        /// </summary>
        public RouteB2BDisplay B2B { get; set; }

        /// <summary>
        /// B2B排序值
        /// </summary>
        public int B2BOrder { get; set; }

        /// <summary>
        /// B2C显示控制
        /// </summary>
        public RouteB2CDisplay B2C { get; set; }

        /// <summary>
        /// B2C排序值
        /// </summary>
        public int B2COrder { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int ClickNum { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }

        /// <summary>
        /// 销售商须知(只有组团社能看到)
        /// </summary>
        public string VendorsNotes { get; set; }

        /// <summary>
        /// 发布商编号
        /// </summary>
        public string Publishers { get; set; }

        /// <summary>
        /// 发布商名称
        /// </summary>
        public string PublishersName { get; set; }

        /// <summary>
        /// 发布商品牌名称
        /// </summary>
        public string CompanyBrand { get; set; }

        /// <summary>
        /// 公司等级
        /// </summary>
        public Model.CompanyStructure.CompanyLev CompanyLev { get; set; }

        /// <summary>
        /// 发布商简称
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 发布人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 发布人名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 主题关系
        /// </summary>
        public IList<MThemeControl> Themes { get; set; }

        /// <summary>
        /// 销售城市关系
        /// </summary>
        public IList<MCityControl> Citys { get; set; }

        /// <summary>
        /// 游览国家关系
        /// </summary>
        public IList<MBrowseCountryControl> BrowseCountrys { get; set; }

        /// <summary>
        /// 游览城市关系
        /// </summary>
        public IList<MBrowseCityControl> BrowseCitys { get; set; }

        /// <summary>
        /// 标准行程
        /// </summary>
        public IList<MStandardPlan> StandardPlans { get; set; }

        /// <summary>
        /// 服务项目
        /// </summary>
        public MServiceStandard ServiceStandard { get; set; }

        /// <summary>
        /// 是否免签
        /// </summary>
        public bool IsNotVisa { get; set; }
    }

    #endregion

    #region 线路,散拼(主题关系,游览国家关系,游览城市关系,销售城市关系,标准行程,服务项目)

    /// <summary>
    /// 主题关系
    /// </summary>
    public class MThemeControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MThemeControl()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 主题编号
        /// </summary>
        public string ThemeId { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        public string ThemeName { get; set; }
    }

    /// <summary>
    /// 销售城市关系
    /// </summary>
    public class MCityControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MCityControl()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }

    /// <summary>
    /// 游览国家关系
    /// </summary>
    public class MBrowseCountryControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MBrowseCountryControl()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 国家编号
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// 是否签证(默认不签证)
        /// </summary>
        public bool IsVisa { get; set; }
    }

    /// <summary>
    /// 游览城市关系
    /// </summary>
    public class MBrowseCityControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MBrowseCityControl()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 县区编号
        /// </summary>
        public int CountyId { get; set; }

        /// <summary>
        /// 县区名称
        /// </summary>
        public string CountyName { get; set; }
    }

    /// <summary>
    /// 标准行程
    /// </summary>
    public class MStandardPlan
    {
        /// <summary>
        /// 
        /// </summary>
        public MStandardPlan()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 途经
        /// </summary>
        public string PlanInterval { get; set; }

        /// <summary>
        /// 交通工具
        /// </summary>
        public TrafficType Vehicle { get; set; }

        /// <summary>
        /// 住宿
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// 早
        /// </summary>
        public bool Early { get; set; }

        /// <summary>
        /// 中
        /// </summary>
        public bool Center { get; set; }

        /// <summary>
        /// 晚
        /// </summary>
        public bool Late { get; set; }

        /// <summary>
        /// 第几天行程
        /// </summary>
        public int PlanDay { get; set; }

        /// <summary>
        /// 行程内容
        /// </summary>
        public string PlanContent { get; set; }
    }

    /// <summary>
    /// 服务项目
    /// </summary>
    public class MServiceStandard
    {
        /// <summary>
        /// 
        /// </summary>
        public MServiceStandard()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 住宿
        /// </summary>
        public string ResideContent { get; set; }

        /// <summary>
        /// 用餐
        /// </summary>
        public string DinnerContent { get; set; }

        /// <summary>
        /// 景点
        /// </summary>
        public string SightContent { get; set; }

        /// <summary>
        /// 用车
        /// </summary>
        public string CarContent { get; set; }

        /// <summary>
        /// 导游
        /// </summary>
        public string GuideContent { get; set; }

        /// <summary>
        /// 往返交通
        /// </summary>
        public string TrafficContent { get; set; }

        /// <summary>
        /// 其他包含
        /// </summary>
        public string IncludeOtherContent { get; set; }

        /// <summary>
        /// 报价不含
        /// </summary>
        public string NotContainService { get; set; }

        /// <summary>
        /// 赠送项目
        /// </summary>
        public string GiftInfo { get; set; }

        /// <summary>
        /// 儿童及其他安排
        /// </summary>
        public string ChildrenInfo { get; set; }

        /// <summary>
        /// 购物安排
        /// </summary>
        public string ShoppingInfo { get; set; }

        /// <summary>
        /// 自费项目
        /// </summary>
        public string ExpenseItem { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
    }

    #endregion

    #region 网店线路信息

    /// <summary>
    /// 网店线路实体
    /// </summary>
    public class MShopRoute
    {
        /// <summary>
        /// 
        /// </summary>
        public MShopRoute()
        {
        }

        /// <summary>
        /// 线路编号
        /// </summary>
        public string RouteId { get; set; }

        /// <summary>
        /// 散拼计划编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendType RecommendType { get; set; }

        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LeaveDate { get; set; }

        /// <summary>
        /// 余位
        /// </summary>
        public int MoreThan { get; set; }

        /// <summary>
        /// 成人市场价
        /// </summary>
        public decimal RetailAdultPrice { get; set; }

        /// <summary>
        /// 成人结算价
        /// </summary>
        public decimal SettlementAudltPrice { get; set; }

        /// <summary>
        /// 儿童市场价
        /// </summary>
        public decimal RetailChildrenPrice { get; set; }

        /// <summary>
        /// 儿童结算价
        /// </summary>
        public decimal SettlementChildrenPrice { get; set; }

        /// <summary>
        /// 单房差
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 团队班次文字描述(列表)
        /// </summary>
        public string TeamPlanDes { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int ClickNum { get; set; }

        /// <summary>
        /// 出发城市名称
        /// </summary>
        public string StartCityName { get; set; }

        /// <summary>
        /// 线路来源
        /// </summary>
        public RouteSource RouteSource { get; set; }

        /// <summary>
        /// 团队参考价格
        /// </summary>
        public decimal IndependentGroupPrice { get; set; }
    }

    #endregion

    #region 线路搜索实体

    /// <summary>
    /// 线路搜索实体
    /// </summary>
    public class MRouteSearch
    {
        /// <summary>
        /// 
        /// </summary>
        public MRouteSearch()
        {
        }

        /// <summary>
        /// 标题,线路特色/标题,途径区域,特色/标题
        /// </summary>
        public string RouteKey { get; set; }

        /// <summary>
        /// 价格(默认全部)
        /// </summary>
        public PublicCenterPrice Price { get; set; }

        /// <summary>
        /// 天数(选择)
        /// </summary>
        public PublicCenterRouteDay RouteDay { get; set; }

        /// <summary>
        /// 天数(输入)
        /// </summary>
        public int DayNum { get; set; }

        /// <summary>
        /// 主题(默认全部)
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// 线路类型
        /// </summary>
        public SystemStructure.AreaType? RouteType { get; set; }

        /// <summary>
        /// 专线(默认全部)
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 出发城市(默认全部)
        /// </summary>
        public int StartCity { get; set; }

        /// <summary>
        /// 出发城市名称
        /// </summary>
        public string StartCityName { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 出团月份 0:全部,1:无出团计划 3：月份
        /// 默认0：全部
        /// 值在3的情况下,要给Year,Month属性赋值
        /// </summary>
        public int LeaveMonth { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 线路状态
        /// </summary>
        public RouteStatus? RouteStatus { get; set; }

        /// <summary>
        /// 线路推荐状态
        /// </summary>
        public RecommendType? RecommendType { get; set; }

        /// <summary>
        /// 专线商编号
        /// </summary>
        public string Publishers { get; set; }

        /// <summary>
        /// 线路来源
        /// </summary>
        public RouteSource? RouteSource { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 专线发布商所在省份
        /// </summary>
        public int PublishersProvinceId { get; set; }

        /// <summary>
        /// 专线发布商所在城市
        /// </summary>
        public int PublishersCityId { get; set; }
    }

    #endregion

    #region 推荐线路查询实体

    /// <summary>
    /// 推荐线路查询实体
    /// </summary>
    public class MTuiJianRouteSearch
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 销售城市编号集合
        /// </summary>
        public int[] SellCityIds { get; set; }

        /// <summary>
        /// 线路区域集合
        /// </summary>
        public int[] AreaIds { get; set; }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public RecommendType? Type { get; set; }

        /// <summary>
        /// B2B显示控制
        /// </summary>
        public RouteB2BDisplay? B2BDisplay { get; set; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }
    }

    #endregion
}