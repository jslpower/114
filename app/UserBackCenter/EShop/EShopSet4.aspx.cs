using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店模板4管理首页
    /// </summary>
    /// Author:汪奇志 2010-11-10
    public partial class EShopSet4 : BasePage
    {
        #region Attributes
        /// <summary>
        /// 公司编号
        /// </summary>
        protected string CompanyId = string.Empty;
        /// <summary>
        /// 公司信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo = null;
         /// <summary>
        /// 高级网店信息
        /// </summary>
        private EyouSoft.Model.ShopStructure.HighShopCompanyInfo CompanyEShopInfo = null;

        /// <summary>
        /// 头部Bannner样式
        /// </summary>
        protected string STYHeadBanner { get; set; }
        /// <summary>
        /// 轮换广告JS
        /// </summary>
        protected string RollJs = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("EShopPage4.aspx", "登录");
                return;
            }

            this.InitCInfo();
            this.InitInfo();
            this.InitNews();
            this.InitLinks();
            this.InitAreas();
            this.InitPromotionTours();
            this.InitRollScripts();

            this.InitCYZH(this.ltrFTRQ, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.风土人情, 3);
            this.InitCYZH(this.ltrWXTX, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.温馨提醒, 3);
            this.InitCYZH(this.ltrZYTJ, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.旅游资源推荐, 3);
            this.InitCYZH(this.ltrZHJS, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍, 7);

            this.Page.ClientScript.RegisterClientScriptInclude(Guid.NewGuid().ToString(), "/DatePicker/WdatePicker.js");
        }        

        #region private memebers
        /// <summary>
        /// 初始化公司相关信息
        /// </summary>
        private void InitCInfo()
        {
            this.CompanyId = this.SiteUserInfo.CompanyID;
            this.CompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.CompanyId);
            this.CompanyEShopInfo = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(this.CompanyId);
        }

        /// <summary>
        /// 初始化网店头部 企业名片 登录块 版权
        /// </summary>
        private void InitInfo()
        {
            //Head
            if (this.CompanyInfo.AttachInfo.CompanyShopBanner.BannerType == EyouSoft.Model.CompanyStructure.ShopBannerType.Default)
            {
                this.phDefaultHead.Visible = true;
                this.imgHeadLogo.Src = Domain.FileSystem + this.CompanyInfo.AttachInfo.CompanyShopBanner.CompanyLogo;
                this.ltrHeadLogoName.Text = this.CompanyInfo.CompanyName;
                this.STYHeadBanner = "background:url('" + Domain.FileSystem + this.CompanyInfo.AttachInfo.CompanyShopBanner.BannerBackground + "');background-repeat:no-repeat;";
            }
            else
            {
                this.phPersonalizeHead.Visible = true;
                this.ltrPersonalizeHead.Text = Utils.GetEShopImgOrFalash(this.CompanyInfo.AttachInfo.CompanyShopBanner.ImagePath, "javascript:void(0)", 960, 219);
            }

            //企业名片
            this.ltrCompanyBrandName.Text = this.CompanyInfo.CompanyBrand;
            this.ltrContactName.Text = this.CompanyInfo.ContactInfo.ContactName + "&nbsp;" + Utils.GetBigImgMQ(this.CompanyInfo.ContactInfo.MQ);
            this.ltrContactMobile.Text = this.CompanyInfo.ContactInfo.Mobile;
            this.ltrContactTelephone.Text = this.CompanyInfo.ContactInfo.Tel;
            this.ltrContactFax.Text = this.CompanyInfo.ContactInfo.Fax;
            this.ltrContactAddress.Text = this.CompanyInfo.CompanyAddress;
            this.imgCompanyCard.Src = Utils.GetLineShopImgPath(this.CompanyInfo.AttachInfo.CompanyCard.ImagePath, 1);

            //版权
            this.ltrCopyRight.Text = this.CompanyEShopInfo.ShopCopyRight;
        }

        /// <summary>
        /// 初始化最新旅游动态
        /// </summary>
        private void InitNews()
        {
            IList<EyouSoft.Model.ShopStructure.HighShopNews> news = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetTopNumberList(6, this.CompanyId);

            if (news != null && news.Count > 0)
            {
                this.rptNews.DataSource = news;
                this.rptNews.DataBind();
            }

            news = null;
        }

        /// <summary>
        /// 初始化友情链接
        /// </summary>
        private void InitLinks()
        {
            var links = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(this.CompanyId);

            if (links != null && links.Count > 0)
            {
                this.rptLinks.DataSource = links;
                this.rptLinks.DataBind();
            }

            links = null;
        }

        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitAreas()
        {
            var areas = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.CompanyId);

            if (areas != null && areas.Count > 0)
            {
                this.rptAreas.DataSource = this.rtpAreas1.DataSource = areas;
                this.rptAreas.DataBind();
                this.rtpAreas1.DataBind();
            }

            areas = null;
        }

        /// <summary>
        /// 初始化特价旅游线路
        /// </summary>
        private void InitPromotionTours()
        {
            IList<EyouSoft.Model.NewTourStructure.MRoute> RoutelistTJ = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetRecommendList(6, this.CompanyId, EyouSoft.Model.NewTourStructure.RecommendType.特价, null);
            if (RoutelistTJ != null && RoutelistTJ.Count > 0)
            {
                this.rptPromotionTours.DataSource = RoutelistTJ;
                this.rptPromotionTours.DataBind();
            }
            RoutelistTJ = null;
        }

        /// <summary>
        /// 初始化轮换广告JS
        /// </summary>
        private void InitRollScripts()
        {
            string s = ";imag[{0}]=\"{1}\";link[{2}]=\"{3}\";text[{4}]=\"{5}\";";
            StringBuilder tmp = new StringBuilder();
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> advs = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5, this.CompanyId);

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
        /// 初始化出游指南
        /// </summary>
        /// <param name="ltr">Literal</param>
        /// <param name="type">出游指南类型</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        private void InitCYZH(Literal ltr, EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType type, int expression)
        {
            var items = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(3, this.CompanyId, (int)type, null);
            if (items == null || items.Count < 1) return;
            StringBuilder html = new StringBuilder();

            #region 综合介绍
            if (type == EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.综合介绍)
            {
                foreach (var item in items)
                {
                    html.AppendFormat(@"<tr><td height=""19""><a href=""{0}"">·{1}</a></td></tr>", "javascript:void(0)"
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
                html.AppendFormat(s, "javascript:void(0)"
                    , Utils.GetLineShopImgPath(items[0].ImagePath, 4)
                    , "javascript:void(0)"
                    , Utils.GetText(items[0].Title, 8)
                    , Utils.GetText(items[0].ContentText, 26, true));

                html.Append(@"<tr><td><table width=""205"" border=""0"" align=""right"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#F4F4F4"">");

                for (int i = 1; i < items.Count; i++)
                {
                    html.AppendFormat(@"<tr><td height=""20""><a href=""{0}"">·{1}</a></td></tr>", "javascript:void(0)"
                                                      , Utils.GetText(items[i].Title, 14, true));
                }

                html.Append("</table></td></tr>");
            }
            #endregion

            ltr.Text = html.ToString();
        }
        #endregion
    }
}
