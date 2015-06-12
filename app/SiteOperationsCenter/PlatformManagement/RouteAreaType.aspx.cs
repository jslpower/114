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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.Text;


namespace SiteOperationsCenter.PlatformManagement
{
    /// <summary>
    /// 页面功能：线路区域分类
    /// 开发人：杜桂云      开发时间：2010-07-05
    /// </summary>
    public partial class RouteAreaType : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int ProvinceId = 0;
        protected bool EditFlag = false;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.平台管理_线路区域分类 };
            EditFlag=CheckMasterGrant(parms);

            if (!EditFlag)
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_线路区域分类, true);
                return;
            }
            else
            {
                BindProvince();
                BindShortProvince();
                //给省份或者城市对应的已选列表控件传类型值：0—长线   1—短线
                this.AreaList1.TypeID = 0;
                this.AreaList2.TypeID = 1;
            }
        }
        #endregion

        #region 绑定长线的省份
        //绑定省份
        private void BindProvince()
        {
            string divStr = "";
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                //int index = 0;
                divStr = "<ul><li>请选择省份：</li>";
                foreach (EyouSoft.Model.SystemStructure.SysProvince model in ProvinceList)
                {
                    divStr += "<li><a href=\"javascript:void(0)\" onclick=\"ChangeClass(this,'0');ShowAreaAndCompany('" + model.ProvinceId + "','0','0');\" name='a_Province' id='a_Province_" + model.ProvinceId + "' onfocus=\"blur()\" ><nobr>" + model.ProvinceName + "</nobr></a></li>";
                }
                divStr += "</ul>";
            }
            this.div_ShowProvince.InnerHtml = divStr.ToString();
            //释放资源
            ProvinceList = null;
        }
        #endregion

        #region 绑定国内短线的省份
        //绑定省份
        private void BindShortProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.ddl_ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.ddl_ProvinceList.DataSource = ProvinceList;
                this.ddl_ProvinceList.DataTextField = "ProvinceName";
                this.ddl_ProvinceList.DataValueField = "ProvinceId";
                this.ddl_ProvinceList.DataBind();
            }
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择省份", "0"));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList(this.options[this.selectedIndex].value)"));
            //释放资源
            ProvinceList = null;
        }
        #endregion

        #region 添加长线下面省份和线路区域关联信息
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (EditFlag)
            {
                //省份和线路区域关联的底层类
                string[] areaID = this.AreaList1.GetCheckValues;
                string ProID = Utils.GetFormValue("hdProvinceID");
                IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> sysAreaList = new List<EyouSoft.Model.SystemStructure.SysAreaSiteControl>();
                if (areaID != null && areaID.Length > 0)
                {
                    EyouSoft.Model.SystemStructure.SysAreaSiteControl model = null;
                    foreach (string aId in areaID)
                    {
                        string[] aIdInfo = aId.Split('|');
                        model = new EyouSoft.Model.SystemStructure.SysAreaSiteControl();
                        if (!string.IsNullOrEmpty(ProID))
                        {
                            model.AreaId = int.Parse(aIdInfo[0]);
                            model.AreaName = aIdInfo[1];
                            model.CityId = 0;
                            model.ProvinceId = int.Parse(ProID);
                            model.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内长线;
                            sysAreaList.Add(model);
                        }
                    }
                    EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().AddSysAreaSiteControl(sysAreaList);
                    if (!string.IsNullOrEmpty(ProID))
                    {
                        MessageBox.ResponseScript(this, "alert('操作成功!');SetClass('" + ProID + "',0,0);ShowAreaAndCompany('" + ProID + "',0,0);");
                    }
                }
                else
                {
                    MessageBox.ResponseScript(this, "alert('请先选择您要设置的省份和线路区域!');");
                }
                //释放资源
                sysAreaList = null;
            }
            else 
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_线路区域分类,true);
            }
        }
        #endregion

        #region 添加短线下面城市和线路区域关联信息
        protected void btn_SaveShortArea_Click(object sender, EventArgs e)
        {
            if (EditFlag)
            {
                string[] areaID = this.AreaList1.GetShortckvalues;
                string Pro_CityID = Utils.GetFormValue("hdCityID");
                string[] pro_city = Pro_CityID.Split('|');
                IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> sysAreaList = new List<EyouSoft.Model.SystemStructure.SysAreaSiteControl>();
                if (areaID != null && areaID.Length > 0)
                {
                    EyouSoft.Model.SystemStructure.SysAreaSiteControl model = null;
                    foreach (string aInfo in areaID)
                    {
                        model = new EyouSoft.Model.SystemStructure.SysAreaSiteControl();
                        string[] aIdInfo = aInfo.Split('|');
                        if (!string.IsNullOrEmpty(Pro_CityID))
                        {
                            model.ProvinceId = int.Parse(pro_city[0]);
                            model.CityId = int.Parse(pro_city[1]);
                            model.AreaId = int.Parse(aIdInfo[0]);
                            model.AreaName = aIdInfo[1];
                            model.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内短线;
                            sysAreaList.Add(model);
                        }
                    }
                    EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().AddSysAreaSiteControl(sysAreaList);
                    if (pro_city != null && pro_city.Length > 1)
                    {
                        MessageBox.ResponseScript(this, "alert('操作成功!');SetClass('" + pro_city[0] + "','" + pro_city[1] + "',1);ShowAreaAndCompany('" + pro_city[0] + "','" + pro_city[1] + "',1);");
                    }
                }
                else
                {
                    MessageBox.ResponseScript(this, "alert('请先选择您要设置的省份城市和线路区域!');");
                }
                //释放资源
                sysAreaList = null;
            }
            else
            {
                Utils.ResponseNoPermit(YuYingPermission.平台管理_线路区域分类, true);
            }
        }
        #endregion

    }
}
