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

namespace UserBackCenter
{
    public partial class MQFriendList : System.Web.UI.Page
    {
        private int MQID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MQID = Utils.GetInt(Request.QueryString["MQID"],0);
                InitMQFriends();
            }
        }

        #region 绑定分组下面的好友
        private void InitMQFriends()
        {
            string GroupName = Utils.InputText(Request.QueryString["GroupName"]);
            EyouSoft.IBLL.MQStructure.IIMFriendList bll = EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance();
            IList<EyouSoft.Model.MQStructure.IMFriendList> list = bll.GetFriends(MQID, GroupName);
            if (list != null && list.Count > 0)
            {
                rptMQFriendList.DataSource = list;
                rptMQFriendList.DataBind();
            }
            list = null;
            bll = null;
        }

        protected string GetFriendInfo(int FriendMQId, string FriendName, bool IsOnline)
        {
            string tmpVal = string.Empty;
            FriendName = Utils.GetText(FriendName, 20,false);
            if (!IsOnline)
            {
                tmpVal = string.Format("<a href=\"javascript:void(0);\" class=\"mqoutline\" style=\"CURSOR:pointer;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&uid={0}')\">{1}</a>", FriendMQId, FriendName);
            }
            else
            {
                tmpVal = string.Format("<a href=\"javascript:void(0);\" class=\"mqonline\" style=\"CURSOR:pointer;\" onclick=\"window.open('http://im.tongye114.com:9000/webmsg.cgi?version=1&uid={0}')\">{1}</a>", FriendMQId, FriendName);
            }
            return tmpVal;
        }
        #endregion
    }
}
