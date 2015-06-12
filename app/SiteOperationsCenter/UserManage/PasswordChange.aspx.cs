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
namespace SiteOperationsCenter.UserManage
{
    public partial class PasswordChange : EyouSoft.Common.Control.YunYingPage
    {
        protected string userName;
        protected bool haveUpdate = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Utils.GetQueryStringValue("method");
            userName = MasterUserInfo.UserName;
            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_修改密码))
            {
                Utils.ResponseNoPermit(YuYingPermission.帐户管理_修改密码, true);
                return;
            }
            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_修改密码))
            {
                haveUpdate = false;
            }

            if (method == "update")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有修改密码的权限!");
                    return;
                }
                string oldPass = Utils.GetQueryStringValue("oldpass");
                string newPass1 = Utils.GetQueryStringValue("newpass1");
                string newPass2 = Utils.GetQueryStringValue("newpass2");
                if (oldPass == "" || newPass1 == "" || newPass2 == "")
                {
                    Utils.ResponseMegNoComplete();
                    return;
                }
                if (newPass1 != newPass2)
                {
                    Utils.ResponseMeg(false, "前后两次密码不匹配!");
                    return;
                }
                EyouSoft.IBLL.SystemStructure.ISystemUser userBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
                EyouSoft.Model.SystemStructure.SystemUser userModel = userBll.GetSystemUserModel(MasterUserInfo.ID);
                if (userModel != null && userModel.PassWordInfo.NoEncryptPassword == oldPass)
                {
                    if (userBll.UpdateUserPassWord(MasterUserInfo.ID, newPass1) > 0)
                    {
                        Utils.ResponseMeg(true, "密码已修改,请妥善保管!");
                    }
                    else
                    {
                        Utils.ResponseMegError();
                    }
                }
                else
                {
                    Utils.ResponseMeg(false, "原密码不正确!");
                }

            }
           
        }
    }
}
