<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUser.aspx.cs" Inherits="IMFrame.RouteAgency.TourManger.SetUser" EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消息提醒设置</title>
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
            color: #2F1004;
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
            color: #2F1004;
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
        a.bbl
        {
            color: #000066;
        }
        a.bbl:visited
        {
            color: #000066;
        }
        a.bbl:hover
        {
            color: #ff0000;
        }
        .bar_on_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            border: 1px solid #E7C994;
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
        .miz ul
        {
            margin: auto 0;
            padding: 0;
        }
        .miz li
        {
            display: inline;
            float: left;
        }
    </style>
</head>
<body>
    <form runat="server" id="form1">
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="line-height: 15px; color: #999999;">
                            消息设置，您可以分别设置线路区域的订单提醒由哪些账号接收。
                        </td>
                    </tr>
                </table>
                <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            请选择区域类型<select><option>国内长线</option><option>国际线</option><option>周边游</option></select>
                            &nbsp;
                        </td>
                        <td>
                            <table border="0" align="left" cellpadding="0" id="IsSettionTable" cellspacing="0"
                                style="border: 1px solid #EF9739; background: #FFF8E2; display: none">
                                <tr>
                                    <td align="center">
                                        <label id="labIsSettion">
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                
                <asp:Repeater ID="RepeaterList" runat="server">
                <ItemTemplate>
                    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:8px;">
                      <tr>
                        <td align="left"><img src="<%=ImageServerUrl %>/im/images/icobu.gif" width="16" height="16" style="margin-bottom:-3px;" /><strong><%# ((EyouSoft.Model.SystemStructure.AreaBase)DataBinder.GetDataItem(Container)).AreaName %></strong></td>
                        <td>
                            <table border="0" align="left" cellpadding="0" id="table_msg_<%# ((EyouSoft.Model.SystemStructure.AreaBase)DataBinder.GetDataItem(Container)).AreaId %>" cellspacing="0" style="border: 1px solid #EF9739; background: #FFF8E2; display: none">
                                <tr>
                                    <td align="center"></td>
                                </tr>
                            </table>
                        </td>
                      </tr>
                      <tr>
                        <td align="center" colspan="2"><img src="<%=ImageServerUrl %>/im/images/lineb.gif" width="210" height="6" /></td>
                      </tr>
                      <tr>
                        <td align="left" colspan="2">
		                <div class="miz">
			                <ul>
			                    <div id="div_User_<%# ((EyouSoft.Model.SystemStructure.AreaBase)DataBinder.GetDataItem(Container)).AreaId %>"></div>
			                </ul>
                        </div>
                        </td>
                      </tr>
                    </table>      
                </ItemTemplate>
                </asp:Repeater>          
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="35" align="center">
                            <input type="button" id="btnEnd" onclick="SetEnd();" value="结束设置" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
