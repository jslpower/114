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
    /// 酒店详细内页
    /// </summary>
    public partial class HotelDetail2:System.Web.UI.Page
    {
        protected EyouSoft.Model.HotelStructure.HotelInfo hotelModel;//酒店实体
        protected string strRateHtml;
        protected string hotelCode;
        protected string comeDate;
        protected string leaveDate;
        protected string efPriceHTML;//额外收费
        protected string ImageServerPath;
        protected int cityId;
        protected void Page_Load(object sender, EventArgs e)
        {  
            ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

            #region 根据查询条件获取酒店信息实体
            hotelCode = Utils.GetQueryStringValue("hotelCode");
            comeDate = Utils.GetQueryStringValue("comeDate");
            leaveDate = Utils.GetQueryStringValue("leaveDate");
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
                Response.Clear();
                Response.Write(theMess);
                Response.End();
                return;
            }
            #endregion

            BindRate();//绑定房型

        }

        #region 绑定房型列表
        /// <summary>
        /// 绑定房型
        /// </summary>
        protected void BindRate()
        {   //酒店实体不为空
          
            bool IsLogin = Utils.GetQueryStringValue("IsLogin") == "True";
            string login = "";
            if (!IsLogin)
            {
                login = "<a href=\"" + EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Request.UrlReferrer.ToString(), "登录") + "\"><font class=\"C_red\" style=\"font-weight:normal\">登录查看</font></a>";
            }
            if (hotelModel != null)
            {
                IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> typeList = hotelModel.RoomTypeList;
                if (typeList != null && typeList.Count > 0)
                {
                    StringBuilder strBuilder = new StringBuilder();
                    EyouSoft.Model.HotelStructure.RoomTypeInfo typeInfo1 = typeList[0];
                    Dictionary<string, decimal> efs = new Dictionary<string, decimal>();
                    foreach(EyouSoft.Model.HotelStructure.RateInfo rInfo in typeInfo1.RoomRate.RateInfos)
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
                                    "<td rowspan=\"2\" align=\"center\" valign=\"middle\" bgcolor=\"#FFFFFF\"><a href=\"HotelBook.aspx?hotelCode={11}&comeDate={12}&leaveDate={13}&roomCode={14}&vendorCode={15}&ratePlanCode={16}&cityId={17}\"><img src=\"{9}/images/hotel/yuding.gif\" /></a></td></tr>" +
                                    "<tr><td height=\"25\" colspan=\"9\" align=\"left\" nowrap=\"nowrap\" bgcolor=\"#FFFFFF\"><font class=\"fbb\">房型描述：</font>{10}</td></tr>", typeInfo.RoomTypeName, rateInfo.DisplayPrice, Utils.GetMoney(rateInfo.AmountPrice), Utils.GetMoney(rateInfo.CommissionAmount), IsLogin ? (Utils.GetMoney(rateInfo.Percent * 100) + "%") : login, IsLogin ? ("￥" + decimal.Round(rateInfo.BalancePrice)) : login, rateInfo.FreeMeal == 0 ? "无" : rateInfo.FreeMeal.ToString(), typeInfo.BedType, typeInfo.RoomRate.Internet ? "有" : "无", ImageServerPath, Utils.GetText2(typeInfo.RoomDescription, 48, true), hotelCode, comeDate, leaveDate, typeInfo.RoomTypeCode, typeInfo.VendorCode, typeInfo.RatePlanCode,cityId);

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
    }
}
