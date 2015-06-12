using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
namespace UserPublicCenter.HomeControl
{   
    /// <summary>
    /// 酒店信息
    /// xuty 2011/5/19
    /// </summary>
    public partial class Hotels : System.Web.UI.UserControl
    {
        protected string hotelSh;//上海
        protected string hotelHz;//杭州
        protected string hotelSz;//苏州
        protected string hotelNj;//南京
        protected string hotelWx;//无锡
        protected string hotelGa;//港澳
      
        protected string ImageServerPath;
        protected void Page_Load(object sender, EventArgs e)
        {   
           ImageServerPath = (Page as EyouSoft.Common.Control.FrontPage).ImageServerUrl;
            EyouSoft.IBLL.HotelStructure.IBNewHotel hotelBll = EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance();
            StringBuilder strBuilder = new StringBuilder();
            IList <EyouSoft.Model.HotelStructure.MNewHotelInfo> hotelList1=hotelBll.GetList(EyouSoft.Model.HotelStructure.CityAreaType.港澳, null, 6);
            if (hotelList1 != null && hotelList1.Count > 0)
            {
                foreach (EyouSoft.Model.HotelStructure.MNewHotelInfo model in hotelList1)
                {
                    strBuilder.AppendFormat("<tr><td height=\"26\"><a title=\"{5}\">{0}</a></td><td><div style=\"width:63px;height:12px;background:url({4}/images/new2011/index/index30_69.gif) no-repeat;background-position:{1}px;\"></div></td><td><b style=\"color:#5c9008;\">￥{2}</b></td><td><a target=\"blank\" href=\"tencent://message/?uin={3}&site=同业114&menu=yes\" class=\"rotf\">点击询问</a></td></tr>", Utils.GetText(model.HotelName, 12, true), -(5 - (int)model.HotelStar) * 12, Utils.GetMoney(model.MenShiPrice), model.QQ, ImageServerPath, model.HotelName);
                }
            }
            hotelGa = strBuilder.ToString();
           
            IList<EyouSoft.Model.HotelStructure.MNewHotelInfo> hotelList2 = hotelBll.GetList(EyouSoft.Model.HotelStructure.CityAreaType.华东五市, null, 6);
            var groupList=hotelList2.GroupBy(i => i.CityName);
            foreach (var group in groupList)
            {
                switch (group.Key)
                {
                    case "上海":
                        hotelSh = GetHotelHtml(group);
                        break;
                    case "杭州":
                        hotelHz = GetHotelHtml(group);
                        break;
                    case "苏州":
                        hotelSz = GetHotelHtml(group);
                        break;
                    case "南京":
                        hotelNj = GetHotelHtml(group);
                        break;
                    case "无锡":
                        hotelWx = GetHotelHtml(group);
                        break;
                }
            }
            
        }
        /// <summary>
        /// 获取酒店html
        /// </summary>
        /// <param name="hotelList"></param>
        /// <returns></returns>
        protected string GetHotelHtml(IGrouping<string,EyouSoft.Model.HotelStructure.MNewHotelInfo> hotelList)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (hotelList != null)
            {
                foreach (EyouSoft.Model.HotelStructure.MNewHotelInfo model in hotelList)
                {
                    strBuilder.AppendFormat("<tr><td height=\"26\"><a title=\"{5}\">{0}</a></td><td><div style=\"width:63px;height:12px;background:url('{4}/images/new2011/index/index30_69.gif') no-repeat;background-position:{1}px;\"></div></td><td><b style=\"color:#5c9008;\">￥{2}</b></td><td><a target=\"blank\" href=\"tencent://message/?uin={3}&site=同业114&menu=yes\" class=\"rotf\">点击询问</a></td></tr>", Utils.GetText(model.HotelName, 11, true), -(5 - (int)model.HotelStar) * 12, Utils.GetMoney(model.MenShiPrice), model.QQ, ImageServerPath, model.HotelName);
                }
            }
            return strBuilder.ToString();
        }
      
    }
}