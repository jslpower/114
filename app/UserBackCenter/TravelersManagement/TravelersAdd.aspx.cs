using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Function;
using EyouSoft.Model.TicketStructure;

namespace UserBackCenter.TravelersManagement
{
    /// <summary>
    /// 说明：用户后台—营销工具—常旅客管理（添加）
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class TravelersAdd : BackPage
    {
        protected string tblID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.营销工具_常旅客管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            string id = Utils.GetQueryStringValue("id");
            if (Utils.GetQueryStringValue("method") == "ajax")
            {
                this.AjaxMethod(Utils.GetQueryStringValue("type"),id);
                return;
            }
            if (!IsPostBack)
            {
                //使用GUID作为页面Table容器的id.
                tblID = Guid.NewGuid().ToString();
                this.initData(id);
            }
        }
        protected void initData(string id)
        {
            this.BindVisitorType();
            this.BindCardType();
            this.InitCountryList();
            this.InitProvince();
            //编辑时初始化
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.TicketStructure.TicketVisitor BLL = new EyouSoft.BLL.TicketStructure.TicketVisitor();
                TicketVistorInfo Model = BLL.GetTicketVisitorInfo(id);
                if (null != Model)
                {
                    this.txtNameCn.Value = Model.ChinaName;//中文名
                    this.txtNameEn.Value = Model.EnglishName;//英文名
                    this.ddlType.SelectedValue =((int)Model.VistorType).ToString();//旅客类型
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("TravelersAdd.setSex('{0}');", (int)Model.ContactSex), true);//性别
                    this.txtMobile.Value = Model.Mobile;//手机号码
                    this.txtTel.Value = Model.ContactTel;//电话号码
                    this.txtAddress.Value = Model.Address;//邮寄地址
                    this.txtPostCode.Value = Model.ZipCode;//邮编
                    this.txtBirth.Value = string.Format("{0:yyyy-MM-dd}",Model.BirthDay);//生日
                    this.txtCardId.Value = Model.IdCardCode;//身份证
                    this.txtPassport.Value = Model.PassportCode;//护照
                    this.ddlOtherCard.SelectedValue = ((int)Model.CardType).ToString();//其它证件
                    this.txtCardNum.Value = Model.CardNo;//证件号码
                    this.ddlCountry.SelectedValue = Model.CountryId.ToString();//所在国家
                    //地区，省、市、区
                    this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("TravelersAdd.InitPlace('{0}','{1}','{2}');", Model.ProvinceId, Model.CityId, Model.DistrictId), true);
                    this.txtRemark.Value = Model.Remark;//备注
                }
            }
        }
        /// <summary>
        /// 绑定旅客类型
        /// </summary>
        private void BindVisitorType()
        {
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.TicketVistorType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.ddlType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), str)).ToString()));
                }
            }
            //释放资源
            typeList = null;
        }
        /// <summary>
        /// 绑定证件类型
        /// </summary>
        private void BindCardType()
        {
            this.ddlOtherCard.Items.Clear();
            List<EnumObj> typeList = EnumObj.GetList(typeof(EyouSoft.Model.TicketStructure.TicketCardType));
            if (typeList != null && typeList.Count > 0)
            {
                this.ddlOtherCard.DataSource = typeList;
                this.ddlOtherCard.DataTextField = "text";
                this.ddlOtherCard.DataValueField = "value";
                this.ddlOtherCard.DataBind();
                if (this.ddlOtherCard.Items[0].Value == "-1")
                {
                    this.ddlOtherCard.Items[0].Text = "--请选择--";
                }
            }
        }
        /// <summary>
        /// 绑定省份
        /// </summary>
        protected void InitProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.ProvinceList.DataSource = ProvinceList;
                this.ProvinceList.DataTextField = "ProvinceName";
                this.ProvinceList.DataValueField = "ProvinceId";
                this.ProvinceList.DataBind();
            }
            this.ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CityList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CountyList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ProvinceList.Attributes.Add("onchange", string.Format("SetList('{0}',$(this).val(),'0');", this.CityList.ClientID) + string.Format("SetList('{0}',1,$('#{1}').val(),'0');", this.CountyList.ClientID,this.CityList.ClientID));
            this.CityList.Attributes.Add("onchange", string.Format("SetList('{0}',1,$(this).val(),'0');", this.CountyList.ClientID));
        }
        /// <summary>
        /// 初始化国家
        /// </summary>
        private void InitCountryList()
        {
            //this.ddlCountry.Items.Add(new ListItem("-请选择-", "-1"));
            ////string countryInfo = this.ddlCountry.SelectedItem.Value;
            //IList<EyouSoft.Model.TicketStructure.TicketNationInfo> list = EyouSoft.BLL.TicketStructure.TicketNationInfo.CreateInstance().GetList("");
            //if (null != list && list.Count > 0)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        ListItem liCountry = new ListItem();
            //        liCountry.Text = list[i].CountryCode + "--" + list[i].CountryName;
            //        liCountry.Value = list[i].NationId.ToString();
            //        this.ddlCountry.Items.Add(liCountry);
            //    }
            //}
            //this.ddlCountry.DataBind();
            this.ddlCountry.Items.Add(new ListItem("-请选择-", "-1"));
            EyouSoft.BLL.SystemStructure.BSysCountry nationBLL = new EyouSoft.BLL.SystemStructure.BSysCountry();
            IList<EyouSoft.Model.SystemStructure.MSysCountry> lst= nationBLL.GetCountryList();
            if (null != lst && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    ListItem liCountry = new ListItem();
                    liCountry.Text = lst[i].CName + "--" + lst[i].EnName;
                    liCountry.Value = lst[i].CountryId.ToString();
                    this.ddlCountry.Items.Add(liCountry);
                }
                this.ddlCountry.DataBind();
            }
        }
        /// <summary>
        /// ajax方法
        /// </summary>
        /// <param name="type"></param>
        protected void AjaxMethod(string type,string id)
        {
            if (string.IsNullOrEmpty(type))
            {
                return;
            }
            switch (type)
            {
                //保存数据
                case "save":
                    this.pageSave(id);
                    break;
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        protected void pageSave(string id)
        {
            StringBuilder strMsg = new StringBuilder();
            EyouSoft.BLL.TicketStructure.TicketVisitor BLL = new EyouSoft.BLL.TicketStructure.TicketVisitor();
            TicketVistorInfo Model;
            if (string.IsNullOrEmpty(id))
            {//新增
                Model = new TicketVistorInfo();
                Model.IssueTime = DateTime.Now;
                strMsg.Append(this.commonModel(Model));
                if (strMsg.Length == 0 && BLL.AddTicketVisitorInfo(Model))
                {
                    strMsg.Append("添加成功！");
                }
                else
                {
                    strMsg.Remove(0, strMsg.Length);
                    strMsg.Append("添加失败！");
                }
            }
            else
            { //编辑
                Model = BLL.GetTicketVisitorInfo(id);
                if (null != Model)
                {
                    strMsg.Append(this.commonModel(Model));
                    if (strMsg.Length == 0 && BLL.UpdateTicketVisitorInfo(Model))
                    {
                        strMsg.Append("更新成功！");
                    }
                    else
                    {
                        strMsg.Remove(0, strMsg.Length);
                        strMsg.Append("更新失败！");
                    }
                }
            }
            Response.Clear();
            Response.Write(strMsg);
            Response.End();
        }
                /// <summary>
        /// 编辑与添加的公共model部分
        /// </summary>
        protected string commonModel(TicketVistorInfo Model)
        {
            StringBuilder str = new StringBuilder();
            string txtNameCn = Utils.GetFormValue(this.txtNameCn.UniqueID).Trim();//中文名
            string txtNameEn = Utils.GetFormValue(this.txtNameEn.UniqueID).Trim();//英文名
            int ddlType =Utils.GetInt(Utils.GetFormValue(this.ddlType.UniqueID),-1);//旅客类型
            int rdSex =Utils.GetInt(Utils.GetFormValue("rdSex"),0);//性别
            string txtMobile = Utils.GetFormValue(this.txtMobile.UniqueID).Trim();//手机号码
            string txtTel = Utils.GetFormValue(this.txtTel.UniqueID).Trim();//电话号码
            string txtAddress = Utils.GetFormValue(this.txtAddress.UniqueID).Trim();//邮寄地址
            string txtPostCode = Utils.GetFormValue(this.txtPostCode.UniqueID).Trim();//邮编
            DateTime? txtBirth =Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtBirth.UniqueID));//生日
            string txtCardId = Utils.GetFormValue(this.txtCardId.UniqueID).Trim();//身份证
            string txtPassport = Utils.GetFormValue(this.txtPassport.UniqueID).Trim();//护照
            int ddlOtherCard =Utils.GetInt(Utils.GetFormValue(this.ddlOtherCard.UniqueID),-1);//其它证件
            string txtCardNum = Utils.GetFormValue(this.txtCardNum.UniqueID).Trim();//证件号码
            int ddlCountry =Utils.GetInt(Utils.GetFormValue(this.ddlCountry.UniqueID),-1);//所在国家
            int ddlProvince =Utils.GetInt(Utils.GetFormValue(this.ProvinceList.UniqueID),-1);//省
            int ddlCity =Utils.GetInt(Utils.GetFormValue(this.CityList.UniqueID),-1);//市
            int ddlDistrict=Utils.GetInt(Utils.GetFormValue(this.CountyList.UniqueID),-1);//区
            string txtRemark = Utils.GetFormValue(this.txtRemark.UniqueID).Trim();//备注
            /*数据验证开始*/
            if (string.IsNullOrEmpty(txtNameCn)&&string.IsNullOrEmpty(txtNameEn))
            {
                str.Append("姓名不能为空！\\n");
            }
            if (string.IsNullOrEmpty(txtMobile))
            {
                str.Append("手机号不能为空！\\n");
            }
            if (str.Length > 0)
            {
                return str.ToString();
            }
            /*数据验证结束*/
            Model.Address = txtAddress;
            Model.BirthDay = txtBirth;
            Model.CardNo = txtCardNum;
            if (ddlOtherCard != -1)
            {
                Model.CardType = (TicketCardType)ddlOtherCard;
            }
            Model.ChinaName = txtNameCn;
            Model.CityId = ddlCity;
            Model.CompanyId = this.SiteUserInfo.CompanyID;
            Model.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)rdSex;
            Model.ContactTel = txtTel;
            Model.CountryId = ddlCountry;
            //Model.DataType = TicketDataType.None;
            Model.DistrictId = ddlDistrict;
            Model.EnglishName = txtNameEn;
            Model.IdCardCode = txtCardId;
            Model.Mobile = txtMobile;
            EyouSoft.BLL.TicketStructure.TicketNationInfo nationBLL=new EyouSoft.BLL.TicketStructure.TicketNationInfo();
            IEnumerable<EyouSoft.Model.TicketStructure.TicketNationInfo> lstCountry =nationBLL.GetList("").Where(m => m.NationId == ddlCountry);
            if (null != lstCountry && lstCountry.Count() > 0)
            {
                EyouSoft.Model.TicketStructure.TicketNationInfo countryModel = lstCountry.First();
                Model.NationInfo = countryModel;
            }
            Model.PassportCode = txtPassport;
            Model.ProvinceId = ddlProvince;
            Model.Remark = txtRemark;
            if(ddlType!=-1)
            {
                Model.VistorType = (TicketVistorType)ddlType;
            }
            Model.ZipCode = txtPostCode;
            return str.ToString();
        }
    }
}
