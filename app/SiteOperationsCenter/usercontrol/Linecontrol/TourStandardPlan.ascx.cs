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
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.usercontrol.Linecontrol
{
    public partial class TourStandardPlan : System.Web.UI.UserControl
    {
        #region 自定义变量
        protected string strTraffic = string.Empty;

        protected string tmpStandardPlan = string.Empty;
        /// <summary>
        /// 页面最外层的tableid
        /// </summary>
        private string _containerid;
        public string ContainerID
        {
            set { _containerid = value; }
            get { return _containerid; }
        }
        /// <summary>
        /// 判断是团队和线路的发布类型
        /// </summary>
        private string _releasetype;
        public string ReleaseType
        {
            set { _releasetype = value; }
            get { return _releasetype; }
        }

        private string _moduletype;
        /// <summary>
        /// 判断是线路还是团队
        /// </summary>
        public string ModuleType
        {
            set { _moduletype = value; }
            get { return _moduletype; }
        }

        private IList<EyouSoft.Model.TourStructure.TourStandardPlan> _toursandardplan = null;
        /// <summary>
        /// 团队标准发布行程信息
        /// </summary>
        public IList<EyouSoft.Model.TourStructure.TourStandardPlan> TourStandardPlanInfo
        {
            set { _toursandardplan = value; }
            get { return _toursandardplan; }
        }

        private IList<MStandardPlan> _routesandardplan = null;
        /// <summary>
        /// 线路标准发布行程信息
        /// </summary>
        public IList<MStandardPlan> RouteStandardPlanInfo
        {
            set { _routesandardplan = value; }
            get { return _routesandardplan; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ModuleType == "tour")
                {
                    InitTourStandardPlan();
                }
                else
                {
                    InitRouteStandardPlan();
                }
                foreach (string str in Enum.GetNames(typeof(EyouSoft.Model.NewTourStructure.TrafficType)))
                {
                    hidarrTraffic.Value += str + ",";
                }
                hidarrTraffic.Value = hidarrTraffic.Value.TrimEnd(',');
            }
        }

        #region 初始化团队行程信息
        /// <summary>
        /// 初始化团队行程信息
        /// </summary>
        private void InitTourStandardPlan()
        {
            if (TourStandardPlanInfo != null && TourStandardPlanInfo.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                string[] arrTraffic = Enum.GetNames(typeof(TrafficType));
                foreach (EyouSoft.Model.TourStructure.TourStandardPlan planModel in TourStandardPlanInfo)
                {
                    str.Append("<tr>");
                    // 日程
                    str.AppendFormat("<td rowspan=\"2\" align=\"center\" class=\"zhonglan\"> <input type=\"hidden\" class=\"input\" id=\"DayJourney\" name=\"DayJourney\" value=\"{0}\">D{0} </td>", planModel.PlanDay);
                    // 区间
                    str.Append("<td align=\"center\" class=\"zhonglan_l\"> 途径：");
                    str.AppendFormat("<input name=\"SightArea{0}\" id=\"SightArea\" type=\"text\" class=\"input\" value=\"{1}\" size=\"12\" /></td>", planModel.PlanDay, planModel.PlanInterval);
                    str.AppendFormat("<td class=\"zhonglan_l\" width=\"120\" align=\"center\"><select name=\"Vehicle{0}\" id=\"Vehicle\">", planModel.PlanDay);
                    str.Append("<option value=\"\">请选择</option>");

                    for (int i = 0; i < arrTraffic.Length; i++)
                    {
                        if (arrTraffic[i] == planModel.Vehicle)
                        {
                            str.AppendFormat("<option value=\"{0}\" selected>{0}</option>", arrTraffic[i]);
                        }
                        else
                        {
                            str.AppendFormat("<option value=\"{0}\">{0}</option>", arrTraffic[i]);
                        }
                    }
                    str.Append("</select></td>");
                    // 班次
                    str.AppendFormat("<td align=\"center\" class=\"zhonglan_l\"><input name=\"TrafficNumber{0}\" id=\"TrafficNumber\" type=\"text\" class=\"input\" value=\"{1}\" size=\"15\" /></td>", planModel.PlanDay, planModel.TrafficNumber);
                    // 住宿
                    str.AppendFormat("<td align=\"center\" class=\"zhonglan_l\"><input name=\"Resideplan{0}\" id=\"Resideplan\" type=\"text\" class=\"input\" value=\"{1}\" size=\"15\" /></td>", planModel.PlanDay, planModel.House);
                    // 用餐
                    str.Append("<td align=\"center\" class=\"zhonglan_l\">");
                    string oneChecked = string.Empty, twoChecked = string.Empty, threeChecked = string.Empty;
                    if (planModel.Dinner.Contains("早"))
                    {
                        oneChecked = "checked";
                    }
                    if (planModel.Dinner.Contains("中"))
                    {
                        twoChecked = "checked";
                    }
                    if (planModel.Dinner.Contains("晚"))
                    {
                        threeChecked = "checked";
                    }
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlan{0}\" id=\"DinnerPlan\" value=\"早\" " + oneChecked + " />&nbsp;早&nbsp;", planModel.PlanDay);
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlan{0}\" id=\"DinnerPlan\" value=\"中\" " + twoChecked + " />&nbsp;中&nbsp;", planModel.PlanDay);
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlan{0}\" id=\"DinnerPlan\" value=\"晚\" " + threeChecked + " />&nbsp;晚&nbsp;", planModel.PlanDay);
                    str.Append("</td>");
                    str.Append("<td rowspan=\"2\" align=\"center\" class=\"zhonglan_l\"><a href=\"javascript:void(0);\" onclick=\"TourStandardPlan.DeleteOneyDay(this,'" + ContainerID + "');return false;\">删除</a></td>");
                    str.Append("</tr>");
                    str.Append("<tr>");
                    str.AppendFormat("<td colspan=\"5\" align=\"left\" class=\"zhonglan_l\"><textarea name=\"JourneyInfo{0}\" id=\"JourneyInfo\" cols=\"120\" rows=\"3\" class=\"input\">{1}</textarea></td>", planModel.PlanDay, planModel.PlanContent);
                    str.Append("</tr>");
                }
                TourStandardPlanInfo = null;
                tmpStandardPlan = str.ToString();
                str = null;
            }
        }
        #endregion

        #region 初始化线路行程信息
        /// <summary>
        /// 初始化线路行程信息
        /// </summary>
        private void InitRouteStandardPlan()
        {
            if (RouteStandardPlanInfo != null && RouteStandardPlanInfo.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                string[] arrTraffic = Enum.GetNames(typeof(TrafficType));
                RouteStandardPlanInfo = RouteStandardPlanInfo.OrderBy(p => p.PlanDay).ToList();
                foreach (MStandardPlan planModel in RouteStandardPlanInfo)
                {
                    str.Append("<tr>");
                    // 日程
                    str.AppendFormat("<td rowspan=\"2\" align=\"center\" class=\"zhonglan\"><input type=\"hidden\" class=\"input\" id=\"DayJourney\" name=\"DayJourney\" value=\"{0}\"> D{0} </td>", planModel.PlanDay);
                    // 途经
                    str.Append("<td align=\"center\" class=\"zhonglan_l\"> 途经：");
                    str.AppendFormat("<input name=\"SightArea{0}\" id=\"SightArea\" type=\"text\" class=\"input\" value=\"{1}\" size=\"12\" /></td>", planModel.PlanDay, planModel.PlanInterval);
                    str.AppendFormat("<td class=\"zhonglan_l\" width=\"120\" align=\"center\"><select name=\"Vehicle{0}\" id=\"Vehicle\">", planModel.PlanDay);
                    str.Append("<option value=\"\">请选择</option>");

                    for (int i = 0; i < arrTraffic.Length; i++)
                    {
                        if (arrTraffic[i] == planModel.Vehicle.ToString())
                        {
                            str.AppendFormat("<option value=\"{0}\" selected>{0}</option>", arrTraffic[i]);
                        }
                        else
                        {
                            str.AppendFormat("<option value=\"{0}\">{0}</option>", arrTraffic[i]);
                        }
                    }
                    str.Append("</select></td>");

                    // 住宿
                    str.AppendFormat("<td align=\"center\" class=\"zhonglan_l\"><input name=\"Resideplan{0}\" id=\"Resideplan\" type=\"text\" class=\"input\" value=\"{1}\" size=\"15\" /></td>", planModel.PlanDay, planModel.House);
                    // 用餐
                    str.Append("<td align=\"center\" class=\"zhonglan_l\">");
                    string oneChecked = string.Empty, twoChecked = string.Empty, threeChecked = string.Empty;
                    if (planModel.Early)
                    {
                        oneChecked = "checked";
                    }
                    if (planModel.Center)
                    {
                        twoChecked = "checked";
                    }
                    if (planModel.Late)
                    {
                        threeChecked = "checked";
                    }
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlanEarly{0}\" id=\"DinnerPlanEarly{0}\" value=\"早\" " + oneChecked + " />&nbsp;早&nbsp;", planModel.PlanDay);
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlanCenter{0}\" id=\"DinnerPlanCenter{0}\" value=\"中\" " + twoChecked + " />&nbsp;中&nbsp;", planModel.PlanDay);
                    str.AppendFormat("<input type=\"checkbox\" name=\"DinnerPlanLate{0}\" id=\"DinnerPlanLate{0}\" value=\"晚\" " + threeChecked + " />&nbsp;晚&nbsp;", planModel.PlanDay);
                    str.Append("</td>");
                    //str.Append("<td rowspan=\"2\" align=\"center\" class=\"zhonglan_l\"><a href=\"javascript:void(0);\" onclick=\"TourStandardPlan.DeleteOneyDay(this,'" + ContainerID + "');return false;\">删除</a></td>");
                    str.Append("</tr>");
                    str.Append("<tr>");
                    str.AppendFormat("<td colspan=\"5\" align=\"left\" class=\"zhonglan_l\"><textarea name=\"JourneyInfo{0}\" id=\"JourneyInfo\" cols=\"120\" rows=\"3\" class=\"input\">{1}</textarea></td>", planModel.PlanDay, planModel.PlanContent);
                    str.Append("</tr>");
                }
                RouteStandardPlanInfo = null;
                tmpStandardPlan = str.ToString();
                str = null;
            }
        }
        #endregion
    }
}