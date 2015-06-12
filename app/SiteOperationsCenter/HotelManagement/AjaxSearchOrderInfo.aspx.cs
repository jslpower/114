using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// ajax酒店订单查询列表信息 
    /// 开发人：孙川  时间：2010-12-08
    /// </summary>
    public partial class AjaxSearchOrderInfo : EyouSoft.Common.Control.YunYingPage
    {
        protected int PageSize = 10;        //分页数
        protected int PageIndex = 0;        //分页索引

        protected void Page_Load(object sender, EventArgs e)
        {
            PageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (!IsPostBack)
            {
                BindOrderInfo();
            }
        }

        /// <summary>
        /// 绑定搜索的订单列表
        /// </summary>
        protected void BindOrderInfo()
        {
            int recordCount = 0;
            string BuyerUName = Utils.InputText(Request.QueryString["CustomerName"]);
            string OrderId = Utils.InputText(Request.QueryString["OrderId"]);
            DateTime? CreateSDate = Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CreateSDate"]));
            DateTime? CreateEDate = Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CreateEDate"]));
            DateTime? CheckInSDate =  Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CheckInSDate"]));
            DateTime? CheckInEDate = Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CheckInEDate"]));
            DateTime? CheckOutSDate = Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CheckOutSDate"]));
            DateTime? CheckOutEDate = Utils.GetDateTimeNullable( Utils.InputText( Request.QueryString["CheckOutEDate"]));
            string HotelName = Utils.InputText(Request.QueryString["HotelName"]);
            string OrderSate = Request.QueryString["OrderSate"];

            EyouSoft.Model.HotelStructure.OrderStateList? StateEmnu = null;
            switch (OrderSate)
            {
                case "1":
                    StateEmnu = EyouSoft.Model.HotelStructure.OrderStateList.已确认;
                    break;
                case "2":
                    StateEmnu = EyouSoft.Model.HotelStructure.OrderStateList.取消;
                    break;
                case "3":
                    StateEmnu = EyouSoft.Model.HotelStructure.OrderStateList.处理中;
                    break;
                case "4":
                    StateEmnu = EyouSoft.Model.HotelStructure.OrderStateList.NOWSHOW;
                    break;
                default: break;
            }

            //订单查询
            EyouSoft.Model.HotelStructure.SearchOrderInfo ModelSearchOrder = new EyouSoft.Model.HotelStructure.SearchOrderInfo();
            ModelSearchOrder.BuyerUName = BuyerUName == "" ? null : BuyerUName;
            ModelSearchOrder.ResOrderId = OrderId == "" ? null : OrderId;
            ModelSearchOrder.CreateSDate = CreateSDate;
            ModelSearchOrder.CreateEDate = CreateEDate;
            ModelSearchOrder.CheckInSDate = CheckInSDate;
            ModelSearchOrder.CheckInEDate = CheckInEDate;
            ModelSearchOrder.CheckOutSDate = CheckOutSDate;
            ModelSearchOrder.CheckOutEDate = CheckOutEDate;
            ModelSearchOrder.HotelName = HotelName == "" ? null : HotelName;
            ModelSearchOrder.OrderState = StateEmnu;
            

            IList<EyouSoft.Model.HotelStructure.OrderInfo> ModelList = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, ModelSearchOrder);
            
            if (ModelList != null && ModelList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "OrderSearch.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "OrderSearch.LoadData(this);", 0);
                this.crptIOList.DataSource = ModelList;
                this.crptIOList.DataBind();
                ModelList = null;
            }
            else
            {
                this.crptIOList.EmptyText = "<tr><td colspan=\"14\"><div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">没有找到相关的数据!</span></div></td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
            }
            ModelSearchOrder = null;
        }

        /// <summary>
        /// 获取旅客姓名
        /// </summary>
        public string GetGuests(IList<EyouSoft.HotelBI.HBEResGuestInfo> GuestList)
        {
            string Result = "";
            StringBuilder strGustsName = new StringBuilder();
            if (GuestList != null && GuestList.Count > 0)
            {
                foreach (EyouSoft.HotelBI.HBEResGuestInfo Guset in GuestList)
                {
                    strGustsName.Append(Guset.PersonName + "，<br/>");
                }
                Result = strGustsName.ToString().Substring(0, strGustsName.Length-1) ;
            }

            return Result;
        }
        
    }
}
