using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Common.Function;
using EyouSoft.Model.ScenicStructure;
using System.Collections.Generic;
using EyouSoft.Common;

namespace SeniorOnlineShop.shop
{
    public partial class ScenicList : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        public int PageSize = 10;
        public int pageindex = 1;
        protected string CompanyId = string.Empty;
        private int RecordCount = 0; //总记录数
        #endregion
        protected SeniorOnlineShop.master.GeneralShop Master;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master = (SeniorOnlineShop.master.GeneralShop)this.Page.Master;
            Master.HeadIndex = 4;
            Page.Title = string.Format(PageTitle.ScenicDetail_Title, CityModel.CityName, Master.CompanyInfo.CompanyName);
            AddMetaTag("description", string.Format(PageTitle.ScenicDetail_Des, CityModel.CityName, Master.CompanyInfo.CompanyName));
            AddMetaTag("keywords", string.Format(EyouSoft.Common.PageTitle.ScenicDetail_Keywords, Master.CompanyInfo.CompanyName));
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;
            SecondMenu1.CurrMenuIndex = 3;
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
            pageindex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            if (pageindex <= 0)
                pageindex = 1;
            var list = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetList(PageSize, pageindex,
                                                                                         ref RecordCount,
                                                                                         Master.CompanyId);
            if (list != null && list.Count > 0)
            {
                rptSightArea.DataSource = list;
                rptSightArea.DataBind();
            }
            else
            {
                this.rptSightArea.EmptyText = "没有相关数据!";
                this.ExporPageInfoSelect1.Visible = false;
            }
            

            //绑定分页控件
            this.ExporPageInfoSelect1.intPageSize = PageSize;
            this.ExporPageInfoSelect1.intRecordCount = RecordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageindex;
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
        /// 取景区的形象照片
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="scenicId"></param>
        /// <returns></returns>
        protected string GetScenicImg(object obj)
        {
            IList<MScenicImg> list = (IList<MScenicImg>)obj;
            string imgAddress = string.Empty;
            if (list != null && list.Count > 0)
            {
                //每个景区，形象照片只有一张
                foreach (MScenicImg i in list)
                {
                    if (i.ImgType == ScenicImgType.景区形象)
                    {
                        imgAddress = i.ThumbAddress;
                        break;
                    }
                }
            }
            return imgAddress;
        }
    }
}
