<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogManagement.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.LogManagement" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" id="tbSearch">
        <tr>
            <td class="search_bg">
                &nbsp;操作员
                <asp:TextBox runat="server" ID="Operator" Width="100"></asp:TextBox>
                订单号
                <asp:TextBox runat="server" ID="OrderNo" Width="100"></asp:TextBox>
                <img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/yunying/chaxun.gif" width="62"
                    onclick="OnSearch()" height="21" style="margin-bottom: -3px; cursor: pointer" />
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center">
        <tr>
            <td width="695" height="25">
                <strong>&nbsp;订单操作记录</strong>
            </td>
        </tr>
    </table>
    <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C7DEEB"
        class="table_basic" style="margin-top: 5px;">
        <tr>
            <th>
                序号
            </th>
            <th>
                操作订单号
            </th>
            <th>
                操作单位
            </th>
            <th>
                操作员
            </th>
            <th>
                操作类型
            </th>
            <th>
                操作日期
            </th>
            <th>
                操作描述
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rptLogList">
            <ItemTemplate>
                <tr>
                    <td height="25" align="left" bgcolor="#FFFFFF">
                        <%# GetCount() %>
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <%#Eval("OrderNo")%>
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <%#Eval("CompanyName")%>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <%#Eval("OperatorName")%>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <%#Eval("OrderType").ToString()%>
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <%#Eval("IssueTime")%>
                    </td>
                    <td align="left" bgcolor="#FFFFFF">
                        <%#Eval("Remark")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div align="right">
        <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        function OnSearch() {
            var OrderNo = $("#<%=OrderNo.ClientID %>").val();
            var Operator = $("#<%=Operator.ClientID %>").val();
            window.location.href = "LogManagement.aspx?OrderNo=" + OrderNo + "&Operator=" + Operator;
        }
        $("#tbSearch input").bind("keypress", function(e) {
            if (e.keyCode == 13) {
                OnSearch();
                return false;
            }
        })
    </script>

</body>
</html>
