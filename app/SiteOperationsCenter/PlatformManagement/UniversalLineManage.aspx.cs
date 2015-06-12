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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——通用专线区域维护
    /// 开发人：李晓欢      开发时间：2011-12-26
    /// </summary>
    public partial class UniversalLineManage : EyouSoft.Common.Control.YunYingPage
    {
        #region 页面加载
        protected string GetArealistHtml = string.Empty;
        protected string GetOutRouteList = string.Empty;
        protected string GetRouteAreaList = string.Empty;
        protected bool deletFlag = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_通用专线区域 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.平台管理_通用专线区域, true);
                    return;
                }
                //页面请求删除操作
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeletID"])))
                {
                    DelProcess(Utils.InputText(Request.QueryString["DeletID"]));
                    Response.Write(deletFlag);
                    Response.End();
                }
            }

            //绑定线路区域数据
            GetAreaList();
            GetOutAreaList();
            GetZBRouteList();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected string GetAreaList()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内长线);
            if (areaList != null && areaList.Count > 0)
            {
                GetArealistHtml = "<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#C8E0EB\" class=\"table_basic\"><tr></tr>";
                int index = -1;
                for (int i = 0; i < areaList.Count; i++)
                {
                    GetArealistHtml += "<tr>";
                    for (int j = 0; j < 4; j++)
                    {
                        index++;
                        if (index > areaList.Count - 1)
                        {
                            GetArealistHtml += "";
                        }
                        else
                        {
                            GetArealistHtml += "<td width=\"25%\" height=\"22\" align=\"left\">" + (index + 1) + ".&nbsp;" + areaList[index].AreaName + "&nbsp;&nbsp;<a class=\"updateRouteArea\" href=\"javascript:void(0);\" RouteAreaId=" + areaList[index].AreaId + " routeType=\"0\">修改</a> <a href=\"javascript:void(0);\" class=\"deleteRouteArea\" RouteAreaId=" + areaList[index].AreaId + ">删除</a></td>";
                        }
                    }
                    GetArealistHtml += "</tr>";
                }
                GetArealistHtml += "</table>";
            }
            return GetArealistHtml;

        }
        protected string GetOutAreaList()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
            if (areaList != null && areaList.Count > 0)
            {
                GetOutRouteList = "<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#C8E0EB\" class=\"table_basic\"><tr></tr>";
                int index = -1;
                for (int i = 0; i < areaList.Count; i++)
                {
                    GetOutRouteList += "<tr>";
                    for (int j = 0; j < 4; j++)
                    {
                        index++;
                        if (index > areaList.Count - 1)
                        {
                            GetOutRouteList += "";
                        }
                        else
                        {
                            GetOutRouteList += "<td width=\"25%\" height=\"22\" align=\"left\">" + (index + 1) + ".&nbsp;" + areaList[index].AreaName + "&nbsp;&nbsp;<a class=\"updateRouteArea\" href=\"javascript:void(0);\" RouteAreaId=" + areaList[index].AreaId + " routeType=\"1\">修改</a> <a href=\"javascript:void(0);\" class=\"deleteRouteArea\" RouteAreaId=" + areaList[index].AreaId + ">删除</a></td>";
                        }
                    }
                    GetOutRouteList += "</tr>";
                }
                GetOutRouteList += "</table>";
            }
            return GetOutRouteList;
        }
        protected string GetZBRouteList()
        {
            IList<EyouSoft.Model.SystemStructure.SysArea> areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内短线);
            if (areaList != null && areaList.Count > 0)
            {
                GetRouteAreaList = "<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#C8E0EB\" class=\"table_basic\"><tr></tr>";
                int index = -1;
                for (int i = 0; i < areaList.Count; i++)
                {
                    GetRouteAreaList += "<tr>";
                    for (int j = 0; j < 4; j++)
                    {
                        index++;
                        if (index > areaList.Count - 1)
                        {
                            GetRouteAreaList += "";
                        }
                        else
                        {
                            GetRouteAreaList += "<td width=\"25%\" height=\"22\" align=\"left\">" + (index + 1) + ".&nbsp;" + areaList[index].AreaName + "&nbsp;&nbsp;<a class=\"updateRouteArea\" href=\"javascript:void(0);\" RouteAreaId=" + areaList[index].AreaId + " routeType=\"2\">修改</a> <a href=\"javascript:void(0);\" class=\"deleteRouteArea\" RouteAreaId=" + areaList[index].AreaId + ">删除</a></td>";
                        }
                    }
                    GetRouteAreaList += "</tr>";
                }
                GetRouteAreaList += "</table>";
            }
            return GetRouteAreaList;
        }
        #endregion


        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelProcess(string DelID)
        {
            string strErr = "";
            if (StringValidate.IsInteger(DelID) == false)
            {
                strErr += "未找到您要删除的线路区域";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            deletFlag = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().DeleteSysArea(int.Parse(DelID));
            return;
        }
        #endregion
    }
}
