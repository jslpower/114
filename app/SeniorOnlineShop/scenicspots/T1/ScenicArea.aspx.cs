using System;
using System.Collections;
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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.BLL.ScenicStructure;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop.scenicspots.T1
{
    public partial class ScenicArea : EyouSoft.Common.Control.FrontPage
    {
        #region 变量
        public int intPageSize = 10;
        public int CurrencyPage = 1;
        protected string CompanyId = string.Empty;
        private int intRecordCount = 0; //总记录数
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //导航样式制定
            ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CTAB = SeniorOnlineShop.master.SPOTT1TAB.景区门票;
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 读取景区列表
        /// </summary>
        protected void InitPage()
        {
            CompanyId = ((SeniorOnlineShop.master.ScenicSpotsT1)base.Master).CompanyId;
            CurrencyPage = StringValidate.GetIntValue(Request.QueryString["Page"], 1);
            IList<MScenicArea> listArea = BScenicArea.CreateInstance().GetList(intPageSize, CurrencyPage,
                ref intRecordCount, CompanyId);
            if (listArea != null && listArea.Count > 0)
            {
                rpt_Scenic.DataSource = listArea;
                rpt_Scenic.DataBind();

                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
                {
                    //是否重写 分页的链接
                    this.ExporPageInfoSelect1.IsUrlRewrite = true;
                    //设置需要替换的值
                    this.ExporPageInfoSelect1.Placeholder = "#Page#";
                    //获得线路的url 赋值给分页控件
                    this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#Page#";
                }
                else
                {
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                }
                //this.ExporPageInfoSelect1.PageLinkURL = "/scenicspots/t1/ScenicArea.aspx?";
                this.ExporPageInfoSelect1.LinkType = 3;
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

            return  imgAddress;
        }
    }
}
