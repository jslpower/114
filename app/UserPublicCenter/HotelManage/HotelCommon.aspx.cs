using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店常识页面
    /// 功能：显示酒店的常识信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-15
    /// </summary>
    public partial class HotelCommon : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置导航栏
                this.CityAndMenu1.HeadMenuIndex = 4;
                this.HotelSearchControl1.ImageServerPath = this.ImageServerPath;
                //设置用户控件城市ID
                this.CommonUserControl1.CityId = CityId;
                this.SpecialHotelControl1.CityId = CityId;
                this.HotelSearchControl1.CityId = CityId;
                this.HotHotelControl1.CityId = CityId;
                //设置图片控件的宽度
                this.ImgFristControl1.ImageWidth = "223px";
                this.ImgSecondControl1.ImageWidth = "223px";
            }
        }
    }
}
