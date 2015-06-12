<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamConfirm.aspx.cs" MasterPageFile="~/MasterPage/NewPrintPage.Master"
    Inherits="UserBackCenter.PrintPage.TeamConfirm" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainPrint" runat="server">
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
                                        value="checkbox" type="checkbox" name="checkbox" /><lable for="cbkContect">基本信息</lable><br />
                                    <input id="cbkLocalCompany" hidval="tblLocalCompanyInfo" onclick="TourInfoPrintPage.hide(this,'tblLocalCompanyInfo')"
                                        value="checkbox" type="checkbox" name="checkbox" /><lable for="cbkLocalCompany">游客信息</lable><br />
                                    <input id="cbkPriceInfo" hidval="tblTourPriceDetail" onclick="TourInfoPrintPage.hide(this,'tblTourPriceDetail')"
                                        value="checkbox" type="checkbox" name="checkbox" /><lable for="cbkPriceInfo">确认区</lable><br />
                                    <input id="cbkServiceStandard" hidval="tr_ServiceStandard" onclick="TourInfoPrintPage.hide(this,'tr_ServiceStandard')"
                                        value="checkbox" type="checkbox" name="checkbox2" /><lable for="cbkServiceStandard">行程</lable>
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
                <td id="printPage" align="left" style="border-bottom: #c9c9c9 1px solid; border-left: #c9c9c9 1px solid;
                    padding-bottom: 10px; font-size: 12px; border-right: #808080 5px solid">
                    <table width="700" align="center" height="53">
                        <tr>
                            <td>
                                <table height="50" align="center" width="500" valign="middle" class="print_title24">
                                    <tr>
                                        <td align="center">
                                            <span class="HeadTitle">
                                                <asp:Label ID="Zhuanxian" runat="server">
                                                </asp:Label>团队确认单</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="700" align="center">
                        <tbody>
                            <tr id="tr_CollectionContect">
                                <td>
                                    <table width="700" align="center">
                                        <tr>
                                            <td height="31" align="left">
                                                <strong>团队基本信息：</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="ftxt">
                                                <table width="700" class="table_normal2">
                                                    <tr>
                                                        <td width="99" align="right">
                                                            组团社：
                                                        </td>
                                                        <td width="255" align="left">
                                                            <asp:Label ID="TravelName" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="right" nowrap="nowrap">
                                                            专线商：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Zhuanxian1" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            组团联系人：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="ZutuanContact" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                        <td width="83" align="right" nowrap="nowrap">
                                                            专线联系人：
                                                        </td>
                                                        <td width="258" align="left">
                                                            <asp:Label ID="ZhuanxianContact" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            团&nbsp; 号：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="TeamId" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="right" nowrap="nowrap">
                                                            线路名称：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="RouteName" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            出发时间：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="LeaveDate" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="right" nowrap="nowrap">
                                                            人数：
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label runat="server" ID="PersonNum">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="43" align="right" valign="top">
                                                            备注：
                                                        </td>
                                                        <td colspan="3" align="left" valign="top">
                                                            <asp:Label ID="Remark" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tblLocalCompanyInfo">
                                <td>
                                    <table width="700" align="center">
                                        <tr>
                                            <td height="35" align="left">
                                                <strong>游客名单：（可选择是否打印）</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Repeater runat="server" ID="TouristInfomation">
                                                    <ItemTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="lin1">
                                                                    <table width="99%">
                                                                        <tr>
                                                                            <td width="2%" rowspan="2" align="center" valign="top" nowrap="nowrap">
                                                                                <h1 class="line_4_hui">
                                                                                    <%#GetCount() %></h1>
                                                                            </td>
                                                                            <td width="5%" height="25" align="right" nowrap="nowrap">
                                                                                姓名：
                                                                            </td>
                                                                            <td width="7%" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("VisitorName")%>
                                                                            </td>
                                                                            <td width="6%" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("Sex") %>
                                                                                <%#Eval("CradType")%>
                                                                            </td>
                                                                            <td width="14%" align="right" nowrap="nowrap">
                                                                                联系方式：
                                                                            </td>
                                                                            <td align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("Mobile")%>
                                                                            </td>
                                                                            <td width="8%" align="right" nowrap="nowrap">
                                                                                市场价：
                                                                            </td>
                                                                            <td width="18%" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#GetRetailPrice((EyouSoft.Model.TicketStructure.TicketVistorType)Eval("CradType"), TOModel.PersonalPrice, TOModel.ChildPrice)%>
                                                                            </td>
                                                                            <td width="6%" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <span class="kanri_koumoku">结算价：</span>
                                                                            </td>
                                                                            <td width="10%" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <span class="kanri_koumoku">
                                                                                    <%#GetSettlementPrice((EyouSoft.Model.TicketStructure.TicketVistorType)Eval("CradType"), TOModel.SettlementAudltPrice, TOModel.SettlementChildrenPrice)%>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="25" align="right" nowrap="nowrap">
                                                                                <span class="line_1_hui">身份证：</span>
                                                                            </td>
                                                                            <td colspan="2" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("IdentityCard")%>
                                                                            </td>
                                                                            <td align="right" nowrap="nowrap" class="line_1_hui">
                                                                                护照：
                                                                            </td>
                                                                            <td align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("Passport")%>
                                                                            </td>
                                                                            <td align="right" nowrap="nowrap" class="line_1_hui">
                                                                                备注：
                                                                            </td>
                                                                            <td colspan="3" align="left" nowrap="nowrap" class="line_1_hui">
                                                                                <%#Eval("Notes")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="3" colspan="10">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tblTourPriceDetail">
                                <td>
                                    <table width="700" align="center">
                                        <tr>
                                            <td height="35" align="left">
                                                <strong>盖章确认：</strong><strong>（可选择是否打印）</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ftxt">
                                                <table width="700" class="table_normal2">
                                                    <tr>
                                                        <td width="79" rowspan="2" align="right">
                                                            团款：
                                                        </td>
                                                        <td height="35" colspan="3" align="left">
                                                            <strong>市场价合计： <span>
                                                                <input name="SumPrice" type="text" class="inputborder_bot" id="SumPrice" size="16"
                                                                    style="text-align: center" />
                                                            </span>元，结算价合计： <span>
                                                                <input name="SumSettlement" type="text" class="inputborder_bot" id="SumSettlement"
                                                                    size="16" runat="server" style="text-align: center" />
                                                            </span>元 ,返利合计： <span>
                                                                <input name="SumRebate" type="text" class="inputborder_bot" id="SumRebate" size="16"
                                                                    style="text-align: center" />
                                                            </span>元 </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35" colspan="3" align="left">
                                                            <strong>实际汇款： <span>
                                                                <input name="FactRemittance" type="text" class="inputborder_bot" id="FactRemittance"
                                                                    size="16" runat="server" style="text-align: center" />
                                                            </span>元，余款： <span>
                                                                <input name="Balance" type="text" class="inputborder_bot" id="Balance" size="16"
                                                                    runat="server" style="text-align: center" />
                                                            </span>元</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="44" align="right">
                                                            结算方式：
                                                        </td>
                                                        <td colspan="3" align="left">
                                                            请在 <span>
                                                                <input name="RemittanceTime" type="text" onfocus="WdatePicker()" onfocus="WdatePicker(minDate:'%y-%M-#{%d}'})"
                                                                    class="inputborder_bot" id="RemittanceTime" size="10" style="text-align: center" /></span>
                                                            前 打款至我方账户，确定单签名盖章和汇款水单一起回传。待确认到款后， 我方将盖章回传确定单。
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <p>
                                                                组团社银行：</p>
                                                            <p>
                                                                &nbsp;
                                                            </p>
                                                        </td>
                                                        <td width="236" align="left" valign="top">
                                                            <asp:Label runat="server" ID="ztBankInfo">
                                                            </asp:Label>
                                                        </td>
                                                        <td width="88" align="right">
                                                            <p>
                                                                专线商银行：</p>
                                                            <p>
                                                                &nbsp;</p>
                                                        </td>
                                                        <td width="292" height="54" align="left" valign="top">
                                                            <asp:Label ID="zxBankInfo" runat="server">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="133" align="right">
                                                            <p>
                                                                组团社盖章：</p>
                                                            <p>
                                                                <strong>&nbsp;&nbsp;</strong></p>
                                                        </td>
                                                        <td align="right">
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                <%=GetTime() %>&nbsp;</p>
                                                        </td>
                                                        <td align="right">
                                                            <p>
                                                                专线商盖章：</p>
                                                            <p>
                                                                &nbsp;</p>
                                                        </td>
                                                        <td align="right">
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                &nbsp;</p>
                                                            <p>
                                                                <%=GetTime() %>&nbsp;</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="tr_ServiceStandard">
                                <td>
                                    <table width="700" align="center">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td height="35" align="left">
                                                            <strong>行程信息及相关</strong> <strong>：（可选择是否打印）</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ftxt">
                                                            <table width="700" class="table_normal2">
                                                                <tr>
                                                                    <td width="88" align="center">
                                                                        行<br />
                                                                        程
                                                                    </td>
                                                                    <%if (isStandard)
                                                                      { %>
                                                                    <td width="612" align="left">
                                                                        <asp:Repeater runat="server" ID="richeng">
                                                                            <HeaderTemplate>
                                                                                <table width="610" class="table_normal2" align="center">
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
                                                                                    <td bgcolor="#ffffff" rowspan="2">
                                                                                        <div align="center">
                                                                                            <strong>D<%#Eval("PlanDay")%></strong></div>
                                                                                    </td>
                                                                                    <td bgcolor="#ffffff">
                                                                                        <strong>
                                                                                            <%#Eval("PlanInterval")%><%#Eval("Vehicle").ToString()%></strong>
                                                                                    </td>
                                                                                    <td bgcolor="#ffffff">
                                                                                        <%#Eval("House")%>
                                                                                    </td>
                                                                                    <td bgcolor="#ffffff">
                                                                                        <div align="center">
                                                                                            <%#(bool)Eval("Early") == false ? "※" :"早"%>
                                                                                            <%#(bool)Eval("Center")==false ? "※": "中"%>
                                                                                            <%#(bool)Eval("Late") == false ? "※" : "晚"%>
                                                                                            <%--※※※--%>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td bgcolor="#ffffff" colspan="3">
                                                                                       <%#Eval("PlanContent")!=null? Utils.TextToHtml(Eval("PlanContent").ToString()):""%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </td>
                                                                    <%}
                                                                      else
                                                                      { %>
                                                                    <td width="683" align="left">
                                                                        <asp:Label runat="server" ID="FastStandard"></asp:Label>
                                                                    </td>
                                                                    <%} %>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        报<br />
                                                                        价<br />
                                                                        包<br />
                                                                        含
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="Containes"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        报<br />
                                                                        价<br />
                                                                        不<br />
                                                                        含
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label runat="server" ID="NoContaines"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript">
        var TourInfoPrintPage = {
            hide: function(obj, containerId) {
                if (obj.checked) {
                    $("#" + containerId).hide()
                } else {
                    $("#" + containerId).show()
                }
            }
        }
    </script>

</asp:Content>
