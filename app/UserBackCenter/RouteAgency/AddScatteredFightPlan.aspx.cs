using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using System.Text;
using EyouSoft.IBLL.NewTourStructure;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 添加修改散拼计划
    /// </summary>
    /// 创建人：柴逸宁
    /// 2011-12-30
    public partial class AddScatteredFightPlan : BackPage
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
        /// 团队状态html
        /// </summary>
        protected string PowderTourStatusStr = string.Empty, leaveDateStr = string.Empty;

        protected int AreaType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAreaType();

            SetPowderTourStatus();
            //线路id
            hd_routeid.Value = Utils.GetQueryStringValue("routeid").Length > 0 ? Utils.GetQueryStringValue("routeid") : Utils.GetFormValue("routeid");
            Key = "ScatteredFightPlan" + Guid.NewGuid().ToString();
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
            }
            if (!IsPostBack)
            {
                InitPowderList();
            }

        }

        /// <summary>
        /// 获取线路区域类型
        /// </summary>
        /// <returns></returns>
        protected void GetAreaType()
        {
            MRoute routeModel =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(
                    Utils.GetQueryStringValue("routeid").Length > 0
                        ? Utils.GetQueryStringValue("routeid")
                        : Utils.GetFormValue("routeid"));
            if (routeModel == null)
                AreaType = 0;
            else
                AreaType = (int)routeModel.RouteType;
        }

        /// <summary>
        /// 添加保存
        /// </summary>
        private void AddSave()
        {
            //出团日期
            string[] leaveDateStr = Utils.GetFormValue("leaveDate").Split(',');
            MRoute routeModel = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(Utils.GetFormValue("routeid"));
            //提前天数
            int advanceDayRegistration = routeModel.AdvanceDayRegistration;
            MPowderList model = new MPowderList();
            #region 已注释
            //发布人名称
            //model.OperatorName = SiteUserInfo.UserName;
            ////主要浏览城市
            //model.BrowseCitys = routeModel.BrowseCitys;
            ////主要浏览国家
            //model.BrowseCountrys = routeModel.BrowseCountrys;
            ////线路特色
            //model.Characteristic = routeModel.Characteristic;
            ////点击量
            //model.ClickNum = routeModel.ClickNum;
            ////发布商品牌名称
            //model.CompanyBrand = routeModel.CompanyBrand;
            ////公司等级
            //model.Companylev = routeModel.Companylev;
            ////发布商简称
            //model.Introduction = routeModel.Introduction;
            #endregion
            //发布人IP
            model.IP = Request.UserHostAddress;
            //发布人编号
            model.OperatorId = SiteUserInfo.ID;
            //公司编号
            model.Publishers = SiteUserInfo.CompanyID;
            //线路Id
            model.RouteId = Utils.GetFormValue("routeid");
            //线路名称
            model.RouteName = routeModel.RouteName;
            //专线类型
            model.AreaId = routeModel.AreaId;
            //团队人数
            model.TourNum = Utils.GetInt(Utils.GetFormValue("txt_tourNum"));
            //是否限制人数
            model.IsLimit = Utils.GetFormValue("hd_isLimit") == "0";
            if (!model.IsLimit)
            {
                //余位
                model.MoreThan = Utils.GetInt(Utils.GetFormValue("txt_moreThan"), model.TourNum);
            }
            //成人市场价
            model.RetailAdultPrice = Utils.GetDecimal(Utils.GetFormValue("txt_retailAdultPrice"));
            //成人结算价
            model.SettlementAudltPrice = Utils.GetDecimal(Utils.GetFormValue("txt_settlementAudltPrice"));
            //儿童市场价
            model.RetailChildrenPrice = Utils.GetDecimal(Utils.GetFormValue("txt_retailChildrenPrice"), 0);
            //儿童结算价
            model.SettlementChildrenPrice = Utils.GetDecimal(Utils.GetFormValue("txt_settlementChildrenPrice"), 0);
            //单房差
            model.MarketPrice = Utils.GetDecimal(Utils.GetFormValue("txt_marketPrice"));
            //集合说明
            model.SetDec = Utils.GetFormValue("txt_setDec");
            //线路销售备注
            model.TourNotes = Utils.GetFormValue("txt_tourNotes");
            //领队全陪
            model.TeamLeaderDec = Utils.GetFormValue("txt_teamLeaderDec");
            //航班出发时间
            model.StartDate = Utils.GetFormValue("txt_startDate");
            //航班返回时间
            model.EndDate = Utils.GetFormValue("txt_endDate");

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
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().AddBatchPowder(model, leaveDate, registrationEndDate));
            Response.End();
        }
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
        /// <summary>
        /// 初始化计划列表
        /// </summary>
        private void InitPowderList()
        {
            //分页参数
            int pageSize = 10,
                pageCurrent = Utils.GetInt(Utils.GetQueryStringValue("page"), 1),
                recordCount = 0;
            //线路名称
            lbl_lineName.Text = Utils.GetQueryStringValue("routename");
            #region 添加需要验证的日期
            /*已经存在计划的日期不允许重复添加*/
            MPowderSearch queryModel = new MPowderSearch();
            queryModel.RouteId = Utils.GetQueryStringValue("routeid");
            IPowderList bll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
            var list = bll.GetList(0, queryModel, 0);
            DateTime[] leaveDate = bll.GetList(0, queryModel, 0).Select(testls => testls.LeaveDate).ToArray();
            int i = leaveDate.Length;
            /*已经添加计划的日期*/
            while (i-- > 0)
            {
                /*因为iframe的日期格式为yyyy-M-d,所以你懂得*/
                leaveDateStr += leaveDate[i].ToString("yyyy-M-d") + ",";
            }
            /*当天日期*/
            //leaveDateStr += DateTime.Now.ToString("yyyy-M-d") + ",";
            #endregion
            //当前线路散拼计划列表
            IList<MPowderList> ls = bll.GetList(
                pageSize,
                pageCurrent,
                ref recordCount,
                Utils.GetQueryStringValue("routeid"),
                Utils.GetQueryStringValue("tourNo"),
                Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startDate")),
                Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endDate")));
            txt_selectEndDate.Value = Utils.GetQueryStringValue("endDate");
            txt_selectStartDate.Value = Utils.GetQueryStringValue("startDate");
            if (ls != null && ls.Count > 0)
            {
                ExportPageInfo1.Visible = true;
                rpt_Tlist.DataSource = ls;
                rpt_Tlist.DataBind();
                this.ExportPageInfo1.intPageSize = pageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = pageCurrent;
                //this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams.Add("routeid", Utils.GetQueryStringValue("routeid"));
                this.ExportPageInfo1.UrlParams.Add("routename", Utils.GetQueryStringValue("routename"));
                this.ExportPageInfo1.UrlParams.Add("tourNo", Utils.GetQueryStringValue("tourNo"));
                this.ExportPageInfo1.UrlParams.Add("startDate", Utils.GetQueryStringValue("startDate"));
                this.ExportPageInfo1.UrlParams.Add("endDate", Utils.GetQueryStringValue("endDate"));
            }
        }
        /// <summary>
        /// 设置团队状态html
        /// </summary>
        private void SetPowderTourStatus()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(PowderTourStatus));
            rpt_updateStatus.DataSource = typeli;
            rpt_updateStatus.DataBind();
            StringBuilder sb = new StringBuilder();
            sb.Append("<select class=sel_PowderTourStatus name=sel_PowderTourStatus>");
            foreach (var d in typeli)
            {
                sb.Append("<option  value=" + d.Value + ">" + d.Text + "</option>");
            }
            sb.Append("</select>");
            PowderTourStatusStr = sb.ToString();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            string[] tourIds = Utils.GetQueryStringValue("tourIds").Split('|');
            Response.Clear();
            Response.Write(EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().DeletePowder(tourIds));
            Response.End();

        }
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
    }
}
