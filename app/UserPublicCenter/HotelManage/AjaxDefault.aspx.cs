using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.HotelManage
{
    public partial class AjaxDefault : Page
    {
        private EyouSoft.IBLL.HotelStructure.IHotelLocalInfo iBll = null;
        protected string CityId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cId = Utils.GetQueryStringValue("CityId");
                string cityCode = Utils.GetQueryStringValue("cityCode");
                if (cityCode != "" && cId != "")
                {
                    CityId = cId;
                    RecomHotelListByCityId(cityCode);
                }
            }
        }

        #region 推荐酒店列表
        /// <summary>
        /// 获得推荐酒店列表
        /// </summary>
        /// <param name="cityId"></param>
        protected void RecomHotelListByCityId(string cityCode)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> list = GetList(EyouSoft.Model.HotelStructure.HotelShowType.特推酒店, cityCode, 18);
            if (list != null && list.Count > 0)
            {
                this.lclListLeft.Text = "";
                this.lclListRight.Text = "";
                int rowTypeL = 0;
                int rowTypeR = 0;

                if (list.Count > 0)
                {
                    if (list[0].IsTop)
                    {

                        this.lclLeft.Text = "<div class=\"imgLAreapic\"><a  target=\"_blank\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[0].HotelCode, Utils.GetInt(CityId)) + "\"> <img width=\"108px\" height=\"77px\" src=\"" + EyouSoft.HotelBI.Utils.ImagesUrl + list[0].HotelImg + "\" /></a></div><div class=\"imgRArea\" ><a target=\"_blank\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[0].HotelCode, Utils.GetInt(CityId)) + "\">" + Utils.GetText(list[0].HotelName, 18, true) + "</a><br>促销价：<font class=\"frb\">￥" + list[0].MarketingPrice.ToString("0") + "</font><br>简介;" + Utils.GetText(list[0].ShortDesc, 27, true) + "</div><div class=\"clear\"></div>";
                    }
                    else
                    {
                        this.lclLeft.Text = "";
                    }
                }
                if(list.Count>1)
                {
                    if (list[1].IsTop)
                    {
                        this.lclRight.Text = "<div class=\"imgLAreapic\"><a target=\"_blank\"  href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[1].HotelCode, Utils.GetInt(CityId)) + "\"> <img width=\"108px\" height=\"77px\" src=\"" + EyouSoft.HotelBI.Utils.ImagesUrl + list[1].HotelImg + "\" /></a></div><div class=\"imgRArea\"><a target=\"_blank\"  href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[1].HotelCode, Utils.GetInt(CityId)) + "\">" + Utils.GetText(list[1].HotelName, 18, true) + "</a><br>促销价：<font class=\"frb\">￥" + list[1].MarketingPrice.ToString("0") + "</font><br>简介;" + Utils.GetText(list[1].ShortDesc, 27, true) + "</div><div class=\"clear\"></div>";
                    }
                    else
                    {
                        this.lclRight.Text = "";
                    }
                }


                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (i == 0 && list[i].IsTop)
                        {
                            continue;
                        }
                        if (rowTypeL == 0)
                        {
                            this.lclListLeft.Text += "<div style=\"width: 320px; height: 18px;border-bottom:1px #ffdc9e dashed;padding:6px 0 0 15px;\"><span style=\"width: 150px; float:left;\"><a target=\"_blank\"  title=\"" + list[i].HotelName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[i].HotelCode, Utils.GetInt(CityId)) + "\">·" + Utils.GetText(list[i].HotelName, 10, true) + "</a></span> <span style=\"width: 80px;float:left;\">" + GetImageByLeave((int)list[i].Rank) + "</span> <span style=\"width:65px;float:left;\"><font class=\"frb\">￥" + list[i].MarketingPrice.ToString("0") + "起</font></span></div>";
                            rowTypeL = 1;
                        }
                        else
                        {
                            this.lclListLeft.Text += "<div style=\"width: 320px; height: 18px;border-bottom:1px #ffdc9e dashed;padding:6px 0 0 15px;background-color:#fffaee;\"><span style=\"width: 150px;float:left;\"><a  target=\"_blank\"  title=\"" + list[i].HotelName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[i].HotelCode, Utils.GetInt(CityId)) + "\">·" + Utils.GetText(list[i].HotelName, 10, true) + "</a></span> <span style=\"width: 80px;float:left;\">" + GetImageByLeave((int)list[i].Rank) + "</span> <span style=\"width:65px;float:left;\"><font class=\"frb\">￥" + list[i].MarketingPrice.ToString("0") + "起</font></span></div>";
                            rowTypeL = 0;
                        }
                    }

                    if (i % 2 != 0)
                    {
                        if (i == 1 && list[i].IsTop)
                        {
                            continue;
                        }

                        if (rowTypeR == 0)
                        {
                            this.lclListRight.Text += "<div style=\"width: 320px; height: 18px;border-bottom:1px #ffdc9e dashed;padding:6px 0 0 15px;\"><span style=\"width: 150px;float:left;\"><a  target=\"_blank\" title=\"" + list[i].HotelName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[i].HotelCode, Utils.GetInt(CityId)) + "\">·" + Utils.GetText(list[i].HotelName, 10, true) + "</a></span> <span style=\"width: 80px;float:left;\">" + GetImageByLeave((int)list[i].Rank) + "</span> <span style=\"width:65px;float:left;\"><font class=\"frb\">￥" + list[i].MarketingPrice.ToString("0") + "起</font></span></div>";
                            rowTypeR = 1;
                        }
                        else
                        {
                            this.lclListRight.Text += "<div style=\"width: 320px; height: 18px;border-bottom:1px #ffdc9e dashed;padding:6px 0 0 15px;background-color:#fffaee;\"><span style=\"width: 150px;float:left;\"><a  target=\"_blank\" title=\"" + list[i].HotelName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(list[i].HotelCode, Utils.GetInt(CityId)) + "\">·" + Utils.GetText(list[i].HotelName, 10, true) + "</a></span> <span style=\"width: 80px;float:left;\">" + GetImageByLeave((int)list[i].Rank) + "</span> <span style=\"width:65px;float:left;\"><font class=\"frb\">￥" + list[i].MarketingPrice.ToString("0") + "起</font></span></div>";
                            rowTypeR = 0;
                        }
                    }

                }
            }
        }
        #endregion

        #region 根据酒店等级显示图片
        /// <summary>
        /// 获得酒店星级图片
        /// </summary>
        /// <param name="leave"></param>
        /// <returns></returns>
        protected string GetImageByLeave(int leave)
        {
            StringBuilder str = new StringBuilder();
            if (leave > 5) { leave = leave - 5; }
            for (int i = 0; i < leave; i++)
            {
                str.Append("<img src=\"" + ImageManage.GetImagerServerUrl(1) + "/images/hotel/start_02.gif\" width=\"12px\" height=\"13px\" align='absmiddle' />");
            }
            return str.ToString();
        }
        #endregion

        #region 共用GetList 方法
        /// <summary>
        /// 获得LIST方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="cityCode">城市编号</param>
        /// <param name="topNum">条数</param>
        /// <returns></returns>
        protected IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> GetList(EyouSoft.Model.HotelStructure.HotelShowType type, string cityCode, int topNum)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> list = null;
            iBll = new EyouSoft.BLL.HotelStructure.HotelLocalInfo();
            list = iBll.GetList(type, cityCode, topNum);
            if (list != null && list.Count > 0)
            {
                return list;
            }
            return null;
        }
        #endregion
    }
}
