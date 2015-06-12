<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderOutSourceTab.ascx.cs"
    Inherits="UserBackCenter.usercontrol.UserOrder.OrderOutSourceTab" %>
        <td width="117" align="center" valign="bottom"
        style="background: url(<%=imagePath1 %>) no-repeat scroll 0% 0% transparent; width: 142px; height: 25px;"
        >
            <a class="orderlink" style="-moz-margin-left:-5px;" href="/UserOrder/OrdersAllOutSource.aspx" rel="ordertaburl"><strong style="font-size: 14px;
                font-weight: bold; color: #333333;">所有订单</strong></a>
        </td>
        <td width="117" height="25" align="center" valign="bottom"
        style="background: url(<%=imagePath2 %>) no-repeat scroll 0% 0% transparent; width: 142px; height: 25px;"
        >
            <a class="orderlink" href="/UserOrder/OrdersOutSource.aspx" rel="ordertaburl"><strong style="font-size: 14px;
                font-weight: bold; color: #333333;">最近交易的订单</strong></a>
        </td>

<style>
/* Firefox */
@-moz-document url-prefix()
{.orderlink { margin-left:-38px; }}
</style>
<script type="text/javascript">
    $(function(){
        $("#OrdersindexNavigationBar a[rel='ordertaburl']").click(function(){
            topTab.url(topTab.activeTabIndex,$(this).attr("href"));
            return false;
        });
    });
</script>

