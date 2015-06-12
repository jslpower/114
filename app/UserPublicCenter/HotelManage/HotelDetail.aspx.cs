using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Control;
namespace UserPublicCenter.HotelManage
{   
    /// <summary>
    /// 酒店详细
    /// xuty 2010/12/
    /// </summary>
    public partial class HotelDetail : EyouSoft.Common.Control.FrontPage
    {
        protected EyouSoft.Model.HotelStructure.HotelInfo hotelModel;//酒店实体
        protected string strRateHtml;
        protected string hotelCode;
        protected string comeDate;
        protected string leaveDate;
        protected string efPriceHTML;//额外收费
        protected int cityId;
        protected void Page_Load(object sender, EventArgs e)
        {
           this.CityAndMenu1.HeadMenuIndex = 4;
           hotelCode=Utils.GetQueryStringValue("hotelCode");
           comeDate=Utils.GetQueryStringValue("comeDate");
           leaveDate=Utils.GetQueryStringValue("leaveDate");
           HotelSearch1.ImageServerPath = ImageServerPath;
        
           HotelSearch1.CityId = Utils.GetInt(Utils.GetQueryStringValue("CityID"));
           CommonUser1.CityId = HotelSearch1.CityId;
           SpecialHotel1.CityId = HotelSearch1.CityId;
           HotHotel1.CityId = HotelSearch1.CityId;
           GetHotelModel();
        }

        /// <summary>
        /// 获取导航条图片的链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetMainImgLink()
        {
            string linkHref = Domain.UserBackCenter + "/hotelcenter/hotelordermanage/teamonlinesubmit.aspx";
            if (!IsLogin)
            {
                linkHref = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(linkHref, "登录");
            }
            return linkHref;
        }

        #region 绑定房型列表
        /// <summary>
        /// 绑定房型
        /// </summary>
        protected void BindRate()
        {   //酒店实体不为空

         
            string login = "";
            if (!IsLogin)
            {
                login = "<a href=\"" + EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Request.Url.ToString(), "登录") + "\"><font class=\"C_red\" style=\"font-weight:normal\">登录查看</font></a>";
            }
            if (hotelModel != null)
            {
                IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> typeList = hotelModel.RoomTypeList;
                if (typeList != null && typeList.Count > 0)
                {
                    StringBuilder strBuilder = new StringBuilder();
                    EyouSoft.Model.HotelStructure.RoomTypeInfo typeInfo1 = typeList[0];
                    Dictionary<string, decimal> efs = new Dictionary<string, decimal>();
                    foreach (EyouSoft.Model.HotelStructure.RateInfo rInfo in typeInfo1.RoomRate.RateInfos)
                    {
                        if (rInfo.ExcessiveFees != null)
                        {
                            foreach (EyouSoft.Model.HotelStructure.ExcessiveFee ef in rInfo.ExcessiveFees)
                            {
                                if (efs.ContainsKey(ef.FeeName))
                                {
                                    efs[ef.FeeName] += ef.Amount;
                                }
                                else
                                {
                                    efs.Add(ef.FeeName, ef.Amount);
                                }
                            }
                        }
                    }
                    StringBuilder efBuilder = new StringBuilder();
                    foreach (KeyValuePair<string, decimal> ef in efs)
                    {
                        efBuilder.AppendFormat("{0}：{1}({2}至{3})<br/>", ef.Key, ef.Value, comeDate, leaveDate);
                    }
                    efPriceHTML = efBuilder.ToString();
                    foreach (EyouSoft.Model.HotelStructure.RoomTypeInfo typeInfo in typeList)
                    {
                        if (typeInfo.RoomRate != null && typeInfo.RoomRate.RateInfos != null && typeInfo.RoomRate.RateInfos.Count > 0)
                        {
                            EyouSoft.Model.HotelStructure.RateInfo rateInfo = typeInfo.RoomRate.RateInfos[0];
                            strBuilder.AppendFormat("<tr><td height=\"25\" align=\"center\" nowrap=\"nowrap\" bgcolor=\"#FFFFFF\"><font class=\"C_Grb\">{0}</font></td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">￥<del>{1}</del></td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">￥{2}</td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">￥{3}</td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\"><font class=\"frb\">{4}</font></td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\"><font class=\"C_Grb\"><strong>{5}</strong></font></td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">{6}</td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">{7}</td>" +
                                    "<td align=\"center\" bgcolor=\"#FFFFFF\">{8}</td>" +
                                    "<td rowspan=\"2\" align=\"center\" valign=\"middle\" bgcolor=\"#FFFFFF\"><a href=\"/HotelManage/HotelBook.aspx?hotelCode={11}&comeDate={12}&leaveDate={13}&roomCode={14}&vendorCode={15}&ratePlanCode={16}&cityId={17}\"><img src=\"{9}/images/hotel/yuding.gif\" /></a></td></tr>" +
                                    "<tr><td height=\"25\" colspan=\"9\" align=\"left\" nowrap=\"nowrap\" bgcolor=\"#FFFFFF\"><font class=\"fbb\">房型描述：</font>{10}</td></tr>", typeInfo.RoomTypeName, rateInfo.DisplayPrice, Utils.GetMoney(rateInfo.AmountPrice), Utils.GetMoney(rateInfo.CommissionAmount), IsLogin ? (Utils.GetMoney(rateInfo.Percent * 100) + "%") : login, IsLogin ? ("￥" + decimal.Round(rateInfo.BalancePrice)) : login, rateInfo.FreeMeal == 0 ? "无" : rateInfo.FreeMeal.ToString(), typeInfo.BedType, typeInfo.RoomRate.Internet ? "有" : "无", ImageServerPath, Utils.GetText2(typeInfo.RoomDescription, 48, true), hotelCode, comeDate, leaveDate, typeInfo.RoomTypeCode, typeInfo.VendorCode, typeInfo.RatePlanCode, cityId);

                        }
                    }
                    strRateHtml = strBuilder.ToString();
                }
            }
            string method = Utils.GetQueryStringValue("method");
            if (method == "changeDate")
            {
                Response.Clear();
                Response.Write(strRateHtml);
                Response.End();
                return;
            }
        }
        #endregion

        #region 判断查询酒店时是否有错误信息
        protected string IsOk(EyouSoft.HotelBI.ErrorInfo errorInfo, EyouSoft.Model.HotelStructure.HotelInfo model)
        {

            string mess = "";
            if (errorInfo != null)
            {
                switch (errorInfo.ErrorType)
                {
                    case EyouSoft.HotelBI.ErrorType.业务级错误:
                        mess = "酒店或房型不存在";
                        break;
                    case EyouSoft.HotelBI.ErrorType.未知错误:
                        mess = "查询数据超时,请稍后再试";
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
            if (mess == "")
            {
                if (model == null || string.IsNullOrEmpty(model.HotelCode))
                {
                    mess = "酒店或房型不存在";
                }
            }
            return mess;

        }
        #endregion

        protected void GetHotelModel()
        {
            #region 根据查询条件获取酒店信息实体
          
            cityId = Utils.GetInt(Utils.GetQueryStringValue("cityId"));
            comeDate = comeDate == "" ? DateTime.Now.ToString("yyyy-MM-dd") : comeDate;
            leaveDate = leaveDate == "" ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : leaveDate;
            EyouSoft.HotelBI.SingleSeach search = new EyouSoft.HotelBI.SingleSeach();
            search.HotelCode = hotelCode;
            search.CheckInDate = comeDate;
            search.CheckOutDate = leaveDate;
            search.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;
            EyouSoft.HotelBI.ErrorInfo errorInfo;
            hotelModel = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(search, out errorInfo);
            #endregion

            #region 判断查询酒店时是否有错误
            string theMess = IsOk(errorInfo, hotelModel);
            if (theMess != "")
            {
                hotelModel = new EyouSoft.Model.HotelStructure.HotelInfo();
                hotelModel.HotelPosition = new EyouSoft.Model.HotelStructure.HotelPositionInfo();
                Utils.ShowError(theMess, "hotel");
                return;
            }
            #endregion
            this.Title = hotelModel.HotelName + "_酒店预订_酒店信息_同业114酒店频道";
            this.AddMetaTag("description", "同业114酒店频道,提供"+hotelModel.HotelName+"的房型图片,环境介绍,返佣结算报价等明细信息,是旅游同业全面了解该酒店信息的资源网站");
            BindRate();//绑定房型
        }

      

        
    }
}
