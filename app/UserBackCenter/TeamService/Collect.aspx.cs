using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.BLL.CompanyStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-我的收藏
    /// </summary>
    /// 创建人：柴逸宁
    /// 2012-2-8
    public partial class Collect : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty, isShowPrice = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_我的收藏管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            Key = "Collect" + Guid.NewGuid().ToString();
            if (!IsPostBack)
            {
                isShowPrice = Utils.GetQueryStringValue("isShowPrice");
                BindICollect();
                BindGoCity();
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            //分页参数
            int pageSize = 15,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1),
                recordCount = 0;

            MPowderSearch queryModel = new MPowderSearch();
            #region 查询参数
            //关键字
            queryModel.TourKey = Utils.GetQueryStringValue("keyWord");
            txt_keyWord.Value = queryModel.TourKey;
            //专线商
            queryModel.Publishers = Utils.GetQueryStringValue("publishers");
            ddl_iCollect.SelectedValue = queryModel.Publishers;
            //出发地Id
            queryModel.StartCityId = Utils.GetInt(Utils.GetQueryStringValue("goCityId"));
            ddl_goCity.SelectedValue = Utils.GetQueryStringValue("goCityId");
            //出团时间
            queryModel.LeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeS"));
            txt_goTimeS.Value = Utils.GetQueryStringValue("goTimeS");
            //回团时间
            queryModel.EndLeaveDate = Utils.GetDateTime(Utils.GetQueryStringValue("goTimeE"));
            txt_goTimeE.Value = Utils.GetQueryStringValue("goTimeE");
            #endregion
            IList<MPowderList> list = BPowderList.CreateInstance().GetCollectionPowder(
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


                this.ExportPageInfo1.UrlParams.Add("goCityId", Utils.GetQueryStringValue("goCityId"));
                this.ExportPageInfo1.UrlParams.Add("publishers", Utils.GetQueryStringValue("publishers"));
                this.ExportPageInfo1.UrlParams.Add("goTimeS", Utils.GetQueryStringValue("goTimeS"));
                this.ExportPageInfo1.UrlParams.Add("goTimeE", Utils.GetQueryStringValue("goTimeE"));
                this.ExportPageInfo1.UrlParams.Add("keyWord", queryModel.TourKey);


            }
            else
            {
                //不存在列表数据
                pnlNodata.Visible = true;
                ExportPageInfo1.Visible = false;
            }
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
                //rpt_DepartureCity.DataSource = list;
                //rpt_DepartureCity.DataBind();

            }
        }
        /// <summary>
        /// 绑定我的收藏专线商
        /// </summary>
        private void BindICollect()
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> ls = CompanyFavor.CreateInstance().GetListCompany(SiteUserInfo.CompanyID);
            if (ls != null && ls.Count > 0)
            {
                ddl_iCollect.DataTextField = "CompanyName";
                ddl_iCollect.DataValueField = "ID";
                ddl_iCollect.AppendDataBoundItems = true;
                ddl_iCollect.DataSource = ls;
                ddl_iCollect.DataBind();
                rpt_iCollect.DataSource = ls;
                rpt_iCollect.DataBind();

            }
        }
        /// <summary>
        /// 绑定出发城市
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
        /// 获取预定打印按钮
        /// </summary>
        /// <returns></returns>
        protected string GetButt(string tourID)
        {
            return "<a href=\"/Order/OrderByTour.aspx?TourID=" + tourID + "\" class=\"basic_btn Order\" value=\"" + tourID + "\"><span>预订</span></a><a href=\"/PrintPage/TeamTourInfo.aspx?TeamId=" + tourID + "\" target=\"_blank\" class=\"basic_btn\"><span>打印</span></a>";
        }
    }
}
