using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteOperationsCenter.Others
{
    /// <summary>
    /// 机票接口访问记录信息
    /// </summary>
    /// Author:汪奇志 2011-05-10
    public partial class TicketLogs : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pageSize = 20, pageIndex = 0, recordCount = 0;
            pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);

            IList<EyouSoft.Model.TicketStructure.MLBLogInfo> logs = EyouSoft.BLL.TicketStructure.BLogTicket.CreateInstance().GetLogs(pageSize, pageIndex,ref recordCount, null);

            if (logs != null && logs.Count > 0)
            {
                this.rptLogs.DataSource = logs;
                this.rptLogs.DataBind();

                this.rptLogs.Visible = true;
                this.phNotFound.Visible = false;

                string scripts = @"var pageConfig = {{
                    pageSize:{0},
                    pageIndex: {1},
                    recordCount:{2},
                    pageCount: 0,
                    showPrev: true,
                    showNext: true
                }};
                $(document).ready(function(){{
                    AjaxPageControls.replace(""divPage"", pageConfig);
                }});";

                this.RegisterScripts(string.Format(scripts, pageSize, pageIndex, recordCount));
            }
            else
            {
                this.rptLogs.Visible = false;
                this.phNotFound.Visible = true;
            }
        }

        /// <summary>
        /// register scripts
        /// </summary>
        /// <param name="scripts"></param>
        private void RegisterScripts(string scripts)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), scripts, true);
        }
    }
}
