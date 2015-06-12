using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 线路栏目下的线路区域列表
    /// </summary>
    public partial class RouteList : System.Web.UI.UserControl
    {
        private bool _isDefault = false;
        private bool _isNew = true;
        private bool _isChecklist = false;

        /// <summary>
        /// 是否是从首页进去的团队列表页
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 是否新版本
        /// </summary>
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        /// <summary>
        /// 是否以选项列表展示
        /// </summary>
        public bool IsCheckList
        {
            get { return _isChecklist; }
            set { _isChecklist = value; }
        }
        protected string strRouteList1 = "";
        protected string strRouteList2 = "";
        protected string strRouteList3 = "";
        protected string ImageServerPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!IsCheckList)
                    this.BindRouteList();
                else
                    this.BindRouteCheckList();
            }
        }

        protected void BindRouteList()
        {
            string strUrlParms = "";

            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            int CityId = page.CityId;
            string strUrl = "/TourManage/TourList.aspx";
            if (IsDefault)
            {
                strUrl = "/TourManage/DefaultTourList.aspx";
            }
            if (CityId > 0)
            {
                strUrlParms = "&CityId=" + CityId;
            }

            EyouSoft.Model.SystemStructure.SysCity Model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (Model != null)
            {
                IList<EyouSoft.Model.SystemStructure.SysCityArea> Citylist = Model.CityAreaControls.OrderBy(i => i.SortId).ToList();
                foreach (EyouSoft.Model.SystemStructure.SysCityArea item in Citylist)
                {
                    if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)
                    {
                        //strRouteList1 += string.Format("<a href=\"{0}?TourAreaId={1}\">{2}</a>",strUrl, item.AreaId + strUrlParms, item.AreaName);
                        //根据规则生成新的URL
                        if (IsDefault)
                        {
                            strRouteList1 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                        else
                        {
                            strRouteList1 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                    {
                        //根据规则生成新的URL
                        if (IsDefault)
                        {
                            strRouteList2 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                        else
                        {
                            strRouteList2 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)
                    {
                        //根据规则生成新的URL
                        if (IsDefault)
                        {
                            strRouteList3 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetDefTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                        else
                        {
                            strRouteList3 += string.Format("<a style=\"color:#074387\" title=\"" + item.AreaName + "\" href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetTourAreaUrl(item.AreaId, CityId) + "\">{0}</a>", Utils.GetText2(item.AreaName, 5, false));
                        }
                    }
                }
                Citylist = null;
            }
            Model = null;

        }

        protected void BindRouteCheckList()
        {
            FrontPage page = this.Page as FrontPage;
            ImageServerPath = page.ImageServerUrl;
            int CityId = page.CityId;
            EyouSoft.Model.SystemStructure.SysCity Model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (Model != null)
            {
                IList<EyouSoft.Model.SystemStructure.SysCityArea> Citylist = Model.CityAreaControls;
                strRouteList1 += "<ul style='clear:both;'>";
                strRouteList2 += "<ul style='clear:both;'>";
                strRouteList3 += "<ul style='clear:both;'>";
                foreach (EyouSoft.Model.SystemStructure.SysCityArea item in Citylist)
                {
                    if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线)
                    {
                        strRouteList1 += string.Format("<li style='float:left;width:25%; line-height:20px;color:#074387'><input name=\"checkRouteGC\" type=\"checkbox\" value=\"{1}\" />{0}", Utils.GetText2(item.AreaName, 5, false), item.AreaId);
                       
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                    {
                        strRouteList2 += string.Format("<li style='float:left;width:25%; color:#074387 line-height:20px%;'><input name=\"checkRouteGJ\" type=\"checkbox\" value=\"{1}\" />{0}", Utils.GetText2(item.AreaName, 5, false), item.AreaId);
                       
                    }
                    else if (item.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线)
                    {
                        strRouteList3 += string.Format("<li style='float:left;width:25%; color:#074387 line-height:20px%;'><input name=\"checkRouteGD\" type=\"checkbox\" value=\"{1}\" />{0}", Utils.GetText2(item.AreaName, 5, false), item.AreaId);
                    }
                }
                strRouteList1 += "</li></ul>";
                strRouteList2 += "</li></ul>";
                strRouteList3 += "</li></ul>";
                Citylist = null;
            }
            Model = null;
        }
    }
}