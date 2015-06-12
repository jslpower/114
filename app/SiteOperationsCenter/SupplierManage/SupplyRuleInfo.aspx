<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplyRuleInfo.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.SupplyRuleInfo" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <title>供求规则（5条）</title>
    <style>
        #jiaodianList
        {
            list-style: none;
            padding-left: 0;
        }
        #jiaodianList li input
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td style="border-bottom: 1px solid;" height="20">
                <strong>
                    <img src="<%= ImageServerUrl %>/images/yunying/icn_pen02.gif" width="13" height="13"></strong>
                供求管理 &gt; 供求规则（5条）
            </td>
        </tr>
    </table>
        <table width="960" border="1" cellspacing="0" cellpadding="8" style="border: 1px solid #ccc;
            padding: 1px;">
            <tr>
                <td height="26" align="right" bgcolor="#C0DEF3">
                    <strong>供求规则头条</strong>
                </td>
                <td height="26">
                    新闻标题：
                    <input type="text" id="txtsupTopTitle" runat="server" name="txtsupTopTitle" size="40" value="" />
                    <br />
                    连接地址：
                    <input type="text" id="txtsupTopHref" runat="server" name="txtsupTopHref" size="40" value="" />
                    <br />
                    新闻概要：
                    <textarea type="text" runat="server" id="txtsupRuleDescription" name="txtsupRuleDescription" size="40" value="" cols="39" rows="6"></textarea>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    <p>
                        <strong>供求规则一</strong></p>
                </td>
                <td height="28">
                    <p>
                        标&nbsp;&nbsp;&nbsp;&nbsp;题：
                        <input type="text" runat="server" name="txtRuleTitle1" id="txtRuleTitle1" size="40" value="" />
                        <br />
                        连&nbsp;&nbsp;&nbsp;&nbsp;接：
                        <input type="text"  runat="server" name="txtRuleHref1" id="txtRuleHref1" size="40" value="" />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    <p>
                        <strong>供求规则二</strong></p>
                </td>
                <td height="28">
                    <p>
                        标&nbsp;&nbsp;&nbsp;&nbsp;题：
                        <input type="text" runat="server" name="txtRuleTitle2" id="txtRuleTitle2" size="40" value="" />
                        <br />
                        连&nbsp;&nbsp;&nbsp;&nbsp;接：
                        <input type="text" runat="server" name="txtRuleHref2" id="txtRuleHref2" size="40" value="" />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    <p>
                        <strong>供求规则三</strong></p>
                </td>
                <td height="28">
                    <p>
                        标&nbsp;&nbsp;&nbsp;&nbsp;题：
                        <input type="text" runat="server" name="txtRuleTitle3" id="txtRuleTitle3" size="40" value="" />
                        <br />
                        连&nbsp;&nbsp;&nbsp;&nbsp;接：
                        <input type="text" runat="server" name="txtRuleHref3" id="txtRuleHref3" size="40" value="" />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    <p>
                        <strong>供求规则四</strong></p>
                </td>
                <td height="28">
                    <p>
                        标&nbsp;&nbsp;&nbsp;&nbsp;题：
                        <input type="text" runat="server" name="txtRuleTitle4" id="txtRuleTitle4" size="40" value="" />
                        <br />
                        连&nbsp;&nbsp;&nbsp;&nbsp;接：
                        <input type="text" runat="server" name="txtRuleHref4" id="txtRuleHref4" size="40" value="" />
                        <br />
                    </p>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    &nbsp;
                </td>
                <td height="28">
                    <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
