using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.Register
{
    /// <summary>
    /// 找回密码
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class FindPassWord : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty( Request.QueryString["isExitEmail"]))
            {
                string UserName = Server.UrlDecode(Request.QueryString["UserName"]);
                string Email = Server.UrlDecode(Request.QueryString["Email"]);
                Response.Write(isExitEmail(UserName,Email));
                Response.End();
 
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SendToEmail"]))
            {
                this.tr_FindPwd.Visible = false;
                this.tr_SendToEmail.Visible = true;
            }
        }
        /// <summary>
        /// 验证Email与会员用户信息是否相同
        /// </summary>
        protected bool isExitEmail(string UserName,string Email)
        {
            bool isPass = false;
            if (UserName != "" && Email != "")
            {
                EyouSoft.Model.CompanyStructure.CompanyUser Model = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModelByUserName(UserName);
                if (Model != null)
                {
                    string UserEmail = Model.ContactInfo.Email.ToString();
                    if (!string.IsNullOrEmpty(UserEmail))
                    {
                        //邮件比较的时候，忽略大小写
                        if (Email.Equals(UserEmail, StringComparison.OrdinalIgnoreCase))
                        {
                            isPass = true;
                        }
                      
                    }
                }
                Model = null;
            }
            return isPass;
        }

        /// <summary>
        /// 发送重置密码邮件
        /// </summary>
        protected void btnFindPassWord_Click(object sender, EventArgs e)
        {
            this.btnFindPassWord.Enabled = false;
            string strUserName = Request.Form["txtUserName"];
            string strEmail =Request.Form["txtContactEmail"];

            string errMessage = "";

            #region 页面验证
            if (string.IsNullOrEmpty(strUserName))
            {
                errMessage += "登录用户名不能为空!\\n";
            }
            if (string.IsNullOrEmpty(strEmail))
            {
                errMessage += "邮箱地址不能为空!\\n";
            }
            else {
                if (!EyouSoft.Common.Function.StringValidate.IsEmail(strEmail))
                {
                    errMessage += "请输入正确的Email地址!\\n";
                }
                else {
                    if (!isExitEmail(strUserName, strEmail))
                    {
                        errMessage += "Email与该用户不匹配!!\\n";
                    }
                }
            }
            #endregion 
            if (errMessage != "")
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, errMessage);
                return;
            }
            else
            {
                //SSO票据=DES加密(新字符串);
                string token = UpdateUserPwd.EncodeToken(UpdateUserPwd.GenerateToken(strUserName));
                string findPwdLink =Domain.UserPublicCenter + "/Register/SetPassWord.aspx?s=" + token;
                //发送邮件成功
                if (EyouSoft.Common.Email.ReminderEmailHelper.SendUpdatePasswordEmail(strEmail, findPwdLink, Domain.UserPublicCenter + "/Register/FindPassWord.aspx"))
                {
                    Response.Redirect("/Register/FindPassWord.aspx?SendToEmail=1");
                }
            }
        }
    }
}
