using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Linq;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Function;
namespace UserBackCenter.TeamService
{   
    /// <summary>
    /// 页面功能：组团下订单
    /// 开发人：xuty 开发时间：2010-06-30
    /// </summary>
    public partial class RouteOrder : EyouSoft.Common.Control.BasePage
    {
        protected string routeName;//线路名称
        protected string startDate;//出发日期
        protected int remain;//剩余空位

        protected string company;//产品发布单位
        protected string contant;//联系人
        protected string tel;//联系电话
        protected string MQ;//联系MQ
        protected string leaveCity;//出港城市
        protected string tourId;//团队编号
        protected int customerNo = 1;//游客信息组编号
        protected int adultNum;//成人数
        protected int childNum;//儿童数
        protected string weekDay;
        protected string parentId;
        EyouSoft.IBLL.TourStructure.ITour tourBll;
        EyouSoft.Model.TourStructure.TourInfo tourModel;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsLogin)//是否登录
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }
            if (!IsCompanyCheck)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('尚未审核通过!');if(window.parent.Boxy){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();}else{window.close();}</script>");
                return;
            }
            //是否开通组团服务
            if (!SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('你尚未开通组团服务!');if(window.parent.Boxy){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();}else{window.close();}</script>");
                return;
            }
            tourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            tourId = Utils.GetQueryStringValue("tourid");
            //初始化团队信息
            if (tourId != "")
            {
                adultNum = Utils.GetInt(Request.QueryString["adultnum"], 0);//获取成人数
                childNum = Utils.GetInt(Request.QueryString["childnum"], 0);//获取儿童数
                LoadRouteOrder(tourId);
            }
            //下订单
            if (Page.IsPostBack)
            {  
               SaveOrder();
            }
            
        }

        #region 绑定报价类型项
        protected void ro_rpt_priceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {   
            EyouSoft.Model.TourStructure.TourPriceDetail priceModel = e.Item.DataItem as EyouSoft.Model.TourStructure.TourPriceDetail;
            decimal adultPrice=priceModel.PriceDetail.Where(i=>i.CustomerLevelType==EyouSoft.Model.CompanyStructure.CustomerLevelType.同行).First().AdultPrice;
            decimal childPrice=priceModel.PriceDetail.Where(i=>i.CustomerLevelType==EyouSoft.Model.CompanyStructure.CustomerLevelType.同行).First().ChildrenPrice;
            decimal roomPrice=priceModel.PriceDetail.Where(i=>i.CustomerLevelType==EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差).First().AdultPrice;
            StringBuilder builder = new StringBuilder();
            string class1="class=\"ro_noselect\"";
            string class2="style=\"display:none\" class=\"ro_select\"";
            string check = "";
            string disabled = "disabled=\"disabled\"";
            if (e.Item.ItemIndex == 0)//默认第一个报价类型展开
            {
               class1 = "class=\"ro_noselect\" style=\"display:none\"";
               class2 = "class=\"ro_select\"";
               check = "checked='checked'";
               disabled = "";
               ro_txtSumPrice.Value =string.Format("{0:f}", adultPrice * adultNum + childPrice * childNum);//初始总额
            }
            string str = string.Format("<span><input name=\"ro_rdiPriceStandId\" type=\"radio\" id=\"changgui\" value=\"{0}\" {7}  onclick=\"RouteOrder.selectPrice(this)\"/>" +
                        "<label>{1}&nbsp;</label>" +
                        "<span {5}>成人价￥<label>{2,0:f}</label>&nbsp; 儿童价￥<label>{3,0:f}</label>&nbsp;单房差￥<label>{4,0:f}</label></span><span {6}>" +
                        "成人数<input name=\"ro_txtManCount\" id=\"ro_txtManCount1\" {8} runat=\"server\" onchange=\"RouteOrder.changePeopleCount(this,'1')\" type=\"text\" sourceCount=\"{9}\" value=\"{9}\"  style=\"border:1px solid #7F9DB9;width:20px;\"/>×单价<input name=\"ro_txtManPrice\" id=\"ro_txtManPrice\" type=\"text\" readonly=\"readonly\" {8} value=\"{2,0:f}\"  style=\"width:50px;border:1px solid #7F9DB9; color:Gray\"/><span style=\"font-size:20px; font-weight:bold; color:#cc0000;\">+</span>" +
                        "儿童数<input name=\"ro_txtChildCount\" id=\"ro_txtChildCount1\" {8} onchange=\"RouteOrder.changePeopleCount(this,'0')\" sourceCount=\"{10}\" type=\"text\" value=\"{10}\"  style=\"width:20px;border:1px solid #7F9DB9;\"/>×单价<input name=\"ro_txtChildPrice\" id=\"ro_txtChildPrice\" type=\"text\" {8} value=\"{3,0:f}\"  readonly=\"readonly\"  style=\"width:50px;border:1px solid #7F9DB9;color:Gray\"/><span style=\"font-size:20px; font-weight:bold; color:#cc0000;\">+</span>" +
                        "单房差<input name=\"ro_txtOneRoomCount\" {8} onchange=\"RouteOrder.ro_change(this)\" id=\"ro_txtOneRoomCount1\" sourceCount=\"0\" type=\"text\" value=\"0\"  style=\"width:20px;border:1px solid #7F9DB9;\"/>×单价<input name=\"ro_txtOneRoomPrice\" id=\"ro_txtOneRoomPrice\" {8} type=\"text\" value=\"{4,0:f}\" readonly=\"readonly\"  style=\"width:50px;border:1px solid #7F9DB9;color:Gray\"/>+" +
                        "其它费用<input name=\"ro_txtOtherPrice\" {8} onchange=\"RouteOrder.ro_change(this)\" id=\"ro_txtOtherPrice1\" type=\"text\" value=\"0\" sourceCount=\"0\"  style=\"width:50px;border:1px solid #7F9DB9;\"/></span></span><br/>", priceModel.PriceStandId, priceModel.PriceStandName, adultPrice, childPrice, roomPrice, class1, class2, check, disabled,adultNum,childNum);
            LiteralControl liter = new LiteralControl(str);
            e.Item.Controls.Add(liter);
        }
        #endregion

        #region 初始化线路订单的信息
        protected void LoadRouteOrder(string tourId)
        {
           tourModel = tourBll.GetTourInfo(tourId);
           
           if (tourModel != null)
           {  
              
               //是否隐藏下订单按钮
               if (tourBll.IsDeleted(tourId))
               {
                   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('该团队不存在!');if(window.parent.Boxy.getIframeDialog){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();}else{window.close();}</script>");
                   return;
               }
               if (tourModel.TourState == EyouSoft.Model.TourStructure.TourState.收客 && tourModel.RemnantNumber > 0)
               {
                   ro_SaveOrder.Visible = true;
               }
               else if (tourModel.TourState != EyouSoft.Model.TourStructure.TourState.收客)
               {
                   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('该团队已停收!');</script>");
               }
               else if (tourModel.RemnantNumber == 0)
               {
                   Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('该团队人数已满!');</script>");
               }
               parentId = tourModel.ParentTourID;//获取模板团编号
           }
           else
           {

               Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('该团队不存在!');if(window.parent.Boxy.getIframeDialog){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();}else{window.close();}</script>");
               return;
           }

           routeName = tourModel.RouteName;//线路名称
           startDate = tourModel.LeaveDate.ToString("yyyy-MM-dd");
           weekDay = EyouSoft.Common.Utils.ConvertWeekDayToChinese(tourModel.LeaveDate);
           remain = tourModel.RemnantNumber;//剩余人数
           
           company = tourModel.CompanyName;//公司名称
           contant = tourModel.TourContact;//团队负责人
           tel = tourModel.TourContactTel;
           MQ = tourModel.TourContacMQ;
           int cityId= tourModel.LeaveCity;//出港城市ID
           ro_traffic.Value = tourModel.LeaveTraffic;
           EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityId);
           if (cityModel != null)
           {
               leaveCity = cityModel.CityName;//获取出港城市名
           }
           //绑定报价类型   leaveCity 
           EyouSoft.IBLL.SystemStructure.ISysCity cityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
           ro_rpt_priceList.DataSource = tourModel.TourPriceDetail;
           ro_rpt_priceList.DataBind();
        }
        #endregion

        #region 下订单
        protected void SaveOrder()
        {
            //团队所属公司ID
            string TourCompanyId = "";

            //获得订单信息
            tourModel = tourBll.GetTourInfo(tourId);
            string priceId = Utils.GetFormValue("ro_rdiPriceStandId");//获取选择的报价等级编号
            EyouSoft.Model.TourStructure.TourOrder orderModel = new EyouSoft.Model.TourStructure.TourOrder();
            orderModel.AdultNumber =Utils.GetInt(Utils.GetFormValue("ro_txtManCount"),0);//获取成人数
            orderModel.BuyCompanyID = SiteUserInfo.CompanyID;//预定单位
            orderModel.BuyCompanyName = SiteUserInfo.CompanyName;//预定单位名
            orderModel.ChildNumber = Utils.GetInt(Utils.GetFormValue("ro_txtChildCount"),0);//儿童数
            EyouSoft.Model.TourStructure.TourPriceDetail price = tourModel.TourPriceDetail.Where(i => i.PriceStandId == priceId).First();
            //根据获取的报价等级编号获取相应报价等级
            orderModel.ChildPrice = price.PriceDetail.Where(i => i.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行).First().ChildrenPrice;//儿童价
            TourCompanyId = tourModel.CompanyID;
            orderModel.CompanyID = TourCompanyId;
            orderModel.ContactFax = SiteUserInfo.ContactInfo.Fax;
            orderModel.ContactMQ = SiteUserInfo.ContactInfo.MQ;
            orderModel.ContactName = SiteUserInfo.ContactInfo.ContactName;
            orderModel.ContactQQ = SiteUserInfo.ContactInfo.QQ;
            orderModel.ContactTel = SiteUserInfo.ContactInfo.Tel;
            orderModel.LastOperatorID = SiteUserInfo.ID;//最后操作人
            orderModel.MarketNumber = int.Parse(Utils.GetFormValue("ro_txtOneRoomCount"));//单房差数
            orderModel.MarketPrice = price.PriceDetail.Where(i => i.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.单房差).First().AdultPrice;//单房差价
            orderModel.OperatorContent = Utils.GetFormValue("ro_txtOperatorContent");//操作留言
            orderModel.OperatorID = SiteUserInfo.ID;//操作人ID
            orderModel.OperatorName = SiteUserInfo.ContactInfo.ContactName;//操作人名
            orderModel.OrderType = 0;//预定类型
            orderModel.OtherPrice = decimal.Parse(Utils.GetFormValue("ro_txtOtherPrice"));//其他费用
            orderModel.PeopleNumber = orderModel.ChildNumber + orderModel.AdultNumber;//总人数
            if (orderModel.PeopleNumber == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('请填写游客!');</script>");
            }
            orderModel.AreaType = tourModel.AreaType;
            orderModel.AreaId = tourModel.AreaId;
            orderModel.PersonalPrice = price.PriceDetail.Where(i => i.CustomerLevelType == EyouSoft.Model.CompanyStructure.CustomerLevelType.同行).First().AdultPrice;//成人价
            orderModel.PriceStandId = priceId;//报价等级
            orderModel.RouteName = tourModel.RouteName;
            orderModel.LeaveDate = tourModel.LeaveDate;//出发时间
            orderModel.SpecialContent = Utils.GetFormValue("ro_txtSpecialContent");//特别要求
            orderModel.SumPrice = orderModel.AdultNumber * orderModel.PersonalPrice + orderModel.ChildNumber * orderModel.ChildPrice + orderModel.MarketNumber * orderModel.MarketPrice + orderModel.OtherPrice;//总金额
            orderModel.TourCompanyId = tourModel.CompanyID;//专线编号
            orderModel.TourCompanyName = tourModel.CompanyName;//专线公司名
            orderModel.TourDays = tourModel.TourDays;//天数
            orderModel.SaveSeatDate = DateTime.Now;//留位时间
            orderModel.TourId = tourModel.ID;//团队编号
            orderModel.TourNo = tourModel.TourNo;//团号
            orderModel.TourType = EyouSoft.Model.TourStructure.TourType.组团团队;
            orderModel.IssueTime = DateTime.Now;//添加时间
            orderModel.OrderSource= EyouSoft.Model.TourStructure.TourOrderOperateType.组团社下单;//订单来源
         
            //获得游客信息
            List<EyouSoft.Model.TourStructure.TourOrderCustomer> customerList = new List<EyouSoft.Model.TourStructure.TourOrderCustomer>();
            List<string> custNoList = Request.Form.AllKeys.Where(i => i.Contains("CustomerName")).Select(i=>i.Substring(12,i.Length-12)).ToList();
            foreach(string customerNo in custNoList)
            {
                EyouSoft.Model.TourStructure.TourOrderCustomer customerModel = new EyouSoft.Model.TourStructure.TourOrderCustomer();
                customerModel.CompanyID = SiteUserInfo.CompanyID;//所属公司
                customerModel.CompanyName = SiteUserInfo.CompanyName;//所属公司名
                customerModel.ContactTel = Utils.GetFormValue("CustomerTelphone" + customerNo);
                customerModel.CradNumber = Utils.GetFormValue("CertificateNo" + customerNo);//证件编号
                customerModel.CradType =(EyouSoft.Model.TourStructure.CradType)int.Parse(Utils.GetFormValue("CertificateName" + customerNo));//证件类型
                customerModel.Remark = Utils.GetFormValue("CustomerRemark" + customerNo);//备注信息
                customerModel.Sex = Utils.GetFormValue("CustomerSex" + customerNo)=="1"?true:false;//性别
                customerModel.SiteNo = Utils.GetFormValue("CustomerSiteNo" + customerNo);//座位号
                orderModel.SeatList+= customerModel.SiteNo + ",";//座位号集
                customerModel.VisitorName = Utils.GetFormValue("CustomerName" + customerNo);//姓名
                customerModel.VisitorType = Utils.GetFormValue("CustomerType" + customerNo)=="1"?true:false;//类型(成人,儿童)
                customerModel.IssueTime = DateTime.Now;
                customerModel.RouteName = routeName;
                customerModel.TourId = tourId;//团队ID
                customerModel.TourNo = tourModel.TourNo;//团号
                customerList.Add(customerModel);
            }
            orderModel.SeatList=orderModel.SeatList.TrimEnd(',');//获得座位号
            orderModel.TourOrderCustomer = customerList;//保存游客信息
            EyouSoft.IBLL.TourStructure.ITourOrder tourOrderBll = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance();
            //开始下订单
            if (tourOrderBll.AddTourOrder(orderModel)> 0)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyInfo =
                    EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(TourCompanyId);

                bool isSend = Utils.SendSMSForReminderOrder(
                    TourCompanyId,
                    SiteUserInfo.CompanyName,
                    SiteUserInfo.ContactInfo.ContactName,
                    _companyInfo != null ? _companyInfo.CityId : 0);


                if (isSend)
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo model =
                        EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(TourCompanyId);
                    //发送短信记录
                    EyouSoft.Model.ToolStructure.MsgTipRecord tipModel = new EyouSoft.Model.ToolStructure.MsgTipRecord();
                    tipModel.Email = string.Empty;
                    tipModel.FromMQID = SiteUserInfo.ContactInfo.MQ;
                    tipModel.ToMQID = model.ContactInfo.MQ;//接收方MQ
                    tipModel.Mobile = model.ContactInfo.Mobile;//接收方手机
                    tipModel.MsgType = EyouSoft.Model.ToolStructure.MsgType.NewOrder;
                    tipModel.SendWay = EyouSoft.Model.ToolStructure.MsgSendWay.SMS;

                    EyouSoft.BLL.ToolStructure.MsgTipRecord msgTipBll = new EyouSoft.BLL.ToolStructure.MsgTipRecord();
                    msgTipBll.Add(tipModel);
                }

                Utils.SendEmailForReminderOrder(TourCompanyId, SiteUserInfo.CompanyName, SiteUserInfo.UserName);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('预定成功!');if(window.parent.Boxy){window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();if(window.parent.RouteStock){window.parent.RouteStock.refresh();};}else{window.close();}</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script>;alert('预定失败!');</script>");
            }


        }
        #endregion
    }
  
}
