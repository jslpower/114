<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPayabled.aspx.cs" Inherits="UserBackCenter.FinanceManage.AccountPayabled" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/FinancialManagement/FinancialManageTab.ascx" TagName="FinancialManageTab"
    TagPrefix="uc1" %>
<asp:content id="AccountsReceivable" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
    <tr>
        <td valign="top">
            <uc1:FinancialManageTab ID="FinancialManageTab1" TabIndex="3" runat="server" />
            <table width="100%" id="tbl_AccountsPayable" border="1" cellpadding="2" cellspacing="0" bordercolor="#B9D3E7"
                class="zttype">
                <tr>
                    <th width="6%" align="center">团号</th>
                    <th width="23%" align="center">线路名称</th>
                    <th width="9%" align="center">出团日期</th>
                    <th width="15%" align="center">供应商</th>
                    <th width="9%" align="center">供应商类型</th>
                    <th width="6%" align="center">人数</th>
                    <th width="8%" align="center">应付总额</th>
                    <th width="8%" align="center">清算日期</th>
                </tr>
                <asp:Repeater runat="server" ID="rptAccountsPayabled">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("TourNo")%>
                            </td>
                            <td align="left">
                                <a href='<%# Eval("TourId").ToString().Trim()=="0"?"javascript:;":string.Format("/PrintPage/TeamInformationPrintPage.aspx?TourID={0}",Eval("TourId")) %>' target='<%# Eval("TourId").ToString().Trim()=="0"?"":"_blank" %>' ><%#Eval("RouteName")%></a>
                            </td>
                            <td align="center" class="tbline">
                                <%#Eval("LeaveDate")%>
                            </td>
                            <td align="center">
                               <%#Eval("CompanyName")%>
                            </td>
                            <td align="center">
                                <%#Eval("CompanyType")%>
                            </td>
                            <td align="center">
                                <%#Eval("PCount")%>
                            </td>
                            <td align="center">
                                <%#Eval("SumPrice","{0:c2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("ClearTime", "{0:yyyy-MM-dd}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr runat="server" visible="false" id="DataEmpty">
                    <td colspan="8" align="center" style="padding:6px;">对不起，没有你要找的已付帐款信息！</td>
                </tr>
            </table>
            <div  id="AccountsPayable_ExportPage"  class="F2Back" style="text-align:right;" >
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
            </div>
        </td>
    </tr>
</table>
<script language="javascript" type="text/javascript">
    var AccountsPayable = {
        queryObject:<%=FinancialManageTab1.ClientID %>,
        page: "<%=intPageIndex %>",
        initPage: function() {
            var self=this;
            $("#tbl_AccountsPayable tr").mouseover(function(i) {
                if (i > 0) this.style.backgroundColor = "#FFF9E7";
            }).mouseout(function(i) {
                if (i > 0) this.style.backgroundColor = "";
            });
            $("#AccountsPayable_ExportPage a").each(function() {
                $(this).click(function() {  
                    var _href=self.queryObject.getUrl();               
                    topTab.url(topTab.activeTabIndex,_href+"&Page="+$(this).text());
                    return false;
                })
            });
        }
    };
    $(function() {
        AccountsPayable.initPage();
    })
</script>
</asp:content>
