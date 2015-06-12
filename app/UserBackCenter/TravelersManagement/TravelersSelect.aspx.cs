using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.TicketStructure;

namespace UserBackCenter.TravelersManagement
{
    /// <summary>
    /// 常旅客选择框
    /// 创建人：徐从栎  2011-01-10
    /// </summary>
    public partial class TravelersSelect : EyouSoft.Common.Control.BasePage
    {
        protected int pageSize = 50;
        protected int pageIndex = 1;
        protected int recordCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitData();
            }
        }
        protected void InitData()
        {
            this.pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string kw = Server.UrlDecode(Utils.GetQueryStringValue("kw"));//姓名、手机号
            EyouSoft.BLL.TicketStructure.TicketVisitor BLL = new EyouSoft.BLL.TicketStructure.TicketVisitor();
            MVisitorSearchInfo Model = new MVisitorSearchInfo();
            //Model.Type = TicketDataType.线路常旅客;
            Model.KeyWord = kw;
            IList<TicketVistorInfo> lst = BLL.GetVisitors(this.pageSize, this.pageIndex, ref recordCount, this.SiteUserInfo.CompanyID, Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.BindPage(kw);
            }
            else
            {
                this.RepList.Controls.Add(new Literal() { Text = "<li style='width:100%;height:100%;text-align:center;border-bottom:solid 1px #AED7EE;border-right:solid 1px #AED7EE;'>暂无信息！</li>" });
                this.ExportPageInfo1.Visible = false;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="kw">关键字（姓名、手机号）</param>
        protected void BindPage(string kw)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("kw", kw);
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        protected string EnCodeStr(object o)
        {
            return Server.HtmlEncode(Convert.ToString(o));
        }
    }
}
