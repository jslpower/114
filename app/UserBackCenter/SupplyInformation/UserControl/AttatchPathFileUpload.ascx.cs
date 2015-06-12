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
using System.ComponentModel;
namespace SupplyInformation.UserControl
{
    public partial class AttatchPathFileUpload : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = Domain.ServerComponents;
        /// <summary>
        /// 上次上传的文件路径
        /// </summary>
        public string LastDocFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    } 
}