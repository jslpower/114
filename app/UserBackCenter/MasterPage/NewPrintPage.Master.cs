using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace UserBackCenter.MasterPage
{ 
    /// <summary>
    /// 打印单模版页
    /// 创建时间：2011-12-22 方琪
    /// </summary>
    public partial class NewPrintPage : System.Web.UI.MasterPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnWordPrint_Click(object sender, EventArgs e)
        {
            string browser = this.Context.Request.UserAgent.ToUpper();
            string printHtml = Request.Form["txtPrintHTML"];
            string title = Utils.GetString(this.Page.Title.ToString(), "打印单");
            string saveFileName = string.Format("{0}.doc", title);
            Response.ClearContent();
            if (browser.Contains("MS") && browser.Contains("IE"))
            {
                saveFileName = System.Web.HttpUtility.UrlEncode(saveFileName);
            }
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><title>打印预览</title><style type=\"text/css\">");
            sb.Append(".HeadTitle{font-family:\"黑体\";font-size: 25px;line-height: 120%;font-weight: bold;}");
            sb.Append("table{border-collapse: collapse;}table td{border-collapse: collapse;padding-left: 4px;}");
            sb.Append(".table_normal2{border: solid #000000;border-width: 1px 0 0 1px;}");
            sb.Append(".table_normal2 td, .table_normal2 th{border: solid #000000;border-width: 0 1px 1px 0;}");
            sb.Append(".table_normal{border: solid #000000;border-width: 1px 0 0 1px;}");
            sb.Append(".table_normal .normaltd{border: solid #000000;border-width: 0 1px 1px 0;}");
            sb.Append(".table_noneborder{border: none;}.table_l_border{border-left: 1px solid #000;}");
            sb.Append(".table_t_border{border-top: 1px solid #000;}.table_r_border{border-right: 1px solid #000;}");
            sb.Append(".table_b_border{border-bottom: 1px solid #000;}.td_noneborder{border: none;}");
            sb.Append(".td_l_border{border: solid #000;border-width: 0 0 0 1px;}");
            sb.Append(".td_l_t_border{border: solid #000;border-width: 1px 0 0 1px;}");
            sb.Append(".td_l_r_border{border: solid #000;border-width: 0 1px 0 1px;}");
            sb.Append(".td_l_b_border{border: solid #000;border-width: 0 0 1px 1px;}");
            sb.Append(".td_t_border{border: solid #000;border-width: 1px 0 0 0;}");
            sb.Append(".td_t_r_border{border: solid #000;border-width: 1px 1px 0 0;}");
            sb.Append(".td_t_b_border{border: solid #000;border-width: 1px 0 1px 0;}");
            sb.Append(".td_r_border{border: solid #000;border-width: 0 1px 0 0;}");
            sb.Append(".td_r_b_border{border: solid #000;border-width: 0 1px 1px 0;}");
            sb.Append(".td_b_border{border: solid #000;border-width: 0 0 1px 0;}");
            sb.AppendFormat("</style></head><body><div id=\"divPrintPreview\">{0}</div></body></html>", printHtml);
            Response.Write(sb.ToString());
            Response.End();
        }

    }
}
