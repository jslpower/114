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
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店后台设置 最新动态列表
    /// 创建者：袁惠 创建时间：2010-6-29
    /// </summary>
    
    public partial class SetNewsList : EyouSoft.Common.Control.BasePage
    {
        public int intPageSize = 15;
        public int CurrencyPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            if (!IsPostBack)
            {
                //初始化信息
                InitNewsList(); //列表
            }
        }
      
        /// <summary>
        /// 加载数据列表
        /// </summary>
        private void InitNewsList()
        {
            pnlNoData.Visible = false;
            int intRecordCount = 0; //总记录数
            CurrencyPage = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            IList<EyouSoft.Model.ShopStructure.HighShopNews> list = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetWebList(intPageSize, CurrencyPage, ref intRecordCount, this.SiteUserInfo.CompanyID, "");
            //绑定数据集
            if (list.Count > 0)
            {
                this.rptNews.DataSource = list;
                this.rptNews.DataBind();
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            }
            else
            {
                this.pnlNoData.Visible = true;
                this.ExportPageInfo1.Visible = false;
            }

           
            list = null;
        }
    }
}
