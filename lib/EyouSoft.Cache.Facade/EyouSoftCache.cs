using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;


namespace EyouSoft.Cache.Facade
{
    public class CacheRefreshAction : ICacheItemRefreshAction
    {
        public void Refresh(string key, object expiredValue, CacheItemRemovedReason removalReason)
        {
            // Item has been removed from cache. Perform desired actions here, based upon
            // the removal reason (e.g. refresh the cache with the item).
        }
    }

    #region 带时间截缓存
    /// <summary>
    /// 带时间截缓存
    /// </summary>
    /// <typeparam name="T">缓存对象类型</typeparam>
    [Serializable]
    public class EyouSoftCacheTime<T>
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        public T Data
        {
            get;
            set;
        }
        /// <summary>
        /// 时间截
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }
    }
    #endregion

    #region 登陆验证中心缓存
    /// <summary>
    /// 登陆验证中心缓存
    /// </summary>
    public class EyouSoftSSOCache
    {
        private static ICacheManager instance = null;
        private static object lockHelper = new object();

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, object value)
        {
            GetCacheService().Add(key, value);
        }
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="AbsoluteTime"></param>
        public static void Add(string key, object value, DateTime AbsoluteTime)
        {
            GetCacheService().Add(key, value, new CacheItemPriority(), null, new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime(AbsoluteTime));
        }
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="RefreshAction"></param>
        /// <param name="AbsoluteTime"></param>
        public static void Add(string key, object value, CacheRefreshAction RefreshAction, DateTime AbsoluteTime)
        {
            GetCacheService().Add(key, value, new CacheItemPriority(), RefreshAction, new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime(AbsoluteTime));
        }
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return GetCacheService().GetData(key);
        }
        /// <summary>
        /// 清除缓存对象
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            GetCacheService().Remove(key);
        }
        /// <summary>
        /// 单体模式返回当前类的实例
        /// </summary>
        /// <returns></returns>
        private static ICacheManager GetCacheService()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = CacheFactory.GetCacheManager("SSOCache");
                    }
                }
            }
            return instance;
        }

    }
    #endregion

    #region 平台缓存
    /// <summary>
    /// 平台缓存
    /// </summary>
    public class EyouSoftCache
    {
        private static ICacheManager instance = null;
        private static object lockHelper = new object();

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, object value)
        {
            GetCacheService().Add(key, value);
        }
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="AbsoluteTime"></param>
        public static void Add(string key, object value, DateTime AbsoluteTime)
        {
            GetCacheService().Add(key, value, new CacheItemPriority(), null, new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime(AbsoluteTime));
        }
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="RefreshAction"></param>
        /// <param name="AbsoluteTime"></param>
        public static void Add(string key, object value, CacheRefreshAction RefreshAction, DateTime AbsoluteTime)
        {
            GetCacheService().Add(key, value, new CacheItemPriority(), RefreshAction, new Microsoft.Practices.EnterpriseLibrary.Caching.Expirations.AbsoluteTime(AbsoluteTime));
        }
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return GetCacheService().GetData(key);
        }
        /// <summary>
        /// 清除缓存对象
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            GetCacheService().Remove(key);
        }
        /// <summary>
        /// 单体模式返回当前类的实例
        /// </summary>
        /// <returns></returns>
        private static ICacheManager GetCacheService()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = CacheFactory.GetCacheManager("Cache Manager1");
                    }
                }
            }
            return instance;
        }

    }
    #endregion
}

/*
 * 缓存标签管理
 */
#region 缓存标签管理
namespace EyouSoft.CacheTag
{
    /// <summary>
    /// 公司缓存标签
    /// </summary>
    public static class Company
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public const string CompanyUser = "PLATFORM/COMPANY/USER/";
        /// <summary>
        /// 单位信息
        /// </summary>
        public const string CompanyInfo = "PLATFORM/COMPANY/";
        /// <summary>
        /// 单位状态信息
        /// </summary>
        public const string CompanyState = "PLATFORM/COMPANY/STATE/";
        /// <summary>
        /// 单位设置
        /// </summary>
        public const string CompanySetting = "PLATFORM/COMPANY/SETTING/";
        /// <summary>
        /// 推广类型
        /// </summary>
        public const string TourStateBase = "PLATFORM/COMPANY/TOURSTATEBASE/";
        /// <summary>
        /// 信用诚信体系配置信息
        /// </summary>
        public const string RateConfig = "PLATFORM/COMPANY/RATECONFIG/";
        /// <summary>
        /// 信用次数
        /// </summary>
        public const string RateCount = "PLATFORM/COMPANY/RATECOUNT/";
        /// <summary>
        /// 信用分值
        /// </summary>
        public const string RateScore = "PLATFORM/COMPANY/RATESCORE/";
        /// <summary>
        /// 公司城市区域关系
        /// </summary>
        public const string CompanyCity = "PLATFORM/COMPANY/CITYAREA/";
        /// <summary>
        /// 公司常用出港城市
        /// </summary>
        public const string CompanySite = "PLATFORM/COMPANY/SITEAREA/";       
        /// <summary>
        /// 公司高级网店详细信息
        /// </summary>
        public const string CompanyHighShop = "PLATFORM/COMPANY/HIGHSHOP/";
        /// <summary>
        /// 公司常用报价等级
        /// </summary>
        public const string CompanyPriceStand = "PLATFORM/COMPANY/PRICESTAND/";

    }
    /// <summary>
    /// 系统缓存标签
    /// </summary>
    public static class System
    {
        /// <summary>
        /// 系统用户
        /// </summary>
        public const string SystemUser = "PLATFORM/SYSTEM/USER/";
        /// <summary>
        /// 系统域名
        /// </summary>
        public const string SystemDomain = "PLATFORM/SYSTEM_DOMAIN/";
        /// <summary>
        /// 系统设置
        /// </summary>
        public const string SystemSetting = "PLATFORM/SYSTEM_SETTING";
        /// <summary>
        /// 省份列表
        /// </summary>
        public const string SystemProvince = "PLATFORM/SYSTEM_PROVINCE";
        /// <summary>
        /// 已开通的省份
        /// </summary>
        public const string SystemProvinceEnabled = "PLATFORM/SYSTEM_PROVINCE_ENABLED";
        /// <summary>
        /// 城市列表
        /// </summary>
        public const string SystemCity = "PLATFORM/SYSTEM_CITY";
        /// <summary>
        /// 城市信息({0}:城市编号)
        /// </summary>
        public const string SystemCityInfo = "PLATFORM/SYSTEM_CITY/{0}";
        /// <summary>
        /// 县区列表
        /// </summary>
        public const string SystemCounty = "PLATFORM/SYSTEM_County";
        /// <summary>
        /// 县区信息({0}:县区编号)
        /// </summary>
        public const string SystemCountyInfo = "PLATFORM/SYSTEM_County/{0}";
        /// <summary>
        /// 地标列表
        /// </summary>
        public const string SystemLankMarks = "PLATFORM/SYSTEM_LankMarks";
        /// <summary>
        /// 城市区域关系
        /// </summary>
        public const string SystemCityArea = "PLATFORM/SYSTEM_CITYAREA";
        /// <summary>
        /// 长短线区域城市关系
        /// </summary>
        public const string SystemAreaCatalog = "PLATFORM/SYSTEM_AREACATALOG";
        /// <summary>
        /// 系统线路区域
        /// </summary>
        public const string SystemArea = "PLATFORM/SYSTEM_AREA";
        /// <summary>
        /// 首页友情链接
        /// </summary>
        public const string SystemFriendLink = "PLATFORM/SYSTEM_FRIENDLINK/";
        /// <summary>
        /// 系统信息
        /// </summary>
        public const string SystemInfo = "PLATFORM/SYSTEM_INFO";
        /// <summary>
        /// 个人中心登陆广告
        /// </summary>
        public const string CategoryAdv = "PLATFORM/SYSTEM/CATEGORYADV/";
        /// <summary>
        /// 线路主题
        /// </summary>
        public const string RouteTheme = "PLATFORM/SYSTEM/ROUTE_THEME";
        /// <summary>
        /// 系统客户等级
        /// </summary>
        public const string CustomerLevel = "PLATFORM/SYSTEM/CUSTOMER_LEVEL";
        /// <summary>
        /// 系统统计数据
        /// </summary>
        public const string SummaryCount = "PLATFORM/SYSTEM/SUMMARY_COUNT";
        /// <summary>
        /// 景点主题
        /// </summary>
        public const string SightTheme = "PLATFORM/SYSTEM/SIGHT_THEME";
        /// <summary>
        /// 最新景区主题
        /// </summary>
        public const string ScenicTheme = "PLATFORM/SYSTEM/SCENIC_THEME";
        /// <summary>
        /// 酒店周边环境
        /// </summary>
        public const string HotelArea = "PLATFORM/SYSTEM/HOTEL_AREA";
        /// <summary>
        /// 目的地地接社城市
        /// </summary>
        public const string LocalCity = "PLATFORM/SYSTEM/OLD_LOCAL_CITY";
        /// <summary>
        /// 系统出港城市更新时间
        /// </summary>
        public const string SysSiteUpdateKey = "PLATFORM/SYSTEM/SYSSITEUPDATE/";
        /// <summary>
        /// 存在线路的线路区域
        /// </summary>
        public const string ExistsTourAreas = "PLATFORM/SYSTEM/EXISTSTOURAREAS/";

        /// <summary>
        /// 系统国家数据缓存标签
        /// </summary>
        public const string SystemCountry = "PLATFORM/SYSTEM_COUNTRY";
        /// <summary>
        /// 平台配置信息
        /// </summary>
        public const string SysSetting = "PLATFORM/SYS/SETTING";
    }
    /// <summary>
    /// 团队缓存标签
    /// </summary>
    public static class Tour
    {
        /// <summary>
        /// 专线已接收线路
        /// </summary>
        public const string ReceivedRoute = "PLATFORM/COMPANY/ROUTE/RECEIVED/";
    }
    /// <summary>
    /// 广告缓存标签
    /// </summary>
    public static class Adv
    {
        /// <summary>
        /// 平台广告({0}:位置,{1}:关系编号)
        /// </summary>
        public const string SystemAdv = "PLATFORM/SYSTEM/SYSTEM_ADV/{0}/{1}";
        /// <summary>
        /// 平台广告更新时间({0}:位置)
        /// </summary>
        public const string SystemAdvUpdateKey = "PLATFORM/SYSTEM/SYSTEM_ADV/{0}/UPDATETIME";
        /// <summary>
        /// 平台广告位置({0}:广告栏目)
        /// </summary>
        public const string SystemAdvPostion = "PLATFORM/SYSTEM/SYSTEM_ADVPOSTION/{0}";
    }
    /// <summary>
    /// MQ缓存标签
    /// </summary>
    public static class MQ
    {
        /// <summary>
        /// MQ用户信息
        /// </summary>
        public const string MQUser = "PLATFORM/MQ/USER/";
    }
    /// <summary>
    /// 机票缓存标签
    /// </summary>
    public static class AirTickets
    {
        /// <summary>
        /// 航空公司
        /// </summary>
        public const string TicketFlightCompany = "PLATFORM/AIRTICKET/TICKETFLIGHTCOMPANY";
        /// <summary>
        /// 机场
        /// </summary>
        public const string TicketSeattle = "PLATFORM/AIRTICKET/TICKETSEATTLE";
        /// <summary>
        /// 供应商
        /// </summary>
        public const string TicketSupplierInfo = "PLATFORM/AIRTICKET/TICKETSUPPLIER/";
        /// <summary>
        /// <summary>
        /// 国家
        /// </summary>
        public const string TicketNational = "PLATFORM/AIRTICKET/TICKETNATIONAL";
        /// <summary>
        /// 供应山出票成功率
        /// </summary>
        public const string TicketSuccessRate = "PLATFORM/AIRTICKET/SUCCESSRATE/";
    }
    /// <summary>
    /// 酒店缓存标签
    /// </summary>
    public static class Hotel
    {
        /// <summary>
        /// 单酒店信息
        /// </summary>
        public const string HotelInfo = "PLATFORM/HOTELINFO/";
        /// <summary>
        /// 酒店列表
        /// </summary>
        public const string HotelList = "PLATFORM/HOTELLIST/";
    }
    /// <summary>
    /// 高级网店客服系统缓存标签
    /// </summary>
    public static class OlServer
    {
        /// <summary>
        /// 客户cookie名称
        /// </summary>
        public const string OLSGUESTCOOKIENAME = "OlServer/OLSGC/";
        /// <summary>
        /// 客服cookie名称
        /// </summary>
        public const string OLSSERVICECOOKIENAME = "OlServer/OLSSC/";
        /// <summary>
        /// 在线客服配置信息缓存名称
        /// </summary>
        public const string OLSCONFIGCACHENAME = "OlServer/OLSERVERCONFIG/";
        /// <summary>
        /// 在线客服清理用户状态的间隔时间的缓存名称
        /// </summary>
        public const string OLSERVERCLEARUSEROUTINTERVALCACHENAME = "OlServer/CLEARUSEROUTINTERVAL/";
    }

    /// <summary>
    /// 提醒数据缓存标签类
    /// </summary>
    public static class Remind
    {
        /// <summary>
        /// 平台首页右侧滚动显示的用户行为数据缓存标签
        /// </summary>
        public const string HeadRollRemind = "Remind/HeadRollRemind/";
    }

    /// <summary>
    /// 旅游签证缓存标签类
    /// </summary>
    public static class Visa
    {
        /// <summary>
        /// 新版平台首页同业百宝箱旅游签证的国家搜索行为数据缓存标签
        /// </summary>
        public const string CountrySearch = "Visa/CountrySearch/";
    }
}
#endregion