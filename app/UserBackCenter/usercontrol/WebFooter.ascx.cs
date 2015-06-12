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

namespace UserBackCenter.usercontrol
{
    public partial class WebFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.IBLL.SystemStructure.ISystemInfo bll = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance();
            EyouSoft.Model.SystemStructure.SystemInfo model = bll.GetSystemInfoModel();
            if (model != null) {
                this.ltrCopyRight.Text = model.AllRight;
            }
            model = null;
            bll = null;
        }
    }
}