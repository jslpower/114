<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketTopMenu.ascx.cs" Inherits="UserPublicCenter.AirTickets.TeamBook.TicketTopMenu" %>
<div class="sub_menu">
        <ul>
        	<li id="HeaderMenu_tab1">查询</li>
            <li id="HeaderMenu_tab2">选择</li>
            <li id="HeaderMenu_tab3">预订</li>
            <li id="HeaderMenu_tab4">支付</li>
            <li id="HeaderMenu_tab5">完成</li>
        </ul>
        <div class="sub_menu_right">&nbsp;</div>
    </div>
    <script type="text/javascript">

$(document).ready(function()
{ 
  var HeaderMenuTab=$("#HeaderMenu_<%=TabIndex%>");
  if(HeaderMenuTab)
  {
      HeaderMenuTab.attr("class", "over");
  }
});

</script>