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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 页面功能：修改子账户
    /// 开发人：xuty 开发时间：2010-07-06
    /// </summary>
    public partial class SonUserSet : BasePage
    {
        protected int itemIndex = 1;
        protected EyouSoft.IBLL.CompanyStructure.ICompanyUser sonUserBll;
        protected EyouSoft.Model.CompanyStructure.CompanyUser sonUserModel;
        protected string account;
        protected int recordCount;
        protected bool haveArea = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            //是否登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx", "对不起，你尚未登录！");
                return;
            }
            if (!CheckGrant(TravelPermission.系统设置_子账户管理, TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            sonUserBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            if (method == "checkAccount")
            {
                IsIn();
                return;
            }
            string id = Utils.GetQueryStringValue("sonuserid");
            if (id != "")
                sonUserModel = sonUserBll.GetModel(id);
            if (!Page.IsPostBack)
            {
                haveArea = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                if (id == "" || method == "copy")
                {
                    sus_txtNewPassword1.Attributes.Add("valid", "required|limit");
                    sus_txtNewPassword1.Attributes.Add("errmsg", "请填写密码|密码长度为6-16个字符");
                    sus_txtNewPassword1.Attributes.Add("custom", "SonUserSet.checkPass");
                    sus_txtNewPassword1.Attributes.Add("min", "6");
                    sus_txtNewPassword1.Attributes.Add("max", "16");
                }
                else
                {
                    sus_txtNewPassword1.Attributes.Add("valid", "limit");
                    sus_txtNewPassword1.Attributes.Add("errmsg", "密码长度为6-16个字符");
                    sus_txtNewPassword1.Attributes.Add("custom", "SonUserSet.checkPass");
                    sus_txtNewPassword1.Attributes.Add("min", "6");
                    sus_txtNewPassword1.Attributes.Add("max", "16");
                }
                BindDepartAndRole();//绑定部门和角色
                BindManageArea();//绑定经营区域
                if (method == "update" || method == "copy")//如果是修改或复制则加载初始数据
                {
                    LoadSonUser(method);
                }
            }
            if (Page.IsPostBack)
            {
                //获取子账户经营区域
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = new List<EyouSoft.Model.SystemStructure.AreaBase>();
                var keyList = Request.Form.AllKeys.Where(key => key.Contains("checkbox_Area_"));
                foreach (string k in keyList)
                {
                    EyouSoft.Model.SystemStructure.AreaBase area = new EyouSoft.Model.SystemStructure.AreaBase();
                    area.AreaId = int.Parse(Utils.GetFormValue(k));
                    areaList.Add(area);
                }
                if (method == "update")//修改子账户
                {
                    UpdateSonUser(areaList);
                }
                if (method == "add" || method == "copy")//添加子账户
                {
                    int SonUserNumLimit = 0;
                    IList<EyouSoft.Model.CompanyStructure.CompanyUser> sonUserList = sonUserBll.GetList(SiteUserInfo.CompanyID, 1, 1, ref recordCount);
                    EyouSoft.Model.CompanyStructure.CompanySetting comSeting = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                    if (comSeting != null)
                    {
                        SonUserNumLimit = comSeting.OperatorLimit;
                    }
                    //if (recordCount == SonUserNumLimit)
                    //{
                    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('对不起,你只能建"+SonUserNumLimit+"个账户!');if(window.parent.Boxy){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();}else{window.close();}</script>");
                    //    return;
                    //}
                    AddSonUser(areaList);
                }
            }
        }

        #region 绑定部门和角色
        protected void BindDepartAndRole()
        {
            int recordCount = 0;
            EyouSoft.IBLL.CompanyStructure.ICompanyDepartment departBll = EyouSoft.BLL.CompanyStructure.CompanyDepartment.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> departList = departBll.GetList(SiteUserInfo.CompanyID, 1000, 1, ref recordCount);
            sus_selDepart.DataTextField = "DepartName";
            sus_selDepart.DataValueField = "ID";
            sus_selDepart.DataSource = departList;
            sus_selDepart.DataBind();
            ListItem item1 = new ListItem("请选择部门", "");
            sus_selDepart.Items.Insert(0, item1);
            recordCount = 0;
            EyouSoft.IBLL.CompanyStructure.ICompanyUserRoles roleBll = EyouSoft.BLL.CompanyStructure.CompanyUserRoles.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.CompanyUserRoles> roleList = roleBll.GetList(SiteUserInfo.CompanyID, 1000, 1, ref recordCount);
            sus_selRole.DataTextField = "RoleName";
            sus_selRole.DataValueField = "ID";
            sus_selRole.DataSource = roleList;
            sus_selRole.DataBind();
            ListItem item2 = new ListItem("请选择角色", "");
            sus_selRole.Items.Insert(0, item2);
            departList = null;
            roleList = null;
        }
        #endregion

        #region 初始化子账户
        protected void LoadSonUser(string method)
        {
            sus_selDepart.Value = sonUserModel.DepartId;
            sus_selRole.Value = sonUserModel.RoleID;
            sus_txtTel.Value = sonUserModel.ContactInfo.Tel;
            sus_txtFax.Value = sonUserModel.ContactInfo.Fax;
            sus_txtMobile.Value = sonUserModel.ContactInfo.Mobile;
            sus_txtMSN.Value = sonUserModel.ContactInfo.MSN;
            sus_txtQQ.Value = sonUserModel.ContactInfo.QQ;
            sus_selDepart.Value = sonUserModel.DepartId;
            sus_selRole.Value = sonUserModel.RoleID;
            if (method == "update")//如果修改则隐藏账户名,获取姓名
            {
                account = sonUserModel.UserName;
                sus_txtAccount.Visible = false;
                sus_txtUserName.Value = sonUserModel.ContactInfo.ContactName;
            }
        }
        #endregion

        #region 绑定经营区域
        protected void BindManageArea()
        {
            if (haveArea)
            {
                IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(SiteUserInfo.CompanyID);
                var longAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线);
                sus_rpt_LongAreaList.DataSource = longAreaList;
                sus_rpt_LongAreaList.DataBind();
                var shortAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线);
                sus_rpt_ShortAreaList.DataSource = shortAreaList;
                sus_rpt_ShortAreaList.DataBind();
                var exitAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线);
                sus_rpt_ExitAreaList.DataSource = exitAreaList;
                sus_rpt_ExitAreaList.DataBind();
            }
        }
        #endregion

        #region 修改子账户
        protected void UpdateSonUser(List<EyouSoft.Model.SystemStructure.AreaBase> areaList)
        {
            if (!IsCompanyCheck)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('对不起,你尚未审核通过!');</script>");
                return;
            }
            sonUserModel.DepartId = Utils.GetFormValue(sus_selDepart.UniqueID);
            if (!string.IsNullOrEmpty(sonUserModel.DepartId))
            {
                var departModel =
                    EyouSoft.BLL.CompanyStructure.CompanyDepartment.CreateInstance().GetModel(sonUserModel.DepartId);
                sonUserModel.DepartName = departModel == null ? string.Empty : departModel.DepartName;
            }

            sonUserModel.RoleID = Utils.GetFormValue(sus_selRole.UniqueID);
            if (sus_txtNewPassword1.Value.Trim() != "")
            {
                EyouSoft.Model.CompanyStructure.PassWord pass = new EyouSoft.Model.CompanyStructure.PassWord();
                pass.NoEncryptPassword = sus_txtNewPassword1.Value;
                sonUserModel.PassWordInfo = pass;
            }
            sonUserModel.ContactInfo.Fax = Utils.InputText(sus_txtFax.Value, 50);
            sonUserModel.ContactInfo.Mobile = Utils.InputText(sus_txtMobile.Value, 20);
            sonUserModel.ContactInfo.MSN = Utils.InputText(sus_txtMSN.Value, 50);
            sonUserModel.ContactInfo.QQ = Utils.InputText(sus_txtQQ.Value, 20);
            sonUserModel.ContactInfo.ContactName = Utils.InputText(sus_txtUserName.Value, 20);
            sonUserModel.ContactInfo.Tel = Utils.InputText(sus_txtTel.Value, 20);
            sonUserModel.Area = areaList;
            if (sonUserBll.UpdateChild(sonUserModel))
            {
                ResponseMegSuccess();
            }
            else
            {
                ResponseMegError();
            }
        }
        #endregion

        #region 添加子账户
        protected void AddSonUser(List<EyouSoft.Model.SystemStructure.AreaBase> areaList)
        {
            if (!IsCompanyCheck)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('对不起,你尚未审核通过!');</script>");
                return;
            }
            sonUserModel = new EyouSoft.Model.CompanyStructure.CompanyUser();
            sonUserModel.DepartId = Utils.GetFormValue(sus_selDepart.UniqueID);
            sonUserModel.RoleID = Utils.GetFormValue(sus_selRole.UniqueID);
            EyouSoft.Model.CompanyStructure.PassWord pass = new EyouSoft.Model.CompanyStructure.PassWord();
            pass.NoEncryptPassword = sus_txtNewPassword1.Value;
            sonUserModel.PassWordInfo = pass;
            sonUserModel.ContactInfo.Tel = Utils.InputText(sus_txtTel.Value, 20);
            sonUserModel.ContactInfo.Fax = Utils.InputText(sus_txtFax.Value, 50);
            sonUserModel.ContactInfo.Mobile = Utils.InputText(sus_txtMobile.Value, 20);
            sonUserModel.ContactInfo.MSN = Utils.InputText(sus_txtMSN.Value, 50);
            sonUserModel.ContactInfo.QQ = Utils.InputText(sus_txtQQ.Value, 20);
            sonUserModel.ContactInfo.ContactName = Utils.InputText(sus_txtUserName.Value, 20);
            sonUserModel.UserName = Utils.InputText(sus_txtAccount.Value, 20);
            sonUserModel.CompanyID = SiteUserInfo.CompanyID;
            sonUserModel.Area = areaList;
            EyouSoft.Model.ResultStructure.ResultInfo result = sonUserBll.Add(sonUserModel);
            if (result == EyouSoft.Model.ResultStructure.ResultInfo.Exists)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('该账户已经存在!');window.location='" + Request.Url.ToString() + "';</script>");

            }
            else
                if (result == EyouSoft.Model.ResultStructure.ResultInfo.Succeed)
                {
                    ResponseMegSuccess();
                }
                else if (result == EyouSoft.Model.ResultStructure.ResultInfo.Error)
                {
                    ResponseMegError();
                }

        }
        #endregion

        #region 结果提示
        protected void ResponseMegError()
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('操作失败!');</script>");
        }
        protected void ResponseMegSuccess()
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('操作完成!');window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();window.parent.SonUserManage.refresh();</script>");
        }
        #endregion

        #region 绑定经营范围项操作
        protected string GetItem()
        {
            string str = "";
            if (itemIndex % 4 == 0)
            {
                str = "</tr><tr>";
            }
            itemIndex++;
            return str;
        }
        #endregion

        #region 绑定项时判断账户是否经营该专线
        protected string GetChecked(int Id)
        {
            string isCheck = "";
            if (sonUserModel != null && sonUserModel.Area != null)
            {
                if (sonUserModel.Area.Where(i => i.AreaId == Id).Count() > 0)
                {
                    isCheck = "checked='checked'";
                }
            }
            return isCheck;
        }
        #endregion

        #region 判断账户是否已经存在
        protected void IsIn()
        {
            string userName = Utils.GetQueryStringValue("username");
            if (sonUserBll.IsExists(userName))
            {
                Utils.ResponseMeg(false, "该账户已经存在");
            }
            else
            {
                Utils.ResponseMeg(true, "该账户可以使用");
            }
        }
        #endregion
    }

}
