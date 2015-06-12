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
using System.Text;
using System.Collections.Generic;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散拼计划管理 散拼列表的查询删除
    /// 蔡永辉 2011-12-20
    /// </summary>
    public partial class AjaxScatteredfightList : EyouSoft.Common.Control.YunYingPage
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
            string action = "";
            action = Utils.GetQueryStringValue("action");//ajax请求类型


            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            if (!Page.IsPostBack)
            {
                if (action == "Delete")
                {
                    Delete();
                }
                BindLineList();
            }
        }

        #region 删除计划
        protected void Delete()
        {
            string argument = "";
            string[] strlist = { "" };
            argument = Utils.GetQueryStringValue("argument");//ajax请求参数
            if (!string.IsNullOrEmpty(argument))
            {
                if (EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().DeletePowder(argument))
                {
                    Response.Clear();
                    Response.Write("ok");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("error");
                Response.End();
            }
        }
        #endregion


        /// <summary>
        /// 绑定散拼计划列表
        /// </summary>
        protected void BindLineList()
        {
            int recordCount = 0;
            string SearchKeyword = Utils.InputText(Request.QueryString["SearchKeyword"]);//关键字
            int Line1 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line1"]);//专线国内国外周边
            int Line2 = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Line2"]);//专线编号
            string BusinessLine = Utils.GetQueryStringValue("BusinessLine");//专线商
            string Departure = Utils.InputText(Request.QueryString["Departure"]);//出发地
            string StartDate = Utils.InputText(Request.QueryString["StartDate"]);//出团开始时间
            string EndDate = Utils.InputText(Request.QueryString["EndDate"]);//出团结束时间
            string PriceStart = Utils.InputText(Request.QueryString["PriceStart"]);//价格范围(开始)
            string PriceEnd = Utils.InputText(Request.QueryString["PriceEnd"]);//价格范围(结束)
            int Themeid = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["Themeid"]);//主题

            MPowderSearch SearchModel = new MPowderSearch();
            if (SearchKeyword != "")
                SearchModel.TourKey = SearchKeyword;
            if (Line1 > -1)
                SearchModel.AreaType = (AreaType)Line1;
            if (Line2 > 0)
                SearchModel.AreaId = Line2;
            if (BusinessLine != "0")
                SearchModel.Publishers = BusinessLine;
            if (!string.IsNullOrEmpty(Departure))
                SearchModel.StartCityName = Departure;
            if (!string.IsNullOrEmpty(StartDate))
                SearchModel.LeaveDate = Convert.ToDateTime(StartDate);
            if (!string.IsNullOrEmpty(EndDate))
                SearchModel.EndLeaveDate = Convert.ToDateTime(EndDate);
            if (!string.IsNullOrEmpty(PriceStart))
                SearchModel.StartPrice = Convert.ToDecimal(PriceStart);
            if (!string.IsNullOrEmpty(PriceEnd))
                SearchModel.EndPrice = Convert.ToDecimal(PriceEnd);
            if (Themeid > 0)
                SearchModel.ThemeId = Themeid;
            IList<MPowderList> listScenicArea = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, SearchModel);

            if (listScenicArea.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "ScatteredfightManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ScatteredfightManage.LoadData(this);", 0);
                this.repList.DataSource = listScenicArea;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"98%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr class=\"list_basicbg\">");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">全选</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">团号</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">出发地</th><th align=\"middle\" nowrap=\"nowrap\">线路名称</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">发布单位</th><th align=\"middle\" nowrap=\"nowrap\">类型</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">出团日期</th align=\"middle\" nowrap=\"nowrap\"><th>报名截止</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">人数</th><th align=\"middle\" nowrap=\"nowrap\">余位</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">状态</th><th align=\"middle\" nowrap=\"nowrap\">成人价</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">儿童价</th><th align=\"middle\" nowrap=\"nowrap\">单房差</th>");
                strEmptyText.Append("<th align=\"middle\" nowrap=\"nowrap\">游客</th><th align=\"middle\" nowrap=\"nowrap\">功能</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无散拼计划</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr>");
                strEmptyText.Append(" <td align=\"center\"><input type=\"checkbox\" name=\"checkbox2\" id=\"checkbox22\" />");
                strEmptyText.Append("全选</td>");
                //strEmptyText.Append("<td colspan=\"15\" align=\"left\"><input type=\"submit\" name=\"button\" id=\"button\" value=\"统一修改团队行程\" /></td>");
                strEmptyText.Append(" </tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            //SearchModel = null;
            listScenicArea = null;
        }


        #region 获取样式
        protected string GetClass(string statue)
        {
            string strclass = "";
            switch (statue)
            {
                case "成团":
                    strclass = "chengtuan";
                    break;

                case "客满":
                    strclass = "keman";
                    break;

                case "收客":
                    strclass = "zhengc";
                    break;

                case "停收":
                    strclass = "tings";
                    break;
            }
            return strclass;
 
        }

        #endregion


        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }
    }
}
