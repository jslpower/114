using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.IBLL.CompanyStructure;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 我的散拼计划
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-15
    public partial class ScatteredFightPlan : BackPage
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
            #region Ajax请求
            //Ajax请求
            string setType = Utils.GetQueryStringValue("SetType");
            switch (setType)
            {
                case "RecommendType":
                    //推荐状态
                    SetRecommendType();
                    break;
                case "PowderTourStatus":
                    //团队状态
                    SetPowderTourStatus();
                    break;
                case "Del":
                    //删除
                    Del();
                    break;
            }
            #endregion
            Key = "ScatteredFightPlan" + Guid.NewGuid().ToString();
            //配置用户控件
            ILine1.Key = Key;
            ILine1.SelectFunctionName = "ScatteredFightPlan.GetList";
            ILine1.UserId = SiteUserInfo.ID;
            //绑定专线下拉
            BindZX();
            //绑定推荐类型
            BindRecommendType();
            //绑定团队状态
            BindPowderTourStatus();
            if (!IsPostBack)
            {
                InitPage();
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
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数赋值
            //专线Id
            queryModel.AreaId = Utils.GetInt(Utils.GetQueryStringValue("lineId"), 0);
            ILine1.CheckedId = queryModel.AreaId.ToString();
            ddl_ZX.SelectedValue = queryModel.AreaId.ToString();
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.TourKey.Length > 0 ? queryModel.TourKey : string.Empty;
            ////专线类型
            //queryModel.AreaType = null;
            //if (Utils.GetIntSign(Utils.GetQueryStringValue("lineType"), -1) >= 0)
            //{
            //    queryModel.AreaType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("lineType"));
            //}
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeS"), DateTime.MinValue);
            txt_goTimeS.Value = queryModel.LeaveDate == DateTime.MinValue ? string.Empty : queryModel.LeaveDate.ToString("yyyy-MM-dd");
            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"), DateTime.MinValue);
            txt_goTimeE.Value = queryModel.EndLeaveDate == DateTime.MinValue ? string.Empty : queryModel.EndLeaveDate.ToString("yyyy-MM-dd");
            #endregion
            IList<MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
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
                this.ExportPageInfo1.UrlParams.Add("lineId", queryModel.AreaId.ToString());
                this.ExportPageInfo1.UrlParams.Add("lineType", Utils.GetQueryStringValue("lineType"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", queryModel.LeaveDate.ToString("yyyy-MM-dd"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", queryModel.EndLeaveDate.ToString("yyyy-MM-dd"));
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);
            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
            }

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
        /// 绑定团队状态
        /// </summary>
        private void BindPowderTourStatus()
        {
            //获取团队状态
            rpt_powderTourStatus.DataSource = EnumObj.GetList(typeof(PowderTourStatus)); ;
            rpt_powderTourStatus.DataBind();

        }
        /// <summary>
        /// 设置推荐类型
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private void SetRecommendType()
        {
            Response.Clear();
            Response.Write(BPowderList.CreateInstance().UpdatePowderRecommend((RecommendType)Utils.GetInt(Utils.GetQueryStringValue("SetValue")), Utils.GetQueryStringValue("ids").Split(',')).ToString());
            Response.End();
        }
        /// <summary>
        /// 设置团队类型
        /// </summary>
        private void SetPowderTourStatus()
        {
            Response.Clear();
            Response.Write(BPowderList.CreateInstance().UpdateStatus((PowderTourStatus)Utils.GetInt(Utils.GetQueryStringValue("SetValue")), Utils.GetQueryStringValue("ids").Split(',')).ToString());
            Response.End();
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
