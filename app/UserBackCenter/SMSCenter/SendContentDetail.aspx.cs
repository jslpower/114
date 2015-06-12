using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 查看某一消息记录的发送成功/失败的号码(内容)
    /// </summary>
    public partial class SendContentDetail : EyouSoft.Common.Control.BasePage
    {
        //查看类型 1:内容 0:号码
        private int isSendContent = 0;
        private string TotalId = "";
        //发送状态 0:所有 1:成功 2:失败
        private int SendStatus = 0;
        private string strErr = "";
        protected string MessageInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            isSendContent = Utils.GetInt(Request.QueryString["isSendContent"]);
            SendStatus = Utils.GetInt(Request.QueryString["SendStatus"]);
            TotalId = Request.QueryString["TotalId"];
            strErr = "alert('没有找到有关信息!');window.parent.Boxy.getIframeDialog(" + Request.QueryString["iframeId"] + ").hide()";
            if (string.IsNullOrEmpty(TotalId))
            {
                EyouSoft.Common.Function.MessageBox .ResponseScript(this.Page,strErr);
                return;
            }
            if (!Page.IsPostBack)
            {
                this.GetMessage();
            }
        }
        protected void GetMessage()
        {
            string Message = "";
            StringBuilder strMessage = new StringBuilder();
            if (isSendContent == 1)//查看内容
            {
                Message = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance().GetSendContent(TotalId, SiteUserInfo.CompanyID);
                if (string.IsNullOrEmpty(Message))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, strErr);
                    return;
                }
                else
                {
                    strMessage.Append(Message);
                }
            }
            else
            {
                IList< EyouSoft.Model.SMSStructure.SendDetailInfo> list = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance().GetSendDetails(TotalId, SiteUserInfo.CompanyID, SendStatus);
                if (list != null && list.Count > 0)
                {
                    foreach (EyouSoft.Model.SMSStructure.SendDetailInfo model in list)
                    {
                        strMessage.AppendFormat("<div>{0}  {1}</div>", EyouSoft.Common.Utils.GetEncryptMobile(model.Mobile, model.IsEncrypt), model.ReturnResult == 0 ? "发送成功!" : "发送失败!");
                    }
                }

            }
            MessageInfo = strMessage.ToString();

        }


    }
}
