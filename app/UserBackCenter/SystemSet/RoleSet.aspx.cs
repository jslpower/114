using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Control;
using EyouSoft.Common.Function;
using EyouSoft.Common;
using ControlLibrary;
namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 页面功能：角色修改
    /// 开发人：xuty 开发时间：2010-06-29
    /// </summary>
    public partial class RoleSet : BasePage
    {
        protected int pageSize = 1000;
        protected int pageIndex = 1;
        protected int recordCount;
        protected int itemIndex = 1;
        protected int rowIndex = 1;
        protected bool isFirst = true;
        List<string> perList;
        EyouSoft.Model.CompanyStructure.CompanyUserRoles roleModle;
        protected string ImageServerUrl = Domain.ServerComponents;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyUserRoles roleBll;

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }
            if (!CheckGrant(TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            string roleid = Utils.GetQueryStringValue("roleid");
            roleBll = EyouSoft.BLL.CompanyStructure.CompanyUserRoles.CreateInstance();
            if (!Page.IsPostBack)
            {
                if (roleid != "")
                {
                    roleModle = roleBll.GetModel(roleid);
                    rs_txtRoleName.Value = roleModle.RoleName;
                    perList = roleModle.PermissionList.Split(',').ToList<string>();
                }
                BindPermit();
            }
            else
            {
                bool isSuccess = true;
                if (!IsCompanyCheck)
                {
                    MessageBox.Show(this, "对不起,你尚未审核通过!");
                    return;
                }
                if (roleid == "")//添加角色
                {
                    if (!AddPermit())
                    {
                        isSuccess = false;
                    }
                }
                else//修改角色
                {
                    if (!UpdatePermit(roleid))
                    {
                        isSuccess = false;
                    }
                }
                if (isSuccess)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('操作完成!');window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();window.parent.PermitManage.refresh();</script>");
                }
                else
                {
                    MessageBox.Show(this, "操作失败!");
                }
            }
        }

        #region 绑定具体权限
        protected string GetCateSonItem(object list)
        {
            if (list.Equals(null))
                return string.Empty;

            var permitList = (IList<EyouSoft.Model.SystemStructure.SysPermission>)list;
            var builder = new StringBuilder();
            foreach (EyouSoft.Model.SystemStructure.SysPermission permitModel in permitList)
            {
                builder.Append(string.Format("<tr><td><input name='checkper'id='checkper_{0}'  type='checkbox' {2} value='{0}' align='left' width='10%'></td><td align='left'><label for='checkper_{0}'>{1}</label></td></tr>", permitModel.Id, permitModel.PermissionName, GetChecked(permitModel.Id)));
            }
            return builder.ToString();
        }
        #endregion

        #region 判断是否换行
        protected string GetItem()
        {
            StringBuilder builder = new StringBuilder();
            if (itemIndex % 4 == 0)
            {
                builder.Append("<tr/><tr>");
            }
            itemIndex++;
            return builder.ToString();
        }
        #endregion

        #region 添加角色权限
        protected bool AddPermit()
        {
            roleModle = new EyouSoft.Model.CompanyStructure.CompanyUserRoles();
            roleModle.CompanyID = SiteUserInfo.CompanyID;
            roleModle.RoleName = Utils.InputText(Utils.GetFormValue(rs_txtRoleName.UniqueID), 20);

            roleModle.OperatorID = SiteUserInfo.ID;
            roleModle.PermissionList = Utils.GetFormValue("checkper");
            return roleBll.Add(roleModle);

        }
        #endregion

        #region 修改角色权限
        protected bool UpdatePermit(string roleid)
        {
            roleModle = roleBll.GetModel(roleid);
            roleModle.RoleName = Utils.InputText(Utils.GetFormValue(rs_txtRoleName.UniqueID), 20);



            roleModle.OperatorID = SiteUserInfo.ID;
            roleModle.PermissionList = Utils.GetFormValue("checkper");
            return roleBll.Update(roleModle);

        }
        #endregion

        #region 绑定权限列表
        protected void BindPermit()
        {
            EyouSoft.Model.SystemStructure.SysPermissionCategory permissionCategory =
                EyouSoft.BLL.SystemStructure.SysPermission.CreateInstance().GetAllPermissionByUser(
                    SiteUserInfo.CompanyRole.RoleItems);

            if (permissionCategory == null)
                return;

            ltrCategoryName.Text = permissionCategory.CategoryName;
            rs_PermitList.DataSource = permissionCategory.SysPermissionClass;
            rs_PermitList.DataBind();
        }
        #endregion

        #region 判断权限是否选中
        protected string GetChecked(int Id)
        {
            string ischecked = "";
            if (perList != null)
            {
                if (perList.Contains(Id.ToString()))
                    ischecked = "checked='checked'";
            }
            return ischecked;
        }
        #endregion
    }
}
