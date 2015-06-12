using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-单团预定
    /// </summary>
    /// 创建人:柴逸宁
    public partial class SingleGroupPre : BackPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 是否为国际线路,是否组团
        /// </summary>
        protected bool isGJ = false, isZT = false;
        /// <summary>
        /// 线路Id,团号,线路发布商公司,团队参考价,MQ,QQ,提前预定天数
        /// </summary>
        protected string
            routeId = string.Empty,
            tourId = string.Empty,
            companyID = string.Empty,
            IndependentGroupPrice,
            MQ = string.Empty,
            QQ = string.Empty,
            advanceDayRegistration = string.Empty,
            title = "专线商";

        /// <summary>
        /// 区分专线、地接、组团
        /// </summary>
        protected int IntRouteSource = 0;
        /// <summary>
        /// 团队订单状态
        /// </summary>
        protected TourOrderStatus orderStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("Operating").Length > 0)
            {
                Save();
            }
            Key = "SingleGroupPre" + Guid.NewGuid().ToString();
            if (!IsPostBack)
            {
                InitPage();
            }

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitPage()
        {
            routeId = Utils.GetQueryStringValue("routeId");
            tourId = Utils.GetQueryStringValue("tourId");
            IntRouteSource = Utils.GetInt(Utils.GetQueryStringValue("intRouteSource"), 0);
            if (IntRouteSource >= 0)
            {
                if (IntRouteSource == 2)
                {
                    title = "地接社";
                }
                else
                    title = "专线社";
            }
            else
                title = "组团社";
            
            //判断是否为组团
            isZT = Utils.GetQueryStringValue("isZT").Length > 0;
            if (routeId.Length > 0)
            {
                if (!SiteUserInfo.CompanyRole.HasRole(CompanyType.组团))
                {
                    Response.Clear();
                    Response.Write("<script>javascript: window.location.href='/default.aspx';</script>");
                    Response.End();
                    return;
                }
                #region 单团预定初始化
                MRoute model = BRoute.CreateInstance().GetModel(routeId);
                if (model != null)
                {
                    advanceDayRegistration = model.AdvanceDayRegistration.ToString();
                    companyID = model.Publishers;
                    //lbl_MQ.Text = SiteUserInfo.ContactInfo.MQ;
                    //lbl_QQ.Text = SiteUserInfo.ContactInfo.QQ;
                    //线路名称
                    lbl_routeName.Text = model.RouteName;
                    //发布人名称
                    lbl_operatorName.Text = model.OperatorName;
                    CompanyDetailInfo cdModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(model.Publishers);
                    MQ = cdModel.ContactInfo.MQ;
                    QQ = cdModel.ContactInfo.QQ;
                    a_gowd.HRef = Utils.GetShopUrl(model.Publishers);
                    lbl_operatorName.Text = cdModel.CompanyName;
                    lbl_sTraffic.Text = model.StartTraffic + "  " + model.StartCityName;
                    lbl_eTraffic.Text += model.EndTraffic + "  " + model.EndCityName;

                    isGJ = model.RouteType == AreaType.国际线;
                    //团队参考价
                    lbl_groupNum.Text = (model.IndependentGroupPrice == 0 ?
                        "一团一议" : Utils.FilterEndOfTheZeroDecimal(model.IndependentGroupPrice) + " 元") + ",  最小成团人数 " + model.GroupNum + " 人";
                    //定金
                    if (isGJ)
                    {
                        string strPrice = "成人：";
                        //成人定金
                        if (model.AdultPrice <= 0)
                            strPrice += (model.AdultPrice == -1 ? "电询" : "无需定金");
                        else
                            strPrice += (model.AdultPrice.ToString("F0") + "元");

                        strPrice += "  儿童：";
                        //儿童定金
                        if (model.ChildrenPrice <= 0)
                            strPrice += (model.ChildrenPrice == -1 ? "电询" : "无需定金");
                        else
                            strPrice += (model.ChildrenPrice.ToString("F0") + "元");

                        lbl_Price.Text = strPrice;
                    }
                    //组团社联系人姓名
                    txt_travelContact.Value = Request.QueryString["contact"] ?? SiteUserInfo.ContactInfo.ContactName;
                    //组团社联系人电话
                    txt_travelTel.Value = Request.QueryString["tel"] ?? SiteUserInfo.ContactInfo.Tel;
                    //成人数未传值默认为1人
                    string adult = Request.QueryString["adult"] ?? "1";
                    //儿童数未传值默认为0人
                    string child = Request.QueryString["child"] ?? "0";
                    //预定人数=成人数+儿童数
                    txt_adultPrice.Value = (Utils.GetInt(adult) + Utils.GetInt(child)).ToString();
                    //参考金额＝预定人数*参考金额
                    //参考总金额
                    lbl_tourPrice.Text = model.IndependentGroupPrice == 0 ?
                        "一团一议" :
                        Utils.FilterEndOfTheZeroDecimal((Utils.GetInt(adult) + Utils.GetInt(child)) * model.IndependentGroupPrice) + " 元";
                    IndependentGroupPrice = model.IndependentGroupPrice == 0 ?
                        "一团一议" :
                        model.IndependentGroupPrice.ToString();

                }
                #endregion
            }
            else if (tourId.Length > 0)
            {
                #region 团队订单管理查看初始化
                MTourList model = BTourList.CreateInstance().GetModel(tourId);
                if (model != null)
                {
                    orderStatus = model.OrderStatus;
                    //出团时间
                    lbl_leaveDate.Text = model.StartDate.ToString("yyyy-MM-dd");
                    //线路名称(定团旅行社)
                    lbl_routeName.Text = model.RouteName;
                    lbl_sTraffic.Text = model.StartTraffic + "  " + model.StartCityName;
                    lbl_eTraffic.Text += model.EndTraffic + "  " + model.EndCityName;
                    if (isZT)
                    {
                        MQ = model.BusinessMQ;
                        QQ = model.BusinessQQ;
                        lbl_operatorName.Text = model.BusinessName;
                        a_gowd.HRef = Utils.GetShopUrl(model.Business);
                        lbl_OrderNo.Text = model.OrderNo;
                        //组团社联系人
                        txt_travelContact.Value = model.TravelContact;
                        //组团社联系人电话
                        txt_travelTel.Value = model.TravelTel;
                        //游客联系人
                        txt_visitorContact.Value = model.VisitorContact;
                        //游客联系人电话
                        txt_visitorTel.Value = model.VisitorTel;
                    }
                    else
                    {
                        MQ = model.TravelMQ;
                        QQ = model.TravelQQ;
                        lbl_operatorName.Text = model.TravelName;
                        a_gowd.HRef = Utils.GetShopUrl(model.Travel);
                        //组团社联系人
                        lbl_travel.Text = model.TravelContact + " 联系电话:" + model.TravelTel;
                        //游客联系人
                        lbl_visitor.Text = model.VisitorContact + " 联系电话:" + model.VisitorTel;
                    }


                    if (model.RouteId.Length > 0)
                    {
                        routeId = model.RouteId;
                        MRoute routeModel = BRoute.CreateInstance().GetModel(model.RouteId);
                        if (routeModel != null)
                        {
                            isGJ = routeModel.RouteType == AreaType.国际线;
                            #region 团队参考价
                            //团队参考价
                            lbl_groupNum.Text = "最小成团人数: " + routeModel.GroupNum + " 人," +
                                (routeModel.IndependentGroupPrice == 0 ?
                                "一团一议"
                                :
                                "参考价: " + Utils.FilterEndOfTheZeroDecimal(routeModel.IndependentGroupPrice) + " 元");
                            if (isZT)
                            {
                                lbl_groupNum.Text += "</br>同业销售须知:</br>";
                                lbl_groupNum.Text += model.VendorsNotes;
                                lbl_businessNotes.Text = model.BusinessNotes;
                                txt_travelNotes.Value = model.TravelNotes;
                            }
                            else
                            {
                                lbl_travelNotes.Text = model.TravelNotes;
                                txt_businessNotes.Value = model.BusinessNotes;
                            }
                            #endregion
                            #region 定金
                            //定金
                            if (isGJ)
                            {
                                lbl_Price.Text = "成人定金：" + (model.AdultPrice < 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(model.AdultPrice)) + "  儿童定金：" + (model.ChildrenPrice < 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(model.ChildrenPrice));
                            }
                            #endregion
                            #region 参考总金额
                            //参考总金额
                            lbl_tourPrice.Text = routeModel.IndependentGroupPrice == 0 ?
                                "一团一议" :
                                Utils.FilterEndOfTheZeroDecimal(model.ScheduleNum * routeModel.IndependentGroupPrice) + " 元";
                            IndependentGroupPrice = routeModel.IndependentGroupPrice == 0 ?
                                "一团一议" :
                                Utils.FilterEndOfTheZeroDecimal(routeModel.IndependentGroupPrice) + " 元";
                            #endregion
                        }

                    }
                    //预定人数
                    txt_adultPrice.Value = model.ScheduleNum.ToString();
                    //下单时间
                    lbl_issueTime.Text = model.IssueTime.ToString("yyyy-MM-dd HH:mm") + " " + model.OperatorName;
                }
                #endregion
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {

            routeId = Utils.GetQueryStringValue("routeId");
            tourId = Utils.GetQueryStringValue("tourId");
            if (tourId.Length <= 0 && routeId.Length > 0)
            {
                #region 组团社-单团预定
                MTourList model = new MTourList();
                MRoute brModel = BRoute.CreateInstance().GetModel(routeId);
                //线路Id
                model.RouteId = routeId;
                //线路名称
                model.RouteName = brModel.RouteName;
                //下单人编号
                model.OperatorId = SiteUserInfo.ID;
                //下单人名称
                model.OperatorName = SiteUserInfo.UserName;
                //下单时间
                model.IssueTime = DateTime.Now;
                //预定人数
                model.ScheduleNum = Utils.GetInt(Utils.GetFormValue(txt_adultPrice.UniqueID));
                //组团社备注
                model.TravelNotes = Utils.GetFormValue(txt_travelNotes.UniqueID);
                //组团社联系人
                model.TravelContact = Utils.GetFormValue(txt_travelContact.UniqueID);
                //组团社联系人电话
                model.TravelTel = Utils.GetFormValue(txt_travelTel.UniqueID);
                //游客联系人
                model.VisitorContact = Utils.GetFormValue(txt_visitorContact.UniqueID);
                //游客联系人电话
                model.VisitorTel = Utils.GetFormValue(txt_visitorTel.UniqueID);
                //出发日期
                model.StartDate = Utils.GetDateTime(Utils.GetFormValue(txt_leaveDate.UniqueID));
                //预订公司编号
                model.Travel = SiteUserInfo.CompanyID;
                //发布线路专线商或地接社公司编号
                model.Business = Utils.GetQueryStringValue("companyID");
                bool isSave = BTourList.CreateInstance().AddTourList(model);

                #region 发送短信模块
                if (isSave)
                {
                    EyouSoft.Model.SystemStructure.MSysSettingInfo SettingInfoModel = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSysSetting();
                    //获得组团公司实体
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo travelComModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                    ////获得专线或地接公司实体
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo tourComModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(brModel.Publishers);
                    if (SettingInfoModel != null && travelComModel != null)
                    {
                        if (SettingInfoModel.OrderSmsCompanyTypes.Contains(travelComModel.CompanyLev))
                        {
                            string sendMsg = SettingInfoModel.OrderSmsTemplate;
                            sendMsg = sendMsg.Replace("[预订公司]", SiteUserInfo.CompanyName);
                            sendMsg = sendMsg.Replace("[预订联系电话]", SiteUserInfo.ContactInfo.Mobile);
                            sendMsg = sendMsg.Replace("[预订出发时间]", model.StartDate.ToString("yyyy-MM-dd"));
                            sendMsg = sendMsg.Replace("[预订产品]", model.RouteName);
                            sendMsg = sendMsg.Replace("[预订数量]", model.ScheduleNum.ToString() + "人");

                            #region 发送操作
                            EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo = new EyouSoft.Model.SMSStructure.SendMessageInfo();
                            sendMessageInfo.CompanyId = SiteUserInfo.CompanyID;
                            sendMessageInfo.CompanyName = SiteUserInfo.CompanyName;
                            sendMessageInfo.UserId = this.SiteUserInfo.ID;
                            sendMessageInfo.UserFullName = SiteUserInfo.ContactInfo.ContactName;
                            sendMessageInfo.SMSContent = sendMsg;
                            sendMessageInfo.SendTime = DateTime.Now;
                            //添加要发送的手机号码
                            List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> moblieList = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();
                            EyouSoft.Model.SMSStructure.AcceptMobileInfo mobileModel = new EyouSoft.Model.SMSStructure.AcceptMobileInfo();
                            mobileModel.IsEncrypt = false;
                            if (tourComModel.ContactInfo != null)
                            {
                                mobileModel.Mobile = tourComModel.ContactInfo.Mobile;
                            }
                            moblieList.Add(mobileModel);
                            sendMessageInfo.Mobiles = moblieList;

                            //发送通道
                            EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[Convert.ToInt32(SettingInfoModel.OrderSmsChannelIndex)];
                            sendMessageInfo.SendChannel = sendChannel;
                            sendMessageInfo.SendType = EyouSoft.Model.SMSStructure.SendType.直接发送;
                            EyouSoft.IBLL.SMSStructure.ISendMessage sBll = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance();
                            EyouSoft.Model.SMSStructure.SendResultInfo SendResultModel = sBll.Send(sendMessageInfo);
                            SendResultModel = null;
                            sendMessageInfo = null;

                            #endregion
                        }
                    }
                }
                #endregion

                Response.Clear();
                Response.Write(isSave.ToString().ToLower());
                Response.End();
                #endregion
            }
            else if (tourId.Length > 0)
            {
                #region 组团-地接-专线修改
                MTourList model = BTourList.CreateInstance().GetModel(tourId);
                model.ScheduleNum = Utils.GetInt(Utils.GetFormValue(txt_adultPrice.UniqueID));
                if (Utils.GetQueryStringValue("isZT").Length > 0)
                {
                    //组团修改
                    model.TravelNotes = Utils.GetFormValue(txt_travelNotes.UniqueID);
                    //组团社联系人
                    model.TravelContact = Utils.GetFormValue(txt_travelContact.UniqueID);
                    //组团社联系人电话
                    model.TravelTel = Utils.GetFormValue(txt_travelTel.UniqueID);
                    //游客联系人
                    model.VisitorContact = Utils.GetFormValue(txt_visitorContact.UniqueID);
                    //游客联系人电话
                    model.VisitorTel = Utils.GetFormValue(txt_visitorTel.UniqueID);
                    Response.Clear();
                    Response.Write(BTourList.CreateInstance().OrderModifyZT(model));
                    Response.End();
                }
                else
                {
                    //专线,地接修改
                    model.BusinessNotes = Utils.GetFormValue(txt_businessNotes.UniqueID);
                    Response.Clear();
                    Response.Write(BTourList.CreateInstance().OrderModifyZXDJ(model));
                    Response.End();
                }
                #endregion
            }
        }
    }
}
