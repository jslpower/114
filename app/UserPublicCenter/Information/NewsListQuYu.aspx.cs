using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace UserPublicCenter.Information
{
    /// <summary>
    /// 区域列表
    /// 作者：xuqh 2011-4-2
    /// </summary>
    public partial class NewsListQuYu : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageSize = 30;
        protected int pageIndex = 1;
        int recordCount = 0;

        //资讯省份名称
        protected string provinceName = string.Empty;
        //查询实体
        EyouSoft.Model.NewsStructure.SearchOrderInfo queryModel = null;

        protected IList<EyouSoft.Model.NewsStructure.NewsModel> lsNews = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                this.Page.Title = "全国旅游咨询";

                if (Request["TourAreaId"] != null)
                {
                    if (int.Parse(Request["TourAreaId"].ToString()) == 35)
                    {
                        provinceName = "全国";
                    }
                    else
                    {
                        if (EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(int.Parse(Request["TourAreaId"].ToString())) != null)
                        {
                            EyouSoft.Model.SystemStructure.SysProvince model = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(int.Parse(Request["TourAreaId"].ToString()));
                            provinceName = model.ProvinceName;
                        }
                    }
                    InitQuYu(int.Parse(Request["TourAreaId"].ToString()));
                }
            }
        }

        /// <summary>
        /// 初始化区域信息
        /// </summary>
        private void InitQuYu(int tourAreaId)
        {
            EyouSoft.BLL.NewsStructure.NewsBll bll = new EyouSoft.BLL.NewsStructure.NewsBll();
            lsNews = new List<EyouSoft.Model.NewsStructure.NewsModel>();

            queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
            queryModel.ProvinceId = tourAreaId;
            lsNews = bll.GetList(pageSize, pageIndex, ref recordCount,queryModel);

            if (lsNews != null && lsNews.Count > 0)
            {
                rptQuYu.DataSource = lsNews;
                rptQuYu.DataBind();

                BindPageDown();
                BindPageUp();
            }
            else
            {
                this.ExportPageInfoUp.Visible = false;
                this.ExportPageInfoDown.Visible = false;
            }

            this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_List_Title, provinceName + "-大全");
            AddMetaTag("keywords",string.Format(EyouSoft.Common.PageTitle.Information_List_Keywords,provinceName));
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.Information_List_Des, provinceName));
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPageDown()
        {
            this.ExportPageInfoDown.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfoDown.IsUrlRewrite = true;
                this.ExportPageInfoDown.Placeholder = "#PageIndex#";
                this.ExportPageInfoDown.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfoDown.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfoDown.UrlParams = Request.QueryString;
            }
            this.ExportPageInfoDown.intPageSize = pageSize;
            this.ExportPageInfoDown.CurrencyPage = pageIndex;
            this.ExportPageInfoDown.intRecordCount = recordCount;
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPageUp()
        {
            this.ExportPageInfoUp.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfoUp.IsUrlRewrite = true;
                this.ExportPageInfoUp.Placeholder = "#PageIndex#";
                this.ExportPageInfoUp.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfoUp.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfoUp.UrlParams = Request.QueryString;
            }
            this.ExportPageInfoUp.intPageSize = pageSize;
            this.ExportPageInfoUp.CurrencyPage = pageIndex;
            this.ExportPageInfoUp.intRecordCount = recordCount;
        }
    }
}
