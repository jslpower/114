using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using EyouSoft.Common;

namespace IMFrame.Ticket
{
    /// <summary>
    /// 修改功能：前台js，图片，样式引用新的方式，添加回程时间查询
    /// 修改人：杜桂云    修改时间：2010-08-14
    /// </summary>
    public partial class _17NATicket : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string TicketManageUrl = "";
        protected string mqID = "";
        protected string mqPass = "";
        protected string helpUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            mqID = SiteUserInfo.ContactInfo.MQ;
            mqPass = SiteUserInfo.PassWordInfo.MD5Password;
            if (!this.Page.IsPostBack)
            {
                if (IsLogin)
                {
                    Adpost.Ticket.Model.TicketQueryResult result = Adpost.Ticket.BLL.Ticket.DefaultUrl(InitSysModel(), InitUserModel());
                    TicketManageUrl =result.TicketGotoUrl;
                }
            }
            helpUrl = Domain.UserPublicCenter + "/HelpCenter/help/TicketHelp.html";
        }

        /// <summary>
        /// 初始化系统信息实体
        /// </summary>
        /// <returns></returns>
        private Adpost.Ticket.Model.TicketSystemModel InitSysModel()
        {
            Adpost.Ticket.Model.TicketSystemModel sysModel = new Adpost.Ticket.Model.TicketSystemModel();
            sysModel.TicketInterfaceUrl = EyouSoft.Common.Domain.TicketSearchUrl;
            return sysModel;
        }

        /// <summary>
        /// 初始化用户信息实体
        /// </summary>
        /// <returns></returns>
        private Adpost.Ticket.Model.TicketUserModel InitUserModel()
        {
            Adpost.Ticket.Model.TicketUserModel userModel = new Adpost.Ticket.Model.TicketUserModel();
            userModel.UserFlag = true;

            ////test 
            //userModel.UserName = "2183";
            //userModel.UserPassword = Adpost.Ticket.BLL.Ticket.MD5Encrypt("000000").ToUpper();

            userModel.UserName = SiteUserInfo.OpUserId.ToString();
            
            userModel.UserPassword = SiteUserInfo.PassWordInfo.MD5Password;
            userModel.TicketGroupGuid = EyouSoft.Common.Domain.TicketDefaultGroupGUID;
            userModel.TrueName = SiteUserInfo.ContactInfo.ContactName;
            userModel.CorpName = SiteUserInfo.CompanyName;
            userModel.Email = SiteUserInfo.ContactInfo.Email;
            userModel.Mobile = SiteUserInfo.ContactInfo.Mobile;
            userModel.QQ = SiteUserInfo.ContactInfo.QQ;
            return userModel;
        }
    }

}
