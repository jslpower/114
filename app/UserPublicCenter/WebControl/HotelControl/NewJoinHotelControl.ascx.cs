using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.WebControl.HotelControl
{
    /// <summary>
    /// 最新加入酒店控件
    /// 功能：显示最新加入酒店信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-12-06 
    /// </summary>
    public partial class NewJoinHotelControl : System.Web.UI.UserControl
    {
        //当前城市ID
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
                /// 获得最新加入酒店列表
                /// </summary>
                this.rpt_NewHotel.DataSource = new EyouSoft.BLL.HotelStructure.HotelLocalInfo().GetList(EyouSoft.Model.HotelStructure.HotelShowType.最新加入酒店, "", 10);
                this.rpt_NewHotel.DataBind();
            }
        }
    }
}