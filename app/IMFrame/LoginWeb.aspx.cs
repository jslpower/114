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
    /// 页面功能:进入纯WEB的各子系统的跳转页面
    /// 开发人：zzy  开发时间：2010-8-18
    /// </summary>
    public partial class LoginWeb : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsLogin)
                {
                    //goto参数表示为要跳转的目的地地址(在mq服务端进行配置)
                    string strUrl = GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + "/" + Server.UrlDecode(Request.QueryString["goto"]));
                    Response.Redirect(strUrl);
                    Response.End();
                }
            }
        }
    }
}
