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
    /// 页面功能：销售区域维护——第二步选择线路区域
    /// 开发人：杜桂云      开发时间：2010-06-28
    /// </summary>
    public partial class ChooseRouteArea : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int count = 0;
        protected int CityID = 0;
        protected int ProvinceID = 0;
        private string[] AreaIdList = null;    //该城市下所拥有的线路区域
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_城市管理 };
            if (!CheckMasterGrant(parms)) 
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_城市管理, true);
                return;
            }
            // 通过城市ID获得已经拥有的线路区域的id  
            CityID = Utils.GetInt(Request.QueryString["CityID"]);
            ProvinceID = Utils.GetInt(Request.QueryString["ProvinceID"]);
            EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            string tmpAreaIdList = "";
            if (cityModel != null)
            {
                if (cityModel.CityAreaControls != null)
                {
                    IList<EyouSoft.Model.SystemStructure.SysCityArea> areList = cityModel.CityAreaControls;
                    if (areList != null && areList.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.SysCityArea model in areList)
                        {
                            tmpAreaIdList += model.AreaId + ",";
                        }
                    }
                    areList = null;
                }
                if (tmpAreaIdList != "")
                {
                    tmpAreaIdList = tmpAreaIdList.Substring(0, tmpAreaIdList.Length - 1);
                    AreaIdList = tmpAreaIdList.Split(",".ToCharArray());
                }
            }
            //释放资源
            cityModel = null;
            //初始化信息绑定
            BindTourAreaList(CityID,ProvinceID);
        }
        #endregion

        #region 绑定线路区域
        private void BindTourAreaList(int CityID, int ProvinceID)
        {

            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> areaList = null;
            IList<EyouSoft.Model.SystemStructure.SysArea> outList = null;
            areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(ProvinceID);
            if (areaList != null && areaList.Count > 0)
            {
                //国内长线，取城市所在省份下的所有线路区域
                this.dl_GNLongRouteList.DataSource = areaList;
                this.dl_GNLongRouteList.DataBind();
            }
            areaList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(CityID);
            if (areaList != null && areaList.Count > 0)
            {
                //国内短线
                this.dl_GNShortRouteList.DataSource = areaList;
                this.dl_GNShortRouteList.DataBind();
            }
            outList = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
            if (outList != null && outList.Count > 0)
            {
                //出境线路区域
                this.dl_OutRouteList.DataSource = outList;
                this.dl_OutRouteList.DataBind();
            }
            //释放资源
            areaList = null;
            outList = null;
        }
        #endregion

        #region 显示专线区域控制的checkbox
        /// <summary>
        /// 显示专线区域控制的checkbox
        /// </summary>
        /// <param name="AreaId">专线区域id</param>
        /// <returns></returns>
        protected string ShowTourAreaCheckBox(string AreaId,string AreaName,string RouteType)
        {
            bool isSelected = false;  //判断是否被选择过
            string returnVal = "";
            //判断该专线是否已经被该城市选择过
            if (AreaIdList != null)
            {
                foreach (string id in AreaIdList)
                {
                    if (!string.IsNullOrEmpty(id) && StringValidate.IsInteger(id) && int.Parse(AreaId) == int.Parse(id))
                    {
                        isSelected = true;
                        break;
                    }
                }
            }

            if (!isSelected)
            {
                returnVal = string.Format("<input type=\"checkbox\" id=\"TourAreaID_{0}\" name=\"cbkName_{2}\"  value=\"{0}|{1}\">", AreaId, AreaName, RouteType);
            }
            else
            {
                returnVal = string.Format("<input type=\"checkbox\" id=\"TourAreaID_{0}\" name=\"cbkName_{2}\"  value=\"{0}|{1}\"  checked=true >", AreaId, AreaName, RouteType);
            }
            return returnVal;
        }
        #endregion

        #region 显示排序和是否首页显示的信息
        protected string ShowEditeInfo(string AreaId,string RouteType) 
        {
            string returnVal = "";
            EyouSoft.Model.SystemStructure.SysCity cityModel=EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityID);
            if (cityModel != null) 
            {
                IList<EyouSoft.Model.SystemStructure.SysCityArea> cityAreaList=cityModel.CityAreaControls;
                if (cityAreaList != null && cityAreaList.Count > 0) 
                {
                    foreach (EyouSoft.Model.SystemStructure.SysCityArea model in cityAreaList) 
                    {
                        if (model.AreaId == int.Parse(AreaId) && model.RouteType.ToString() == RouteType) 
                        {
                            if (model.IsDefaultShow==true)
                                return returnVal = string.Format("<span class='fonth'>跳至<input name='textfield222' type='text' class='textfield' size='1' value='{0}' />位</span><input name='cbk_IsShowIndex' type='checkbox' id='cbk_IsShowIndex' checked/>首页</span>", model.SortId.ToString());
                            else
                                return returnVal = string.Format("<span class='fonth'>跳至<input name='textfield222' type='text' class='textfield' size='1' value='{0}' />位</span><input name='cbk_IsShowIndex' type='checkbox' id='cbk_IsShowIndex'/>首页</span>", model.SortId.ToString());
                        }
                    }
                }
            }
            //释放资源
            cityModel = null;
            return returnVal = "<span class='fonth'>跳至<input name='textfield222' type='text' class='textfield' size='1' value='' />位</span><input name='cbk_IsShowIndex' type='checkbox' id='cbk_IsShowIndex'/>首页</span>";
        }
        #endregion

        #region 保存所选线路区域
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<EyouSoft.Model.SystemStructure.SysCityAreaControl> modelList = new List<EyouSoft.Model.SystemStructure.SysCityAreaControl>();
            string[] LongVal = Utils.GetFormValues("cbkName_国内长线");
            string[] ShortVal = Utils.GetFormValues("cbkName_国内短线");
            string[] OutVal = Utils.GetFormValues("cbkName_国际线");
           //添加国内长线模块
            if (LongVal != null && LongVal.Length>0)
            {
                EyouSoft.Model.SystemStructure.SysCityAreaControl model = null;
                foreach (string LongM in LongVal)
                {
                    model = new EyouSoft.Model.SystemStructure.SysCityAreaControl();
                    string[] strList = LongM.Split('_');
                    string[] areaInfo = strList[0].Split('|');
                    model.AreaId = int.Parse(areaInfo[0]);
                    model.AreaName = areaInfo[1];
                    model.CityId = CityID;
                    model.CityName = "";
                    if (strList[2] != null)
                    {
                        if (strList[2].ToString() == "1")
                            model.IsDefaultShow = true;
                        else
                            model.IsDefaultShow = false;
                    }
                    else
                    {
                        model.IsDefaultShow = false;
                    }
                    model.ProvinceId = ProvinceID;
                    model.ProvinceName = "";
                    model.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内长线;
                    if (strList[1] != null && StringValidate.IsInteger(strList[1].ToString()))
                    {
                        model.SortId = int.Parse(strList[1].ToString());
                    }
                    else
                    {
                        model.SortId = 0;
                    }
                    model.TourCount = 0;
                    modelList.Add(model);
                }
            }
            //添加国内短线模块
            if (ShortVal != null && ShortVal.Length > 0)
            {
                EyouSoft.Model.SystemStructure.SysCityAreaControl model = null;
                foreach (string ShortM in ShortVal)
                {
                    model = new EyouSoft.Model.SystemStructure.SysCityAreaControl();
                    string[] strList = ShortM.Split('_');
                    string[] areaInfo = strList[0].Split('|');
                    model.AreaId = int.Parse(areaInfo[0]);
                    model.AreaName = areaInfo[1];
                    model.CityId = CityID;
                    model.CityName = "";
                    if (strList[2] != null)
                    {
                        if (strList[2].ToString() == "1")
                            model.IsDefaultShow = true;
                        else
                            model.IsDefaultShow = false;
                    }
                    else
                    {
                        model.IsDefaultShow = false;
                    }
                    model.ProvinceId = ProvinceID;
                    model.ProvinceName = "";
                    model.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内短线;
                    if (strList[1] != null && StringValidate.IsInteger(strList[1].ToString()))
                    {
                        model.SortId = int.Parse(strList[1].ToString());
                    }
                    else
                    {
                        model.SortId = 0;
                    }
                    model.TourCount = 0;
                    modelList.Add(model);
                }
            }
            //添加国际线模块
            if (OutVal != null && OutVal.Length > 0)
            {
                EyouSoft.Model.SystemStructure.SysCityAreaControl model = null;
                foreach (string OutM in OutVal)
                {
                    model = new EyouSoft.Model.SystemStructure.SysCityAreaControl();
                    string[] strList = OutM.Split('_');
                    string[] areaInfo = strList[0].Split('|');
                    model.AreaId = int.Parse(areaInfo[0]);
                    model.AreaName = areaInfo[1];
                    model.CityId = CityID;
                    model.CityName = "";
                    if (strList[2] != null)
                    {
                        if (strList[2].ToString() == "1")
                            model.IsDefaultShow = true;
                        else
                            model.IsDefaultShow = false;
                    }
                    else
                    {
                        model.IsDefaultShow = false;
                    }
                    model.ProvinceId = ProvinceID;
                    model.ProvinceName = "";
                    model.RouteType = EyouSoft.Model.SystemStructure.AreaType.国际线;
                    if (strList[1] != null && StringValidate.IsInteger(strList[1].ToString()))
                    {
                        model.SortId = int.Parse(strList[1].ToString());
                    }
                    else
                    {
                        model.SortId = 0;
                    }
                    model.TourCount = 0;
                    modelList.Add(model);
                }
            }
            //执行操作
            EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().UpdateSysSiteAreaControl( CityID,modelList);
            //释放资源
            modelList = null;
            MessageBox.ShowAndRedirect(this, "设置线路区域成功！", "ChooseRouteAgency.aspx?CityID=" + CityID);
        }
        #endregion
    }
}
