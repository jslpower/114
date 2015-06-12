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

namespace TourUnion.WEB.IM
{
    /// <summary>
    /// 页面功能:进入MQ中嵌套的WEB各子系统的跳转页面
    /// 开发人：zzy  开发时间：2010-4-26
    /// </summary>
    public partial class LoginMQWeb : System.Web.UI.Page
    {
        protected string url = "";
        protected bool isTrue = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TourUnion.Account.Model.Login.LoginResultInfo resultinfo = TourUnion.WEB.IM.LoginWeb.LoginWebMQPublic(true, true, TourUnion.Account.Enum.SystemMedia.MQ);

                isTrue = resultinfo.IsSucceed;  //登录成功
                if (isTrue)
                    url = resultinfo.RedirectUrl;
                else
                    url = resultinfo.ErrorInfo.ToString();

                resultinfo = null;
            }
        }
    }
}
