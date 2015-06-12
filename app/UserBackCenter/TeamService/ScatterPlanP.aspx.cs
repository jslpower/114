using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    ///组团-周边散拼计划
    /// </summary>
    public partial class ScatterPlanP : BackPage
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
        /// 线路区域类型
        /// </summary>
        protected AreaType areaType = AreaType.国内短线;
        /// <summary>
        /// 出发地ID,旅游
        /// </summary>
        protected string startCityId = string.Empty, powderDay = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_线路散客订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            Key = "ScatterPlanP" + Guid.NewGuid().ToString();
            #region 用户控件配置
            ILine1.Key = Key;
            ILine1.SelectFunctionName = "ScatterPlanP.GetList";
            ILine1.Title = "周边线路";
            ILine1.UserId = SiteUserInfo.ID;
            ILine1.SetAreaType = areaType;
            ILine1.SiteUserInfo_CityId = SiteUserInfo.CityId;
            ILine1.IsTongYe = true;
            #endregion
            BindRecommendType();
            BindCity();
            InitPage();

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
            rpt_type.DataSource = typeli;
            rpt_type.DataBind();
        }
        /// <summary>
        /// 绑定城市
        /// </summary>
        private void BindCity()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(SiteUserInfo.CompanyID);
            if (list != null && list.Count > 0)
            {
                rpt_DepartureCity.DataSource = list;
                rpt_DepartureCity.DataBind();

            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),
                recordCount = 0;
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            ILine1.CheckedId = queryModel.AreaId.ToString();
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.TourKey.Length > 0 ? queryModel.TourKey : string.Empty;
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Request.QueryString["goTimeS"], (DateTime.Now + new TimeSpan(1, 0, 0, 0)));
            txt_goTimeS.Value = queryModel.LeaveDate.Date == (DateTime.Now + new TimeSpan(1, 0, 0, 0)).Date ? string.Empty : queryModel.LeaveDate.ToString("yyyy-MM-dd");
            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"), DateTime.MinValue);
            txt_goTimeE.Value = queryModel.EndLeaveDate == DateTime.MinValue ? string.Empty : queryModel.EndLeaveDate.ToString();
            //出发地Id
            startCityId = Utils.GetQueryStringValue("goCityId");
            queryModel.StartCityId = Utils.GetInt(startCityId);
            //出发地Name
            queryModel.StartCityName = Utils.GetQueryStringValue("goCityName");
            txt_goCity.Value = queryModel.StartCityName;
            //推荐类型
            queryModel.RecommendType = null;
            if (Utils.GetIntSign(Utils.GetQueryStringValue("status"), -1) >= 0)
            {
                queryModel.RecommendType = (RecommendType)Utils.GetInt(Utils.GetQueryStringValue("status"));
            }
            //出游天数
            if (Utils.GetIntSign(Utils.GetQueryStringValue("travelDays"), -1) >= 0)
            {
                queryModel.PowderDay = (PowderDay)Utils.GetInt(Utils.GetQueryStringValue("travelDays"));
                powderDay = Utils.GetQueryStringValue("travelDays");
            }
            #endregion
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                pageSize,
                pageCurrent,
                ref recordCount,
                 AreaType.国内短线,
                 SiteUserInfo.CityId,
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
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("status"));
                this.ExportPageInfo1.UrlParams.Add("goCityId", Utils.GetQueryStringValue("goCityId"));
                this.ExportPageInfo1.UrlParams.Add("goCityName", Utils.GetQueryStringValue("goCityName"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDate.ToString("yyyy-MM-dd"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString());
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
                this.ExportPageInfo1.UrlParams.Add("travelDays", Utils.GetQueryStringValue("travelDays"));
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
                ExportPageInfo1.Visible = false;
            }

        }
        /// <summary>
        /// 获取预定打印按钮
        /// </summary>
        /// <returns></returns>
        protected string GetButt(string tourID)
        {
            return "<a href=\"/Order/OrderByTour.aspx?TourID=" + tourID + "\" class=\"basic_btn Order\" value=\"" + tourID + "\"><span>预订</span></a><a href=\"/PrintPage/TeamTourInfo.aspx?TeamId=" + tourID + "\" target=\"_blank\" class=\"basic_btn\"><span>打印</span></a>";
        }
    }
}
