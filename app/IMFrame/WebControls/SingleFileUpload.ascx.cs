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

namespace IMFrame.WebControls
{
    public partial class SingleFileUpload : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = Domain.ServerComponents;

        private string fPath = string.Empty;
        private FType fType = FType.Logo;

        public FType FType
        {
            get { return fType; }
            set { fType = value; }
        }

        protected string FPath
        {
            get
            {
                if (!this.HavingImg)
                {
                    this.fPath = ImageServerUrl + (FType == FType.Logo ? "/im/images/chat_window_default_logo.png" : "/im/images/chat_window_default_ad_105.png");
                }
                else
                {
                    this.FHeight = 20;
                    this.fPath = ImageServerUrl + (FType == FType.Logo ? "/im/images/chat_window_default_logo_having.png" : "/im/images/chat_window_default_ad_having.png");
                }

                return this.fPath;
            }
        }

        public int FHeight { get; set; }

        public bool HavingImg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }

    public enum FType
    {
        Logo=0,
        Ad
    }
}