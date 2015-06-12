<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTicketMessage.aspx.cs"
    Inherits="SiteOperationsCenter.AdvertiseTicket.EditTicketMessage" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .input1
        {
            border-right: #082d71 1px solid;
            border-top: #082d71 1px solid;
            font-size: 9pt;
            border-left: #082d72 1px solid;
            color: #000000;
            border-bottom: #082d71 1px solid;
            background-color: #ffffff 12;
        }
        .eali ul
        {
            margin: auto 0;
            padding: 0;
        }
        .eali li
        {
            display: inline;
            float: left;
            padding: 2px 5px;
        }
        .eali li a.lion
        {
            background: #ff6600;
            font-size: 14px;
            color: #ffffff;
            padding: 2px 5px;
        }
        .eali li a.lion:hover
        {
            background: #ff6600;
            font-size: 14px;
            padding: 2px 5px;
        }
        .eali a
        {
            font-size: 14px;
            padding: 2px 5px;
        }
        .eali a:hover
        {
            background: #F1B790;
            font-size: 14px;
            padding: 2px 5px;
        }
        .eali a:visited
        {
            font-size: 14px;
        }
        .addInf_con tr td
        {
            padding-left: 55px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px #cccccc solid;
        margin-bottom: 10px;">
        <tr>
            <td height="30" align="center">
                <strong style="font-size: 14px;">
                    <asp:Literal ID="literTicketArticleTitle" runat="server"></asp:Literal></strong>
            </td>
        </tr>
        
    </table>
    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8"
        bgcolor="#FAFAFA" class="addInf_con">
        <tr>
            <td height="30" align="left" bgcolor="#C0DEF3">
                航班类型：
            </td>
            <td width="85%" height="30" align="left" id="tdVoyageSet">
                <asp:Literal ID="literVoyageSet" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                乘机人数：
            </td>
            <td align="left">
                <asp:Literal ID="leterSumPeople" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                <span class="unnamed1">*</span>出发时间：
            </td>
            <td align="left">
                <cc1:DatePicker ID="txtTakeOffDate" runat="server" Width="100px" />&nbsp;
                  <span id="errMsg_txtTakeOffDate" class="unnamed1" style="display:none">请选择出发时间</span>
            </td>
        </tr>
        <tr  id="trReturnDate"  style="display:none">
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                <span class="unnamed1">*</span>返程时间：
            </td>
            <td align="left">
                <cc1:DatePicker ID="txtReturnDate" runat="server" Width="100px" />&nbsp;
                  <span id="errMsg_txtReturnDate" class="unnamed1"  style="display:none">请选择返程时间</span>
                  <span id="errMsg_BtweenDate" class="unnamed1"  style="display:none">返程时间不能小于出发时间</span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                乘客类型：
            </td>
            <td align="left">
                <asp:Literal ID="literPeopleCountryType" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="15%" height="120" align="left" bgcolor="#C0DEF3">
                备注：
            </td>
            <td align="left">
                <textarea name="txtRemark" id="txtRemark" runat="server" cols="60" rows="6"></textarea>
            </td>
        </tr>
        <tr>
            <td height="40" colspan="2" align="left">
                <span style="color: #FF0000; font-size: 14px; font-weight: bold;">采购商资料</span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                <span class="unnamed1">*</span>公司名称：
            </td>
            <td align="left">
                <asp:TextBox ID="txtCompanyName" runat="server" Width="214px" valid="required" errmsg="请输入单位名称" ></asp:TextBox>
                <span id="errMsg_txtCompanyName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                <span class="unnamed1">*</span>联系人：
            </td>
            <td align="left">
                <asp:TextBox ID="txtContactName" runat="server" Width="94px"  valid="required" errmsg="请输入联系人姓名"></asp:TextBox>
          <span id="errMsg_txtContactName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3" >
                <span class="unnamed1">*</span>联系电话：
            </td>
            <td align="left">
                <asp:TextBox ID="txtContactTel" runat="server"  valid="required|isPhone" errmsg="请输入联系电话|请填写正确的联系电话"></asp:TextBox>
            <span id="errMsg_txtContactTel" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="30" align="left" bgcolor="#C0DEF3">
                <span class="unnamed1">*</span>联系地址：
            </td>
            <td align="left">
                <asp:TextBox ID="txtContactAddress" runat="server" Width="341px" valid="required" errmsg="请输入联系地址"> </asp:TextBox>
            <span id="errMsg_txtContactAddress" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td width="15%" height="40" align="left">
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerUrl %>/images/yunying/bc_btn.jpg"
                    width="88" height="26" style="cursor: pointer" id="btnSave" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                        src="<%=ImageServerUrl %>/images/yunying/qx_btn.jpg" width="88" height="26" id="btnCancel"
                        style="cursor: pointer" />
            </td>
        </tr>
        <input  type="hidden" runat="server" id="hidEditId" name="hidEditId" />
       
    </table>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            if ($.trim($("#tdVoyageSet").html()) != "单程") {
                $("#trReturnDate").show();
            }
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));
            $("#txtTakeOffDate_dateTextBox").focus(function() { $("#errMsg_txtTakeOffDate").hide(); });
            $("#txtReturnDate_dateTextBox").focus(function() { $("#errMsg_txtReturnDate").hide(); $("#errMsg_BtweenDate").hide(); });

            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                var isTrue = true;
                if ($.trim($("#txtTakeOffDate_dateTextBox").val()) == "") {
                    $("#errMsg_txtTakeOffDate").show();
                    isTrue = false;
                }
                if ($.trim($("#tdVoyageSet").html()) != "单程" && $.trim($("#txtReturnDate_dateTextBox").val()) == "") {
                    $("#errMsg_txtReturnDate").show();
                    isTrue = false;
                }
                if ($.trim($("#txtTakeOffDate_dateTextBox").val()) != "" &&($.trim($("#tdVoyageSet").html()) != "单程"&& $.trim($("#txtReturnDate_dateTextBox").val()) != "")) {
                    var startDate = $.trim($("#txtTakeOffDate_dateTextBox").val());
                    var returnDate = $.trim($("#txtReturnDate_dateTextBox").val());
                    var NewstartDate = new Date(startDate.split("-")[0], parseInt(startDate.split("-")[1], 10), parseInt(startDate.split("-")[2], 10));
                    var NewreturnDate = new Date(returnDate.split("-")[0], parseInt(returnDate.split("-")[1], 10), parseInt(returnDate.split("-")[2], 10));

                    if (NewstartDate > NewreturnDate) {
                        $("#errMsg_BtweenDate").show();
                        isTrue = false;
                    }
                }
                var isvalid = ValiDatorForm.validator(form, "span");
                if (isTrue && isvalid) {
                    $.ajax({
                        type: "POST",
                        dataType: 'html',
                        url: "EditTicketMessage.aspx?Type=Update",
                        data: $("#form1").serializeArray(),
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                alert("修改成功!");
                                window.location.href = "<%=strReturnUrl %>";
                            } else {
                                alert("修改失败!");
                                return false;
                            }
                        }
                    });
                }
            });
            $("#btnCancel").click(function() {
                window.location.href = "<%=strReturnUrl %>";
            });
        });
    </script>
    </form>
</body>
</html>
