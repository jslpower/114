using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using UserPublicCenter.MasterPage;

namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// ［线路］团队列表（从线路栏目查看详细信息）
    /// 开发人：刘玉灵  时间：2010-6-24
    /// 孙川 时间：2011-05-20 界面调整
    /// </summary>
    public partial class oldTourList : EyouSoft.Common.Control.FrontPage
    { 
        /// <summary>
        /// 线路区域ID
        /// </summary>
        protected int TourAreaId = 0;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 返回页面地址
        /// </summary>
        protected string ReturnUrl = "";
        /// <summary>
        /// 日历当前月
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        /// <summary>
        /// 团队打印单地址
        /// </summary>
        protected string TourPrintPage = EyouSoft.Common.Domain.UserBackCenter + "/PrintPage/TeamInformationPrintPage.aspx";
        /// <summary>
        /// 五个批发商信息
        /// </summary>
        protected string FiveCompany = "";
        /// <summary>
        /// 首页链接
        /// </summary>
        
        protected string strAllEmpty = "";
        //专线名称
        public string strRourListName = "所有线路";
        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
            TourAreaId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["TourAreaId"]);
            
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                
                NewPublicCenter master = (this.Master as NewPublicCenter);
                master.HeadMenuIndex = 2;
                this.TourSearchKeys1.IsDefault = false;
                BindCompanyAndTour();
            }

            
            string tourAreaName = this.hidRourName.Value;
            this.Page.Title = string.Format(PageTitle.RouteList_Title, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName);
            AddMetaTag("description", string.Format(PageTitle.RouteList_Des, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName));
            AddMetaTag("keywords", string.Format(PageTitle.RouteList_Keywords, tourAreaName, tourAreaName, tourAreaName));


        }

        /// <summary>
        /// 绑定线路区域下的5个广告位及团队信息
        /// </summary>
        protected void BindCompanyAndTour()
        {
            int recordCount = 0;
            string SearchType = Request.QueryString["SearchType"];
            int intSearchId = 0;
            string SearchId = Request.QueryString["Id"];

            intSearchId = EyouSoft.Common.Function.StringValidate.GetIntValue(SearchId);

            StringBuilder strEmpty = new StringBuilder();
            string ErrorInfo = "";
            int? SearchTourArea = null;
            if (TourAreaId > 0)
            {
                EyouSoft.Model.SystemStructure.SysArea Model = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(TourAreaId);
                if (Model != null)
                {
                    ErrorInfo += "“" + Model.AreaName + "”";
                    this.hidRourName.Value = Model.AreaName;
                    strRourListName = Model.AreaName;
                    
                }
                Model = null;
                SearchTourArea = TourAreaId;
            }

            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany CompanySearch = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            CompanySearch.CityId = CityId;
            CompanySearch.AreaId = TourAreaId;
            IList<EyouSoft.Model.CompanyStructure.CompanyPicTxt> CompanyList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListCityAreaAdvRouteAgency(CompanySearch);
            StringBuilder StrCompanyList = new StringBuilder();
            if (CompanyList != null && CompanyList.Count > 0)
            {
                

                int CompanyCount = CompanyList.Count;
                for (int i = 0; i < CompanyList.Count; i++)
                {
                    bool isSelect = false;
                    string classNmae = "pingpaibg";
                    string strisSelect = " onmouseout=\"this.style.border='1px solid #ccc'\" onmouseover=\"this.style.border='1px  solid #FFAE78'\"";
                    if (SearchType != null && SearchType.Equals("Company", StringComparison.OrdinalIgnoreCase))
                    {
                        if (CompanyList[i].ID == SearchId)
                        {
                            isSelect = true;
                            ErrorInfo += "“" + CompanyList[i].CompanyName + "”";
                            //strEmpty.AppendFormat("“{0}”", CompanyList[i].CompanyName);
                            strisSelect = "";
                            classNmae = "pingpaibgon";
                        }
                    }

                    StrCompanyList.AppendFormat("<li {0} ><div " + strisSelect + ">", isSelect ? " class=\"active\" " : "");
                    StrCompanyList.AppendFormat("<a href=\"{0}\"><img src=\"{1}\" width=\"114\" height=\"50\" border=\"0\" /></a>", "/TourManage/TourList.aspx?CityId=" + CityId + "&TourAreaId=" + TourAreaId + "&SearchType=Company&Id=" + CompanyList[i].ID, CompanyList[i].CompanyLogo.ImagePath != "" ? EyouSoft.Common.Domain.FileSystem + CompanyList[i].CompanyLogo.ImagePath : EyouSoft.Common.Utils.NoLogoImage100_55);
                    StrCompanyList.AppendFormat("<a href=\"{0}\"><p>{1}</p></a></div></li>", "/TourManage/TourList.aspx?CityId=" + CityId + "&TourAreaId=" + TourAreaId + "&SearchType=Company&Id=" + CompanyList[i].ID, CompanyList[i].CompanyName);
                }
               

            }
            CompanyList = null;
            CompanySearch = null;


            FiveCompany = StrCompanyList.ToString();



            IList<EyouSoft.Model.TourStructure.TourBasicInfo> TourList = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
            if (!string.IsNullOrEmpty(SearchType))
            {
                //按主题类型
                if (SearchType.Equals("Theme", StringComparison.OrdinalIgnoreCase))
                {
                    TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByRouteTheme(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, intSearchId, false,true);
                }
                //按价格区间
                else if (SearchType.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    int? MinPrice = null;
                    int? MaxPrice = null;
                    string PriceType = "之间";
                    switch (SearchId)
                    {
                        case "1":
                            MinPrice = 1;
                            MaxPrice = 300;
                            break;
                        case "2":
                            MinPrice = 300;
                            MaxPrice = 800;
                            break;
                        case "3":
                            MinPrice = 800;
                            MaxPrice = 1600;
                            break;
                        case "4":
                            MinPrice = 1600;
                            MaxPrice = 3000;
                            break;
                        case "5":
                            MinPrice = 3000;
                            MaxPrice = 6000;
                            break;
                        case "6":
                            MinPrice = 6000;
                            PriceType = "以上";
                            break;
                    }
                    TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByPriceRange(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, MinPrice, MaxPrice, false,true);
                    if (MinPrice != null || MaxPrice != null)
                    {
                        StringBuilder strPrice = new StringBuilder();
                        strPrice.AppendFormat("“价格在{0}{3}{1}{2}”", MinPrice == null ? "" : MinPrice.ToString(), MaxPrice == null ? "" : MaxPrice.ToString(), PriceType, PriceType == "之间" ? "-" : "");
                        ErrorInfo += strPrice.ToString();
                        strPrice = null;
                       
                    }
                }
                //按行程天数
                else if (SearchType.Equals("Day", StringComparison.OrdinalIgnoreCase))
                {
                    int DaysType = 0;
                    if (intSearchId == 5)
                    {
                        DaysType = 1;
                    }
                    if (intSearchId == 8) //首页5日游查询
                    {
                        intSearchId = 5;
                    }

                    TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByTourDays(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, DaysType, intSearchId, false,true);
                    if (intSearchId > 0)
                    {
                        StringBuilder strDays = new StringBuilder();
                        strDays.AppendFormat("“{0}”", intSearchId == 5 ? "5日游及以上" : intSearchId.ToString() + "日游");
                        ErrorInfo += strDays.ToString();
                        strDays = null;
                        
                    }
                }
                //按公司单位
                else if (SearchType.Equals("Company", StringComparison.OrdinalIgnoreCase))
                {
                    TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetToursByCompanyId(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, SearchId, false,true);
                }
                else if (SearchType.Equals("More", StringComparison.OrdinalIgnoreCase))
                {
                    string RouteName = Utils.InputText(Request.QueryString["RouteName"]);
                    int intDays = 0;
                    string strDays = Utils.InputText(Request.QueryString["Days"]);
                    if (!string.IsNullOrEmpty(strDays) && EyouSoft.Common.Function.StringValidate.IsInteger(strDays))
                    {
                        intDays = Convert.ToInt32(strDays);
                    }
                    int? SearchDay = null;
                    if (intDays > 0)
                    {
                        SearchDay = intDays;

                    }
                    string CompanyName = Utils.InputText(Request.QueryString["CompanyName"]);
                    DateTime? StartDate = null;
                    string strStartDate = Utils.InputText(Request.QueryString["StartDate"]);
                    if (!string.IsNullOrEmpty(strStartDate) && EyouSoft.Common.Function.StringValidate.IsDateTime(strStartDate))
                    {
                        StartDate = Convert.ToDateTime(strStartDate);
                    }
                    DateTime? EndDate = null;
                    string strEndDate = Utils.InputText(Request.QueryString["EndDate"]);
                    if (!string.IsNullOrEmpty(strEndDate) && EyouSoft.Common.Function.StringValidate.IsDateTime(strEndDate))
                    {
                        EndDate = Convert.ToDateTime(strEndDate);
                    }
                    TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTours(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, RouteName, SearchDay, CompanyName, StartDate, EndDate, false,true);
                    if (!string.IsNullOrEmpty(RouteName))
                    {
                        ErrorInfo += "“" + RouteName + "”";
                        
                    }
                    if (intDays > 0)
                    {
                        ErrorInfo += "“" + intDays + "天”";
                        
                    }
                    if (!string.IsNullOrEmpty(CompanyName))
                    {
                        ErrorInfo += "“" + CompanyName + "”";
                        
                    }
                    if (StartDate != null)
                    {
                        ErrorInfo += "“" + Convert.ToDateTime(StartDate).ToShortDateString() + "”";
                        
                    }
                    if (EndDate != null)
                    {
                        ErrorInfo += "“" + Convert.ToDateTime(EndDate).ToShortDateString() + "”";
                        
                    }
                }
            }
            else
            {
                TourList = EyouSoft.BLL.TourStructure.Tour.CreateInstance().GetTours(PageSize, PageIndex, ref recordCount, CityId, SearchTourArea, false,true);
            }

            if (TourList != null && TourList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;


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

               

                this.repTourList.DataSource = TourList;
                this.repTourList.DataBind();
            }
            else
            {
                strEmpty.Append("<div class=\"noresult\" >");
                if (!string.IsNullOrEmpty(SearchType))
                {
                    //按主题类型
                    if (SearchType.Equals("Theme", StringComparison.OrdinalIgnoreCase))
                    {
                        string RouteTypeName = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetFieldNameById(intSearchId, EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
                        if (!string.IsNullOrEmpty(RouteTypeName))
                        {
                            ErrorInfo += "“" + RouteTypeName + "”";
                            
                        }
                    }

                }
                strEmpty.Append("<div class=\"p1\"><h3><span class='timg'><img src='" + ImageServerUrl + "/images/UserPublicCenter/paoqian.gif' /></span><span>抱歉，在" + ErrorInfo + "没有找到相关的结果!</span></h3></div>");
                strEmpty.Append("<div class=\"p2\"><a href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/routeagency/addquicktour.aspx\"><img src=\"" + ImageServerUrl + "/images/new2011/xianlu/nobg_07.gif\" alt=\"我要添加产品\"/></a></div>");
                strEmpty.Append("<div class=\"p3\"><b>建议你：</b><br />");
                strEmpty.Append("·适当删减或更改关键词试试<br /> ·看看输入的文字是否有错别字<br />·如关键词中含有城市名，去掉城市名试试 <br />·换个分类搜索试试");
                strEmpty.Append("</div>");
                strAllEmpty = strEmpty.ToString();
                this.ExporPageInfoSelect1.Visible = false;
                this.repTourList.EmptyText = strAllEmpty;
                
            }
            TourList = null;

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

            string Message = "";
            //是否开通高级网店
            if (isOpenHighShop)
            {
                Message = string.Format("{0}</p>", "<a href='" + GoToUrl + "' target='_blank'><img src=\"" + ImageServerPath + "/images/UserPublicCenter/shopico.gif\" /></a>");
            }
            else
            {
                Message = string.Format("{0}</p>", "");
            }
            strCompany = string.Format("<p>供应商：<a href=\"{0}\" target='_blank'><span>{1}</span></a>{2}", GoToUrl, CompanyName, Message);

            

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
