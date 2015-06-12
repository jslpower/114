using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店列表页面
    /// 功能：显示搜索出来的酒店的列表
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class HotelSearchList : EyouSoft.Common.Control.FrontPage
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置title
                this.Page.Title = EyouSoft.Common.PageTitle.Hotel_Title;
                //设置关键字
                AddMetaTag("description", EyouSoft.Common.PageTitle.Hotel_Des);
                AddMetaTag("keywords", EyouSoft.Common.PageTitle.Hotel_Keywords);
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 4;
                
                #region 获得参数
                //城市三字码
                string cityCode = Utils.GetQueryStringValue("cityCode");
                //入住日期
                string inTime = Utils.GetQueryStringValue("inTime");
                //离开日期
                string leaveTime = Utils.GetQueryStringValue("leaveTime");
                //开始价格
                decimal? priceBegin = Utils.GetDecimal(Utils.GetQueryStringValue("priceBegin"));
                //结束价格
                decimal? priceEnd = Utils.GetDecimal(Utils.GetQueryStringValue("priceEnd"));
                //地理位置
                string landMark = Utils.GetQueryStringValue("landMark");
                //酒店星级
                string hotelLevel = Utils.GetQueryStringValue("hotelLevel");
                if (hotelLevel == null || hotelLevel == "") { hotelLevel = "0"; }
                //即时确认
                string instant = Utils.GetQueryStringValue("instant");
                //接机服务
                string service = Utils.GetQueryStringValue("service");
                //上网设施
                string internet = Utils.GetQueryStringValue("internet");
                //装修时间
                string decoration = Utils.GetQueryStringValue("decoration");
                //特殊房型
                string special = Utils.GetQueryStringValue("special");
                //床型
                string bed = Utils.GetQueryStringValue("bed");
                //行政区域
                string district = Utils.GetQueryStringValue("district");
                //查询方式
                string searchWay = Utils.GetQueryStringValue("searchWay");
                //酒店名称
                string hotelName = Utils.GetQueryStringValue("hotelName");
                //排序
                string sort = Utils.GetQueryStringValue("sort");
                #endregion

                #region 设置查询控件值
                this.HotelSearchControl1.CityCode = cityCode;
                this.HotelSearchControl1.InTime = inTime;
                this.HotelSearchControl1.LeaveTime = leaveTime;
                this.HotelSearchControl1.PriceBegin = Convert.ToDecimal(priceBegin);
                this.HotelSearchControl1.PriceEnd = Convert.ToDecimal(priceEnd);
                this.HotelSearchControl1.HotelLevel = hotelLevel;
                this.HotelSearchControl1.HotelName = hotelName;
                this.HotelSearchControl1.Instant = Utils.GetInt(instant, 0);
                this.HotelSearchControl1.Service = Utils.GetInt(service, 0);
                this.HotelSearchControl1.Internet = Utils.GetInt(internet, 0);
                this.HotelSearchControl1.Decoration = decoration;
                this.HotelSearchControl1.Special = special;
                this.HotelSearchControl1.Bed = bed;
                this.HotelSearchControl1.LandMark = landMark;
                #endregion

                #region 页面控件赋值
                if (inTime != "" && Utils.GetDateTimeNullable(inTime) != null)
                {
                    this.lblInTime.Text = inTime;
                }
                if (leaveTime != "" && Utils.GetDateTimeNullable(leaveTime) != null)
                {
                    this.lblLeaveTime.Text = leaveTime;
                }
                if (Utils.GetDateTimeNullable(inTime) != null && Utils.GetDateTimeNullable(leaveTime) != null)
                {
                    TimeSpan sp = Utils.GetDateTime(leaveTime) - Utils.GetDateTime(inTime);
                    this.lblDayCount.Text = sp.Days.ToString();

                }

                //显示城市名
                EyouSoft.Model.HotelStructure.HotelCity cityModel = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetModel(cityCode);
                if (cityModel != null)
                {
                    this.litCity.Text = cityModel.CityName;
                }
                if (priceBegin <= 0 && priceEnd <= 0)
                {
                    this.litMoney.Text ="";
                }
                else if (priceBegin <= 0 && priceEnd > 0)
                {
                    this.litMoney.Text = priceEnd + "元以下";
                }
                else if (priceBegin > 0 && priceEnd > 0)
                {
                    this.litMoney.Text = priceBegin + " - " + priceEnd + "元";
                }
                #endregion

                #region 设置用户控件城市ID
                //查询控件
                this.HotelSearchControl1.CityId = this.CityId;
                //常识控件
                this.CommonUserControl1.CityId = this.CityId;
                //促销酒店
                this.SpecialHotelControl1.CityId = this.CityId;
                //最新加入酒店
                this.HotHotelControl1.CityId = this.CityId;
                #endregion

                //排序设置图片
                SetImgBySort(sort);
                //设置用户控件图片URL
                this.HotelSearchControl1.ImageServerPath = this.ImageServerPath;
                //设置图片宽度
                this.ImgFristControl1.ImageWidth = "225px";
                this.ImgSecondControl1.ImageWidth = "225px";

                this.hideUrl.Value = Request.Url.Query;
            }
        }



        #region 生成排序的链接
        /// <summary>
        /// 根据排序生成链接并显示相印的图片
        /// </summary>
        /// <param name="sort"></param>
        protected void SetImgBySort(string sort)
        {
            string priceUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.Url.LocalPath;
            Dictionary<string, string> dicList = GetUrlParaDic(Request.Url.ToString());
            if (dicList != null && dicList.ContainsKey("sort"))
            {
                dicList.Remove("sort");
            }

            if (dicList != null && dicList.Count > 0)
            {
                priceUrl += "?";
                foreach (var val in dicList)
                {
                    priceUrl += val.Key + "=" + val.Value + "&";
                }
            }

            switch (sort)
            {
                case "": this.lclPriceSort.Text = "<a style='' href=\"" + priceUrl + "sort=1\"><font class=\"C_Grb\">价格</font></a>";
                    this.lclStarSort.Text = "<a style='' href=\"" + priceUrl + "sort=3\"><font class=\"C_Grb\">酒店星级</font></a>";
                    break;
                case "1":
                    this.lclPriceSort.Text = "<a style='padding-top:3px;' href=\"" + priceUrl + "sort=2\"><font class=\"C_Grb\">价格</font><img src=\"" + ImageServerPath + "/images/hotel/icon0402.gif\" align=\"absmiddle\" height='10px' /></a>";
                    this.lclStarSort.Text = "<a style='' href=\"" + priceUrl + "sort=3\"><font class=\"C_Grb\">酒店星级</font></a>";
                    break;
                case "2":
                    this.lclPriceSort.Text = "<a style='padding-top:3px;' href=\"" + priceUrl + "sort=1\"><font class=\"C_Grb\">价格</font><img src=\"" + ImageServerPath + "/images/hotel/icon04.gif\" align=\"absmiddle\" height='10px' /></a>";
                    this.lclStarSort.Text = "<a style='' href=\"" + priceUrl + "sort=3\"><font class=\"C_Grb\">酒店星级</font></a>";
                    break;
                case "3":
                    this.lclPriceSort.Text = "<a style='' href=\"" + priceUrl + "sort=1\"><font class=\"C_Grb\">价格</font></a>";
                    this.lclStarSort.Text = "<a style='padding-top:3px;' href=\"" + priceUrl + "sort=4\"><font class=\"C_Grb\">酒店星级</font><img src=\"" + ImageServerPath + "/images/hotel/icon0402.gif\" align=\"absmiddle\" height='10px' /></a>";
                    break;
                default:
                    this.lclPriceSort.Text = "<a style='' href=\"" + priceUrl + "sort=1\"><font class=\"C_Grb\">价格</font></a>";
                    this.lclStarSort.Text = "<a style='padding-top:3px;' href=\"" + priceUrl + "sort=3\"><font class=\"C_Grb\">酒店星级</font><img src=\"" + ImageServerPath + "/images/hotel/icon04.gif\" align=\"absmiddle\" height='10px' /></a>";
                    break;
            }
        }
        #endregion

        #region 获得URL参数集合
        /// <summary>
        /// 根据URL获得参数的泛型集合
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetUrlParaDic(string url)
        {
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            if (url != null && url != "" && url.IndexOf("?") > 0)
            {
                string p = url.Substring(url.IndexOf("?") + 1, url.Length - url.IndexOf("?") - 1);
                string[] list = p.Split('&');
                if (list.Count() > 0)
                {
                    dicPara.Clear();
                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (list[i].Split('=').Count() == 2)
                        {
                            if (!dicPara.ContainsKey(list[i].Split('=')[0]))
                            {
                                dicPara.Add(list[i].Split('=')[0], list[i].Split('=')[1]);
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
                return dicPara;
            }
            else
            {
                return null;
            }
        }
        #endregion

    
    }
}
