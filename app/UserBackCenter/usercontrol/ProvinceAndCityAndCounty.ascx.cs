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
namespace SiteOperationsCenter.usercontrol
{
    public partial class ProvinceAndCityAndCounty : System.Web.UI.UserControl
    {
        private int _setProvinceId = 0;

        /// <summary>
        /// 默认选中省份ID
        /// </summary>
        public int SetProvinceId
        {
            get { return _setProvinceId; }
            set { _setProvinceId = value; }
        }
        private int _setCityId = 0;
        /// <summary>
        /// 默认选中城市ID
        /// </summary>
        public int SetCityId
        {
            get { return _setCityId; }
            set { _setCityId = value; }
        }
        private int _setCountyId = 0;
        /// <summary>
        /// 默认选中县区ID
        /// </summary>
        public int SetCountyId
        {
            get { return _setCountyId; }
            set { _setCountyId = value; }
        }

        private bool _isShowRequired = false;
        /// <summary>
        /// 是否显示必填（*）信息
        /// </summary>
        public bool IsShowRequired
        {
            get { return _isShowRequired; }
            set { _isShowRequired = value; }
        }
        #region
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "');");
            BindProvince();
            //BindCity();
            /* 默认选中项 */
            if (SetProvinceId > 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + SetProvinceId + ");ChangeList('" + this.ddl_CityList.ClientID + "','" + this.ddl_CountyList.ClientID + "'," + SetProvinceId + ");", true);
                if (SetCityId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity(" + SetCityId + ");ChangeCountyList('" + SetCountyId + "','" + this.ddl_CityList.ClientID + "','" + this.ddl_ProvinceList.ClientID + "');", true);
                    if (SetCountyId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCounty(" + SetCountyId + ");", true);
                    }
                }
            }
            if (IsShowRequired)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$('#ProvinceRequired').show();$('#CityRequired').show();$('#CountyRequired').show();", true);
            }
        }
        #endregion

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            //this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "'，'" + this.dll_CountyList.ClientID + "', this.options[this.selectedIndex].value)"));

            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "','" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value);"));

            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", "0"));

            //this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "',this.options[this.selectedIndex].value，'" + this.ddl_ProvinceList.ClientID + "')"));
            this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value,'" + this.ddl_ProvinceList.ClientID + "');"));
            this.ddl_CountyList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        #endregion
    }
}