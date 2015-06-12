
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace WEB
{
    public class Person
	{
	    public int ID { get; set; }
	    public string Name { get; set; }
	    public int Age { get; set; }
	    public DateTime BirthDay { get; set; }
	}

    public partial class _Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Request.Cookies["ssotest"] != null)
                {
                    btnLogin.Disabled = true;
                    btnLogin.Value = "已登录";
                    if (Session["UID"] != null)
                        txtUsername.Value = Session["UID"].ToString();
                }
            }
            Response.Write(DateTime.Now);
            Person p = new Person();
	        p.ID = 1;
	        p.Name = "张三";
	        p.Age = 20;
	        p.BirthDay = DateTime.Now.AddYears(-20);

           
        string _Key = "92$#@!#@5tr%u8wsf]543$,23{e7w%$#";
        string _IV = "!54~1)e74&m3+-q#";
        EyouSoft.Common.EncryptUtility.HashCrypto crypto = new EyouSoft.Common.EncryptUtility.HashCrypto();
        crypto = new EyouSoft.Common.EncryptUtility.HashCrypto();
        crypto.Key = _Key;
        crypto.IV = _IV;

        if (Request.QueryString["d"] != null)
        {
            Response.Write(crypto.DeRijndaelEncrypt(Request.QueryString["d"]));
            Response.End();
        }
        else if (Request.QueryString["c"] != null)
        {
            Response.Write(crypto.RijndaelEncrypt(Request.QueryString["c"]));
            Response.End();
        }


            //EyouSoft.HotelBI.SingleSeach searchModel = new EyouSoft.HotelBI.SingleSeach();
            //searchModel.HotelCode = "SOHOTO2235";//酒店代码
            //searchModel.CheckInDate = "2011-01-24";//入住时间
            //searchModel.CheckOutDate = "2011-01-25";//离店时间
            //searchModel.RoomTypeCode = "INCE022";//房型代码
            //searchModel.VendorCode = string.Empty;//供应商代码
            //searchModel.RatePlanCode = "BK001";//价格计划代码
            //searchModel.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;//查询完整酒店信息
            //EyouSoft.HotelBI.ErrorInfo errorInfo = null;//错误信息实体
            //EyouSoft.Model.HotelStructure.HotelInfo hotelModel =
            //    EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(searchModel, out errorInfo);//酒店实体

            //EyouSoft.BLL.ToolStructure.MsgTipRecord.CreateInstance().IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.AddFriend, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, "48144", 362);
            //IList<EyouSoft.Model.TicketStructure.TicketSeattle> tlist = null;
            //tlist = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattleByFreight(null);
            //tlist = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattleByFreight(136);
            //Response.Write(EyouSoft.Common.SerializationHelper.ConvertJSON<Person>(p));
            //Response.Write(EyouSoft.Common.SerializationHelper.InvertJSON<Person>(@"{""Age"":20,""BirthDay"":""\/Date(644135865375+0800)\/"",""ID"":1,""Name"":""张三""} "));
            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Reset();
            stop.Start();

            //IList<EyouSoft.Model.TourStructure.TourInfo> tlist = RabbitMQ.Client.Examples.SingleGet.Main(new string[] { "192.168.1.254:5672", "q1" });
            //Response.Write("团队数：" + tlist.Count);
            //foreach (EyouSoft.Model.TourStructure.TourInfo m in tlist)
            //{
            //    Response.Write("团队创建时间：" + m.CreateTime.ToString() + "<BR>");
            //    EyouSoft.BLL.TourStructure.Tour.CreateInstance().UpdateTemplateTourInfo(m);
            //}

            //EyouSoft.Model.SystemStructure.SysCompanyDomain model = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, "");
            //if (model != null)
            //    Response.Write(model.Domain);
            //EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(362, EyouSoft.Model.AdvStructure.AdvPosition.首页广告精品推荐图文);
            //Response.Write(string.CompareOrdinal("ASDF", "asdf"));
            //string ltr = "<font color=red>adfsdfdf</font>";
            //string[] ddd = System.Text.RegularExpressions.Regex.Split("<font color=red>adfsdfdf</font>", "<(.|\\n)*?>"
            //    );
            //if (ddd.Length == 5)
            //{
            //    Response.Write(ltr.Replace(ddd[2],ddd[2].Substring(1,3)));
            //}

            //Response.Write(EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel("bangbu.tongye114.com").CityId);

            //List<int> asdf = new List<int>();
            //EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().AddSysArea(new EyouSoft.Model.SystemStructure.SysArea() {
            //    AreaName = "Asdfsd",
            //    RouteType = EyouSoft.Model.SystemStructure.AreaType.国内短线
            //});
            //EyouSoft.BLL.CompanyStructure.CompanyDepartment.CreateInstance().Exists("8ec1b5ab-9cd7-45b8-92ab-b90f8ad85f28", "dasf","");
           // EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel("ec50ba84-4b07-4ecc-abac-7c192068d11d");
           // EyouSoft.Model.SystemStructure.SysCompanyDomain domain = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, "www.test.com:30000");
           //Response.Write(domain.CompanyId);
            //EyouSoft.BLL.SystemStructure.SysPermissionCategory.CreateInstance().GetList(new int[2] { 1, 2 });
            //EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();

            //IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvImagesList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(362, EyouSoft.Model.AdvStructure.AdvPosition.首页广告旅游动态图文);

            //EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance().UpdateUserPassWord("5345", "000000","000000q");

            //EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetCompanyState("0001a989-c9f7-4235-a426-ba23fa77b4c1");


            //EyouSoft.Model.AdvStructure.AdvInfo adv = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().DeleteAdv(152);
            //adv.Title = "111111111111111111";
            //adv.ImgPath = "dddddddddddddd";
            //EyouSoft.BLL.AdvStructure.Adv.CreateInstance().UpdateAdv(adv);
            //EyouSoft.BLL.AdvStructure.Adv.CreateInstance().DeleteAdv(149);
            //EyouSoft.Model.CommunityStructure.InfoArticle modeli = new EyouSoft.Model.CommunityStructure.InfoArticle();
            //modeli.AreaId = EyouSoft.Model.CommunityStructure.TopicAreas.成功故事;
            //modeli.ArticleTag = "sdgsdg,sgsdge";
            //modeli.ArticleText = "1111111111";
            //modeli.ArticleTitle = "2222222222";
            //modeli.Editor = "3";
            //modeli.ID = "28C94009-70B7-4D39-9C97-33EB6CB1D889";
            //modeli.ImgPath = "388888888777";
            //modeli.ImgThumb = "36666666699";
            //modeli.Source = "lllllllllllll";
            //modeli.TitleColor = "#FFFFF";
            //EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().Delete(modeli.ID);


            //EyouSoft.Model.SystemStructure.Affiche modela = new EyouSoft.Model.SystemStructure.Affiche();
            //modela.AfficheClass = EyouSoft.Model.SystemStructure.AfficheType.成功出票;
            //modela.AfficheInfo = "asdadsfsdfsd";
            //modela.AfficheTitle = "爱上对方爱上对方按时打发短发是打发打";
            //modela.PicPath = "5555558888";
            //modela.ID = 13;
            //EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().Delete(13);

            //EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.System.SysSiteUpdateKey, DateTime.Now);

            //IList<EyouSoft.Model.SystemStructure.CityBase> list7 = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanyPortCity("0001a989-c9f7-4235-a426-ba23fa77b4c1");
            //Response.Write(list7.Count);
            stop.Stop();
            Response.Write("<BR>执行时间:" + stop.ElapsedMilliseconds);
            Response.Write("<script>setTimeout('location.reload()', 5000);</script>");
            Response.End();

            //EyouSoft.Cache.Facade.CacheObject<EyouSoft.Model.CompanyStructure.Company> model5 = (EyouSoft.Cache.Facade.CacheObject<EyouSoft.Model.CompanyStructure.Company>)EyouSoft.BLL.TestBLL.CreateInstance().GetCache("asdfa11");
          


            EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().DeleteSysFriendLink(38);
            return;
            EyouSoft.Model.CompanyStructure.ProductInfo model = new EyouSoft.Model.CompanyStructure.ProductInfo();
            model.ProductRemark = "14B17664-9CB5-483C-8A44-6A7F0F55C12E";
            model.ImagePath = "99887y66";
            model.ImageLink = "";
            model.ID = 2;
            IList<EyouSoft.Model.CompanyStructure.ProductInfo> list5 = new List<EyouSoft.Model.CompanyStructure.ProductInfo>();
            list5.Add(model);
            //EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().SetProduct("ec50ba84-4b07-4ecc-abac-7c192068d11d","","",list5);
            return;
            EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance().InStepFriends(10481);
            if (EyouSoft.BLL.TestBLL.CreateInstance().GetCache("adasdf") != null)
                Response.Write("<BR>CACHE:" + EyouSoft.BLL.TestBLL.CreateInstance().GetCache("adasdf").ToString());

            EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.文字);
            EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Remove(new string[1] { "asdf" });
            EyouSoft.Model.SystemStructure.SystemInfo SystemModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemInfoModel();
            EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance().GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.文字);
            //EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Remove(new string[2] { "adsfsdf", "3333" });
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> list  = EyouSoft.BLL.SystemStructure.SysPermissionCategory.CreateInstance().GetList(new int[1]{1});
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> list1 = EyouSoft.BLL.SystemStructure.SysPermissionCategory.CreateInstance().GetList(EyouSoft.Model.SystemStructure.PermissionType.大平台);
            string[] df = {};
            EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().GetDomainList(df);
            //EyouSoft.SSOComponent.Entity.SSOResponse response = new EyouSoft.SSOComponent.Remote.UserLogin().UserLoginPassword("enowinfo", "ba44e622a04a76beade0da36a3760d76", "",EyouSoft.SSOComponent.Entity.PasswordType.MD5);
            //bool a = response.IsValid;

            //EyouSoft.SSOComponent.Entity.UserInfo User = new EyouSoft.SSOComponent.Entity.UserInfo();
            //User.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.车队);
            //new EyouSoft.SSOComponent.UserLogin().UserLoginAct("enowinfo", "ba44e622a04a76beade0da36a3760d76", "", EyouSoft.SSOComponent.Entity.PasswordType.MD5);

            //EyouSoft.Model.TourStructure.TourInfo model = new EyouSoft.Model.TourStructure.TourInfo();
            //Response.Write("<BR>Master:" + EyouSoft.BLL.TourStructure.Tour.CreateInstance().InsertTemplateTourInfo(model));
            //Response.Write("<BR>Master:" + EyouSoft.BLL.TourStructure.Tour.CreateInstance().DeleteByVirtual("dfg"));
            EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(5, 0, null, null, null);
            EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel("1e5edc20-4b83-4c16-8c52-008e0c09bd1e");
            EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemInfoModel();
            EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(0);

            EyouSoft.Model.SystemStructure.SystemInfo info = new EyouSoft.Model.SystemStructure.SystemInfo();
            info.ID = 1;
            info.SystemRemark = "assdfdf";
            info.SystemName = "测试系统";
            EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().UpdateSystemInfo(info);

            EyouSoft.Model.CompanyStructure.CompanyAreaSetting area = new EyouSoft.Model.CompanyStructure.CompanyAreaSetting();
            area.AreaID = 1;
            area.CompanyID = "1e5edc20-4b83-4c16-8c52-008e0c09bd1e";
            area.PrefixText = "YL";
            IList<EyouSoft.Model.CompanyStructure.CompanyAreaSetting> areal = new List<EyouSoft.Model.CompanyStructure.CompanyAreaSetting>();
            areal.Add(area);
            EyouSoft.BLL.CompanyStructure.CompanyAreaSetting.CreateInstance().Update(areal);


            //Response.Write("<BR>Master:" + EyouSoft.BLL.TestBLL.CreateInstance().TestConn(1));
           // //Response.Write("<BR>Slave:" + EyouSoft.BLL.TestBLL.CreateInstance().TestData());
            if(EyouSoft.BLL.TestBLL.CreateInstance().GetCache("adasdf")!=null)
                Response.Write("<BR>CACHE:" + EyouSoft.BLL.TestBLL.CreateInstance().GetCache("adasdf").ToString());
           // //Response.Write("<BR>2Master:" + EyouSoft.BLL.Test.TestBLL2.CreateInstance().TestConn());
           // //Response.Write("<BR>2Slave:" + EyouSoft.BLL.Test.TestBLL2.CreateInstance().TestData());
            stop.Stop();
            Response.Write("<BR>执行时间:" + stop.ElapsedMilliseconds);

            System.IO.StreamReader ad = new System.IO.StreamReader(@"C:\companylog.txt");
            while (ad.Peek()>0)
            {
                string a = ad.ReadLine();
                Response.Write("pppp:" + a + "<BR>");
            }
            ad.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string domain = "";
            for (int i = 1; i < Request.Url.Host.Split('.').Length; i++)
            {
                domain += Request.Url.Host.Split('.')[i] + ".";
            }
            if (domain.EndsWith("."))
                domain = domain.Substring(0, domain.Length - 1);
            HttpCookie cookie=Request.Cookies["ssotest"];  
            cookie.Expires=DateTime.Today.AddDays(-10);  
            cookie.Domain = domain;
            Response.AppendCookie(cookie);
            Response.Redirect(".");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session.Add("UID", Request["txtUsername"]);
            Response.Write("<html><body><form id=\"frmLogin\" action=\"http://login.ty.com/sso/login.aspx?\">");
            Response.Write("<input type=hidden name=RedirectUrl value=\"" + Server.UrlEncode(Request.Url.ToString()) + "\" />");
            Response.Write("<input id=\"txtUsername\" name=\"txtUsername\" type=hidden value=\"" + Request["txtUsername"] + "\"/></form>");
            Response.Write("<script>debugger;document.forms[0].submit();</script></body></html>");            
            Response.End();
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Write("<html><body><form id=\"frmLogin\" action=\"http://login.ty.com/sso/logout.aspx?\">");
            Response.Write("<input type=hidden name=RedirectUrl value=\"" + Server.UrlEncode(Request.Url.ToString()) + "\" />");
            Response.Write("<input id=\"txtUsername\" name=\"txtUsername\" type=hidden value=\"" + Session["UID"] + "\"/></form>");
            Response.Write("<script>document.forms[0].submit();</script></body></html>");
            Session.Remove("UID");
            Response.End();
            Server.Transfer("", true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["CacheItem"] = System.Convert.ToInt32(txtUsername.Value);
        }

        #region 成员方法
        /// <summary>
        /// 汉字查完整拼音
        /// </summary>
        public static string Convert(string ChineseChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char str in ChineseChar.ToCharArray())
            {
                sb.Append(GetSingleSpell(str));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 汉字查拼音首字母
        /// </summary>
        public static string ConvertFirst(string ChineseChar)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char str in ChineseChar.ToCharArray())
            {
                sb.Append(GetSingleSpellFirst(str));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 单个汉字查拼音
        /// </summary>
        private static string GetSingleSpell(char ChineseChar)
        {
            StreamReader TextReader = new StreamReader(@"C:\winpy.txt", true);
            string text = TextReader.ReadLine();
            while (text != null && text.ToCharArray()[0] != ChineseChar)
            {
                TextReader.Peek();
                text = TextReader.ReadLine();
            }
            TextReader.Close();
            if (text != null)
            {
                string[] tmpArr = text.Replace(ChineseChar.ToString(), "").Split(' ');
                text = tmpArr[tmpArr.Length - 1];
            }
            else
            {
                text = "";
            }
            return text;
        }

        /// <summary>
        /// 单个汉字查拼音首字母
        /// </summary>
        protected static string GetSingleSpellFirst(char ChineseChar)
        {
            StreamReader TextReader = new StreamReader(@"C:\winpy.txt", true);
            string text = TextReader.ReadLine();
            while (text != null && text.ToCharArray()[0] != ChineseChar)
            {
                TextReader.Peek();
                text = TextReader.ReadLine();
            }
            TextReader.Close();
            if (text != null)
            {
                string[] tmpArr = text.Replace(ChineseChar.ToString(), "").Split(' ');
                text = tmpArr[tmpArr.Length - 1].Substring(0, 1);
            }
            else
            {
                text = "";
            }
            return text;
        }
        #endregion

        
    }
}
