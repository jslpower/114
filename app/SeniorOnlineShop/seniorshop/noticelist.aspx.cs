using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Web.UI.HtmlControls;

namespace SeniorOnlineShop.seniorshop
{
    /// <summary>
    /// 高级网店 同业公告
    /// 李晓欢 2011-12-14
    /// </summary>
    public partial class noticelist : EyouSoft.Common.Control.FrontPage
    {
        #region 成员变量
        private int CurrencyPage = 1;
        private int intPageSize = 10;
        protected string CompanyID = "";
        protected bool IsLogins = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CompanyID = this.Master.CompanyId;
                BindInfo();
                Page.Title = "同业公告";
                IsLogins = IsLogin;
            }
        }

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

            EyouSoft.Model.NewsStructure.MQueryPeerNews querySearch=new EyouSoft.Model.NewsStructure.MQueryPeerNews ();
            querySearch.CompanyId = this.Master.CompanyId;
            querySearch.KeyWord = keyWord;
            IList<EyouSoft.Model.NewsStructure.MPeerNews> listNews = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance().GetGetPeerNewsList(intPageSize, CurrencyPage, ref intRecordCount, querySearch);
            if (listNews != null && listNews.Count > 0)
            {
                //绑定数据源
                this.rptNewsList.DataSource = listNews;
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
            listNews = null;
        }
        #endregion

        /// <summary>
        /// 获取快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl(string ID)
        {
            string url = EyouSoft.Common.Domain.UserBackCenter + "/TongYeInfo/InfoShow.aspx?pageFrom=shop&InfoId=" + ID;
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        protected void rptNewsList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                HtmlAnchor link = e.Item.FindControl("linkNew") as HtmlAnchor;
                if (link != null)
                {
                    link.InnerText = Utils.InputText(DataBinder.Eval(e.Item.DataItem, "Title").ToString());
                    link.Attributes.Add("NoticesId", DataBinder.Eval(e.Item.DataItem, "NewId").ToString());
                    if (IsLogin) //登录
                    {
                        link.HRef = Domain.UserBackCenter + "/TongYeInfo/InfoShow.aspx?pageFrom=shop&InfoId=" + DataBinder.Eval(e.Item.DataItem, "NewId").ToString();

                    }                   
                }
            }
        }
    }
}
