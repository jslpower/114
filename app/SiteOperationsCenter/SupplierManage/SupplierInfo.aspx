<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierInfo.aspx.cs" Inherits="SiteOperationsCenter.SupplierManage.SupplierInfo" %>

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
    <table align="center" border="0" cellpadding="0" cellspacing="1" width="98%" id="query" onkeydown="if(event.keyCode == 13){document.getElementById('btnQuery').click();event.returnValue=false;return false;}">
        <tr>
            <td colspan="5" style="border-bottom: 1px solid rgb(204, 204, 204);" height="25">
                查找方式：时间
                <cc1:DatePicker runat="server" ID="txtStartTime" Width="100" />
                -
                <cc1:DatePicker runat="server" ID="txtEndTime" Width="100" />
                &nbsp;&nbsp;
                分类
                <asp:DropDownList runat="server" ID="ddlSupplierType">
                </asp:DropDownList>
                区域
                <asp:DropDownList runat="server" ID="ddlCity">
                </asp:DropDownList>
                关键字：
                <input id="txtKeyWord" name="txtKeyWord" runat="server" type="text" />
                <input id="btnQuery" name="btnQuery" value=" 查 找 " type="button" onclick="QueryHandle()" />
            </td>
        </tr>
    </table>
    <table align="center" border="1" bordercolor="#468fc1" cellpadding="0" cellspacing="0"
        width="98%" id="ExchangList">
        <tr>
            <td width="8%" align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif">
                <input type="checkbox" id="ckAll" /><label for="ckAll">全选</label>
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="8%" height="25">
                时间
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="10%">
                分类
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="10%">
                区域
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="22%">
                主题
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="10%">
                浏览/回复
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="10%">
                发布人
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="12%">
                置顶|删除
            </td>
            <td align="center" background="<%= ImageServerUrl %>/images/yunying/bar_img.gif"
                width="10%">
                审核
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptSupplierList" OnItemCommand="RepeaterList_ItemCommand"
            OnItemDataBound="RepeaterList_ItemDataBound">
            <ItemTemplate>
                <tr name="TrSupplier">
                    <td align="center">
                        <input type="checkbox" name="ckExchangID" value="<%# Eval("ID") %>" /><%# (CurrencyPage-1)*intPageSize+Container.ItemIndex+1 %>
                    </td>
                    <td align="center" height="25">
                        <%# Eval("IssueTime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%# Eval("TopicClassID") %>
                    </td>
                    <td align="center">
                        <%# GetCityNameById(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"ProvinceId"))==0?"":DataBinder.Eval(Container.DataItem,"ProvinceId").ToString())%>
                    </td>
                    <td>
                        <a href="/SupplierManage/SeeSupplier.aspx?ID=<%# Eval("ID") %>" target="_blank">
                            <%# Eval("ExchangeTitle") %></a>
                    </td>
                    <td align="center">
                        <%# Eval("ViewCount") %>/<a target="_blank" href="/SupplierManage/CommentList.aspx?ID=<%# Eval("ID") %>&Type=1"><%# Eval("WriteBackCount") %></a>
                    </td>
                    <td align="center">
                        <%# Eval("OperatorName") %><br>
                        <%# Eval("CompanyName") %>
                    </td>
                    <td align="center">
                        <asp:ImageButton runat="server" ID="ibtnSetTop" ImageUrl='<%# ImageServerUrl + (!string.IsNullOrEmpty(Eval("IsTop").ToString()) && bool.Parse(Eval("IsTop").ToString()) ? "/images/yunying/noheadtopic_1.gif" : "/images/yunying/headtopic_1.gif") %>'
                            AlternateText='<%# !string.IsNullOrEmpty(Eval("IsTop").ToString()) && bool.Parse(Eval("IsTop").ToString()) ? "已置顶" : "未置顶" %>'
                            Width="20" Height="17" ImageAlign="Bottom" CommandName="Top" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() + "," + Eval("IsTop").ToString() %>' />
                        <asp:Button runat="server" ID="btnDel" CommandName="Del" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                            Text="删除" />
                    </td>
                    <td align="center">
                        <asp:Button runat="server" ID="btnCheck" Text='<%# bool.Parse(Eval("IsCheck").ToString())?"取消审核":"马上审核" %>'
                            CommandName="Check" CommandArgument='<%# Eval("ID").ToString()+","+Eval("IsCheck").ToString() %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr runat="server" id="trNoData" visible="false">
            <td align="center" height="100" colspan="9">
                暂无数据
            </td>
        </tr>
        <tr>
            <td colspan="9" id="ckAllTD">
                <asp:Button runat="server" ID="btnCheckAll" Text=" 批 量 审 核 " OnClick="btnCheckAll_Click" />
                <asp:Button runat="server" ID="btnDeleteAll" Text=" 批 量 删 除 " OnClick="btnDeleteAll_Click" />
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

    <script type="text/javascript">
        function mouseovertr(o) {
            $(o).css("background-color","#FFF9E7");
        }
        function mouseouttr(o) {
            $(o).removeAttr("style");
        }

        function QueryHandle() {
            var StartTime = $("#<%= txtStartTime.ClientID %>_dateTextBox").val();
            var EndTime = $("#<%= txtEndTime.ClientID %>_dateTextBox").val();
            var KeyWord = $("#<%= txtKeyWord.ClientID %>").val();
            var SupplierType = $("#<%= ddlSupplierType.ClientID %>").val();
            var CityId = $("#<%= ddlCity.ClientID %>").val();

            location.href = "/SupplierManage/SupplierInfo.aspx?StartTime=" + encodeURI(StartTime) + "&EndTime=" + encodeURI(EndTime) + "&Type=" + SupplierType + "&CityId=" + CityId + "&KeyWord=" + encodeURI(KeyWord);
        }
        $(function() {
        
            $("tr").filter("[name]='TrSupplier'").each(function(){
                $(this).mouseover(function(){
                    $(this).css("background-color","#FFF9E7");
                });
                $(this).mouseout(function(){
                    $(this).removeAttr("style");
                });
            });
            $("#ckAll").click(function() {
                var cked = $(this).attr("checked");
                $("#ExchangList").children().find("input[type]='checkbox'").each(function() {
                    $(this).attr("checked", cked);
                });
            });
            if ($("#ExchangList").children(0).children().length == 2) {
                $("#ckAllTD").hide();
            }
            $("#<%= btnCheckAll.ClientID %>").click(function() {
                var flag = false;
                $(":checkbox[name]='ckExchangID'").each(function() {
                    if ($(this).attr("checked")) {
                        flag = true;
                        return true;
                    }
                });
                if (!flag) {
                    alert("请选择要审核的数据！");
                    return false;
                }
            });
        });
    </script>

    </form>
</body>
</html>
