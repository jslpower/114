using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using EyouSoft.Common;

namespace SiteOperationsCenter.usercontrol
{
    public partial class DocFileUpload : System.Web.UI.UserControl
    {
        /// <summary>
        /// 附件上传
        /// </summary>
        /// DYZ 2011-12-22
        /// 
        protected string _ToCompanyId = string.Empty;
        /// <summary>
        /// 指定上传的文件所属的公司ID
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

        protected string _ButtonText = "选择文件";
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