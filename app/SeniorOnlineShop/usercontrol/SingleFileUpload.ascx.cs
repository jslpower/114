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
namespace SeniorOnlineShop.usercontrol
{
    /// <summary>
    /// 团队制定-行程要求上传 
    /// 创建者：luofx 时间：2010-11-11
    /// </summary>
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

        protected string pageTitle = "团队定制_同业114旅游交易平台";
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}