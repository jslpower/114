<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsReceived.aspx.cs"
    Inherits="UserBackCenter.FinanceManage.AccountsReceived" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/FinancialManagement/FinancialManageTab.ascx" TagName="FinancialManageTab"
    TagPrefix="uc1" %>
<asp:content id="AccountsReceivable" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <table width="100%" id="tbl_AccountsReceived" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <uc1:FinancialManageTab ID="FinancialManageTab2" TabIndex="1" runat="server" />
                <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                    class="zttype">
                    <tr>
                        <th width="6%" align="center">
                            团号
                        </th>
                        <th width="22%" align="center">
                            线路名称
                        </th>
                        <th width="9%" align="center">
                            发团时间
                        </th>
                        <th width="6%" align="center">
                            订单号
                        </th>
                        <th width="16%" align="center">
                            组团社
                        </th>
                        <th width="6%" align="center">
                            人数
                        </th>
                        <th width="6%" align="center">
                            下单人
                        </th>
                        <th width="8%" align="center">
                            合同金额
                        </th>
                        <th width="8%" align="center">
                            清算日期
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAccountsReceived">
                        <ItemTemplate>
                            <tr >
                                <td align="center">
                                    <%#Eval("TourNo")%>
                                </td>
                                <td align="left">
                                    &nbsp;<a href='<%# Eval("TourId").ToString().Trim()=="0"?"javascript:;":string.Format("/PrintPage/TeamInformationPrintPage.aspx?TourID={0}",Eval("TourId")) %>' target='<%# Eval("TourId").ToString().Trim()=="0"?"":"_blank" %>'><%#Eval("RouteName")%></a>
                                </td>
                                <td align="center" class="tbline">
                                    <%#Eval("LeaveDate", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderNo")%>
                                </td>
                                <td align="center">
                                     <%#Eval("RetailersName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PeopleNum")%>
                                </td>
                                <td align="center">
                                   <%#Eval("OperatorName")%><br />
                                   <%#EyouSoft.Common.Utils.GetMQ(Eval("OperatorMQ").ToString())%>                                        
                                </td>
                                <td align="center">
                                    ￥<%#Eval("SumPrice","{0:F2}")%>
                                </td>
                                <td align="center">
                                    <span class="tbline"><%#Eval("ClearTime", "{0:yyyy-MM-dd}")%></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>  
                <tr runat="server" id="NoData" visible="false">
                    <td colspan="9" align="center" style=" padding:6px;">
                        对不起，没有你要找的已收帐款信息！
                    </td>    
                </tr>                                
                </table>
                 <div  id="AccountsReceived_ExportPage"  class="F2Back" style="text-align:right;" >
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
            </div>
            </td>
        </tr>
    </table>
   <script language="javascript" type="text/javascript">
       var AccountsReceived = {
           queryObject:<%=FinancialManageTab2.ClientID %>,
           page: "<%=intPageIndex %>",
           initPage: function() {
                var self=this;
                $("#tbl_AccountsReceived tr").mouseover(function(i) {
                    if (i > 0) this.style.backgroundColor = "#FFF9E7";
                }).mouseout(function(i) {
                    if (i > 0) this.style.backgroundColor = "";
                })
                $("#AccountsReceived_ExportPage a").each(function() {
                    $(this).click(function() {    
                        var _href=self.queryObject.getUrl();       
                        var equalIndex = $(this).attr("href").lastIndexOf("=");
                        var page = $(this).attr("href").substring(equalIndex).split("=")[1];          
                        topTab.url(topTab.activeTabIndex,_href+"&Page="+page);
                        return false;
                    })
                });                
           }        
       };
       $(function() {
           AccountsReceived.initPage();
       });
   </script>
</asp:content>
