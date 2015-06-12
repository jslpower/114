using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 信息反馈页用户控件
    /// 功能：显示左边菜单信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04   
    public partial class AboutUsLeftControl : System.Web.UI.UserControl
    {
        #region 定义变量
        protected string ImageServerPath = "";
        private string _setIndex;

        public string SetIndex
        {
            get { return _setIndex; }
            set { _setIndex = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FrontPage page = this.Page as FrontPage;
                ImageServerPath = page.ImageServerUrl;
                ImgInit();
            }
        }
        #region 设置左侧导航背景
        protected void ImgInit()
        {
            switch (this.SetIndex)
            {
                case "1": this.a1.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "2": this.a2.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "3": this.a3.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "4": this.a4.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "5": this.a5.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "6": this.a7.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                case "8": this.a8.Style.Add("background", "url(" + ImageServerPath + "/images/UserPublicCenter/companyon.gif) no-repeat"); break;
                default: break;
            }
        }
        #endregion
    }
}