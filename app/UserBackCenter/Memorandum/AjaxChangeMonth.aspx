<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxChangeMonth.aspx.cs"
    Inherits="UserBackCenter.Memorandum.AjaxChangeMonth" %>

<form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="22%" height="22" align="center" bgcolor="#EFF7FF">
            <strong>日程安排 <a href="/Memorandum/AddMemorandum.aspx" onclick="topTab.open($(this).attr('href'),'添加备忘录',{isRefresh:false,data:{UpPage:0}});return false;"
                style="font-size: 14px; font-weight: bold;">
                <img src="<%=ImageServerPath %>/images/arrowpl.gif" width="11" height="9" />添加日程</a></strong>
        </td>
        <td width="55%" align="center" bgcolor="#EFF7FF">
            <img src="<%=ImageServerPath %>/images/dadeleft.gif" width="11" height="12" id="imgPrevMonth"
                style="cursor: pointer;" /><strong><asp:label id="lblVisibleDate" runat="server"></asp:label></strong><img
                    src="<%=ImageServerPath %>/images/daderight.gif" width="11" height="12" id="imgNextMonth"
                    style="cursor: pointer;" />
        </td>
        <td width="23%" align="center" bgcolor="#EFF7FF">
            <a href="/Memorandum/AddMemorandum.aspx" onclick="topTab.open($(this).attr('href'),'添加备忘录',{isRefresh:false,data:{UpPage:0}});return false;"
                style="font-size: 14px; font-weight: bold;">
                <img src="<%=ImageServerPath %>/images/arrowpl.gif" width="11" height="9" />添加日程</a>
        </td>
    </tr>
</table>
<asp:calendar id="CalendarDate" runat="server" firstdayofweek="Monday" borderwidth="1px"
    bordercolor="#DBE2F2" cssclass="datezt" width="99%" backcolor="White" height="200px"
    cellspacing="0" cellpadding="0" ondayrender="CalendarDate_DayRender" useaccessibleheader="true">
                    <NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom">
                    </NextPrevStyle>
                    <DayHeaderStyle Font-Size="9pt" Font-Bold="False" BorderWidth="1" BackColor="#F0F5FB"
                        BorderColor="#DBE2F2" Height="10px"></DayHeaderStyle>
                    <DayStyle BorderWidth="1" Height="70" HorizontalAlign="Center" BorderColor="#DBE2F2"></DayStyle>
                    <TitleStyle Font-Size="14pt" BorderWidth="0px" Height="22px" BackColor="#EFF7FF" Width="14%"
                        Font-Bold="True"></TitleStyle>
                </asp:calendar>

<script type="text/javascript">
    $("#<%=tblID %>").find("table[id$=CalendarDate]").find("tr").eq(0).css("display","none");
    $("#<%=tblID %>").find("img[id$=imgPrevMonth]").click(function(){$("#<%=tblID %>").find("a[id=PrevMonth]").click();});
    $("#<%=tblID %>").find("img[id$=imgNextMonth]").click(function(){$("#<%=tblID %>").find("a[id=NextMonth]").click();});
</script>

</form>
