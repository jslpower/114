//Author:汪奇志 210-12-01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace EyouSoft.HotelBI
{
    /// <summary>
    /// HotelBI Utility
    /// </summary>
    public class Utils
    {
        #region static constants

        #region 请求指令
        /// <summary>
        /// 取消订单请求指令
        /// </summary>
        public const string TH_HotelCancelRQ = "TH_HotelCancelRQ";        
        /// <summary>
        /// 预订订单请求指令
        /// </summary>
        public const string TH_HotelResRQ = "TH_HotelResRQ";        
        /// <summary>
        /// 单个订单详细信息查询指令
        /// </summary>
        public const string TH_HotelResDetailSearchRQ = "TH_HotelResDetailSearchRQ";
        /// <summary>
        /// 单酒店查询指令
        /// </summary>
        public const string TH_HotelSingleAvailRQ = "TH_HotelSingleAvailRQ";
        /// <summary>
        /// 多酒店查询指令
        /// </summary>
        public const string TH_HotelMultiAvailRQ = "TH_HotelMultiAvailRQ";

        /// <summary>
        /// 城市信息查询缓存指令
        /// </summary>
        public const string TH_CityDetailsSearchRQ = "TH_CityDetailsSearchRQ";
        /// <summary>
        /// 地标行政区查询缓存指令
        /// </summary>
        public const string TH_LandMarkSearchRQ = "TH_LandMarkSearchRQ";        
        /// <summary>
        /// 多酒店静态信息查询缓存指令
        /// </summary>
        public const string TH_HotelStaticInfoCacheRQ = "TH_HotelStaticInfoCacheRQ";
        /// <summary>
        /// 酒店数据缓存查询指令
        /// </summary>
        public const string TH_HotelAvailabilityCacheRQ = "TH_HotelAvailabilityCacheRQ";
        /// <summary>
        /// 单酒店静态信息查询指令
        /// </summary>
        public const string TH_RoomTypeStaticInfoCacheRQ = "TH_RoomTypeStaticInfoCacheRQ";
        /// <summary>
        /// 酒店价格计划控制缓存查询指令
        /// </summary>
        public const string TH_RateplanControlCacheRQ = "TH_RateplanControlCacheRQ";
        /// <summary>
        /// 酒店价格计划佣金缓存查询指令
        /// </summary>
        public const string TH_RateplanCommCacheRQ = "TH_RateplanCommCacheRQ";
        #endregion

        #region 返回指令
        /// <summary>
        /// 取消订单请求返回指令
        /// </summary>
        public const string TH_HotelCancelRS = "TH_HotelCancelRS";
        /// <summary>
        /// 预订订单请求返回指令
        /// </summary>
        public const string TH_HotelResRS = "TH_HotelResRS";
        /// <summary>
        /// 单个订单详细信息查询返回指令
        /// </summary>
        public const string TH_HotelResDetailSearchRS = "TH_HotelResDetailSearchRS";
        /// <summary>
        /// 单酒店查询返回指令
        /// </summary>
        public const string TH_HotelSingleAvailRS = "TH_HotelSingleAvailRS";
        /// <summary>
        /// 多酒店查询返回指令
        /// </summary>
        public const string TH_HotelMultiAvailRS = "TH_HotelMultiAvailRS";

        /// <summary>
        /// 城市信息查询缓存返回指令
        /// </summary>
        public const string TH_CityDetailsSearchRS = "TH_CityDetailsSearchRS";        
        /// <summary>
        /// 地标行政区查询缓存返回指令
        /// </summary>
        public const string TH_LandMarkSearchRS = "TH_LandMarkSearchRS";        
        /// <summary>
        /// 多酒店静态信息查询缓存返回指令
        /// </summary>
        public const string TH_HotelStaticInfoCacheRS = "TH_HotelStaticInfoCacheRS";
        /// <summary>
        /// 酒店数据缓存查询返回指令
        /// </summary>
        public const string TH_HotelAvailabilityCacheRS = "TH_HotelAvailabilityCacheRS";
        /// <summary>
        /// 单酒店静态信息查询返回指令
        /// </summary>
        public const string TH_RoomTypeStaticInfoCacheRS = "TH_RoomTypeStaticInfoCacheRS";
        /// <summary>
        /// 酒店价格计划控制缓存查询返回指令
        /// </summary>
        public const string TH_RateplanControlCacheRS = "TH_RateplanControlCacheRS";
        /// <summary>
        /// 酒店价格计划佣金缓存查询返回指令
        /// </summary>
        public const string TH_RateplanCommCacheRS = "TH_RateplanCommCacheRS";
        #endregion

        #region HeaderApplication
        /// <summary>
        /// Header Application hotelbe
        /// </summary>
        public const string HeaderApplication_hotelbe = "hotelbe";
        /// <summary>
        /// Header Application availCache
        /// </summary>
        public const string HeaderApplication_availCache = "availCache";
        #endregion

        /// <summary>
        /// HBE 接口地址
        /// </summary>
        public const string HBEURL = "http://dlink.sohoto.com/directlink/send.do";
        /// <summary>
        /// HBE 酒店图片地址前缀
        /// </summary>
        public const string ImagesUrl = "http://media.sohoto.com/market/images/hotelimages";//"http://www.sohoto.com/TDPWeb/htl/images/imagesHotel";//"http://www.sohoto.com/tdpweb/htl/images/imageshotel"; //"http://media.sohoto.com/market/images/hotelimages/";
        /// <summary>
        /// WebRequest Timeout
        /// </summary>
        private const int WebRequestTimeout = 30000;
        
        #endregion

        #region private members
        /// <summary>
        /// Create TransactionName
        /// </summary>
        /// <param name="transactionName">指令名称</param>
        /// <returns></returns>
        private static string CreateTransactionName(string transactionName)
        {
            return string.Format("<TransactionName>{0}</TransactionName>", transactionName);
        }

        /// <summary>
        /// Create Header
        /// </summary>
        /// <returns></returns>
        private static string CreateHeader()
        {
            return CreateHeader(new Header());
        }

        /// <summary>
        /// Create Header
        /// </summary>
        /// <param name="header">header info</param>
        /// <returns></returns>
        private static string CreateHeader(Header header)
        {
            return "<Header>" +
                            "<SessionID>" + header.SessionID + "</SessionID>" +
                            "<Invoker>" + header.Invoker + "</Invoker>" +
                            "<Encoding>" + header.Encoding + "</Encoding>" +
                            "<Locale>" + header.Locale + "</Locale>" +
                            "<SerialNo>" + header.SerialNo + "</SerialNo>" +
                            "<TimeStamp>" + header.TimeStamp + "</TimeStamp>" +
                            "<Application>" + header.Application + "</Application>" +
                            "<Language>" + header.Language + "</Language>" +
                            "</Header>";
        }

        /// <summary>
        /// Create IdentityInfo and Source
        /// </summary>
        /// <returns></returns>
        private static string CreateIdentityInfo()
        {
            return CreateIdentityInfo(new IdentityInfo());
        }

        /// <summary>
        /// Create IdentityInfo and Source
        /// </summary>
        /// <returns></returns>
        private static string CreateIdentityInfo(IdentityInfo identityInfo)
        {
            return "<IdentityInfo>" +
                            "<OfficeID>" + identityInfo.OfficeID + "</OfficeID>" +
                            "<UserID>" + identityInfo.UserID + "</UserID>" +
                            "<Password>" + identityInfo.Password + "</Password>" +
                            "<Role>" + identityInfo.Role + "</Role>" +
                            "</IdentityInfo>" +
                            "<Source>" +
                            "<OfficeCode>" + identityInfo.OfficeID + "</OfficeCode>" +
                            "<UniqueID>" + identityInfo.UniqueID + "</UniqueID>" +
                            "<BookingChannel>" + identityInfo.BookingChannel + "</BookingChannel>" +
                            "</Source>";
        }

        /// <summary>
        /// 获取业务级错误指令错误节点名称
        /// </summary>
        /// <param name="transactionName">指令名称</param>
        /// <returns></returns>
        private static string GetBusinessErrorNodeName(string transactionName)
        {
            string nodeName = string.Empty;

            switch (transactionName)
            {
                case TH_HotelCancelRS: nodeName = "HotelCancelRS"; break;
                case TH_HotelResRS: nodeName = "HotelResRS"; break;
                case TH_HotelMultiAvailRS: nodeName = "HotelAvailRS"; break;//HotelMultiAvailRS
                case TH_HotelResDetailSearchRS: nodeName = "HotelResDetailSearchRS"; break;
                case TH_HotelSingleAvailRS: nodeName = "HotelAvailRS"; break;//HotelSingleAvailRS
            }

            return nodeName;
        }
        #endregion

        #region public members
        /// <summary>
        /// 转换 布尔为 Y 或 N
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string BoolToYesOrNo(bool b)
        {
            return b ? "Y" : "N";
        }

        /// <summary>
        /// Create Request XML,Header.Application="hotelbe"
        /// </summary>
        /// <param name="transactionName"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string CreateRequestXML(string transactionName, string xml)
        {
            return CreateRequestXML(transactionName, xml, HeaderApplication_hotelbe);
        }

        /// <summary>
        /// Create Request XML
        /// </summary>
        /// <param name="transactionName">TransactionName</param>
        /// <param name="xml"></param>
        /// <param name="headerApplication">Header.Application</param>
        /// <returns></returns>
        public static string CreateRequestXML(string transactionName, string xml, string headerApplication)
        {
            Header header = new Header();
            header.Application = headerApplication;

            StringBuilder s = new StringBuilder();
            s.Append("<OTRequest>");
            s.Append(Utils.CreateHeader(header));
            s.Append(Utils.CreateTransactionName(transactionName));
            s.Append(xml);
            s.Append(Utils.CreateIdentityInfo());
            s.Append("</OTRequest>");

            return s.ToString();
        }

        /// <summary>
        /// 创建订单状态异步通知返回指令
        /// </summary>
        /// <param name="code">正确错误代码</param>
        /// <param name="desc">正确错误描述</param>
        /// <returns></returns>
        public static string CreateOrderStatusNotifRS(string code,string desc)
        {
            string xml = string.Format("<Code>{0}</Code><Description>{1}</Description>", code, desc);

            return string.Format("<OTResonse>{0}{1}{2}</OTResonse>", Utils.CreateTransactionName("SP_HotelOrderStatusNotifRS")
                , Utils.CreateHeader()
                , Utils.CreateIdentityInfo()
                , xml);
        }

        #region WebRequest
        /// <summary>
        /// 酒店系统发送请求指令,请求返回内容不进行HttpUtility.UrlDecode
        /// </summary>
        /// <param name="xml">请求指令</param>
        /// <returns></returns>
        public static string CreateRequest(string xml)
        {
            return CreateRequest(xml, false);
        }

        /// <summary>
        /// 酒店系统发送请求指令
        /// </summary>
        /// <param name="xml">请求指令</param>
        /// <param name="isUrlDecode">返回内容是否进行HttpUtility.UrlDecode</param>
        /// <returns></returns>
        public static string CreateRequest(string xml, bool isUrlDecode)
        {
            StringBuilder responseText = new StringBuilder();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HBEURL);
            request.Timeout = WebRequestTimeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding encode = System.Text.Encoding.UTF8;

            byte[] bytes = encode.GetBytes("request=" + xml);
            request.ContentLength = bytes.Length;

            Stream oStreamOut = request.GetRequestStream();
            oStreamOut.Write(bytes, 0, bytes.Length);
            oStreamOut.Close();

            HttpWebResponse response = null;

            try
            {
                int i = 3;
                while (i > 0)
                {
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK) break;
                    else response = null;
                    i--;
                }
            }
            catch { response = null; }

            if (response != null)
            {
                try
                {
                    Stream resStream = null;
                    resStream = response.GetResponseStream();

                    StreamReader readStream = new StreamReader(resStream, encode);

                    Char[] read = new Char[256];
                    int count = readStream.Read(read, 0, 256);
                    while (count > 0)
                    {
                        string s = new String(read, 0, count);
                        responseText.Append(s);
                        count = readStream.Read(read, 0, 256);
                    }

                    resStream.Close();
                }
                catch { }
            }

            if (isUrlDecode)
            {
                return System.Web.HttpUtility.UrlDecode(responseText.ToString());
            }

            return responseText.ToString();
        }
        #endregion

        /// <summary>
        /// 请求返回错误处理
        /// </summary>
        /// <param name="xml">请求返回指令</param>
        /// <returns></returns>
        public static ErrorInfo ResponseErrorHandling(string xml)
        {
            ErrorInfo errorInfo = new ErrorInfo() { ErrorType = ErrorType.None };

            if (string.IsNullOrEmpty(xml))
            {
                errorInfo.ErrorType = ErrorType.未知错误;
                errorInfo.ErrorCode = "-1";
                errorInfo.ErrorDesc = "请求返回的XML为空";

                return errorInfo;
            }

            try
            {
                XElement xResponse = XElement.Parse(xml);             
                //指令节点
                XElement xTransactionName = xResponse.Element("TransactionName");
                if (xTransactionName != null)
                {
                    //指令名称
                    string transactionName = xTransactionName.Value;
                    XElement xTransactionElement = xResponse.Element(GetBusinessErrorNodeName(transactionName));
                    if (xTransactionElement != null && xTransactionElement.Element("Errors") != null)//业务级错误
                    {
                        XElement xBusinessError = xTransactionElement.Element("Errors").Element("Error");
                        errorInfo.ErrorType = ErrorType.业务级错误;
                        errorInfo.ErrorCode = xBusinessError.Attribute("ErrorCode").Value;
                        errorInfo.ErrorDesc = xBusinessError.Attribute("ErrorDesc").Value;
                    }
                }
                else
                {
                    XElement xSystemError = xResponse.Element("Errors");
                    if (xSystemError != null)//系统级错误
                    {
                        xSystemError = xSystemError.Element("Error");
                        errorInfo.ErrorType = ErrorType.系统级错误;
                        errorInfo.ErrorCode = xSystemError.Attribute("ErrorCode").Value;
                        errorInfo.ErrorDesc = xSystemError.Attribute("ErrorDesc").Value;
                    }
                }
            }
            catch
            {
                errorInfo.ErrorType = ErrorType.未知错误;
                errorInfo.ErrorCode = "-2";
                errorInfo.ErrorDesc = "错误处理时解析XML错误";
            }

            return errorInfo;
        }

        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="XAttribute">xAttribute</param>
        /// <returns>Value</returns>
        public static string GetXAttributeValue(XAttribute xAttribute)
        {
            if (xAttribute == null)
                return string.Empty;

            return xAttribute.Value;
        }

        /// <summary>
        /// Get XAttribute Value
        /// </summary>
        /// <param name="xElement">XElement</param>
        /// <param name="attributeName">Attribute Name</param>
        /// <returns></returns>
        public static string GetXAttributeValue(XElement xElement, string attributeName)
        {
            return GetXAttributeValue(xElement.Attribute(attributeName));
        }

        /// <summary>
        /// Get XElement
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElement</returns>
        public static XElement GetXElement(XElement xElement, string xName)
        {
            XElement x = xElement.Element(xName);

            if (x != null) return x;

            return new XElement(xName);
        }

        /// <summary>
        /// Get XElements
        /// </summary>
        /// <param name="xElement">parent xElement</param>
        /// <param name="xName">xName</param>
        /// <returns>XElements</returns>
        public static IEnumerable<XElement> GetXElements(XElement xElement, string xName)
        {
            var x = xElement.Elements(xName);
            if(x == null)
                return new List<XElement>();

            return x;
        }

        /// <summary>
        /// 将字符串转化为数字 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            int result = 0;
            bool b = Int32.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为数字 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            decimal result = 0;
            bool b = decimal.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为decimal 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            return GetDecimal(key, 0);
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string key, double defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }

            double result = 0;
            bool b = double.TryParse(key, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转化为double 若值不是数字返回0
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(string key)
        {
            return GetDouble(key, 0);
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回defaultValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(s))
            {
                return defaultValue;
            }

            DateTime result = defaultValue;
            bool b = DateTime.TryParse(s, out result);

            return result;
        }

        /// <summary>
        /// 将字符串转换成日期格式 若不能转换成日期将返回DateTime.MinValue
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s)
        {
            return GetDateTime(s, DateTime.MinValue);
        }
        #endregion
    }
}
