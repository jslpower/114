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

namespace UserBackCenter.RouteAgency
{
    public partial class SelectChildTourNo : System.Web.UI.Page
    {
        protected int AreaType = 0;
        protected string flag = string.Empty;
        protected string ReleaseType = string.Empty, ContaierID = string.Empty;
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))",DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected void Page_Load(object sender, EventArgs e)
        {
            string AreaValue = Server.UrlDecode(Request.QueryString["AreaValue"]);
            if (!String.IsNullOrEmpty(AreaValue))
            {
                EyouSoft.Model.SystemStructure.SysArea model = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(Utils.GetInt(AreaValue, 0));
                if (model != null)
                {
                    if (model.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                    {
                        AreaType = 1;
                    }
                    else {
                        AreaType = 0;
                    }
                }
            }
            flag = Utils.InputText(Request.QueryString["flag"]);
            ReleaseType = Utils.InputText(Request.QueryString["ReleaseType"]);
            ContaierID = Utils.InputText(Request.QueryString["ContaierID"]);
        }
    }
}
