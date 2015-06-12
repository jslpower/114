using System;
using System.Collections.Generic;
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
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-27
    /// 描述：供应商-行业资讯
    /// </summary>
    public partial class InfoArticle : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Supplier site = (Supplier)this.Master;
            if(site!=null)
                site.MenuIndex = 2;
            if (!IsPostBack)
            {
                #region 景点新闻
                Viewpoint.TopNumber = 7;
                Viewpoint.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.景区新闻;
                Viewpoint.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯; 
                #endregion

                #region 旅行社新闻
                Travel.TopNumber = 7;
                Travel.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.旅行社;
                Travel.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯; 
                #endregion

                #region 酒店新闻
                Hotel.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.酒店新闻;
                Hotel.TopNumber = 7;
                Hotel.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯; 
                #endregion

                #region 政策解读
                Policy.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.政策解读;
                Policy.TopNumber = 8;
                Policy.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯; 
                #endregion

                #region 成功故事
                Success.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.成功故事;
                Success.TopNumber = 10;
                Success.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯; 
                #endregion

                #region 绑定新闻，资讯列表
                EyouSoft.IBLL.CommunityStructure.IInfoArticle IBll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
                EyouSoft.Model.CommunityStructure.InfoArticle model= null;
                IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = null;

                #region 绑定新闻
                model =new EyouSoft.Model.CommunityStructure.InfoArticle();
                model=IBll.GetHeadInfo(EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, EyouSoft.Model.CommunityStructure.TopicAreas.新闻资讯);
                if (model != null)
                {
                    lbNews.Text = string.Format("<a href='/SupplierInfo/ArticleInfo.aspx?Id={0}' title=\"{2}\">{1}</a>", model.ID, model.TitleColor.Length == 0 ? Utils.GetText(model.ArticleTitle, 22) : string.Format("<font color='{0}'>{1}</font>", model.TitleColor, Utils.GetText(model.ArticleTitle, 22)), model.ArticleTitle);
                    lbNewsContent.Text =Utils.GetText(StringValidate.LoseHtml(model.ArticleText),75,true);
                    ltnews.Text = string.Format("<a href=\"/SupplierInfo/ArticleInfo.aspx?Id={0}\"><span class=\"ff0000\">阅读全文>></span></a>", model.ID);
                }
                model = null;

                list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
                list = IBll.GetTopNumList(12, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, EyouSoft.Model.CommunityStructure.TopicAreas.新闻资讯, null, string.Empty);
                rpNews.DataSource = list;
                rpNews.DataBind();
                list = null;
                #endregion

                #region 绑定资讯
                model = new EyouSoft.Model.CommunityStructure.InfoArticle();
                model = IBll.GetHeadInfo(EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, EyouSoft.Model.CommunityStructure.TopicAreas.行业动态);
                if (model != null)
                {
                    lbInfo.Text = string.Format("<a href='/SupplierInfo/ArticleInfo.aspx?Id={0}' title=\"{2}\">{1}</a>", model.ID, model.TitleColor.Length == 0 ? Utils.GetText(model.ArticleTitle, 22) : string.Format("<font color='{0}'>{1}</font>", model.TitleColor, Utils.GetText(model.ArticleTitle, 22)), model.ArticleTitle);
                    lbinfoContent.Text = Utils.GetText(StringValidate.LoseHtml(model.ArticleText), 70, true);
                    ltinfo.Text = string.Format("<a href=\"/SupplierInfo/ArticleInfo.aspx?Id={0}\"><span class=\"ff0000\">阅读全文>></span></a>",model.ID);
                }
                model = null;

                list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
                list = IBll.GetTopNumList(13, EyouSoft.Model.CommunityStructure.TopicClass.行业资讯, EyouSoft.Model.CommunityStructure.TopicAreas.行业动态, null, string.Empty);
                rpInfo.DataSource = list;
                rpInfo.DataBind();
                list = null;
                #endregion

                IBll = null;
                #endregion

                #region 最新行业资讯
                CommonTopic1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
                CommonTopic1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
                CommonTopic1.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
                //CommonTopic1.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.行业动态;
                #endregion

                #region 同业之星访谈
                CommonTopic2.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业之星访谈;
                CommonTopic2.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
                CommonTopic2.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.同业之星;
                CommonTopic2.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
                CommonTopic2.CurrCityId = CityId;
                //CommonTopic2.TopicClass = null;
                //CommonTopic2.TopicArea = null;
                #endregion

                #region 同业学堂
                CommonTopic3.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业学堂;
                CommonTopic3.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
                #endregion

                #region 图片新闻
                CommonTopic4.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
                CommonTopic4.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
                CommonTopic4.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
                #endregion

                #region 旗帜广告

                System.Collections.Generic.IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道行业资讯旗帜广告);
                if (AdvListBanner != null && AdvListBanner.Count > 0)
                {
                    ltrAdv.Text = string.Format("<a target=\"_blank\" title=\"{3}\" href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" width=\"250\" height=\"85\"></a>", AdvListBanner[0].RedirectURL, Domain.FileSystem + AdvListBanner[0].ImgPath, AdvListBanner[0].Title, AdvListBanner[0].Title);
                }
                if (AdvListBanner != null) AdvListBanner.Clear();
                AdvListBanner = null;

                #endregion
            }
        }
    }
}
