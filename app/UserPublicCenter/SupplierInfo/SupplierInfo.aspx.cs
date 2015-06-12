using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text.RegularExpressions;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// author dj
    /// date 2011-5-14
    /// 供求首页
    /// </summary>
    public partial class SupplierNewInfo : EyouSoft.Common.Control.FrontPage
    {
        protected IList<EyouSoft.Model.SystemStructure.SysProvince> plist = null;//省份列表
        protected IList<EyouSoft.Model.CommunityStructure.ExchangeList> exList = null;//供求信息钱表
        protected IList<EyouSoft.Model.CommunityStructure.MSupplyDemandRule> msrlist = null;//供求规则列表
        protected IList<EyouSoft.Model.CommunityStructure.MSupplyDemandPic> piclist = null;//供求焦点图
        protected void Page_Load(object sender, EventArgs e)
        {
            //供求频道移除了分站的要求，修改了RewriteUrl，所以将老的ReWriter URL请求进行301响应处理。
            //this can tells a search engine to update their cache and reassign the old url to the new url.
            Regex re = new Regex(@"^/info_(\d+)$", RegexOptions.IgnoreCase);
            if (re.IsMatch(Request.RawUrl))
            {
                Utils.RedirectPermanent(EyouSoft.Common.URLREWRITE.SupplierInfo.InfoDefaultUrlWrite(0), true);
            }

            #region 动态添加Title以及keywords，description
            Page.Title = EyouSoft.Common.PageTitle.Supplier_Title;
            AddMetaTag("description", EyouSoft.Common.PageTitle.Supplier_Des);
            AddMetaTag("keywords", EyouSoft.Common.PageTitle.Supplier_Keywords);
            #endregion
            if (!IsPostBack)
            {
                Bind();
            }
        }

        /// <summary>
        /// 初使化绑定
        /// </summary>
        private void Bind()
        {
            int totalc = 0;
            int pageSize = 40;
            int pageIndex = Utils.GetInt(Request.QueryString["page"], 1);

            EyouSoft.Model.CommunityStructure.SearchInfo se  = new EyouSoft.Model.CommunityStructure.SearchInfo();

            se.BeforeOrAfter = false;
            plist = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            exList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetList(pageSize,pageIndex, ref totalc, se);
            piclist = EyouSoft.BLL.CommunityStructure.BSupplyDemandPic.CreateInstance().GetList();
            msrlist = EyouSoft.BLL.CommunityStructure.BSupplyDemandRule.CreateInstance().GetList();

            //分页控件初始化
            if (exList != null && exList.Count > 0)
            {
                if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
                {
                    this.ExporPageInfoSelect1.Placeholder = "#PageIndex#";
                    this.ExporPageInfoSelect1.IsUrlRewrite = true;
                    this.ExporPageInfoSelect1.PageLinkURL = 
                        EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_p#PageIndex#";
                }
                else
                {
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }

                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = totalc;
            }

        }

        /// <summary>
        /// 获取只看供应或只看求购连接地址
        /// </summary>
        public string GetReturnUrl(int cat)
        {
            string m = this.Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(EyouSoft.Common.Domain.UserPublicCenter + EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("", 0, 0, 0, CityModel.ProvinceId, cat, CityId), "_top", "");
        }
    }
}
