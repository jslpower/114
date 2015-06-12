<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSearchOrderInfo.aspx.cs"
    Inherits="SiteOperationsCenter.HotelManagement.AjaxSearchOrderInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
    <tr background="<%= ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
        <td height="23" width="4%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong >订单号</strong>
        </td>
        <td width="7%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong >采购用户名</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>订单状态</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>审核状态</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>旅客姓名</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>酒店名称</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>房型</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>入住日期</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>离店日期</strong>
        </td>
        <td width="4%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>间夜数</strong>
        </td>
        <td width="5%"  align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>总价</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>返佣比例</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>返佣金额</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>采购账户</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>订单操作</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>审核操作</strong>
        </td>
    </tr>
    <cc1:CustomRepeater ID="crptIOList" runat="server">
        <ItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center">
                    <%# Eval("ResOrderId")%>
                </td>
                <td height="25" align="center">
                    <a href="javascript:void(0)" onmouseover="OrderSearch.GetCompanyDetailInfo('<%# Eval("BuyerCId")%>',event,this);"
                        onmouseout="wsug(event, 0)">
                        <%# Eval("BuyerUName")%>
                </td>
                <td align="center" >
                    <%# Eval("OrderState")%>
                </td>
                <td align="center" >
                    <%# Eval("CheckState")%>
                </td>
                <td align="center" >
                    <%# GetGuests( ((EyouSoft.Model.HotelStructure.OrderInfo)GetDataItem()).ResGuests ) %>
                </td>
                <td align="center" >
                    <%# Eval("HotelName")%>
                </td>
                <td align="center" >
                    <%# Eval("RoomTypeName")%>
                </td>
                <td align="center" >
                    <%# Eval("CheckInDate","{0:yyyy-MM-dd}") %>
                </td>
                <td align="center" >
                    <%# Eval("CheckOutDate", "{0:yyyy-MM-dd}")%>
                </td>
                <td align="center" >
                    <%# Eval("RoomNight")%>
                </td>
                <td align="center" >
                    <%#  Utils.FilterEndOfTheZeroString(Eval("TotalAmount", "{0:C}"))%>
                </td>
                <td align="center" >
                    <%# Utils.GetDecimal(Eval("CommissionPercent").ToString()).ToString("f2") %>
                </td>
                <td align="center" >
                    <%# Utils.GetMoney(decimal.Round(Convert.ToDecimal(Eval("TotalAmount")) * Convert.ToDecimal(Eval("CommissionPercent"))))%>
                </td>
                <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.LookHotelAoccount("<%# Eval("BuyerCId")%>");return false;'>
                        查看</a>
                </td>
                <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.RES %>");return false;'>
                        处理中</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.CON %>");return false;'>
                        确认</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.CAN %>");return false;'>
                        取消</a>
                </td>
               <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.待审结) %>");return false;'>
                        待审结</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.入住正常) %>");return false;'>
                        已入住</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.NOWSHOW) %>");return false;'>
                        nowshow</a>       
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center">
                    <%# Eval("ResOrderId")%>
                </td>
                <td height="25" align="center">
                    <a href="javascript:void(0)" onmouseover="OrderSearch.GetCompanyDetailInfo('<%# Eval("BuyerCId")%>',event,this);"
                        onmouseout="wsug(event, 0)">
                        <%# Eval("BuyerUName")%>
                </td>
                <td align="center" >
                    <%# Eval("OrderState")%>
                </td>
                <td align="center" >
                    <%# Eval("CheckState")%>
                </td>
                <td align="center" >
                    <%# GetGuests( ((EyouSoft.Model.HotelStructure.OrderInfo)GetDataItem()).ResGuests ) %>
                </td>
                <td align="center" >
                    <%# Eval("HotelName")%>
                </td>
                <td align="center" >
                    <%# Eval("RoomTypeName")%>
                </td>
                <td align="center" >
                    <%# Eval("CheckInDate","{0:yyyy-MM-dd}") %>
                </td>
                <td align="center" >
                    <%# Eval("CheckOutDate", "{0:yyyy-MM-dd}")%>
                </td>
                <td align="center" >
                    <%# Eval("RoomNight")%>
                </td>
                <td align="center" >
                    <%#  Utils.FilterEndOfTheZeroString(Eval("TotalAmount", "{0:C}"))%>
                </td>
                <td align="center" >
                    <%# Utils.GetDecimal(Eval("CommissionPercent").ToString()).ToString("f2") %>
                </td>
                <td align="center" >
                    <%# Utils.GetMoney(decimal.Round(Convert.ToDecimal(Eval("TotalAmount")) * Convert.ToDecimal(Eval("CommissionPercent"))))%>
                </td>
                <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.LookHotelAoccount("<%# Eval("BuyerCId")%>");return false;'>
                        查看</a>
                </td>
                <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.RES %>");return false;'>
                        处理中</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.CON %>");return false;'>
                        确认</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelOrderState("<%# Eval("ResOrderId")%>","<%= EyouSoft.HotelBI.HBEResStatus.CAN %>");return false;'>
                        取消</a>   
                </td>
                <td align="center" >
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.待审结) %>");return false;'>
                        待审结</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.在入住) %>");return false;'>
                        已入住</a><br />
                    <a href="javascript:void(0)" onclick='OrderSearch.SetHotelCheckState("<%# Eval("ResOrderId")%>","<%=(int)( EyouSoft.Model.HotelStructure.CheckStateList.NOWSHOW) %>");return false;'>
                        nowshow</a>       
                </td>
            </tr>
        </AlternatingItemTemplate>
    </cc1:CustomRepeater>
    <tr background="<%= ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
        <td height="23" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>订单号</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>采购用户名</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>订单状态</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>审核状态</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>旅客姓名</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>酒店名称</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>房型</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>入住日期</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>离店日期</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>间夜数</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>总价</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>返佣比例</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>返佣金额</strong>
        </td>
        <td align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>采购账户</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>订单操作</strong>
        </td>
        <td width="5%" align="center" valign="middle" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
            <strong>审核操作</strong>
        </td>
    </tr>
</table>
<table width="99%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>
