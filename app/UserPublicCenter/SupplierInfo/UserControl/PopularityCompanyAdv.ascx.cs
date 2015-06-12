using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 本周最具人气企业推荐
    /// </summary>
    /// 周文超 2010-08-01
    public partial class PopularityCompanyAdv : EyouSoft.Common.Control.BaseUserControl
    {
        private int CityId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Common.Control.FrontPage page = this.Page as EyouSoft.Common.Control.FrontPage;
            if (page != null)
                CityId = page.CityId;
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道最具人气企业推荐);
            rptAdvList.DataSource = AdvListBanner;
            rptAdvList.DataBind();
            if (AdvListBanner != null) AdvListBanner.Clear();
            AdvListBanner = null;
        }

        #region 前台函数

        /// <summary>
        /// 最具人气企业推荐图片连接
        /// </summary>
        /// <returns></returns>
        protected string ShowPicAdvInfo(string RedirectURL, string ImagPath)
        {
            string PicImgUrl = "";
            if (string.IsNullOrEmpty(ImagPath) || ImagPath.Trim() == "广告默认链接" || ImagPath.Trim() == "广告默认图片")
            {
                PicImgUrl = string.Format("<a href='{0}' target='{2}'><image src='{1}' width='99' height='72' border='0'></a>", RedirectURL, Utils.NoLogoImage92_84, RedirectURL == EyouSoft.Common.Utils.EmptyLinkCode ? "_self" : "_blank");
            }
            else
            {
                PicImgUrl = string.Format("<a href='{0}' target='{2}'><image src='{1}' width='99' height='72' border='0'></a>", RedirectURL, Domain.FileSystem + ImagPath, RedirectURL == EyouSoft.Common.Utils.EmptyLinkCode ? "_self" : "_blank");
            }
            return PicImgUrl;
        }

        /// <summary>
        /// 最具人气企业推荐图片文字链接
        /// </summary>
        /// <returns></returns>
        protected string ShowTitleAdvInfo(string Title, string RedirectURL)
        {
            string PicImgUrl = "";
            PicImgUrl = string.Format("<a href='{0}' title='{2}' target='{3}'>{1}</a>", RedirectURL, Utils.GetText(Title, 6, true), Title, RedirectURL == EyouSoft.Common.Utils.EmptyLinkCode ? "_self" : "_blank");
            return PicImgUrl;
        }

        #endregion
    }
}