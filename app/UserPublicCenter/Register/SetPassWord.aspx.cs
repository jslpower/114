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
    /// 功能:通过邮箱链接重置用户密码
    /// 开发人：刘玉灵   时间：2010-10-27
    /// </summary>
    public partial class SetPassWord : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                this.tr_FalseSetPwd.Visible = true;
                this.tr_SetPassWord.Visible = false;
            }
        }


        protected void btnSetPassWord_Click(object sender, EventArgs e)
        {
            string UserName = Server.UrlDecode(Request.Form["txtUserName"]);

            if (String.IsNullOrEmpty(UserName))
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "用户名不能为空");
                return;
            }

            //验证所填用户名是否是从邮箱点过来需要修改密码的用户名
            int errorCode;
            bool isValid = false;
            try
            {
                isValid = UpdateUserPwd.IsTokenValid(UpdateUserPwd.DecodeToken(Request.QueryString["s"]), UserName, out errorCode);
            }
            catch (System.Exception)
            {
                isValid = false;
                errorCode = 3;
            }
            if (isValid)
            {
                string NewPwd = Server.UrlDecode(Request.Form["txtPassWord"]);
                if (UserName != "" && NewPwd != "")
                {
                    EyouSoft.Model.CompanyStructure.CompanyUser Model = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModelByUserName(UserName);
                    if (Model != null)
                    {
                        string UserId = Model.ID;
                        EyouSoft.Model.CompanyStructure.PassWord PwdModel = new EyouSoft.Model.CompanyStructure.PassWord();
                        PwdModel.NoEncryptPassword = NewPwd;
                        if (EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().UpdatePassWord(UserId, PwdModel))
                        {
                            this.tr_TrueSetPwd.Visible = true;
                            this.tr_SetPassWord.Visible = false;
                        }
                        PwdModel = null;
                    }
                    Model = null;
                }
            }
            else {
                if (errorCode == 1)
                {
                    EyouSoft.Common.Function.MessageBox.Show(this.Page, "请填写正确的用户名");
                    return;
                }
                else if (errorCode == 2)
                {
                    this.tr_LinkOut.Visible = true;
                    this.tr_SetPassWord.Visible = false;
                }
                else
                {
                    this.tr_FalseSetPwd.Visible = true;
                    this.tr_SetPassWord.Visible = false;
                }
            }

        }
    }
}
