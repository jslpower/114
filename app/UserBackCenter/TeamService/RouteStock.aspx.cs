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
using EyouSoft.Common.Control;
//using EyouSoft.BLL.TourStructure;
//using EyouSoft.Model.TourStructure;
using EyouSoft.Common;
namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 页面功能：组团服务采购
    /// 开发人：xuty 开发时间：2010-6-24
    /// </summary>
    public partial class RouteStock : BackPage
    {
        protected int allRoute;//全部线路
        protected int longRoute;//国内长线
        protected int shortRoute;//周边游
        protected int exitRoute;//出境线路

        protected string allStyle;
        protected string longStyle;
        protected string shortStyle;
        protected string exitStyle;

        protected int pageSize=10;
        protected int pageIndex=1;
        protected int recordCount;
        protected string method;
        protected bool haveUpdate = true;
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        EyouSoft.IBLL.TourStructure.ITour tourBll;//团队业务逻辑
        EyouSoft.IBLL.SystemStructure.ISysCity cityBll;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.组团_线路采购预定))
            {
                Utils.ResponseNoPermit();
                return;
            }
          
            //是否设置目录
            if (EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance().GetAllFavorCount(SiteUserInfo.CompanyID) < 1)
            {
                rs_rpt_tourList.EmptyText = "<tr><td style='text-align:center'>您尚未添加任何产品供应商，请先<a href='javascript:void(0);' onclick='javascript:$(\"#divDirectoryset\").find(\"a\").click();return false'>挑选专线</a>！</td></tr>";
                ListItem cityItem = new ListItem("选择全部出港城市", "");
                rs_selCity.Items.Insert(0, cityItem);
                this.rs_ExportPageInfo1.Visible = false;
                return;
            }
            method = Utils.GetQueryStringValue("method");
            cityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            tourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            BindTourData();//绑定团队列表
            LoadAreaTypeStateInfo();//加载筛选条件信息
            BindPage();//设置分页
            //恢复搜索条件
            if (method == "cityfilt")
            {
                rs_selCity.Value = Utils.GetQueryStringValue("cityid");// 出港城市
            }
            else if (method == "search")
            {
                rs_selRouteCompany.Value =Utils.InputText(Server.UrlDecode(Request.QueryString["companyid"]??""));
                rs_txtEndTime.Value = Utils.InputText(Server.UrlDecode(Request.QueryString["endtime"]??""));
                rs_txtRouteName.Value = Utils.InputText(Server.UrlDecode(Request.QueryString["routename"]??""));
                rs_txtStartTime.Value = Utils.InputText(Server.UrlDecode(Request.QueryString["starttime"] ?? ""));
                rs_selRouteArea.Value = Utils.InputText(Server.UrlDecode(Request.QueryString["routearea"] ?? ""));
            }
        }

        #region 绑定团队列表
        protected void BindTourData()
        {
            int? cityId = Utils.GetIntNull(Utils.GetQueryStringValue("cityid"));//出港城市
            string strAreaType = Utils.GetQueryStringValue("areatype");//区域类型
            switch (strAreaType)
            {
                case "0":
                    longStyle = "style='color:red'";
                    break;
                case "1":
                    exitStyle = "style='color:red'";
                    break;
                case "2":
                    shortStyle="style='color:red'";
                    break;
                case "3":
                    allStyle = "style='color:red'";
                    break;
                   
            }
            EyouSoft.Model.SystemStructure.AreaType? areaType=(strAreaType==""||strAreaType=="3")?null: new System.Nullable<EyouSoft.Model.SystemStructure.AreaType>((EyouSoft.Model.SystemStructure.AreaType)Enum.Parse(typeof(EyouSoft.Model.SystemStructure.AreaType),strAreaType));
            int? areaId=Utils.GetIntNull(Utils.InputText(Server.UrlDecode(Request.QueryString["routearea"]??"")));//线路区域
            string routeName =Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["routename"]??"")),null);//线路名称
            string companyId = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["companyid"]??"")),null);//专线商
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["starttime"]??"")),new System.Nullable<DateTime>());//出团开始日期
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["endtime"]??"")),new System.Nullable<DateTime>());//出团截止日期
            IList<EyouSoft.Model.TourStructure.TourInfo> tourInfoList = tourBll.GetAttentionTours(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyID, cityId, areaId, routeName, companyId, startTime, endTime,areaType);
            if (tourInfoList.Count > 0)
            {
                rs_rpt_tourList.DataSource = tourInfoList;
                rs_rpt_tourList.DataBind();
            }
            else
            {
                rs_rpt_tourList.EmptyText = "<tr><td style='text-align:center;'>暂无团队信息</td></tr>";
                this.rs_ExportPageInfo1.Visible = false;
            }
            tourInfoList = null;
        }
        #endregion

        #region 初始化筛选和查询条件
        protected void LoadAreaTypeStateInfo()
        {   
            //绑定出港城市
            rs_selCity.DataTextField = "CityName";
            rs_selCity.DataValueField = "CityId";
            rs_selCity.DataSource = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList(SiteUserInfo.ProvinceId, -1, null, true, null);
            rs_selCity.DataBind();
            ListItem cityItem = new ListItem("选择全部出港城市", "");
            rs_selCity.Items.Insert(0, cityItem);
            //绑定类型区域团队统计信息
            EyouSoft.Model.TourStructure.AreaTypeStatInfo atsi = tourBll.GetAttentionTourByAreaTypeStats(SiteUserInfo.CompanyID);
            if (atsi != null)
            {
                allRoute = atsi.All;
                longRoute = atsi.Long;
                shortRoute = atsi.Short;
                exitRoute = atsi.Exit;
            }
            //绑定线路区域
            EyouSoft.IBLL.CompanyStructure.ICompanyFavor favorBll = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance();
            rs_selRouteArea.DataTextField = "AreaName";
            rs_selRouteArea.DataValueField = "AreaId";
            rs_selRouteArea.DataSource = favorBll.GetAllFavorArea(SiteUserInfo.CompanyID);
            rs_selRouteArea.DataBind();
            ListItem areaItem=new ListItem("请选择","");
            rs_selRouteArea.Items.Insert(0,areaItem);
            //绑定专线商
            rs_selRouteCompany.DataTextField = "CompanyName";
            rs_selRouteCompany.DataValueField = "ID";
            rs_selRouteCompany.DataSource= favorBll.GetListCompany(SiteUserInfo.CompanyID);
            rs_selRouteCompany.DataBind();
            ListItem areaItem1 = new ListItem("请选择", "");
            rs_selRouteCompany.Items.Insert(0, areaItem1);
        }
        #endregion

      

        #region 设置分页
        protected void BindPage()
        {
            this.rs_ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            switch(method)
            {   
                case "cityfilt":
                    this.rs_ExportPageInfo1.UrlParams.Add("method", method);
                    this.rs_ExportPageInfo1.UrlParams.Add("cityid", Utils.GetQueryStringValue("cityid"));
                    break;
                case "areatype":
                    this.rs_ExportPageInfo1.UrlParams.Add("method", method);
                    this.rs_ExportPageInfo1.UrlParams.Add("areatype", Utils.GetQueryStringValue("areatype"));
                break;
                case "search":
                this.rs_ExportPageInfo1.UrlParams.Add("method", method);
                this.rs_ExportPageInfo1.UrlParams.Add("routearea",Utils.InputText(Server.UrlDecode(Request.QueryString["routearea"]??"")));
                this.rs_ExportPageInfo1.UrlParams.Add("routename", Utils.InputText(Server.UrlDecode(Request.QueryString["routename"]??"")));
                this.rs_ExportPageInfo1.UrlParams.Add("companyid", Utils.InputText(Server.UrlDecode(Request.QueryString["companyid"]??"")));
                this.rs_ExportPageInfo1.UrlParams.Add("starttime", Utils.InputText(Server.UrlDecode(Request.QueryString["starttime"] ?? "")));
                this.rs_ExportPageInfo1.UrlParams.Add("endtime", Utils.InputText(Server.UrlDecode(Request.QueryString["endtime"] ?? "")));
                break;
            }
            this.rs_ExportPageInfo1.intPageSize = pageSize;
            this.rs_ExportPageInfo1.CurrencyPage = pageIndex;
            this.rs_ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion

       
    }
   
}
