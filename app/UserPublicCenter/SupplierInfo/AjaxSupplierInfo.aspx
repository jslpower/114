<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSupplierInfo.aspx.cs"
    Inherits="UserPublicCenter.SupplierInfo.AjaxSupplierInfo" %>

<head runat="server">
</head>
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0"
    style="margin: 10px 0 10px 0; border-bottom: 1px solid #ccc;">
    <asp:repeater runat="server" id="rptExchangeList" onitemdatabound="RepeatorList_ItemDataBound">
        <ItemTemplate>
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" height="27">
                <td width="76%" height="24" class="lan14">
                    <asp:Literal runat="server" ID="ltrTagHtml"></asp:Literal>
                    <div style="float: left;">
                        <asp:Literal runat="server" ID="ltrProvince"></asp:Literal><asp:Literal runat="server"
                            ID="ltrTitle"></asp:Literal>
                    </div>
                </td>
                <td width="4%" class="huise">
                    <%# Eval("IssueTime","{0:MM.dd}")%>
                </td>
                <td width="13%" class="huise">
                   <span style="margin-left:4px;" title="<%# Eval("CompanyName") %>"><%# EyouSoft.Common.Utils.GetText(Eval("CompanyName").ToString(),7)%></span>
                </td>
                <td width="7%">
                    <asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr bgcolor="#EFEFEF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" height="27">
                <td width="76%" height="24" class="lan14">
                    <asp:Literal runat="server" ID="ltrTagHtml"></asp:Literal>
                    <div style="float: left;">
                        <asp:Literal runat="server" ID="ltrProvince"></asp:Literal><asp:Literal runat="server"
                            ID="ltrTitle"></asp:Literal>
                    </div>
                </td>
                <td width="4%" class="huise">
                    <%# Eval("IssueTime","{0:MM.dd}")%>
                </td>
                <td width="13%" class="huise">
                    <span title="<%# Eval("CompanyName") %>" style="margin-left:4px;"><%# EyouSoft.Common.Utils.GetText(Eval("CompanyName").ToString(),7)%></span>
                </td>
                <td width="7%">
                    <asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:repeater>
    <input type="hidden" id="hPageSize" value="<%= intPageSize %>" />
    <input type="hidden" id="hPageIndex" value="<%= CurrencyPage %>" />
    <input type="hidden" id="hRecordCount" value="<%= intRecordCount %>" />
</table>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#E2EDFF";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>

