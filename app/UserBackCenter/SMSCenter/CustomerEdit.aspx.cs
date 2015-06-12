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
using EyouSoft.Common.Control;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：SMS客户编辑页
    /// 开发人：xuty 开发时间：2010-08-04
    /// </summary>
    public partial class CustomerEdit : BackPage
    {
        EyouSoft.IBLL.SMSStructure.ICustomer customerBll;
        protected string isAdd = "add";
        protected string customerId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            customerBll = EyouSoft.BLL.SMSStructure.Customer.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            if (method == "addClass")
            {
                AddClass();//添加类
                return;
            }
            if (method == "update" || method == "add")
            {
                AddOrUpdateCustomer(method);//添加或修改客户
                return;
            }
            if (method == "isExist")
            {
                IsExist();//判断号码是否已经存在
                return;
            }
            if (method == "delClass")
            {
                DeleteClass();//删除客户类别
                return;
            }
            LoadData();//初始化数据
        }

        #region 添加类别
        protected void AddClass()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起，你尚未审核通过!");
                return;
            }
            string className = Utils.InputText(Server.UrlDecode(Request.QueryString["classname"] ?? ""));
            EyouSoft.Model.SMSStructure.CustomerCategoryInfo cateModel = new EyouSoft.Model.SMSStructure.CustomerCategoryInfo();
            cateModel.CategoryName = className;//类别名称
            cateModel.CompanyId = SiteUserInfo.CompanyID;
            cateModel.UserId = SiteUserInfo.ID;
            int cateId = customerBll.InsertCategory(cateModel);
            if (cateId > 0)
            {
                Utils.ResponseMeg(true, cateId.ToString());
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion

        #region 删除客户类别
        protected void DeleteClass()
        {
            if (!IsCompanyCheck)//是否审核
            {
                Utils.ResponseMegError();
                return;
            }
            int classId = Utils.GetInt(Utils.GetQueryStringValue("classid"), -1);//获取要删除的客户类别
            if (customerBll.DeleteCategory(classId))
            {
                Utils.ResponseMegSuccess();
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion

        #region 添加或修改客户
        protected void AddOrUpdateCustomer(string method)
        {
            if (!IsCompanyCheck)//是否审核
            {
                Utils.ResponseMegError();
                return;
            }
            string moible = Utils.InputText(Server.UrlDecode(Request.QueryString["moible"] ?? ""));//手机号
            string companyName = Utils.InputText(Server.UrlDecode(Request.QueryString["companyname"] ?? ""));//单位名称
            string userName = Utils.InputText(Server.UrlDecode(Request.QueryString["username"] ?? ""));//姓名
            string remark = Utils.InputText(Server.UrlDecode(Request.QueryString["remark"] ?? ""));//备注
            string cateName = Utils.InputText(Server.UrlDecode(Request.QueryString["catename"] ?? ""));//类别名称
            int cateId = Utils.GetInt(Request.QueryString["cateid"]);//类别Id
            string custid = Utils.GetQueryStringValue("custid");
            //省份|城市
            int provinceId = Utils.GetInt(Request.QueryString["provinceId"]);
            int cityId = Utils.GetInt(Request.QueryString["cityid"]);
            EyouSoft.Model.SMSStructure.CustomerInfo customerModel = new EyouSoft.Model.SMSStructure.CustomerInfo();
            if (method == "update")
            {
                if (customerBll.IsExistsCustomerMobile(SiteUserInfo.CompanyID, moible, custid))//判断客户号码是否已经存在
                {
                    Utils.ResponseMeg(false, "该号码已经存在!");
                    return;
                }
                customerModel = customerBll.GetCustomerInfo(custid);
            }
            else if (method == "add")
            {
                if (customerBll.IsExistsCustomerMobile(SiteUserInfo.CompanyID, moible))//判断客户号码是否已经存在
                {
                    Utils.ResponseMeg(false, "该号码已经存在!");
                    return;
                }
            }
            //省份|城市
            customerModel.ProvinceId = provinceId;
            customerModel.CityId = cityId;
            customerModel.UserId = SiteUserInfo.ID;
            customerModel.Remark = remark;
            customerModel.Mobile = moible;
            customerModel.CategoryId = cateId;
            customerModel.CustomerContactName = userName;
            customerModel.CompanyId = SiteUserInfo.CompanyID;
            customerModel.CustomerCompanyName = companyName;
            customerModel.CategoryName = cateName;
            bool isSuccess = true;
            if (method == "update")
            {
                isSuccess = customerBll.UpdateCustomer(customerModel);//执行修改操作
                if (isSuccess) { Utils.ResponseMeg(true, "修改成功"); }
                else { Utils.ResponseMeg(true, "修改失败"); }
            }
            else if (method == "add")
            {
                isSuccess = customerBll.InsertCustomer(customerModel);//执行添加造作
                if (isSuccess) { Utils.ResponseMeg(true, "添加成功"); }
                else { Utils.ResponseMeg(true, "添加失败"); }
            }

        }
        #endregion

        #region 初始化信息
        protected void LoadData()
        {
           //2010-12-17 修改客户类型-lihc
            BindCustomerCate();


            string custId = Utils.GetQueryStringValue("custid");
            if (custId != "")
            {
                EyouSoft.Model.SMSStructure.CustomerInfo customerModel = customerBll.GetCustomerInfo(custId);
                if (customerModel != null)
                {
                    isAdd = "update";
                    customerId = custId;
                    ce_selUserClass.Value = customerModel.CategoryId.ToString();
                    ce_txtCompanyName.Value = customerModel.CustomerCompanyName;
                    ce_txtMoible.Value = customerModel.Mobile;
                    ce_txtUserName.Value = customerModel.CustomerContactName;
                    ce_txtRemark.Value = customerModel.Remark;
                    PAC_Select.SetProvinceId = customerModel.ProvinceId;
                    PAC_Select.SetCityId = customerModel.CityId;
                }
            }
        }
        #endregion
        #region 绑定客户类型
        protected void BindCustomerCate()
        {
            //2010-12-16修改
            //客户类型来源 枚举型
            Type CusType = typeof(EyouSoft.Model.CompanyStructure.CompanyType);
            foreach (string s in Enum.GetNames(CusType))
            {
                if (s != EyouSoft.Model.CompanyStructure.CompanyType.全部.ToString())
                {
                    ListItem item = new ListItem(s, Enum.Format(CusType, Enum.Parse(CusType, s), "d").Trim());
                    ce_selUserClass.Items.Add(item);
                }

            }
            //2010-12-16被替换部分   
            //cl_selUserClass.DataTextField = "CategoryName";
            //cl_selUserClass.DataValueField = "CategoryId";
            //cl_selUserClass.DataSource = EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetCategorys(SiteUserInfo.CompanyID);
            //cl_selUserClass.DataBind();
            ce_selUserClass.Items.Insert(0, new ListItem("选择类型", "-1"));
        }
        #endregion

        #region 判断号码是否已经存在
        protected void IsExist()
        {
            string moible = Utils.GetQueryStringValue("moible");
            if (customerBll.IsExistsCustomerMobile(SiteUserInfo.CompanyID, moible))
            {
                Utils.ResponseMegError();
            }
            else
            {
                Utils.ResponseMegError();
            }
        }
        #endregion


    }
}
