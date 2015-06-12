using System;
using System.Collections;
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
using System.Collections.Generic;


namespace UserPublicCenter
{
    /// <summary>
    /// 平台母版页
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class PublicCenterMasterPage : System.Web.UI.MasterPage
    {
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
