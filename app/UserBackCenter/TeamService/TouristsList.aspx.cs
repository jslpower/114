using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社 散拼计划 预定 游客名单列表
    /// 李晓欢 2011-12-23
    /// </summary>
    public partial class TouristsList : EyouSoft.Common.Control.BasePage
    { 
        #region 参数
        protected int intPageIndex = 1;
        private int intPageSize = 10;
        private int RecordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string Key = EyouSoft.Common.Utils.GetQueryStringValue("SearchKey");
                if (Key != null && !string.IsNullOrEmpty(Key))
                {
                    this.SearchKey.Value = Key;
                }

                BindTouristsList();
                
            }
        }

        #region 绑定游客名单
        protected void BindTouristsList()
        {
            intPageIndex = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("page"), 1);
            BindPage();
        }
        #endregion

        #region 分页
        protected void BindPage()
        {
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
        }
        #endregion
    }
}
