using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
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
using ControlLibrary;
namespace SiteOperationsCenter.UserManage
{
    /// <summary>
    /// 页面功能：运营后台账户设置
    /// 开发时间：2010-07-23 开发人：xuty
    /// </summary>
    public partial class UserSet : EyouSoft.Common.Control.YunYingPage
    {
        protected string areaHTML;//账户负责区域
        EyouSoft.IBLL.SystemStructure.ISystemUser userBll;
        string userPermitList;//账户权限列表
        protected bool isAdd;
        protected string userTypeHtml;//用户类别
        IList<int> userTypes;//用户类别
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_子帐号管理))
            {
                Utils.ResponseNoPermit(YuYingPermission.帐户管理_子帐号管理, true);
                return;
            }
            int userId = Utils.GetInt(Request.QueryString["userid"]);//获取账户编号
            userBll=EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            if (Page.IsPostBack)
            {
                if (userId==0)//添加账户
                {
                    AddUser();
                }
                else//修改账户
                {
                    UpdateUser(userId);
                }
            }
            else
            {
               
                us_txtPass1.Attributes.Add("custom", "UserSet.checkPass");
                us_txtPass1.Attributes.Add("min", "6");
                us_txtPass1.Attributes.Add("max", "16");
                if (userId != 0)//初始化账户信息
                {
                    LoadUserInfo(userId);
                    us_txtPass1.Attributes.Add("valid", "limit|custom");
                    us_txtPass1.Attributes.Add("errmsg", "密码长度为6-16个字符|密码不能单独使用数字，字母或特殊字符");
                }
                else
                {
                    isAdd = true;
                    us_txtPass1.Attributes.Add("valid", "required|limit|custom");
                    us_txtPass1.Attributes.Add("errmsg", "请填写密码|密码长度为6-16个字符|密码不能单独使用数字，字母或特殊字符");
                }
                BindPermit();//绑定权限列表
                BindCustomerType();//绑定类型
            }
        }

        #region 绑定所有权限
        protected void BindPermit()
        {
            EyouSoft.IBLL.SystemStructure.ISysPermissionCategory permissionCategoryBll = EyouSoft.BLL.SystemStructure.SysPermissionCategory.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysPermissionCategory> permissionCategoryList = permissionCategoryBll.GetList(EyouSoft.Model.SystemStructure.PermissionType.运营后台);
            us_rpt_PCateList.DataSource = permissionCategoryList;
            us_rpt_PCateList.DataBind();
        }
        #endregion

        #region 初始化账户信息
        protected void LoadUserInfo(int userId)
        {
            EyouSoft.Model.SystemStructure.SystemUser userModel = userBll.GetSystemUserModel(userId);
            us_txtFax.Value = userModel.ContactFax;
            us_txtMoible.Value = userModel.ContactMobile;
            us_txtRealName.Value = userModel.ContactName;
            us_txtTel.Value = userModel.ContactTel;
            us_txtUserName1.Value = userModel.UserName;
            us_txtUserName1.Attributes.Add("readonly", "readonly");
            userPermitList = userModel.PermissionList;//账户权限
            userTypes = userModel.CustomerTypeIds;
            //获取账户负责区域列表
            EyouSoft.IBLL.SystemStructure.ISysCity cityBll=EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
           
            StringBuilder areaBuilder = new StringBuilder();
            if (userModel.AreaId != null)
            {
                foreach (int areaId in userModel.AreaId)
                {
                    EyouSoft.Model.SystemStructure.SysCity cityModel = cityBll.GetSysCityModel(areaId);
                    areaBuilder.Append(string.Format("<input type='checkbox' checked='checked' id='ckSale_{0}' name='ckSellCity' value='{0}' /><label for='ckSale_{0}'>{1}</label>", areaId.ToString(), cityModel != null ? cityModel.CityName : "暂无"));
                }
            }
            areaHTML = areaBuilder.ToString();
            //绑定类别
         
           
         }
        #endregion

        #region 添加账户
        protected void AddUser()
        {
            EyouSoft.Model.SystemStructure.SystemUser userModel = new EyouSoft.Model.SystemStructure.SystemUser();
            userModel.UserName = Utils.GetFormValue(us_txtUserName1.UniqueID,16);
            string pass1 = Utils.GetFormValue(us_txtPass1.UniqueID);
            string pass2 = Utils.GetFormValue(us_txtPass2.UniqueID);
            userModel.PassWordInfo.NoEncryptPassword = pass1;
            userModel.ContactName = Utils.GetFormValue(us_txtRealName.UniqueID,16);
            userModel.ContactTel = Utils.GetFormValue(us_txtTel.UniqueID);
            if (userModel.UserName == "" || pass1 == "" || pass2 == "" || userModel.ContactName == "" || userModel.ContactTel == "")
            {
                MessageBox.Show(this,"数据请填写完整!");
                return;
            }


            if (pass1 != pass2)
            {
                MessageBox.Show(this,"两次密码不一致!");
                return;
            }
            EyouSoft.IBLL.SystemStructure.ISystemUser sysUserBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            if (sysUserBll.ExistsByUserName(userModel.UserName))
            {
                MessageBox.Show(this, "用户名已存在!");
                return;
            }
            string areaIds = Utils.GetFormValue("ckSellCity");
            userModel.ContactMobile = Utils.GetFormValue(us_txtMoible.UniqueID);
            userModel.ContactFax = Utils.GetFormValue(us_txtFax.UniqueID,30);
            userModel.PermissionList = Utils.GetFormValue("chk_Permit");
            if (areaIds != "")
            {
                userModel.AreaId = areaIds.Split(',').Select(i => Utils.GetInt(i)).ToList<int>();
            }
            //获取绑定的用户类型
            IList<int> custTypeList = Utils.GetFormValues("chkCustType").Select(i => int.Parse(i)).ToList();
            userModel.CustomerTypeIds = custTypeList;

            if (EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance().AddSystemUser(userModel) > 0)
            {
                MessageBox.ShowAndRedirect(this, "保存成功!", "UserListManage.aspx");
            }
            else
            {
                MessageBox.Show(this, "保存失败!");
            }
          
        }
        #endregion

        #region 修改账户
        protected void UpdateUser(int userId)
        {
            string userName = Utils.GetFormValue(us_txtUserName1.UniqueID,16);
            string pass1 = Utils.GetFormValue(us_txtPass1.UniqueID);
            string pass2 = Utils.GetFormValue(us_txtPass2.UniqueID);
            string realName = Utils.GetFormValue(us_txtRealName.UniqueID,16);
            string userTel = Utils.GetFormValue(us_txtTel.UniqueID);
            if (userName == ""|| realName == "" || userTel == "")
            {
                MessageBox.Show(this,"数据请填写完整!");
                return;
            }
            if (pass1!= pass2)
            {
                MessageBox.Show(this,"两次密码不一致!");
                return;
            }
            EyouSoft.Model.SystemStructure.SystemUser userModel = userBll.GetSystemUserModel(userId);
            if (userModel != null)
            {
                string areaIds = Utils.GetFormValue("ckSellCity");
                if (areaIds != "")
                {
                    userModel.AreaId = areaIds.Split(',').Select(i => Utils.GetInt(i)).ToList<int>();
                }
                userModel.UserName = userName;
                userModel.ContactFax = Utils.GetFormValue(us_txtFax.UniqueID,30);
                userModel.ContactMobile = Utils.GetFormValue(us_txtMoible.UniqueID);
                userModel.ContactTel = userTel;
                userModel.ContactName = realName;
                userModel.PermissionList = Utils.GetFormValue("chk_Permit");
                //获取绑定的用户类型
                IList<int> custTypeList = Utils.GetFormValues("chkCustType").Select(i => int.Parse(i)).ToList();
                userModel.CustomerTypeIds = custTypeList;

                if (pass1!= "")
                {
                    userBll.UpdateUserPassWord(userModel.ID, pass1);
                }
            }
            if (userBll.UpdateSystemUser(userModel) > 0)
            {
                MessageBox.ShowAndRedirect(this, "保存成功!", "UserListManage.aspx");
             }
            else
            {
                MessageBox.Show(this, "保存失败!");
            }
        }
        #endregion

        #region 绑定大类项
        protected void us_rpt_PCateList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                CustomRepeater rpt = e.Item.FindControl("us_rpt_PClassList") as CustomRepeater;
                if (rpt != null)
                {
                    EyouSoft.Model.SystemStructure.SysPermissionCategory categoryModel = e.Item.DataItem as EyouSoft.Model.SystemStructure.SysPermissionCategory;
                    rpt.DataSource = categoryModel.SysPermissionClass;
                    rpt.DataBind();
                }
            }

        }
        #endregion

        #region 返回具体权限列表
        protected string GetPermitList(IList<EyouSoft.Model.SystemStructure.SysPermission> permitList)
        {   
            
            int count = 0;
            if (permitList != null && permitList.Count > 0)
            {
                count = permitList.Count;
            }
            StringBuilder permitBuilder = new StringBuilder();
            int itemIndex = 1;
            permitBuilder.Append("<tr>");
            foreach (EyouSoft.Model.SystemStructure.SysPermission permit in permitList)
            {
                if (itemIndex % 5 == 1)
                {
                    permitBuilder.Append("</tr><tr>");
                }

                permitBuilder.Append(string.Format("<td width='20%'  align='left'  ><input type='checkbox' {2} name='chk_Permit' id='chk_Permit{0}' value='{0}' {3} /><label for='chk_Permit{0}'>{1}</label></td>", permit.Id, permit.PermissionName, GetPermitCheck(permit.Id), MasterUserInfo.PermissionList.Contains(permit.Id) ? "" : "disabled='disabled'"));
                if (itemIndex == count)
                {
                    if (count % 5 != 0)
                    {
                        int remain = 5 - count % 5;
                        for (int i = 1; i <= remain; i++)
                        {
                            permitBuilder.Append("<td width='20%' align='left'  ></td>");
                        }
                    }
                }
                itemIndex++;
            }
            permitBuilder.Append("</tr>").Remove(0, 9);
            return permitBuilder.ToString();

            //StringBuilder permitBuilder = new StringBuilder();
            //if (permitList != null)
            //{
            //    foreach (EyouSoft.Model.SystemStructure.SysPermission permit in permitList)
            //    {
            //        permitBuilder.Append(string.Format(" <input type=\"checkbox\" {2} name=\"chk_Permit\" value=\"checkbox\" value='{0}' />{1}",permit.Id, permit.PermissionName,GetPermitCheck(permit.Id)));

            //    }
            //}
            //return permitBuilder.ToString();
           
        }
        #endregion

        #region 判断账户是否具有该权限
        protected string GetPermitCheck(int permitId)
        {   
            string isCheck="";
            if (!string.IsNullOrEmpty(userPermitList))
            {
                if(userPermitList.Contains(permitId.ToString()))
                {
                    isCheck="checked='checked'";
                }
            }
            return isCheck;
        }
        #endregion

        /// <summary>
        /// 绑定用户类型列表
        /// </summary>
        protected void BindCustomerType()
        {
            //用户类型BLL
            EyouSoft.IBLL.PoolStructure.ICustomerType typeBll = EyouSoft.BLL.PoolStructure.CustomerType.CreateInstance();
            StringBuilder typeBuilder = new StringBuilder();
            IList<EyouSoft.Model.PoolStructure.CustomerTypeInfo> CustomerTypeList = typeBll.GetCustomerTypeList();
            if (CustomerTypeList != null && CustomerTypeList.Count > 0)
            {
                if (userTypes == null)
                {
                    foreach (EyouSoft.Model.PoolStructure.CustomerTypeInfo typeInfo in CustomerTypeList)
                    {
                        typeBuilder.AppendFormat("<input type='checkbox' name='chkCustType' value='{0}' id='chkCustType{0}'/><label for='chkCustType{0}'>{1}</label>", typeInfo.TypeId, typeInfo.TypeName);
                    }
                }
                else
                {
                    foreach (EyouSoft.Model.PoolStructure.CustomerTypeInfo typeInfo in CustomerTypeList)
                    {
                        typeBuilder.AppendFormat("<input type='checkbox' name='chkCustType' value='{0}' id='chkCustType{0}' {2}/><label for='chkCustType{0}'>{1}</label>", typeInfo.TypeId, typeInfo.TypeName, userTypes.Where(id => id == typeInfo.TypeId).Count() > 0 ? "checked='checked'" : "");
                    }
                }
               
            }
            userTypeHtml = typeBuilder.ToString();
        }

    }
}
