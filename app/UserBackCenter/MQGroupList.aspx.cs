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

namespace UserBackCenter
{
    public partial class MQGroupList : System.Web.UI.Page
    {
        protected int MQID = 0, FriendCount = 0, GroupCount = 0;
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MQID = Utils.GetInt(Request.QueryString["MQID"], 0);
                if (!String.IsNullOrEmpty(Request.QueryString["IsStep"]) && Request.QueryString["IsStep"] == "1")
                {
                    bool isResult = StepFriends(MQID);
                    Response.Clear();
                    Response.Write(isResult);
                    Response.End();
                }
                else
                {
                    InitMQGroup();
                    InitFriend();
                }
            }
        }
        #region 绑定好友分组
        private void InitMQGroup()
        {
            EyouSoft.IBLL.MQStructure.IIMGroupList bll = EyouSoft.BLL.MQStructure.IMGroupList.CreateInstance();
            IList<EyouSoft.Model.MQStructure.IMGroupList> list = bll.GetFriendGroups(MQID);
            if (list != null && list.Count > 0)
            {
                GroupCount = list.Count;
                this.rptMQGroupName.DataSource = list;
                this.rptMQGroupName.DataBind();
            }
            list = null;
            bll = null;
        }
        #endregion

        #region 绑定不是分组的好友
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

        private void InitFriend()
        {
            EyouSoft.IBLL.MQStructure.IIMFriendList bll = EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance();
            IList<EyouSoft.Model.MQStructure.IMFriendList> list = bll.GetFriends(MQID, string.Empty);
            if (list != null && list.Count > 0)
            {
                rptNoGroupFriend.DataSource = list;
                rptNoGroupFriend.DataBind();
            }

            IList<EyouSoft.Model.MQStructure.IMFriendList> FriendList = bll.GetFriendList(MQID);
            if (FriendList != null && FriendList.Count > 0)
            {
                FriendCount = FriendList.Count;
            }
            FriendList = null;
            list = null;
            bll = null;
        }

        #endregion

        protected bool StepFriends(int MQID)
        {
            EyouSoft.IBLL.MQStructure.IIMFriendList bll = EyouSoft.BLL.MQStructure.IMFriendList.CreateInstance();
            return bll.InStepFriends(MQID);
        }
    }
}

