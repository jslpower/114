using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.WebControl.HotelControl
{
    /// <summary>
    /// 特推酒店控件
    /// 功能：显示特推酒店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class SpecialHotelControl : System.Web.UI.UserControl
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
                this.rpt_Special.DataSource = new EyouSoft.BLL.HotelStructure.HotelLocalInfo().GetList(EyouSoft.Model.HotelStructure.HotelShowType.特推酒店, "", 10);
                this.rpt_Special.DataBind();

            }
        }
    }
}