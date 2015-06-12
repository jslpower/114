using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Model.ScenicStructure;

namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 景区详细信息页面
    /// 功能：添加或修改景区信息
    /// 创建人：方琪
    /// 创建时间： 2011-10-21  
    /// </summary>
    public partial class AddOrUpdateScenice : EyouSoft.Common.Control.BasePage
    {
        protected string EditId = string.Empty;
        protected string returnUrl = string.Empty;
        protected string CompanyId = string.Empty;
        protected string SceniceId = string.Empty;
        protected StringBuilder LankMarks = new StringBuilder();
        protected StringBuilder ThemeCheckBox = new StringBuilder();
        protected IList<EyouSoft.Model.ScenicStructure.MScenicTheme> themeList = null;
        protected IList<EyouSoft.Model.SystemStructure.MSystemLandMark> LankMarksList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Request.QueryString["EditId"];
            CompanyId = this.SiteUserInfo.CompanyID;
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
                initPage();
                if (!IsLogin)
                {
                    EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
                }
                if (EditId != null)
                {
                    //页面赋值
                    GetSceniceInfo();
                    SceniceId = EditId.ToString();
                }
                else
                {
                    BindThemecheck();
                }
            }

            string type = Utils.GetQueryStringValue("type");
            int argument = Utils.GetInt(Utils.GetQueryStringValue("arg"));
            if (type == "BinLankId" && argument != 0)
            {
                Response.Clear();
                Response.Write(GetLandMark(argument));
                Response.End();
            }
        }

        #region 获取地标
        /// <summary>
        /// 获取地标
        /// </summary>
        /// <param name="argument">参数城市编号</param>
        /// <returns>html</returns>
        protected string GetLandMark(int argument)
        {
            int index = 0;
            StringBuilder sb = new StringBuilder();
            System.Collections.Generic.IList<EyouSoft.Model.SystemStructure.MSystemLandMark> LandMarklist = EyouSoft.BLL.SystemStructure.BSystemLandMark.CreateInstance().GetList(argument);
            if (LandMarklist != null)
            {
                if (LandMarklist.Count > 0)
                {
                    foreach (EyouSoft.Model.SystemStructure.MSystemLandMark LandMark in LandMarklist)
                    {
                        index++;
                        sb.Append(string.Format("<input type=\"checkbox\" name=\"LandMark\" value=\"{0}\" id=\"cbxl_{1}\" /><label for=\"cbxl_{1}\">{2}</label> &nbsp;", LandMark.Id, index, LandMark.Por));
                    }
                    return sb.ToString();
                }
                else
                {
                    return string.Format("<label>暂无数据</label>");
                }
            }
            else
            {
                return string.Format("<label>暂无数据</label>");
            }
        }
        #endregion


        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void initPage()
        {
            BindProvince();
            //绑定景区等级
            this.ScenicLevel.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.ScenicStructure.ScenicLevel));
            this.ScenicLevel.DataTextField = "Text";
            this.ScenicLevel.DataValueField = "Value";
            this.ScenicLevel.DataBind();
            this.ScenicLevel.Items.Insert(0, new ListItem("请选择", "0"));

            //绑定联系人
            EyouSoft.Model.CompanyStructure.QueryParamsUser QueryParams = new EyouSoft.Model.CompanyStructure.QueryParamsUser();
            QueryParams.IsShowAdmin = true;
            IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> contactOperator = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(CompanyId, QueryParams);

            //  "00005578-903A-4E65-8B2D-2EAB3A21D5CE"
            string selectValue = "";
            foreach (var item in contactOperator)
            {
                ListItem li = new ListItem();
                li.Text = item.ContactInfo.ContactName + " | " + item.UserName;
                li.Value = item.ID;
                if (item.IsAdmin)
                {
                    selectValue = item.ID;
                }
                this.ContactOperator.Items.Add(li);
            }
            this.ContactOperator.Items[Utils.GetInt(selectValue)].Selected = true;
        }

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.dropProvinceId.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.dropProvinceId.DataSource = ProvinceList;
                this.dropProvinceId.DataTextField = "ProvinceName";
                this.dropProvinceId.DataValueField = "ProvinceId";
                this.dropProvinceId.DataBind();
            }
            this.dropProvinceId.Items.Insert(0, new ListItem("请选择", "0"));
            this.dropProvinceId.Attributes.Add("onchange", string.Format("ChangeList('" + this.dropCityId.ClientID + "', this.options[this.selectedIndex].value)"));

            this.dropCityId.Items.Insert(0, new ListItem("请选择", "0"));
            this.dropCityId.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.dropCountyId.ClientID + "', this.options[this.selectedIndex].value,'" + this.dropProvinceId.ClientID + "');BinLankId();"));
            this.dropCountyId.Items.Insert(0, new ListItem("请选择", "0"));
            //this.dropCountyId.Attributes.Add("onchange", string.Format("BinLankId()"));

        }
        #endregion

        private void BindThemecheck()
        {
            ThemeCheckBox = ThemeCheckBox.Remove(0, ThemeCheckBox.Length);
            themeList = EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList();
            //绑定主题
            int index = 0;
            foreach (EyouSoft.Model.ScenicStructure.MScenicTheme theme in themeList)
            {
                index++;

                ThemeCheckBox.Append(string.Format("<input type=\"checkbox\" name=\"ThemeCheck\" value=\"{0}\" id=\"cbx_{1}\" valid=\"requireChecked\" min=\"1\" onchange=\"maxSum('cbx_{1}')\" errmsgend=\"ThemeCheck\" errmsg=\"至少选择一个景区主题！\"/><label for=\"cbx_{1}\">{2}</label>  &nbsp;", theme.ThemeId, index, theme.ThemeName));

            }
        }

        /// <summary>
        /// 保存或添加景区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.ScenicStructure.MScenicArea Model = new EyouSoft.Model.ScenicStructure.MScenicArea();
            Model.ContactOperator = Utils.InputText(Utils.GetFormValue(ContactOperator.UniqueID));
            Model.Company = new EyouSoft.Model.CompanyStructure.CompanyInfo();
            Model.Company.ID = CompanyId;
            Model.Operator = this.SiteUserInfo.UserName;
            Model.ScenicName = Utils.InputText(this.SceniceName.Value.Trim());
            Model.EnName = Utils.InputText(this.EnName.Value.Trim());
            Model.X = Utils.InputText(this.jingdu.Value);
            Model.Y = Utils.InputText(this.weidu.Value);
            Model.Telephone = Utils.InputText(this.Telephone.Value.Trim());
            Model.CnAddress = Utils.InputText(this.CnAddress.Value.Trim());
            Model.EnAddress = Utils.InputText(this.EnAddress.Value.Trim());
            Model.ScenicLevel = (ScenicLevel)Utils.GetInt(Utils.GetFormValue(ScenicLevel.UniqueID));
            Model.SetYear = Utils.GetInt(this.SetYear.Value.Trim());
            Model.OpenTime = Utils.InputText(this.OpenTime.Value);
            Model.Description = Utils.EditInputText(this.txtDescription.Text);
            Model.Traffic = Utils.InputText(this.Traffic.Value);
            Model.Facilities = Utils.InputText(this.Facilities.Value);
            Model.Notes = Utils.InputText(this.Notes.Value);
            Model.ProvinceId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropProvinceId.UniqueID));
            Model.CityId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropCityId.UniqueID));
            Model.CountyId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropCountyId.UniqueID));

            #region
            //Model.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ProvinceAndCityAndCounty1.FindControl("ddl_ProvinceList").UniqueID));
            //Model.CityId = Utils.GetInt(Utils.GetFormValue(this.ProvinceAndCityAndCounty1.FindControl("ddl_CityList").UniqueID));
            //Model.CountyId = Utils.GetInt(Utils.GetFormValue(this.ProvinceAndCityAndCounty1.FindControl("ddl_CountyList").UniqueID));
            #endregion

            #region 获取主题
            List<MScenicTheme> listi = new List<MScenicTheme>();
            foreach (string item in Utils.GetFormValues("ThemeCheck"))
            {
                MScenicTheme modelTheme = new MScenicTheme();
                modelTheme.ThemeId = Utils.GetInt(item);
                modelTheme.ThemeName = "";
                listi.Add(modelTheme);
                modelTheme = null;
            }
            Model.ThemeId = listi;
            #endregion

            #region  获取地标
            List<MScenicRelationLandMark> listland = new List<MScenicRelationLandMark>();
            foreach (string item in Utils.GetFormValues("LandMark"))
            {
                MScenicRelationLandMark ModelLandMard = new MScenicRelationLandMark();
                ModelLandMard.LandMarkId = Utils.GetInt(item);
                ModelLandMard.ScenicId = "";
                listland.Add(ModelLandMard);
                ModelLandMard = null;
            }
            #endregion
            Model.LankId = listland;
            #region 判断是添加操作还是修改操作
            //判断是添加操作还是修改操作！
            if (EditId != null && EditId != "")
            {
                Model.ScenicId = EditId;
                EyouSoft.Model.ScenicStructure.MScenicArea oldModel = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(EditId);
                Model.B2B = oldModel.B2B;
                Model.B2BOrder = oldModel.B2BOrder;
                Model.B2C = oldModel.B2C;
                Model.B2COrder = oldModel.B2COrder;
                bool resut = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().Update(Model);
                if (resut)
                {
                    MessageBox.ResponseScript(this.Page, "alert(\"修改成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();parent.topTab.url(parent.topTab.activeTabIndex,'/ScenicManage/MyScenice.aspx');");
                }
                else
                {
                    MessageBox.ResponseScript(this.Page, "alert('修改失败!');");
                }

            }
            else
            {
                Model.B2B = ScenicB2BDisplay.常规;
                Model.B2C = ScenicB2CDisplay.常规;
                bool resut = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().Add(Model);
                if (resut)
                {
                    MessageBox.ResponseScript(this.Page, "alert(\"增加成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();parent.topTab.url(parent.topTab.activeTabIndex,'/ScenicManage/MyScenice.aspx');");
                }
                else
                {
                    MessageBox.ResponseScript(this.Page, "alert('增加失败!');");
                }

            }
            #endregion
            Model = null;
        }

        #region 初始化景区信息
        /// <summary>
        /// 初始化景区信息
        /// </summary>
        protected void GetSceniceInfo()
        {
            EyouSoft.Model.ScenicStructure.MScenicArea Model = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(EditId, CompanyId);
            if (Model != null)
            {
                this.SceniceName.Value = Model.ScenicName;
                this.EnName.Value = Model.EnName;
                this.X.InnerText = Model.X;
                this.jingdu.Value = Model.X;
                this.Y.InnerText = Model.Y;
                this.weidu.Value = Model.Y;
                this.Telephone.Value = Model.Telephone;
                this.CnAddress.Value = Model.CnAddress;
                this.EnAddress.Value = Model.EnAddress;
                this.ScenicLevel.SelectedValue = ((int)Model.ScenicLevel).ToString();
                this.ContactOperator.SelectedValue = Model.ContactOperator.ToString();
                this.SetYear.Value = Model.SetYear == 0 ? "" : Model.SetYear.ToString();
                this.OpenTime.Value = Model.OpenTime;
                this.txtDescription.Text = Model.Description;
                this.Traffic.Value = Model.Traffic;
                this.Facilities.Value = Model.Facilities;
                this.Notes.Value = Model.Notes;
                //加载景区主题
                BindSceniceThemeId(Model.ThemeId);
                //加载相关城市的地标区域
                BindBinLankIdByCity(Model.CityId, Model.LankId);
                //省份 城市
                if (Model.ProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince('" + Model.ProvinceId + "');ChangeList('" + this.dropCityId.ClientID + "','" + Model.ProvinceId + "');", true);
                    if (Model.CityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity('" + Model.CityId + "');ChangeCountyList('" + this.dropCountyId.ClientID + "','" + Model.CityId + "','" + this.dropProvinceId.ClientID + "');", true);
                        if (Model.CountyId > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCounty('" + Model.CountyId + "');", true);
                        }
                    }
                }
            }
            Model = null;
        }
        #endregion

        #region
        /// <summary>
        /// 勾上model里的被选中的选择框
        /// </summary>
        /// <param name="lankId">景区地标model</param>
        private void InitCheckBox(IList<MScenicRelationLandMark> lankId)
        {
            if (lankId == null || lankId.Count < 1)
            {
                return;
            }
            var strJs = new StringBuilder();
            strJs.Append(" var arrData = new Array(); ");
            for (int i = 0; i < lankId.Count; i++)
            {
                strJs.AppendFormat(" arrData[{0}] = {1}; ", i, lankId[i].LandMarkId);
            }
            strJs.Append(" function CheckedBox() { ");
            strJs.Append(" for (var = 0;i < arrData.length;i++) { ");
            strJs.Append(" $(\"intput[type='checkbox'][name='LandMark']\").each(function(i) { if($(this).val() == arrData[i]) { $(this).attr(\"checked\",\"checked\") }  }); ");
            strJs.Append(" } ");
            strJs.Append(" } ");
            EyouSoft.Common.Function.MessageBox.ResponseScript(this, strJs.ToString());
        }
        #endregion


        #region 城市地标区域的绑定
        /// <summary>
        /// 获取该城市下的所有地标
        /// </summary>
        /// <param name="CityId">城市编号</param>
        protected void BindBinLankIdByCity(int CityId, IList<MScenicRelationLandMark> LandMark)
        {
            List<int> IntList = new List<int>();
            LankMarksList = EyouSoft.BLL.SystemStructure.BSystemLandMark.CreateInstance().GetList(CityId);
            if (LandMark != null)
            {
                if (LandMark.Count > 0)
                {
                    foreach (MScenicRelationLandMark item in LandMark)
                    {
                        IntList.Add(item.LandMarkId);
                    }
                }
            }
            if (LankMarksList.Count > 0)
            {
                LankMarks = new StringBuilder();
                int index = 0;
                foreach (EyouSoft.Model.SystemStructure.MSystemLandMark LankMark in LankMarksList)
                {
                    index++;
                    if (IntList.Count > 0)
                    {
                        if (IntList.Contains(LankMark.Id))
                        {
                            LankMarks.Append(string.Format("<input type=\"checkbox\" name=\"LandMark\" checked=\"checked\" value=\"{0}\" id=\"cbxl_{1}\" /><label for=\"cbxl_{1}\">{2}</label>  &nbsp;", LankMark.Id, index, LankMark.Por));
                        }
                        else
                        {
                            LankMarks.Append(string.Format("<input type=\"checkbox\" name=\"LandMark\" value=\"{0}\" id=\"cbxl_{1}\" /><label for=\"cbxl_{1}\">{2}</label>  &nbsp;", LankMark.Id, index, LankMark.Por));
                        }
                    }
                    else
                    {
                        LankMarks.Append(string.Format("<input type=\"checkbox\" name=\"LandMark\" value=\"{0}\" id=\"cbxl_{1}\" /><label for=\"cbxl_{1}\">{2}</label>  &nbsp;", LankMark.Id, index, LankMark.Por));
                    }
                }
            }
        }
        #endregion

        #region 景区主题绑定
        /// <summary>
        /// 景区主题绑定
        /// </summary>
        /// <param name="SceniceTheme"></param>
        protected void BindSceniceThemeId(IList<MScenicTheme> SceniceTheme)
        {
            int index = 0;
            ThemeCheckBox = ThemeCheckBox.Remove(0, ThemeCheckBox.Length);
            List<int> IntList = new List<int>();
            if (SceniceTheme != null)
            {
                if (SceniceTheme.Count > 0)
                {
                    foreach (MScenicTheme item in SceniceTheme)
                    {
                        IntList.Add(item.ThemeId);
                    }
                }
            }
            themeList = EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList();
            foreach (EyouSoft.Model.ScenicStructure.MScenicTheme theme in themeList)
            {
                index++;
                if (IntList.Count > 0)
                {
                    if (IntList.Contains(theme.ThemeId))
                    {
                        ThemeCheckBox.Append(string.Format("<input type=\"checkbox\" name=\"ThemeCheck\" checked=\"checked\" value=\"{0}\" id=\"cbx_{1}\" valid=\"requireChecked\" min=\"1\" onchange=\"maxSum('cbx_{1}')\" errmsgend=\"ThemeCheck\" errmsg=\"至少选择一个景区主题！\"/><label for=\"cbx_{1}\">{2}</label>  &nbsp;", theme.ThemeId, index, theme.ThemeName));
                    }
                    else
                    {
                        ThemeCheckBox.Append(string.Format("<input type=\"checkbox\" name=\"ThemeCheck\" value=\"{0}\" id=\"cbx_{1}\" valid=\"requireChecked\" min=\"1\" onchange=\"maxSum('cbx_{1}')\" errmsgend=\"ThemeCheck\" errmsg=\"至少选择一个景区主题！\"/><label for=\"cbx_{1}\">{2}</label>  &nbsp;", theme.ThemeId, index, theme.ThemeName));
                    }
                }
                else
                {
                    ThemeCheckBox.Append(string.Format("<input type=\"checkbox\" name=\"ThemeCheck\" value=\"{0}\" id=\"cbx_{1}\" valid=\"requireChecked\" min=\"1\" onchange=\"maxSum('cbx_{1}')\" errmsgend=\"ThemeCheck\" errmsg=\"至少选择一个景区主题！\"/><label for=\"cbx_{1}\">{2}</label>  &nbsp;", theme.ThemeId, index, theme.ThemeName));
                }
            }
        }
        #endregion
    }
}
