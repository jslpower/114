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
    /// <summary>
    /// 页面功能：运营后台账户管理
    /// 开发时间：2010-07-23 开发人：xuty
    /// </summary>
    public partial class UserListManage : EyouSoft.Common.Control.YunYingPage
    {
        protected bool haveUpdate = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_子帐号管理))
            {
                Utils.ResponseNoPermit(YuYingPermission.帐户管理_子帐号管理, true);
                return;
            }
            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_子帐号管理))
            {
                haveUpdate = false;
            }
            string method = Utils.GetQueryStringValue("method");
            if (method == "setState")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限！");
                    return;
                }
                SetUserState();
                return;
               
            }
        }

        #region 设置用户状态
        protected void SetUserState()
        {
            EyouSoft.IBLL.SystemStructure.ISystemUser userBll = EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance();
            int userId = Utils.GetInt(Request.QueryString["id"]);
            string state = Utils.GetQueryStringValue("state");
            if (userBll.SetIsDisable(userId, state == "unforbid"))
            {
                Utils.ResponseMegSuccess();
            }
            else
            {
                Utils.ResponseMegError();
            }
            
        }
        #endregion
    }
}
