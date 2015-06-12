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
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.SystemStructure;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 线路管理 线路区域列表，搜索查询
    /// 蔡永辉 2011-12-19
    /// </summary>
    public partial class AjaxLineList : EyouSoft.Common.Control.YunYingPage
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
        /// 绑定线路列表
        /// </summary>
        protected void BindLineList()
        {
            int recordCount = 0;
            int Line1 = -1;
            string SearchKeyword = Utils.InputText(Request.QueryString["SearchKeyword"]);//关键字
            int intRecommendType = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["RecommendType"]);//推荐类型
            Line1 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line1"]);//专线国内国外周边
            int Line2 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line2"]);//专线编号
            int BusinessLine = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["BusinessLine"]);//专线商编号
            string Departure = Utils.InputText(Request.QueryString["Departure"]);//出发地
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["ProvinceId"]);//专线商所在省份
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["CityId"]);//专线商所在城市
            MRouteSearch SearchModel = new MRouteSearch();
            IList<MRoute> listMRoute = new List<MRoute>();//线路list初始化
            if (!string.IsNullOrEmpty(SearchKeyword))
                SearchModel.RouteKey = SearchKeyword;
            if (!string.IsNullOrEmpty(Departure))
                SearchModel.StartCityName = Departure;
            if (Line1 > -1)
                SearchModel.RouteType = (AreaType)Line1;
            if (BusinessLine > 0)
                SearchModel.Publishers = BusinessLine.ToString();
            if (Line2 > 0)
                SearchModel.AreaId = Line2;
            if (intRecommendType > 0)
                SearchModel.RecommendType = (RecommendType)intRecommendType;
            if (ProvinceId > 0)
                SearchModel.PublishersProvinceId = ProvinceId;
            if (CityId > 0)
                SearchModel.PublishersCityId = CityId;
            listMRoute = BRoute.CreateInstance().GetOperationsCenterList(PageSize, PageIndex, ref recordCount, SearchModel);
            if (listMRoute.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LineManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "LineManage.LoadData(this);", 0);
                this.repList.DataSource = listMRoute;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th nowrap=\"nowrap\">全选</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">线路名称</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">发布单位</th><th nowrap=\"nowrap\">状态</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">天数</th><th nowrap=\"nowrap\">班级计划</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">成人</th nowrap=\"nowrap\"><th>儿童</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">B2B</th><th nowrap=\"nowrap\">B2C</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">计划管理</th><th nowrap=\"nowrap\">点击</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">创建时间</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">操作</th>");                
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无线路信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            SearchModel = null;
            listMRoute = null;
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
