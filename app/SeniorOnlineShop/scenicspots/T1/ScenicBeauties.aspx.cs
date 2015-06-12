using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景区美图
    /// Create:luofx  Date:2010-12-8
    /// </summary>
    public partial class ScenicBbeauties : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        protected string TabIndex = string.Empty;
        protected string TabName = string.Empty;
        protected string StrTitle = string.Empty;
        protected string CompanyId = string.Empty;
        protected string GuideHtml = string.Empty;
        public int intPageSize = 9;
        public int CurrencyPage = 1;
        private int intRecordCount = 0; //总记录数
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //导航样式制定
            ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = SeniorOnlineShop.master.SPOTT1TAB.景区美图;
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            CompanyId = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CompanyId;
            CurrencyPage = StringValidate.GetIntValue(Request.QueryString["Page"], 1);
            var qmodel = new EyouSoft.Model.ScenicStructure.MScenicImgSearch
            {
                ImgType =
                    new EyouSoft.Model.ScenicStructure.ScenicImgType?[]
                                         {
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.景区形象,
                                             EyouSoft.Model.ScenicStructure.ScenicImgType.其他
                                         }
            };
            IList<EyouSoft.Model.ScenicStructure.MScenicImg> list =
                EyouSoft.BLL.ScenicStructure.BScenicImg.CreateInstance().GetList(intPageSize, CurrencyPage,
                                                                                 ref intRecordCount, CompanyId, qmodel);
            if (list != null && list.Count > 0)
            {
                //加上文件系统URL前缀
                ((List<EyouSoft.Model.ScenicStructure.MScenicImg>)list).ForEach(item =>
                {
                    item.Address = EyouSoft.Common.Domain.FileSystem + item.Address;
                    item.ThumbAddress = EyouSoft.Common.Domain.FileSystem + item.ThumbAddress;
                });
                this.rptData.DataSource = list;
                this.rptData.DataBind();
                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                //this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                //this.ExporPageInfoSelect1.PageLinkURL = "/scenicspots/t1/ScenicBeauties.aspx?";
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
            else
            {
                NoData.Visible = true;
            }
            list = null;
        }
    }
}
