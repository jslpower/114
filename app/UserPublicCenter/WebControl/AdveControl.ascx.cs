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
    /// 页面功能：机票前台页广告模块通用控件
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class AdveControl : System.Web.UI.UserControl
    {
        #region 成员变量
        protected int index = 0;
        protected string ImageServerPath = "";
        protected string imgUrl1 = "";
        protected string imgUrl2 = "";
        protected int CityID = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            CityID = page.CityId;
            BindImgInfo(CityID);
        }
        #endregion

        #region 绑定页面相关信息
        protected void BindImgInfo(int CityID)
        {
            EyouSoft.IBLL.AdvStructure.IAdv iBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = null;
            //供求信息频道文章及列表右侧11
            AdvListBanner = iBll.GetAdvs(CityID, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道文章及列表右侧1);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                if (Utils.EmptyLinkCode == AdvListBanner[0].RedirectURL)
                {
                    imgUrl1 = string.Format("<image src='{0}' width='250' height='170'>", Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
                else
                {
                    imgUrl1 = string.Format("<a href='{0}' target='_blank'><image src='{1}' width='250' height='170'></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
            }
            //供求信息频道文章及列表右侧12
            AdvListBanner = iBll.GetAdvs(CityID, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道文章及列表右侧2);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                if (Utils.EmptyLinkCode == AdvListBanner[0].RedirectURL)
                {
                    imgUrl2 = string.Format("<image src='{0}' width='250' height='170'>", Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
                else
                {
                    imgUrl2 = string.Format("<a href='{0}' target='_blank'><image src='{1}' width='250' height='170'></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath);
                }
            }
            //本周最具人气企业推荐 
            AdvListBanner = iBll.GetAdvs(CityID, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道最具人气企业推荐);
            if (AdvListBanner != null && AdvListBanner.Count > 0)
            {
                this.dal_PicAdvList.DataSource = AdvListBanner;
                this.dal_PicAdvList.DataBind();
            }
            //释放资源
            AdvListBanner = null;
            iBll = null;
            //行业资讯
            //本周新闻排行
            EyouSoft.IBLL.CommunityStructure.IInfoArticle aBll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> aModelList = null;
            aModelList = aBll.GetTopNumCurrWeekList(8, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯,null,false);
            if (aModelList != null && aModelList.Count > 0)
            {
                this.rpt_NewsTopList.DataSource = aModelList;
                this.rpt_NewsTopList.DataBind();
            }
            //最新新闻咨询
            aModelList = aBll.GetTopNumList(8, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, null, false, "");
            if (aModelList != null && aModelList.Count > 0)
            {
                this.rpt_NewNewsInfo.DataSource = aModelList;
                this.rpt_NewNewsInfo.DataBind();
            }
            //同业学堂
            aModelList = aBll.GetTopNumList(8, null,EyouSoft.Model.CommunityStructure.TopicAreas.新手计调, false, "");
            if (aModelList != null && aModelList.Count > 0)
            {
                this.rep_NewEnjoyStudy.DataSource = aModelList;
                this.rep_NewEnjoyStudy.DataBind();
            }
            aModelList = aBll.GetTopNumList(8, null, EyouSoft.Model.CommunityStructure.TopicAreas.带团经验分享,false, "");
            if (aModelList != null && aModelList.Count > 0)
            {
                this.rep_TourEnjoy.DataSource = aModelList;
                this.rep_TourEnjoy.DataBind();
            }
            aModelList = aBll.GetTopNumList(8, null, EyouSoft.Model.CommunityStructure.TopicAreas.景区案例分析, false, "");
            if (aModelList != null && aModelList.Count > 0)
            {
                this.rep_AnLiFenXi.DataSource = aModelList;
                this.rep_AnLiFenXi.DataBind();
            }
        }
        #endregion
 
        #region 处理相关项的绑定
        protected string BindOrderNew(string ID,string titleName,int typeID) 
        {
            index++;
            string linkUrl = Utils.GeneratePublicCenterUrl("/SupplierInfo/ArticleInfo.aspx?id=" + ID, CityID); 
            if (typeID == 0)
            {
                return string.Format("<li><span class=\"hong16\">{0}</span><a href='{3}' title='{2}'>{1}</a></li>", index, Utils.GetText(titleName, 15, true),titleName,linkUrl);
            }
            else 
            {
                return string.Format("<li>·<a href='{2}' title='{1}'>{0}</a></li>", Utils.GetText(titleName, 15, true), titleName, linkUrl);
            }
        }
        /// <summary>
        /// 最具人气企业推荐图片连接
        /// </summary>
        /// <returns></returns>
        protected string ShowPicAdvInfo(string RedirectURL, string ImagPath)
        {
            if (ImagPath == "")
            {
                ImagPath = Utils.NoLogoImage92_84;
            }
            else 
            {
                ImagPath = Domain.FileSystem + ImagPath;
            }
            string PicImgUrl = "";
            PicImgUrl = string.Format("<a href='{0}' target='_blank'><image src='{1}' width='99' height='72' border='0'></a>", RedirectURL,  ImagPath);
            return PicImgUrl;
        }
        /// <summary>
        /// 最具人气企业推荐图片文字链接
        /// </summary>
        /// <returns></returns>
        protected string ShowTitleAdvInfo(string Title, string RedirectURL)
        {
            string PicImgUrl = "";
            PicImgUrl = string.Format("<a href='{0}' title='{2}' target='_blank'>{1}</a>", RedirectURL, Utils.GetText(Title, 6, true), Title);
            return PicImgUrl;
        }
        #endregion
    }
}