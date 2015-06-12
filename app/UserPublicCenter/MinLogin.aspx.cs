using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter
{
    public partial class MinLogin : System.Web.UI.Page
    {
        /// <summary>
        /// 设置登录成功后的要跳转到的页面URL
        /// PS:如果地址为空，则关闭窗口，刷新父页面。
        /// </summary>
        protected string ReturnUrl = "";
        /// <summary>
        /// 设置登录成功后的进行跳转的Target属性,默认为当前窗口，即弹窗内
        /// _top,_parent,_self,_blank
        /// </summary>
        protected string LocationTarget = "_self";

        /// <summary>
        /// 要在页面上显示的信息
        /// </summary>
        protected string Msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Utils.GetQueryStringValue("ReturnUrl");
            Msg = Utils.GetQueryStringValue("msg");

            if (Request.QueryString["LocationTarget"] != null)
            {
                LocationTarget = Utils.GetQueryStringValue("LocationTarget");
            }
        }
    }
}
    