using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.ComponentModel;

namespace SiteOperationsCenter.usercontrol
{
    /// <summary>
    /// 供求信息菜单
    /// </summary>
    /// 周文超 2010-07-27
    public partial class GuestInterviewMenu : System.Web.UI.UserControl
    {
        protected string ImageServerUrl = "";
        private string htmlaclass = "button";
        private string currhtmlaclass = "button_xz";
        /// <summary>
        /// 菜单索引
        /// </summary>
        [Bindable(true)]
        public int MenuIndex
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            YunYingPage page = this.Page as YunYingPage;
            ImageServerUrl = page.ImageServerUrl;


            a1.Attributes.Add("class", htmlaclass);
            a2.Attributes.Add("class", htmlaclass);
            a3.Attributes.Add("class", htmlaclass);
            switch (MenuIndex)
            {
                case 1:
                    a1.Attributes.Add("class", currhtmlaclass);
                    break;
                case 2:
                    a2.Attributes.Add("class", currhtmlaclass);
                    break;
                case 3:
                    a3.Attributes.Add("class", currhtmlaclass);
                    break;
                default:
                    a1.Attributes.Add("class", currhtmlaclass);
                    break;
            }     
        }
    }
}