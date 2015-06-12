<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetExpenseDetails.aspx.cs"
    Inherits="UserBackCenter.SMSCenter.GetExpenseDetails" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<cc2:CustomRepeater ID="GetExpenseDetail_repExpenseDetail" runat="server">
  <HeaderTemplate>
<table width="80%" border="1" cellpadding="2" cellspacing="0" bordercolor="#C9DEEF">
    <tr align="center">
        <td width="30%" bgcolor="#E4F3FF">
            发送时间
        </td>
        <td width="35%" bgcolor="#E4F3FF">
            号码
        </td>
        <td width="10%" bgcolor="#E4F3FF">
            金额
        </td>
        <td width="12%" bgcolor="#E4F3FF">
            内容
        </td>
        <td width="13%" bgcolor="#E4F3FF">
            短信通道
        </td>
    </tr>
  </HeaderTemplate>
      <ItemTemplate>
         <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
        <td>
           <%# Convert.ToDateTime(Eval("IssueTime").ToString())%>
        </td>
        <td>
            <a href="javascript:void(0)"  onclick='return AccountInfo.GetSendDetails("0","<%# Eval("TotalId") %>","1");'>发送成功号码</a>(<%#Eval("SuccessCount")%>)
            <div><a href="javascript:void(0)" onclick=' return AccountInfo.GetSendDetails("0","<%# Eval("TotalId") %>","2");' >发送失败号码</a>(<%#Eval("ErrorCount")%>)</div>
        </td>
        <td>
           <%#Convert.ToDecimal(Eval("UseMoeny").ToString()).ToString("F2")%>元
        </td>
        <td>
            <a href="javascript:void(0)" onclick=' return AccountInfo.GetSendDetails("1","<%# Eval("TotalId") %>","0");'>内容</a>
        </td>
        <td>
            <%# ((EyouSoft.Model.SMSStructure.SMSChannel)Eval("SendChannel")).ChannelName %>
        </td>
    </tr>
    </ItemTemplate>
        <FooterTemplate>
     
      </table>
        </FooterTemplate>
</cc2:CustomRepeater>
<table width="80%" border="1" id="GetExpenseDetail_tbSumCountAndMoney" runat="server" visible="false"
    cellpadding="2" cellspacing="0" bordercolor="#C9DEEF">
    <tr>
        <td bgcolor="#f5f5f5" width="30%">
            汇总：
        </td>
        <td bgcolor="#f5f5f5" width="35%">
            共<asp:label runat="server" id="GetExpenseDetail_labSumSendCount" text="0"></asp:label>条
        </td>
        <td bgcolor="#f5f5f5" width="35%">
            共<asp:label runat="server" id="GetExpenseDetail_labSumSendMoney" text="0"></asp:label>元
        </td>
    </tr>
</table>
<table width="100%" border="0" runat="server" id="GetExpenseDetail_tbExporPageInfo" cellpadding="2"
    cellspacing="0">
    <tr>
        <td align="right" >
        <div class="digg">
            <cc1:ExporPageInfoSelect ID="GetExpenseDetail_ExporPageInfoSelect" PageStyleType="NewButton" runat="server" />
        </div>
        </td>
    </tr>
</table>
