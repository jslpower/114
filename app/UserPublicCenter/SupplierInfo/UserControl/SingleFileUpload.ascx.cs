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
namespace SupplierInfo.UserControl
{
    public partial class SingleFileUpload : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = Domain.ServerComponents;

        protected int _ImageWidth = 300;
        [Bindable(true)]
        public int ImageWidth
        {
            set
            {
                _ImageWidth = value;
            }
        }

        protected int _ImageHeight = 300;
        [Bindable(true)]
        public int ImageHeight
        {
            set
            {
                _ImageHeight = value;
            }
        }

        protected string _JsMethodfileQueued = "fileQueued";
        [Bindable(true)]
        public string JsMethodfileQueued
        {
            set
            {
                _JsMethodfileQueued = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}