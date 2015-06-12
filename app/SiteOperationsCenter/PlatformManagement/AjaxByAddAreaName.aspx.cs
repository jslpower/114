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
    /// 页面功能：平台管理——通用专线维护国内长线列表
    /// 开发人：杜桂云      开发时间：2010-06-25
    /// </summary>
    public partial class AjaxByAddAreaName : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        //执行操作后的返回值
        protected bool deletFlag = true;
        protected bool EditFlag = false;
        #endregion
        protected string GetArealistHtml = string.Empty;

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_通用专线区域 };
            EditFlag = CheckMasterGrant(parms);
            if (!EditFlag)
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_通用专线区域, true);
                return;
            }
            //绑定线路区域数据
            GetAreaList();
            if (!Page.IsPostBack)
            {
                //页面请求删除操作
                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeletID"])))
                {
                    DelProcess(Utils.InputText(Request.QueryString["DeletID"]));
                    Response.Write(deletFlag);
                    Response.End();
                }               
            }           
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
                            GetArealistHtml += "<td width=\"25%\" height=\"22\" align=\"left\">" + (index + 1) + ".&nbsp;<input name=\"LevleName4\" value=" + areaList[index].AreaName + " size=\"12\" maxlength=\"10\" /><a class=\"updateRouteAreaC\" href=\"javascript:void(0);\" RouteAreaId=" + areaList[index].AreaId + ">修改</a> <a href=\"javascript:void(0);\" class=\"deleteRouteAreaC\" RouteAreaId=" + areaList[index].AreaId + ">删除</a></td>";
                        }
                    }
                    GetArealistHtml += "</tr>";
                }
                GetArealistHtml += "</table>";
            }
            return GetArealistHtml;
            
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
