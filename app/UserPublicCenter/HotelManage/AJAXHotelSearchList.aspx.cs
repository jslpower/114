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
    /// --------------------------------
    /// 2011-1-20 恢复从多酒店信息绑定每个酒店房型
    /// </summary>
    public partial class AJAXHotelSearchList : BasePage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;

        protected string returnUrl = "";
        protected int CityId = 0;

        protected string inTime = "";
        protected string leaveTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 获得参数
                //城市三字码
                string cityCode = Utils.GetQueryStringValue("cityCode");
                //入住日期
                inTime = Utils.GetQueryStringValue("inTime");
                //离开日期
                leaveTime = Utils.GetQueryStringValue("leaveTime");
                //开始价格
                decimal? priceBegin = Utils.GetDecimal(Utils.GetQueryStringValue("priceBegin"));
                //结束价格
                decimal? priceEnd = Utils.GetDecimal(Utils.GetQueryStringValue("priceEnd"));
                //地理位置
                string landMark = Utils.GetQueryStringValue("landMark");
                string LandMarkTxt = Utils.GetQueryStringValue("LandMarkTxt");
                //酒店星级
                string hotelLevel = Utils.GetQueryStringValue("hotelLevel");
                if (hotelLevel == null || hotelLevel == "") { hotelLevel = "0"; }
                //即时确认
                string instant = Utils.GetQueryStringValue("instant");
                //接机服务
                string service = Utils.GetQueryStringValue("service");
                //上网设施
                string internet = Utils.GetQueryStringValue("internet");
                //装修时间
                string decoration = Utils.GetQueryStringValue("decoration");
                //特殊房型
                string special = Utils.GetQueryStringValue("special");
                //床型
                string bed = Utils.GetQueryStringValue("bed");
                //行政区域
                string district = Utils.GetQueryStringValue("district");
                string districtTxt = Utils.GetQueryStringValue("districtTxt");
                //查询方式
                string searchWay = Utils.GetQueryStringValue("searchWay");
                //酒店名称
                string hotelName = Utils.GetQueryStringValue("hotelName");
                //排序
                string sort = Utils.GetQueryStringValue("sort");
                //设置城市
                CityId = Utils.GetInt(Utils.GetQueryStringValue("CityId"));
                //点击登录时返回的URL
                returnUrl = Utils.GetQueryStringValue("returnUrl");
                #endregion

                #region 为查询MODEL赋值
                EyouSoft.HotelBI.MultipleSeach searchModel = new EyouSoft.HotelBI.MultipleSeach();
                // searchModel.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;
                searchModel.RoomTypeDetailShowed = true;
                searchModel.CityCode = cityCode;
                searchModel.CheckInDate = inTime;
                searchModel.CheckOutDate = leaveTime;
                if (priceBegin > 0) { searchModel.PriceMinRate = priceBegin; }
                if (priceEnd > 0) { searchModel.PriceMaxRate = priceEnd; }
                if (landMark != "") { searchModel.LandMark = Server.UrlDecode(LandMarkTxt); }
                searchModel.HotelRank = (EyouSoft.HotelBI.HotelRankEnum)Enum.Parse(typeof(EyouSoft.HotelBI.HotelRankEnum), hotelLevel);
                searchModel.District = Server.UrlDecode(districtTxt);
                if (hotelName != "")
                {
                    searchModel.HotelChineseName = hotelName;
                }

                if (instant == "1") { searchModel.ConfirmRightNowIndicator = true; } else { searchModel.ConfirmRightNowIndicator = false; }

                if (service == "1") { searchModel.AirReception = EyouSoft.HotelBI.BoolEnum.Y; } else { searchModel.AirReception = EyouSoft.HotelBI.BoolEnum.none; }

                if (internet == "1") { searchModel.Internet = EyouSoft.HotelBI.BoolEnum.Y; } else { searchModel.Internet = EyouSoft.HotelBI.BoolEnum.none; }

                searchModel.Fitment = decoration;

                searchModel.RoomName = special;

                searchModel.BedType = bed;

                #region 设置页面排序
                EyouSoft.HotelBI.HotelOrderBy hotelSort;
                switch (sort)
                {
                    case "": hotelSort = EyouSoft.HotelBI.HotelOrderBy.Default; break;
                    case "1": hotelSort = EyouSoft.HotelBI.HotelOrderBy.PRICEHTL; break;
                    case "2": hotelSort = EyouSoft.HotelBI.HotelOrderBy.PRICELTH; break;
                    case "3": hotelSort = EyouSoft.HotelBI.HotelOrderBy.STARHTL; break;
                    default: hotelSort = EyouSoft.HotelBI.HotelOrderBy.STARLTH; break;
                }

                searchModel.OrderBy = hotelSort;
                #endregion

                //设置分页
                searchModel.IsPageView = true;
                searchModel.NumOfEachPage = 10;
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                searchModel.PageNo = pageIndex;
                #endregion

                //酒店初始化列表
                ListDataInit(searchModel);
            }
        }


        #region 初始化酒店列表
        /// <summary>
        /// 列表数据初始化
        /// </summary>
        /// <param name="searchModel"></param>
        private void ListDataInit(EyouSoft.HotelBI.MultipleSeach searchModel)
        {
            EyouSoft.Model.HotelStructure.RespPageInfo pageInfoModel = null;
            EyouSoft.HotelBI.ErrorInfo errorModel = new EyouSoft.HotelBI.ErrorInfo();
            IList<EyouSoft.Model.HotelStructure.HotelInfo> list = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelList(searchModel, ref pageInfoModel, out errorModel);

            //如果接口查询异常 则提示 查询超时
            if (errorModel == null || errorModel.ErrorType == EyouSoft.HotelBI.ErrorType.未知错误 || errorModel.ErrorType == EyouSoft.HotelBI.ErrorType.系统级错误)
            {
                //提示查询超时
                this.lblMsg.Text = "查询超时，请重新查询或稍后再试.";
                //不显示分页控件
                this.ExportPageInfo.Visible = false;
                return;
            }

            if (list != null)
            {
                if (list.Count > 0)
                {
                    this.rpt_list.DataSource = list;
                    this.rpt_list.DataBind();
                    if (pageInfoModel != null)
                    {
                        recordCount = pageInfoModel.TotalNum;
                    }
                    BindPage();
                }
                else
                {
                    //提示没有查询到酒店
                    this.lblMsg.Text = "未找到相关酒店,建议您修改相关查询条件后再查询.";
                    this.ExportPageInfo.Visible = false;
                }

            }
            else
            {
                //提示没有查询到酒店
                this.lblMsg.Text = "未找到相关酒店,建议您修改相关查询条件后再查询.";
                this.ExportPageInfo.Visible = false;
            }

            list = null;
        }
        #endregion


        #region 根据酒店生成房间HTML
        /// <summary>
        /// 获取绑定酒店的房间列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        protected string GetRoomByHotel(IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> list, int num, string hotelCode)
        {
            StringBuilder strHead = new StringBuilder();
            StringBuilder strHidden = new StringBuilder();

            EyouSoft.HotelBI.SingleSeach search = new EyouSoft.HotelBI.SingleSeach();
            EyouSoft.HotelBI.ErrorInfo errorInfo;

            //search.HotelCode = hotelCode;
            //search.CheckInDate = inTime;
            //search.CheckOutDate = leaveTime;
            //search.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;

            //EyouSoft.Model.HotelStructure.HotelInfo hotelModel = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(search, out errorInfo);

            //获得单酒店的房间信息
            IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> roomList = list;

            //if (hotelModel != null)
            //{
            //    IList<EyouSoft.Model.HotelStructure.RoomTypeInfo> rList = hotelModel.RoomTypeList;
            //    //如果单酒店房间信息有数据时 优先显示
            //    if (rList != null && rList.Count > 0)
            //    {
            //        roomList.Clear();
            //        roomList = rList;
            //    }
            //}

            if (roomList != null && roomList.Count > 0)
            {
                //设置标题
                strHead.Append("<table width=\"550px\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><th width=\"70px\" height=\"20px\" align=\"center\" bgcolor=\"#FFFFFF\"> 房型</th><th width=\"55px\" align=\"center\" bgcolor=\"#FFFFFF\">门市价</th> <th width=\"55px\" align=\"center\" bgcolor=\"#FFFFFF\">销售价</th><th width=\"55px\" align=\"center\" bgcolor=\"#FFFFFF\"> 返佣价</th><th width=\"60px\" align=\"center\" nowrap=\"nowrap\" bgcolor=\"#FFFFFF\"> 返佣比例 </th> <th width=\"60px\" align=\"center\" bgcolor=\"#FFFFFF\">结算价</th><th width=\"45px\" align=\"center\" bgcolor=\"#FFFFFF\">早餐</th><th width=\"45px\" align=\"center\" bgcolor=\"#FFFFFF\"> 床型</th><th width=\"45px\" align=\"center\" bgcolor=\"#FFFFFF\">宽带 </th><th width=\"60px\" align=\"center\" bgcolor=\"#FFFFFF\"> 预定</th></tr>");

                strHidden.Append("<div id=\"div_list_" + num + "\" style=\"display: none;\"><table width=\"550px\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" id=\"con_two_" + num + "\" >");

                string displayPrice = ""; //门市价
                string amountPrice = "";  //销售价
                string commissionAmount = ""; //返佣价
                string percent = ""; //返佣 百分比  登录才显示
                string balancePrice = "";//结算价   登录才显示
                string freeMeal = ""; //早餐数量
                string Internet = "";//是否有网

                int count = 0;
                for (int i = 0; i < roomList.Count; i++)
                {
                    if (roomList[i].RoomRate.RateInfos.Count > 0)
                    {
                        displayPrice = roomList[i].RoomRate.RateInfos[0].DisplayPrice.ToString("0");
                        amountPrice = roomList[i].RoomRate.RateInfos[0].AmountPrice.ToString("0");
                        commissionAmount = roomList[i].RoomRate.RateInfos[0].CommissionAmount.ToString("0");
                        freeMeal = EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(roomList[i].RoomRate.RateInfos[0].FreeMeal);
                        if (IsLogin)
                        {
                            percent = EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(roomList[i].RoomRate.RateInfos[0].Percent * 100) + "%";
                            balancePrice = roomList[i].RoomRate.RateInfos[0].BalancePrice.ToString("0");
                        }
                        else
                        {
                            percent = "<a href=\"" + EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(returnUrl, "登录") + "\"><font class=\"C_red\">登录查看</font></a>";
                            balancePrice = "<a href=\"" + EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(returnUrl, "登录") + "\"><font class=\"C_red\">登录查看</font></a>";

                        }

                        if (freeMeal == "0")
                        {
                            freeMeal = "无早";
                        }
                        else
                        {
                            freeMeal = "有早";
                        }


                        if (roomList[i].RoomRate.Internet)
                        {
                            Internet = "有";
                        }
                        else
                        {
                            Internet = "无";
                        }

                        if (count < 2)
                        {
                            strHead.Append("<tr class=\"xuxian\"> <td width=\"70px\" align=\"center\" nowrap=\"nowrap\" style=\"white-space:normal; word-break:break-all;\">" + roomList[i].RoomTypeName + "</td><td width=\"55px\" align=\"center\">￥<del>" + displayPrice + "</del></td><td  width=\"55px\"  align=\"center\">￥" + amountPrice + "</td><td  width=\"55px\"  align=\"center\"> ￥" + commissionAmount + "</td><td  width=\"60px\"  align=\"center\">" + percent + "</td> <td  width=\"60px\"  align=\"center\"> " + balancePrice + "</td> <td  width=\"45px\"  align=\"center\">" + freeMeal + "</td><td  width=\"45px\"  align=\"center\">" + roomList[i].BedType + "</td><td  width=\"45px\"  align=\"center\">" + Internet + "</td> <td  width=\"60px\"  align=\"center\" valign=\"middle\"> <a href=\"javascript:void(0);\" onclick=\"OpenYuDing('" + hotelCode + "','" + roomList[i].RoomTypeCode + "','" + roomList[i].VendorCode + "','" + roomList[i].RatePlanCode + "')\" ><img src=\"" + ImageServerUrl + "/images/hotel/yuding.gif\" /></a></td></tr>");
                            count++;
                        }
                        else
                        {
                            strHidden.Append("<tr class=\"xuxian\" > <td  width=\"70px\" align=\"center\" nowrap=\"nowrap\" style=\"white-space:normal; word-break:break-all;\">" + roomList[i].RoomTypeName + "</td><td align=\"center\" width=\"55px\" >￥<del>" + displayPrice + "</del></td><td align=\"center\" width=\"55px\" >￥" + amountPrice + "</td><td align=\"center\" width=\"55px\" > ￥" + commissionAmount + "</td><td align=\"center\" width=\"60px\" >" + percent + "</td> <td align=\"center\" width=\"60px\" > " + balancePrice + "</td> <td align=\"center\" width=\"45px\" >" + freeMeal + "</td><td align=\"center\" width=\"45px\" >" + roomList[i].BedType + "</td><td align=\"center\" width=\"45px\" >" + Internet + "</td> <td align=\"center\" valign=\"middle\" width=\"60px\" > <a href=\"javascript:void(0);\" onclick=\"OpenYuDing('" + hotelCode + "','" + roomList[i].RoomTypeCode + "','" + roomList[i].VendorCode + "','" + roomList[i].RatePlanCode + "')\" ><img src=\"" + ImageServerUrl + "/images/hotel/yuding.gif\" /></a></td></tr>");
                        }
                    }
                }

                strHead.Append("</table>");

                if (roomList.Count >= 3)
                {
                    strHidden.Append("</table><div style=\"clear:both;\"></div></div>");
                    strHidden.Append("<table width=\"550px\"><tr><td colspan=\"9\" align=\"center\" nowrap=\"nowrap\" bgcolor=\"#f1f1ef\">&nbsp;</td><td height=\"25\" align=\"right\" nowrap=\"nowrap\" bgcolor=\"#f1f1ef\"> <a id=\"a_" + num + "\" href=\"javascript:void(0);\" onclick=\"ShowHideRoom('" + num + "')\" >全部房型</a><img id=\"img_connn_" + num + "\" src=\"" + ImageServerUrl + "/images/hotel/iconnn.gif\" /></td></tr></table>");
                }
                else
                {
                    strHidden.Append("</table><div style=\"clear:both;\"></div></div>");
                }

                strHead.Append("</table>");
            }
            else
            {
                return "没有找到符合条件的房间!";
            }
            return strHead.ToString() + strHidden.ToString();
        }
        #endregion

        #region 获得酒店的第一张图片并显示,没有则提示
        /// <summary>
        /// 获得酒店的图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string GetImageByList(IList<EyouSoft.Model.HotelStructure.HotelImagesInfo> list)
        {
            string html = "尚未添加图片";

            if (list != null && list.Count > 0)
            {
                html = string.Format("<img src='{0}' border='0' />", EyouSoft.HotelBI.Utils.ImagesUrl + list[0].ImageURL);
            }

            return html;
        }
        #endregion

        #region 显示酒店星级
        /// <summary>
        /// 根据酒店星级显示相关信息
        /// </summary>
        /// <param name="leaveEnum"></param>
        /// <returns></returns>
        protected string GetHotelLevelByEnum(EyouSoft.HotelBI.HotelRankEnum leaveEnum)
        {
            int leave = (int)(leaveEnum);
            if (leave > 0 && leave < 6)
            {
                return "<font size=\"+2\">" + leave + "</font>星";
            }
            if (leave >= 6)
            {
                return "准<font size=\"+2\">" + (leave - 5).ToString() + "</font>星";
            }
            return "无";
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo.UrlParams = Request.QueryString;
            this.ExportPageInfo.intPageSize = pageSize;
            this.ExportPageInfo.CurrencyPage = pageIndex;
            this.ExportPageInfo.intRecordCount = recordCount;
            this.ExportPageInfo.PageLinkURL = "#/HotelManage/HotelSearchList.aspx?";
        }
        #endregion
    }
}
