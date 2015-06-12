<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="UserBackCenter.TicketsCenter.tenpay.Show" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style type="text/css">
        .div_line
        {
            height: 30px;
            width: 300px;
        }
        .div_line_left
        {
            width: 100px;
            height: 30px;
            text-align: right;
            float: left;
            font-size: 12px;
            line-height: 30px;
        }
        .div_line_right
        {
            width: 188px;
            height: 30px;
            text-align: left;
            float: left;
            padding-left: 10px;
            font-size: 12px;
            line-height: 30px;
        }
    </style>
</head>
<body id="Body1" runat="server">
    <form id="Form1" runat="server">
    <div style="width: 1004px; text-align: center">
        <div style=" height:25px;"> 
            <asp:Localize ID="lclMsg" runat="server"></asp:Localize></div>
        <div style="width: 300px; border: solid 1px #dadada">
            <div class="div_line">
                <div class="div_line_left">
                    套餐名称:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_name" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <div class="div_line_left">
                    类型:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_type" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line" id="panelHangKong" runat="server">
                <div class="div_line_left">
                    航空公司:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_air" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <div class="div_line_left">
                    运价数(条):</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_count" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <div class="div_line_left">
                    支付金额:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_price" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <div class="div_line_left">
                    开始时间:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_begin" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <div class="div_line_left">
                    结束时间:</div>
                <div class="div_line_right">
                    <asp:Label ID="lbl_msg_end" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="div_line">
                <asp:HiddenField ID="msg_hide_logId" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
