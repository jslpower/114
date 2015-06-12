//HBE数据缓存接口信息业务实体
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.HotelBI.AvailCache
{
    #region 城市信息查询缓存指令信息业务实体
    /// <summary>
    /// 城市信息查询缓存指令信息业务实体
    /// </summary>
    public class MCityDetailsSearchRQInfo
    {
        /// <summary>
        /// 所查城市所属国家代码
        /// </summary>
        private string _CountryCode = "CN";

        /// <summary>
        /// default constructor
        /// </summary>
        public MCityDetailsSearchRQInfo() { }

        /// <summary>
        /// 所查城市所属国家代码 CN:中国 String类型;’ALL’查询所有国家; US:美国
        /// </summary>
        public string CountryCode
        {
            get { return this._CountryCode; }
            set { this._CountryCode = value; }
        }
        /// <summary>
        /// 接口预留 不填
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_CityDetailsSearchRQ, CreateCityDetailsSearchRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create CityDetailsSearchRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateCityDetailsSearchRQXML()
        {
            return string.Format(@"
<CityDetailsSearchRQ>
    <CountryCode>{0}</CountryCode>
    <CountryName></CountryName>
</CityDetailsSearchRQ>", this._CountryCode);
        }
    }
    #endregion

    #region 地标行政区查询指令信息业务实体
    /// <summary>
    /// 地标行政区查询指令信息业务实体
    /// </summary>
    public class MLandMarkSearchRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MLandMarkSearchRQInfo() { }

        /// <summary>
        /// 地标行政区查询类型
        /// </summary>
        public LandMarkSearchType LandMarkType { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_LandMarkSearchRQ, CreateLandMarkSearchRQXML(), Utils.HeaderApplication_availCache); }
        }
        /// <summary>
        /// Create LandMarkSearchRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateLandMarkSearchRQXML()
        {
            return string.Format(@"
<LandMarkSearchRQ>
		<Category>{0}</Category>
		<CountryCode>{1}</CountryCode>
		<CountryName>{2}</CountryName>
        <cityCode>{3}</cityCode>
</LandMarkSearchRQ>", LandMarkType.ToString(), string.Empty, string.Empty, CityCode);
        }
    }
    #endregion    

    #region 多酒店静态信息查询缓存指令信息业务实体
    /// <summary>
    /// 多酒店静态信息查询缓存指令信息业务实体
    /// </summary>
    public class MHotelStaticInfoCacheRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MHotelStaticInfoCacheRQInfo() { }

        /// <summary>
        /// 预留 不填
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string HotelEnglishName { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 国家代码 不填默认为中国
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_HotelStaticInfoCacheRQ, CreateHotelStaticInfoCacheRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create HotelStaticInfoCacheRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateHotelStaticInfoCacheRQXML()
        {
            return string.Format(@"
<HotelStaticInfoCacheRQ>
    <HotelStaticInfoCacheCriteria> 
        <HotelCode></HotelCode>
        <HotelName></HotelName>
        <HotelEnglishName></HotelEnglishName>
        <CityCode>{0}</CityCode>
        <CityName></CityName>
        <Rank></Rank>
        <CountryCode>{1}</CountryCode>
    </HotelStaticInfoCacheCriteria>
</HotelStaticInfoCacheRQ>", this.CityCode, this.CountryCode);
        }
    }
    #endregion

    #region 酒店数据缓存查询信息业务实体
    /// <summary>
    /// 酒店数据缓存查询信息业务实体
    /// </summary>
    /// Author:汪奇志 2011-05-11
    public class MHotelAvailabilityCacheRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MHotelAvailabilityCacheRQInfo() { }

        /// <summary>
        /// 从即日起查询之后多久的数据。现在为查询三个月(3M)
        /// </summary>
        public string DateRange { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 接口预留 不填
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 接口预留 不填
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// 接口预留 不填
        /// </summary>
        public string RatePlan { get; set; }
        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_HotelAvailabilityCacheRQ, CreateHotelAvailabilityCacheRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create HotelAvailabilityCacheRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateHotelAvailabilityCacheRQXML()
        {
            return string.Format(@"<HotelAvailabilityCacheRQ>
    <HotelAvailabilityCacheCriteria>
        <DateRange></DateRange>
        <HotelCode>{0}</HotelCode>
        <RoomType></RoomType>
        <Vendor></Vendor>
        <RatePlan></RatePlan>
    </HotelAvailabilityCacheCriteria>
</HotelAvailabilityCacheRQ>", HotelCode);
        }
    }
    #endregion

    #region 单酒店静态信息查询指令信息业务实体
    /// <summary>
    /// 单酒店静态信息查询指令信息业务实体
    /// </summary>
    public class MRoomTypeStaticInfoCacheRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRoomTypeStaticInfoCacheRQInfo() { }

        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }

        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_RoomTypeStaticInfoCacheRQ, CreateRoomTypeStaticInfoCacheRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create RoomTypeStaticInfoCacheRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateRoomTypeStaticInfoCacheRQXML()
        {
            return string.Format(@"
<HotelRoomTypeStaticInfoCacheRQ>
    <HotelRoomTypeStaticInfoCacheCriteria> 
        <HotelCode>{0}</HotelCode>
    </HotelRoomTypeStaticInfoCacheCriteria>
</HotelRoomTypeStaticInfoCacheRQ>", this.HotelCode);
        }
    }
    #endregion

    #region 酒店价格计划控制缓存查询信息业务实体
    /// <summary>
    /// 酒店价格计划控制缓存查询信息业务实体
    /// </summary>
    public class MRateplanControlCacheRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRateplanControlCacheRQInfo() { }

        /// <summary>
        /// 从即日起查询之后多久的数据。现在固定为查询三个月(3M)
        /// </summary>
        public string DateRange { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string RatePlan { get; set; }

        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_RateplanControlCacheRQ, CreateRateplanControlCacheRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create RateplanControlCacheRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateRateplanControlCacheRQXML()
        {
            return string.Format(@"
<RateplanControlCacheRQ>
    <RateplanControlCacheCriteria> 
        <DateRange>{0}</DateRange>
        <HotelCode>{1}</HotelCode>
        <RoomType></RoomType>
        <Vendor></Vendor>
        <RatePlan></RatePlan>
    </RateplanControlCacheCriteria>
</RateplanControlCacheRQ>", this.DateRange, this.HotelCode);
        }
    }
    #endregion

    #region 酒店价格计划佣金缓存查询信息业务实体
    /// <summary>
    /// 酒店价格计划佣金缓存查询信息业务实体
    /// </summary>
    public class MRateplanCommCacheRQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MRateplanCommCacheRQInfo() { }

        /// <summary>
        /// 从即日起查询之后多久的数据。现在固定为查询三个月(3M)
        /// </summary>
        public string DateRange { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// 预留 不填
        /// </summary>
        public string RatePlan { get; set; }

        /// <summary>
        /// 请求XML指令
        /// </summary>
        public string RequestXML
        {
            get { return Utils.CreateRequestXML(Utils.TH_RateplanCommCacheRQ, CreateRateplanCommCacheRQXML(), Utils.HeaderApplication_availCache); }
        }

        /// <summary>
        /// Create RateplanCommCacheRQ XML
        /// </summary>
        /// <returns></returns>
        private string CreateRateplanCommCacheRQXML()
        {
            return string.Format(@"
<RateplanCommCacheRQ>
    <RateplanCommCacheCriteria> 
        <DateRange>{0}</DateRange>
        <HotelCode>{1}</HotelCode>
        <RoomType></RoomType>
        <Vendor></Vendor>
        <RatePlan></RatePlan>
    </RateplanCommCacheCriteria> 
</RateplanCommCacheRQ>", this.DateRange, this.HotelCode);
        }
    }
    #endregion
}
