using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Security.Membership;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 组团查找选择
    /// 创建者：luofx 时间：2010-7-5
    /// </summary>
    public partial class QueryTour : EyouSoft.Common.Control.YunYingPage
    {
        private int intPageSize = 21, intPageIndex = 1, intRecordCount = 0;
        protected string CompanyName = string.Empty;
        private int rpti = 1;
        /// <summary>
        /// 线路区域id
        /// </summary>
        protected string AeraID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                UserProvider.RedirectLoginOpenTopPage(Domain.SiteOperationsCenter + "/Login.aspx", "对不起，你还未登录或登录过期，请重新登录！");
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany QueryModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            if (Request.QueryString["Province"] != null)
            {
                QueryModel.PorvinceId = Utils.GetInt(Request.QueryString["Province"]);
                ProvinceAndCityList1.SetProvinceId = QueryModel.PorvinceId;
            }
            else
            {
                QueryModel.PorvinceId = 0;
            }
            if (Request.QueryString["City"] != null)
            {
                QueryModel.CityId = Utils.GetInt(Request.QueryString["City"]);
                ProvinceAndCityList1.SetCityId = QueryModel.CityId;
            }
            else
            {
                QueryModel.CityId = 0;
            }
            AeraID = Utils.GetQueryStringValue("AeraID");
            if (Utils.GetInt(AeraID) > 0)
                QueryModel.AreaId = Utils.GetInt(AeraID);
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            CompanyName = Utils.GetQueryStringValue("CompanyName");
            QueryModel.CompanyName = CompanyName;
            QueryModel.CompanyType = Utils.GetQueryStringValue("comType") == "1" ? EyouSoft.Model.CompanyStructure.CompanyType.专线 : EyouSoft.Model.CompanyStructure.CompanyType.地接;
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo Ibll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();

            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> lists = Ibll.GetListTourAgency(QueryModel, intPageSize, intPageIndex, ref intRecordCount);//(QueryModel, intPageSize, intPageIndex, ref intRecordCount);
            rptQueryTour.DataSource = lists;
            rptQueryTour.DataBind();
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            if (rptQueryTour.Items.Count < 1)
            {
                Literal1.Text = "暂无" + QueryModel.CompanyType.ToString() + "可以选择！";
            }
            Ibll = null;
            lists = null;
            QueryModel = null;
            lists = null;
        }
        /// <summary>
        /// 添加html标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptQueryTour_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (rpti % 3 == 0)
                {
                    e.Item.Controls.Add(new LiteralControl("</tr><tr>"));
                }
                int currentIndex = intPageSize * (intPageIndex - 1) + e.Item.ItemIndex + 1;//当前加载项
                if (intRecordCount == currentIndex)
                {
                    if (rpti % 3 == 1)
                    {
                        e.Item.Controls.Add(new LiteralControl("<td width=\"33%\" style=\"background-color: #EBF4FF; text-align: left; font-size: 12px; word-break:break-all;border:1px solid #4F9DE3;\">&nbsp;</td><td width=\"33%\" style=\"background-color: #EBF4FF; text-align: left; font-size: 12px; word-break:break-all;border:1px solid #4F9DE3;\">&nbsp;</td>"));
                    }
                    if (rpti % 3 == 2)
                    {
                        e.Item.Controls.Add(new LiteralControl("<td width=\"33%\" style=\"background-color: #EBF4FF; text-align: left; font-size: 12px; word-break:break-all; border:1px solid #4F9DE3;\">&nbsp;</td>"));
                    }
                }
            }
            rpti++;
        }

    }
}
