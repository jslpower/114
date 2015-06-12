<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteDetail.aspx.cs" Inherits="UserBackCenter.PrintPage.RouteDetail" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>线路详细</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("right") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="hr_10">
    </div>
    <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="50" align="center" class="print_title24">
                <asp:Label ID="RouteName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <a href='/PrintPage/LineTourInfo.aspx?RouteId=<%=RouteId %>' class="basic_btn"><span>
                    打印</span></a>
                <input type="hidden" id="RouteId" value="<%=RouteId %>" />
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc">
        <tr>
            <td width="150" align="right" bgcolor="#f2f9fe">
                供应商：
            </td>
            <td width="250" align="left">
                <a href='<%=ShopURL%>' target="_blank">
                    <asp:Label ID="CompanyName" runat="server"></asp:Label></a>
                <%=Mq %>
            </td>
            <td width="160" align="right" bgcolor="#F2F9FE">
                专线类型：
            </td>
            <td width="306" align="left">
                <asp:Label ID="RouteType" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发交通和城市：
            </td>
            <td align="left">
                <asp:Label ID="StartTraffic" runat="server"></asp:Label>
            </td>
            <td align="right" bgcolor="#F2F9FE">
                线路主题：
            </td>
            <td align="left">
                <asp:Label ID="RouteTheme" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                返回交通和城市：
            </td>
            <td align="left">
                <asp:Label ID="EndTraffic" runat="server"></asp:Label>
            </td>
            <td align="right" bgcolor="#f2f9fe">
                主要游览地区：
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="MainTourArea" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                天数：
            </td>
            <td align="left">
                <span class="ff0000">
                    <asp:Label ID="Day" runat="server"></asp:Label></span> 天 <span class="ff0000">
                        <asp:Label ID="Night" runat="server"></asp:Label>
                    </span>晚
            </td>
            <td align="right" bgcolor="#F2F9FE">
                独立成团：
            </td>
            <td align="left">
                最小成团人数 <span class="ff0000">
                    <asp:Label ID="Min" runat="server"></asp:Label></span> 团队参考价格： <span class="ff0000">
                        <asp:Label ID="TeamPrice" runat="server"></asp:Label></span>
            </td>
        </tr>
        <%if (isInternational)
          { %>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                线路定金：
            </td>
            <td align="left">
                <asp:Literal runat="server" ID="ltrPrice"></asp:Literal> 
            </td>
            <td align="right" bgcolor="#F2F9FE">
                签证地区：
            </td>
            <td align="left">
                <asp:Label ID="VisaArea" runat="server"></asp:Label>
            </td>
        </tr>
        <%} %>
    </table>
    <div class="hr_10">
    </div>
    <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc">
        <tr>
            <td width="150" align="right" bgcolor="#F2F9FE">
                线路特色：
            </td>
            <td align="left">
                <asp:Label ID="RouteFeatures" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <%if (isStandard)
      {%>
    <div style="width: 900px; margin: 0 auto;" id="standardTable">
        <asp:Repeater runat="server" ID="richeng">
            <HeaderTemplate>
                <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
                    class="padd5">
                    <tr>
                        <td width="50" height="20" align="center" nowrap bgcolor="#F2F9FE">
                            <strong>日程</strong>
                        </td>
                        <td height="20" bgcolor="#F2F9FE">
                            <strong>行程安排</strong>
                        </td>
                        <td height="20" bgcolor="#F2F9FE">
                            <strong>酒店</strong>
                        </td>
                        <td height="20" bgcolor="#F2F9FE">
                            <strong>餐食</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td rowspan="2" align="center" bgcolor="#F2F9FE">
                        <strong>D<%#Eval("PlanDay")%></strong>
                    </td>
                    <td align="center">
                        <strong>
                            <%#Eval("PlanInterval")%><%#Eval("Vehicle").ToString()%></strong>
                    </td>
                    <td>
                        <%#Eval("House")%>
                    </td>
                    <td>
                        <%#(bool)Eval("Early") == false ? "※" :"早"%>
                        <%#(bool)Eval("Center")==false ? "※": "中"%>
                        <%#(bool)Eval("Late") == false ? "※" : "晚"%>
                        <%--※※※--%>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <%#Eval("PlanContent")!=null?Utils.TextToHtml(Eval("PlanContent").ToString()):""%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <%}
      else
      { %>
    <div style="width: 900px; margin: 0 auto;" id="quickTable">
        <table width="900" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
            class="padd5">
            <tr>
                <td width="150" align="right" bgcolor="#f2f9fe">
                    日程安排：
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="QuickStandard"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <%} %>
    <div class="hr_10">
    </div>
    <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
        class="padd5">
        <tr>
            <td width="50" align="center" bgcolor="#F2F9FE">
                <strong>包含</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Containers" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>不含</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="NoContainers" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>儿童</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Children" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>赠送</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Gift" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>购物</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Shopping" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>自费</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="OwnExpense" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#F2F9FE">
                <strong>备注</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="Remark" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <%if (isShow)
      { %>
    <asp:Repeater runat="server" ID="TourList">
        <HeaderTemplate>
            <table width="900" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc" class="liststyle" style="margin-top:1px;">
                <tr class="list_basicbg">
                    <th nowrap="nowrap" class="list_basicbg">
                        团号
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        出团日期
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        报名截止
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        人数
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        余位
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        状态
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        成人价
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        儿童价
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        单房差
                    </th>
                    <th nowrap="nowrap" class="list_basicbg">
                        操作
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <a href='/PrintPage/TeamRouteDetails.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">
                        <%#Eval("TourNo")%></a>
                </td>
                <td>
                    <%#Convert.ToDateTime(Eval("LeaveDate")).ToString("yyyy-MM-dd")%>
                </td>
                <td>
                    <%# Convert.ToDateTime(Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>
                </td>
                <td>
                    <%#Eval("TourNum")%>
                </td>
                <td>
                    <%#Eval("MoreThan")%>
                </td>
                <td>
                    <%#Eval("PowderTourStatus").ToString()%>
                </td>
                <td>
                    <%#Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                </td>
                <td>
                    <%#Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                </td>
                <td>
                    <%#Utils.FilterEndOfTheZeroString( Eval("MarketPrice").ToString())%>
                </td>
                <td>
                    <%#GetUrl(Eval("TourId").ToString(), Convert.ToDateTime(Eval("LeaveDate")))%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <table id="Table1" cellspacing="0" cellpadding="4" width="900" align="center" border="0">
        <tr>
            <td>
                <div id="RouteDetail_ExportPage" class="F2Back" style="text-align: right;" height="40">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="6"
                        runat="server"></cc2:ExportPageInfo>
                </div>
            </td>
        </tr>
    </table>
    <%} %>
    </form>
</body>
</html>
