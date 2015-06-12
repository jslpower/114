<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketLogs.aspx.cs" Inherits="SiteOperationsCenter.Others.TicketLogs" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>机票接口访问记录</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>
    <style type="text/css">
    .lbLogs{width:100%; border-left:1px solid #333;border-top:1px solid #333;}
    .lbLogs thead td{height:30px; line-height:30px; text-align:center;}
    .lbLogs tr td{height:30px;line-height:30px;}
    .lbLogs tfoot td{height:30px;line-height:30px;}
    .lbLogs td{border-right:1px solid #333;border-bottom:1px solid #333;}
    .lbLogs .bg1{ background:#efefef;}
    .lbLogs .bg2{background:#ffffff;}
    
    #divPage {	padding-right: 3px; padding-left: 3px; padding-bottom: 3px; margin: 3px; margin-top:10px; padding-top: 3px; text-align: center}
    #divPage a {	border: #54A11C 1px solid; padding-right: 5px;padding-left: 5px; padding-bottom: 2px; margin: 2px;  color: #54A11C; padding-top: 2px;text-decoration: none}
    #divPage a:hover {	border: #54A11C 1px solid; background:#54A11C; color: #fff;}
    #divPage a:active {	border: #54A11C 1px solid;  color: #000; }
    #divPage span.current {	border: #54A11C 1px solid; padding-right: 5px;padding-left: 5px; font-weight: bold; padding-bottom: 2px; margin: 2px; color: #fff; padding-top: 2px; background-color: #54A11C}
    #divPage span.disabled {	border: #eee 1px solid; padding-right: 5px;  padding-left: 5px; padding-bottom: 2px; margin: 2px;  color: #ddd; padding-top: 2px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater runat="server" ID="rptLogs">
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" class="lbLogs">
                    <thead class="bg1">
                        <td>
                            公司名称
                        </td>
                        <td>
                            联系人
                        </td>
                        <td>
                            手机
                        </td>
                        <td>
                            电话
                        </td>
                        <td>
                            QQ
                        </td>
                        <td>
                            总访问次数
                        </td>
                        <td>
                            一周内访问次数
                        </td>
                        <td>
                            最近访问时间
                        </td>
                        <td>总|一周内登录</td>
                    </thead>       
            </HeaderTemplate>            
            <ItemTemplate>
                <tr <%# ((Container.ItemIndex+1)%2==0)? "class='bg1'":"class='bg2'"%> >
                    <td>
                        &nbsp;<%#Eval("CompanyName") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("ContactName") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("ContactMobile") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("ContactTelephone") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("ContactQQ") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("TotalTimes") %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("WeekTimes") %>
                    </td>
                    <td>
                        &nbsp;<%#string.Format("{0:yyyy-MM-dd}",Eval("LatestDate")) %>
                    </td>
                    <td>
                        &nbsp;<%#Eval("LoginNumber") %>&nbsp;|&nbsp;<%#Eval("WeekLoginNumber")%>
                    </td>
                </tr>
            </ItemTemplate>  
            <FooterTemplate>
                <tfoot>
                    <td colspan="9">
                        <div id="divPage"></div>
                    </td>
                </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        <asp:PlaceHolder runat="server" ID="phNotFound" Visible="false">
            <div style="line-height:30px; text-align:center;">没有任何查询机票接口的信息！</div>
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
