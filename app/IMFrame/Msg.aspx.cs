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

namespace IMFrame
{
    /// <summary>
    /// 页面功能:通过消息中心提供的url,在消息的链接地址表中查询用户id并中转登录到b/s页面
    /// zhangzy   2010-8-14
    /// </summary>
    public partial class Msg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder strJs = new StringBuilder();
                strJs.AppendFormat("<script>alert('该地址已失效!');window.location='{0}';</script>", EyouSoft.Common.Domain.UserPublicCenter);

                string message_id = Request.QueryString["g"];
                if (!string.IsNullOrEmpty(message_id))
                {
                    EyouSoft.Model.MQStructure.IMMessageUrl model = EyouSoft.BLL.MQStructure.IMMessageUrl.CreateInstance().GetIm_Message_Url(message_id);
                    if (model != null)
                    {
                        string gotourl = EyouSoft.Common.Utils.GetDesPlatformUrlForMQMsg(model.RedirectUrl, model.MQID.ToString(), model.MD5Pw);
                        model = null;
                        if (!string.IsNullOrEmpty(gotourl))
                        {
                            Response.Redirect(gotourl, true);
                            return;
                        }
                    }
                    model = null;
                }

                Response.Write(strJs.ToString());
                Response.End();
            }
        }
    }
}
