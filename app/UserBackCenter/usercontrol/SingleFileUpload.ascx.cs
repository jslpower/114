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
namespace Demo1.usercontrol
{
    public partial class SingleFileUpload : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = Domain.ServerComponents;

        protected int _ImageWidth = 1024;
        [Bindable(true)]
        public int ImageWidth
        {
            set
            {
                _ImageWidth = value;
            }
        }

        protected int _ImageHeight = 768;
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

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        private int _themImageWidth = 400;
        [Bindable(true)]
        public int ThemImageWidth
        {
            get { return _themImageWidth; }
            set { _themImageWidth = value; }
        }
        /// <summary>
        /// 缩略图高度
        /// </summary>
        private int _themImageHeight = 300;
        [Bindable(true)]
        public int ThemImageHeight
        {
            get { return _themImageHeight; }
            set { _themImageHeight = value; }
        }

        private bool _isGenerateThumbnail = false;
        /// <summary>
        /// 是否生成小的缩略图
        /// </summary>
        [Bindable(true)]
        public bool IsGenerateThumbnail
        {
            get { return _isGenerateThumbnail; }
            set { _isGenerateThumbnail = value; }
        }



        protected string pageTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageTitle = "旅行社后台_同业114";
        }
    }
}