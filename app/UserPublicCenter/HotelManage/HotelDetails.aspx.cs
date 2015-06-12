using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店详细信息页面
    /// 功能：显示普通酒店的详细信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    /// </summary>
    public partial class HotelDetails : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CityAndMenu1.HeadMenuIndex = 4;
                //酒店内最热酒店列表
                this.NewAttrHotelControl1.CityId = this.CityId;
                //特价酒店列表
                this.DiscountControl2.CityId = this.CityId;
                //接收ID进行景区详细信息初始化
                string agencyId = Request.QueryString["Cid"];
                if (agencyId != null)
                {
                    this.GeneralShopControl1.SetAgencyId = agencyId.ToString();
                    this.HotelRightControl1.Cid = this.CityId;
                }
            }
        }

        #region 获得酒店的供求信息
        //protected void GetMessageByHotel()
        //{ 
        //     IList<EyouSoft.Model.CommunityStructure.ExchangeList> modelList= EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(10,EyouSoft.Model.CommunityStructure.ExchangeType.酒店,this.CityId,true);
        //    if (modelList != null && modelList.Count > 0) 
        //    {
        //        this.rptHotelMessage.DataSource = modelList;
        //        this.rptHotelMessage.DataBind();
        //    }
        //}
        #endregion
    }
}
