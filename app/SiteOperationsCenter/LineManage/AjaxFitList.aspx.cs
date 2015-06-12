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
using System.Text;
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散客订单列表查询
    /// 蔡永辉 2011-12-21
    /// </summary>
    public partial class AjaxFitList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            if (!Page.IsPostBack)
            {
                BindLineList();
            }
        }
        /// <summary>
        /// 绑定散客订单列表
        /// </summary>
        protected void BindLineList()
        {
            int recordCount = 0;
            string SearchKeyword = Utils.InputText(Request.QueryString["SearchKeyword"]);//关键字
            int Line1 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line1"]);//专线国内国外周边
            int Line2 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line2"]);//专线区域
            string BusinessLine = Utils.GetQueryStringValue("BusinessLine");//专线商
            string tourid = Utils.InputText(Request.QueryString["tourid"]);//团号
            string StartDate = Utils.InputText(Request.QueryString["StartDate"]);//出发时间
            string EndDate = Utils.InputText(Request.QueryString["EndDate"]);//返回时间
            int OrderStatus = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["OrderStatus"]);//订单状态
            int SearchPaymentStatus = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["PaymentStatus"]);//支付状态
            int sortBytime = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Sort"]);//时间排序
            MTourOrderSearch SearchModel = new MTourOrderSearch();

            #region 订单状态
            List<PowderOrderStatus> listPowder = new List<PowderOrderStatus>();
            foreach (EnumObj item in EnumObj.GetList(typeof(PowderOrderStatus)))
            {
                if (item.Value == OrderStatus.ToString())
                    listPowder.Add((PowderOrderStatus)Utils.GetInt(item.Value));
            }
            SearchModel.PowderOrderStatus = listPowder;
            #endregion

            #region 支付状态
            List<PaymentStatus> listPay = new List<PaymentStatus>();
            foreach (EnumObj item in EnumObj.GetList(typeof(PaymentStatus)))
            {
                if (item.Value == SearchPaymentStatus.ToString())
                    listPay.Add((PaymentStatus)Utils.GetInt(item.Value));
            }
            SearchModel.PaymentStatus = listPay;
            #endregion
            if (SearchKeyword != "")
                SearchModel.OrderKey = SearchKeyword;
            if (sortBytime > 0)
                SearchModel.Order = sortBytime;
            if (Line1 > -1)
                SearchModel.AreaType = (AreaType)Line1;
            if (Line2 > 0)
                SearchModel.AreaId = Line2;
            if (BusinessLine != "0")
                SearchModel.Publishers = BusinessLine.ToString();
            if (tourid != "")
                SearchModel.TourId = tourid;
            if (StartDate != "")
                SearchModel.LeaveDateS = StartDate;
            if (EndDate != "")
                SearchModel.LeaveDateE = EndDate;

            IList<MTourOrder> listScenicArea = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, SearchModel);//线路list初始化
            if (listScenicArea.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "FitManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "FitManage.LoadData(this);", 0);
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>订单号</th>");
                strEmptyText.Append("<th>出发时间</th>");
                strEmptyText.Append("<th>线路名</th><th>状态</th>");
                strEmptyText.Append("<th>支付</th><th>专线商</th>");
                strEmptyText.Append("<th>预定单位</th><th>游客</th>");
                strEmptyText.Append("<th>电话</th><th>人数</th>");
                strEmptyText.Append("<th>预定时间</th><th>操作</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无订单信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            listScenicArea = null;
        }



        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
    }
}
