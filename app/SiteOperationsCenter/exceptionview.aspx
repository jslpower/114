<%@ Page Language="C#" Inherits="EyouSoft.Common.Control.YunYingPage" EnableViewState=false%>
<%@ Import namespace="Microsoft.Practices.EnterpriseLibrary.Data" %>
<%@ Import namespace="EyouSoft.Common" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="System.Data.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
	<link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
	<script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
	<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>                
                申请时间
                <input name="applyStartDate" type="text" class="textfield" size="12" onfocus="WdatePicker()"/>-<input name="applyFinishDate" type="text" class="textfield" size="12" onfocus="WdatePicker()"/><asp:Button runat=server ID="btnDoQuery" Text="提交" 
        onclick="btnDoQuery_Click" />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rptList" runat="server">
		<HeaderTemplate>
		<table width=100%>
		<tr>
		<td width=120>错误发生时间&nbsp;&nbsp;</td>
		<td width=250>错误发生页面</td>
		<td width=180>错误信息</td>
		<td width=420>错误发生位置</td>
		</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
			<td  width=120>
				<%#Eval("IssueTime")%>&nbsp;&nbsp;
			</td>
			<td  width=250>
				<%#Eval("RequestUrl")%>
			</td>
			<td  width=180 style="WORD-BREAK:BREAK-ALL">
				<font color="blue"><%#Eval("ErrorMessage")%></font>
			</td>
			<td  width=420>
				<font color="red"><%#Eval("StackTrace").ToString().Replace("\n","<br>")%></font>
			</td>
			</tr>
			<tr>
			<td colspan=4><br></td>
			</tr>
		</ItemTemplate>
		<FooterTemplate>
		</table>
		</FooterTemplate>
	</asp:Repeater>
	<table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td>
        </tr>
    </table>
	<script runat=server>

		protected void btnDoQuery_Click(object sender, EventArgs e)
		{
			
			Response.Redirect("exceptionview.aspx?applyStartDate="+Request.Form["applyStartDate"]+"&applyFinishDate="+Request.Form["applyFinishDate"]);

		}

		protected void Page_Load(object sender, EventArgs e)
        {
			int pageSize = 20;
			int pageIndex = 1;
			int recordCount = 0;
			StringBuilder cmdQuery = new StringBuilder();

			cmdQuery.Append(" 1=1 ");

			if (!String.IsNullOrEmpty(Request.QueryString["applyStartDate"]))
            {
                cmdQuery.AppendFormat(" AND IssueTime>='{0}' ", Request.QueryString["applyStartDate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["applyFinishDate"]))
            {
                cmdQuery.AppendFormat(" AND IssueTime<='{0}' ", Request.QueryString["applyFinishDate"]);
            }

			pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
			Database logstore = DatabaseFactory.CreateDatabase("LogStore");
			IDataReader rdr = EyouSoft.Common.DAL.DbHelper.ExecuteReader(logstore, pageSize, pageIndex, ref recordCount, "tbl_LogException", "ErrorID", "*", cmdQuery.ToString(), "IssueTime DESC");
			rptList.DataSource = rdr;
			rptList.DataBind();
			rdr.Close();

			this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;

        }
	</script>
    </form>
</body>
</html>
