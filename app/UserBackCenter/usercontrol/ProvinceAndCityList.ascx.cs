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
using System.Text;

namespace UserBackCenter.usercontrol
{
    /// <summary>
    /// 页面功能：身份和城市的联动菜单
    /// 开发人：杜桂云      开发时间：2010-07-05
    /// ---------------------------
    /// 新增功能：默认选中省份/城市
    /// 修改人：刘玉灵      时间：2010-7-8
    /// </summary>
    public partial class ProvinceAndCityList : System.Web.UI.UserControl
    {
        protected int _ProvinceId = 0;
        /// <summary>
        /// 设置要显示的省份ID
        /// </summary>
        public int ProvinceId
        {
            get
            {
                return _ProvinceId;
            }
            set
            {
                _ProvinceId = value;
            }
        }

        protected int _CityId = 0;
        /// <summary>
        /// 设置要显示的城市ID
        /// </summary>
        public int CityId
        {
            get
            {
                return _CityId;
            }
            set
            {
                _CityId = value;
            }
        }


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
        private bool _isShowRequired = false;
        /// <summary>
        /// 是否显示必填（*）信息
        /// </summary>
        public bool IsShowRequired
        {
            get { return _isShowRequired; }
            set { _isShowRequired = value; }
        }

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindProvince();
                BindCity();
                /* 默认选中项 */
                if (SetProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + SetProvinceId + ");ChangeList('" + this.ddl_CityList.ClientID + "'," + SetProvinceId + ");", true);
                    if (SetCityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity(" + SetCityId + ");", true);
                    }
                }
                if (IsShowRequired)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$('#ProvinceRequired').show();$('#CityRequired').show()", true);
                }
            }
            
        }
        #endregion

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList=EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.ddl_ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.ddl_ProvinceList.DataSource = ProvinceList;
                this.ddl_ProvinceList.DataTextField = "ProvinceName";
                this.ddl_ProvinceList.DataValueField = "ProvinceId";
                this.ddl_ProvinceList.DataBind();
            }
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", ""));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "', this.options[this.selectedIndex].value)"));
        }

        //绑定城市
        private void BindCity()
        {
            //获取城市列表
            IList<EyouSoft.Model.SystemStructure.SysCity> cList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList();
            this.ddl_CityList.Items.Clear();
            StringBuilder strCity = new StringBuilder();
            strCity.Append("\nvar CityCount;\n");
            strCity.Append("arrCity = new Array();\n");
            if (cList != null && cList.Count > 0)
            {
                int index = 0;
                foreach (EyouSoft.Model.SystemStructure.SysCity model in cList)
                {
                    ListItem item = new ListItem(model.CityName, model.CityId.ToString());
                    //数组下标，城市名称，城市ID，该城市所属的省份ID
                    //每行new一个含3列的的数组
                    string str = string.Format("\narrCity[{0}]=new Array('{1}', {2}, {3});", index.ToString(),model.CityName,  model.CityId.ToString(),  model.ProvinceId.ToString());
                    strCity.Append(str);
                    index++;
                }
                strCity.Append("\nCityCount=" + index.ToString() + ";");
                strCity.Append("\nfunction ChangeList(CityTextId, ProvinceId)");
                strCity.Append("\n{var Obj = document.getElementById(CityTextId);"); //获得要被绑定小类的控件对象
                strCity.Append("\nObj.length = 0;");
                strCity.Append("\nObj.options[0] = new Option('请选择', '');");
                strCity.Append("if(ProvinceId==0)");
                strCity.Append("{Obj.options[0].selected=true;return;}");
                strCity.Append("\nvar index=1;");  //smallListObj之后的options下标从1开始                
                strCity.Append("\nfor(var i=0; i<CityCount; i++)");
                //Array数组行和列的下标都是从0开始的
                //arrCity的第i行第3列(下标为2<从0开始>)，即为大类的ID，若选择的大类ID等于当前循环的，则取出该小类
                strCity.Append("\n{if(arrCity[i][2]==ProvinceId)");
                //根据该大类ID，得到小类名称和ID，复制给options的Text和Value值
                strCity.Append("\n{Obj.options[index]=new Option(arrCity[i][0],arrCity[i][1]);\nindex=index+1;}");
                strCity.Append("\n}");
                strCity.Append("\n}");

                //注册该JS
                MessageBox.ResponseScript(this.Page,strCity.ToString());
                //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(),"Provifdsjflsd",
            }
            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", ""));
        }
        #endregion

    }
}