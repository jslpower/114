using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Xml.Linq;
using EyouSoft.HotelBI;
using System.Data;

namespace EyouSoft.ConsoleApp
{
    public class HotelRQ
    {
        #region main
        public static void HotelRQMain(string[] args)
        {
            string t = "0";

            if (args != null && args.Length > 0)
            {
                t = args[0];
            }

            while (true)
            {
                if (t == "0")
                {
                    Console.Write(@"请输入指令类型:
1:城市信息查询缓存指令
2:地标行政区查询缓存指令
3:多酒店静态信息查询缓存指令
4:酒店数据缓存查询指令
5:单酒店静态信息查询指令
6:酒店价格计划控制缓存查询
7:酒店价格计划佣金缓存查询
100:载入地标行政区缓存数据至本地数据库
101:载入酒店价格及佣金信息至本地数据库
exit:退出
");
                    t = Console.ReadLine();
                }


                if (t == "1")
                {
                    CreateCityDetailsSearchRQRequest();
                }
                else if (t == "2")
                {
                    CreateLandMarkSearchRQRequest();
                }
                else if (t == "3")
                {
                    CreateHotelStaticInfoCacheRQRequest();
                }
                else if (t == "4")
                {
                    CreateHotelAvailabilityCacheRQRequest();
                }
                else if (t == "5")
                {
                    CreateRoomTypeStaticInfoCacheRQRequest();
                }
                else if (t == "6")
                {
                    CreateRateplanControlCacheRQRequest();
                }
                else if (t == "7")
                {
                    CreateRateplanCommCacheRQRequest();
                }
                else if (t == "exit")
                {
                    break;
                }
                else if (t == "100")
                {
                    System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
                    stop.Reset();
                    stop.Start();

                    HotelRQ rq = new HotelRQ();
                    rq.LoadLandMark();
                    rq = null;

                    stop.Stop();
                    Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
                }
                else if (t == "101")
                {
                    System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
                    stop.Reset();
                    stop.Start();

                    HotelRQ rq = new HotelRQ();
                    rq.LoadHotel();
                    rq = null;

                    stop.Stop();
                    Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
                }
                else
                {
                    continue;
                }

                t = "0";
            }


        }

        /// <summary>
        /// 城市信息查询缓存指令
        /// </summary>
        static void CreateCityDetailsSearchRQRequest()
        {
            string countryCode = "CN";
            while (true)
            {
                Console.Write("请输入要查询的国家代码 如（CN）：\n");

                countryCode = Console.ReadLine();

                if (countryCode == "exit") break;

                CreateCityDetailsSearchRQRequest(countryCode);
            }
        }

        /// <summary>
        /// 城市信息查询缓存指令
        /// </summary>
        /// <param name="countryCode"></param>
        static void CreateCityDetailsSearchRQRequest(string countryCode)
        {
            EyouSoft.HotelBI.AvailCache.MCityDetailsSearchRQInfo info = new EyouSoft.HotelBI.AvailCache.MCityDetailsSearchRQInfo();
            info.CountryCode = countryCode;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 地标行政区查询缓存指令
        /// </summary>
        static void CreateLandMarkSearchRQRequest()
        {
            string cityCode = "PEK";
            EyouSoft.HotelBI.LandMarkSearchType type = EyouSoft.HotelBI.LandMarkSearchType.POR;

            while (true)
            {
                Console.Write("请输入城市代码 如（PEK）：\n");
                cityCode = Console.ReadLine();
                if (cityCode == "exit") break;

                Console.Write("请输入地标行政区查询类型：0：POR(地标信息)，1：DST(行政区信息)，2：TRA(交通信息)，3：SGT(周围景观)\n");
                int t = 0;
                int.TryParse(Console.ReadLine(), out t);
                type = (EyouSoft.HotelBI.LandMarkSearchType)t;

                CreateLandMarkSearchRQRequest(cityCode, (EyouSoft.HotelBI.LandMarkSearchType)type);
            }
        }

        /// <summary>
        /// 地标行政区查询缓存指令
        /// </summary>
        /// <param name="cityCode"></param>
        /// <param name="type"></param>
        static void CreateLandMarkSearchRQRequest(string cityCode, EyouSoft.HotelBI.LandMarkSearchType type)
        {
            EyouSoft.HotelBI.AvailCache.MLandMarkSearchRQInfo info = new EyouSoft.HotelBI.AvailCache.MLandMarkSearchRQInfo();
            info.CityCode = cityCode;
            info.LandMarkType = type;


            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 多酒店静态信息查询缓存指令
        /// </summary>
        static void CreateHotelStaticInfoCacheRQRequest()
        {
            string cityCode = "PEK";

            while (true)
            {
                Console.Write("请输入城市代码 如（PEK）：\n");
                cityCode = Console.ReadLine();
                if (cityCode == "exit") break;
                CreateHotelStaticInfoCacheRQRequest(cityCode);
            }
        }

        /// <summary>
        /// 多酒店静态信息查询缓存指令
        /// </summary>
        /// <param name="cityCode"></param>
        static void CreateHotelStaticInfoCacheRQRequest(string cityCode)
        {
            EyouSoft.HotelBI.AvailCache.MHotelStaticInfoCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MHotelStaticInfoCacheRQInfo();
            info.CityCode = cityCode;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 酒店数据缓存查询指令
        /// </summary>
        static void CreateHotelAvailabilityCacheRQRequest()
        {
            string hotelCode = "SOHOTO0011";
            while (true)
            {
                Console.Write("请输入要查询的酒店代码 如（SOHOTO0011）：\n");

                hotelCode = Console.ReadLine();

                if (hotelCode == "exit") break;

                CreateHotelAvailabilityCacheRQRequest(hotelCode);
            }
        }

        /// <summary>
        /// 酒店数据缓存查询指令
        /// </summary>
        /// <param name="hotelCode"></param>
        static void CreateHotelAvailabilityCacheRQRequest(string hotelCode)
        {
            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            EyouSoft.HotelBI.AvailCache.MHotelAvailabilityCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MHotelAvailabilityCacheRQInfo();
            info.HotelCode = hotelCode;
            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);
            CreateFile(hotelCode + "RQ.xml", info.RequestXML);
            CreateFile(hotelCode + "RS.xml", s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 单酒店静态信息查询指令
        /// </summary>
        static void CreateRoomTypeStaticInfoCacheRQRequest()
        {
            string hotelCode = "SOHOTO0011";
            while (true)
            {
                Console.Write("请输入要查询的酒店代码 如（SOHOTO0011）：\n");

                hotelCode = Console.ReadLine();

                if (hotelCode == "exit") break;

                CreateRoomTypeStaticInfoCacheRQRequest(hotelCode);
            }
        }

        /// <summary>
        /// 单酒店静态信息查询指令
        /// </summary>
        /// <param name="hotelCode"></param>
        static void CreateRoomTypeStaticInfoCacheRQRequest(string hotelCode)
        {
            EyouSoft.HotelBI.AvailCache.MRoomTypeStaticInfoCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MRoomTypeStaticInfoCacheRQInfo();
            info.HotelCode = hotelCode;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 酒店价格计划控制缓存查询
        /// </summary>
        static void CreateRateplanControlCacheRQRequest()
        {
            string hotelCode = "SOHOTO0011";
            while (true)
            {
                Console.Write("请输入要查询的酒店代码 如（SOHOTO0011）：\n");

                hotelCode = Console.ReadLine();

                if (hotelCode == "exit") break;

                CreateRateplanControlCacheRQRequest(hotelCode);
            }
        }

        /// <summary>
        /// 酒店价格计划控制缓存查询
        /// </summary>
        /// <param name="hotelCode"></param>
        static void CreateRateplanControlCacheRQRequest(string hotelCode)
        {
            EyouSoft.HotelBI.AvailCache.MRateplanControlCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MRateplanControlCacheRQInfo();
            info.HotelCode = hotelCode;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }

        /// <summary>
        /// 酒店价格计划佣金缓存查询
        /// </summary>
        static void CreateRateplanCommCacheRQRequest()
        {
            string hotelCode = "SOHOTO0011";
            while (true)
            {
                Console.Write("请输入要查询的酒店代码 如（SOHOTO0011）：\n");

                hotelCode = Console.ReadLine();

                if (hotelCode == "exit") break;

                CreateRateplanCommCacheRQRequest(hotelCode);
            }
        }

        /// <summary>
        /// 酒店价格计划佣金缓存查询
        /// </summary>
        /// <param name="hotelCode"></param>
        static void CreateRateplanCommCacheRQRequest(string hotelCode)
        {
            EyouSoft.HotelBI.AvailCache.MRateplanCommCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MRateplanCommCacheRQInfo();
            info.HotelCode = hotelCode;

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            Console.WriteLine(info.RequestXML);
            string s = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            Console.WriteLine(s);

            stop.Stop();
            Console.WriteLine("执行时间:" + stop.ElapsedMilliseconds);
        }
        #endregion

        #region static constants
        //static constants
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["HotelStore"].ConnectionString;
        private const string SQL_SELECT_HotelCity = "SELECT CityCode FROM tbl_HotelCity";
        private const string SQL_INSERT_HotelLandMark = "INSERT INTO [tbl_HotelLandMark]([HotelId],[HotelCode],[LandMarkType],[CityCode],[LandMarkName])SELECT HotelId,@HotelCode,@LandMarkType,@CityCode,@LandMarkName FROM tbl_Hotel WHERE HotelCode=@HotelCode";
        private const string SQL_SELECT_GetHotels = "SELECT HotelId,HotelCode FROM tbl_Hotel";
        private const string SQL_DELETE_HotelRate = "DELETE FROM tbl_HotelRate WHERE HotelId=@HotelId;";
        private const string SQL_DELETE_HotelRateComm = "DELETE FROM tbl_HotelRatePlanComm WHERE HotelId=@HotelId";
        private const string SQL_INSERT_HotelRate = "INSERT INTO [tbl_HotelRate]([HotelId],[RoomTypeId],[VendorCode],[RatePlanCode],[Payment],[AmountPrice],[AmountBeforeTax],[DisplayPrice],[StartDate],[EndDate],[FreeMeal]) SELECT @HotelId,RoomTypeId,@VendorCode,@RatePlanCode,@Payment,@AmountPrice,@AmountBeforeTax,@DisplayPrice,@StartDate,@EndDate,@FreeMeal FROM tbl_HotelRoomType WHERE HotelId=@HotelId AND RoomTypeCode=@RoomTypeCode";
        private const string SQL_INSERT_HotelRateComm = "INSERT INTO [tbl_HotelRatePlanComm]([RatePlanCommId],[HotelId],[RoomTypeId],[RatePlanCode],[VendorCode],[StartDate],[EndDate],[Percent],[Fix],[Commisiontype]) SELECT NEWID(),@HotelId,RoomTypeId,@RatePlanCode,@VendorCode,@StartDate,@EndDate,@Percent,@Fix,@Commisiontype FROM tbl_HotelRoomType WHERE HotelId=@HotelId AND RoomTypeCode=@RoomTypeCode";
        private const string SQL_UPDATE_SetHotelSortPrice = "UPDATE tbl_hotel SET SortPrice=ISNULL((SELECT TOP 1 AmountPrice FROM tbl_HotelRate AS B WHERE B.HotelId=A.HOtelId),0) FROM tbl_Hotel A";
        #endregion

        #region private members
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="s"></param>
        private static void CreateFile(string fileName, string s)
        {
            CreateFile(fileName, s, false);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="s"></param>
        private static void CreateFile(string fileName, string s, bool isAppend)
        {
            string filePath = ConfigurationManager.AppSettings.Get("HotelRQTmpFilePath");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string path = filePath + fileName;

            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            try
            {
                StreamWriter sw = new StreamWriter(path, isAppend, System.Text.Encoding.UTF8);
                sw.Write(s);
                sw.Close();
            }
            catch { }
        }

        /// <summary>
        /// 载入酒店价格信息
        /// </summary>
        /// <param name="hotelId">酒店编号</param>
        /// <param name="hotelCode">酒店代码</param>
        private void LoadHotelRate(string hotelId, string hotelCode)
        {
            Console.WriteLine("正在载入酒店" + hotelCode + "价格信息");
            EyouSoft.HotelBI.AvailCache.MHotelAvailabilityCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MHotelAvailabilityCacheRQInfo();
            info.HotelCode = hotelCode;

            string xml = string.Empty;

            try
            {
                xml = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            }
            catch (Exception e)
            {
                CreateFile("HttpRequestRateError.txt", e.Message + e.StackTrace + "\n" + info.RequestXML + "\n", true);
                return;
            }

            if (string.IsNullOrEmpty(xml))
            {
                Console.WriteLine("酒店" + hotelCode + "价格信息请求返回的XML数据为空");
                CreateFile("HttpRequestRateError.txt", "价格信息请求返回的XML数据为空" + info.RequestXML + "\n", true);
                return;
            }

            SqlParameter parm = new SqlParameter("@HotelId", SqlDbType.Char);
            parm.Value = hotelId;

            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_DELETE_HotelRate, parm);

            try
            {
                XElement xResponse = XElement.Parse(xml);
                XElement xHotelAvailabilityCacheRS = Utils.GetXElement(xResponse, "HotelAvailabilityCacheRS");
                XElement xHotelRoomTypesRoot = Utils.GetXElement(xHotelAvailabilityCacheRS, "HotelRoomTypes");
                var xHotelRoomTypes = Utils.GetXElements(xHotelRoomTypesRoot, "HotelRoomType");

                SqlParameter[] parms ={
                                      new SqlParameter("@HotelId",SqlDbType.NVarChar),
                                      new SqlParameter("@RoomTypeCode",SqlDbType.NVarChar),
                                      new SqlParameter("@VendorCode",SqlDbType.NVarChar),
                                      new SqlParameter("@RatePlanCode",SqlDbType.NVarChar),
                                      new SqlParameter("@Payment",SqlDbType.NVarChar),
                                      new SqlParameter("@AmountPrice",SqlDbType.Money),
                                      new SqlParameter("@AmountBeforeTax",SqlDbType.Money),
                                      new SqlParameter("@DisplayPrice",SqlDbType.Money),
                                      new SqlParameter("@StartDate",SqlDbType.DateTime),
                                      new SqlParameter("@EndDate",SqlDbType.DateTime),
                                      new SqlParameter("@FreeMeal",SqlDbType.Int),
                                 };

                foreach (var xHotelRoomType in xHotelRoomTypes)
                {
                    string roomTypeCode = Utils.GetXAttributeValue(xHotelRoomType, "RoomType");
                    string vendorCode = Utils.GetXAttributeValue(xHotelRoomType, "Vendor");
                    string ratePlanCode = Utils.GetXAttributeValue(xHotelRoomType, "RatePlan");

                    XElement xRatesRoot = Utils.GetXElement(xHotelRoomType, "Rates");
                    var xRates = Utils.GetXElements(xRatesRoot, "Rate");

                    foreach (var xRate in xRates)
                    {
                        string payment = Utils.GetXAttributeValue(xRate, "Payment");
                        decimal amountPrice = Utils.GetDecimal(Utils.GetXElement(xRate, "AmountPrice").Value);
                        decimal displayPrice = Utils.GetDecimal(Utils.GetXElement(xRate, "DisplayPrice").Value);
                        decimal amountBeforeTax = Utils.GetDecimal(Utils.GetXElement(xRate, "AmountBeforeTax").Value);
                        DateTime startDate = Utils.GetDateTime(Utils.GetXAttributeValue(xRate, "StartDate"), DateTime.Now);
                        DateTime endDate = Utils.GetDateTime(Utils.GetXAttributeValue(xRate, "EndDate"), DateTime.Now);
                        int freeMeal = (int)Utils.GetDecimal(Utils.GetXElement(xRate, "FreeMeal").Value, 0);

                        parms[0].Value = hotelId;
                        parms[1].Value = roomTypeCode;
                        parms[2].Value = vendorCode;
                        parms[3].Value = ratePlanCode;
                        parms[4].Value = payment;
                        parms[5].Value = amountPrice;
                        parms[6].Value = amountBeforeTax;
                        parms[7].Value = displayPrice;
                        parms[8].Value = startDate;
                        parms[9].Value = endDate;
                        parms[10].Value = freeMeal;

                        Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_INSERT_HotelRate, parms);
                    }
                }
            }
            catch (Exception e)
            {
                CreateFile("ParseRateXMLError.txt", e.Message + e.StackTrace + "\n" + xml + "\n", true);
                return;
            }


        }

        /// <summary>
        /// 载入酒店价格计划佣金信息
        /// </summary>
        /// <param name="hotelId">酒店编号</param>
        /// <param name="hotelCode">酒店代码</param>
        private void LoadHotelRatePlanComm(string hotelId, string hotelCode)
        {
            Console.WriteLine("正在载入酒店" + hotelCode + "价格计划佣金信息");
            EyouSoft.HotelBI.AvailCache.MRateplanCommCacheRQInfo info = new EyouSoft.HotelBI.AvailCache.MRateplanCommCacheRQInfo();
            info.HotelCode = hotelCode;

            string xml = string.Empty;

            try
            {
                xml = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);
            }
            catch (Exception e)
            {
                CreateFile("HttpRequestRatePlanCommError.txt", e.Message + e.StackTrace + "\n" + info.RequestXML + "\n", true);
                return;
            }

            if (string.IsNullOrEmpty(xml))
            {
                Console.WriteLine("酒店" + hotelCode + "价格计划佣金信息请求返回的XML数据为空");
                CreateFile("HttpRequestRatePlanCommError.txt", "价格计划佣金信息请求返回的XML数据为空" + info.RequestXML + "\n", true);
                return;
            }

            SqlParameter parm = new SqlParameter("@HotelId", SqlDbType.Char);
            parm.Value = hotelId;

            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_DELETE_HotelRateComm, parm);

            try
            {
                XElement xResponse = XElement.Parse(xml);
                XElement xRatePlanCommRS = Utils.GetXElement(xResponse, "RatePlanCommRS");
                XElement xRatePlanCommsRoot = Utils.GetXElement(xRatePlanCommRS, "RatePlanComms");
                var xRatePlanComms = Utils.GetXElements(xRatePlanCommsRoot, "RatePlanComm");

                SqlParameter[] parms ={
                                      new SqlParameter("@HotelId",SqlDbType.Char),
                                      new SqlParameter("@RatePlanCode",SqlDbType.Char),
                                      new SqlParameter("@VendorCode",SqlDbType.Char),
                                      new SqlParameter("@StartDate",SqlDbType.DateTime),
                                      new SqlParameter("@EndDate",SqlDbType.DateTime),
                                      new SqlParameter("@Percent",SqlDbType.Decimal),
                                      new SqlParameter("@Fix",SqlDbType.Money),
                                      new SqlParameter("@Commisiontype",SqlDbType.Char),
                                      new SqlParameter("@RoomTypeCode",SqlDbType.Char),
                                 };

                parms[0].Value = hotelId;

                foreach (var xRatePlanComm in xRatePlanComms)
                {
                    string roomTypeCode = Utils.GetXElement(xRatePlanComm, "RoomTypeCode").Value;
                    string ratePlanCode = Utils.GetXElement(xRatePlanComm, "RatePlanCode").Value;
                    string vendorCode = Utils.GetXElement(xRatePlanComm, "VendorCode").Value;
                    DateTime startDate = Utils.GetDateTime(Utils.GetXElement(xRatePlanComm, "StartDate").Value);
                    DateTime endDate = Utils.GetDateTime(Utils.GetXElement(xRatePlanComm, "EndDate").Value);
                    decimal percent = Utils.GetDecimal(Utils.GetXElement(xRatePlanComm, "Percent").Value);
                    decimal fix = Utils.GetDecimal(Utils.GetXElement(xRatePlanComm, "Fix").Value);
                    string commisiontype = Utils.GetXElement(xRatePlanComm, "Commisiontype").Value;

                    parms[1].Value = ratePlanCode;
                    parms[2].Value = vendorCode;
                    parms[3].Value = startDate;
                    parms[4].Value = endDate;
                    parms[5].Value = percent;
                    parms[6].Value = fix;
                    parms[7].Value = commisiontype;
                    parms[8].Value = roomTypeCode;

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_INSERT_HotelRateComm, parms);
                }
            }
            catch (Exception e)
            {
                CreateFile("ParseRatePlanCommXMLError.txt", e.Message + e.StackTrace + "\n" + xml + "\n", true);
                return;
            }

        }

        /// <summary>
        /// 设置酒店排序价格
        /// </summary>
        private void SetHotelSortPrice()
        {
            Console.WriteLine("正在设置酒店排序价格");
            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_UPDATE_SetHotelSortPrice);
            Console.WriteLine("设置酒店排序价格完成");
        }
        #endregion

        #region public members
        /// <summary>
        /// 加载城市地标行政区信息
        /// </summary>
        public void LoadLandMark()
        {
            Console.WriteLine("正在加载酒店地标信息");
            IList<string> citys = new List<string>();
            using (SqlDataReader rdr = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, SQL_SELECT_HotelCity))
            {
                while (rdr.Read())
                {
                    citys.Add(rdr[0].ToString());
                }
            }

            if (citys != null && citys.Count > 0)
            {
                foreach (string s in citys)
                {
                    try
                    {
                        LoadLandMarkPOR(s);
                    }
                    catch (Exception e)
                    {
                        CreateFile("LoadLandMarkError.txt", e.Message + e.StackTrace, true);
                    }
                }
            }

            Console.WriteLine("酒店地标信息加载完成！");
        }

        /// <summary>
        /// 地标信息
        /// </summary>
        /// <param name="cityCode">城市代码</param>
        private void LoadLandMarkPOR(string cityCode)
        {
            Console.WriteLine("正在加载" + cityCode + "地标信息");
            EyouSoft.HotelBI.AvailCache.MLandMarkSearchRQInfo info = new EyouSoft.HotelBI.AvailCache.MLandMarkSearchRQInfo();
            info.CityCode = cityCode;
            info.LandMarkType = EyouSoft.HotelBI.LandMarkSearchType.POR;

            string xml = xml = EyouSoft.HotelBI.Utils.CreateRequest(info.RequestXML, true);

            if (string.IsNullOrEmpty(xml))
            {
                Console.WriteLine(cityCode + "地标信息加载失败,请求返回的XML数据为空");
                return;
            }

            XElement xResponse = XElement.Parse(xml);
            XElement xLandMarkSearchRS = xResponse.Element("LandMarkSearchRS");
            XElement xLandMarks = EyouSoft.HotelBI.Utils.GetXElement(xLandMarkSearchRS, "LandMarks");
            var xLandMarkInfos = EyouSoft.HotelBI.Utils.GetXElements(xLandMarks, "LandMarkInfo");

            foreach (var xLandMarkInfo in xLandMarkInfos)
            {
                string landMarkName = EyouSoft.HotelBI.Utils.GetXElement(xLandMarkInfo, "LandMarkName").Value;
                string hotels = EyouSoft.HotelBI.Utils.GetXElement(xLandMarkInfo, "Hotels").Value;

                string[] hotelCodes = hotels.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (hotelCodes == null || hotelCodes.Length < 1) continue;

                SqlParameter[] parms ={
                                      new SqlParameter("@HotelCode",DbType.String),
                                      new SqlParameter("@LandMarkType",DbType.Byte),
                                      new SqlParameter("@CityCode",DbType.String),
                                      new SqlParameter("@LandMarkName",DbType.String)
                                 };

                foreach (string hotelCode in hotelCodes)
                {
                    parms[0].Value = hotelCode;
                    parms[1].Value = EyouSoft.HotelBI.LandMarkSearchType.POR;
                    parms[2].Value = cityCode;
                    parms[3].Value = landMarkName;

                    Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, SQL_INSERT_HotelLandMark, parms);
                }
            }
        }

        /// <summary>
        /// 载入酒店价格及佣金信息
        /// </summary>
        public void LoadHotel()
        {
            DataSet ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, SQL_SELECT_GetHotels);

            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 1)
            {
                Console.WriteLine("未找到任何要载入的酒店信息");
                return;
            }

            int i = 0;
            foreach (System.Data.DataRow row in ds.Tables[0].Rows)
            {
                Console.WriteLine("Hotel:" + ++i);
                string hotelId = row["HotelId"].ToString().Trim();
                string hotelCode = row["HotelCode"].ToString().Trim();

                if (string.IsNullOrEmpty(hotelId) || string.IsNullOrEmpty(hotelCode)) continue;

                try
                {
                    LoadHotelRate(hotelId, hotelCode);
                }
                catch (Exception e)
                {
                    CreateFile("LoadHotelRateError.txt", e.Message + e.StackTrace, true);
                }

                try
                {
                    LoadHotelRatePlanComm(hotelId, hotelCode);
                }
                catch (Exception e)
                {
                    CreateFile("LoadHotelRatePlanCommError.txt", e.Message + e.StackTrace, true);
                }
            }

            try
            {
                SetHotelSortPrice();
            }
            catch (Exception e)
            {
                CreateFile("SetHotelSortPriceError.txt", e.Message + e.StackTrace, true);
            }
        }
        #endregion
    }
}
