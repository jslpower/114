<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/NewPrintPage.Master"
    CodeBehind="LineTourInfo.aspx.cs" Inherits="UserBackCenter.PrintPage.LineTourInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="MainPrint" runat="server">
    <table width="940" align="left" class="table_noneborder">
        <tbody>
            <tr>
                <td valign="top" rowspan="2" width="202" align="right">
                    <table style="margin-top: 90px" width="83%" align="right" id="tblFloating">
                        <tbody>
                            <tr>
                                <td style="color: #999999" align="left">
                                    勾选复选框隐藏指定项目<br>
                                    打勾为隐藏，不打勾为显示
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #999999" align="left">
                                    <input id="cbkContect" hidval="tr_CollectionContect" onclick="TourInfoPrintPage.hide(this,'tr_CollectionContect')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <lable for="cbkContect">联系方式</lable>
                                    <br />
                                    <input id="cbkLocalCompany" hidval="tblLocalCompanyInfo" onclick="TourInfoPrintPage.hide(this,'tblLocalCompanyInfo')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <lable for="cbkLocalCompany">出发时间和价格</lable>
                                    <br />
                                    <input id="cbkPriceInfo" hidval="tblTourPriceDetail" onclick="TourInfoPrintPage.hide(this,'tblTourPriceDetail')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <lable for="cbkPriceInfo">行程特色</lable>
                                    <br />
                                    <input id="cbkServiceStandard" hidval="tr_ServiceStandard" onclick="TourInfoPrintPage.hide(this,'tr_ServiceStandard')"
                                        value="checkbox" type="checkbox" name="checkbox2" />
                                    <lable for="cbkServiceStandard">服务标准</lable>
                                    <br />
                                    <input id="cbkContent" hidval="tr_Content" onclick="TourInfoPrintPage.hide(this,'tr_Content')"
                                        value="checkbox" type="checkbox" name="checkbox2" />
                                    <lable for="cbkContent">银行信息</lable>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="738">
                    <table id="tbltopright" width="100%">
                        <tbody>
                            <tr>
                                <td width="2%" align="left">
                                    <img src="<%= ImageServerUrl %>/images/printboxt-l.gif" width="16" height="37">
                                </td>
                                <td background="<%= ImageServerUrl %>/images/printboxt-m.gif" width="98%">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" id="printPage" style="padding-bottom: 10px; font-size: 12px; border-left: #c9c9c9 1px solid;
                    border-right: #808080 5px solid">
                    <table width="700" align="center">
                        <tbody>
                            <tr>
                                <td align="right">
                                    <strong>打印时间：
                                        <asp:Label ID="LabGetTime" runat="server"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:PlaceHolder runat="server" ID="plhContact">
                        <table width="700" align="center">
                            <tr id="tr_CollectionContect">
                                <td>
                                    <table width="700" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="342" align="center" class="print_title24">
                                                    <span class="HeadTitle"><strong>
                                                        <asp:Label ID="TravelName" runat="server">
                                                        </asp:Label>
                                                    </strong></span>
                                                </td>
                                                <td width="351" align="right" nowrap>
                                                    <%= TravelLogo %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <strong>地址：</strong><asp:Label ID="TravelAddress" runat="server"></asp:Label>
                                                    <strong>联系人：</strong><asp:Label ID="Contact" runat="server"></asp:Label>
                                                    <strong>传真：</strong><asp:Label ID="Fax" runat="server"></asp:Label>
                                                    <strong>电话：</strong><asp:Label ID="Phone" runat="server"></asp:Label>
                                                    <strong>手机：</strong><asp:Label ID="Mobile" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                    <table width="700" align="center">
                        <tbody>
                            <tr>
                                <td width="686" height="35" colspan="3">
                                    <div align="center">
                                        <a href='<%= EyouSoft.Common.Domain.UserPublicCenter + "/tour_" + EyouSoft.Common.Utils.GetQueryStringValue("RouteId") %>' target="_blank"><strong>
                                            <asp:Label ID="RouteName" runat="server"></asp:Label>
                                            【行程单】</strong></a></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="700" align="center">
                        <tbody>
                            <tr id="tblLocalCompanyInfo">
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="118" align="right">
                                                    线路名称：
                                                </td>
                                                <td width="178">
                                                    <asp:Label ID="RouteName1" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    主 题：
                                                </td>
                                                <td>
                                                    <asp:Label ID="Themes" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    出发交通和城市：
                                                </td>
                                                <td>
                                                    <asp:Label ID="TrafficandCity" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    天数：
                                                </td>
                                                <td>
                                                    <asp:Label ID="Dates" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    返程交通和城市：
                                                </td>
                                                <td>
                                                    <asp:Label ID="TrafficandBackCity" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    报名截止时间：
                                                </td>
                                                <td>
                                                    <asp:Label ID="ApplyEndDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    主要游览地区：
                                                </td>
                                                <td>
                                                    <asp:Label ID="MainTourArea" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    市场价：
                                                </td>
                                                <td>
                                                    <span>
                                                        <input name="Price" type="text" id="Price" size="6" runat="server" style="width: 100px;" /></span>元/人
                                                </td>
                                            </tr>
                                            <%if (isInternational)
                                              { %>
                                            <tr>
                                                <td align="right">
                                                    签证区域：
                                                </td>
                                                <td>
                                                    <asp:Label ID="VisaArea" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    定金：
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="ltrPrice"></asp:Literal>
                                                </td>
                                            </tr>
                                            <%} %>
                                            <%if (isShow)
                                              { %>
                                            <tr>
                                                <td align="right">
                                                    出团计划：
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="GroupPlan" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <%} %>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left" id="tblTourPriceDetail">
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tbody>
                                            <tr>
                                                <td width="30">
                                                    <div align="center">
                                                        <strong>特<br />
                                                            色</strong></div>
                                                </td>
                                                <td width="663">
                                                    <asp:Label ID="RouteFeatures" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <%if (isStandard)
                              { %>
                            <tr>
                                <td>
                                    <asp:Repeater runat="server" ID="richeng">
                                        <HeaderTemplate>
                                            <table width="700" class="table_normal2" align="center">
                                                <tr>
                                                    <td width="30" height="20" align="center" nowrap bgcolor="#F2F9FE">
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
                                                <td rowspan="2">
                                                    <div align="center">
                                                        <strong>D<%#Eval("PlanDay")%></strong></div>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <%#Eval("PlanInterval")%><%#Eval("Vehicle").ToString()%></strong>
                                                </td>
                                                <td>
                                                    <%#Eval("House")%>
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <%#(bool)Eval("Early") == false ? "※" :"早"%>
                                                        <%#(bool)Eval("Center")==false ? "※": "中"%>
                                                        <%#(bool)Eval("Late") == false ? "※" : "晚"%>
                                                        <%--※※※--%>
                                                    </div>
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
                                </td>
                            </tr>
                            <%}
                              else
                              { %>
                            <tr>
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tr>
                                            <td width="30">
                                                <strong>日程</strong>
                                            </td>
                                            <td width="663">
                                                <asp:Label runat="server" ID="FastisStandard"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%} %>
                            <tr id="tr_ServiceStandard">
                                <td align="left" class="td_noneborder">
                                    <table width="700" align="center" class="table_normal2">
                                        <tbody>
                                            <tr>
                                                <td rowspan="7" width="30" id="td_rowspan">
                                                    <div align="center">
                                                        <strong>服<br />
                                                            <br />
                                                            务<br />
                                                            <br />
                                                            标<br />
                                                            <br />
                                                            准</strong></div>
                                                </td>
                                                <td width="37" align="center">
                                                    包含
                                                </td>
                                                <td width="623">
                                                    <asp:Label ID="Containers" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    不含
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="NoContainers" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    儿童
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Children" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    赠送
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Gift" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    购物
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Shopping" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    自费
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="OwnExpense" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="37" align="center">
                                                    备注
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Remark" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr_Content">
                                <td align="left">
                                    <table width="700" align="center" class="table_normal2">
                                        <tbody>
                                            <tr>
                                                <td height="5" colspan="4" bgcolor="#cccccc">
                                                    <div align="center">
                                                        <strong>银行信息</strong></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" align="center">
                                                    <strong>公司账户:</strong>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="CompanyAcount" runat="server"></asp:Label></strong>
                                                </td>
                                                <td width="100" align="center">
                                                    <strong>个人账户:</strong>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="PersonerAcount" runat="server"></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <strong>支付宝:</strong>
                                                </td>
                                                <td colspan="3">
                                                    <strong>
                                                        <asp:Label ID="Alipay" runat="server"></asp:Label></strong>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>

    <script language="javascript" type="text/javascript">
        var TourInfoPrintPage = {
            hide: function(obj, containerId) {
                if (obj.checked) {
                    $("#" + containerId).hide()
                } else {
                    $("#" + containerId).show()
                }
            },
            TrCount: 7
        }
        $(function() {
            if ($.trim($("#<%=Containers.ClientID %>").text()) == "") {  //如果该行内容为空，则不显示该行
                $("#<%=Containers.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=NoContainers.ClientID %>").text()) == "") {
                $("#<%=NoContainers.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=Children.ClientID %>").text()) == "") {
                $("#<%=Children.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=Gift.ClientID %>").text()) == "") {
                $("#<%=Gift.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=Shopping.ClientID %>").text()) == "") {
                $("#<%=Shopping.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=OwnExpense.ClientID %>").text()) == "") {
                $("#<%=OwnExpense.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            if ($.trim($("#<%=Remark.ClientID %>").text()) == "") {
                $("#<%=Remark.ClientID %>").closest("tr").remove();
                TourInfoPrintPage.TrCount--;
            }
            $("#td_rowspan").attr("rowspan", TourInfoPrintPage.TrCount);
        })

       
    </script>

</asp:Content>
