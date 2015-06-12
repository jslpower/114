using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using EyouSoft.Model.PoolStructure;

namespace WEB
{
    public partial class TestByWQZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //EyouSoft.BLL.MQStructure.IMMember.CreateInstance().SetGroupMember(1000900,109);
           //int number = EyouSoft.BLL.MQStructure.IMMember.CreateInstance().GetGroupMember(1000900);
           //Response.Write(number);

            //EyouSoft.BLL.TourStructure.Tour.CreateInstance().IncreaseClicks("00016d8b-5bab-4c04-9dce-ec03d0a1d93f");
            //this.TestGetHoterOrderInfo("F1WJET");
            //this.TestL();
            //this.TestGetHoterOrderInfo("F1P2P9");
            //this.TestCreateHotelOrder();
            //this.TestHotelBI();
            #region
            //this.TestCreateHotelOrder();
            //this.TestHotelBI();
            //this.TestTran();
            //Server.Transfer("TestByLugy.aspx");
            //this.InsertFundRegister();
            //this.DbTypeToSqlDbType();


            //int recordcount1 = 0;
            //var nnnn = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetWholesalersStatistics(10, 1, ref recordcount1, 0, null, null, 0, 0, null, null);

            //var b1 = EyouSoft.BLL.TourStructure.Tour.CreateInstance().IsDeleted("00139778-ba56-4306-a35b-fc7877b16a90");
            //var b2 = EyouSoft.BLL.TourStructure.Tour.CreateInstance().IsDeleted("2cd51008-ab82-46bf-a3c8-d0bd081a8c38");
            //var b3 = EyouSoft.BLL.TourStructure.Tour.CreateInstance().IsDeleted("2cd51008-ab82-46bf-a3c8-d0bd081a8c39");
            //var b4 = EyouSoft.BLL.TourStructure.Tour.CreateInstance().IsDeleted("");


            //var o = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(362, EyouSoft.Model.AdvStructure.AdvPosition.酒店频道通栏banner1);

            //IList<string> c=new List<string>();
            //c.Add(Guid.NewGuid().ToString());
            //c.Add(Guid.NewGuid().ToString());

            //EyouSoft.BLL.SystemStructure.CompanyCityAd.CreateInstance().AddCompanyCityAd(203, 2, c);

            //var obj=EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance().GetApplyInfo("fb23a8f2-75e0-4850-966a-ca354a373620");

            //this.InitAreas();
            //this.GetMQRemindHavingOrderTours();

            //Response.Write(EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourInfo("07d3dfcb-ad98-4e10-9865-40104b7f34d7").TourContact);

            

            //EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea("1B8549C0-0BDF-41C8-A145-79B77DBDCEB9");
            //EyouSoft.IBLL.TourStructure.ICompanyContractSignet bll = EyouSoft.BLL.TourStructure.CompanyContractSignet.CreateInstance();

            /*
            EyouSoft.BLL.TestBLL.CreateInstance().WT();

            byte a = 1;
            int b = (int)a;


            var x = new[]
            {
                new { Id = (byte)1, Name = "Tom", Age = (int?)null },
                new { Id = (byte)2, Name = "Jim", Age = (int?)10 }
            };

            using (var r = x.ToDataReader())
            {
                while (r.Read())
                {
                    Response.Write(string.Format("Id:{0} Name:{1} Age:{2}<br/>", r.GetByte(0), r.GetString(1), r.GetNullableInt32(2)));
                }
            }*/


            //EyouSoft.BLL.TourStructure.Tour.CreateInstance().SetTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState.促销, "推广说明AAAAA", "019f6c61-0746-4719-95fa-22a574bd8741", "01c31213-4751-47b3-97b9-7835534100d2");

            //string[] a = new string[] { "8c1aaac3-f4ab-434b-9af7-24d851aacb43", "9fb433af-aff7-47d2-8052-973daf80895a", "a2e8a926-fd8b-4293-8f15-0eb0fd43c28e", "acfbf14a-85da-407e-9093-6325148b97c4", "bdf206d5-cd2d-403f-b55f-823214358ab8", "ce1f2bae-98b0-4a00-9710-ab0be0b4df0f" };
            //EyouSoft.BLL.TourStructure.Tour.CreateInstance().SetTourState(EyouSoft.Model.TourStructure.TourState.收客, a);

            //EyouSoft.Model.TourStructure.TourInfo t = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourInfo("0267e9cc-5a87-42ed-a2ed-83cc8912c3b2");


            //int recordcount=0;
            //EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetStartingTours(10, 1, ref recordcount, "1e5edc20-4b83-4c16-8c52-008e0c09bd1e", null, null, null, null, null);

            //object obj=EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetUndisposedOrderTours(10, 1, ref recordcount, "1e5edc20-4b83-4c16-8c52-008e0c09bd1e", null);

            //EyouSoft.BLL.SMSStructure.Account.CreateInstance();
            //IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> tours=EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetChildrenTours("25b85384-38a1-409f-b74d-2233eb9dcf4c");
            //

            //this.AdvTest();

            //EyouSoft.BLL.SystemStructure.ProductSuggestion.CreateInstance();
            //int recordCount=0;
            //IList<EyouSoft.Model.TourStructure.TourVisitInfo> items=EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourVisitedHistorys(10, 1, ref recordCount, "ce1f2bae-98b0-4a00-9710-ab0be0b4df0f");
            //Response.Write(items.Count);

            //IList<EyouSoft.Model.TourStructure.TourBasicInfo> itmes = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByCompanyId(10, 1, ref recordCount, 373, 183, "29d046a2-7c8a-4196-9a67-965b8af5e0fe", false);

            //IList<EyouSoft.Model.TourStructure.TourPriceDetail> prices = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTourPriceDetail("07884864-91a5-456f-ae17-302a04c627ed");

            //EyouSoft.BLL.TourStructure.Tour.CreateInstance().DeleteByVirtual("0035fef2-bc08-4618-839e-2c75e29efc00");

            //ApplyServiceTest();

            //AdvTest1();


            //IList<EyouSoft.Model.TourStructure.TourInfo> list=EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetEshopTours(10, 1, ref recordCount, "1dd19a67-622e-476e-8dd1-342661804ea1", null, null, null, null, null);
            #endregion
        }

        private void TestL()
        {
            IList<ContacterInfo> mobiles = new List<ContacterInfo>();

            mobiles.Add(new ContacterInfo() { ContacterId = 0, Mobile = "1" });
            mobiles.Add(new ContacterInfo() { ContacterId = 0, Mobile = "123456789" });
            mobiles.Add(new ContacterInfo() { ContacterId = 0, Mobile = "123456789" });
            mobiles.Add(new ContacterInfo() { ContacterId = 0, Mobile = "1" });
            mobiles.Add(new ContacterInfo() { ContacterId = 0, Mobile = "0" });

            IList<string> existsMobiles = new List<string>();

            if (mobiles != null && mobiles.Count > 0)
            {
                var iEnumerable = mobiles.GroupBy(i => i.Mobile);

                foreach (var iGrouping in iEnumerable)
                {
                    if (iGrouping.Count() > 1) existsMobiles.Add(iGrouping.Key);
                }
            }

            Response.Write(existsMobiles.Count);
        }

        private void TestGetHoterOrderInfo(string resOrderId)
        {
            int errorCode;
            string errorDesc;
            var info = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetInfo(resOrderId, out errorCode, out errorDesc);


        }

        private void TestCancelHotelOrder(string resOrderId)
        {
            string errorDesc;
            var result = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().Cancel(resOrderId, "取消订单测试", out errorDesc);
        }

        private void TestCreateHotelOrder()
        {
            EyouSoft.Model.HotelStructure.OrderInfo info = new EyouSoft.Model.HotelStructure.OrderInfo();
            info.HotelCode = "SOHOTO0738";
            info.CityCode = "HGH";
            info.CountryCode = "CN";
            info.RatePlanCode = "BK002";
            info.RoomTypeCode = "INCE003";
            info.CheckInDate = DateTime.Parse("2010-12-07");
            info.CheckOutDate = DateTime.Parse("2010-12-15");
            info.VendorCode = "SOHOTO";
            info.VendorName = "SOHOTO";
            info.Quantity = 2;
            info.ArriveEarlyTime = "1600";
            info.ArriveLateTime = "1800";
            info.TotalAmount = 800;
            info.ResGuests = new List<EyouSoft.HotelBI.HBEResGuestInfo>();
            EyouSoft.HotelBI.HBEResGuestInfo guest1 = new EyouSoft.HotelBI.HBEResGuestInfo();
            guest1.GuestTypeIndicator = EyouSoft.HotelBI.HBEGuestTypeIndicator.D;
            guest1.IsMobileContact = true;
            guest1.Mobile = "15868456678";
            guest1.PersonName = "test汪奇志";
            info.ResGuests.Add(guest1);

            EyouSoft.HotelBI.HBEResGuestInfo guest2 = new EyouSoft.HotelBI.HBEResGuestInfo();
            guest2.GuestTypeIndicator = EyouSoft.HotelBI.HBEGuestTypeIndicator.F;
            guest2.IsMobileContact = true;
            guest2.Mobile = "13777476875";
            guest2.PersonName = "test张志瑜";
            info.ResGuests.Add(guest2);

            info.ContacterFullname = "test周文超";
            info.ContacterMobile = "15168353279";
            info.IsMobileContact = true;
            info.BuyerCId = "201012030001";
            info.BuyerUId = "201012030001";
            info.SpecialRequest = "测试时的特殊要求";


            string errorDesc;
            int result = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().Add(info, out errorDesc);

            if (result > 0)
            {
                //this.TestCancelHotelOrder(info.ResOrderId);
                Response.Write(info.ResOrderId);
            }
        }

        private void TestHotelBI()
        {
            string xml = "";
            string xml1 = "<OTResponse>		<Errors>			<Error ErrorCode=\"错误代码1\" ErrorDesc=\"错误描述1\"/>		</Errors></OTResponse>";
            string xml2 = "<OTResponse>	<TransactionName>TH_HotelCancelRQ</TransactionName>	<HotelCancelRS>		<Errors>			<Error ErrorCode=\"错误代码2\" ErrorDesc=\"错误描述2\"/>		</Errors>	</HotelCancelRS></OTResponse>";
            var errorInfo1=EyouSoft.HotelBI.Utils.ResponseErrorHandling(xml);
            var errorInfo2 = EyouSoft.HotelBI.Utils.ResponseErrorHandling(xml1);
            var errorInfo3 = EyouSoft.HotelBI.Utils.ResponseErrorHandling(xml2);

        }

        private void TestTran()
        {
            if (EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().TestTran())
                Response.Write("success");
            else
                Response.Write("failed");
        }

        private void InsertFundRegister()
        {
            EyouSoft.Model.ToolStructure.FundRegisterInfo register = new EyouSoft.Model.ToolStructure.FundRegisterInfo();
            register.BillingAmount = 50;
            register.ContactName = "汪奇志";
            register.InvoiceNo = "201011090001";
            register.IsBilling = true;
            register.IsChecked = true;
            register.IssueTime = DateTime.Now;
            register.ItemAmount = 50;
            register.ItemId = "2c9a9578-e0e2-4e5c-8752-739057913576";
            register.ItemTime = DateTime.Now;
            register.OperatorId = "2c9a9578-e0e2-4e5c-8752-739057913576";
            register.PayType = EyouSoft.Model.ToolStructure.FundPayType.签单挂账;
            register.RegisterId = Guid.NewGuid().ToString();
            register.RegisterType = EyouSoft.Model.ToolStructure.FundRegisterType.收款;
            register.Remark = "备注...";

            Response.Write(EyouSoft.BLL.ToolStructure.FundRegister.CreateInstance().Add(register));
        }

        private void AdvTest1()
        {
            EyouSoft.Model.AdvStructure.AdvInfo info = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvInfo(243);

            Response.Write(EyouSoft.BLL.AdvStructure.Adv.CreateInstance().IsValid(info.Position, info.StartDate, info.EndDate, info.Range, info.Relation,info.AdvId));
        }

        private void DbTypeToSqlDbType()
        {
            foreach (object obj in Enum.GetValues(typeof(System.Data.DbType)))
            {
                try
                {
                    Response.Write(obj.ToString() + ":" + ConvertToSqlDbType((DbType)obj) + "</br>");
                }
                catch { }
            }
        }

        /// <summary>
        /// 广告测试
        /// </summary>
        private void AdvTest()
        {
            int recordCount = 0;
            IList<int> relation = new List<int>();
            EyouSoft.IBLL.AdvStructure.IAdv bll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();

            DateTime d1 = Convert.ToDateTime("2010-07-27");
            DateTime d2 = Convert.ToDateTime("2010-08-06");

            //bool val = bll.IsValid(EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道促销广告, d1, d2, EyouSoft.Model.AdvStructure.AdvRange.全国, null);

            EyouSoft.Model.AdvStructure.AdvInfo advInfo = new EyouSoft.Model.AdvStructure.AdvInfo();

            advInfo.Position = EyouSoft.Model.AdvStructure.AdvPosition.首页广告精品推荐图文;
            advInfo.Category = EyouSoft.Model.AdvStructure.AdvCategory.同业114广告;
            advInfo.Title = "test title";
            advInfo.Remark = "test content";
            advInfo.RedirectURL = "www.baidu.com";
            advInfo.ImgPath = "图片路径";
            //advInfo.CompanyId = "1e5edc20-4b83-4c16-8c52-008e0c09bd1e";
            //advInfo.CompanyName = "测试公司名称";
            advInfo.ContactInfo = "联系信息";
            advInfo.StartDate = d1;
            advInfo.EndDate = d2;
            advInfo.OperatorId = 4;
            advInfo.OperatorName = "汪奇志";
            advInfo.IssueTime = DateTime.Now;
            advInfo.Range = EyouSoft.Model.AdvStructure.AdvRange.全国;
            advInfo.Relation = null;
            relation.Add(1);
            advInfo.Relation = relation;

            //Response.Write(bll.InsertAdv(advInfo)+"<br/>");
            
            
            //advInfo.Range = EyouSoft.Model.AdvStructure.AdvRange.全国;
            //relation.Add(1);
            //relation.Add(2);
            //relation.Add(4);
            //relation.Add(17);
            //advInfo.Relation = relation;
            //advInfo.AdvId = 33;
            //advInfo.Title = "test title update";
            //bll.UpdateAdv(advInfo);

            //前台
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advs = bll.GetAdvs(362, EyouSoft.Model.AdvStructure.AdvPosition.首页广告优秀企业展示);
            //运营后台
            //IList<EyouSoft.Model.AdvStructure.AdvInfo> advs1=bll.GetAdvs(10, 1,ref recordCount, EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告1, 1, null,null, d1, d2);
            //bll.SetAdvSort(50, EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告1, 2, 100);
            //Response.Write(advs.Count+"<br/>"+recordCount);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyServiceTest()
        {
            int recordCount=0;
            EyouSoft.Model.SystemStructure.SysApplyServiceInfo info = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();
           
            info.ApplyServiceType = EyouSoft.Model.CompanyStructure.SysService.HighShop;
            info.ApplyText = "www.g.com";
            info.ApplyTime = DateTime.Now;
            info.CityId = 1;
            info.CityName = "aq";
            info.CompanyId = "bdfa4c0c-9ebc-497c-93d0-dd7255e5cab4";
            info.CompanyName = "company name";
            info.ContactAddress = "company address";
            info.ContactMobile = "15888888888";
            info.ContactMQ = "10000";
            info.ContactName = "contact name";
            info.ContactQQ = "10000";
            info.ContactTel = "057188888888";
            info.ProvinceId = 1;
            info.ProvinceName = "ah";
            info.UserId = "916b9d06-72e7-410c-8cd9-a8859d04c85e";

            EyouSoft.IBLL.SystemStructure.ISysApplyService bll = EyouSoft.BLL.SystemStructure.SysApplyService.CreateInstance();

            bll.Apply(info);

            bll.GetApplyInfo("d63d71cb-f1c4-4317-b944-9bf8884aa41f");

            Response.Write(bll.GetApplys(10, 1, ref recordCount, EyouSoft.Model.CompanyStructure.SysService.HighShop, null, null, null, null, null, null).Count);

            Response.Write(bll.GetApplyState("bdfa4c0c-9ebc-497c-93d0-dd7255e5cab4", EyouSoft.Model.CompanyStructure.SysService.HighShop));

            Response.Write(bll.IsApply("bdfa4c0c-9ebc-497c-93d0-dd7255e5cab4", EyouSoft.Model.CompanyStructure.SysService.HighShop));


            EyouSoft.Model.SystemStructure.EshopCheckInfo checkinfo = new EyouSoft.Model.SystemStructure.EshopCheckInfo();
            checkinfo.ApplyId = "d63d71cb-f1c4-4317-b944-9bf8884aa41f";
            checkinfo.ApplyState = EyouSoft.Model.SystemStructure.ApplyServiceState.审核通过;
            checkinfo.CheckTime = DateTime.Now;
            checkinfo.DomainName = "www.google.com";
            checkinfo.EnableTime = Convert.ToDateTime("2010-01-01");
            checkinfo.ExpireTime = Convert.ToDateTime("2010-01-31");
            checkinfo.OperatorId = 1;

            Response.Write(bll.EshopChecked(checkinfo));

            /*Response.Write(bll.Renewed("d63d71cb-f1c4-4317-b944-9bf8884aa41f", Convert.ToDateTime("2010-02-01")
                , Convert.ToDateTime("2010-08-28"), 1, DateTime.Now));*/



        }


        private System.Data.SqlDbType ConvertToSqlDbType(System.Data.DbType pSourceType)
        {
            SqlParameter paraConver = new SqlParameter();
            paraConver.DbType = pSourceType;
            return paraConver.SqlDbType;
        }

        private void GetMQRemindHavingOrderTours()
        {
            int recordcount=0;

            EyouSoft.Model.TourStructure.OrderState[] defaulUndisposedOrderState = new EyouSoft.Model.TourStructure.OrderState[] { EyouSoft.Model.TourStructure.OrderState.未处理
                , EyouSoft.Model.TourStructure.OrderState.处理中 };

            //IList<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo> tours =
            //    EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetMQRemindHavingOrderTours(1, 1, ref recordcount, "ddfdc500-d41e-459d-a068-242e76479daa", null,null, defaulUndisposedOrderState);

            //int historyTourCount = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetMQRemindHistoryToursCount("ddfdc500-d41e-459d-a068-242e76479daa", "");
        }


        private void InitAreas()
        {
            EyouSoft.Model.SystemStructure.SysCity cityInfo = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(362);
            string output = string.Empty;

            if (cityInfo != null && cityInfo.CityAreaControls != null)
            {
                var cityAreas = cityInfo.CityAreaControls.Select((tmp, index) => new { AreaId = tmp.AreaId, AreaName = tmp.AreaName, AreaType = (int)tmp.RouteType });

                if (cityAreas != null)
                {
                    output = EyouSoft.Common.SerializationHelper.ConvertJSON<List<EyouSoft.Model.SystemStructure.SysCityArea>>(cityInfo.CityAreaControls.ToList());
                }

                //JavaScriptSerializer a = new JavaScriptSerializer();
                //output = a.Serialize(cityAreas);
            }

            Response.Write(output);
        }
    }

   


    #region DataReaderExtensions
    /// <summary>
    /// Provides extension methods for IDataReader
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Returns a string if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A string, or null if the column's value is NULL</returns>
        public static string GetNullableString(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (string)null : reader.GetString(index);
        }

        /// <summary>
        /// Returns a bool if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A bool, or null if the column's value is NULL</returns>
        public static bool? GetNullableBool(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (bool?)null : reader.GetBoolean(index);
        }

        /// <summary>
        /// Returns a DateTime if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A DateTime, or null if the column's value is NULL</returns>
        public static DateTime? GetNullableDateTime(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (DateTime?)null : reader.GetDateTime(index);
        }

        /// <summary>
        /// Returns a byte if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A byte, or null if the column's value is NULL</returns>
        public static byte? GetNullableByte(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (byte?)null : reader.GetByte(index);
        }

        /// <summary>
        /// Returns a short if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A short, or null if the column's value is NULL</returns>
        public static short? GetNullableInt16(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (short?)null : reader.GetInt16(index);
        }

        /// <summary>
        /// Returns an int if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>An int, or null if the column's value is NULL</returns>
        public static int? GetNullableInt32(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (int?)null : reader.GetInt32(index);
        }

        /// <summary>
        /// Returns a float if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A float, or null if the column's value is NULL</returns>
        public static float? GetNullableFloat(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (float?)null : reader.GetFloat(index);
        }

        /// <summary>
        /// Returns a double if one is present, or null if not
        /// </summary>
        /// <param name="reader">The IDbReader to read from</param>
        /// <param name="index">The index of the column to read from</param>
        /// <returns>A double, or null if the column's value is NULL</returns>
        public static double? GetNullableDouble(this IDataRecord reader, int index)
        {
            return reader.IsDBNull(index) ? (double?)null : reader.GetDouble(index);
        }

        /// <summary>
        /// Returns an implementation of IDataReader based on the public properties of T,
        /// given an IEnumerable&lt;T&gt; />
        /// </summary>
        /// <typeparam name="T">A type with properties from which to read</typeparam>
        /// <param name="items">A collection of instances of T</param>
        /// <returns>An implementation of IDataReader based on the public properties of T</returns>
        public static IDataReader ToDataReader<T>(this IEnumerable<T> items)
        {
            return new DataReader<T>(items);
        }


        /// <summary>
        /// Private implementation of IDataReader for an IEnumerable&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">A type with properties from which to read</typeparam>
        private class DataReader<T> : IDataReader
        {
            public DataReader(IEnumerable<T> items)
            {
                _enumerator = items.GetEnumerator();

                foreach (var prop in typeof(T).GetProperties())
                {
                    _properties[prop.Name] = prop;
                }
            }

            private Dictionary<string, PropertyInfo> _properties
                = new Dictionary<string, PropertyInfo>();
            private IEnumerator<T> _enumerator;

            #region IDataReader Members

            public void Close()
            {
            }

            public int Depth
            {
                get { return 0; }
            }

            public DataTable GetSchemaTable()
            {
                throw new NotImplementedException();
            }

            public bool IsClosed
            {
                get { return false; }
            }

            public bool NextResult()
            {
                return false;
            }

            public bool Read()
            {
                return _enumerator.MoveNext();
            }

            public int RecordsAffected
            {
                get { throw new NotImplementedException(); }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion

            #region IDataRecord Members

            public int FieldCount
            {
                get { return _properties.Count; }
            }

            public bool GetBoolean(int i)
            {
                return (bool)GetValue(i);
            }

            public byte GetByte(int i)
            {
                return (byte)GetValue(i);
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public char GetChar(int i)
            {
                return (char)GetValue(i);
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public IDataReader GetData(int i)
            {
                throw new NotImplementedException();
            }

            public string GetDataTypeName(int i)
            {
                var prop = _properties.Values.ToArray()[i];
                return prop.PropertyType.ToString();
            }

            public DateTime GetDateTime(int i)
            {
                return (DateTime)GetValue(i);
            }

            public decimal GetDecimal(int i)
            {
                return (decimal)GetValue(i);
            }

            public double GetDouble(int i)
            {
                return (double)GetValue(i);
            }

            public Type GetFieldType(int i)
            {
                var prop = _properties.Values.ToArray()[i];
                return prop.PropertyType;
            }

            public float GetFloat(int i)
            {
                return (float)GetValue(i);
            }

            public Guid GetGuid(int i)
            {
                return (Guid)GetValue(i);
            }

            public short GetInt16(int i)
            {
                return (short)GetValue(i);
            }

            public int GetInt32(int i)
            {
                return (int)GetValue(i);
            }

            public long GetInt64(int i)
            {
                return (long)GetValue(i);
            }

            public string GetName(int i)
            {
                return _properties.Keys.ToArray()[i];
            }

            public int GetOrdinal(string name)
            {
                return _properties.Keys.ToList().IndexOf(name);
            }

            public string GetString(int i)
            {
                return (string)GetValue(i);
            }

            public object GetValue(int i)
            {
                var prop = _properties.Values.ToArray()[i];
                return prop.GetValue(_enumerator.Current, null);
            }

            public int GetValues(object[] values)
            {
                throw new NotImplementedException();
            }

            public bool IsDBNull(int i)
            {
                var prop = _properties.Values.ToArray()[i];
                var val = prop.GetValue(_enumerator.Current, null);
                return (val == null || val == DBNull.Value);
            }

            public object this[string name]
            {
                get
                {
                    return _properties[name].GetValue(_enumerator.Current,
                        null);
                }
            }

            public object this[int i]
            {
                get
                {
                    var prop = _properties.Values.ToArray()[i];
                    return prop.GetValue(_enumerator.Current, null);
                }
            }

            #endregion
        }
    }
    #endregion
}
