using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace UserPublicCenter.SupplierInfo
{
    public partial class Supplier : System.Web.UI.MasterPage
    {
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected int menuindex = 1;
        protected int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Common.Control.FrontPage page = this.Page as EyouSoft.Common.Control.FrontPage;
            if (page != null)
                CityId = page.CityId;

            if (!IsPostBack)
            {
                this.CityAndMenu1.HeadMenuIndex = 7;
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPageData() 
        {
            //初始化广告
            ltrAdvBraner.Text = GetAdvPath();

            //初始快速发布供求图片
            EyouSoft.SSOComponent.Entity.UserInfo SiteUserInfo = new EyouSoft.SSOComponent.Entity.UserInfo();
            bool IsLogin = EyouSoft.Security.Membership.UserProvider.IsUserLogin(out SiteUserInfo);
            if (!IsLogin && menuindex == 1)
            {
                ltrRelease.Text = string.Format("<a href=\"{0}\"><img src=\"{1}/images/UserPublicCenter/kuaichufabu.gif\" width=\"163\"border=\"0\" height=\"27\" /></a>", EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Utils.GeneratePublicCenterUrl("/SupplierInfo/SupplierInfo.aspx", CityId), string.Empty), Domain.ServerComponents);
            }
            SiteUserInfo = null;

            //初始化统计数据
            EyouSoft.Model.SystemStructure.SummaryCount model = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            ltrSummaryCount.Text = string.Format("<li>注册用户<strong>({0}人)</strong>本周供需信息<strong>({1}条)</strong></li>", model.User + model.UserVirtual, model.Intermediary + model.IntermediaryVirtual);
            model = null;
        }

        /// <summary>
        /// 获取图片广告路径
        /// </summary>
        /// <returns></returns>
        private string GetAdvPath()
        {
            string strPath = string.Empty;
            System.Collections.Generic.IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道通栏banner);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                strPath = Utils.GetImgOrFalash(AdvListBanner[0].ImgPath, AdvListBanner[0].RedirectURL);// string.Format("<a target=\"_blank\" title=\"{3}\" href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" width=\"970\" height=\"70\"></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath, AdvListBanner[0].Title, AdvListBanner[0].Title);
            }
            if (AdvListBanner != null) AdvListBanner.Clear();
            AdvListBanner = null;
            return strPath;
        }

        /// <summary>
        /// 导航栏索引值(1=供求信息，2=行业资讯，3=嘉宾访谈，4=同业学堂)
        /// </summary>
        public int MenuIndex
        {
            set 
            {
                menuindex = value;
            }
        }
    }
}
