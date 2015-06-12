#region 命名空间
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace EyouSoft.HotelBI
{    
    #region 公共验证信息
    /// <summary>
    /// HBE 接口验证信息
    /// </summary>
    public class IdentityInfo
    {

        #region 接口认证信息 /IdentityInfo 结点

        private string _OfficeID = "ZJF114";
        /// <summary>
        /// HBE OFFICE ID 必填
        /// </summary>
        public string OfficeID { get { return _OfficeID; } set { _OfficeID = value; } }

        private string _UserID = "ZJF11400C";
        /// <summary>
        /// HBE UserID 必填
        /// </summary>
        public string UserID { get { return _UserID; } set { _UserID = value; } }

        private string _Password = "12345678";
        /// <summary>
        /// HBE Password 必填
        /// </summary>
        public string Password { get { return _Password; } set { _Password = value; } }

        private string _Role = "";
        /// <summary>
        /// 角色    接口预留
        /// </summary>
        public string Role { get { return _Role; } set { _Role = value; } }
        #endregion

        #region 接口 SOURCE 信息
        /// <summary>
        /// 系列编号 接口预留
        /// </summary>
        public string UniqueID { get; set; }

        private string _BookingChannel = "HOTELBE";
        /// <summary>
        /// 预定渠道代码(默认填写:HOTELBE) 必填
        /// </summary>
        public string BookingChannel { get { return _BookingChannel; } set { _BookingChannel = value; } }
        #endregion        
    }

    /// <summary>
    /// 头部验证信息
    /// </summary>
    public class Header
    {
        #region 头部实体
        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID { get; set; }
        /// <summary>
        /// 调用者
        /// </summary>
        public string Invoker { get; set; }
        /// <summary>
        /// 字符编码
        /// </summary>
        public string Encoding { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Locale { get; set; }
        /// <summary>
        /// SerialNo
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 时间戳 YYYY-mm-DD HH:MM:SS
        /// </summary>
        public string TimeStamp { get; set; }

        private string _application = "hotelbe";
        /// <summary>
        /// 子系统名称 必填
        /// </summary>
        public string Application { get { return _application; } set { _application=value; } }

        private string _language = "CN";
        /// <summary>
        /// 语言标识 默认为”CN”
        /// </summary>
        public string Language { get { return _language; } set { _language=value; } }
        #endregion
    }
    #endregion

    #region 公共结构
    /// <summary>
    /// 布尔结构体
    /// </summary>
    public enum BoolEnum { 
        /// <summary>
        /// 未选择
        /// </summary>
        none,
        /// <summary>
        /// Y(true)
        /// </summary>
        Y,
        /// <summary>
        /// N(false)
        /// </summary>
        N
    }
    /// <summary>
    /// 查询完整的酒店信息还是只查询价格、房态、政策等动态信息
    /// </summary>
    public enum AvailReqTypeEnum
    { 
        /// <summary>
        /// 只包含动态信息
        /// </summary>
        noneStatics,
        /// <summary>
        /// 含动态、静态信息
        /// </summary>
        includeStatic
    }
    /// <summary>
    /// 装修级别
    /// </summary>
    public enum QualityLevelEnum
    {
        /// <summary>
        /// 无要求
        /// </summary>
        None,
        /// <summary>
        /// 豪华
        /// </summary>
        豪华,
        /// <summary>
        /// 标准
        /// </summary>
        标准,
        /// <summary>
        /// 经济
        /// </summary>
        经济
    }
    /// <summary>
    /// 酒店星级
    /// </summary>
    public enum HotelRankEnum
    {
        /// <summary>
        /// 未选择星级
        /// </summary>
        _00,
        /// <summary>
        /// 一星
        /// </summary>
        _1S,
        /// <summary>
        /// 二星
        /// </summary>
        _2S,
        /// <summary>
        /// 三星
        /// </summary>
        _3S,
        /// <summary>
        /// 四星
        /// </summary>
        _4S,
        /// <summary>
        /// 五星
        /// </summary>
        _5S,
        /// <summary>
        /// 准一星
        /// </summary>
        _1A,
        /// <summary>
        /// 准二星
        /// </summary>
        _2A,
        /// <summary>
        /// 准三星
        /// </summary>
        _3A,
        /// <summary>
        /// 准四星
        /// </summary>
        _4A,
        /// <summary>
        /// 准五星
        /// </summary>
        _5A
    }
    /// <summary>
    /// 酒店排序
    /// </summary>
    public enum HotelOrderBy {
        /// <summary>
        /// 未指定排序，调用接口默认
        /// </summary>
        Default,
        /// <summary>
        /// 星级低到高
        /// </summary>
        STARLTH,
        /// <summary>
        /// 星级高到低
        /// </summary>
        STARHTL,
        /// <summary>
        /// 价格低到高
        /// </summary>
        PRICELTH,
        /// <summary>
        /// 价格高到低
        /// </summary>
        PRICEHTL
    }

    /// <summary>
    /// 地标行政区查询类型
    /// </summary>
    public enum LandMarkSearchType
    {
        /// <summary>
        /// 地标信息
        /// </summary>
        POR=0,
        /// <summary>
        /// 行政区信息
        /// </summary>
        DST,
        /// <summary>
        /// 交通信息
        /// </summary>
        TRA,
        /// <summary>
        /// 周围景观
        /// </summary>
        SGT
    }
    #endregion

    #region 请求返回错误信息业务实体
    /// <summary>
    /// 请求返回错误级别
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 无错误
        /// </summary>
        None = 0,
        /// <summary>
        /// 业务级错误
        /// </summary>
        业务级错误,
        /// <summary>
        /// 系统级错误
        /// </summary>
        系统级错误,
        /// <summary>
        /// 未知错误
        /// </summary>
        未知错误
    }

    /// <summary>
    /// 请求返回错误信息业务实体
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ErrorInfo() { }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDesc { get; set; }
        /// <summary>
        /// 请求返回错误级别
        /// </summary>
        public ErrorType ErrorType {get;set;}
    }
    #endregion
}