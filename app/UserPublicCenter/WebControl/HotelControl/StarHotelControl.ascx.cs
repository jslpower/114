using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.WebControl.HotelControl
{
    /// <summary>
    /// 明星酒店控件
    /// 功能：显示明星酒店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class StarHotelControl : System.Web.UI.UserControl
    {
        //当前城市ID
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        private string _imageServerPath;

        public string ImageServerPath
        {
            get { return _imageServerPath; }
            set { _imageServerPath = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                /// <summary>
                /// 获得明星酒店列表
                /// </summary>
                this.rpt_StarHotel.DataSource = new EyouSoft.BLL.HotelStructure.HotelLocalInfo().GetList(EyouSoft.Model.HotelStructure.HotelShowType.明星酒店推荐, "", 8);
                this.rpt_StarHotel.DataBind();
            }
        }
    }
}