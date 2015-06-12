using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.ComponentModel;
using EyouSoft.Common.Control;

namespace UserBackCenter.usercontrol
{
    public partial class DocFileUpload : System.Web.UI.UserControl
    {
    
        protected string ImageServerUrl = Domain.ServerComponents;

        protected string pageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
                pageTitle = "旅行社后台_同业114";
        }
        
    }
}