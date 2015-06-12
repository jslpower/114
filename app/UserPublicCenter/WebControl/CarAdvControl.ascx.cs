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
using EyouSoft.Common.Control;
using System.Text;
using System.Collections.Generic;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 页面功能：供应商普通网店车队广告模块通用控件
    /// 开发人：杜桂云      开发时间：2010-07-24
    /// </summary>
    public partial class CarAdvControl : System.Web.UI.UserControl
    {
        #region 成员变量
        //两张广告图片
        protected string ImgUrl1 = "";
        protected string ImgUrl2 = "";
        protected string ImageServerPath = "";
        protected int PageCityID = 0;
        protected bool isOpenHighShop = false;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            PageCityID = page.CityId;
            ImageServerPath = page.ImageServerUrl;
            if (!IsPostBack) 
            {
                //绑定图文广告
                GetBannerImg(page.CityId);
                //绑定供求信息
                BindInfo(page.CityId) ;
            }

        }
        #endregion

        #region 绑定供求信息
        /// <summary>
        /// 绑定供求信息
        /// </summary>
        /// <param name="cityID"></param>
        protected void BindInfo(int cityID) 
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> modelList= EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(10,EyouSoft.Model.CommunityStructure.ExchangeType.车辆,cityID,null);
            if (modelList != null && modelList.Count > 0) 
            {
                this.rpt_NeedNewInfo.DataSource = modelList;
                this.rpt_NeedNewInfo.DataBind();
            }
        }
        #endregion

        #region 获取广告的地址和连接
        /// <summary>
        /// 获取广告的地址和连接
        /// </summary>
        protected void GetBannerImg(int CityId)
        {
            EyouSoft.IBLL.AdvStructure.IAdv iBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = null;
            //车队频道旗帜广告1
            AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告1);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                if (Utils.EmptyLinkCode == AdvListBanner[0].RedirectURL)
                {
                    ImgUrl1 = string.Format("<image  alt=\"图片广告\"  src='{0}' width='220' height='90'>", Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
                else
                {
                    ImgUrl1 = string.Format("<a href='{0}' target='_blank'><image  alt=\"图片广告\"  src='{1}' width='220' height='90'></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
            }
            //车队频道旗帜广告2
            AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.车队频道旗帜广告2);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                if (Utils.EmptyLinkCode == AdvListBanner[0].RedirectURL)
                {
                    ImgUrl2 = string.Format("<image src='{0}' alt=\"图片广告\"  width='220' height='90'>",  Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
                else
                {
                    ImgUrl2 = string.Format("<a href='{0}' target='_blank'><image src='{1}' alt=\"图片广告\"  width='220' height='90'></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
            }
            //最新加入
            AdvListBanner = iBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.车队频道最新加入);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                this.rpt_NewsEnjoy.DataSource = AdvListBanner;
                this.rpt_NewsEnjoy.DataBind();
            }
            //释放资源
            AdvListBanner = null;
            iBll = null;
        }
        /// <summary>
        /// 获取最新加入广告的公司网店连接
        /// </summary>
        /// <param name="ComName"></param>
        /// <param name="RedirectURL"></param>
        /// <returns></returns>
        protected string GetShopLink(string ComName, string RedirectURL)
        {
            return string.Format(" <li><a href='{0}' title='{2}' target=\"_blank\">{1}</a></li>", RedirectURL, Utils.GetText(ComName, 13, true), ComName);
        }
        //供求信息标题
        protected string GetTitleLink(string ID,string TitleName)
        {
            string linkUrl = "/SupplierInfo/ExchangeInfo.aspx?id=" + ID+"&CityID="+PageCityID;
            return string.Format("<li><a href='{2}' title='{1}'>{0}</a></li>", Utils.GetText(TitleName, 13, true), TitleName, linkUrl);
          
        }
        #endregion
    }
}