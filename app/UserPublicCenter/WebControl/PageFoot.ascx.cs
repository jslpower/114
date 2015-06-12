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

namespace UserPublicCenter.WebControl
{
    public partial class PageFoot : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUnionRight();
            }
        }

        /// <summary>
        /// 版权
        /// </summary>
        protected string UnionRight = "";
        /// <summary>
        /// 获的版权
        /// </summary>
        protected void GetUnionRight()
        {
            EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (Model != null)
            {
                UnionRight = Model.AllRight;
            }

            Model = null;
        }
    }
}