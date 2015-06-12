using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 历史团队
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12.21
    public partial class HistoryTeam : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.专线_线路管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            Key = "HistoryTeam" + Guid.NewGuid().ToString();
            if (Utils.GetQueryStringValue("isDel").Length > 0)
            {
                Del();
            }
            #region 配置用户控件
            ILine1.Key = Key;
            ILine1.SelectFunctionName = "HistoryTeam.GetList()";
            ILine1.UserId = SiteUserInfo.ID;
            #endregion
            InitPage();
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
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            //绑定专线下拉
            BindZX();
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            ILine1.CheckedId = queryModel.AreaId.ToString();
            ddl_ZX.SelectedValue = queryModel.AreaId.ToString();
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.TourKey.Length > 0 ? queryModel.TourKey : string.Empty;
            //专线类型
            queryModel.AreaType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            {
                queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            }
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeS"), DateTime.MinValue);
            txt_goTimeS.Value = queryModel.LeaveDate == DateTime.MinValue ? string.Empty : queryModel.LeaveDate.ToString("yyyy-MM-dd");
            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"), DateTime.MinValue);
            txt_goTimeE.Value = queryModel.EndLeaveDate == DateTime.MinValue ? string.Empty : queryModel.EndLeaveDate.ToString("yyyy-MM-dd");
            queryModel.RouteId = Utils.GetQueryStringValue("routeId");
            #endregion
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetHistoryList(
                pageSize,
                pageCurrent,
                ref recordCount,
                SiteUserInfo.CompanyID,
                queryModel);

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
                this.ExportPageInfo1.UrlParams.Add("routeId", queryModel.RouteId);
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            string[] tourIds = Utils.GetQueryStringValue("ids").Split(',');
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().DeletePowder(tourIds));
            Response.End();

        }
    }
}
