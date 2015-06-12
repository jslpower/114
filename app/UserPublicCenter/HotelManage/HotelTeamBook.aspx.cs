using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Control;
using EyouSoft.Model.HotelStructure;
namespace UserPublicCenter.HotelManage
{  
    /// <summary>
    /// 酒店团队预订
    /// xuty 2010/12/8
    /// </summary>
    public partial class HotelTeamBook : EyouSoft.Common.Control.FrontPage
    {
        protected string hotelName;//酒店名称
        protected string comeDate;//入住日期
        protected string leaveDate;//离店日期
        protected string hotelCode;//酒店代码
        protected int days;//入住天数
        protected DateTime cDate;
        protected DateTime lDate;
        protected string roomTypeList;//房型种类集合
        protected int cityId;
        protected void Page_Load(object sender, EventArgs e)
        {   
            this.CityAndMenu1.HeadMenuIndex = 4;
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(this.Request.Url.ToString(), "请登录后预定");
                return;
            }
            string method = Utils.GetFormValue("method");

            #region 根据条件获取酒店实体
            EyouSoft.HotelBI.SingleSeach searchModel = new EyouSoft.HotelBI.SingleSeach();
            comeDate = Utils.GetQueryStringValue("comeDate");//入住日期
            leaveDate = Utils.GetQueryStringValue("leaveDate");//离店日期
            HotelSearch1.CityId = Utils.GetInt(Utils.GetQueryStringValue("cityId"));
            CommonUser1.CityId = HotelSearch1.CityId;
            cityId = HotelSearch1.CityId;
            comeDate = comeDate == "" ? DateTime.Now.ToString("yyyy-MM-dd") : comeDate;//如果取不到则取当前日
            leaveDate = leaveDate == "" ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : leaveDate;//如果取不到则为明天
            cDate=Utils.GetDateTime(comeDate);
            lDate=Utils.GetDateTime(leaveDate);
            days =(lDate-cDate).Days;
            hotelCode = Utils.GetQueryStringValue("hotelCode");//酒店代号
            searchModel.HotelCode = hotelCode;
            searchModel.CheckInDate = comeDate;
            searchModel.CheckOutDate = leaveDate;
            searchModel.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;
            EyouSoft.HotelBI.ErrorInfo errorInfo = null;
            EyouSoft.Model.HotelStructure.HotelInfo hotelModel = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(searchModel,out errorInfo);

            #endregion

            string theMess = IsOk(errorInfo, hotelModel);

            #region 初始化酒店信息
            //如果不是下单则绑定房型类别
            if (method != "save")
            {
                HotelSearch1.ImageServerPath = ImageServerPath;
                if (theMess == "")
                {
                  
                    hotelName = hotelModel.HotelName;
                    this.Page.Title = hotelName+"_酒店团队预订_同业114酒店频道";
                }
                else
                {
                    Utils.ShowError(theMess, "");
                }
            }
            #endregion
            #region 预定团队
            else
            {
                if (theMess!= "")//如果酒店为空则输出失败
                {
                    Utils.ResponseMeg(false, theMess);
                    return;
                }
                //团队定制实体
                EyouSoft.Model.HotelStructure.HotelTourCustoms tourModel = new EyouSoft.Model.HotelStructure.HotelTourCustoms();
                tourModel.BudgetMax = Utils.GetDecimal(Utils.GetFormValue("htb_txtBudgetMax"));//预算最大值
                tourModel.BudgetMin = Utils.GetDecimal(Utils.GetFormValue("htb_txtBudgetMin"));//预算最小值
                tourModel.GuestType = Utils.GetFormValue("htb_selPType") == "1" ? EyouSoft.HotelBI.HBEGuestTypeIndicator.F : EyouSoft.HotelBI.HBEGuestTypeIndicator.D;//宾客类型
                tourModel.OtherRemark = Utils.GetFormValue("htb_txtRemark");//其他要求
                tourModel.PeopleCount = Utils.GetInt(Utils.GetFormValue("htb_txtPCount"));//人数
                tourModel.RoomCount = Utils.GetInt(Utils.GetFormValue("htb_txtRoomNum"));//房间数
                tourModel.TourType = (TourTypeList)Utils.GetInt(Utils.GetFormValue("htb_selTourType"), 1);//团队类型
                tourModel.RoomAsk =  Utils.GetFormValue("htb_selRoomType");//房间要求
                tourModel.CityCode = hotelModel.CityCode;
                tourModel.CompanyId = SiteUserInfo.CompanyID;
                tourModel.HotelCode = hotelModel.HotelCode;
                tourModel.HotelName = hotelModel.HotelName;
                tourModel.HotelStar = hotelModel.Rank;
                tourModel.LiveEndDate = lDate;
                tourModel.LiveStartDate = cDate;
                tourModel.Payment = EyouSoft.HotelBI.HBEPaymentType.T;
                //预订团队订单
                if (EyouSoft.BLL.HotelStructure.HotelTourCustoms.CreateInstance().Add(tourModel))
                {
                    Utils.ResponseMegSuccess();//输出成功
                }
                else
                {
                    Utils.ResponseMegError();//输出失败
                }
            }
            #endregion

        }
        #region 判断查询酒店时是否有错误
        /// <summary>
        /// 查询酒店时是否有错误信息
        /// </summary>
        /// <param name="errorInfo">错误信息实体</param>
        /// <param name="model">酒店实体</param>
        /// <returns></returns>
        protected string IsOk(EyouSoft.HotelBI.ErrorInfo errorInfo, EyouSoft.Model.HotelStructure.HotelInfo model)
        {

            string mess = "";
            if (errorInfo != null)
            {
                switch (errorInfo.ErrorType)
                {
                    case EyouSoft.HotelBI.ErrorType.业务级错误:
                        mess = string.Format("{0}至{1}房型已满", comeDate, leaveDate);
                        break;
                    case EyouSoft.HotelBI.ErrorType.未知错误:
                        mess = "查询数据超时";
                        break;
                    case EyouSoft.HotelBI.ErrorType.None:
                        mess = "";
                        break;
                    case EyouSoft.HotelBI.ErrorType.系统级错误:
                        mess = "查询数据时出错";
                        break;

                }
            }
            else
            {
                mess = "查询数据时出错";
            }
            if (model == null || string.IsNullOrEmpty(model.HotelCode))
            {
                mess = string.Format("{0}至{1}房型已满",comeDate,leaveDate);
            }
            return mess;

        }
        #endregion 
    }
}
