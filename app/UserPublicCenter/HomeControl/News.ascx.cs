using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.HomeControl
{
    public partial class News : System.Web.UI.UserControl
    {
        protected string newImg;//行业资讯图片
        protected string noBannerAdv;//非通栏广告
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取非通栏广告
            int cityId = (this.Page as EyouSoft.Common.Control.FrontPage).CityId;
            IList<EyouSoft.Model.AdvStructure.AdvInfo> noBannerAdvs = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(cityId, EyouSoft.Model.AdvStructure.AdvPosition.首页资讯非通栏广告);
            EyouSoft.Model.AdvStructure.AdvInfo noBannerModel = noBannerAdvs.FirstOrDefault();
            noBannerAdv = noBannerModel != null ? String.Format("<a href=\"{0}\" target=\"_blank\"><img style=\"width:324px;height:60px;\" src=\"{3}{1}\"  alt=\"{2}\"/></a>", noBannerModel.RedirectURL, noBannerModel.ImgPath, noBannerModel.Title, EyouSoft.Common.Domain.FileSystem) : "<img  alt=\"\" style=\"width:324px;height:60px;\" />";
            BindNews();
        }

        #region 绑定行业资讯
        /// <summary>
        /// 绑定行业资讯
        /// </summary>
        protected void BindNews()
        {
            //绑定行业资讯
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> newList =EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance().GetHeadNews(10);
            if (newList != null)
            {
                if (newList.Count >= 1)
                {   //获取第一条行业资讯
                    EyouSoft.Model.NewsStructure.WebSiteNews newModel = newList[0];
                    newImg = string.Format("<a href=\"{0}\" target=\"_blank\"><img src=\"{1}\" alt=\"{2}\" /></a>", GetUrl(newModel.GotoUrl,newModel.Id), Utils.GetNewImgUrl(newModel.PicPath, 2), newModel.AfficheTitle);
                }
                rptNew1.DataSource = newList.Skip(1).Take(5);
                rptNew1.DataBind();
                if (newList.Count > 6)
                {
                    rptNew2.DataSource = newList.Skip(6).Take(4);
                    rptNew2.DataBind();
                }
            }
        }
        /// <summary>
        /// 返回url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected string GetUrl(Object url,object Id)
        {
            if (url != null)
            {
                string urlStr = url.ToString();
                if (urlStr.Length > 0)
                {
                    return urlStr;
                }
                else
                {
                    return EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(1, (int)Id);
                }
            }
            return EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(1, (int)Id);
           
        }
        #endregion
    }
}