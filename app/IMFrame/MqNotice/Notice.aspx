<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notice.aspx.cs" Inherits="IMFrame.MqNotice.Notice" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<html>
<head runat="server">
    <title>公告</title>
    <style type="text/css">
        body
        {
            border: 0px;
             margin-top:3px;
             margin-left:3px;
            overflow: hidden
        }
         A:visited
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:active
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:link
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:hover
        {
            color: rgb(255,2,4);
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="font-size: 12px; border: 0px; width:150px;">
            <tbody>
                <cc1:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                               <%# Container.ItemIndex+1 %>.&nbsp;<a  href="<%#GetUrl(Eval("ID"))%>" title="<%#Eval("Title")%>"
                                    target="_blank"><%# EyouSoft.Common.Utils.GetText(Eval("Title").ToString(),8,true)%></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc1:CustomRepeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
