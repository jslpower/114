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

namespace UserBackCenter.RouteAgency.RouteManage
{
    public partial class PrintPreview : EyouSoft.Common.Control.FrontPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region WORD 打印按钮 CLICK事件
        /// <summary>
        /// WORD 打印按钮 CLICK事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWordPrint_Click(object sender, EventArgs e)
        {
            string printHtml = Request.Form["txtPrintHTML"];
            string saveFileName = HttpUtility.UrlEncode("行程单.doc");
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", saveFileName));
            Response.ContentType = "application/ms-word";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            string wordTemplate = @"
                <html>
                <head>
                    <title>打印预览</title>
                    <style type=""text/css"">
                    body {{color:#000000;font-size:12px;font-family:""宋体"";background:#fff; margin:0px;}}
                    img {{border: thin none;}}
                    table {{border-collapse:collapse;width:710px;}}
                    td {{font-size: 12px; line-height:18px;color: #000000;  }}	
                    .headertitle {{font-family:""黑体""; font-size:25px; line-height:120%; font-weight:bold;}}
                    </style>
                </head>
                <body>
                    <div id=""divPrintPreview"">{0}</div>
                </body>
                </html>";

            Response.Write(string.Format(wordTemplate, printHtml));

            Response.End();
        }
        #endregion
    }
}
