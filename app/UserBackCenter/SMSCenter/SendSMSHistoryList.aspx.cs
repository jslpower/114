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
using EyouSoft.Common;
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：短信发送的历史记录
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// </summary>
    public partial class SendSMSHistoryList : EyouSoft.Common.Control.BackPage
    {
        #region 成员变量初始化
        //每页显示的条数
        private static int intPageSize = 10;
        internal int intRecordCount;
        internal string companyid;
        private int CurrencyPage = 1;
        protected string ShowBeginDate = null;
        protected string ShowEndDate = null;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取公司的编号
            companyid = this.SiteUserInfo.CompanyID;
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["isExcel"]))) 
            {
                this.ExcelClick();
            }
            if (!Page.IsPostBack)
            {
                //调用绑定历史记录信息方法
                BindInfo();
            }
           
        }
        #endregion

        #region 绑定历史记录信息
        private void BindInfo()
        {
            //获取类型编号   
            int StatusId =Utils.GetInt(Request.QueryString["StatusId"], 0);
            if (Utils.InputText(Request.QueryString["StatusId"])!= null)
            {
                SendStatus.Value =Utils.InputText(Request.QueryString["StatusId"]);
            }
            //获取查询的关键字
            string keyword =Server.UrlDecode(Utils.InputText(Request.QueryString["keyword"]));
            if (!String.IsNullOrEmpty(keyword))
            {
                txtKey.Value = keyword;
            }
            //开始时间 
            DateTime? startTime = Utils.GetDateTimeNullable(Request.QueryString["leavDate"]);
            //结束时间
            DateTime? EndTime = Utils.GetDateTimeNullable(Request.QueryString["returnDate"]);
            if (startTime.HasValue)
            {
                ShowBeginDate = startTime.Value.ToShortDateString();
            }
            if (EndTime.HasValue)
            {
                ShowEndDate = EndTime.Value.ToShortDateString();
            }
            //根据关键字查询短信息历史信息
            CurrencyPage =Utils.GetInt(Request.QueryString["Page"], 1);

            if (CurrencyPage < 1) { CurrencyPage = 1; }

            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetListInfoByKey = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance().GetSendHistorys(intPageSize, CurrencyPage, ref intRecordCount, companyid, keyword, StatusId, startTime, EndTime);
            if (GetListInfoByKey != null && GetListInfoByKey.Count > 0)
            {
                //绑定短语信息
                ReListMoblie.DataSource = GetListInfoByKey;
                ReListMoblie.DataBind();
                //绑定分页控件
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                string []param={"urlType","t","page"};
                string linkUrl=StringValidate.BuildUrlString( Request.QueryString,param);
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?" + linkUrl + "&";
            }
            else
            {
                this.NoData.Visible = true;
                this.div_Expage.Visible = false;
            }
            GetListInfoByKey = null;
        }
        #endregion

        #region 导出Excel
        /// <summary>   
        /// 导出Excel   
        /// </summary>   
        /// <param name="ds">Ilist数据源</param>   
        /// <param name="FileName">文件名</param>   
        /// <param name="biaotou">表头</param>   
        public void EduceExcel(IList<EyouSoft.Model.SMSStructure.SendDetailInfo> ds, string FileName, string[] biaotou)
        {
            //清空页面所有内容输出
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;//设置缓冲输出     
            HttpContext.Current.Response.Charset = Encoding.UTF8.ToString();//设置输出流的HTTP字符集  
            //获取编码
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            //将Http头添加到输出流
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("短语历史信息记录.xls", System.Text.Encoding.UTF8).ToString());
            //设置输出流Http类型
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Write(HTML(ds, biaotou));
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Clear();

        }
        #endregion

        #region 数据列表项绑定事件
        protected void ReListMoblie_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //取得当前项的索引值加1,因为项的索引值是从0开始的.
            int intSmsId = e.Item.ItemIndex + 1;
            if (e.Item.ItemIndex != -1)
            {
                System.Web.UI.WebControls.Label lblSmsID = (Label)e.Item.FindControl("lblSMSID");
                lblSmsID.Text = Convert.ToString(((CurrencyPage - 1) * intPageSize) + intSmsId);
            }
        }
        #endregion

        #region 使用Ilist数据源
        /// <summary>   
        /// 使用Ilist作为数据源   
        /// </summary>   
        /// <param name="ds"></param>   
        /// <param name="biaotou"></param>   
        /// <returns></returns>   
        private string HTML(IList<EyouSoft.Model.SMSStructure.SendDetailInfo> ds, string[] biaotou)
        {
            StringBuilder ss = new StringBuilder();
            ss.Append("<table border=\"1\">");
            ss.Append("<tr>");
            ss.Append("<td>序号</td>");
            foreach (string str in biaotou)
            {
                ss.Append("<td>&nbsp;" + str + "</td>");
            }
            ss.Append("</tr>");
            int ii = 1;
            foreach (EyouSoft.Model.SMSStructure.SendDetailInfo o in ds)
            {
                ss.Append("<tr>");
                ss.Append("<td>&nbsp;" + (ii++).ToString() + "</td>");
                ss.Append("<td>&nbsp;" + DateTime.Parse(o.SendTime.ToString()).ToString("yyyy-MM-dd") + "</td>");
                ss.Append("<td>&nbsp;" + EyouSoft.Common.Utils.GetEncryptMobile(o.Mobile, o.IsEncrypt) + "</td>");
                string strSendContent = o.SMSContent;
                ss.Append("<td>&nbsp;" + strSendContent + "</td>");
                ss.Append("<td>&nbsp;" + decimal.Round(o.UseMoeny, 2) + "</td>");
                string SendResultget = "<font color='red'>发送失败</font>";
                if (o.ReturnResult.ToString() == "0")
                {
                    SendResultget = "发送成功";
                }
                ss.Append("<td>&nbsp;" + SendResultget + "</td>");
                ss.Append("</tr>");
            }
            ss.Append("</table>");
            return ss.ToString();
        }
        #endregion

        #region 导出Excel文件
        /// <summary>
        /// 将短语历史信息记录导出Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExcelClick()
        {
            string[] TableTitle = { "发送时间", "号码", "发送内容", "价格(元)", "状态" };
            string ExcelName = string.Empty;
            int intRecordCount = 0;

            //获取类型编号   
            int StatusId =Utils.GetInt(Request.QueryString["StatusId"], 0);
            //获取查询的关键字
            string keyword = "";
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["keyword"])))
            {
                keyword = Utils.InputText(Request.QueryString["keyword"]);
            }
            //开始时间
            DateTime? startTime = Utils.GetDateTimeNullable(Request.QueryString["leavDate"]);
            //结束时间
            DateTime? EndTime = Utils.GetDateTimeNullable(Request.QueryString["returnDate"]);


            //绑定查询条件
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"],1);
            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> listInfo = EyouSoft.BLL.SMSStructure.SendMessage.CreateInstance().GetSendHistorys(intPageSize,CurrencyPage,ref intRecordCount,companyid, keyword, StatusId, startTime, EndTime);
            if (listInfo != null && listInfo.Count > 0)
            {
                EduceExcel(listInfo, ExcelName, TableTitle);
                listInfo = null;
            }
            else
            {
                MessageBox.ResponseScript(this, "alert('没有短信信息 ');topTab.url(topTab.activeTabIndex,'/SMSCenter/SendSMSHistoryList.aspx');");
                return;
            }
        }
        #endregion
    }
}
