<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SceniceOrder.aspx.cs" Inherits="UserBackCenter.ScenicManage.SceniceOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<asp:content id="SceniceOrder" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
        <tr style="background:url(http://localhost:30001/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
            <td width="1%" height="30" align="left">&nbsp;</td>
            <td align="left"><span class="search">&nbsp;</span>
            搜索订单：关键字
            <input name="text2" type="text" size="30" />
            游玩时间
            <input id="txtInTime3" name="txtInTime3" style="width:80px;" />
            <img src="../images/time.gif" width="16" height="13" align="middle" onclick="javascript:$('#txtInTime').focus()" /> <img src="../images/chaxun.gif" width="62" height="21" style="margin-bottom:-4px;"/></td>
        </tr>
    </table>
    
    <table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc" style="width:100%; margin-top:1px;" class="padd5">
           
           <tr class="list_basicbg">
             <th>订单号</th>
             <th>景区</th>
             <th>门票类型</th>
             <th>游玩时间</th>
             <th>数量</th>
             <th>姓名</th>
             <th>联系手机</th>
             <th>金额</th>
             <th>支付方式</th>
             <th>状态</th>
             <th>支付</th>
             <th>状态管理</th>
             <th>功能</th>
           </tr>
    <cc1:CustomRepeater runat="server">
        <ItemTemplate>
            <tr>
                <td><a href="" ><%#Eval("OrderNo") %></a></td>
                <td><%#Eval("SceniceName") %></td>
                <td><%#Eval("TicketType") %></td>
                <td><%#Eval("PlayTime") %></td>
                <td><%#Eval("Count") %></td>
                <td><%#Eval("Name") %></td>
                <td><%#Eval("TelPhone") %></td>
                <td><%#Eval("Acount") %></td>
                <td><%#Eval("PayType") %></td>
                <td><%#Eval("Status") %></td>
                <td><%#Eval("Pay") %></td>
                <td><input type="button" id="StatusManage" value='<%#Eval("aaa") %>' /></td>
                <td><a href="">查看</a> &nbsp;&nbsp;<a href="">日志</a></td>
            </tr>
        </ItemTemplate>
    </cc1:CustomRepeater>
    </table>
</asp:content>
