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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
using Newtonsoft.Json;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 线路选择页面 
    /// 罗丽娥   2010-06-25
    /// ----------------------
    /// 修改人：张新兵，修改时间：2011-1-17
    /// 修改内容：增加 线路名称的 搜索
    /// </summary>
    public partial class RouteList : EyouSoft.Common.Control.BasePage
    {
        protected string tmpReleaseType = string.Empty;
        protected string strStandardPlan = string.Empty;
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        private string CompanyId = "0", UserID = "0";
        private int intPageSize = 30, CurrencyPage = 1;
        protected string ContainerID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断是否已经登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage(Domain.UserBackCenter + "/Default.aspx");
            }
            else
            {
                if (SiteUserInfo != null)
                {
                    UserInfoModel = SiteUserInfo;
                    CompanyId = UserInfoModel.CompanyID;
                    UserID = UserInfoModel.ID;
                }
            }

            tmpReleaseType = Request.QueryString["ReleaseType"];   // 判断是快速发布还是标准发布
            ContainerID = Utils.InputText(Request.QueryString["ContainerID"]);//父页面容器ID
            

            if (!Page.IsPostBack)
            {
                InitRouteArea();
                InitRouteList();
            }
            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Utils.InputText(Request.QueryString["flag"]);
                if (flag.Equals("select", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(SelectRoute());
                    Response.End();
                }
            }
        }

        #region 初始化线路区域
        /// <summary>
        /// 初始化线路区域
        /// </summary>
        private void InitRouteArea()
        {
            int tmpAreaId = Utils.GetInt(Request.QueryString["AreaID"]);//当前选择的线路区域
            // 根据公司ID获取线路区域
            string ReturnVal = "<a "+(tmpAreaId<=0?"style='color:red;'":string.Empty)+" href=\"" + Request.ServerVariables["SCRIPT_NAME"] + "?ReleaseType=" + tmpReleaseType + "&ContainerID=" + ContainerID + "\">所有专线</a>&nbsp;&nbsp;|&nbsp;&nbsp;";
            EyouSoft.IBLL.SystemStructure.ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysArea> list = bll.GetSysAreaList(UserInfoModel.AreaId);
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.SystemStructure.SysArea model in list)
                {
                    ReturnVal += "<a "+(tmpAreaId==model.AreaId?"style='color:red;'":string.Empty)+"  href=\"" + Request.ServerVariables["SCRIPT_NAME"].ToString() + "?AreaID=" + model.AreaId.ToString() + "&ReleaseType=" + tmpReleaseType + "&ContainerID=" + ContainerID + "\">" + model.AreaName + "</a>&nbsp;&nbsp;|&nbsp;&nbsp;";
                }
            }
            list = null;
            bll = null;
            this.lblRouteClass.Text = ReturnVal;
        }
        #endregion

        #region 初始化线路列表
        /// <summary>
        /// 初始化线路列表
        /// </summary>
        private void InitRouteList()
        {
            EyouSoft.Model.TourStructure.ReleaseType ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Quick;
            int intRecordCount = 0;
            int[] AreaId = null;
            int tmpAreaId = 0;//线路区域ID
            string routeName = null;//线路名称
            string userId = string.Empty;//用户编号
            int routeDays = 0;//线路天数
            string contactName = null;//线路联系人


            //线路区域ID
            tmpAreaId = Utils.GetInt(Request.QueryString["AreaID"]);
            if (tmpAreaId > 0)
            {
                AreaId = new int[] { tmpAreaId };
            }

            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);//当前分页索引

            //线路类型
            if (tmpReleaseType == "Quick")
            {
                ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Quick;
            }
            else
            {
                ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Standard;
            }

            //线路名称
            if (Request.QueryString["rn"] != null)
            {
                routeName = Utils.GetQueryStringValue("rn");
            }

            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> list = bll.GetRoutes(
                intPageSize, 
                CurrencyPage, 
                ref intRecordCount, 
                CompanyId,
                userId,
                routeName,
                routeDays, 
                contactName, 
                AreaId, 
                ReleaseType);
            if (list != null && list.Count > 0)
            {
                this.dlRouteList.DataSource = list;
                this.dlRouteList.DataBind();
            }
            else {
                this.pnlNoData.Visible = true;
            }
            this.ExportPageInfo2.intPageSize = intPageSize;
            this.ExportPageInfo2.intRecordCount = intRecordCount;
            this.ExportPageInfo2.CurrencyPage = CurrencyPage;
            this.ExportPageInfo2.UrlParams = Request.QueryString;
            this.ExportPageInfo2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
           
            list = null;
            bll = null;
        }
        #endregion

        #region 选择线路并将线路信息初始化到页面
        protected string SelectRoute()
        {
            string RouteID = Server.UrlDecode(Utils.InputText(Request.QueryString["RouteID"]));
            string str = string.Empty;
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            EyouSoft.Model.TourStructure.RouteBasicInfo model = bll.GetRouteInfo(RouteID);    // 根据线路编号获取线路信息
            if (model != null)
            {
                str = JsonConvert.SerializeObject(model);
            }
            model = null;
            bll = null;
            return str;
        }
        #endregion
    }
}
