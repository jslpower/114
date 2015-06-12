<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMDefault.aspx.cs" Inherits="UserBackCenter.RouteAgency.RouteManage.RMDefault" %>

<asp:content id="RMDefault" contentplaceholderid="ContentPlaceHolder1" runat="server">
<script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
<script type="text/javascript">
    var AddTourism = function(obj) {
        var url = encodeURI($(obj).attr("href"));
        topTab.url(topTab.activeTabIndex, url);
        return false;
    }
</script>
<div class="right">
    	<div id="div_list" class="tablebox">
    	<%if (showGN)
       { %>
    	 <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1">
    	    <tr>
    	      <td align="left" class="title">国内专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	          <asp:Repeater runat="server" id="rpt_gn">
    	              <ItemTemplate>
        	            <a href="/routeagency/routemanage/addtourism.aspx?travelRangeType=<%#(int)Eval("RouteType") %>&travelRangeId=<%#Eval("AreaId") %>&travelRangeName=<%#Eval("AreaName") %>&RouteSource=<%=(int)routeSource %>" onclick="return AddTourism(this)"><%#Eval("AreaName")%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	     
    	      </td>
  	      </tr>
  	    </table>
  	    <%} if (showGJ)
       {%>
    	  <div class="hr_10"></div>
    	<table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1" >
    	    <tr>
    	      <td align="left" class="title">国际专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	          <asp:Repeater runat="server" id="rpt_gj">
    	              <ItemTemplate>
        	            <a  href="/routeagency/routemanage/addtourism.aspx?travelRangeType=<%#(int)Eval("RouteType") %>&travelRangeId=<%#Eval("AreaId") %>&travelRangeName=<%#Eval("AreaName") %>&RouteSource=<%=(int)routeSource %>" onclick="return AddTourism(this)"><%#Eval("AreaName")%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	      </td>
  	      </tr>
  	    </table>
  	    <%} if (showZB)
       { %>
    	  <div class="hr_10"></div>
    	<table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1">
    	    <tr>
    	      <td align="left" class="title">周边及地接专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	         <asp:Repeater runat="server" id="rpt_zb">
    	              <ItemTemplate>
        	            <a  href="/routeagency/routemanage/addtourism.aspx?travelRangeType=<%#(int)Eval("RouteType") %>&travelRangeId=<%#Eval("AreaId") %>&travelRangeName=<%#Eval("AreaName") %>&RouteSource=<%=(int)routeSource %>" onclick="return AddTourism(this)"><%#Eval("AreaName")%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	      </td>
  	      </tr>
  	    </table>
    	  <div class="hr_5"></div>
    	  <%} %>
  	  </div>
  	  <div id="errmsg" style="display:none">如注册时忘记选择专线区域，请电话客户0571-56884627，添加公司相关信息。</div>
</div>
<script type="text/javascript">
    $(function() {
        if ($.trim($("#div_list").html()).length <= 0) {
            $("#errmsg").show();
        }
        if ($("#div_list a").length == 1) {
            $("#div_list a").click();
        }
    })
</script>
</asp:content>
