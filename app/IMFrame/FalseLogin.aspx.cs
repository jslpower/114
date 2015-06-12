using System;
using EyouSoft.Security.Membership;
using EyouSoft.SSOComponent.Entity;

namespace IMFrame
{
    public partial class FalseLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string u = TextBox1.Text.Trim() ;
            string p = TextBox2.Text.Trim();

            UserInfo _userInfo = new UserProvider().MQLogin(Convert.ToInt32(u), p);

            if (_userInfo == null)
            {
                lbl1.Visible = true;
                lbl1.Text = "用户名或者密码错误";
                return;
            }

            UserProvider.GenerateIMFrameUserLoginCookies(UserProvider.GenerateSSOToken(u),u);

            lbl1.Visible = true;
            lbl1.Text = "登录成功，请自行在浏览器中输入地址进行跳转";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel("db9a870b-80e6-499c-9836-84e578affc42");

            //EyouSoft.Common.Email.ReminderEmailHelper.SendAddFriendEmail(model.ContactInfo.ContactName, "roy_bluesky@163.com", "2123");
        }
    }
}
