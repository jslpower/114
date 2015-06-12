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

namespace SeniorOnlineShop.seniorshop
{
    /// <summary>
    /// 页面功能：最新动态
    /// 开发时间：2010-07-13  开发人：杜桂云
    /// </summary>
    public partial class NewsList : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        private int CurrencyPage = 1;
        private int intPageSize = 10;
        protected string CompanyID = "";
        #endregion        

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.Master.CompanyId;
            BindInfo();
            Page.Title = "最新动态";
        }
        #endregion 

        #region 绑定列表
        protected void BindInfo()
        {
            int intRecordCount = 0;
            //查询关键字
            string keyWord = Utils.InputText(Request.QueryString["k"]);
            if (Request.QueryString["Page"] == "" || StringValidate.IsInteger(Request.QueryString["Page"]) == false || Int32.Parse(Request.QueryString["Page"]) == 0)
            {
                CurrencyPage = 1;
            }
            else
            {
                CurrencyPage = Convert.ToInt32(Request.QueryString["Page"]);
            }
            IList< EyouSoft.Model.ShopStructure.HighShopNews> modelList= EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetWebList(intPageSize, CurrencyPage, ref intRecordCount, CompanyID, keyWord);

            if (modelList != null && modelList.Count > 0)
            {
                //绑定数据源
                this.rptNewsList.DataSource = modelList;
                this.rptNewsList.DataBind();
                //绑定分页控件
                this.ExporPageInfoSelect1.intPageSize = intPageSize;
                this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.LinkType = 3;

            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
            }
            modelList = null;
        }
        #endregion

        #region 项绑定时激发
        protected void rptNewsList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                HtmlAnchor link = e.Item.FindControl("linkNew") as HtmlAnchor;
                if (link != null)
                {
                    link.InnerText =Utils.InputText(  DataBinder.Eval(e.Item.DataItem, "Title").ToString());
                    link.HRef = "New.aspx?cid=" + CompanyID + "&newid=" + DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                }
            }
        }
        #endregion
    }
}
