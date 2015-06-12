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
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.Text;
using System.Collections.Generic;

namespace SiteOperationsCenter.ScenicManage
{
    public partial class AddScenic : EyouSoft.Common.Control.YunYingPage
    {
        protected string loginis = "";
        protected string[] ScenicIdAndComId = Utils.GetQueryStringValue("ScenicId").Split('$');
        public StringBuilder strchb = new StringBuilder("");
        // string method = Utils.InputText(Request.QueryString["method"]);
        #region
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 权限判断
            //查询
            //判断是否有【景区_景区管理栏目管理】权限，如果没有改权限，则不能查看改页面
            //if (!this.CheckMasterGrant(YuYingPermission.景区_景区管理栏目管理))
            //{
            //    Utils.ResponseNoPermit(YuYingPermission.景区_景区管理栏目管理, true);
            //    return;
            //}
            //IsUpdateGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目修改);
            //IsDeleteGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目删除);
            //IsAddGant = CheckMasterGrant(YuYingPermission.景区_景区管理栏目新增);
            #endregion
            //初始化枚举绑定
            BindEnum();


            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "');");
            BindProvince();
            //BindCity();
            /* 默认选中项 */
            if (SetProvinceId > 0)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + SetProvinceId + ");ChangeList('" + this.ddl_CityList.ClientID + "'," + this.ddl_CountyList.ClientID + "," + SetProvinceId + ");", true);
                if (SetCityId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity(" + SetCityId + ");ChangeCountyList('" + SetCountyId + "','" + this.ddl_CityList.ClientID + "','" + this.ddl_ProvinceList.ClientID + "')", true);
                    if (SetCountyId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCounty(" + SetCountyId + ");", true);
                    }
                }
            }
            if (IsShowRequired)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$('#ProvinceRequired').show();$('#CityRequired').show();$('#CountyRequired').show()", true);
            }
            if (!IsPostBack)
                intoData();
        }




        #region 取消
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            MessageBox.LocationUrl(this, "ScenicList.aspx");
        }
        #endregion

        #region 初始化数据

        protected void intoData()
        {

            if (ScenicIdAndComId.Length == 2)
            {
                var modelScenic = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(ScenicIdAndComId[0], ScenicIdAndComId[1]);
                if (modelScenic != null)
                {
                    #region 初始化省市县
                    if (modelScenic.ProvinceId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.ddl_ProvinceList.ClientID + "','" + modelScenic.ProvinceId + "');", true);
                        if (modelScenic.CityId > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.ddl_CityList.ClientID + "','" + modelScenic.ProvinceId + "','" + modelScenic.CityId + "');", true);
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.ddl_CountyList.ClientID + "','" + modelScenic.ProvinceId + "','" + modelScenic.CityId + "','" + modelScenic.CountyId + "');", true);
                        }
                        else
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.ddl_CityList.ClientID + "','" + modelScenic.ProvinceId + "','" + modelScenic.CityId + "');", true);
                    }
                    #endregion

                    #region 初始化地标
                    StringBuilder str = new StringBuilder();
                    List<int> listlankaid = new List<int>();
                    int index = 1;
                    var listlank = EyouSoft.BLL.SystemStructure.BSystemLandMark.CreateInstance().GetList(modelScenic.CityId);
                    if (modelScenic.LankId != null)
                    {
                        foreach (var item in modelScenic.LankId)
                        {
                            if (item.LandMarkId != 0)
                                listlankaid.Add(item.LandMarkId);
                        }
                    }
                    foreach (var item in listlank)
                    {
                        if (listlankaid.Contains(item.Id))
                            str.Append("<input type=\"checkbox\" name=\"chkboxLankid\" checked=\"checked\" value=\"" + item.Id + "\" id=\"cbx_" + item.Id + "\" /><label for=\"cbx_" + item.Id + "\">" + item.Por + "</label>");
                        else
                            str.Append("<input type=\"checkbox\" name=\"chkboxLankid\" value=\"" + item.Id + "\" id=\"cbx_" + item.Id + "\" /><label for=\"cbx_" + item.Id + "\">" + item.Por + "</label>");
                        if (index % 5 == 0) { str.Append("<br>"); }
                        index = index + 1;
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$(\"#Td_ChbLankId\").html('" + str + "')", true);
                    #endregion

                    DdlB2B.SelectedValue = ((int)modelScenic.B2B).ToString();
                    if (modelScenic.B2BOrder > 0)
                        txt_B2BOrder.Value = modelScenic.B2BOrder.ToString();
                    DdlB2C.SelectedValue = ((int)modelScenic.B2C).ToString();
                    if (modelScenic.B2COrder > 0)
                        txt_B2COrder.Value = modelScenic.B2COrder.ToString();
                    txt_CnAddress.Value = modelScenic.CnAddress;
                    txt_CompanyId.Value = modelScenic.Company.ID;
                    ScenicContact.SelectedValue = modelScenic.ContactOperator;
                    txt_Description.Value = modelScenic.Description;
                    txt_EnAddress.Value = modelScenic.EnAddress;
                    txt_EnName.Value = modelScenic.EnName;
                    txt_Facilities.Value = modelScenic.Facilities;
                    txt_OpenTime.Value = modelScenic.OpenTime;
                    txt_ScenicName.Value = modelScenic.ScenicName;
                    txt_SetYear.Value = modelScenic.SetYear == 0 ? "" : modelScenic.SetYear.ToString();
                    txt_Telephone.Value = modelScenic.Telephone;
                    txt_Traffic.Value = modelScenic.Traffic;
                    DdlHotelStar.SelectedValue = ((int)modelScenic.ScenicLevel).ToString();
                    X.InnerHtml = modelScenic.X;
                    jingdu.Value = modelScenic.X;
                    Y.InnerHtml = modelScenic.Y;
                    weidu.Value = modelScenic.Y;
                    DdlStatus.SelectedValue = ((int)modelScenic.Status).ToString();
                    DdlHotelStar.SelectedValue = ((int)modelScenic.ScenicLevel).ToString();
                    BindThemId(modelScenic.ThemeId);

                }

            }
        }

        #region 绑定主题
        private void BindThemId(IList<MScenicTheme> thembid)
        {
            strchb = new StringBuilder("");
            IList<int> listthemb = new List<int>();
            if (thembid != null)
            {
                foreach (var model in thembid)
                {
                    if (model.ThemeId > 0)
                        listthemb.Add(model.ThemeId);
                }
            }
            foreach (var item in EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList())
            {
                if (listthemb != null)
                {
                    if (listthemb.Contains(item.ThemeId))
                        strchb.Append(string.Format("<input type=\"checkbox\" checked=\"checked\" name=\"chbTheme\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", item.ThemeId, item.ThemeId, item.ThemeName));
                    else
                        strchb.Append(string.Format("<input type=\"checkbox\" name=\"chbTheme\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", item.ThemeId, item.ThemeId, item.ThemeName));
                }
                else
                    strchb.Append(string.Format("<input type=\"checkbox\" name=\"chbTheme\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", item.ThemeId, item.ThemeId, item.ThemeName));
            }

        }
        #endregion

        public void BindEnum()
        {
            //景区等级绑定

            this.DdlHotelStar.DataSource = EnumObj.GetList(typeof(ScenicLevel));
            this.DdlHotelStar.DataTextField = "Text";
            this.DdlHotelStar.DataValueField = "Value";
            this.DdlHotelStar.DataBind();
            this.DdlHotelStar.Items.Insert(0, new ListItem("不限", "0"));


            //B2B显示绑定
            this.DdlB2B.DataSource = EnumObj.GetList(typeof(ScenicB2BDisplay));
            this.DdlB2B.DataTextField = "Text";
            this.DdlB2B.DataValueField = "Value";
            this.DdlB2B.DataBind();
            DdlB2B.SelectedValue = ((int)ScenicB2BDisplay.常规).ToString();


            //B2C显示绑定
            this.DdlB2C.DataSource = EnumObj.GetList(typeof(ScenicB2CDisplay));
            this.DdlB2C.DataTextField = "Text";
            this.DdlB2C.DataValueField = "Value";
            this.DdlB2C.DataBind();
            DdlB2C.SelectedValue = ((int)ScenicB2CDisplay.常规).ToString();

            //审核状态绑定
            this.DdlStatus.DataSource = EnumObj.GetList(typeof(ExamineStatus));
            this.DdlStatus.DataTextField = "Text";
            this.DdlStatus.DataValueField = "Value";
            this.DdlStatus.DataBind();


            foreach (var item in EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList())
            {
                strchb.Append(string.Format("<input type=\"checkbox\"   name=\"chbTheme\" value=\"{0}\" id=\"cbx_{1}\" /><label for=\"cbx_{1}\">{2}</label>", item.ThemeId, item.ThemeId, item.ThemeName));
            }

            //this.chbThemeId.DataSource = EyouSoft.BLL.ScenicStructure.BScenicTheme.CreateInstance().GetList();
            //this.chbThemeId.DataBind();

        }
        #endregion

        #region 保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MScenicArea modelScenicArea = new MScenicArea();
            if (string.IsNullOrEmpty(ScenicIdAndComId[0]))
            {
                modelScenicArea.ScenicId = Guid.NewGuid().ToString();
                modelScenicArea.IssueTime = DateTime.Now;
            }
            else
            {
                modelScenicArea = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetModel(ScenicIdAndComId[0], Utils.GetFormValue(txt_CompanyId.UniqueID));
            }

            modelScenicArea.B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), Utils.GetFormValue(DdlB2B.UniqueID), true);
            modelScenicArea.B2BOrder = Utils.GetInt(Utils.GetFormValue(txt_B2BOrder.UniqueID), 50);
            modelScenicArea.B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), Utils.GetFormValue(DdlB2C.UniqueID), true);
            modelScenicArea.B2COrder = Utils.GetInt(Utils.GetFormValue(txt_B2COrder.UniqueID), 50);

            #region  获取用户控件中省市县的数据
            modelScenicArea.ProvinceId = Utils.GetInt(Utils.GetFormValue("ddl_ProvinceList"));
            modelScenicArea.CityId = Utils.GetInt(Utils.GetFormValue("ddl_CityList"));
            modelScenicArea.CountyId = Utils.GetInt(Utils.GetFormValue("ddl_CountyList"));
            #endregion
            #region 公司信息
            if (!string.IsNullOrEmpty(Utils.GetFormValue(txt_CompanyId.UniqueID)))
            {

                modelScenicArea.Company = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(Utils.GetFormValue("txt_CompanyId"));
            }
            else
                MessageBox.Show(Page, "请输入公司编号");
            #endregion
            modelScenicArea.ContactName = "";
            modelScenicArea.ContactOperator = Utils.GetFormValue(ScenicContact.UniqueID);
            modelScenicArea.ClickNum = 0;
            modelScenicArea.CnAddress = Utils.GetFormValue(txt_CnAddress.UniqueID);
            modelScenicArea.Description = Utils.EditInputText(this.txt_Description.Value);
            modelScenicArea.EnAddress = Utils.GetFormValue(txt_EnAddress.UniqueID);
            modelScenicArea.EnName = Utils.GetFormValue(txt_EnName.UniqueID);
            modelScenicArea.ExamineOperator = MasterUserInfo.ID;
            modelScenicArea.Facilities = Utils.GetFormValue(txt_Facilities.UniqueID);
            modelScenicArea.Img = new List<MScenicImg>();
            #region 获取地标
            IList<MScenicRelationLandMark> listlank = new List<MScenicRelationLandMark>();

            foreach (string item in Utils.GetFormValues("chkboxLankid"))
            {
                MScenicRelationLandMark modelMScenicRelationLandMark = new MScenicRelationLandMark();
                modelMScenicRelationLandMark.ScenicId = modelScenicArea.ScenicId;
                modelMScenicRelationLandMark.LandMarkId = Utils.GetInt(item);
                listlank.Add(modelMScenicRelationLandMark);
            }
            modelScenicArea.LankId = listlank;
            #endregion
            modelScenicArea.Notes = "";
            modelScenicArea.OpenTime = Utils.GetFormValue(txt_OpenTime.UniqueID);
            //发布用户
            modelScenicArea.Operator = this.hid_Operator.Value;
            modelScenicArea.ScenicName = Utils.GetFormValue(txt_ScenicName.UniqueID);
            modelScenicArea.SetYear = Utils.GetInt(Utils.GetFormValue(txt_SetYear.UniqueID));
            modelScenicArea.Telephone = Utils.GetFormValue(txt_Telephone.UniqueID);
            modelScenicArea.ScenicLevel = (ScenicLevel)Utils.GetInt(Utils.GetFormValue(DdlHotelStar.UniqueID));
            #region 获取主题
            IList<MScenicTheme> listi = new List<MScenicTheme>();

            foreach (string item in Utils.GetFormValues("chbTheme"))
            {
                MScenicTheme modelTheme = new MScenicTheme();
                modelTheme.ThemeId = Utils.GetInt(item);
                modelTheme.ThemeName = "";
                listi.Add(modelTheme);
            }
            modelScenicArea.ThemeId = listi;
            #endregion
            modelScenicArea.Traffic = Utils.GetFormValue(txt_Traffic.UniqueID);
            modelScenicArea.Status = (ExamineStatus)Utils.GetInt(Utils.GetFormValue(DdlStatus.UniqueID));
            modelScenicArea.X = Utils.InputText(this.jingdu.Value);
            modelScenicArea.Y = Utils.InputText(this.weidu.Value);
            //新增景区
            if (string.IsNullOrEmpty(ScenicIdAndComId[0]))
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().Add(modelScenicArea))
                    MessageBox.ShowAndRedirect(Page, "添加成功", "ScenicList.aspx");
                else
                    MessageBox.Show(Page, "添加失败");
            }
            else
            {
                if (EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().Update(modelScenicArea))
                    MessageBox.ShowAndRedirect(Page, "修改成功", "ScenicList.aspx");
                else
                    MessageBox.Show(Page, "修改失败");
            }

        }
        #endregion

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "','" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value)"));
            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", "0"));
            //this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "',this.options[this.selectedIndex].value，'" + this.ddl_ProvinceList.ClientID + "')"));
            this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value,'" + this.ddl_ProvinceList.ClientID + "');BinLankId()"));
            this.ddl_CountyList.Items.Insert(0, new ListItem("请选择", "0"));
        }

        #endregion
    }
}
