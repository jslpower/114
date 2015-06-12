<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QInformationManager.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.QInformationManager" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供求信息管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <style>
      .txtClass{ border-top-width: 1px;border-left-width: 1px; font-size: 16px;
                 border-bottom-width: 1px; width: 420px; text-align: center; border-right-width: 1px;"}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td style="border-bottom: 1px solid;" height="20">
                <strong>
                    <img src="<%= ImageServerUrl %>/images/yunying/icn_pen02.gif" width="13" height="13"></strong>
                供求管理 &gt; 供求信息 管理
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="1" width="98%" id="query"
        onkeydown="if(event.keyCode == 13){document.getElementById('btnQuery').click();event.returnValue=false;return false;}">
        <tr>
            <td colspan="5" style="border-bottom: 1px solid rgb(204, 204, 204);" height="25">
                标题：
                <input id="txtKeyWord" name="txtKeyWord" runat="server" type="text" />
                信息编号：
                <input id="txtinforID" name="txtinforID" runat="server" type="text" />
                <input id="btnQuery" name="btnQuery" value=" 查 找 " type="button" onclick="QueryHandle()" />
                <asp:Button runat="server" ID="btnDeleteAll" Text=" 批 量 删 除 " OnClick="btnDeleteAll_Click" />
            </td>
        </tr>
    </table>
    <table align="center" border="1" bordercolor="#468fc1" cellpadding="0" cellspacing="0"
        width="98%" id="ExchangList">
        <tr>
            <td width="10%" align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif">
                <input type="checkbox" id="ckAll" /><label for="ckAll">全选</label>
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="49%">
                标题
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="26%">
                链接
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="15%">
                编辑|删除
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptSupplierList" OnItemCommand="RepeaterList_ItemCommand"
            OnItemDataBound="RepeaterList_ItemDataBound">
            <ItemTemplate>
                <tr name="TrInfor">
                    <td align="center">
                        <input type="checkbox" name="ckExchangID" value="<%# Eval("ID") %>" />
                        <%# (CurrencyPage-1)*intPageSize+Container.ItemIndex+1 %>
                    </td>
                    <td align="center">
                        <span style="border-bottom: 1px solid #cccccc;">    
                            <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("ExchangeTitle")%>' CssClass="txtClass"></asp:TextBox>
                        </span>
                    </td>
                    <td align="center">
                        <a href="<%= EyouSoft.Common.Domain.UserPublicCenter %>/info_<%# Eval("ID") %>" target="_blank">
                            <%= EyouSoft.Common.Domain.UserPublicCenter%>/info_<%# Eval("ID") %></a>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnEdit" runat="server" CommandName="edit" Text="编辑" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'/>
                        <asp:Button runat="server" ID="btnDel" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                            Text="删除" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr runat="server" id="trNoData" visible="false">
            <td align="center" height="100" colspan="9">
                暂无数据!
            </td>
        </tr>
    </table>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo" runat="server" />
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function mouseovertr(o) {
            $(o).css("background-color", "#FFF9E7");
        }
        function mouseouttr(o) {
            $(o).removeAttr("style");
        }

        function QueryHandle() {
            var KeyWord = $("#<%= txtKeyWord.ClientID %>").val();
            var MessageId = $("#<%= txtinforID.ClientID %>").val();
            location.href = "/SupplierManage/QInformationManager.aspx?KeyWord=" + encodeURI(KeyWord) + "&messageID=" + encodeURI(MessageId);
        }

        $(document).ready(function() {
            $("tr").filter("[name]='TrInfor'").each(function() {
                $(this).mouseover(function() {
                    $(this).css("background-color", "#FFF9E7");
                });
                $(this).mouseout(function() {
                    $(this).removeAttr("style");
                });
            });
            $("#ckAll").click(function() {
                var cked = $(this).attr("checked");
                $("#ExchangList").children().find("input[type]='checkbox'").each(function() {
                    $(this).attr("checked", cked);
                });
            });

            $("#<%=btnDeleteAll.ClientID %>").click(function() {
                var flag = false;
                $(":checkbox[name]='ckExchangID'").each(function() {
                    if ($(this).attr("checked")) {
                        flag = true;
                        return true;
                    }
                });
                if (!flag) {
                    alert("请选择要删除的数据！");
                    return false;
                }
            });

//            $("#rptSupplierList_ctl00_btnDel").click(function() {
//                var id = $("").val();
//                $.ajax({
//                    url: '/SupplierManage/QInformationManager.aspx?action=del&id=' + id,
//                    type: 'post',
//                    dataType: 'json',
//                    success: function(data) {
//                        alert(data);
//                    },
//                    error: function() {
//                        alert("服务器繁忙，请稍后在试!");
//                    }
//                });
//            })
        });
    </script>

</body>
</html>
