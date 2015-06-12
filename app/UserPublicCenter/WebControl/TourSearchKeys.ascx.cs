using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Control;
namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 线路下的搜索条件控件 
    /// </summary>
    public partial class TourSearchKeys : System.Web.UI.UserControl
    {
        private bool _isDefault = true;
        /// <summary>
        /// 是否是首页的团队查询
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        private bool _isRoute = false;

        /// <summary>
        /// 是否是线路栏目首页
        /// </summary>
        public bool IsRoute
        {
            get { return _isRoute; }
            set { _isRoute = value; }
        }
        protected string strUrl = "/TourManage/DefaultTourList.aspx";
        protected string ImageServerPath = "";
        protected int TourAreaId = 0;
        protected int CityId = 0;
        protected string strUrlParms = "";
        /// <summary>
        /// 主题游
        /// </summary>
        protected string AllTourTheme = "";
        /// <summary>
        /// 月份
        /// </summary>
        protected string AllMonth = string.Empty;

        #region 价格和天数
        protected string SearchType = "";
        protected string SetId = "";
        protected int priceid = 0;
        protected int daysid = 0;
        protected string Month = "";
        protected string City = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            CityId = page.CityId;
            if (!IsDefault)
            {
                strUrl = "/TourManage/TourList.aspx";
            }
            //if (IsRoute)
            //{
            //    this.tbMoreSearch.Visible = false;
            //}

            TourAreaId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["TourAreaId"]);
            if (CityId > 0)
            {
                strUrlParms = "&CityId=" + CityId;
            }

            if (!Page.IsPostBack)
            {
                BindTourThemeList();
                BindMonthList();
                SearchType = EyouSoft.Common.Utils.GetQueryStringValue("SearchType");
                SetId = EyouSoft.Common.Utils.GetQueryStringValue("Id");
                City = EyouSoft.Common.Utils.GetQueryStringValue("City");
                if (!string.IsNullOrEmpty(SearchType))
                {

                    if (SearchType != "More" && !string.IsNullOrEmpty(SetId))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetSearchItem('" + SearchType + "','" + SetId + "')", true);
                    }
                    else
                    {
                        if (Utils.GetQueryStringValue("Price") != "0")
                        {
                            priceid = Utils.GetInt(Utils.GetQueryStringValue("Price"));
                        }
                        if (Utils.GetQueryStringValue("Days") != "0")
                        {
                            daysid = Utils.GetInt(Utils.GetQueryStringValue("Days"));
                        }
                        if (Request.QueryString["keyWord"] != null)
                            this.txtRouteName.Value = Request.QueryString["keyWord"].ToString() == "" ? "线路,特色,途径区域" : Request.QueryString["keyWord"];
                        txtRouteName.Style["color"] = txtRouteName.Value == "线路,特色,途径区域" ? "#ccc" : "#000";
                        if (Request.QueryString["City"] != null)
                            this.txtCity.Value = Request.QueryString["City"].ToString() == "" ? "如:杭州" : Request.QueryString["City"];
                        if (txtCity.Value != "如:杭州")
                        {
                            txtCity.Style["color"] = "#000";
                        }
                        //days = Request.QueryString["Days"];
                        //if (!string.IsNullOrEmpty(days) && EyouSoft.Common.Function.StringValidate.IsInteger(days) && Convert.ToInt32(days) > 0)
                        //{
                        //    this.txtDays.Value = days;
                        //}
                        //this.txtCompanyName.Value = Request.QueryString["CompanyName"];
                        this.txtStartDate.Value = Request.QueryString["StartDate"];
                        this.txtEndDate.Value = Request.QueryString["EndDate"];
                    }
                }
            }

        }

        /// <summary>
        /// 绑定所有月份
        /// </summary>
        protected void BindMonthList()
        {
            string SearchType = Request.QueryString["SearchType"];
            int SetId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Id"]);
            string strAllstyle = "style=\"color: red;font-weight:bold\"";
            StringBuilder MonthString = new StringBuilder();
            if (SearchType == "Month" && SetId > 0)
            {
                strAllstyle = "";
            }
            MonthString.AppendFormat("<a href='{0}' {1}>全部</a>", EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId, CityId), SearchType != "Month" ? strAllstyle : "");
            string thisStyle = "";
            for (int i = 0; i < 6; i++)
            {
                if (SearchType == "Month" && SetId >= 0)
                {
                    thisStyle = "style=\"color:red;font-weight:bold\"";
                }
                MonthString.AppendFormat("<a href='{0}' {1}>{2}</a>", EyouSoft.Common.URLREWRITE.Tour.GetTourMonthUrl(TourAreaId, i, CityId), SearchType == "Month" && SetId == i ? thisStyle : "", DateTime.Now.AddMonths(i).ToString("yyyy年MM月"));
            }
            Month = MonthString.ToString();
        }
        /// <summary>
        /// 绑定所有主题游
        /// </summary>
        protected void BindTourThemeList()
        {
            StringBuilder strAllList = new StringBuilder();

            IList<EyouSoft.Model.SystemStructure.SysFieldBase> ThemeList = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetSysFieldBaseList(EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
            string SearchType = Request.QueryString["SearchType"];
            int SetId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Id"]);
            int themeid = Utils.GetInt(Utils.GetQueryStringValue("ThemeName"));
            string strAllstyle = "style=\"color:red;font-weight:bold\"";
            if (SearchType == "Theme" && SetId > 0)
            {
                strAllstyle = "";
            }
            if (SearchType == "More" && themeid > 0)
            {
                strAllstyle = "";
            }
            if (ThemeList.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysFieldBase one in ThemeList)
                {
                    string strthisThem = "";
                    if (SearchType == "Theme" && SetId == one.FieldId)
                    {
                        strthisThem = "style=\"color:red;font-weight:bold\"";
                    }
                    if (SearchType == "More" && themeid == one.FieldId)
                    {
                        strthisThem = "style=\"color:red;font-weight:bold\"";
                    }
                    //根据规则生成新的URL(主题)
                    if (!IsDefault)
                    {
                        strAllList.AppendFormat("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourThemeUrl(TourAreaId, one.FieldId, CityId) + "\"  {1}><nobr>{0}</nobr></a>  ", one.FieldName, strthisThem);
                    }
                    else
                    {
                        strAllList.AppendFormat("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourThemeUrl(TourAreaId, one.FieldId, CityId) + "\"  {1}><nobr>{0}</nobr></a>  ", one.FieldName, strthisThem);
                    }
                }
            }
            ThemeList = null;

            if (!string.IsNullOrEmpty(strAllList.ToString()))
            {
                //根据规则生成新的URL(线路)
                if (!IsDefault)
                {
                    AllTourTheme += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId, CityId) + "\" {0} ><nobr>全部</nobr></a>  ", strAllstyle) + strAllList.ToString();
                }
                else
                {
                    AllTourTheme += string.Format("<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(TourAreaId, CityId) + "\" {0} ><nobr>全部</nobr></a>  ", strAllstyle) + strAllList.ToString();
                }
                AllTourTheme = AllTourTheme.Substring(0, AllTourTheme.Length - 2);
            }
            else
            {
                //根据规则生成新的URL(线路)
                if (!IsDefault)
                {
                    AllTourTheme = "<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(TourAreaId, CityId) + "\" style=\"color:red;font-weight:bold\"><nobr>全部</nobr></a> ";
                }
                else
                {
                    AllTourTheme = "<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(TourAreaId, CityId) + "\" style=\"color:red;font-weight:bold\"><nobr>全部</nobr></a> ";
                }

            }
        }
    }
}