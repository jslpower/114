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
using EyouSoft.Model.NewTourStructure;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散客订单统计列表查询
    /// 蔡永辉 2011-12-22
    /// </summary>
    public partial class AjaxLineDetaile : EyouSoft.Common.Control.YunYingPage
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
            if (!IsPostBack)
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
            string IsOrNoDetal = Utils.GetQueryStringValue("IsDetail");
            string Lineid = Utils.GetQueryStringValue("LineId");
            MOrderStaticSearch SearchModel = new MOrderStaticSearch();
            SearchModel.AreaId = Utils.GetInt(Lineid);
            SearchModel.IsDetail = bool.Parse(IsOrNoDetal);
            IList<MOrderStatic> listScenicArea = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetOrderStaticLst(PageSize, PageIndex, ref recordCount, SearchModel);


            #region 传递数据
            //StringBuilder strb = new StringBuilder("{tolist:[");
            //foreach (MOrderStatic item in listScenicArea)
            //{
            //    strb.Append("{\"RouteName\":\"" + item.RouteName + "\",\"TotalOrder\":\"" + item.TotalOrder + "\""
            //              + ",\"TotalPeople\":\"" + item.TotalPeople + "\",,\"TotalAdult\":\"" + item.TotalAdult + "\""
            //              + ",\"TotalChild\":\"" + item.TotalChild + "\",,\"TotalSale\":\"" + item.TotalSale + "\""
            //              + ",\"TotalSettle\":\"" + item.TotalSettle + "\"},");
            //}
            //Response.Clear();
            //Response.Write(strb.ToString().TrimEnd(',') + "]}");
            //Response.End();

            #endregion

            if (listScenicArea.Count > 0)
            {
                //this.ExporPageInfoSelect1.intPageSize = PageSize;
                //this.ExporPageInfoSelect1.intRecordCount = recordCount;
                //this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                //this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                //this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "LineDetailmanage.LoadData(this);", 1);
                //this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "LineDetailmanage.LoadData(this);", 0);
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th>时间</th>");
                strEmptyText.Append("<th>专线</th>");
                strEmptyText.Append("<th>订单量</th><th>总人数</th>");
                strEmptyText.Append("<th>成人</th><th>儿童</th>");
                strEmptyText.Append("<th>销售总额</th><th>订单总额</th>");
                strEmptyText.Append("<th>功能</th>");
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
