<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScatterPlanC.aspx.cs" Inherits="UserBackCenter.TeamService.ScatterPlanC" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/ILine.ascx" TagName="Route" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="ScatterPlan" runat="server" contentplaceholderid="ContentPlaceHolder1">   
 <script type="text/javascript">
     commonTourModuleData.add({
         ContainerID: '<%=Key %>',
         ReleaseType: 'ScatterPlanC'
     });
    </script> 
     <div id="<%=Key %>" class="right">
     <div class="tablebox">
        <uc1:Route  ID="RouteUC" runat="server"/>
        <div class="hr_5"></div>
        <table id="tab_travelDays" border="0" align="center" cellpadding="0" cellspacing="0"
                style="width: 100%;" class="toolbj1">
                <tr>
                    <td align="left" class="title">
                        出游天数：
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <a href="javascript:void(0);" val="12">五日以下</a> 
                        <a href="javascript:void(0);" val="5">五日游</a>
                        <a href="javascript:void(0);" val="6">六日游</a>
                        <a href="javascript:void(0);"val="7">七日游</a>
                        <a href="javascript:void(0);"val="13">七日以上</a>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
         <table id="tab_goCity"  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%; background:#fff;" class="toolbj padd5">
           <tr>
             <td width="65" height="30" align="left" ><strong>出发地点：</strong></td>
             <td align="left">
                <span>
                    <asp:Repeater id="rpt_DepartureCity" runat="server">
                        <ItemTemplate>
                            <a class="a_City" href="javascript:void(0);" value="<%#Eval("CityId") %>"cname="<%#Eval("CityName")%>"><%#Eval("CityName")%></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </span>
                <a href="javascript:void(0)" id="a_goSet"  onclick="ScatterPlanC.SetCity(this)" ><span class="huise">更多</span></a>
             </td>
           </tr>
         </table>
         <div class="hr_5"></div>
         <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
             <tr style="background:url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                 <td width="1%" height="30" align="left" >&nbsp;</td>
                 <td align="left"><span class="search">&nbsp;</span>关键字
                    <input  type="text" class="keydownSelect" size="20" style="width:60px;"  id="SearchTxt" runat="server"/>
                    出发地
                    <input type="text" class="keydownSelect" size="20" style="width:60px;" id="StartPlace" runat="server"/>                   
                    出团日期
                   <input type="text" class="keydownSelect" size="12" width="60px;"  onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'});" id="StartDate" runat="server"/>至
                   <input type="text" class="keydownSelect" size="12" width="60px;" onfocus="WdatePicker({minDate:'%y-%M-{%d+1}'});"  id="EndDate"  runat="server"/>
                   <button type="button" class="search-btn" id="Search" runat="server">搜索</button>
                   <input id="CheckShowJS" type="checkbox"  runat="server"/>显示结算价</td>
                </tr>
         </table>
		 <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
           <tr style="background:url(<%=ImageServerUrl %>/images/lmnavm.gif); height:32px;">
             <td width="80%" align="left">
                <span class="guestmenu guestmenu02">状 态</span>
                <span class="leixing-c">                
                 <%--//1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩--%>
                 <asp:Repeater runat="server" id="rpt_type">
                    <ItemTemplate>
                        <a class="state<%#Container.ItemIndex+1 %> RecommendType" href="javascript:void(0);" val="<%#Eval("Value") %>"><%#Eval("Text") %></a> 
                    </ItemTemplate>
                </asp:Repeater>
                 <%-- <a class="state1" href="javascript:" value="2">推荐</a>
                  <a class="state2" href="javascript:" value="3">特价</a>
                  <a class="state3" href="javascript:" value="4">豪华</a>
                  <a class="state4" href="javascript:" value="5">热门</a>
                  <a class="state5" href="javascript:" value="6">新品</a> 
                  <a class="state6" href="javascript:" value="8">纯玩</a> 
                  <a class="state7" href="javascript:" value="7">经典</a>--%>
                </span>
             </td>
             <td width="20%" align="center">             
               <a href="/TeamService/LineLibraryList.aspx?type=<%=(int)areaType %>" class="GoRouteList">
                <img src="<%=ImageServerUrl %>/images/arrowpl.gif" />查看国内游线路库</a>
             </td>
           </tr>
         </table>
         <!--列表-->
		 <%--<table width="100%" border="0" align="center">
		   <tr>
		     <td align="left" class="ff0000"><asp:Literal ID="litCompanyName" runat="server"></asp:Literal> 
		      <asp:Literal ID="litContactName" runat="server"></asp:Literal>  
		      电话：<asp:Literal ID="litContactPhone" runat="server"></asp:Literal> 
		      传真： <asp:Literal ID="litContactFax" runat="server"></asp:Literal> 
		      <asp:Literal ID="litContactQQ" runat="server"></asp:Literal> 
		      <%=litContactMQ%>
		      </td>
		   </tr>
		 </table>--%>
         <cc2:CustomRepeater ID="repPlanlist" runat="server">
           <HeaderTemplate>
           <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc" style="width:100%; margin-top:1px;" class="liststylesp">
           <tr class="list_basicbg">
             <th class="list_basicbg">出发</th>
             <th class="list_basicbg">团号</th>
             <th nowrap="nowrap" class="list_basicbg" style="width: 400px">线路名称</th>
             <th nowrap="nowrap" class="list_basicbg">推荐状态</th>
             <th nowrap="nowrap" class="list_basicbg">天数</th>
             <th nowrap="nowrap" class="list_basicbg">出团日期</th>
             <th nowrap="nowrap" class="list_basicbg">报名截止</th>
             <th nowrap="nowrap" class="list_basicbg">人数</th>
             <th nowrap="nowrap" class="list_basicbg">余位</th>
             <th nowrap="nowrap" class="list_basicbg">成人价</th>
             <th nowrap="nowrap" class="list_basicbg">儿童价</th>
             <th nowrap="nowrap" class="list_basicbg SHowJSPrice" style="display:none">结算价(成人/儿童)</th>
             <th nowrap="nowrap" class="list_basicbg">功能</th>
           </tr>
          </HeaderTemplate>
           <ItemTemplate>
             <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>> 
                 <td align="center" nowrap="NOWRAP">
                    <%# Eval("StartCityName")%>
                 </td>
                 <td align="left" nowrap="nowrap">
                    <%#EyouSoft.Common.Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)Eval("CompanyLev"))%><%# Eval("TourNo")%>
                 </td>
                 <td align="left">
                    <a href='/PrintPage/TeamRouteDetails.aspx?TeamId=<%#Eval("TourId") %>'target="_blank"><%# Eval("RouteName")%></a><%=EyouSoft.Common.Utils.GetMQ(SiteUserInfo.ContactInfo.MQ)%>
                 </td>
                 <td align="center" nowrap="NOWRAP">
                   <span class="state<%#(int)Eval("RecommendType")-1 %>"><%#Eval("RecommendType").ToString() == "0" || Eval("RecommendType").ToString() =="无"? "" : Eval("RecommendType")%></span> 
                </td>
                 <td align="center" nowrap="nowrap">
                    <%# Eval("Day")%>天<%# Eval("Late") %>晚
                 </td>
                 <td align="center">
                    <%# Eval("LeaveDate","{0:MM/dd(ddd)}")%>
                 </td> 
                 <td align="center">
                    <span class="ff0000"><%# Eval("RegistrationEndDate", "{0:MM/dd(ddd)}")%></span>
                 </td>
                 <td align="center" nowrap="nowrap">
                    <%#Eval("TourNum")%>
                 </td>
                 <td align="center" nowrap="nowrap">
                    <%# Eval("IsLimit").ToString().ToLower()=="true"?"∞": Eval("MoreThan")%>
                 </td>
                 <td align="center" nowrap="nowrap">
                    <%#Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                 </td>
                 <td align="center" nowrap="NOWRAP">
                    <%#Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                 </td>
                 <td align="center" nowrap="NOWRAP" class="SHowJSPrice"  style="display:none">
                   <span class="ff0000"> <%# Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                    /
                    <span class="ff0000"><%# Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
                 </td>
                 <td align="center" nowrap="nowrap">
                   <%# INItTourState(((int)Eval("PowderTourStatus")).ToString(), Eval("TourId").ToString())%>
                </td>
           </tr>
           </ItemTemplate>
           <FooterTemplate> </table></FooterTemplate>
         </cc2:CustomRepeater>
         <asp:Panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
                 </asp:Panel>
        <table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right" border="0">
                    <tr>
                        <td class="F2Back" align="right" height="40">
                            <cc1:ExportPageInfo ID="ExportPageInfo1" LinkType="4" runat="server" />
                        </td>
                    </tr>
                </table>
		<%-- <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:20px">
           <tr>
             <td align="left"><strong>如何查看结算价?</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：为了防止游客直接看到结算价，默认不显示结算价。如需要查看，请点击 <strong>显示结算价</strong> 前的勾选框。</td>
           </tr>
           <tr>
             <td align="left"><strong>团队的排序是按照什么排序的？</strong></td>
           </tr>
           <tr>
             <td align="left"><strong>答</strong>：首先按照出发时间正向排序，然后按照广告客户推荐，VIP客户，更新时间的顺序排序。</td>
           </tr>
         </table>--%>
         </div>
     </div>
     <script type="text/javascript">
         var ScatterPlanC = {
             Search: function() {
                 var RouteId = "";
                 if ($("#<%=Key %>_tab_ILine .select").length > 0) {
                     RouteId = $("#<%=Key %>_tab_ILine .select").attr("lineid");
                 }
                 //关键字
                 var id = $("#<%=SearchTxt.ClientID %>").val();
                 //出发地
                 var StartPlace = $("#<%=StartPlace.ClientID %>").val();
                 var StartPlaceId = '<%=startCityId %>';
                 if ($("#<%=Key %> #tab_goCity a.ff0000").length > 0) {
                     StartPlaceId = $("#<%=Key %> #tab_goCity a.ff0000").attr("value");
                 }
                 //出发时间
                 var StartDate = $("#<%=StartDate.ClientID %>").val();
                 //返回时间
                 var EndDate = $("#<%=EndDate.ClientID %>").val();
                 var travelDays = $("#<%=Key %> #tab_travelDays .select").attr("val"); //出游天数
                 var queryUrl = encodeURI("/TeamService/ScatterPlanC.aspx?StartPlaceId=" + StartPlaceId +
                 "&RouteId=" + RouteId +
                 "&Id=" + id +
                 "&StartPlace=" + StartPlace +
                 "&StartDate=" + StartDate +
                 "&EndDate=" + EndDate +
                 "&travelDays=" + travelDays);
                 topTab.url(topTab.activeTabIndex, queryUrl);
                 return false;
             },
             //设置城市
             SetCity: function(obj) {
                 Boxy.iframeDialog({
                     iframeUrl: "/RouteAgency/SetLeaveCity.aspx?callBack=ScatterPlanC.BoxyCallBack&Key=<%=Key %>&GetType=a&ContainerID=" + $(obj).attr("id") + "&rnd" + Math.random(),
                     title: "设置出发城市",
                     modal: true,
                     width: "650",
                     height: "450"
                 });
                 return false;
             },
             SetTxtGoCity: function(obj) {
                 var form = $("#<%=Key %>");
                 form.find("#tab_goCity .ff0000").removeClass("ff0000");
                 $(obj).addClass("ff0000");
                 form.find("#<%=StartPlace.ClientID %>").val($(obj).text());
                 ScatterPlanC.Search();
             },
             BoxyCallBack: function() {
                 $("#<%=Key %> #tab_goCity a.a_City").click(function() {
                     ScatterPlanC.SetTxtGoCity(this);
                     return false;
                 })
             }
         };
         $(document).ready(function() {
             var Key = $("#<%=Key %>");
             //回车查询效果
             Key.find(".keydownSelect").keydown(function(e) {
                 if (e.keyCode == 13) {
                     ScatterPlanC.Search();
                     return false;
                 } else {
                     return true;
                 }
             });
             Key.find("#<%=Search.ClientID %>").click(function() {
                 ScatterPlanC.Search();
                 return false;
             });
             //列表颜色效果
             Key.find(".liststylesp tr").hover(
            function() {
                if ($(this).attr("class") != "list_basicbg")
                    $(this).addClass("highlight");
            },
            function() {
                if ($(this).attr("class") != "list_basicbg")
                    $(this).removeClass("highlight");
            })
            .click(function() {
                $(this).parent().find("tr").removeClass("selected");
                $(this).addClass("selected");
            })

             //显示或者隐藏结算价
             Key.find("#<%=CheckShowJS.ClientID %>").click(function() {

                 Key.find(".SHowJSPrice").css("display", $(this).attr("checked") ? "" : "none")
             });

             //查看国内旅游线路库
             Key.find("a.GoRouteList").click(function() {
                 topTab.remove(topTab.activeTabIndex)
                 topTab.open($(this).attr("href"), "国内线路库", { isRefresh: false });
                 return false;
             });

             //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
             Key.find(".leixing-c a").each(function() {
                 $(this).click(function() {
                     var RouteId = "";
                     if ($("#<%=Key %>_tab_ILine .select").length > 0) {
                         RouteId = $("#<%=Key %>_tab_ILine .select").attr("lineid");
                     }
                     //关键字
                     var id = $("#<%=SearchTxt.ClientID %>").val();
                     //出发地
                     var StartPlace = $("#<%=StartPlace.ClientID %>").val();
                     var StartPlaceId = '<%=startCityId %>';
                     if ($("#<%=Key %> #tab_goCity a.ff0000").length > 0) {
                         StartPlaceId = $("#<%=Key %> #tab_goCity a.ff0000").attr("value");
                     }
                     //出发时间
                     var StartDate = $("#<%=StartDate.ClientID %>").val();
                     //返回时间
                     var EndDate = $("#<%=EndDate.ClientID %>").val();
                     var queryUrl = encodeURI("/TeamService/ScatterPlanC.aspx?State=" + $(this).attr("val") + "&StartPlaceId=" + StartPlaceId + "&RouteId=" + RouteId + "&Id=" + id + "&StartPlace=" + StartPlace + "&StartDate=" + StartDate + "&EndDate=" + EndDate);
                     topTab.url(topTab.activeTabIndex, queryUrl);
                     return false;
                 });
             });

             //预定
             Key.find(".basic_btn:not([target='_blank'])").click(function() {
                 var ID = $(this).attr("value");
                 var queryUrl = "/Order/OrderByTour.aspx?TourID=" + ID;
                 topTab.url(topTab.activeTabIndex, queryUrl);
                 return false;
             });
             Key.find("#ExportPageInfo a").click(function() {
                 topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                 return false;
             })
             if ('<%=startCityId %>'.length > 0) {
                 Key.find("#tab_goCity a[value='" + '<%=startCityId %>' + "']").addClass("ff0000");
             }
             Key.find("#<%=StartPlace.ClientID %>").blur(function() {
                 if (Key.find("#tab_goCity a[text='" + $.trim($(this).val()) + "']").length <= 0) {
                     Key.find(".ff0000").removeClass("ff0000")
                 }
             })
             Key.find("#tab_goCity .a_City").click(function() {
                 ScatterPlanC.SetTxtGoCity(this);
                 return false;
             })
             if ('<%=powderDay %>'.length > 0) {
                 Key.find("#tab_travelDays a[val='" + '<%=powderDay %>' + "']").addClass("select");
             }
             Key.find("#tab_travelDays a").click(function() {
                 var form = $("#<%=Key %>");
                 form.find("#tab_travelDays .select").removeClass("select");
                 $(this).addClass("select");
                 ScatterPlanC.Search();
                 return false;
             })
         });
     </script>
</asp:content>
