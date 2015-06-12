using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.HotelStructure;
using System.Text;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 说明：用户后台—旅游资源—酒店团单(查看)
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class GroupOrderShow : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.initData(Utils.GetQueryStringValue("id"));
            }
        }
        protected void initData(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;
            EyouSoft.BLL.HotelStructure.HotelTourCustoms BLL = new EyouSoft.BLL.HotelStructure.HotelTourCustoms();
            HotelTourCustoms Model = BLL.GetModel(Utils.GetInt(id));
            if (null == Model)
                return;
            this.lbStartTime.Text = string.Format("{0:yyyy-MM-dd}—{1:yyyy-MM-dd}",Model.LiveStartDate,Model.LiveEndDate);//入住时间
            EyouSoft.Model.HotelStructure.HotelCity hotelCityModel = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetModel(Model.CityCode);
            if (null != hotelCityModel)
            {
                this.lbCityName.Text = hotelCityModel.CityName;
            }
            this.lbPlace.Text = Model.LocationAsk;//地理位置要求
            this.lbIsPointHotel.Text =string.IsNullOrEmpty(Model.HotelName.Trim())?"无":"是";//是否有指定酒店
            this.lbHotelName.Text = Model.HotelName;//指定酒店名称
            this.lbStar.Text =this.GetHotelLevelByEnum(Model.HotelStar);//星级要求
            this.lbRoom.Text = Model.RoomAsk;//房型要求
            this.lbRoomCount.Text =Convert.ToString(Model.RoomCount);//房间数量
            this.lbPeopleCount.Text =Convert.ToString(Model.PeopleCount);//人数
            this.lbGroupPlan.Text = string.Format("{0}元-{1}元",(int)Model.BudgetMin,(int)Model.BudgetMax);//团房预算
            EyouSoft.Model.CompanyStructure.ContactPersonInfo personModel = Model.Contact;
            if (null != personModel)
            {
                StringBuilder strPerson = new StringBuilder();
                if (!string.IsNullOrEmpty(personModel.ContactName))
                {
                    strPerson.Append("姓名："+personModel.ContactName+"；");
                }
                if (!string.IsNullOrEmpty(personModel.Mobile))
                {
                    strPerson.Append("手机："+personModel.Mobile+"；");
                }
                if (!string.IsNullOrEmpty(personModel.Tel))
                {
                    strPerson.Append("电话："+personModel.Tel+"；");
                }
                if (!string.IsNullOrEmpty(personModel.Fax))
                {
                    strPerson.Append("传真："+personModel.Fax+"；");
                }
                if (!string.IsNullOrEmpty(personModel.Email))
                {
                    strPerson.Append("Email："+personModel.Email+"；");
                }
                this.lbContact.Text = strPerson.ToString();//订单联系人
            }
            this.lbPeopleType.Text =((int)Model.GuestType)==0?"内宾":"外宾";//宾客类型
            this.lbGroupType.Text = Model.TourType.ToString();//团队类型
            this.lbOther.Text = Model.OtherRemark;//其他要求
        }
        /// <summary>
        /// 根据酒店星级显示相关信息
        /// </summary>
        protected string GetHotelLevelByEnum(EyouSoft.HotelBI.HotelRankEnum leaveEnum)
        {
            int leave = (int)(leaveEnum);
            if (leave > 0 && leave < 6)
            {
                return leave + "星";
            }
            if (leave >= 6)
            {
                return "准" + (leave - 5).ToString() + "星";
            }
            return "无";
        }
    }
}