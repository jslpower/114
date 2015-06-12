/*Author:汪奇志 2010-12-08*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景点高级网店模板1首页
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitLinks();
            this.InitScrollAd();
            this.InitScenic();
            this.InitJQMT();
            this.InitBannerAd();
            this.InitOlServer();
        }

        #region private members
        /// <summary>
        /// 初始化滚动广告
        /// </summary>
        private void InitScrollAd()
        {
            StringBuilder scripts = new StringBuilder();
            var ads = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5, this.Master.CompanyId);
            scripts.Append("var sliderData=[];");
            if (ads != null && ads.Count > 0)
            {
                foreach (var ad in ads)
                {
                    scripts.AppendFormat("sliderData.push({{src:'{0}',lnk:'{1}',title:''}});", Domain.FileSystem + ad.ImagePath, ad.LinkAddress);
                }
            }

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SCROLLADSCRIPT", scripts.ToString(), true);
        }

        /// <summary>
        /// 初始化景区
        /// </summary>
        private void InitScenic()
        {
            rptScenic.DataSource = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(2, Master.CompanyId,
                                                                                                     null);
            rptScenic.DataBind();
        }

        /// <summary>
        /// 初始化链接
        /// </summary>
        private void InitLinks()
        {
            this.lnkJQMT.HRef = Utils.GenerateShopPageUrl2(this.Master.TABLINKS_JQMT, this.Master.CompanyId);
        }

        /// <summary>
        /// 初始化景区美图
        /// </summary>
        private void InitJQMT()
        {
            var qmodel = new EyouSoft.Model.ScenicStructure.MScenicImgSearch
            {
                ImgType =
                    new EyouSoft.Model.ScenicStructure.ScenicImgType?[]
                                         {
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.其他
                                         }
            };
            var items = EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(8, Master.CompanyId, qmodel);

            this.rptJQMT.DataSource = items;
            this.rptJQMT.DataBind();
        }

        /// <summary>
        /// 初始化横幅广告
        /// </summary>
        private void InitBannerAd()
        {
            var sInfo = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(this.Master.CompanyId);

            if (sInfo != null && sInfo.ProductInfo != null && sInfo.ProductInfo.Count > 0 && sInfo.ProductInfo[0] != null)
            {
                var adInfo = sInfo.ProductInfo[0];
                this.ltrBannerAd.Text = string.Format("<div class=\"bannerad\"><a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" class=\"add002\"></a></div>", adInfo.ImageLink
                    , Domain.FileSystem + adInfo.ImagePath);
            }
        }
        /// <summary>
        /// 初始化在线服务
        /// </summary>
        public void InitOlServer()
        {
            SeniorOnlineShop.master.ScenicSpotsT1 cMaster = (SeniorOnlineShop.master.ScenicSpotsT1)this.Master;
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
