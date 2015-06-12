using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.HotelManage
{
    public partial class ImgSecondControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// 图片宽度
        /// </summary>
        private string imageWidth;

        public string ImageWidth
        {
            get { return imageWidth; }
            set { imageWidth = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.litImgSecond.Text = "<a href='javascript:void(0);'><img src=" + ImageManage.GetImagerServerUrl(1) + "/images/hotel/main5.jpg" + "  width=\"" + imageWidth + "\"/></a>";
            }
        }
    }
}