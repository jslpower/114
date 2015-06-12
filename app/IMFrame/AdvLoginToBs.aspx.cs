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

namespace TourUnion.WEB.IM
{
    /// <summary>
    /// 页面功能:广告提供的url,通过用户id登录到b/s页面[仅限与MQ聊天窗口的链接登录] 
    /// </summary>
    public partial class AdvLoginToBs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    StringBuilder strJs = new StringBuilder();
                    strJs.Append("请您先进行登录!");
                    strJs.AppendFormat("<script>settimeout(window.location='{0}', 2000);</script>", "http://" + Request.ServerVariables["SERVER_NAME"]);

                    //从MQ广告登录到web页面,考虑安全性,因此在过了广告期后,不再执行以下代码
                    //zhangzy   2010-3-1
                    //Response.Write(strJs.ToString());
                    //Response.End();
                    //return;

                    TourUnion.Account.Model.Login.LoginResultInfo resultinfo = TourUnion.WEB.IM.LoginWeb.LoginWebMQPublic(false, false, TourUnion.Account.Enum.SystemMedia.Web);
                    bool isTrue = resultinfo.IsSucceed;
                    string error = resultinfo.ErrorInfo.ToString();
                    resultinfo = null;
                    //判断有无登录成功,并跳转
                    if (isTrue)
                    {
                        Response.Redirect(Request.QueryString["CsToBsRedirectUrl"], true);
                    }
                    else
                    {
                        strJs.Insert(0, error.ToString());
                    }
                }
            }
        }
    }
}
