using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Common
{
    /// <summary>
    /// 平台各个页面的标题、KeyWords、Description资源
    /// </summary>
    public class PageTitle
    {
        public const string Home_Title = "旅游同业114_{0}旅游信息_{0}旅游线路_{0}旅游攻略_{0}旅游景区_机票预订_酒店预订";
        public const string Home_Keywords = "{0},旅行社,旅游信息,旅游线路,旅游资讯,旅游同业,特价机票,酒店预订";
        public const string Home_Des = "旅游同业114{0}站汇集{0}地区旅游组团社,地接社及专线商的所有旅游线路信息和产品报价,可查询{0}地区的特价酒店,团队机票,旅游线路报价和旅游供求等信息,并以同行价格预订.也可在线发布旅游信息.同业114旅游产品交易平台-扩大您的旅游圈.";


        #region 线路    
        //{0}{1}{2}替换为当前销售城市
        //线路首页                          
        public const string Route_Title = "{0}旅游线路大全_{1}旅游线路推荐_{2}旅游报价_同业114旅游线路频道。";
        public const string Route_Keywords = "旅游线路大全,旅游线路报价,旅游线路安排,旅游线路发布,春节旅游线路,国外旅游线路,组团线路,旅行社线路,同行线路,欧洲旅游,台湾旅游,出境旅游,国内专线,国际专线,旅游专线商,旅游地接社.";
        public const string Route_Des = "同业114旅游线路频道,提供{0}旅游专线商的最新线路报价,包括国内长线,国际线和周边游线路,欧洲旅游,香港台湾旅游,春节旅游等热门旅游线路,还有全国地接旅行社黄页可供查询,方便组团社和地接社的交流和旅游线路安排.";
        //线路列表页 
        public const string RouteList_Title = "{0}出发{1}旅游报价_{2}旅游信息_{3}旅游预订_同业114旅游线路频道";
        public const string RouteList_Keywords = "{0}旅游报价,{1}旅游信息,{2}旅游预订,地接社,旅游专线商.";
        public const string RouteList_Des = "同业114线路频道,提供{0}出发的最新{1}旅游报价,{2}旅游信息,并能实现{3}旅游的在线预订.";
        //线路内容页
        public const string RouteDetails_Title = "{0}_{1}出发旅游行程单";
        public const string RouteDetails_Keywords = "{0},旅游线路,线路安排,旅游专线,行程单.";
        public const string RouteDetails_Des = "同业114旅游线路频道,提供{0}出发旅游行程单,{1}旅游详细安排,{2}信息等,为游客出行和组团社查询提供便利.";
        #endregion





        public const string Plane_Title = "特价机票_打折机票_机票价格查询_同业114机票频道。";
        public const string Plane_Keywords = "团队机票预订,机票价格查询,机票信息,机票返点,打折机票,机票接口,航空公司,航班信息,旅行社机票,国内机票,国际机票,航空公司合作,机票频道";
        public const string Plane_Des = "同业114机票频道,为旅行社提供团队机票,散客机票预订,国内机票和国际机票价格查询,航空公司航班信息查询,特价机票,打折机票信息,东方航空,春秋航空,海南航空等航空公司票务合作信息.旅行社预订机票返点高,出票快.";

        //{0}替换为当前销售城市,{1}景点名称
        //景区栏目
        public const string Scenic_Title = "{0}旅游景点大全_{0}旅游景点推荐_{0}旅游景点介绍_{0}旅游景点图片_同业114景区频道";
        public const string Scenic_Keywords = "旅游景点大全,旅游景点信息,旅游景点图片,热门景点,热门目的地,旅游风景区 景区列表,景点名单,景点门票,景区活动,景区优惠, 景点返佣,景点旅行社合作,景区推广,景点宣传,景区网店.";
        public const string Scenic_Des = "同业114景区频道,为旅游景区推广提供平台,包含最新最全的{0}旅游景点信息,旅游景点联系方式.包括门旅游目的地,最热旅游景点,旅游风景区推荐,为景区和旅行社搭建合作桥梁.";
        //景区内容页
        public const string ScenicDetail_Title = "{1}介绍_{0}旅游景区黄页_同业114景区频道";
        public const string ScenicDetail_Keywords = "{0},旅游景区,景点图片,景点介绍,景点推荐,景点大全.";//{0}景点名称
        public const string ScenicDetail_Des = "同业114景区频道,为您提供{1}介绍,旅游景区图片,{0}旅游景区黄页,为景区提供企业宣传平台,为景区与旅行社合作搭建合作桥梁.";

        //{0}替换为当前销售城市
        public const string Hotel_Title = "全国酒店预订_酒店宾馆查询_团队酒店报价_同业114酒店频道";
        public const string Hotel_Keywords = "酒店信息,酒店预订,酒店大全,酒店宾馆查询,酒店联系方式,特价酒店,连锁酒店,挂三酒店,三星级酒店,四星级酒店,五星级酒店,商务酒店, 酒店返佣,酒店团队报价,旅行社酒店合作,酒店宾馆名录,酒店资源网";
        public const string Hotel_Des = "同业114酒店频道,为旅游同行提供全国各地酒店宾馆的在线查询和预订服务,介绍相关酒店的详细信息,提供团队酒店返佣和结算报价,还有促销酒店和热门酒店推荐,是旅游同业的酒店信息资源网站.";

        //{0}替换为当前销售城市，{1}车队名称
        public const string Car_Title = "{0}车队信息_{0}旅游车队_{0}团队租车_{0}汽车租赁_同业114车队频道";
        public const string Car_Keywords = "旅游车队,旅游租车,团队租车,汽车租赁公司,车队接送服务,车辆信息,旅行社车队合作,车辆预订.";
        public const string Car_Des = "同业114{0}站车队频道,为您提供{0}旅游车队黄页,车辆租赁信息,可以发布团队租车,旅游租车,汽车租赁,车队接送服务等信息的旅游行业网站.";
        //车队内容页
        public const string CarDetail_Title = "{1}_{0}旅游车队黄页_同业114车队频道";
        public const string CarDetail_Keywords = "旅游车队,旅游租车,团队租车,汽车租赁公司,车队接送服务,车辆信息,旅行社车队合作,车辆预订.";
        public const string CarDetail_Des = "同业114车队频道,为您提供{1}介绍及详细信息,{0}旅游车队黄页,让旅行社租车变得简单,是沟通车队与旅行社的桥梁.";

        //{0}替换为当前销售城市，{1}旅游用品名称
        public const string Travel_Title = "{0}户外用品_{0}旅游商品_{0}旅游用品公司_同业114旅游用品频道";
        public const string Travel_Keywords = "旅游用品,户外用品,旅游商品,特色旅游纪念品,定做,旅游帽,导游旗,旅行包,遮阳伞,订购,旅游企业,旅游用品批发";
        public const string Travel_Des = "同业114旅游用品频道,提供{0}旅游用品供应商信息,旅游用品公司黄页,详细的户外用品,旅游商品,旅游纪念品报价信息.还可以联系厂家定做旅游用品哦.";
        //旅游用品内容页
        public const string TravelDetail_Title = "{1}_{0}旅游用品供应商_同业114旅游用品频道";
        public const string TravelDetail_Keywords = "旅游用品,户外用品,旅游商品,特色旅游纪念品,定做,旅游帽,导游旗,旅行包,遮阳伞,订购,旅游企业,旅游用品批发";
        public const string TravelDetail_Des = "同业114旅游用品频道,提供{1}信息,{0}旅游用品公司黄页,还可以联系厂家定做旅游用品哦.";

        public const string Supplier_Title = "旅游供求信息_旅行社_找地接_找组团_旅游报价_同业114供求信息频道";
        public const string Supplier_Keywords = "旅游供求频道,为您提供最新旅游信息,旅游产品即时报价,特价旅游线路和旅行社收客信息查询,还可以发布团队询价,地接报价,组团拼团,旅游票务签证,车辆租赁等旅游供求信息.";
        public const string Supplier_Des = "旅行社线路报价,酒店团队报价,旅游信息发布,组团拼团信息,旅游票务签证,旅行社收客,特价旅游信息,出团计划,寻求旅游合作,团队询价,地接报价,旅行社合作,车辆租赁,导游招聘,旅游同行,行业资讯,同业学堂.";
         
        public const string SupplyList_Title = "{0}{1}旅游供求大全_{0}旅游报价_同业114供求信息频道";
        public const string SupplyList_Keywords = "旅行社线路报价,酒店团队报价,旅游信息发布,组团拼团信息,旅游票务签证,旅行社收客,特价旅游信息,出团计划,寻求旅游合作,团队询价,地接报价,旅行社合作,车辆租赁,导游招聘,旅游同行,行业资讯,同业学堂.";
        public const string SupplyList_Des = "杭州旅游供求频道,为您提供杭州地区最新旅游信息,旅游产品即时报价,特价旅游线路和旅行社收客信息查询,还可以发布团队询价,地接报价,组团拼团,旅游票务签证,车辆租赁等旅游供求信息.";

        //高级网店首页
        public const string SeniorShop_Title = "{0}";
        public const string SeniorShop_Keywords = "{0}，是{1}地区专营{2}的优秀企业，经营同业114高级网店";
        public const string SeniorShop_Des = "{0}，是{1}地区专营{2}的优秀企业，经营同业114高级网店";

        /// <summary>
        /// 普通网店首页
        /// </summary>
        public const string EShop_Title = "{0}_{1}旅游黄页_同业114";
        public const string EShop_Keywords = "{0}，{0}联系方式，{0}联系电话，{1}旅游同业黄页，{1}旅游同业大全";
        public const string EShop_Des = "{0}_{2}_同业114为您提供{0}介绍,{1}旅游黄页,免费旅游企业宣传平台,旅游行业B2B合作平台";
        
        /// <summary>
        /// 普通网店产品页面
        /// </summary>
        public const string EShop_Product_Title = "{0}_{1}旅游产品_同业114";
        public const string EShop_Product_Des = "{0}_同业114为您提供{0}产品,{1}旅游产品黄页,免费旅游产品宣传平台,旅游产品B2B推广平台";
        
        /// <summary>
        /// 普通网店资讯页面
        /// </summary>
        public const string EShop_Info_Title = "{0}_公司资讯_同业114";
        public const string EShop_Info_Des = "{0}同业资讯，请登陆后查看_同业114为您提供{1}最新新闻,{1}旅游新闻,免费旅游动态宣传平台,旅游产品B2B宣传平台";


        //资讯首页
        public const string Information_Title = "同业114旅游资讯-最全面的旅游行业新闻资讯网站";
        public const string Information_Keywords = "旅游资讯 旅游新闻 旅游活动 旅游攻略 旅游热点 旅游线路 旅游营销  同业学堂  旅游信息 旅行社新闻 酒店新闻 旅游社区 景区资讯  旅游指南 出行参考";
        public const string Information_Des = "同业114旅游资讯频道为旅游从业者和网友提供出行参考和旅游攻略指南，包括景区新闻，酒店资讯，机票信息，旅行社新闻，旅游线路在内的热门旅游信息，以及最新的旅游会展，旅游营销，同业学堂等旅游活动，是立足全国旅游同行的最全面的旅游行业新闻资讯网站。";

        //资讯列表
        public const string Information_List_Title = "{0}-同业114旅游资讯";
        public const string Information_List_Keywords = "{0}，旅游资讯，旅游新闻，旅游信息";
        public const string Information_List_Des = "同业114为您提供最新的{0}，旅游新闻和相关的旅行社，线路，酒店，景点等旅游信息，为您的工作和出行提供参考";

        //资讯详细页
        public const string Information_Detail_Title = "{0}-{1}-同业114旅游资讯";
    }
}
