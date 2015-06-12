<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupOrderList.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.GroupOrderList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:Content id="HotelOrderList" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'GroupOrderList'
    });
</script>
<style type="text/css">
    table .odd	{ background:#F3F7FF;}
    table tr.highlight{ background:#FFF3BF;}
    table tr.selected{ background:#FFF3E7;color:#fff;}
</style>
    <div class="right">
       <div class="tablebox">
         <table id="<%=tblID %>" cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;" class="searchTable">
           <tbody><tr style="background:url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
             <td width="1%" height="30" align="left">&nbsp;</td>
             <td align="left"><span class="search">&nbsp;</span>
               搜索订单：关键字(酒店名称)
                 <input type="text" size="30" name="txtKey" id="txtKey"  onkeypress="return GroupOrderList.isEnter(event);"/>
                 <input type="radio"  name="rdTime" id="rdTime0" value="0" checked="checked">
                <label for="rdTime0">入住时间</label>
               <input type="radio" name="rdTime"  id="rdTime1" value="1">
               <label for="rdTime1">离店时间</label>
               <input style="width:80px;" name="txtStartTime" id="txtStartTime"   onkeypress="return GroupOrderList.isEnter(event);" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndTime\')}'})">
                <img width="16" height="13" style="margin-bottom:7px;" align="middle" onclick="javascript:$('#txtStartTime').focus()" src="<%=ImageServerUrl %>/images/time.gif"> -
               <input style="width:80px;" name="txtEndTime" id="txtEndTime"   onkeypress="return GroupOrderList.isEnter(event);" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})">
               <img width="16" height="13"  style="margin-bottom:7px;" align="middle" onclick="javascript:$('#txtEndTime').focus()" src="<%=ImageServerUrl %>/images/time.gif">
               <a href="javascript:void(0);" onclick="GroupOrderList.goSearch();"><img width="62" height="21" style="margin-bottom:-4px;border:0px;" src="<%=ImageServerUrl %>/images/chaxun.gif"></a>
               </td>
           </tr>
         </tbody></table>

         <!--添加信息表格-->
         <table cellspacing="0" cellpadding="1" bordercolor="#9dc4dc" border="1" align="center" class="padd5" style="width:100%; margin-top:1px;">
           
           <tbody><tr class="list_basicbg">
             <%--<th class="list_basicbg">订单号</th>--%>
             <th class="list_basicbg">酒店</th>
             <th class="list_basicbg">房型</th>
             <th class="list_basicbg">入住时间</th>
             <th class="list_basicbg">离开时间</th>
             <th class="list_basicbg">房间数</th>
             <th class="list_basicbg">姓名</th>
             <th class="list_basicbg">联系人性别</th>
             <th class="list_basicbg">联系手机</th>
             <th class="list_basicbg">功能</th>
           </tr>
           <cc1:CustomRepeater runat="server" ID="RepList">
            <ItemTemplate>
                <tr>
                 <%--<td align="left"></td>--%>
                 <td align="left"><%#Eval("HotelName")%></td>
                 <td align="center"><%#Eval("RoomAsk")%></td>
                 <td align="center"><%#Eval("LiveStartDate","{0:yyyy-MM-dd}")%>(<%#string.IsNullOrEmpty(Eval("LiveStartDate").ToString())?"":Utils.ConvertWeekDayToChinese(Convert.ToDateTime(Eval("LiveStartDate")))%>)</td>
                 <td align="center"><%#Eval("LiveEndDate","{0:yyyy-MM-dd}")%>(<%#string.IsNullOrEmpty(Eval("LiveEndDate").ToString()) ? "" : Utils.ConvertWeekDayToChinese(Convert.ToDateTime(Eval("LiveEndDate")))%>)</td>
                 <td align="center"><%#Eval("RoomCount")%></td>
                 <td align="center"><%#Eval("Contact.ContactName")%></td>
                 <td align="center"><%#Eval("Contact.ContactSex")%></td>
                 <td align="center"><%#Eval("Contact.Mobile")%></td>
                 <td align="center">
                    <a href="javascript:void(0);" class="basic_btn" onclick="topTab.url(topTab.activeTabIndex,'/HotelCenter/HotelOrderManage/GroupOrderShow.aspx?id=<%#Eval("Id")%>')"><span>查看</span></a>
                 </td>
               </tr>
            </ItemTemplate>
           </cc1:CustomRepeater>
         </tbody></table>
		 
		 <table width="100%" border="0" id="tablePageTr">
              <tbody>
<%--                  <tr>
                        <td align="left">待处理状态，只有第一次，酒店用户打开订单列表时才能看到，酒店看到订单后直接状态改为处理中。</td>
                  </tr>--%>
                  <tr>
                       <td align="right" class="F2Back"> 
                            <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
                      </td>
                  </tr>
            </tbody>
        </table>

       </div>
     </div>
     <script type="text/javascript">
         var GroupOrderList = {
             //判断是否按回车
             isEnter: function(event) {
                 event = event ? event : window.event;
                 if (event.keyCode == 13) {
                     GroupOrderList.goSearch();
                     return false;
                 }
             },
             _getData: function() {
                 return commonTourModuleData.get('<%=this.tblID %>');
             },
            //搜索
             goSearch: function() {
                    $conObj = $("#" + GroupOrderList._getData().ContainerID);
                     var searchUrl,kw, flag, startTime, endTime;
                     kw = $.trim($conObj.find("#txtKey").val()); //关键字
                     flag = $conObj.find("input[type='radio']:checked").val(); //0:入住时间  1:预订时间
                     startTime = $conObj.find("#txtStartTime").val(); //开始时间
                     endTime = $conObj.find("#txtEndTime").val(); //结束时间
                     searchUrl = "/HotelCenter/HotelOrderManage/GroupOrderList.aspx?kw=" + encodeURIComponent(kw) +
                                     "&flag=" + flag + "&startTime=" + startTime + "&endTime=" + endTime;
                     topTab.url(topTab.activeTabIndex, searchUrl);
                     return false;
                 }
             }
             $(function() {
                 $conObj = $("#" + GroupOrderList._getData().ContainerID);
                 //初始化查询框中的值
                 $conObj.find("#txtKey").val('<%=Server.UrlDecode(Request.QueryString["kw"])%>');
                 $conObj.find("input[type='radio']").each(function() {
                     if ($(this).val() == '<%=Request.QueryString["flag"]%>')
                         this.checked = true;
                 });
                 $conObj.find("#txtStartTime").val('<%=Request.QueryString["startTime"]%>');
                 $conObj.find("#txtEndTime").val('<%=Request.QueryString["endTime"]%>');
                 //分页在当前选项卡中加载
                 $conObj.parent(".tablebox").find("#tablePageTr a").each(function() {
                     $(this).click(function() {
                         topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                         return false;
                     })
                 });
                 //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
                 $conObj.next("table").find("tr:gt(0):even").addClass('odd');
                 $conObj.next("table").find("tr:gt(0)").hover(
		            function() { $(this).addClass('highlight'); },
		            function() { $(this).removeClass('highlight'); }
	            );
             });
     </script>
</asp:Content>