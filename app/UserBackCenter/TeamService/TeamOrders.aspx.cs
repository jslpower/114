using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.BLL.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 团队订单管理(组团地接专线公用)
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-26
    public partial class TeamOrders : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 线路来源
        /// </summary>
        protected string intRouteSource = string.Empty;
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource? routeSource = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (Utils.GetQueryStringValue("routeSource").Length > 0)
            {
                routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1);
                intRouteSource = Utils.GetQueryStringValue("routeSource");
            }
            #region 权限判断
            if (routeSource == RouteSource.专线商添加)
            {
                if (!CheckGrant(TravelPermission.专线_线路团队订单管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            else if (routeSource == RouteSource.地接社添加)
            {
                if (!CheckGrant(TravelPermission.地接_线路团队))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            else
            {
                if (!CheckGrant(TravelPermission.组团_线路团队订单管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            #endregion
            Key = "ScatterPlanP" + Guid.NewGuid().ToString();
            InitPage();

        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {

            BindPowderOrderStatus();
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            bool isSelected = false;
            MTourListSearch queryModel = new MTourListSearch();
            #region 查询实体
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord"); //status
            //订单状态
            queryModel.TourOrderStatus = null;
            //订单状态
            if (Utils.GetQueryStringValue("status").Length > 0 && Utils.GetQueryStringValue("status") != "-1")
            {
                string[] status = Utils.GetQueryStringValue("status").Split(',');
                var tourOrderStatus = new TourOrderStatus?[status.Length];
                for (int i = 0; i < status.Length; i++)
                {
                    if (Utils.GetInt(status[i]) > 0)
                        tourOrderStatus[i] = (TourOrderStatus)Utils.GetInt(status[i]);
                    if (!isSelected && sel_status.Items.FindByValue(status[i]) != null)
                    {
                        sel_status.Items.FindByValue(status[i]).Selected = true;
                        isSelected = true;
                    }
                }
                queryModel.OrderStatus = tourOrderStatus;
            }
            //出团时间
            queryModel.SLeaveDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeS"));
            //出团时间
            queryModel.ELeaveDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("goTimeE"));

            txt_keyWord.Value = queryModel.TourKey;
            txt_goTimeS.Value = queryModel.SLeaveDate.HasValue ? queryModel.SLeaveDate.Value.ToShortDateString() : string.Empty;
            txt_goTimeE.Value = queryModel.ELeaveDate.HasValue ? queryModel.ELeaveDate.Value.ToShortDateString() : string.Empty;

            #endregion
            IList<MTourList> list = new List<MTourList>();
            switch (routeSource)
            {
                case RouteSource.地接社添加:
                    list = BTourList.CreateInstance().GetDJList(pageSize, pageCurrent, ref recordCount, SiteUserInfo.CompanyID, queryModel);
                    break;
                case RouteSource.专线商添加:
                    list = BTourList.CreateInstance().GetZXList(pageSize, pageCurrent, ref recordCount, SiteUserInfo.CompanyID, queryModel);
                    break;
                default:
                    list = BTourList.CreateInstance().GetZTList(pageSize, pageCurrent, ref recordCount, SiteUserInfo.CompanyID, queryModel);
                    break;
            }

            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
                ExportPageInfo1.Visible = true;
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.SLeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.ELeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
                this.ExportPageInfo1.UrlParams.Add("routeSource", routeSource.ToString());
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }
        }
        /// <summary>
        /// 绑定订单状态
        /// </summary>
        private void BindPowderOrderStatus()
        {
            //获取推荐类型列表
            List<EnumObj> ls = EnumObj.GetList(typeof(TourOrderStatus));

            sel_status.DataTextField = "Text";
            sel_status.DataValueField = "Value";
            sel_status.AppendDataBoundItems = true;
            sel_status.DataSource = ls;
            sel_status.DataBind();

            rpt_powderOrderStatus.DataSource = ls;
            rpt_powderOrderStatus.DataBind();
        }
    }
}
