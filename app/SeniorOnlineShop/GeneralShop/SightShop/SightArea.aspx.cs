using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.GeneralShop.SightShop
{
    /// <summary>
    /// 普通网店景区列表页
    /// </summary>
    /// 周文超 2011-11-16
    public partial class SightArea : EyouSoft.Common.Control.FrontPage
    {
        private int _pageSize = 5;
        private int _pageIndex = 1;
        private int _recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HeadIndex = 5;
            Page.Title = string.Format(PageTitle.ScenicDetail_Title, CityModel.CityName, Master.CompanyInfo.CompanyName);
            AddMetaTag("description", string.Format(PageTitle.ScenicDetail_Des, CityModel.CityName, Master.CompanyInfo.CompanyName));
            AddMetaTag("keywords", string.Format(PageTitle.ScenicDetail_Keywords, Master.CompanyInfo.CompanyName));
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;

            if (!IsPostBack)
            {
                InifSightArea();
            }
        }

        /// <summary>
        /// 初始化景区列表 
        /// </summary>
        private void InifSightArea()
        {
            _pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            if (_pageIndex <= 0)
                _pageIndex = 1;
            var list = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(_pageSize, _pageIndex,
                                                                                         ref _recordCount,
                                                                                         Master.CompanyId);
            rptSightArea.DataSource = list;
            rptSightArea.DataBind();

            //绑定分页控件
            this.ExporPageInfoSelect1.intPageSize = _pageSize;
            this.ExporPageInfoSelect1.intRecordCount = _recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = _pageIndex;
            this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
            this.ExporPageInfoSelect1.LinkType = 3;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                //是否重写 分页的链接
                this.ExporPageInfoSelect1.IsUrlRewrite = true;
                //设置需要替换的值
                this.ExporPageInfoSelect1.Placeholder = "#PageIndex#";
                //获得线路的url 赋值给分页控件
                this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            }
        }

        /// <summary>
        /// 生成景区html
        /// </summary>
        /// <param name="obj">景区信息实体</param>
        /// <returns></returns>
        protected string GetSightArea(object obj)
        {
            if (obj == null)
                return string.Empty;

            var model = (EyouSoft.Model.ScenicStructure.MScenicArea)obj;

            var strReturn = new StringBuilder();
            string strImg = string.Empty;
            if (model.Img != null && model.Img.Count > 0)
            {
                var listImg = (List<EyouSoft.Model.ScenicStructure.MScenicImg>)model.Img;
                var img = listImg.Find(t => (t.ImgType == EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象));
                if (img != null)
                    strImg = img.ThumbAddress;
            }
            strReturn.AppendFormat(
                "<div class=\"ptjd_img2\"><a target=\"_blank\" href=\"{0}\" title=\"{2}\"><img src=\"{1}\" width=\"109\" height=\"98\" alt=\"{2}\" /></a></div>",
                Domain.UserPublicCenter + "/jingquinfo_" + model.Id, Utils.GetNewImgUrl(strImg, 3), model.ScenicName);
            strReturn.Append("<div class=\"ptjd_right\">");
            strReturn.AppendFormat("<p class=\"ptjd_title\"><a target=\"_blank\" href=\"{0}\">{1}</a></p>", Domain.UserPublicCenter + "/jingquinfo_" + model.Id, model.ScenicName);
            strReturn.AppendFormat("<p class=\"ptjd_mian\">{0}</p>", Utils.GetText2(Utils.LoseHtml(model.Description), 173, true));
            strReturn.Append("</div>");
            return strReturn.ToString();
        }
    }
}
