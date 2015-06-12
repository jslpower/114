<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnLine.aspx.cs" Inherits="TourUnion.WEB.IM.Search.OnLine" %>

<%@ Register Assembly="ExporPage" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MQ在线人员</title>
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
    </style>

    <script type="text/javascript" src="/IM/js/more_JS.js"></script>

    <script type="text/javascript">
        function CheckFormAdd(im_userid,CustomerCompanyId)
        {           
        	var boolFals=CheckOrbberInstalled(); 
            if(boolFals==true)
            { 
	            location.href='tongye114://mored.orb/cmd=add?uid=' + im_userid;
            }
            else
            {
	            alert("您没有安装同业通即时通信系统, 点击 '确定' 后下载安装!");
	            location.href='http://www.tongye114.com/IM/DownLoad/tongyetong.exe';
            }
        }       
    </script>

</head>
<body oncontextmenu="return false;" scroll=no>
    <form id="form1" runat="server">
    <table width="504" height="320" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top">
                公司类型:<asp:DropDownList id="ddlCompanyType" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">批发商</asp:ListItem>
                    <asp:ListItem Value="2">组团社</asp:ListItem>
                </asp:DropDownList>
                 姓名:
                <input type="text" id="txtContactName" runat="server" name="ContactName" size="4" />
                单位名称:
                <input type="text" id="txtCompanyName" runat="server" name="CompanyName" size="5" />
                城市:
                <input type="text" id="txtCityName" runat="server" name="CityName" size="3" />
                <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="1" cellpadding="2" cellspacing="1" bordercolorlight="#999999"
                            bordercolordark="#ffffff" bgcolor="#ffffff" style="border: 1px solid #cccccc;">
                            <tr>
                                <td width="17%" align="left" bgcolor="#eeeeee">
                                    姓名
                                </td>
                                <td width="30%" align="left" bgcolor="#eeeeee">
                                    单位名称
                                </td>
                                <%--<td width="31%" align="left" bgcolor="#eeeeee">
                                    经营专线
                                </td>--%>
                                <td width="10%" align="left" bgcolor="#eeeeee">
                                    所在城市
                                </td>
                                <td width="10%" align="left" bgcolor="#eeeeee">
                                    加为联系人
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Literal ID="LiteralTr" runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="NoData" runat="server">
                <table width="100%" border="1" cellpadding="2" cellspacing="1" bordercolorlight="#999999"
                    bordercolordark="#ffffff" bgcolor="#ffffff" style="border: 1px solid #cccccc;">
                    <tr>
                        <td width="17%" align="left" bgcolor="#eeeeee">
                            姓名
                        </td>
                        <td width="30%" align="left" bgcolor="#eeeeee">
                            单位名称
                        </td>
                       <%-- <td width="31%" align="left" bgcolor="#eeeeee">
                            经营专线
                        </td>--%>
                        <td width="10%" align="left" bgcolor="#eeeeee">
                            所在城市
                        </td>
                        <td width="10%" align="left" bgcolor="#eeeeee">
                            加为联系人
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10" style="height: 100; text-align: center">
                            暂无数据
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <table width="504" border="0" align="center" id="ExproPage" runat="server" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td height="10" align="center">
                                <cc1:exportpageinfo id="ExportPageInfo1" runat="server"></cc1:exportpageinfo>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="hi_UserId" runat="server" />
     <asp:HiddenField ID="hi_CompanyTypeId" runat="server" />
    </form>
</body>
</html>