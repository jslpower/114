<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRecord.aspx.cs" Inherits="SiteOperationsCenter.Statistics.LoginRecord" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            background-color: #EBF4FF;
            margin: 0;
            color: #333333;
            font-family: "宋体" ,Arial,Helvetica,sans-serif;
            font-size: 12px;
        }
        table
        {
            border-collapse: collapse;
        }
        td
        {
            font-size: 12px;
            line-height: 20px;
        }
        .lr_hangbg
        {
            color: #000000;
            font-size: 12px;
            line-height: normal;
        }
        .mainbody
        {
            margin-left: auto;
            margin-right: auto;
        }
        .tab_luan
        {
            background-color: #2788DD;
            color: #FFFFFF;
            font-size: 12px;
            height:22px;
        }
        .font12_writh
        {
            color: #333333;
            font-size: 12px;
            text-align: center;
        }
        .lr_hangbg2
        {
            background-color: #f3f7ff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainbody">
        <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" class="tab_luand">
            <tr class="tab_luan">
                <th class="font12_writh" style=" width:50px;">
                    序号
                </th>
                 <th class="font12_writh" style=" width:90px;">
                    用户名
                </th>
                <th  style=" width:130px;" class="font12_writh">
                    时间
                </th>
                <th class="font12_writh">
                    城市(IP)
                </th>
            </tr>
             <tr runat="server" id="NoData">
                <th colspan="3" style="text-align:center; padding:8px;">
                    该公司暂无登录记录！
                </th>               
            </tr>
            <asp:Repeater runat="server" ID="rpt_LoginInfo" 
                onitemdatabound="rpt_LoginInfo_ItemDataBound">
                <ItemTemplate>
                    <tr class="lr_hangbg">
                        <td align="center" >
                            <asp:Literal ID="ltrXH" runat="server"></asp:Literal>                            
                        </td>
                        <td align="center">
                            <%#Eval("OperatorName")%>
                        </td>
                        <td align="center">
                            <%#Eval("EventTime")%>
                        </td>
                        <td>
                            <%#Eval("EventArea") %>(<%#Eval("EventIP")%>)
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="lr_hangbg2">
                       <td align="center">
                            <asp:Literal ID="ltrXH" runat="server"></asp:Literal>
                        </td>
                         <td align="center">
                            <%#Eval("OperatorName")%>
                        </td>
                        <td align="center">
                            <%#Eval("EventTime")%>
                        </td>
                        <td>
                            <%#Eval("EventArea") %>(<%#Eval("EventIP")%>)
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
        <div style=" text-align:right;">
            <cc3:exportpageinfo id="ExportPageInfo1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
