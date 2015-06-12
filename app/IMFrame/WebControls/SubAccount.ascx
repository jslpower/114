<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubAccount.ascx.cs"
    Inherits="IMFrame.WebControls.SubAccount" %>

<div style="width: 210px; clear: both; padding-top: 5px;z-index:-1" id="divOperator" >
    共<%= SubAccountCount %>个子账号<asp:DropDownList ID="ddlOperator" runat="server" style=" z-index:-1">
    </asp:DropDownList>
</div>
