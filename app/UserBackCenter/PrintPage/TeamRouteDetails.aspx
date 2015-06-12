<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamRouteDetails.aspx.cs"
    Inherits="UserBackCenter.PrintPage.TeamRouteDetails" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团队线路详细</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("right") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src='<%=JsManage.GetJsFilePath("jquery") %>'></script>

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
                <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%=TeamId %>' class="basic_btn" target="_blank">
                    <span>打印</span></a>
                <%if (IsShow)
                  { %>
                <%if (IsTourAgency || (IsRouteAgency && IsTourAgency))
                  { %>
                <a href="/Order/OrderByTour.aspx?tourID=<%=TeamId %>" class="basic_btn"><span>预订</span></a>
                <%} %>
                <%if (!IsTourAgency && IsRouteAgency)
                  { %>
                <a href="/Order/RouteAgency/AddOrderByRoute.aspx?tourID=<%=TeamId %>" class="basic_btn">
                    <span>代订</span></a><%}
                  } %>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc">
        <tr>
            <td width="150" align="right" bgcolor="#F2F9FE">
                团号：
            </td>
            <td align="left" width="250">
                <asp:Label ID="TourNo" runat="server"></asp:Label>
            </td>
            <td width="160" align="right" bgcolor="#F2F9FE">
                状态：
            </td>
            <td align="left">
                <asp:Label ID="Statue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发时间：
            </td>
            <td align="left">
                <asp:Label ID="BeginDate" runat="server"></asp:Label>
            </td>
            <td align="right" bgcolor="#F2F9FE">
                报名截止：
            </td>
            <td align="left">
                <asp:Label ID="EndDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                <input type="checkbox" name="checkbox" id="jiesuan" onclick="ShoworHide()">
                <lable for="jiesuan"> 结算价</lable>
                成人：
            </td>
            <td align="left">
                <strong class="ff0000">市场价:
                    <asp:Label ID="AdultPrice" runat="server"></asp:Label>
                    <span class="jiesuanjia">
                        <label>
                            结算价:</label>
                        <asp:Label ID="AdultSettlementPrice" class="jiesuanjia" runat="server"></asp:Label></strong></span>
            </td>
            <td align="right" bgcolor="#F2F9FE">
                儿童：
            </td>
            <td align="left">
                <strong class="ff0000">市场价:
                    <asp:Label ID="ChildPrice" runat="server"></asp:Label>
                    <span class="jiesuanjia">
                        <label>
                            结算价:</label>
                        <asp:Label ID="ChildSettlementPrice" class="jiesuanjia" runat="server"></asp:Label></span>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                单房差：
            </td>
            <td align="left">
                <asp:Label ID="danfangcha" runat="server"></asp:Label>
            </td>
            <td align="right" bgcolor="#F2F9FE">
                团队：
            </td>
            <td align="left">
                <asp:Label ID="Team" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                出发班次时间：
            </td>
            <td align="left">
                <asp:Label ID="GoTime" runat="server"></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" bgcolor="#F2F9FE">
                返回班次时间：
            </td>
            <td align="left">
                <asp:Label ID="BackTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                集合说明：
            </td>
            <td align="left">
                <asp:Label ID="CollectionDescription" runat="server"></asp:Label>
            </td>
            <td align="right" bgcolor="#F2F9FE">
                领队全陪说明：
            </td>
            <td align="left">
                <asp:Label ID="DescriptionLeader" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap" bgcolor="#f2f9fe">
                销售商须知：
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="SellerNotice" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap" bgcolor="#f2f9fe">
                团队备注：
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="TeamRemark" runat="server"></asp:Label>
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
                <%--获取MQ--%>
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
                成人 <span class="ff0000">
                    <asp:Label ID="Adult" runat="server"></asp:Label></span> 元 儿童 <span class="ff0000">
                        <asp:Label ID="Child" runat="server"></asp:Label></span> 元&nbsp;&nbsp;&nbsp;
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
      { %>
    <div style="width: 900px; margin: 0 auto;">
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
                        <%#Eval("PlanContent")!=null? Utils.TextToHtml(Eval("PlanContent").ToString()):""%>
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
    <table width="900" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#9dc4dc"
        class="padd5">
        <tr>
            <td width="150" align="center" bgcolor="#F2F9FE">
                <strong>日程</strong>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="FastStandard"></asp:Label>
            </td>
        </tr>
    </table>
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
            <td align="center" width="50" bgcolor="#F2F9FE">
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
    </form>

    <script type="text/javascript">
        function ShoworHide() {
            //alert($("#jiesuan").attr("checked"));
            if ($("#jiesuan").attr("checked")) {
                $(".jiesuanjia").show();
            }
            else {
                $(".jiesuanjia").hide();
            }
        }
        $(function() {
            $(".jiesuanjia").hide();
            if ($.trim($("#<%=Containers.ClientID %>").text()) == "") {  //如果该行内容为空，则不显示该行
                $("#<%=Containers.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=NoContainers.ClientID %>").text()) == "") {
                $("#<%=NoContainers.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=Children.ClientID %>").text()) == "") {
                $("#<%=Children.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=Gift.ClientID %>").text()) == "") {
                $("#<%=Gift.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=Shopping.ClientID %>").text()) == "") {
                $("#<%=Shopping.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=OwnExpense.ClientID %>").text()) == "") {
                $("#<%=OwnExpense.ClientID %>").closest("tr").remove();
            }
            if ($.trim($("#<%=Remark.ClientID %>").text()) == "") {
                $("#<%=Remark.ClientID %>").closest("tr").remove();
            }

        })
        
    </script>

</body>
</html>
