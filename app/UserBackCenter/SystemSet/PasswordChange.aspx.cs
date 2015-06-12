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
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 页面功能：密码修改
    /// 开发人：xuty 开发时间：2010-07-06
    /// 修改人：徐从栎
    /// 修改时间：2011-12-19
    /// </summary>
    public partial class PasswordChange : BackPage
    {
        protected string account;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!CheckGrant(TravelPermission.系统设置_管理栏目))
            //{
            //    Utils.ResponseNoPermit();
            //    return;
            //}
            string method = Utils.GetFormValue("method");
            if (method == "save")
            {
                UpdatePass();//修改密码
            }
            account = SiteUserInfo.UserName;
        }

        #region 修改密码
        protected void UpdatePass()
        {
            if (!IsCompanyCheck)
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string oldPass = Utils.GetFormValue("oldpass");//换取表单原密码
            string newPass = Utils.GetFormValue("newPass");//获取新密码
            if (oldPass == "" || newPass == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                EyouSoft.IBLL.CompanyStructure.ICompanyUser userBll = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
                string oldPass1 = userBll.GetModel(SiteUserInfo.ID).PassWordInfo.NoEncryptPassword;
                if (oldPass1 != oldPass)//判断原密码是否正确
                {
                    Utils.ResponseMeg(false, "原密码错误");
                }
                else
                {   
                    EyouSoft.Model.CompanyStructure.PassWord pass=new EyouSoft.Model.CompanyStructure.PassWord();
                    pass.NoEncryptPassword = newPass;//设置新密码
                    if (userBll.UpdatePassWord(SiteUserInfo.ID, pass)) //更新密码
                    {
                        Utils.ResponseMeg(true, "修改成功!");
                    }
                    else
                    {
                        Utils.ResponseMegError();
                    }
                }
            }
        }
        #endregion
    }
}

