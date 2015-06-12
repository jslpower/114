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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.usercontrol
{
    /// <summary>
    /// 省份、城市、线路区域级联下拉
    /// 罗丽娥  2010-09-17
    /// </summary>
    public partial class ProvinceAndCityAndAreaList : System.Web.UI.UserControl
    {
        private int _defaultprovinceid = 0;
        /// <summary>
        /// 默认省份编号
        /// </summary>
        public int DefaultProvinceID
        {
            set { _defaultprovinceid = value; }
            get { return _defaultprovinceid; }
        }

        private int _defaultcityid = 0;
        /// <summary>
        /// 默认城市编号
        /// </summary>
        public int DefaultCityID
        {
            set { _defaultcityid = value; }
            get { return _defaultcityid; }
        }

        private int _defaultareaid = 0;
        /// <summary>
        /// 默认城市编号
        /// </summary>
        public int DefaultAreaID
        {
            set { _defaultareaid = value; }
            get { return _defaultareaid; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));

            MessageBox.ResponseScript(this.Page, "BindProvinceList('"+ this.drpProvinceID.ClientID +"');");

            this.drpProvinceID.Attributes.Add("onchange", "ChangeList('"+ this.drpCity.ClientID +"',this.options[this.selectedIndex].value);");

            if (!Page.IsPostBack)   
            {
                //InitAreaList();
                if (DefaultProvinceID > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + DefaultProvinceID + ");ChangeList('" + this.drpCity.ClientID + "'," + DefaultProvinceID + ");", true);

                    if (DefaultCityID > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SetCity(" + DefaultCityID + "," + DefaultProvinceID + ");", true);
                        if (DefaultAreaID > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), "SetArea(" + DefaultAreaID + ");", true);
                        }
                    }
                }
            }
        }

        #region 绑定线路区域
        private void InitAreaList()
        {
            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> list = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaSiteControl();
            StringBuilder strArea = new StringBuilder();
            strArea.Append("\nvar AreaCount = 0;");
            strArea.Append("\narrArea = new Array();");
            if (list != null && list.Count > 0)
            {
                int index = 0;
                foreach(EyouSoft.Model.SystemStructure.SysAreaSiteControl model in list)
                {
                    strArea.AppendFormat("\narrArea[{0}]= new Array('{1}','{2}','{3}');",index.ToString(),model.AreaId,model.AreaName,model.CityId);
                    index++;
                }
                strArea.Append("\nAreaCount = " + index.ToString() + ";");

                strArea.Append("\nfunction ChangeAreaList(AreaTextID,CityID){");
                strArea.Append("\nvar obj = document.getElementById(AreaTextID);");
                strArea.Append("\nobj.length = 0;");
                strArea.Append("\nobj.options[0] = new Option('请选择线路区域','0');");
                strArea.Append("\nif(CityID == 0)");
                strArea.Append("\n{obj.options[0].selected = true;return;}");
                strArea.Append("\nvar index = 1;");
                strArea.Append("\nfor(var i = 0; i < AreaCount; i ++ )");
                strArea.Append("\n{if(arrArea[i][0] == CityID)");
                strArea.Append("\n{obj.options[index] = new Option(arrArea[i][1],arrArea[i][0]);index+1;}");
                strArea.Append("\n}\n}");
            }
            MessageBox.ResponseScript(this.Page, strArea.ToString());

            this.drpCity.Attributes.Add("onchange", "ChangeAreaList('drpAreaID',this.options[this.selectedIndex].value);");
            list = null;
        }
        #endregion
    }
}