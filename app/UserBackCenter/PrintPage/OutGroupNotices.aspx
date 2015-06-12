<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/NewPrintPage.Master"
    CodeBehind="OutGroupNotices.aspx.cs" Inherits="UserBackCenter.PrintPage.OutGroupNotices" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="MainPrint" runat="server">
    <table width="940" align="left" class="table_noneborder">
        <tbody>
            <tr>
                <td valign="top" rowspan="3" width="202" align="right">
                    <table style="margin-top: 90px" width="83%" align="right" id="tblFloating">
                        <tbody>
                            <tr>
                                <td style="color: #999999" align="left">
                                    勾选复选框隐藏指定项目<br />
                                    打勾为隐藏，不打勾为显示
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #999999" align="left">
                                    <input id="cbkContect" hidval="tr_CollectionContect" onclick="TourInfoPrintPage.hide(this,'tr_CollectionContect')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <label for="cbkContect">
                                        联系方式</label><br />
                                    <input id="cbkLocalCompany" hidval="tblLocalCompanyInfo" onclick="TourInfoPrintPage.hide(this,'tblLocalCompanyInfo')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <label for="cbkLocalCompany">
                                        基本信息</label><br />
                                    <input id="cbkPriceInfo" hidval="tblTourPriceDetail" onclick="TourInfoPrintPage.hide(this,'tblTourPriceDetail')"
                                        value="checkbox" type="checkbox" name="checkbox" />
                                    <label for="cbkPriceInfo">
                                        行程</label><br />
                                    <input id="cbkServiceStandard" hidval="tr_ServiceStandard" onclick="TourInfoPrintPage.hide(this,'tr_ServiceStandard')"
                                        value="checkbox" type="checkbox" name="checkbox2" />
                                    <label for="cbkServiceStandard">
                                        服务标准</label>
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
            <tr style="margin-top: 10px;">
                <td align="left" id="printPage" style="border-bottom: #c9c9c9 1px solid; border-left: #c9c9c9 1px solid;
                    padding-bottom: 10px; font-size: 12px; border-right: #808080 5px solid">
                    <table width="700" height="83" align="center">
                        <tr>
                            <td height="50" colspan="2" align="center" valign="middle" class="print_title24">
                                <span class="HeadTitle">
                                    <asp:Label runat="server" ID="zutuan"></asp:Label>
                                    出团通知书&nbsp;</span>
                            </td>
                        </tr>
                        <tr>
                            <td width="351" align="left">
                                <asp:Label runat="server" ID="TravelName"></asp:Label>
                            </td>
                            <td width="339" align="right">
                                <strong>打印时间：<asp:Label runat="server" ID="LabGetTime"></asp:Label></strong>
                            </td>
                        </tr>
                    </table>
                    <asp:PlaceHolder runat="server" ID="plhContact">
                        <table width="700" align="center">
                            <tbody>
                                <tr id="tr_CollectionContect">
                                    <td>
                                        <strong>联系人：</strong><asp:Label runat="server" ID="Contact"></asp:Label>
                                        <strong>电话：</strong>
                                        <asp:Label runat="server" ID="Tel"></asp:Label>
                                        <strong>传真：</strong><asp:Label runat="server" ID="Fax"></asp:Label>
                                        <strong>地址：</strong><asp:Label runat="server" ID="Address"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:PlaceHolder>
                    <table align="center">
                        <tbody>
                            <tr>
                                <td width="686" height="35" colspan="3">
                                    <div align="center">
                                        <strong>
                                            <asp:Label runat="server" ID="RouteName"></asp:Label>【行程单】</strong></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="700" align="center">
                        <tbody>
                            <tr id="tblLocalCompanyInfo">
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tr>
                                            <td width="343">
                                                团 号：<asp:Label ID="GroupNo" runat="server"></asp:Label>
                                            </td>
                                            <td width="350">
                                                全陪领队：<asp:Label runat="server" ID="DescriptionLeader"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                出发交通：<asp:Label runat="server" ID="LeaveTraffic"></asp:Label>
                                            </td>
                                            <td>
                                                出发日期：<asp:Label runat="server" ID="LeaveDate"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                返程交通：<asp:Label runat="server" ID="BackTraffic"></asp:Label>
                                            </td>
                                            <td>
                                                返抵达日：<asp:Label runat="server" ID="BackDate"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                集合地点：<asp:Label runat="server" ID="CollectionAddress"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="30" colspan="2">
                                                <strong>成人价：￥<span><input type="text" runat="server" id="AdultPrice" size="10" /></span>
                                                    元 儿童价：￥<span><input type="text" runat="server" id="ChildPrice" size="10" /></span>
                                                    元 单房差：￥<span><input type="text" runat="server" id="danfangcha" size="10" /></span>
                                                    元 </strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%if (isStandard)
                              { %>
                            <tr align="left" id="tblTourPriceDetail">
                                <td bg color="#ffffff">
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
                                                <td rowspan="2" align="center" bgcolor="#F2F9FE">
                                                    <strong>
                                                        <%#Eval("PlanDay")%></strong>
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
                                                <td colspan="3">
                                                    <%#Eval("PlanContent")!=null?Utils.TextToHtml(Eval("PlanContent").ToString()):""%>
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
                              {  %>
                            <tr align="left" id="tblTourPriceDetail">
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tr>
                                            <td width="30">
                                                <strong>日程</strong>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="FastisStandard"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%} %>
                            <tr id="tr_ServiceStandard">
                                <td>
                                    <table width="700" class="table_normal2" align="center">
                                        <tbody>
                                            <tr>
                                                <td id="td_rowspan" rowspan="7" width="30">
                                                    <div align="center">
                                                        <strong>服<br />
                                                            <br />
                                                            务<br />
                                                            <br />
                                                            标<br />
                                                            <br />
                                                            准</strong></div>
                                                </td>
                                                <td align="center" width="37">
                                                    包含
                                                </td>
                                                <td width="623">
                                                    <asp:Label ID="Containers" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="37">
                                                    不含
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="NoContainers" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    儿童
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Children" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    赠送
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Gift" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    购物
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Shopping" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    自费
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="OwnExpense" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
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
        $(document).ready(function() {
            if ($.trim($("#<%=Containers.ClientID %>").text()) == "") {  //如果该行内容为空，则不显示该行
                $("#<%=Containers.ClientID %>").closest().remove();
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
        });
       
    </script>

</asp:Content>
