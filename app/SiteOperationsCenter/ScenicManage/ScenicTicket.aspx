<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicTicket.aspx.cs" Inherits="SiteOperationsCenter.ScenicManage.ScenicTicket" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>景区门票-门票查看</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td id="seachTD" class="search_bg">
                门票类型
                <input id="txtSightName" size="20" name="txtSightName" runat="server" />
                是否审核
                <asp:DropDownList runat="server" ID="ddlCheck">
                </asp:DropDownList>
                状态
                <asp:DropDownList runat="server" ID="ddlState">
                </asp:DropDownList>
                B2B
                <asp:DropDownList runat="server" ID="ddlB2B">
                </asp:DropDownList>
                B2C
                <asp:DropDownList runat="server" ID="ddlB2C">
                </asp:DropDownList>
                <a href="javascript:void(0);" onclick="ScenicTicket.SeachTicket();return false;">
                    <img src="<%= ImageServerUrl %>/images/chaxun.gif" width="62" height="21" style="margin-bottom: -3px;" /></a>
            </td>
        </tr>
    </table>
    <table width="98%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
        class="table_basic" style="margin-top: 5px;">
        <tr class="list_basicbg">
            <th>
                门票类型
            </th>
            <th>
                景区
            </th>
            <th>
                门市价
            </th>
            <th>
                优惠价
            </th>
            <th>
                有效时间
            </th>
            <th>
                支付
            </th>
            <th>
                状态
            </th>
            <th>
                B2B
            </th>
            <th>
                B2C
            </th>
            <th>
                管理
            </th>
        </tr>
        <cc1:CustomRepeater runat="server" ID="rptTicket">
            <ItemTemplate>
                <tr>
                    <td align="left">
                        <%# Eval("TypeName")%>
                    </td>
                    <td align="center">
                            <%# Eval("ScenicName")%>
                    </td>
                    <td align="center">
                        <%# Eval("RetailPrice","{0:C0}")%>
                    </td>
                    <td align="center">
                        <%# Eval("WebsitePrices", "{0:C0}")%>
                    </td>
                    <td align="center">
                        <%# GetGoodTime(Eval("StartTime"), Eval("EndTime"))%>
                    </td>
                    <td align="center">
                        <%# Eval("Payment").ToString()%>
                    </td>
                    <td align="center">
                        <%# GetState(Eval("ExamineStatus"), Eval("Status"))%>
                    </td>
                    <td align="center">
                        <%# Eval("B2B").ToString()%>
                    </td>
                    <td align="center">
                        <%# Eval("B2C").ToString()%>
                    </td>
                    <td align="center">
                        <a href="/ScenicManage/EditScenicTicket.aspx?action=edit&id=<%# Eval("TicketsId") %>&cid=<%# Eval("CompanyId") %>">
                            修改</a>
                        <asp:PlaceHolder runat="server" ID="plnDel" Visible='<%# GetVisible(Eval("ExamineStatus"), Eval("Status")) %>'>
                            <a href="javascript:void(0);" onclick="ScenicTicket.DeleteTicket('<%# Eval("TicketsId") %>','<%# Eval("CompanyId") %>');return false;">
                                删除</a> </asp:PlaceHolder>
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
                <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#seachTD").keydown(function(event) {
                if (event.keyCode == 13) {
                    ScenicTicket.SeachTicket();
                    event.returnValue = false;
                    return false;
                }
            })
        });
        var ScenicTicket = {
            SeachTicket: function() {
                var kw = $("#<%= txtSightName.ClientID %>").val();
                var icCheck = $("#<%= ddlCheck.ClientID %>").val();
                var state = $("#<%= ddlState.ClientID %>").val();
                var b2b = $("#<%= ddlB2B.ClientID %>").val()
                var b2c = $("#<%= ddlB2C.ClientID %>").val()

                var par = { sn: kw, ic: icCheck, st: state, b2b: b2b, b2c: b2c };
                window.location.href = "/ScenicManage/ScenicTicket.aspx?" + $.param(par);
            },
            DeleteTicket: function(tid, cid) {
                if (confirm("确定要删除此门票吗？")) {
                    if (tid != "" && cid != "")
                        window.location.href = "/ScenicManage/ScenicTicket.aspx?action=del&id=" + tid + "&cid=" + cid;
                }
            }
        };
    </script>

    </form>
</body>
</html>
