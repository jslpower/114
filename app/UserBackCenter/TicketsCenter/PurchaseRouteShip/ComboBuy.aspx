<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComboBuy.aspx.cs" Inherits="UserBackCenter.TicketsCenter.PurchaseRouteShip.ComboBuy" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="ComboBuy" runat="server" contentplaceholderid="ContentPlaceHolder1">

      	<ul class="sub_leftmenu" id="con_top_ul">
        	<li><a href="#"  id="three1" onclick="topTab.url(topTab.activeTabIndex, '/TicketsCenter/PurchaseRouteShip/Default.aspx');return false;">常规购买</a></li>
           <li><a href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex, '/TicketsCenter/PurchaseRouteShip/ComboBuy.aspx?type=2');return false;">
        套餐购买</a></li>
    <li><a href="javascript:void(0);" onclick="topTab.url(topTab.activeTabIndex, '/TicketsCenter/PurchaseRouteShip/ComboBuy.aspx?type=3');return false;">
        促销购买</a></li>
        </ul>
        <div class="clearboth"></div>
        <div class="contact_text"><span><font color="#FF0000">备注：</font>如果有个性化需求，可在每周一至周五9：00－17：30另行 <a href="javascript:void(0);" onclick="window.open('<%=Utils.GetMQLink("27440") %>')">联系我们</a>。<font color="#FF0000">供应商管理部：</font><a href="javascript:void(0);" onclick="window.open('<%=Utils.GetMQLink("27440") %>')">MQ</a>
以上所有活动按照自然月计算，本活动解释权归属同业114。</span></div>
        <div class="clearboth"></div>
<div id="con_three_2" style=" width:835px;">
        	<table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8" class="admin_tablebox">
              <tr>
                <th width="69" height="30" align="center" bgcolor="#EEF7FF">套餐项目</th>
                <th width="69" align="center" bgcolor="#EEF7FF">类型</th>
                <th width="69" align="center" bgcolor="#EEF7FF">航空公司</th>
                <th width="69"  align="center" bgcolor="#EEF7FF">始发地</th>
                <th width="69" align="center" bgcolor="#EEF7FF">目的地</th>
                <th width="69" align="center" bgcolor="#EEF7FF">一月价格(元/条)</th>
                <th width="69" align="center" bgcolor="#EEF7FF">一季价格(元/条)</th>
                <th width="69" align="center" bgcolor="#EEF7FF">半年价格(元/条)</th>
                <th width="69" align="center" bgcolor="#EEF7FF">小计(元)</th>
                <th width="69" align="center" bgcolor="#EEF7FF">开始月份</th>
                <th width="69" align="center" bgcolor="#EEF7FF">结束月份</th>
                <th width="69" align="center" bgcolor="#EEF7FF">购买</th>
              </tr>
            <asp:repeater runat="server" ID="rptList">
           <ItemTemplate>
        <tr>
            <td height="30" align="center">
               <%#Eval("PackageName") %>
               <input type="hidden" id="cs_hide_packageId_<%#Container.ItemIndex %>"  value='<%#Eval("id") %>'/>
            </td>
            <td align="center">
                <%#EyouSoft.Model.TicketStructure.RateType.团队散拼.ToString()%>
            </td>
            <td align="center">
                <%#Eval("FlightName")%>
            </td>
            <td align="center">
                <%#GetCityNameById(Eval("HomeCityId").ToString())%>
            </td>
            <td align="center">
                <%#GetCityNameById(Eval("DestCityIds").ToString())%>
            </td>
            <td align="center" bgcolor="#E9EDF4">
                <input type="radio" name="radio_<%#Container.ItemIndex %>"  value="<%#Convert.ToDecimal(Eval("MonthPrice")).ToString("0.00")%>" checked="checked" onclick="ThisDePage.RadioClick(this,'1')" />
               <%#Utils.GetMoney(Convert.ToDecimal(Eval("MonthPrice")))%>
                <input type="hidden" id="cs_hide_price<%#Container.ItemIndex %>" value="<%#Convert.ToDecimal(Eval("MonthPrice")).ToString("0.00")%>" rel="1"/>
            </td>
            <td align="center">
                <input type="radio" name="radio_<%#Container.ItemIndex %>"  value="<%#Convert.ToDecimal(Eval("QuarterPrice")).ToString("0.00")%>" onclick="ThisDePage.RadioClick(this,'2')"/>
               <%#Utils.GetMoney(Convert.ToDecimal(Eval("QuarterPrice")))%>
            </td>
            <td align="center">
                <input type="radio" name="radio_<%#Container.ItemIndex %>" value="<%#Convert.ToDecimal(Eval("HalfYearPrice")).ToString("0.00")%>" onclick="ThisDePage.RadioClick(this,'3')"/>
                <%#Utils.GetMoney(Convert.ToDecimal(Eval("HalfYearPrice")))%>
                
            </td>
            <td align="center">
                <span id="cs_spanAllPrice<%#Container.ItemIndex %>"><%#Utils.GetMoney(Convert.ToDecimal(Eval("MonthPrice")))%></span>
            </td>
            <td align="center">
                 <%#DateTime.Now.Year.ToString() %> 
                <select id="cs_selectMonth<%#Container.ItemIndex %>" onchange="ThisDePage.SelectMonthChange(this)">
                    <%#GetOptionByMonth() %>
                </select>
            </td>
            <td align="center">
                <span id="cs_span_Time<%#Container.ItemIndex %>"><%#DateTime.Now.Year.ToString() %>.<%#DateTime.Now.Month.ToString() %> </span>
            </td>
            <td align="center">
                <input type="button" value="支付" ref="<%#Container.ItemIndex %>" onclick="ThisDePage.PayMoney(this)" />
            </td>
        </tr>
            </ItemTemplate>
        </asp:repeater>
        </table>
            <div id="cs_divPage" style=" height:20px; width:100%; text-align:center; margin-top:5px;">
        <asp:label runat="server" text="" ID="cs_lblMsg"></asp:label>
        <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
    </div>
</div>
         <script type="text/javascript">
             var ThisDePage = {
                 RadioClick: function(obj, type) {
                     var price = Number($(obj).val());
                     var num = $(obj).attr("name").replace("radio_", "");
                     $("#cs_spanAllPrice" + num).html(price);
                     $("#cs_hide_price" + num).val(price);
                     $("#cs_hide_price" + num).attr("rel", type);

                     this.SelectMonthChange($("#cs_selectMonth" + num));
                 },
                 //下拉框 月份选择事件
                 SelectMonthChange: function(obj) {
                     var num = Number($(obj).attr("id").replace("cs_selectMonth", ""));
                     var type = $("#cs_hide_price" + num).attr("rel");

                     var newMonth = Number($(obj).val());
                     switch (type) {
                         case "1": newMonth = newMonth + 0; break;
                         case "2": newMonth = newMonth + 2; break;
                         case "3": newMonth = newMonth + 5; break;
                     }

                     var dateTime = new Date();
                     dateTime.setFullYear(Number("<%=DateTime.Now.Year.ToString() %>"));
                     if (newMonth == 12) {
                         $("#cs_span_Time" + num).html(dateTime.getFullYear() + "." + newMonth);
                     } else {

                         dateTime.setMonth(newMonth);
                         $("#cs_span_Time" + num).html(dateTime.getFullYear() + "." + dateTime.getMonth());
                     }
                 },
                 //支付事件 
                 PayMoney: function(obj) {
                     var num = Number($(obj).attr("ref"));
                     var buyConut = $("#cs_select_" + num + " option:selected").val();
                     var companyId = "<%=companyId%>";
                     var beginTime = "<%=DateTime.Now.Year.ToString() %>-" + $("#cs_selectMonth" + num + " option:selected").val() + "-1";
                     var type = $("#cs_hide_price" + num).attr("rel");
                     var packageId = $("#cs_hide_packageId_" + num).val();
                     var packageType = "<%=packageType %>";
                     var Searchparms = { "buyCount": "", "companyId": "", "SartDateTime": "", "p": "cs", "type": "", "packageId": "", "packageType": "" };
                     Searchparms.buyCount = buyConut;
                     Searchparms.companyId = companyId;
                     Searchparms.SartDateTime = beginTime;
                     Searchparms.type = type;
                     Searchparms.packageId = packageId;
                     Searchparms.packageType = packageType;
                     $.ajax({
                         type: "GET",
                         url: "/ticketscenter/purchaserouteship/PurchasePay.ashx?" + $.param(Searchparms),
                         cache: false,
                         success: function(result) {
                             if (result != "error") {
                                 Boxy.iframeDialog({ title: "购买信息确认", iframeUrl: "/ticketscenter/purchaserouteship/MsgComfirm.aspx?logId=" + result, width: "310px", height: "320px", draggable: true, data: null });
                             }
                         }
                     });
                 }
             };

             $(function() {
                 $("#cs_divPage a").click(function() {
                     var str = $(this).attr("href").match(/&[^&]+$/);
                     pageIndex = str.toString().replace("&Page=", "");
                     topTab.url(topTab.activeTabIndex, "/TicketsCenter/PurchaseRouteShip/Default.aspx?type=<%=packageType %>&Page=" + pageIndex);
                     return false;
                 });

                 $("#cs_divPage select").change(function() {
                     pageIndex = $(this).val();
                     topTab.url(topTab.activeTabIndex, "/TicketsCenter/PurchaseRouteShip/Default.aspx?type=<%=packageType %>Page=" + pageIndex);
                     return false;
                 });

                 if ("<%=packageType %>" == "2") {
                     $("#con_top_ul a").eq(1).addClass("book_default");
                 } else {
                 $("#con_top_ul a").eq(2).addClass("book_default");
                 }
             });
             //关闭iframe
             function PurchaseRouteCloseIframe(iframeId) {
                 var boxy = Boxy.getIframeDialog(iframeId);
                 if (boxy != null && boxy != undefined) {
                     boxy.hide();
                 }
             }
 </script>
</asp:content>
