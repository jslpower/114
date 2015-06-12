<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewOrders.aspx.cs" Inherits="UserBackCenter.Order.NewOrders" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="NewOrders" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'NewOrders'
    });
</script>

<div id="<%=tblID %>" class="tablebox">
         <table id="tbl_arealist" cellspacing="0" cellpadding="0" border="0" align="center" class="toolbj1" style="width:100%;">
           <tbody><tr>
             <td align="left" class="title">我的专线：</td>
           </tr>
           <tr>
             <td align="left">
             <asp:Repeater runat="server" ID="rptAreaList">
               <ItemTemplate>
               <a href="javascript:void(0);" ref="<%#Eval("AreaId") %>"><%#Eval("AreaName")%></a> 
               </ItemTemplate>
             </asp:Repeater>
             </td>
           </tr>
         </tbody></table>
         <table cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;">
           <tbody><tr style="background:url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
             <td width="1%" height="30" align="left">&nbsp;</td>
             <td align="left"><span class="search">&nbsp;</span>关键字：
               <asp:TextBox runat="server" ID="txtSearchKey"></asp:TextBox>
               专线：
             <asp:DropDownList runat="server" ID="ddlAreaList"></asp:DropDownList>
               
               出团日期：
               <asp:TextBox runat="server" ID="txtDateBegin" onfocus="WdatePicker()"></asp:TextBox>
               至
               <asp:TextBox runat="server" ID="txtDateEnd" onfocus="WdatePicker()"></asp:TextBox>
             <button class="search-btn" type="button" id="btnSearch">搜索</button></td>
           </tr>
         </tbody></table>
		 <!--列表-->
<table width="100%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top:5px; margin-bottom:3px;">
           <tbody> 
           <asp:Repeater runat="server" ID="rptList">
              <ItemTemplate>
             
           <tr>
             <td height="48" bgcolor="#E9F2FB" background="<%=ImageServerUrl %>/images/newhang5bg.gif" align="left" style="border:1px solid #C7D9EB" colspan="2">&nbsp;<img width="15" height="16" src="<%=ImageServerUrl %>/images/ttt.gif"> 团号：<%#Eval("TourNo")%> &nbsp;<a target="_blank" href=""><%#Eval("RouteName")%></a>  <a target="_blank" href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>'>打印团队人员名单
</a><br />&nbsp;当前空位<strong class="ff0000"> <%#Eval("MoreThan")%> </strong>个；现有 <span class="chengse"> <%#Eval("Retailer")%><strong>家</strong></span> 零售商 共   <%#Eval("OrderNum")%>订单
</td>
           </tr>
           <tr>
             <td width="5%" valign="top" bgcolor="#E9F2FB" align="right"><img width="20" height="22" alt="查看该线路被预定信息" src="<%=ImageServerUrl %>/images/zhexian.gif"></td>
             <td width="95%" style="padding:5px;">
                 <table width="100%" cellspacing="0" cellpadding="1" bordercolor="#9dc4dc" border="1" align="center" class="liststyle">
                 <tbody><tr class="list_basicbg odd">
                   <th class="list_basicbg">订单号</th>
                   <th class="list_basicbg">组团社</th>
                   <th class="list_basicbg">联系人</th>
                   <th class="list_basicbg">电话</th>
                   <th class="list_basicbg">下单时间</th>
                   <th class="list_basicbg">人数</th>
                   <th class="list_basicbg">状态</th>
                   <th class="list_basicbg">支付状态</th>
                   <th class="list_basicbg">打印</th>
                   <th class="list_basicbg">操作</th>
                 </tr>
                 <%#GetOrderListByTourID(Eval("Orders"),Eval("TourId").ToString())%>
             </tbody></table></td>
           </tr>
            </ItemTemplate>
           </asp:Repeater>
         </tbody></table>
         <!--翻页-->
         <table width="98%" cellspacing="0" cellpadding="4" border="0" align="center" id="Table1">
           <tbody><tr>
             <td align="right" class="F2Back">
               <div id="divExportPage">
             <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server"  CurrencyPageCssClass="RedFnt" LinkType="4"></cc1:ExportPageInfo>
             </div>
             <asp:Literal runat="server" ID="litMsg"></asp:Literal>
              </td>
           </tr>
         </tbody></table>

<%--<table width="100%" border="0">
  <tbody><tr>
    <td align="left"><strong>我来不及处理订单怎么办？</strong>    </td>
  </tr>
  <tr>
    <td align="left"><strong>答</strong>：您可以电话客服中心，说出账户和密码，让客服代为维护。</td>
  </tr>
  <tr>
    <td align="left"><strong>结单是什么意思？</strong></td>
  </tr>
  <tr>
    <td align="left"><strong>答</strong>：默认游客回来后，如果订单状况为已确定，支付状况是全款收取，系统10天后自动结单。您也可以手工结单。返佣的计算以结单订单作为准。</td>
  </tr>
  <tr>
    <td align="left"><strong>如何取消订单？</strong></td>
  </tr>
  <tr>
    <td align="left"><strong>答</strong>：订单取消权限在组团社，请通知联系组团社，由组团社手动取消。</td>
  </tr>
</tbody></table>--%>

       </div>
       
       <script type="text/javascript">

           var NewOrders = {
               SearchFun: function() {
                   var para = { areaID: "", searchKey: "", dateBegin: "", dateEnd: "" };
                   para.areaID = $("#<%=tblID %>").find("[id$=<%=ddlAreaList.ClientID %>]").val();
                   para.searchKey = $.trim($("#<%=tblID %>").find("[id$=<%=txtSearchKey.ClientID %>]").val());
                   para.dateBegin = $.trim($("#<%=tblID %>").find("[id$=<%=txtDateBegin.ClientID %>]").val());
                   para.dateEnd = $.trim($("#<%=tblID %>").find("[id$=<%=txtDateEnd.ClientID %>]").val());
                   topTab.url(topTab.activeTabIndex, "/Order/RouteAgency/NewOrders.aspx?" + $.param(para));
               }
           }
           $(function() {
               $("#<%=tblID %>").find("[id$=tbl_arealist]").find("a").click(function() {
                   $("#<%=tblID %>").find("[id$=tbl_arealist]").find("a").attr("class", "");
                   $(this).attr("class", "select")
                   topTab.url(topTab.activeTabIndex, "/Order/RouteAgency/NewOrders.aspx?areaID=" + $(this).attr("ref"));
                   return false;
               })
               $("#<%=tblID %>").find("#btnSearch").click(function() {
                   NewOrders.SearchFun();
               })
               $("#<%=tblID %>").find("[id$=<%=txtSearchKey.ClientID %>]").keydown(function(e) {
                   if (e.keyCode == 13) {
                       NewOrders.SearchFun();
                   }
               })
               $("#<%=tblID %>").find("[id$=<%=txtSearchKey.ClientID %>]").blur(function() {
                   if ($.trim($(this).val()) == "") {
                       $(this).val("订单编号、线路名称");
                   }
               })
               $("#<%=tblID %>").find("#<%=txtSearchKey.ClientID %>").focus(function() {
                   if ($.trim($(this).val()) == "订单编号、线路名称") {
                       $(this).val("");
                   }
               })

               $("#<%=tblID %>").find("#tbl_arealist").find("a").each(function() {
                   if ($(this).attr("ref") == '<%=areaID %>') {
                       $(this).attr("class", "select");
                   }
               })

               $("#divExportPage a").click(function() {
                   topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                   return false;
               });
           })

          
       </script>

</asp:content>
