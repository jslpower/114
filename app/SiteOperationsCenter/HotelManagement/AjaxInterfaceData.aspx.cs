using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Common.Function;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// 酒店管理：接口数据列表Ajax
    /// 2010-12-02，袁惠
    /// </summary>
    public partial class AjaxInterfaceData : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageSize=20;
        protected int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店后台管理_首页板块数据管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_首页板块数据管理, true);
                    return;
                }
                pageIndex=Utils.GetInt(Request.QueryString["page"],1);
                int recordCount=0;
                int dataType = Utils.GetInt(Request.QueryString["datatype"], -1);  //数据类型
                string InTime = Request.QueryString["CheckInDate"];  //入住日期
                string OutTime = Request.QueryString["CheckOutDate"];  //离店日趋
                string City = Utils.GetQueryStringValue("City");    //城市
                int StarNum =Utils.GetInt(Request.QueryString["StarNum"],0); //星级
                int SelectType = Utils.GetInt(Request.QueryString["SelectType"],0); //选择类型
                string HotelName = Utils.GetQueryStringValue("HotelName");
              //  HotelShowType = Utils.GetInt(Request.QueryString["HotelShowType"], 0);
                if (!StringValidate.IsDateTime(InTime))
                {
                    Utils.ResponseMeg(false, "入住日期格式错误！");
                    return;
                }
                if (!StringValidate.IsDateTime(OutTime))
                {
                    Utils.ResponseMeg(false, "离店日期格式错误！");
                    return;
                }
                if(Convert.ToDateTime(InTime)>Convert.ToDateTime(OutTime))
                {
                    Utils.ResponseMeg(false, "入住日期和离店日期范围填写错误！");
                    return;
                }
                decimal? MinPrice = null;//价格范围
                decimal? MaxPrice = null;
                string price=Utils.GetQueryStringValue("MinPrice");
                if(!string.IsNullOrEmpty(price))
                {
                  
                    if (StringValidate.IsDecimal(price))
                    {
                        MinPrice = Convert.ToDecimal(price);
                    }
                    else
                    {
                        Utils.ResponseMeg(false, "价格下限输入错误！");
                        return;
                    }
                }
                price = Utils.GetQueryStringValue("MaxPrice");
                if (!string.IsNullOrEmpty(price))
                {
                    //price = price.ToString("F2");
                    if (StringValidate.IsDecimal(price))
                    {
                        MaxPrice = Convert.ToDecimal(price);
                    }
                    else
                    {
                        Utils.ResponseMeg(false, "价格上限输入错误！");
                        return;
                    }
                }
                //IList<EyouSoft.Model.HotelStructure.HotelInfo> list = EyouSoft.BLL.HotelStructure.HotelLocalInfo.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, (EyouSoft.Model.HotelStructure.HotelShowType)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.hotelshow), dataType));
                EyouSoft.HotelBI.MultipleSeach searchModel = new EyouSoft.HotelBI.MultipleSeach();
                searchModel.CheckInDate = InTime;
                searchModel.CheckOutDate = OutTime;
                searchModel.CityCode = City;
                searchModel.HotelChineseName = HotelName;
                searchModel.HotelRank = (EyouSoft.HotelBI.HotelRankEnum)StarNum;
                searchModel.IsPageView = true;
                searchModel.NumOfEachPage = 20;
                searchModel.PageNo = pageIndex;
                if(SelectType==1)
                {
                 //  searchModel.Payment = "T";  //前台现付
                }
                searchModel.PriceMaxRate = MaxPrice;
                searchModel.PriceMinRate = MinPrice;
                EyouSoft.Model.HotelStructure.RespPageInfo respPageInfo = null;

                EyouSoft.HotelBI.ErrorInfo errorModel = new EyouSoft.HotelBI.ErrorInfo();
                IList<EyouSoft.Model.HotelStructure.HotelInfo> HotelList = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelList(searchModel, ref respPageInfo, out errorModel);
                //如果接口查询异常 则提示 查询超时
                if (errorModel == null || errorModel.ErrorType == EyouSoft.HotelBI.ErrorType.未知错误 || errorModel.ErrorType == EyouSoft.HotelBI.ErrorType.系统级错误)
                {
                    Response.Clear();
                    Response.Write("查询超时，请重新查询或稍后再试.");                                       
                    Response.End();
                    return;
                }
                hidInterHotel.Value = JsonConvert.SerializeObject(HotelList);
                if (HotelList != null && HotelList.Count > 0)
                {
                    this.ExporPageInfoSelect1.intPageSize = pageSize;
                    this.ExporPageInfoSelect1.intRecordCount =respPageInfo!=null?respPageInfo.TotalNum:recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "FirstPageDataAdd.LoadData(this,\"AjaxInterfaceData.aspx\");", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "FirstPageDataAdd.LoadData(this,\"AjaxInterfaceData.aspx\");", 0);
                    crptInterList.DataSource = HotelList;
                    crptInterList.DataBind();
                    HotelList = null;
                }
                else
                {
                    crptInterList.EmptyText = "<tr><td colspan=\"4\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">暂无数据！</span></div></td></tr>";
                    ExporPageInfoSelect1.Visible = false;
                }
                searchModel = null;
            }
        }
    }
}
