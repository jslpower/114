using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using System.Text;

namespace UserPublicCenter.Information
{
    /// <summary>
    /// 线路详细列
    /// 作者：xuqh 2011-4-1
    /// </summary>
    public partial class NewsXianLu : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageSize = 10;
        protected int pageIndex = 1;
        int recordCount = 0;

        /// <summary>
        /// 线路区域ID
        /// </summary>
        protected int TourAreaId = 0;

        /// <summary>
        /// 返回页面地址
        /// </summary>
        protected string ReturnUrl = "";

        protected string strAllEmpty = "";

        /// <summary>
        /// 日历当前月
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
            TourAreaId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["TourAreaId"]);

            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                this.CityAndMenu1.HeadMenuIndex = 2;
                
                InitXianLu();
            }

            string tourAreaName = this.labTourAreaName.Text;
            this.Page.Title = string.Format(PageTitle.RouteList_Title, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName);
            AddMetaTag("description", string.Format(PageTitle.RouteList_Des, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName));
            AddMetaTag("keywords", string.Format(PageTitle.RouteList_Keywords, tourAreaName, tourAreaName, tourAreaName));
        }

        /// <summary>
        /// 初始化线路信息
        /// </summary>
        private void InitXianLu()
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> TourList = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
            TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByAreaId(pageSize, pageIndex, ref recordCount,TourAreaId,0);

            if (TourList != null && TourList.Count > 0)
            {
                BindPage();
                rptXianLu.DataSource = TourList;
                rptXianLu.DataBind();
            }
            else
            {
                StringBuilder strEmpty = new StringBuilder();
                this.ExportPageInfo.Visible = false;
                strEmpty.Append("没有找到相关的结果");
                strAllEmpty = strEmpty.ToString();
                this.NoDate.Visible = true;
            }
            TourList = null;
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPage()
        {
            this.ExportPageInfo.intPageSize = pageSize;
            this.ExportPageInfo.CurrencyPage = pageIndex;
            this.ExportPageInfo.intRecordCount = recordCount;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfo.IsUrlRewrite = true;
                this.ExportPageInfo.Placeholder = "#PageIndex#";
                this.ExportPageInfo.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo.UrlParams = Request.QueryString;
            }
        }

        /// <summary>
        /// 前台显示公司信息
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="CompanyName">公司名称</param>
        /// <returns></returns>
        protected string GetCompanyInfo(string CompanyId, string CompanyName)
        {
            string strCompany = "";

            bool isOpenHighShop = false;
            EyouSoft.Model.CompanyStructure.CompanyState CompanyStateModel = new EyouSoft.Model.CompanyStructure.CompanyState();

            string GoToUrl = EyouSoft.Common.Utils.GetCompanyDomain(CompanyId, out CompanyStateModel, out isOpenHighShop, EyouSoft.Model.CompanyStructure.CompanyType.专线, CityId);

            string Message = "&nbsp;<span class=\"danhui\">供应商：{0}</span><span class=\"huise\">";
            //是否开通高级网店
            if (isOpenHighShop)
            {
                Message = string.Format(Message, "<a href='" + GoToUrl + "' target='_blank'><img src=\"" + ImageServerPath + "/images/UserPublicCenter/shopico.gif\" /></a>");
            }
            else
            {
                Message = string.Format(Message, "");
            }
            strCompany = string.Format("{2}<a href=\"{0}\" target='_blank'>{1}</a>", GoToUrl, CompanyName, Message);

            return strCompany;
        }

        /// <summary>
        /// 推广说明/团队状态
        /// </summary>
        /// <param name="TourId">团队ID</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="TourState">团队状态</param>
        /// <param name="TourSpreadStateName">推广名称</param>
        /// <returns></returns>
        protected string TourSpreadState(string TourState, string TourSpreadStateName)
        {
            string strTourMarkerNote = TourSpreadStateName;
            //团队推广说明
            if (TourState == "手动客满" || TourState == "自动客满")
            {
                strTourMarkerNote = "<span class=\"keman\">客满</span>";
            }
            if (TourState == "手动停收" || TourState == "自动停收")
            {
                strTourMarkerNote = "<span class=\"tings\">停收</span>";
            }
            return strTourMarkerNote;
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="time">时间</param>
        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }
    }
}
