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
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;
using System.Web.Services;
using EyouSoft.IBLL.CompanyStructure;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 线路列表
    /// 罗丽娥   2010-06-28
    /// </summary>
    /// 专线(地接)线路库
    /// 柴逸宁 2011-12-16
    public partial class RouteView : EyouSoft.Common.Control.BackPage
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 分页变量
        /// </summary>
        private int intPageSize = 15, CurrencyPage = 1;
        /// <summary>
        /// 线路行程库权限
        /// </summary>
        protected bool IsRouteGrant = true;
        /// <summary>
        /// 网页唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource routeSource;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 权限判断
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }

            if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.专线商添加)
            {
                if (!CheckGrant(TravelPermission.专线_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.地接社添加)
            {
                if (!CheckGrant(TravelPermission.地接_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            #endregion
            #region Ajax请求
            //Ajax请求
            string setType = Utils.GetQueryStringValue("SetType");
            switch (setType)
            {
                case "RecommendType":
                    //推荐状态
                    SetRecommendType();
                    break;
                case "RouteStatus":
                    //上下架状态
                    SetRouteStatus();
                    break;
                case "Del":
                    Del();
                    break;
            }
            #endregion
            Key = "RouteView" + Guid.NewGuid().ToString();
            ILine1.Key = Key;
            ILine1.SelectFunctionName = "RouteView.GetList";
            ILine1.UserId = SiteUserInfo.ID;

            this.Page.Title = "专线线路库";
            BindRecommendType();
            //绑定出发地下拉
            BindGoCity();
            //绑定专线下拉
            BindZX();
            //绑定线路列表
            InitRouteList();
            #region 以前代码
            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_管理栏目))
            //{
            //    Utils.ResponseNoPermit("对不起，您没有专线_管理栏目权限!");
            //    return;
            //}

            //if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路行程库))
            //{
            //    IsRouteGrant = false;
            //}

            //if (!Page.IsPostBack)
            //{
            //    InitRouteList();
            //}


            //if (!String.IsNullOrEmpty(Request.QueryString["flag"]) && Request.QueryString["flag"] == "del")
            //{
            //    string flag = Request.QueryString["flag"].ToString();
            //    if (flag.Equals("del", StringComparison.OrdinalIgnoreCase))
            //    {
            //        string RouteIdList = Server.UrlDecode(Utils.InputText(Request.QueryString["RouteIdList"]));
            //        Response.Clear();
            //        Response.Write(DeleteData(RouteIdList));
            //        Response.End();
            //    }
            //}
            #endregion
        }
        /// <summary>
        /// 绑定推荐类型
        /// </summary>
        private void BindRecommendType()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(RecommendType));
            //去除无这个状态
            typeli.RemoveAt(0);
            rpt_type.DataSource = typeli.Where(obj => obj.Value != ((int)RecommendType.推荐).ToString()).ToList();
            rpt_type.DataBind();
        }
        /// <summary>
        /// 绑定出发地下拉
        /// </summary>
        private void BindGoCity()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(SiteUserInfo.CompanyID);
            if (list != null && list.Count > 0)
            {
                ddl_goCity.AppendDataBoundItems = true;
                ddl_goCity.DataTextField = "CityName";
                ddl_goCity.DataValueField = "CityId";
                ddl_goCity.DataSource = list;
                ddl_goCity.DataBind();
            }
        }
        /// <summary>
        /// 绑定专线下拉
        /// </summary>
        private void BindZX()
        {
            ICompanyUser companyUserBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser companyUserModel = companyUserBLL.GetModel(SiteUserInfo.ID);
            if (companyUserModel != null && companyUserModel.Area != null && companyUserModel.Area.Count > 0)
            {
                ddl_ZX.AppendDataBoundItems = true;
                ddl_ZX.DataTextField = "AreaName";
                ddl_ZX.DataValueField = "AreaId";
                ddl_ZX.DataSource = companyUserModel.Area;
                ddl_ZX.DataBind();
            }
        }
        /// <summary>
        /// 初始化线路列表
        /// </summary>
        private void InitRouteList()
        {
            //记录条数
            int intRecordCount = 0;
            //页码
            CurrencyPage = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            //查询实体
            MRouteSearch queryModel = new MRouteSearch();
            #region 查询参数赋值
            //线路来源,专线，地接
            routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1);
            queryModel.RouteSource = routeSource;
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            ILine1.CheckedId = queryModel.AreaId.ToString();
            ddl_ZX.SelectedValue = queryModel.AreaId.ToString();
            //专线类型
            queryModel.RouteType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            {
                queryModel.RouteType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            }
            //出发城市
            queryModel.StartCity = Utils.GetInt(Utils.GetQueryStringValue("goCity"), 0);
            ddl_goCity.SelectedValue = queryModel.StartCity.ToString();
            //关键字
            queryModel.RouteKey = Utils.GetQueryStringValue("keyWord") == "线路名称" ? string.Empty : Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.RouteKey.Length > 0 ? queryModel.RouteKey : "线路名称";
            #endregion
            IRoute bll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            //线路库列表
            IList<MRoute> list = bll.GetBackCenterList(intPageSize, CurrencyPage, ref intRecordCount, SiteUserInfo.CompanyID, queryModel);
            if (list != null && list.Count > 0)
            {
                //存在列表数据
                rpt_List.DataSource = list;
                rpt_List.DataBind();

                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";

                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goCity", queryModel.StartCity.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.RouteKey);
                this.ExportPageInfo1.UrlParams.Add("routeSource", Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1).ToString());
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

        }
        /// <summary>
        /// 设置推荐类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private void SetRecommendType()
        {
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().UpdateRouteRecommend((RecommendType)Utils.GetInt(Utils.GetQueryStringValue("SetValue")), Utils.GetQueryStringValue("ids").Split(',')).ToString());
            Response.End();
        }
        /// <summary>
        /// 设置上下架状态
        /// </summary>
        private void SetRouteStatus()
        {
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().UpdateRouteStatus((RouteStatus)Utils.GetInt(Utils.GetQueryStringValue("SetValue")), Utils.GetQueryStringValue("ids").Split(',')).ToString());
            Response.End();
        }
        /// <summary>
        /// 删除线路
        /// </summary>
        private void Del()
        {
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().DeleteBackRoute((Utils.GetQueryStringValue("ids"))));
            Response.End();
        }
    }
}
