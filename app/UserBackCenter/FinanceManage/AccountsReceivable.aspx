<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsReceivable.aspx.cs"
    Inherits="UserBackCenter.FinanceManage.AccountsReceivable" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/FinancialManagement/FinancialManageTab.ascx" TagName="FinancialManageTab"
    TagPrefix="uc1" %>
<asp:content id="AccountsReceivable" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table width="100%" id="tbl_AccountsReceivable" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
    <tr>
        <td valign="top">
            <uc1:FinancialManageTab ID="FinancialManageTab1" TabIndex="0" runat="server" />
            <table width="100%" id="tbl_AccountsReceivable" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                class="zttype">
                <tr>
                    <th width="9%" align="center">
                        团号
                    </th>
                    <th width="19%" align="center">
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
                    <th width="5%" align="center">
                        人数
                    </th>
                    <th width="7%" align="center">
                        下单人
                    </th>
                    <th width="8%" align="center">
                        合同金额
                    </th>
                    <th width="8%" align="center">
                        已收
                    </th>
                    <th width="8%" align="center">
                        未收
                    </th>
                    <th width="5%" align="center">
                        收款登记
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rptAccountsReceivable">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("TourNo")%>
                            </td>
                            <td align="left">
                                &nbsp;<a href='<%# Eval("TourId").ToString().Trim()=="0"?"javascript:;":string.Format("/PrintPage/TeamInformationPrintPage.aspx?TourID={0}",Eval("TourId")) %>' target='<%# Eval("TourId").ToString().Trim()=="0"?"":"_blank" %>'><%#Eval("RouteName")%></a>
                            </td>
                            <td align="center" class="tbline">
                                <%#Eval("LeaveDate","{0:yyyy-MM-dd}")%>
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
                                <%#Eval("OperatorName")%>
                            </td>
                            <td align="center">
                                ￥<%#Eval("SumPrice","{0:F2}")%>
                            </td>
                            <td align="center">
                                ￥<%#Eval("CheckendPrice", "{0:F2}")%>
                            </td>
                            <td align="center">
                                ￥<%#(Convert.ToDecimal(Eval("SumPrice")) - Convert.ToDecimal(Eval("CheckendPrice"))).ToString("F2")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="Receivable_Record" AccountsReceivable="<%#Eval("ID") %>" >登记</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>   
                <tr runat="server" id="NoData" visible="false">
                    <td colspan="11" align="center"  style=" padding:6px;">
                        对不起，没有你要找的应收帐款信息！
                    </td>                
                </tr>             
            </table>
            <div  id="AccountsReceivable_ExportPage"  class="F2Back" style="text-align:right;" >
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
            </div>
        </td>
    </tr>
</table>
<script language="javascript" type="text/javascript">
    var AccountsReceivable = {
        queryObject:<%=FinancialManageTab1.ClientID %>,
        page: "<%=intPageIndex %>",
        initPage: function() {
            var self=this;
            $("#tbl_AccountsReceivable tr").mouseover(function(i) {
                if (i > 0) this.style.backgroundColor = "#FFF9E7";
            }).mouseout(function(i) {
                if (i > 0) this.style.backgroundColor = "";
            });
            $("#AccountsReceivable_ExportPage a").each(function() {
                $(this).click(function() {    
                    var _href=self.queryObject.getUrl();  
                    var equalIndex = $(this).attr("href").lastIndexOf("=");
                    var page = $(this).attr("href").substring(equalIndex).split("=")[1];                                  
                    topTab.url(topTab.activeTabIndex,_href+"&Page="+page);
                    return false;
                })
            });
            $("#tbl_AccountsReceivable a[AccountsReceivable]").click(function(){             
                var id=$(this).attr("AccountsReceivable");
                Boxy.iframeDialog({ title: "登记收款信息", iframeUrl: "/FinanceManage/EnterReceivables.aspx?id="+id, width: 800, height: 500, draggable: true,afterHide:function(){
                    var _href=self.queryObject.getUrl();                                 
                    topTab.url(topTab.activeTabIndex,_href+"&Page="+self.page);
                }, data: {} });               
                return false;
            });
        }
    };
    $(function() {
        AccountsReceivable.initPage();
    })
</script>
</asp:content>
