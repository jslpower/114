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
using EyouSoft.Common.Function;
using System.Text;
using System.Collections.Generic;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——通用专线维护出境线列表
    /// 开发人：杜桂云      开发时间：2010-06-25
    /// </summary>
    public partial class AjaxByOutRouteList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected bool deletFlag = true;
        protected bool EditFlag = false;
        #endregion
        protected string GetOutRouteList = string.Empty;

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
            //初始化数据绑定
            GetAreaList();

            if (!Page.IsPostBack)
            {
                //处理页面删除请求
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
                             GetOutRouteList += "<td width=\"25%\" height=\"22\" align=\"left\">" + (index + 1) + ".&nbsp;<input name=\"LevleName4\" value=" + areaList[index].AreaName + " size=\"12\" maxlength=\"10\" /><a class=\"updateRouteAreaP\" href=\"javascript:void(0);\" RouteAreaId=" + areaList[index].AreaId + ">修改</a> <a href=\"javascript:void(0);\" class=\"deleteRouteAreaP\" RouteAreaId=" + areaList[index].AreaId + ">删除</a></td>";
                         }
                     }
                     GetOutRouteList += "</tr>";
                 }
                 GetOutRouteList += "</table>";
             }
            return GetOutRouteList;
        }
        #endregion


        #region 新增事件
        private void Add(string AreaName)
        {
            //新增
            string strErr = "";
            if (AreaName == "")
            {
                strErr += "线路区域名称不能为空！\\n";
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            EyouSoft.Model.SystemStructure.SysArea areaModel = new EyouSoft.Model.SystemStructure.SysArea();
            areaModel.AreaName = AreaName;
            areaModel.RouteType = EyouSoft.Model.SystemStructure.AreaType.国际线;
            int reAddInt = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().AddSysArea(areaModel);
            //释放资源
            areaModel = null;
            return;
        }
        #endregion

        #region 修改事件
        private void Eidt(string EidtID, string AreaName)
        {
            string strErr = "";
            if (AreaName == "")
            {
                strErr += "线路区域名称不能为空！\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            EyouSoft.Model.SystemStructure.SysArea areaModel = new EyouSoft.Model.SystemStructure.SysArea();
            areaModel.AreaName = AreaName;
            areaModel.AreaId = int.Parse(EidtID);
            areaModel.RouteType = EyouSoft.Model.SystemStructure.AreaType.国际线;
            int reAddInt = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().UpdateSysArea(areaModel);
            //释放资源
            areaModel = null;
            return;
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
