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
using System.ComponentModel;

namespace UserBackCenter.TicketsCenter.usercontrols
{
    public partial class FileUploadControl : System.Web.UI.UserControl
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

        protected bool _IsUploadSwf = false;
        /// <summary>
        /// 是否允许上传flash
        /// </summary>
        [Bindable(true)]
        public bool IsUploadSwf
        {
            set
            {
                _IsUploadSwf = value;
            }
        }

        protected string pageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageTitle = "机票供应商_同业114";
        }
    }
}