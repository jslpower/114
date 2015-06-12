using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Model.PoolStructure;

namespace SiteOperationsCenter.CustomerManage
{
    /// <summary>
    /// 客户资料——客户资料管理
    /// 开发人：孙川 2010-12-03
    /// </summary>
    public partial class EditCustomer : EyouSoft.Common.Control.YunYingPage
    {
        protected string EditId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetFormValue("method");
            if (method == "checkMoible")
            {
                Response.Clear();
                if (IsExistMobile(null) > 0)        //验证手机号码是否存在
                {
                    Response.Write("hasMoible");
                }
                else
                    Response.Write("");
                Response.End();
                return;
            }
            EditId = Context.Request.QueryString["EditId"];
            if (!IsPostBack)
            {
                BindProvince();
                BindCity();
                InitQueryDropDownList();
                if (!string.IsNullOrEmpty(EditId))
                {
                    this.Panel1.Visible = false;
                    GetCompanyInfo();
                }
                else
                {
                    this.Panel1.Visible = true;
                }
            }
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
        }

        //绑定城市
        private void BindCity()
        {
            //获取城市列表
            IList<EyouSoft.Model.SystemStructure.SysCity> cityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetCityList();
            this.dropCityId.Items.Clear();
            StringBuilder strCity = new StringBuilder();
            strCity.Append("\nvar CityCount;\n");
            strCity.Append("arrCity = new Array();\n");
            if (cityList != null && cityList.Count > 0)
            {
                int index = 0;
                foreach (EyouSoft.Model.SystemStructure.SysCity model in cityList)
                {
                    ListItem item = new ListItem(model.CityName, model.CityId.ToString());
                    //数组下标，城市名称，城市ID，该城市所属的省份ID
                    //每行new一个含3列的的数组
                    string str = string.Format("\narrCity[{0}]=new Array('{1}', {2}, {3});", index.ToString(), model.CityName, model.CityId.ToString(), model.ProvinceId.ToString());
                    strCity.Append(str);
                    index++;
                }
                strCity.Append("\nCityCount=" + index.ToString() + ";");
                strCity.Append("\nfunction ChangeList(CityTextId, ProvinceId)");
                strCity.Append("\n{var Obj = document.getElementById(CityTextId);"); //获得要被绑定小类的控件对象
                strCity.Append("\nObj.length = 0;");
                strCity.Append("\nObj.options[0] = new Option('请选择', 0);");
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
                MessageBox.ResponseScript(this.Page, strCity.ToString());
            }
            this.dropCityId.Items.Insert(0, new ListItem("请选择", "0"));


        }
        #endregion

        #region  客户类型,适用产品
        // 初始话 客户类型,适用产品
        private void InitQueryDropDownList()
        {
            chbCustomerType.Items.Clear();
            chbCustomerType.DataSource = EyouSoft.BLL.PoolStructure.CustomerType.CreateInstance().GetCustomerTypeList();
            chbCustomerType.DataTextField = "TypeName";
            chbCustomerType.DataValueField = "TypeId";
            chbCustomerType.DataBind();

            chbSuitProduct.Items.Clear();
            chbSuitProduct.DataSource = EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().GetSuitProductList();
            chbSuitProduct.DataTextField = "ProductName";
            chbSuitProduct.DataValueField = "ProuctId";
            chbSuitProduct.DataBind();
        }
        #endregion

        #region 初始化公司信息
        /// <summary>
        /// 初始化公司信息
        /// </summary>
        protected void GetCompanyInfo()
        {
            EyouSoft.Model.PoolStructure.CompanyInfo Model = EyouSoft.BLL.PoolStructure.Company.CreateInstance().GetCompanyInfo(EditId);
            if (Model != null)
            {
                BindProvince();
                BindCity();
                InitQueryDropDownList();

                //绑定省份 城市
                if (Model.ProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince('" + Model.ProvinceId + "');ChangeList('" + this.dropCityId.ClientID + "','" + Model.ProvinceId + "');", true);
                    if (Model.CityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity('" + Model.CityId + "');", true);
                    }
                }


                #region 绑定客户类型 适用产品
                //绑定客户类型 适用产品
                IList<CustomerTypeInfo> list = Model.CustomerTypeConfig;
                IList<SuitProductInfo> SuitList = Model.SuitProductConfig;

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = 0; j < chbCustomerType.Items.Count; j++)
                    {
                        if (list[i].TypeId == Utils.GetInt(chbCustomerType.Items[j].Value))
                        {
                            chbCustomerType.Items[j].Selected = true;
                        }
                    }
                }

                for (int i = 0; i < SuitList.Count; i++)
                {
                    for (int j = 0; j < chbSuitProduct.Items.Count; j++)
                    {
                        if (SuitList[i].ProuctId == Utils.GetInt(chbSuitProduct.Items[j].Value))
                        {
                            chbSuitProduct.Items[j].Selected = true;
                        }
                    }
                }
                #endregion

                this.txtCompanyName.Value = Model.CompanyName;                      //公司名称
                this.txtCompanyStar.Value = Model.Star;                             //企业星级
                this.rptContacters.DataSource = Model.Contacters;                   //公司联系人
                this.rptContacters.DataBind();
                this.txtAddress.Value = Model.Address;                              //公司地址
                this.txtCompanyRemark.Value = Model.Remark;                         //公司备注
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "未找到该客户信息!");
                return;
            }
            Model = null;
        }
        #endregion 

        #region 验证手机号码是否存在
        /// <summary>
        /// 手机号码是否存在
        /// </summary>
        private int IsExistMobile(string CompanyId)
        {
            int ResultCount = 0;
            string[] Mobile = Utils.GetFormValues("txt_Mobile");
            string[] ContactId = Utils.GetFormValues("txt_ContactId"); 
            IList<ContacterInfo> MobileList = new List<ContacterInfo>();
            ContacterInfo ModelContacter = null;
            if (Mobile != null && Mobile.Length > 0 && ContactId != null && ContactId.Length>0)
            {
                for (int index = 0; index < Mobile.Length; index++)
                {
                    ModelContacter = new ContacterInfo();
                    ModelContacter.ContacterId = Utils.GetInt( ContactId[index]);
                    ModelContacter.Mobile = Mobile[index];
                    MobileList.Add(ModelContacter);
                    ModelContacter = null;
                }
                ResultCount = EyouSoft.BLL.PoolStructure.Company.CreateInstance().ExistsMobiles(CompanyId, MobileList).Count;
            }
            return ResultCount;
        }
        #endregion 

        #region 修改 新增公司信息
        /// <summary>
        /// 修改公司信息
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            

            int ProvinceId = EyouSoft.Common.Utils.GetInt(Request.Form["dropProvinceId"]);   //省份编号
            int CityId = EyouSoft.Common.Utils.GetInt(Request.Form["dropCityId"]);           //城市编号

            #region 客户类型
            //客户类型
            List<CustomerTypeInfo> CustomerList = new List<CustomerTypeInfo>();
            CustomerTypeInfo CustomerModel = null;
            for (int i = 0; i < chbCustomerType.Items.Count; i++)
            {
                if (chbCustomerType.Items[i].Selected)
                {
                    CustomerModel = new CustomerTypeInfo();
                    CustomerModel = EyouSoft.BLL.PoolStructure.CustomerType.CreateInstance().GetCustomerType(Utils.GetInt(chbCustomerType.Items[i].Value));
                    CustomerList.Add(CustomerModel);
                }
            }
            #endregion

            string txtCompanyName = Utils.InputText(this.txtCompanyName.Value.Trim());       //公司名称
            string txtCompanyStar = Utils.InputText(this.txtCompanyStar.Value.Trim());      //企业星级

            #region 公司联系人
            //公司联系人
            string[] Name = Utils.GetFormValues("txt_Name");
            string[] Birthday = Utils.GetFormValues("txt_Birthday");
            string[] MemorialDay = Utils.GetFormValues("txt_MemorialDay");
            string[] Duties = Utils.GetFormValues("txt_Duties");
            string[] Mobile = Utils.GetFormValues("txt_Mobile");
            string[] Email = Utils.GetFormValues("txt_Email");
            string[] QQ = Utils.GetFormValues("txt_QQ");
            string[] Character = Utils.GetFormValues("txt_Character");
            string[] Hobby = Utils.GetFormValues("txt_Hobby");
            string[] Remarks = Utils.GetFormValues("txt_Remarks");

            List<ContacterInfo> ContacterList = new List<ContacterInfo>();
            if (Name != null)
            {
                EyouSoft.Model.PoolStructure.ContacterInfo ContacterModel = null;
                for (int index = 0; index < Name.Length; index++)
                {
                    if (!string.IsNullOrEmpty(Name[index].Trim()) || !string.IsNullOrEmpty(Birthday[index].Trim()) || !string.IsNullOrEmpty(MemorialDay[index].Trim()) || !string.IsNullOrEmpty(Duties[index].Trim()) || !string.IsNullOrEmpty(Mobile[index].Trim()) || !string.IsNullOrEmpty(Email[index].Trim()) || !string.IsNullOrEmpty(QQ[index].Trim()) || !string.IsNullOrEmpty(Character[index].Trim()) || !string.IsNullOrEmpty(Hobby[index].Trim()) || !string.IsNullOrEmpty(Remarks[index].Trim()))
                    {
                        ContacterModel = new EyouSoft.Model.PoolStructure.ContacterInfo();
                        ContacterModel.Fullname = Name[index];
                        ContacterModel.Birthday = Utils.GetDateTime((Birthday[index]), DateTime.Now);
                        ContacterModel.RememberDay = Utils.GetDateTime(MemorialDay[index], DateTime.Now);
                        ContacterModel.JobTitle = Duties[index];
                        ContacterModel.Mobile = Mobile[index];
                        ContacterModel.Email = Email[index];
                        ContacterModel.QQ = QQ[index];
                        ContacterModel.Character = Character[index];
                        ContacterModel.Interest = Hobby[index];
                        ContacterModel.Remark = Remarks[index];
                        ContacterList.Add(ContacterModel);
                        ContacterModel = null;
                    }
                }
            }
            #endregion

            string txtAddress = Utils.InputText(this.txtAddress.Value.Trim());               //企业地址
            string txtCompanyRemark = Utils.InputText(this.txtCompanyRemark.Value.Trim());   //备注

            #region 适用产品
            List<SuitProductInfo> SuitProductList = new List<SuitProductInfo>();
            SuitProductInfo SuitProductModel = null;
            for (int i = 0; i < chbSuitProduct.Items.Count; i++)
            {
                if (chbSuitProduct.Items[i].Selected)
                {
                    SuitProductModel = new SuitProductInfo();
                    SuitProductModel = EyouSoft.BLL.PoolStructure.SuitProduct.CreateInstance().GetSuitProduct(Utils.GetInt(chbSuitProduct.Items[i].Value));
                    SuitProductList.Add(SuitProductModel);
                }
            }
            #endregion

            #region 保存客户信息
                EyouSoft.Model.PoolStructure.CompanyInfo CompanyModel = new EyouSoft.Model.PoolStructure.CompanyInfo();
                CompanyModel.ProvinceId = ProvinceId;
                CompanyModel.CityId = CityId;
                CompanyModel.CustomerTypeConfig = CustomerList;
                CompanyModel.CompanyName = txtCompanyName;
                CompanyModel.Star = txtCompanyStar;
                CompanyModel.Contacters = ContacterList;
                CompanyModel.Address = txtAddress;
                CompanyModel.Remark = txtCompanyRemark;
                CompanyModel.SuitProductConfig = SuitProductList;

                if (!string.IsNullOrEmpty(EditId))
                {
                    CompanyModel.CompanyId = EditId;
                    bool result = EyouSoft.BLL.PoolStructure.Company.CreateInstance().Update(CompanyModel);
                    if (result)
                    {
                        MessageBox.ShowAndRedirect(this, "修改成功", "Default.aspx");
                    }
                }
                else
                {
                    bool result = EyouSoft.BLL.PoolStructure.Company.CreateInstance().Create(CompanyModel);
                    if (result)
                    {
                        MessageBox.ShowAndRedirect(this, "添加成功", "Default.aspx");
                    }
                }
                CompanyModel = null;
                CustomerList = null;
                ContacterList = null;
                SuitProductList = null;
                #endregion
        }
        #endregion 

    }
}
