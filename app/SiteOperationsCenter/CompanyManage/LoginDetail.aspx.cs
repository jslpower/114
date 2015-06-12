using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 登录明细
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class LoginDetail : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 15;
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
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                GetLoginList();
            }
        }

        /// <summary>
        /// 获的公司的登录明细
        /// </summary>
        protected void GetLoginList()
        {
            int recordCount = 0;
            string CompanyId = Request.QueryString["CompanyId"];

            IList<EyouSoft.Model.CompanyStructure.LogUserLogin> LoginList = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetLoginList(PageSize, PageIndex, ref recordCount, CompanyId);
            if (LoginList != null && LoginList.Count > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;

                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.repLoginList.DataSource = LoginList;
                this.repLoginList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width='100%' border='1' align='center' cellpadding=\"0\" cellspacing=\"1\" class=\"kuang\" >");
                strEmptyText.AppendFormat("<tr background=\"{0}/images/yunying/hangbg.gif\" class=\"white\" height=\"23\"><td width=\"10%\" height=\"23\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>序号</strong></td><td width=\"20%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登陆人姓名</strong> </td><td width=\"30%\" align=\"center\" valign=\"middle\" background=\"{0}/images/yunying/hangbg.gif\"><strong>用户名</strong></td><td width=\"40%\" align=\"center\" background=\"{0}/images/yunying/hangbg.gif\"><strong>登录时间</strong></td></tr>", ImageServerUrl);
                strEmptyText.Append("<tr class=\"huanghui\" ><td  align='center' colspan='10' height='100px'>暂无该旅行社登录记录</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repLoginList.EmptyText = strEmptyText.ToString();
            }
            LoginList = null;
      
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
