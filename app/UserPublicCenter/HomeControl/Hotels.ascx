<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Hotels.ascx.cs" Inherits="UserPublicCenter.HomeControl.Hotels" %>
 <div class="mainbox03-sidebar02 fixed">           <div class="tejia-title">           <h2 class="tjjd">特价酒店</h2>           <span class="tabs-trigger fixed"><a href="javascript:;" class="tabtwo-on" id="two1" onmousemove="setTab('two',1,2)">华东五市酒店</a> <a href="javascript:;" id="two2" onmousemove="setTab('two',2,2)">港澳酒店</a></span> <a href="http://jd.tongye114.com/" class="more02"  target="_blank">更多<s></s></a>           </div><div class="hr_5"></div>             <div id="con_two_1">            <div class="tjjd-Tab"><a href="javascript:;" class="tab-on">上海</a><a href="javascript:;" >杭州</a><a href="javascript:;">苏州</a><a href="javascript:;">南京</a><a href="javascript:;">无锡</a></div>            <!--tjjd-list start-->             <div class="tjjd-list">                 <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">                 <tr class="tjjd-list-head">                 <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>门市价</td>
                  <td>团队价</td>                 </tr>
              <%=hotelSh %>              </table>
             </div>
             <div class="tjjd-list" style="display:none;">
                <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr class="tjjd-list-head">
                  <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>门市价</td>
                  <td>团队价</td>
                </tr>
               <%=hotelHz %>
                </table>
             </div>
              <div class="tjjd-list" style="display:none;">                <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr class="tjjd-list-head">
                  <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>门市价</td>
                  <td>团队价</td>
                </tr>
               <%=hotelSz %>
               </table>
             </div>
              <div class="tjjd-list" style="display:none;">
                <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr class="tjjd-list-head">
                  <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>门市价</td>
                  <td>团队价</td>
                </tr>                 <%=hotelNj %>                </table>
             </div>
              <div class="tjjd-list" style="display:none;">
                <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr class="tjjd-list-head">
                  <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>门市价</td>
                  <td>团队价</td>
                </tr>
                <%=hotelWx %>                </table>
             </div>
             <!--tjjd-list end-->
            </div>             <div id="con_two_2" style="display:none;">
               <table width="97%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr class="tjjd-list-head">
                  <td height="26" width="40%">酒店名称</td>
                  <td>星级</td>
                  <td>优惠价</td>
                  <td>团队价</td>
                </tr>
              <%=hotelGa%>              </table>
            </div>        </div>              