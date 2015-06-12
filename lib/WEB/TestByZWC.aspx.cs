using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.NewsStructure;

namespace WEB
{
    public partial class TestByZwc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;
            var list =
                EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(362);

        }
    }
}
