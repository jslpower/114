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
using System.ComponentModel;
using EyouSoft.Common;

namespace SiteOperationsCenter.usercontrol
{
    public partial class SingleFileUpload : System.Web.UI.UserControl
    {
        protected int _ImageWidth = 1024;
        /// <summary>
        /// 指定图片上传后要生成的宽度
        /// </summary>
        [Bindable(true)]
        public int ImageWidth
        {
            set
            {
                _ImageWidth = value;
            }
        }

        protected int _ImageHeight = 768;
        /// <summary>
        /// 指定图片上传后要生成的高度
        /// </summary>
        [Bindable(true)]
        public int ImageHeight
        {
            set
            {
                _ImageHeight = value;
            }
        }

        private int _ThemImageWidth = 400;
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        [Bindable(true)]
        protected int ThemImageWidth
        {
            get { return _ThemImageWidth; }
            set { _ThemImageWidth = value; }
        }

        private int _ThemImageHeight = 300;
        /// <summary>
        /// 缩略图高度
        /// </summary>
        [Bindable(true)]
        public int ThemImageHeight
        {
            get { return _ThemImageHeight; }
            set { _ThemImageHeight = value; }
        }

        protected string _ToCompanyId = string.Empty;
        /// <summary>
        /// 指定上传的图片所属的公司ID
        /// </summary>
        [Bindable(true)]
        public string ToCompanyId
        {
            set
            {
                _ToCompanyId = value;
            }
        }

        protected SiteOperationsCenterModule _SiteModule = SiteOperationsCenterModule.平台管理;
        /// <summary>
        /// 指定图片上传所在的功能模块
        /// </summary>
        [Bindable(true)]
        public EyouSoft.Common.SiteOperationsCenterModule SiteModule
        {
            set
            {
                _SiteModule = value;
            }
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

        protected string _ButtonText = "选择图像文件";
        /// <summary>
        /// 按钮文本
        /// </summary>
        [Bindable(true)]
        public string ButtonText
        {
            set
            {
                _ButtonText = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}