using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HotelManage
{   
    /// <summary>
    /// 页面功能：订单完成
    /// xuty 2010/12/07
    /// </summary>
    public partial class HotelComplete : EyouSoft.Common.Control.FrontPage
    {
        protected EyouSoft.Model.HotelStructure.OrderInfo orderModel;

        protected string guestNames;//旅客姓名
        protected string guestTypes;//旅客类型
        protected string guestMible;//旅客手机
        protected string resOrderId;//订单号(酒店返回)
        protected string orderState;//
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CityAndMenu1.HeadMenuIndex = 4;
            if (!IsLogin)
            {
                Response.Redirect("~/Register/Login.aspx");
                return;
            }
            HotelSearch1.CityId = Utils.GetInt(Utils.GetQueryStringValue("CityID"));
            CommonUser1.CityId = HotelSearch1.CityId;

            resOrderId = Utils.GetQueryStringValue("resOrderId");
            EyouSoft.IBLL.HotelStructure.IHotelOrder orderBll = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            int errorCode = 0;// 错误代码
            string errorDesc = "";//错误描述
            HotelSearch1.ImageServerPath = ImageServerPath;
            if (method != "cancel")
            {
                orderModel = orderBll.GetInfo(resOrderId, out errorCode, out errorDesc);
                if (orderModel == null)
                {
                    orderModel = new EyouSoft.Model.HotelStructure.OrderInfo();
                    if (errorDesc == "")
                    {
                        errorDesc = "订单不存在";
                    }
                    Utils.ShowError(errorDesc, "hotel");
                    return;
                }
                orderState = orderModel.OrderState.ToString();
                IList<EyouSoft.HotelBI.HBEResGuestInfo> guestList = orderModel.ResGuests;
                if (guestList != null && guestList.Count > 0)
                {
                    foreach (EyouSoft.HotelBI.HBEResGuestInfo guest in guestList)
                    {
                        guestNames += guest.PersonName + ",";//旅客姓名
                        guestTypes += (guest.GuestTypeIndicator == EyouSoft.HotelBI.HBEGuestTypeIndicator.D?"内宾":"外宾") + ",";//旅客类型
                        guestMible = guest.Mobile;//旅客手机
                    }
                    guestNames = guestNames.TrimEnd(',');
                    guestTypes = guestTypes.TrimEnd(',');
                }
                this.Page.Title = orderModel.HotelName + "_酒店预订完成_同业114酒店频道";
            }
            else
            {
                if (orderBll.Cancel(resOrderId, "", out errorDesc) > 0)//取消订单
                {
                    Utils.ResponseMegSuccess();//输出成功
                }
                else
                {
                    Utils.ResponseMeg(false, errorDesc==""?"取消失败":errorDesc);//输出失败
                }
            }
            
        }
        /// <summary>
        /// 获取导航条图片的链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetMainImgLink()
        {
            string linkHref = Domain.UserBackCenter+"/hotelcenter/hotelordermanage/teamonlinesubmit.aspx";
            if (!IsLogin)
            {
                linkHref = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(linkHref, "登录");
            }
            return linkHref;
        }

       
       
    }
}
