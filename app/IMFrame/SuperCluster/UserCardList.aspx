<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCardList.aspx.cs" Inherits="IMFrame.SuperCluster.UserCardList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<style>
    body
    {
        font-size: 9pt;
    }
    a
    {
        color: #828282;
        text-decoration: none;
    }
    a:hover
    {
        color: #F30;
    }
    a img
    {
        border: 0;
    }
    ul, li
    {
        margin: 0;
        padding: 0;
        list-style: none;
    }
    #tags ul
    {
        width: 100%;
        clear: both;
        width: 100%;
        height: 26px;
        border-bottom: 1px solid #d3d3d3;
        margin-bottom: 10px;
    }
    .clear
    {
        clear: both;
        width: 100%;
        margin: 0;
        padding: 0;
        overflow: hidden;
        height: 0px;
    }
    #tags li
    {
        float: left;
        width: 96px;
        height: 26px;
        text-align: center;
        line-height: 26px;
        position: relative;
        top: 1px;
    }
    #tags li a
    {
        display: block;
    }
    #tags li.selectTag
    {
        background: url(<%=ImageServerUrl%>/IM/images/bgg.gif) no-repeat left top;
        position: relative;
        top: 1px;
    }
</style>
<body style="margin: 0; padding: 0; border: 0;">
    <div class="hr-5">
    </div>
    <div id="listContent">
    <div id="tags">
<ul>
  <li><a href="<%=ModeUrl%>">名片模式</a> </li>

  <li class="selectTag"><a href="<%=ListUrl%>">列表模式</a> </li>
  </ul>
</div>
        <div class="clear">
        </div>
        <div style="clear: both; width: 100%; height: 0; margin: 0; padding: 0; overflow: hidden">
        </div>
        <style>
            .page a:link, .page a:active, .page a:visited
            {
                color: #074387;
                text-decoration: none;
            }
            .page a:hover
            {
                color: #F30;
            }
        </style>
        <style>
            .list
            {
                border-collapse: collapse;
                border-right: 1px solid #DBE2E7;
                border-top: 1px solid #DBE2E7;
            }
            .list td
            {
                border-bottom: 1px solid #d3d3d3;
                border-left: 1px solid #d3d3d3;
                overflow: hidden;
                padding: 5px 0;
                text-align: center;
            }
            .list .topbg
            {
                background: url(<%=ImageServerUrl%>/IM/images/tablethbg.gif) repeat-x left top;
            }
        </style>
        <table width="100%" cellpadding="0" cellspacing="0" class="list">
            <tr class="topbg">
                <td width="22%" valign="middle">
                    姓名
                </td>
                <td width="24%" valign="middle">
                    公司
                </td>
                <td width="24%" valign="middle">
                    主营业务
                </td>
                <td width="20%" valign="middle">
                    联系电话
                </td>
                <td width="10%" valign="middle">
                    MQ
                </td>
            </tr>
            <cc2:CustomRepeater ID="Repeater1" runat="server" EmptyText="<tr ><td colspan=5>无数据显示!</td></tr>">
                <ItemTemplate>
                    <tr>
                        <td width="22%" valign="middle">
                            <%#EyouSoft.Common.Utils.GetText(Eval("UserName").ToString(),16)%>
                        </td>
                        <td width="26%" valign="middle">
                            <%#EyouSoft.Common.Utils.GetText(Eval("CompanyName").ToString(),16)%>
                        </td>
                        <td width="26%" valign="middle">
                            <%#EyouSoft.Common.Utils.GetText(Eval("Subject").ToString(),16)%>
                        </td>
                        <td width="16%" valign="middle">
                            <%#Eval("Contact")%>
                        </td>
                        <td width="10%" valign="middle">
                            <%#EyouSoft.Common.Utils.GetMQ(Eval("MQ").ToString())%>
                        </td>
                    </tr>
                </ItemTemplate>
            </cc2:CustomRepeater>
        </table>
        <!--分页 开始-->
        <div align="center">
        <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
        </div>
        <!--分页 结束-->
    </div>
</body>
</html>
