using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 高级网店模板4首页
    /// </summary>
    /// Author:汪奇志 2010-11-11
    public partial class Default : FrontPage
    {
        #region Attributes
        /// <summary>
        /// 轮换广告JS
        /// </summary>
        protected string RollJs = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.CTAB = SeniorOnlineShop.master.T4TAB.首页;

            this.InitRollScripts();
            this.InitPromotionTours();
            this.InitAreas();

            this.InitCYZH(this.ltrFTRQ, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情, 3);
            this.InitCYZH(this.ltrWXTX, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒, 3);
            this.InitCYZH(this.ltrZYTJ, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.旅游资源推荐, 3);
            this.InitCYZH(this.ltrZHJS, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍, 7);
            this.InitOlServer();
            //设置Title.....
            IList<EyouSoft.Model.SystemStructure.AreaBase> arealist = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.Master.CompanyId);
            string strarea = string.Empty;
            this.Title = string.Format(EyouSoft.Common.PageTitle.SeniorShop_Title, string.Empty);
            foreach (EyouSoft.Model.SystemStructure.AreaBase area in arealist)
            {
                strarea = strarea + "、" + area.AreaName;
            }
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.SeniorShop_Des, lbCompanyName.Text, CityModel.ProvinceName + CityModel.CityName, strarea.TrimStart(new char[] { '、' })));            
        }

        #region private members
        /// <summary>
        /// 初始化轮换广告JS
        /// </summary>
        private void InitRollScripts()
        {
            string s = ";imag[{0}]=\"{1}\";link[{2}]=\"{3}\";text[{4}]=\"{5}\";";
            StringBuilder tmp = new StringBuilder();
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> advs = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5, this.Master.CompanyId);

            if (advs != null && advs.Count > 0)
            {
                int i = 1;                
                foreach (EyouSoft.Model.ShopStructure.HighShopAdv adv in advs)
                {
                    tmp.AppendFormat(s, i, Utils.GetLineShopImgPath(adv.ImagePath, 3), i, adv.LinkAddress != string.Empty ? adv.LinkAddress : "#", i, "图片" + i);
                    i++;
                }
            }

            this.RollJs = tmp.ToString();
        }

        /// <summary>
        /// 初始化特价旅游线路
        /// </summary>
        private void InitPromotionTours()
        {            
            IList<EyouSoft.Model.NewTourStructure.MRoute> RoutelistTJ = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetRecommendList(6, this.Master.CompanyId, EyouSoft.Model.NewTourStructure.RecommendType.特价, null);
            if (RoutelistTJ != null && RoutelistTJ.Count > 0)
            {
                this.rptPromotionTours.DataSource = RoutelistTJ;
                this.rptPromotionTours.DataBind();
            }
            RoutelistTJ = null;
        }

        protected string GetRouteDetailUrl(string routeID)
        {
            string url = string.Empty;
            url = "" + EyouSoft.Common.Domain.UserPublicCenter + "/TourManage/TourDetail.aspx?RouteID=" + routeID;
            return url;
        }

        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitAreas()
        {
            var areas=EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.Master.CompanyId);           
            if (areas != null && areas.Count > 0)
            {
                this.rptAreas.DataSource=this.rtpAreas1.DataSource = areas;
                this.rptAreas.DataBind();
                this.rtpAreas1.DataBind();
            }
            areas = null;
        }

        /// <summary>
        /// 初始化出游指南
        /// </summary>
        /// <param name="ltr">Literal</param>
        /// <param name="type">出游指南类型</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        private void InitCYZH(Literal ltr, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type, int expression)
        {
            var items = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(3, this.Master.CompanyId, (int)type, null);
            if (items == null || items.Count < 1) return;
            StringBuilder html = new StringBuilder();

            #region 综合介绍
            if (type == EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍)
            {
                foreach (var item in items)
                {
                    html.AppendFormat(@"<tr><td height=""19""><a href=""{0}"">·{1}</a></td></tr>", Utils.GenerateShopPageUrl2(string.Format("/GuideBookInfo_{0}", item.ID), this.Master.CompanyId)
                        , Utils.GetText(item.Title, 14, true));
                }
            }
            #endregion

            #region 风土人情 温馨提醒 旅游资源推荐
            if (type != EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍)
            {
                string s = @"<tr>
                                    <td height=""90"">
                                        <table width=""202"" border=""0"" align=""right"" cellpadding=""0"" cellspacing=""0"" class=""maintop5"" style=""margin-bottom: 5px;"">
                                            <tr>
                                                <td width=""45%"" rowspan=""2"">
                                                    <a href=""{0}"">
                                                        <img src=""{1}"" width=""79"" height=""67"" border=""0"" class=""borderhui"" /></a>
                                                </td>
                                                <td width=""55%"" height=""24"" style=""padding-left: 3px;"">
                                                    <a href=""{2}"" class=""zhinanbt"">{3}</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign=""top"" style=""padding-left: 3px; line-height: 16px;"">
                                                    {4}
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>";
                html.AppendFormat(s, Utils.GenerateShopPageUrl2(string.Format("/GuideBookInfo_{0}", items[0].ID), this.Master.CompanyId)
                    , Utils.GetLineShopImgPath(items[0].ImagePath, 4)
                    , Utils.GenerateShopPageUrl2(string.Format("/GuideBookInfo_{0}", items[0].ID), this.Master.CompanyId)
                    , Utils.GetText(items[0].Title, 8)
                    , Utils.GetText(items[0].ContentText, 26, true));

                html.Append(@"<tr><td><table width=""205"" border=""0"" align=""right"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#F4F4F4"">");

                for (int i = 1; i < items.Count; i++)
                {
                    html.AppendFormat(@"<tr><td height=""20""><a href=""{0}"">·{1}</a></td></tr>", Utils.GenerateShopPageUrl2(string.Format("/GuideBookInfo_{0}", items[0].ID), this.Master.CompanyId)
                                                      , Utils.GetText(items[i].Title, 14, true));
                }

                html.Append("</table></td></tr>");
            }
            #endregion

            ltr.Text = html.ToString();
        }
        /// <summary>
        /// 初始化在线服务
        /// </summary>
        public void InitOlServer()
        {
            SeniorOnlineShop.master.T4 cMaster = (SeniorOnlineShop.master.T4)this.Master;
            if (cMaster != null)
            {
                lbCompanyName.Text = cMaster.CompanyInfo.CompanyName;
            }
            cMaster = null;

            string Remote_IP = StringValidate.GetRemoteIP();
            EyouSoft.Model.SystemStructure.CityBase cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetClientCityByIp(Remote_IP);
            if (cityModel != null)
            {
                EyouSoft.Model.SystemStructure.SysCity thisCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityModel.CityId);
                if (thisCityModel != null)
                {
                    lbGuestInfo.Text = string.Format("欢迎您来自{0}省{1}市的朋友有什么可以帮助您的吗？", thisCityModel.ProvinceName, thisCityModel.CityName);
                }
                thisCityModel = null;
            }
            cityModel = null;
        }
        #endregion
    }
}
