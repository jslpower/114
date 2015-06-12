using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.AdvertiseTicket
{
    /// <summary>
    /// 机票咨询信息列表
    /// 开发人：刘玉灵  时间：2010-10-12
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 20;
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
            if (!CheckMasterGrant(EyouSoft.Common.YuYingPermission.广告机票_机票查询_管理该栏目))
            {
                Utils.ResponseNoPermit();
                return;
            }

            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"],1);
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                string type = Request.QueryString["Type"].ToString();
                if (type.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write(this.DeletTicket());
                    Response.End();
                }
            }
            if (!Page.IsPostBack)
            {
                BindTicketList();
            }
        }

        /// <summary>
        /// 绑定机票咨询列表信息
        /// </summary>
        private void BindTicketList()
        {
            string Title = Utils.InputText( Server.UrlDecode( Request.QueryString["Title"]));
            int Type = Utils.GetInt(Request.QueryString["Type"]);
            string ContactName = Utils.InputText(Server.UrlDecode( Request.QueryString["ContactName"]));
            DateTime  ? StartDate = Utils.GetDateTimeNullable(Request.QueryString["StartDate"]);
            if (!string.IsNullOrEmpty(Title))
            {
                this.txtTitle.Value = Title;

            }
            EyouSoft.Model.TicketStructure.VoyageType VoyageType = EyouSoft.Model.TicketStructure.VoyageType.所有;
            if (Type>0)
            {
                this.dropType.SelectedValue = Type.ToString();
                switch (Type)
                {
                    case 1:
                        VoyageType = EyouSoft.Model.TicketStructure.VoyageType.单程;
                        break;
                    case 2:
                        VoyageType = EyouSoft.Model.TicketStructure.VoyageType.往返程;
                        break;
                    case 3:
                        VoyageType = EyouSoft.Model.TicketStructure.VoyageType.缺口程;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                this.txtContactName.Value = ContactName;
            }
            if (StartDate!=null)
            {
                this.txtStartDate.Value = StartDate.Value.ToShortDateString();
            }
            int recordCount = 0;
            EyouSoft.Model.TicketStructure.QueryTicketApply TicketModel = new EyouSoft.Model.TicketStructure.QueryTicketApply();
            TicketModel.Title = Title;
            TicketModel.VoyageType = VoyageType;
            TicketModel.ContactName=ContactName;
            TicketModel.TakeOffDateStart = StartDate;
            IList<EyouSoft.Model.TicketStructure.TicketApply>TicketList=EyouSoft.BLL.TicketStructure.TicketApply.CreateInstance().GetList(TicketModel, PageSize, PageIndex, ref recordCount);
            if (recordCount > 0)
            {
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.intRecordCount = recordCount;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.repTicketList.DataSource = TicketList;
                this.repTicketList.DataBind();
            }
            else {
                this.repTicketList.EmptyText = "<tr><td height='100px' align='center' colspan='10'>暂无记录</td></tr>";
                this.ExportPageInfo1.Visible = false;
            }
            TicketModel = null;
            TicketList = null;

        }

        /// <summary>
        /// 删除记录
        /// </summary>
        protected bool DeletTicket()
        {
            bool Result = false;
            string[] strckTicketId = Request.Form.GetValues("ckTicketId");
            if (strckTicketId != null)
            {
                Result = EyouSoft.BLL.TicketStructure.TicketApply.CreateInstance().Delete(strckTicketId);
            }
            return Result;
        }
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }

    }
}
