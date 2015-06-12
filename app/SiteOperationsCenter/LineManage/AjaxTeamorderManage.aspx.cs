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
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 团队订单统计列表查询
    /// 蔡永辉 2011-12-23
    /// </summary>
    public partial class AjaxTeamorderManage : EyouSoft.Common.Control.YunYingPage
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
        /// 绑定团队订单列表
        /// </summary>
        protected void BindLineList()
        {
            int recordCount = 0;
            string SearchKeyword = Utils.InputText(Request.QueryString["SearchKeyword"]);//关键字
            int Line1 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line1"]);//专线国内国外周边
            int Line2 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line2"]);//专线区域
            string BusinessLine = Utils.GetQueryStringValue("BusinessLine");//专线商
            string StartDate = Utils.InputText(Request.QueryString["StartDate"]);//出发地
            string EndDate = Utils.InputText(Request.QueryString["EndDate"]);//出发地
            int OrderStatus = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["OrderStatus"]);//订单状态
            MTourListSearch SearchModel = new MTourListSearch();
            #region 订单状态
            switch (OrderStatus)
            {
                case 0:
                    SearchModel.TourOrderStatus = TourOrderStatus.未确认;
                    break;
                case 1:
                    SearchModel.TourOrderStatus = TourOrderStatus.已确认;
                    break;
                case 2:
                    SearchModel.TourOrderStatus = TourOrderStatus.结单;
                    break;
                case 3:
                    SearchModel.TourOrderStatus = TourOrderStatus.取消;
                    break;
            }
            #endregion
            if (SearchKeyword != "")
                SearchModel.TourKey = SearchKeyword;
            if (Line1 > -1)
                SearchModel.AreaType = (AreaType)Line1;
            //if (Line2 > 0)
            //    SearchModel.TourOrderStatus = Line2;
            //if (StartDate != "")
            SearchModel.SLeaveDate = Utils.GetDateTimeNullable(StartDate);
            //if (EndDate != "")
            SearchModel.ELeaveDate = Utils.GetDateTimeNullable(EndDate);


            IList<MTourList> listScenicArea = EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, SearchModel);
            if (listScenicArea.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "TeamorderManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "TeamorderManage.LoadData(this);", 0);
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr class=\"list_basicbg\">");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">出发城市</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">线路名称</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">出发时间</th><th align=\"middle\" nowrap=\"nowrap\">专线商</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">预订单位</th><th align=\"middle\" nowrap=\"nowrap\">预定时间</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">人数</th><th align=\"middle\" nowrap=\"nowrap\">状态</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">操作</th>");
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
