using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.WebControl.HotelControl
{
    /// <summary>
    /// 促销酒店控件
    /// 功能：显示促销酒店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    ///  //当前城市ID
    public partial class PromoHotelControl : System.Web.UI.UserControl
    {
        private int _cityId;

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /// <summary>
                /// 促销酒店初始化
                /// </summary>
                this.rpt_Promotions.DataSource = new EyouSoft.BLL.HotelStructure.HotelLocalInfo().GetList(EyouSoft.Model.HotelStructure.HotelShowType.促销酒店, "", 10);
                this.rpt_Promotions.DataBind();

            }
        }
    }
}