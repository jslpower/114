<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchasingList.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.StatisticsManage.PurchasingAnalysis.PurchasingList" %>
    
<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="PurchasingList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8"
    id="tb_PurchasingList">
    <tr>
        <th height="35" colspan="10">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="14%" align="center">
                        用户名：
                    </td>
                    <td width="27%" align="left">
                        <input type="text" name="txtUserName" id="txtUserName" runat="server" />
                    </td>
                    <td width="59%" align="left">
                        <a href="javascript:void(0);" onclick="PurchasingList.OnSearch();return false;">
                            <img src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79"
                                height="25" alt="查询" /></a>
                    </td>
                </tr>
            </table>
            <label>
            </label>
        </th>
    </tr>
    <tr>
        <th width="83" height="30" bgcolor="#EEF7FF">
            用户名
        </th>
        <th width="80" bgcolor="#EEF7FF">
            姓名
        </th>
        <th width="83" bgcolor="#EEF7FF">
            公司名称
        </th>
        <th width="83" bgcolor="#EEF7FF">
            注册时间
        </th>
        <th width="83" bgcolor="#EEF7FF">
            最近交易
        </th>
        <th width="83" bgcolor="#EEF7FF">
            手机
        </th>
        <th width="80" bgcolor="#EEF7FF">
            MQ
        </th>
        <th width="83" bgcolor="#EEF7FF">
            统计日
        </th>
        <th width="83" bgcolor="#EEF7FF">
            机票数（张）
        </th>
        <th width="89" bgcolor="#EEF7FF" nowrap="nowrap">
            机票总金额（元）
        </th>
    </tr>
    
    
    <asp:repeater runat="server" id="rptList">
            <ItemTemplate>
            <tr>
            <td height="30" align="center"><%#Eval("BuyerUName")%></td>
            <td align="center"><%#Eval("BuyerContactName")%></td>
            <td align="center"><%#Eval("BuyerCName")%></td>
            <td align="center"><%#Convert.ToDateTime(Eval("BuyerRegisterTime")).ToString("yyyy-MM-dd")%></td>
            <td align="center"><%#Convert.ToDateTime(Eval("BuyerRecentOrderTime")).ToString("yyyy-MM-dd")%></td>
            <td align="center"><%#Eval("BuyerContactMobile")%></td>
            <td align="center"><%#Eval("BuyerMQ")%></td>
            <td align="center"><%#Convert.ToDateTime(Eval("StatTime")).ToString("yyyy-MM-dd")%></td>
            <td align="center"><%#Eval("TicketTotalCount")%></td>
            <td align="center"><%#Utils.GetMoney(Convert.ToDecimal(Eval("TicketTotalAmount")))%></td>
           </tr>
            </ItemTemplate>
   </asp:repeater>
</table>
<div id="divPage" style=" height:20px; padding-top:5px; text-align:center; width:835px">
    <asp:Label runat="server" Text="" ID="lblMsg"></asp:Label>
</div>
<div style=" width:835px; height:20px; text-align:left;">
    <input type="button"  value="返回" onclick="topTab.url(topTab.activeTabIndex, '/ticketscenter/statisticsmanage/purchasinganalysis/default.aspx')" />
</div>

<script type="text/javascript">
    var PurchasingList = {
        OnSearch: function() {
            var goToUrl = " /TicketsCenter/StatisticsManage/PurchasingAnalysis/PurchasingList.aspx?UserName=" + escape($.trim($("#tb_PurchasingList").find("input[id=ctl00_ContentPlaceHolder1_txtUserName]").val()));
            topTab.url(topTab.activeTabIndex, goToUrl);
        }
    };
    $("#tb_PurchasingList input").bind("keypress", function(e) {
        if (e.keyCode == 13) {
            PurchasingList.OnSearch();
            return false;
        }
    });

</script>

</asp:content>
