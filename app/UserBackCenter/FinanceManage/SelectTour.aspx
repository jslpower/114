<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="SelectTour.aspx.cs" Inherits="UserBackCenter.FinanceManage.SelectTour" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
            <tr>
                <td width="24" valign="top">
                    <img src="<%=ImageServerUrl %>/images/searchico2.gif" width="23" height="24" />
                </td>
                <td align="left">
                    线路区域
                    <asp:DropDownList runat="server" ID="dplArea">
                    </asp:DropDownList>
                    团号
                    <input name="TourNumber" runat="server" id="txtTourNumber" type="text" size="15" />
                    线路名称
                    <input name="RouteName" runat="server" id="txtRouteName" type="text" size="15" />
                    出团时间
                    <input name="BeginDate" runat="server" onfocus="WdatePicker()" id="txtBeginDate"
                        type="text" style=" width:70px" />
                    至
                    <input name="EndDate" runat="server" onfocus="WdatePicker()" id="txtEndDate" type="text"
                        style=" width:70px" />
                    <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="Top" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" id="tbl_AccountsReceivable" border="1" cellpadding="2" cellspacing="0"
            bordercolor="#B9D3E7" class="zttype">
            <tr>
                <th width="8%">
                    &nbsp;
                </th>
                <th width="12%" align="center">
                    团号
                </th>
                <th width="52%" align="center">
                    线路名称
                </th>
                <th width="12%" align="center">
                    发团时间
                </th>
                <th width="12%" align="center">
                    线路区域
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_SelectTour" OnItemDataBound="rpt_SelectTour_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            &nbsp;
                            <input type="radio" name="SelectTour" TourNo="<%#Eval("TourNo")%>" id="SelectTour<%#Eval("id")%>" value="<%#Eval("id")%>" />
                            <label for="SelectTour<%#Eval("id")%>">
                                <asp:Literal ID="ltrXH" runat="server"></asp:Literal></label>
                        </td>
                        <td align="center">
                            <%#Eval("TourNo")%>                            
                        </td>
                        <td align="left">
                            <a href="/PrintPage/TeamInformationPrintPage.aspx?TourID=<%#Eval("id")%>" id="RouteName<%#Eval("id")%>"
                                target="_blank">
                                <%#Eval("RouteName")%></a>
                        </td>
                        <td align="center" class="tbline" id="LeaveDate<%#Eval("id")%>">
                            <%#Eval("LeaveDate","{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="center">
                            <%#Eval("AreaName")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr runat="server" id="NoData" visible="false">
                <td colspan="5" align="center">
                    对不起，没有找到你要的团队信息！
                </td>
            </tr>
            <tr>
                <td id="SelectTour_ExportPage" colspan="5">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
                        runat="server"></cc2:ExportPageInfo>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <input type="button" id="btnSelected" value="选 择" />&nbsp;&nbsp;
                    <input type="button" id="btnClose" value="关 闭" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript">
        var SelectTour = {
            needId: '<%=Request.QueryString["NeedId"] %>',
            iframeId: '<%=Request.QueryString["iframeId"] %>',
            selected: function() {
                var count = 0;                
                $("input[name='SelectTour']").each(function() {
                    if (this.checked) {
                        var tourId = $.trim($(this).val());
                        var tourNo = $.trim($(this).attr("tourNo"));
                        var RouteName = $.trim($("#RouteName" + tourId).text());
                        var LeaveDate = $.trim($("#LeaveDate" + tourId).text());
                        var parentWin = parent.document.getElementById(SelectTour.needId).contentWindow;                        
                        parentWin.document.getElementById("hidTourId").value = tourId;
                        parentWin.document.getElementById("txtTourNo").value = tourNo;
                        parentWin.document.getElementById("txtRouteName").value = RouteName;
                        parentWin.document.getElementById("txtLeaveDate").value = LeaveDate;
                        count = count + 1;
                    }
                })
                if (count > 0) {
                    SelectTour.close();
                } else { 
                    alert("对不起，请选择线路信息！")
                }
            },
            close: function() {
                parent.Boxy.getIframeDialog(SelectTour.iframeId).hide();
            }
        }
        $(document).ready(function() {
            $("#btnSelected").click(function() {
                SelectTour.selected();
            });
            $("#btnClose").click(function() {
                SelectTour.close();
            });
        });

    </script>

    </form>
</body>
</html>
