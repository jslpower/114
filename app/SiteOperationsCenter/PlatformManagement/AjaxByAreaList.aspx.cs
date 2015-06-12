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
using System.Collections.Generic;
using System.Text;

namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：平台管理——线路区域分类，根据省份编号显示已经选择的线路区域
    /// 开发人：杜桂云      开发时间：2010-07-1
    /// </summary>
    public partial class AjaxByAreaList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int CityID = 0;
        protected int ProvinceID = 0;
        protected int TypeID = 0;
        protected bool EditFlag = false; //操作权限
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_线路区域分类 };
            EditFlag = CheckMasterGrant(parms);
            if (!EditFlag)
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_线路区域分类, true);
                return;
            }
            ProvinceID = int.Parse(Request.QueryString["ProvinceID"]);
            CityID = int.Parse(Request.QueryString["CityID"]);
            //根据类别绑定相应列表；国内长线：0   国内短线：1
            TypeID = int.Parse(Request.QueryString["TypeID"]);
            //页面请求删除操作
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["DeletID"])))
            {
                DelProcess(Utils.InputText(Request.QueryString["DeletID"]), TypeID);
            }
            InitData();
        }
        #endregion

        #region 数据绑定
        private void InitData()
        {
            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> areaList = null;
            //取省份和城市编号，如果类型为0，则代表国内长线，否则为国内短线

            if (TypeID == 0)
            {
                //根据省份编号查数据列表
                areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(ProvinceID);
            }
            else
            {
                //根据城市编号查数据列表
                areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(CityID);
            }
            if (areaList != null && areaList.Count > 0)
            {
                this.dalList.DataSource = areaList;
                this.dalList.DataBind();
            }
            //释放资源
            areaList = null;
        }
        #endregion

        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelProcess(string DelID, int TypeID)
        {
            if (TypeID == 0)
            {
                EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().DeleteLongAreaSiteControl(ProvinceID, int.Parse(DelID));
                return;
            }
            else
            {
                EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().DeleteShortAreaSiteControl(CityID, int.Parse(DelID));
                return;
            }
        }
        #endregion

        #region 处理项操作事件
        /// <summary>
        ///修改和删除操作
        /// </summary>
        /// <returns></returns>
        public string CreateOperation(string ID)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_线路区域分类 };
            if (!CheckMasterGrant(parms))
            {
                return string.Format("&nbsp;&nbsp;");
            }
            else
            {
                return string.Format("<a href='javascript:void(0);' onclick='DeleteRouteArea({0})'>删除</a>", ID);
            }
        }
        #endregion
    }
}
