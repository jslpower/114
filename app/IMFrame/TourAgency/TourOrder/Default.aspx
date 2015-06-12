<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMFrame.TourAgency.TourOrder.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/WebControls/IMTop.ascx" TagName="IMTop" TagPrefix="uc1" %>
<%--<%@ Register src="../../WebControls/SiteSelect.ascx" tagname="SiteSelect" tagprefix="uc1" %>--%>
<%@ Register Src="/WebControls/SubAccount.ascx" TagName="SubAccount" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我的订单</title>
    <style>
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        a.red
        {
            color: #cc0000;
        }
        a.red:visited
        {
            color: #cc0000;
        }
        a.red:hover
        {
            color: #ff0000;
        }
        .bar_on_comm
        {
            width: 100px;
            height: 21px;
            float: left;
            border: 1px solid #94B2E7;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 100px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(<%=ImageServerUrl %>/im/images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(<%=ImageServerUrl %>/im/images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
    </style>

    <script src="<%=JsManage.GetJsFilePath("jquery") %>" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">
        function ChangeUserData(obj) {
            UserId = obj.value;
        }
        function GetOrderDataList(AreaId) {
            var strUrl = "AjaxOrderTotal.aspx?AreaId=" + AreaId;
            $("#AjaxOrderTotal").html("正在加载....")
            $.ajax
            ({
                url: strUrl,
                cache: false,
                success: function(html) {
                    $("#AjaxOrderTotal").html(html);
                }
            });
        }
        $(function() {
            GetOrderDataList(0);
        })
    </script>

</head>
<body oncontextmenu="return false;" style="margin: 0px;">
    <form id="form1" runat="server">
    <uc1:IMTop ID="IMTop1" PageType="false" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" background="<%=ImageServerUrl %>/im/images/ztopbj.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="33" valign="bottom" background="<%=ImageServerUrl %>/im/images/ztopbj.gif">
                            <table width="210" border="0" height="32" align="center" cellpadding="0" cellspacing="0"
                                style="margin-top: 3px;">
                                <tr>
                                    <td width="100" height="30" align="center" id="ztop" style="background: url(<%=ImageServerUrl %>/im/images/ztopbtttt.gif) no-repeat center;
                                        height: 26px;">
                                        <img src="<%=ImageServerUrl %>/im/images/comments.gif" width="16" height="16" style="margin-bottom: -3px;" />
                                        <a href="/TourAgency/TourManger/TourAreaList.aspx">散客报名</a>
                                    </td>
                                    <td width="110" align="center" id="ztop2" style="background: url(<%=ImageServerUrl %>/im/images/ztopbtttt2.gif) no-repeat center;
                                        font-size: 14px; height: 30px;">
                                        <img src="<%=ImageServerUrl %>/im/images/shop.gif" width="16" height="16" style="margin-bottom: -3px;" />
                                        <a href="/TourAgency/TourOrder/Default.aspx"><font color="#cc0000"><strong>我的订单</strong></font></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" background="<%=ImageServerUrl %>/im/images/topzzh.gif" style="padding: 3px 0 0 3px;">
                <uc2:SubAccount ID="SubAccount1" runat="server" />
                <div runat="server" id="divUserManage" visible="false">
                    <a href="" runat="server" id="a_SonAccountManage">子账号管理</a> | <a href="" runat="server"
                        id="a_AddSonAccount">添加子账号</a></div>
            </td>
        </tr>
    </table>
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <div id="div_ZTOrder" runat="server">
                    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 8px;">
                        <tr>
                            <td align="left">
                                <img src="<%=ImageServerUrl %>/im/images/icobu.gif" width="16" height="16" style="margin-bottom: -3px;" /><strong>
                                    订单管理</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src="<%=ImageServerUrl %>/im/images/lineb.gif" width="210" height="6" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <tr>
                                    <td align="left">
                                        <div style="color: #333333; padding-top: 3px; height: 20px; background: #F3FAFF;">
                                            当前为所有订单均为未发团订单。</div>
                                        <div id="AjaxOrderTotal">
                                        </div>
                                    </td>
                                </tr>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
