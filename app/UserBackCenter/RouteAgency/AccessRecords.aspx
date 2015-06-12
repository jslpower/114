<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessRecords.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AccessRecords" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-color: #EAF3FB;
        }
        textarea
        {
            border: 1px solid #7F9DB9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="left">
                <strong>该团队访问记录</strong>
            </td>
        </tr>
    </table>
    <table id="tblCustomerList" cellspacing="1" cellpadding="0" width="100%" align="center"
        bgcolor="#93B5D7" border="0" sortcol="5">
        <tbody>
            <tr align="middle" bgcolor="#f9f9f4">
                <td width="6%" height="20" align="center" bgcolor="#D4E6F7">
                    序号
                </td>
                <td width="28%" align="center" bgcolor="#D4E6F7">
                    组团社名称<br />
                </td>
                <td width="20%" align="center" bgcolor="#D4E6F7">
                    浏览人<br />
                </td>
                <td width="12%" align="center" bgcolor="#D4E6F7">
                    联系电话<br />
                </td>
                <td width="11%" align="center" bgcolor="#D4E6F7">
                    手机<br />
                </td>
                <td width="10%" align="center" bgcolor="#D4E6F7">
                    QQ<br />
                </td>
                <td width="15%" align="center" bgcolor="#D4E6F7">
                    浏览时间
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rptRecords" OnItemDataBound="rpt_AccessRecords_ItemDataBound">
                <ItemTemplate>
                    <tr bgcolor="#ffffff">
                        <td align="middle" bgcolor="#EDF6FF">
                            <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                            <!--序号-->
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("ClientCompanyName")%>
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("ClientUserContactName")%>
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("ClientUserContactTelephone")%>
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("ClientUserContactMobile")%>
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("ClinetUserContactQQ")%>
                        </td>
                        <td align="middle" bgcolor="#EDF6FF">
                            <%#Eval("VisitedTime","{0:yyyy-MM-dd}")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <tr bgcolor="#ffffff" runat="server" id="NoData" visible="false">
                <td colspan="7" align="middle" bgcolor="#EDF6FF">
                    暂无访问记录谢谢！
                </td>
            </tr>
        </tbody>
    </table>
    <div id="ExportPage" class="F2Back" style="text-align: right;" height="40">
        <cc2:exportpageinfo id="ExportPageInfo1" currencypagecssclass="RedFnt" linktype="4"
            runat="server"></cc2:exportpageinfo>
    </div>
    </form>
</body>
</html>
