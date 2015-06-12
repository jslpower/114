using System;
using System.Collections;
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
using EyouSoft.Model.NewTourStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 线路管理 团队计划列表 以及添加修改
    /// 蔡永辉 2011-12-19
    /// </summary>
    public partial class TeamAdd : EyouSoft.Common.Control.YunYingPage
    {
        protected int currentPage = 0;
        protected bool IsGrantUpdate = true;
        protected string RouteId = "";
        protected string AreaId = "";
        protected string CompanyId = "";
        /// <summary>
        /// 当前日期
        /// </summary>
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        /// <summary>
        /// 团队状态
        /// </summary>
        protected string PowderTourStatusStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            RouteId = Utils.GetQueryStringValue("RouteId");
            AreaId = Utils.GetQueryStringValue("AreaId");
            CompanyId = Utils.GetQueryStringValue("CompanyId");
            hd_routeid.Value = RouteId != "" ? RouteId : Utils.GetFormValue("RouteId");
            CheckDj();
            string operating = Utils.GetQueryStringValue("Operating");
            switch (operating)
            {
                case "AddSave":
                    AddSave();
                    break;
                case "UpSave":
                    UpdateSave();
                    break;
                case "Delect":
                    Del();
                    break;
                case "UpdatePowderTourStatus":
                    UpdatePowderTourStatus();
                    break;
                case "BtnOption":
                    BtnOption();
                    break;
                case "CheckExcitDate":
                    CheckExcitDate();
                    break;
            }
        }

        /// <summary>
        /// 获取线路区域类型
        /// </summary>
        /// <returns></returns>
        protected int GetAreaType()
        {
            MRoute routeModel =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(
                    Utils.GetQueryStringValue("routeid").Length > 0
                        ? Utils.GetQueryStringValue("routeid")
                        : Utils.GetFormValue("routeid"));

            if (routeModel == null)
                return 0;

            return (int)routeModel.RouteType;
        }

        #region  检查是否是地接
        private void CheckDj()
        {
            MRoute modelinfo = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(RouteId);
            if (modelinfo.RouteSource == RouteSource.地接社添加)
                Response.Redirect("LineList.aspx");
        }

        #endregion

        #region 检查日期是否已经存在计划
        private void CheckExcitDate()
        {
            bool IsExsite = false;
            DateTime datavalue = Utils.GetDateTime(Utils.GetQueryStringValue("datavalue"));
            IList<MPowderList> TeamLinelist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(
                            RouteId, "", null, null);
            foreach (MPowderList modelMPowderList in TeamLinelist)
            {
                if (modelMPowderList.LeaveDate == datavalue)
                {
                    IsExsite = true;
                    break;
                }
            }
            Response.Clear();
            Response.Write(IsExsite.ToString());
            Response.End();
        }
        #endregion

        #region 设置状态
        protected void BtnOption()
        {
            string[] tourIds = Utils.GetQueryStringValue("tourIds").Split('|');
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().UpdateStatus((PowderTourStatus)Utils.GetInt(Utils.GetQueryStringValue("TourStatus")), tourIds));
            Response.End();
        }
        #endregion

        #region 添加保存
        /// <summary>
        /// 添加保存
        /// </summary>
        private void AddSave()
        {
            MRoute routeModel = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(Utils.GetFormValue("RouteId"));
            if (routeModel != null)
            {
                //提前天数
                int advanceDayRegistration = routeModel.AdvanceDayRegistration;
                MPowderList model = new MPowderList();

                #region 线路信息赋值
                //线路Id
                model.RouteId = Utils.GetFormValue("RouteId");
                model.Characteristic = routeModel.Characteristic;
                //线路名称
                model.RouteName = routeModel.RouteName;
                //专线类型
                model.AreaId = routeModel.AreaId;
                model.AreaName = routeModel.AreaName;
                //发布人编号
                model.OperatorId = routeModel.OperatorId;
                model.OperatorName = routeModel.OperatorName;
                //公司编号
                model.Publishers = CompanyId;
                model.BrowseCitys = routeModel.BrowseCitys;
                model.BrowseCountrys = routeModel.BrowseCountrys;
                model.Citys = routeModel.Citys;
                model.ClickNum = 0;
                model.EndCity = routeModel.EndCity;
                model.EndCityName = routeModel.EndCityName;
                model.Day = routeModel.Day;
                model.EndTraffic = routeModel.EndTraffic;
                model.FitQuotation = routeModel.FitQuotation;
                model.IssueTime = DateTime.Now;
                model.Late = routeModel.Late;
                //收客状态
                model.PowderTourStatus = PowderTourStatus.收客;
                model.RecommendType = RecommendType.新品;
                model.RouteImg = routeModel.RouteImg;
                model.RouteImg1 = routeModel.RouteImg1;
                model.RouteImg2 = routeModel.RouteImg2;
                model.ServiceStandard = routeModel.ServiceStandard;
                model.StandardPlans = routeModel.StandardPlans;
                model.StartTraffic = routeModel.StartTraffic;
                model.TeamLeaderDec = routeModel.TeamPlanDes;
                model.Themes = routeModel.Themes;
                model.VendorsNotes = routeModel.VendorsNotes;

                EyouSoft.Model.CompanyStructure.CompanyInfo comModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
                if (comModel != null)
                {
                    model.CompanyLev = comModel.CompanyLev;
                    model.CompanyBrand = comModel.CompanyBrand;
                    model.Introduction = comModel.Introduction;
                }

                #endregion
                //团队人数
                model.TourNum = Utils.GetInt(Utils.GetFormValue("txt_tourNum"));

                //是否限制人数
                model.IsLimit = Utils.GetFormValue("hd_isLimit") == "0";
                if (!model.IsLimit)
                {
                    //余位
                    model.MoreThan = Utils.GetInt(Utils.GetFormValue("txt_moreThan"));
                }
                //成人市场价
                model.RetailAdultPrice = Utils.GetDecimal(Utils.GetFormValue("txt_retailAdultPrice"));
                //成人结算价
                model.SettlementAudltPrice = Utils.GetDecimal(Utils.GetFormValue("txt_settlementAudltPrice"));
                //儿童市场价
                model.RetailChildrenPrice = Utils.GetDecimal(Utils.GetFormValue("txt_retailChildrenPrice"));
                //儿童结算价
                model.SettlementChildrenPrice = Utils.GetDecimal(Utils.GetFormValue("txt_settlementChildrenPrice"));
                //单房差
                model.MarketPrice = Utils.GetDecimal(Utils.GetFormValue("txt_marketPrice"));
                //集合说明
                model.SetDec = Utils.GetFormValue("txt_marketPrice");
                //线路销售备注
                model.TourNotes = Utils.GetFormValue("txt_tourNotes");
                //领队全陪
                model.TeamLeaderDec = Utils.GetFormValue("txt_tourNotes");

                model.StartDate = Utils.GetFormValue(this.txt_startDate.UniqueID);
                model.EndDate = Utils.GetFormValue(this.txt_endDate.UniqueID);

                //出团日期
                string[] leaveDateStr = Utils.GetFormValue("leaveDate").Split(',');

                //计算有几个出团日期
                int i = leaveDateStr.Length;
                //出团日期
                DateTime[] leaveDate = new DateTime[i];
                //报名截止时间
                DateTime[] registrationEndDate = new DateTime[i];
                //遍历
                while (i-- > 0)
                {
                    //出团日期
                    leaveDate[i] = Utils.GetDateTime(leaveDateStr[i]);
                    //计算截止时间
                    registrationEndDate[i] = leaveDate[i] - new TimeSpan(advanceDayRegistration, 0, 0, 0);

                    //周六继续提前1天
                    if ((int)registrationEndDate[i].DayOfWeek == 6)
                    {
                        registrationEndDate[i] = registrationEndDate[i] - new TimeSpan(1, 0, 0, 0);
                    }
                    //周日继续提前2天
                    if ((int)registrationEndDate[i].DayOfWeek == 0)
                    {
                        registrationEndDate[i] = registrationEndDate[i] - new TimeSpan(2, 0, 0, 0);
                    }
                }
                model.IP = Page.Request.UserHostAddress;
                bool ret = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().AddBatchPowder(model, leaveDate, registrationEndDate);
                Response.Clear();
                if (ret)
                    Response.Write(true);
                else
                    Response.Write(false);
                Response.End();
            }
        }
        #endregion

        #region 修改保存
        /// <summary>
        /// 修改保存
        /// </summary>
        private void UpdateSave()
        {
            string data = Utils.GetFormValue("data");
            string[] modelStr = data.Split('|');
            int i = modelStr.Length;
            IList<MPowderList> ls = new List<MPowderList>();
            while (i-- > 0)
            {
                string[] subModelStr = modelStr[i].Split(',');
                MPowderList model = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(subModelStr[0]);
                //报名截止时间
                model.RegistrationEndDate = Utils.GetDateTime(subModelStr[1]);
                //人数
                model.TourNum = Utils.GetInt(subModelStr[2]);
                //留位
                model.SaveNum = Utils.GetInt(subModelStr[3]);
                //状态
                model.PowderTourStatus = (PowderTourStatus)Utils.GetInt(subModelStr[4]);
                //余位
                model.MoreThan = Utils.GetInt(subModelStr[5]);
                //成人市场价
                model.RetailAdultPrice = Utils.GetDecimal(subModelStr[6]);
                //成人结算价
                model.SettlementAudltPrice = Utils.GetDecimal(subModelStr[7]);
                //儿童市场价
                model.RetailChildrenPrice = Utils.GetDecimal(subModelStr[8]);
                //儿童结算价
                model.SettlementChildrenPrice = Utils.GetDecimal(subModelStr[9]);
                //单房差
                model.MarketPrice = Utils.GetDecimal(subModelStr[10]);
                ls.Add(model);

            }
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().UpdatePowerList(ls));
            Response.End();
        }
        #endregion

        #region 设置团队状态
        /// <summary>
        /// 设置团队状态html
        /// </summary>
        private void SetPowderTourStatus()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(PowderTourStatus));
            //rpt_updateStatus.DataSource = typeli;
            //rpt_updateStatus.DataBind();
            StringBuilder sb = new StringBuilder();
            sb.Append("<select style=\"display: none\"  class=sel_PowderTourStatus name=sel_PowderTourStatus>");
            foreach (var d in typeli)
            {
                sb.Append("<option  value=" + d.Value + ">" + d.Text + "</option>");
            }
            sb.Append("</select>");
            PowderTourStatusStr = sb.ToString();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            string[] tourIds = Utils.GetQueryStringValue("tourIds").Split('|');
            bool isDelete = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().DeletePowder(tourIds);
            Response.Clear();
            Response.Write(isDelete ? "1" : "0");
            Response.End();

        }
        #endregion

        #region 修改散拼团状态
        /// <summary>
        /// 修改散拼团状态
        /// </summary>
        private void UpdatePowderTourStatus()
        {
            string[] tourIds = Utils.GetQueryStringValue("tourIds").Split('|');
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().UpdateStatus((PowderTourStatus)Utils.GetInt(Utils.GetQueryStringValue("TourStatus")), tourIds));
            Response.End();
        }
        #endregion

    }
}
