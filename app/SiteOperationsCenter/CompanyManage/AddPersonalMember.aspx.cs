﻿using System;
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
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 个人会员添加 2012-1-12
    /// 蔡永辉
    /// </summary>
    public partial class AddPersonalMember : EyouSoft.Common.Control.YunYingPage
    {
        protected string userid = "";
        protected string companyid = "";
        protected StringBuilder strArea = null;
        protected StringBuilder strDepartment = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Utils.GetQueryStringValue("UserId");
            companyid = Utils.GetQueryStringValue("CompanyId");
            if (!IsPostBack)
            {
                BindDropdownList();
            }
            InitData();
        }

        #region 初始化数据
        protected void InitData()
        {
            CompanyUser InitmodelUser = new CompanyUser();
            InitmodelUser = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(userid);
            if (InitmodelUser != null)
            {
                txt_UserName.Value = InitmodelUser.UserName;
                txt_MQNickname.Value = InitmodelUser.MqNickName;
                txt_Post.Value = InitmodelUser.Job;
                txt_MQNickname.Value = InitmodelUser.MqNickName;

                #region 个人会员联系信息
                txt_RealName.Value = InitmodelUser.ContactInfo.ContactName;
                dropSex.SelectedValue = ((int)InitmodelUser.ContactInfo.ContactSex).ToString();
                txt_Email.Value = InitmodelUser.ContactInfo.Email;
                txt_Fax.Value = InitmodelUser.ContactInfo.Fax;
                txt_MQ.Value = InitmodelUser.ContactInfo.MQ;
                txt_MSN.Value = InitmodelUser.ContactInfo.MSN;
                txt_QQ.Value = InitmodelUser.ContactInfo.QQ;
                txt_tel.Value = InitmodelUser.ContactInfo.Tel;
                txt_Mobile.Value = InitmodelUser.ContactInfo.Mobile;
                #endregion

                GetlineByCompanyId(InitmodelUser.Area);
                dropDepartment.SelectedValue = InitmodelUser.DepartId;
                dropPermissions.SelectedValue = InitmodelUser.RoleID;
            }
        }

        #endregion

        #region 保存修改数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CompanyUser modelUser = new CompanyUser();
            modelUser = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(userid);
            if (modelUser != null)
            {
                MLBYNUserInfo a = new MLBYNUserInfo();
                string password = Utils.GetFormValue(txt_Password.UniqueID);
                if (!string.IsNullOrEmpty(password))
                {
                    modelUser.PassWordInfo.NoEncryptPassword = password;
                }
                modelUser.UserName = Utils.GetFormValue(txt_UserName.UniqueID);
                modelUser.MqNickName = Utils.GetFormValue(txt_MQNickname.UniqueID);
                modelUser.Job = Utils.GetFormValue(txt_Post.UniqueID);
                modelUser.MqNickName = Utils.GetFormValue(txt_MQNickname.UniqueID);

                #region 个人会员联系信息
                ContactPersonInfo ContactInfo = new ContactPersonInfo();
                ContactInfo.ContactName = Utils.GetFormValue(txt_RealName.UniqueID);
                ContactInfo.ContactSex = (Sex)Enum.Parse(typeof(Sex), Utils.GetFormValue(dropSex.UniqueID));
                ContactInfo.Email = Utils.GetFormValue(txt_Email.UniqueID);
                ContactInfo.Fax = Utils.GetFormValue(txt_Fax.UniqueID);
                ContactInfo.Mobile = Utils.GetFormValue(txt_Mobile.UniqueID);
                ContactInfo.MQ = Utils.GetFormValue(txt_MQ.UniqueID);
                ContactInfo.MSN = Utils.GetFormValue(txt_MSN.UniqueID);
                ContactInfo.QQ = Utils.GetFormValue(txt_QQ.UniqueID);
                ContactInfo.Tel = Utils.GetFormValue(txt_tel.UniqueID);
                #endregion

                #region 线路区域
                List<AreaBase> listArea = new List<AreaBase>();
                foreach (string AreaId in Utils.GetFormValues("chbArea"))
                {
                    AreaBase modelAreaBase = new AreaBase();
                    if (!string.IsNullOrEmpty(AreaId))
                    {
                        modelAreaBase.AreaId = Utils.GetInt(AreaId);
                        listArea.Add(modelAreaBase);
                    }
                    modelAreaBase = null;

                }
                modelUser.Area = listArea;
                #endregion

                modelUser.ContactInfo = ContactInfo;
                if (EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().UpdateChild(modelUser))
                {
                    MessageBox.ShowAndRedirect(Page, "修改成功", "PersonalMemberList.aspx");
                }
                else
                    MessageBox.Show(Page, "修改失败");
            }

        }
        #endregion

        #region  关闭
        protected void btnClosed_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CompanyManage/PersonalMemberList.aspx");
        }
        #endregion

        #region 获取公司的专线
        protected void GetlineByCompanyId(List<AreaBase> listUserArea)
        {
            strArea = new StringBuilder();
            int i = 1;
            if (!string.IsNullOrEmpty(companyid))
            {
                IList<AreaBase> listLine = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(companyid);
                if (listLine != null)
                {

                    foreach (AreaBase modelArea in listLine)
                    {

                        if (listUserArea != null)
                        {
                            foreach (AreaBase modelUserArea in listUserArea)
                            {
                                if (modelUserArea.AreaId == modelArea.AreaId)
                                    strArea.Append(string.Format("<input type=\"checkbox\" checked=\"checked\" name=\"chbArea\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", modelUserArea.AreaId, modelUserArea.AreaId, modelUserArea.AreaName));
                                else
                                    strArea.Append(string.Format("<input type=\"checkbox\" name=\"chbArea\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", modelUserArea.AreaId, modelUserArea.AreaId, modelUserArea.AreaName));
                            }
                        }
                        else
                            strArea.Append(string.Format("<input type=\"checkbox\" name=\"chbArea\" value=\"{0}\" id=\"cbxt_{1}\" /><label for=\"cbxt_{1}\">{2}</label>", modelArea.AreaId, modelArea.AreaId, modelArea.AreaName));

                        if (i % 4 == 0)
                        {
                            strArea.Append("<br>");
                        }
                        i++;
                    }
                }
            }
        }

        #endregion

        #region 绑定下拉列表
        protected void BindDropdownList()
        {
            //权限列表
            int recount = 0;
            dropPermissions.DataSource = EyouSoft.BLL.CompanyStructure.CompanyUserRoles.CreateInstance().GetList(companyid, 1000, 1, ref recount);
            dropPermissions.DataTextField = "RoleName";
            dropPermissions.DataValueField = "ID";
            dropPermissions.DataBind();

            //部门
            IList<CompanyDepartment> listdepart = EyouSoft.BLL.CompanyStructure.CompanyDepartment.CreateInstance().GetList(companyid);
            this.dropDepartment.DataSource = listdepart;
            this.dropDepartment.DataTextField = "DepartName";
            this.dropDepartment.DataValueField = "ID";
            this.dropDepartment.DataBind();

            //性别
            this.dropSex.DataSource = EnumObj.GetList(typeof(Sex));
            this.dropSex.DataTextField = "Text";
            this.dropSex.DataValueField = "Value";
            this.dropSex.DataBind();
        }
        #endregion
    }
}