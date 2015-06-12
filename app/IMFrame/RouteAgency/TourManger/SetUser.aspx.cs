using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;

namespace IMFrame.RouteAgency.TourManger
{
    /// <summary>
    /// 设置MQ消息提醒
    /// zhangzy   2010-8-14
    /// </summary>
    public partial class SetUser : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!Page.IsPostBack)
             {
                 if (IsLogin)
                 {
                     InitList();                     
                 }
             }
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        private void InitList()
        {
            EyouSoft.Model.CompanyStructure.QueryParamsUser query = new EyouSoft.Model.CompanyStructure.QueryParamsUser();
            query.IsShowAdmin = true;
            IList<EyouSoft.Model.CompanyStructure.CompanyUserBase> userList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(SiteUserInfo.CompanyID, query);
            query = null;
            if (userList != null && userList.Count > 0)
            {
                IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(SiteUserInfo.CompanyID);
                if (areaList != null && areaList.Count > 0)
                {
                    this.RepeaterList.DataSource = areaList;
                    this.RepeaterList.DataBind();

                    //获得已选中的用户ID
                    IList<EyouSoft.Model.MQStructure.IMAcceptMsgPeople> selectedUserList = EyouSoft.BLL.MQStructure.IMAcceptMsgPeople.CreateInstance().GetAcceptMsgList(SiteUserInfo.CompanyID, null, 0);

                    //注册js
                    StringBuilder scripts = new StringBuilder();
                    scripts.AppendFormat("SetMQMsgUser.Config.CompanyId=\"{0}\";", SiteUserInfo.CompanyID);
                    scripts.AppendFormat("SetMQMsgUser.Config.UserList={0};", Newtonsoft.Json.JsonConvert.SerializeObject(userList));
                    scripts.AppendFormat("SetMQMsgUser.Config.AreaList={0};", Newtonsoft.Json.JsonConvert.SerializeObject(areaList));
                    scripts.AppendFormat("SetMQMsgUser.Config.SelectedUserList={0};", Newtonsoft.Json.JsonConvert.SerializeObject(selectedUserList));
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), scripts.ToString(), true);
                }
                areaList = null;
            }

            userList = null;
        }
    }
}
