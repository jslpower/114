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
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace SiteOperationsCenter
{
    /// <summary>
    /// 运营后台登录页面
    /// 创建人：张新兵
    /// 创建时间：2010-07-23
    /// </summary>
    public partial class Login:System.Web.UI.Page
    {
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
