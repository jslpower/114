using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.HotelStructure;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 说明：用户后台—旅游资源—酒店团单(列表)
    /// 创建人：徐从栎
    /// 创建时间：2011-12-15
    /// </summary>
    public partial class GroupOrderList : BackPage
    {
        protected int pageSize = 15;
        protected int pageIndex = 1;
        protected int recordCount;
        protected string tblID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.tblID = Guid.NewGuid().ToString();
                this.initData();
            }
        }
        protected void initData()
        {
            this.pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string kw =Server.UrlDecode(Utils.GetQueryStringValue("kw"));//关键字
            string flag = Utils.GetQueryStringValue("flag");//0:入住时间  1:离店时间
            DateTime? startTime =Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startTime"));//开始时间
            DateTime? endTime =Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endTime"));//结束时间
            EyouSoft.BLL.HotelStructure.HotelTourCustoms BLL = new EyouSoft.BLL.HotelStructure.HotelTourCustoms();
            SearchTourCustomsInfo Model=new SearchTourCustomsInfo();
            Model.HotelName = kw;
            Model.CompanyId = this.SiteUserInfo.CompanyID;
            if (flag == "0")//入住时间
            {
                Model.CheckInEDate = endTime;
                Model.CheckInSDate = startTime;
            }
            else
            { //离店时间
                Model.CheckOutEDate = endTime;
                Model.CheckOutSDate = startTime;
            }
            IList<HotelTourCustoms> lst=BLL.GetList(this.pageSize,this.pageIndex,ref this.recordCount,Model);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                this.BindPage(kw, flag, startTime, endTime);
            }
            else
            {
                this.RepList.Controls.Add(new Literal() { Text = "<tr><td colspan='9' align='center'>暂无信息！</td></tr>" });
                this.ExportPageInfo1.Visible = false;
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="kw">关键字</param>
        /// <param name="flag">0:入住时间  1:预订时间</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        protected void BindPage(string kw,string flag,DateTime? startTime,DateTime? endTime)
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams.Add("kw", kw);
            this.ExportPageInfo1.UrlParams.Add("flag", flag);
            this.ExportPageInfo1.UrlParams.Add("startTime",Convert.ToString(startTime));
            this.ExportPageInfo1.UrlParams.Add("endTime",Convert.ToString(endTime));
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
    }
}
