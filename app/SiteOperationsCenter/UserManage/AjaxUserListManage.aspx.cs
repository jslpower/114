using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Common.Function;
namespace SiteOperationsCenter.UserManage
{
    /// <summary>
    /// 页面功能：Ajax获取账户列表
    /// 开发时间：2010-07-23 开发人：xuty
    /// </summary>
    public partial class AjaxUserListManage : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageIndex;
        protected int pageSize=15;
        protected int recordCount;
        protected int itemIndex = 0;
        protected string result;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_子帐号管理))
            {
                Utils.ResponseNoPermit(YuYingPermission.帐户管理_子帐号管理, true);
                return;
            }
           
            string method = Utils.GetFormValue("method");
            if (method == "delete")
            {
                if (!CheckMasterGrant(YuYingPermission.帐户管理_管理该栏目, YuYingPermission.帐户管理_子帐号管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.帐户管理_子帐号管理,true);
                    return;
                }
                DeleteUser();
            }
            pageIndex = Utils.GetInt(Request.Form["Page"], 1);
            itemIndex = (pageIndex - 1) * pageSize + 1;
            BindUserList();
        }

        #region 删除账户
        protected void DeleteUser()
        {
            string userIds= Utils.GetFormValue("userid");
            IList<int> idList = userIds.Split(',').Select(i=>Utils.GetInt(i)).ToList<int>();
            if (EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance().DeleteSystemUser(idList))
            {
                result = "<script>alert('删除成功!')</script>";
            }
            else
            {
                result = "<script>alert('删除失败!')</script>";
            }
        }
        #endregion

        #region 绑定账户列表
        protected void BindUserList()
        {  
           int cityId=Utils.GetInt(Request.Form["city"]);
           string userName=Utils.GetFormValue("username");
           string realName=Utils.GetFormValue("realname");
           IList<EyouSoft.Model.SystemStructure.SystemUser> userList =EyouSoft.BLL.SystemStructure.SystemUser.CreateInstance().GetSystemUserList(pageSize, pageIndex, ref recordCount, 1, cityId, userName, realName);
           if (userList.Count > 0)
           {
               aulm_rpt_UserList.DataSource = userList;
               aulm_rpt_UserList.DataBind();
               BindPage();
           }
           else
           {
               aulm_rpt_UserList.EmptyText = "暂无账户信息";
               ExporPageInfoSelect1.Visible = false;
           }
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
            this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "UserListManage.loadData(this);", 1);
            this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "UserListManage.loadData(this);", 0);
        }
        #endregion


    }
}
