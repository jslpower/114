<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPreview.aspx.cs" ValidateRequest="false" Inherits="UserBackCenter.RouteAgency.RouteManage.PrintPreview" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>打印预览</title>
    <style type="text/css">
    body {color:#000000;font-size:12px;font-family:"宋体";background:#fff; margin:0px;}
    img {border: thin none;}
    table{border-collapse:collapse;}
    td {font-size: 12px; line-height:18px;color: #000000;  }	
    .headertitle {font-family:"黑体"; font-size:25px; line-height:120%; font-weight:bold;}
    #divPrintPreview table {width:790px}
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">    
    <div id="divPrintConfig" style="width:100%">
        <div style="display:none">
            <input type="hidden" id="txtPrintHTML" name="txtPrintHTML" />
            <input type="hidden" id="txtRouteName" name="txtRouteName" style="display:none" />
            <asp:Button runat="server" ID="btnWordPrint" Text="导出到word" OnClick="btnWordPrint_Click" />
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
            <tr>
                <td height="31" valign="bottom" bgcolor="#FFE08B" style="border-bottom: 1px solid #DCAE30;">
                    <table width="790" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>                            
                            <td align="center" valign="bottom" style="text-align:right">
                                <a href="javascript:void(0)" onclick="printConfig.printPage()"><img src="<%=ImageServerPath %>/images/zjprint.gif" alt="直接打印" width="80" height="22" border="0" /></a> 
                                <a href="javascript:void(0)" onclick="return printConfig.wordPrint()"><img src="<%=ImageServerPath %>/images/dcprint.gif" alt="导出为word格式文件，在word里编辑打印" width="80" height="22" border="0" /></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPrintPreview">
    </div>
    <script type="text/javascript">
        var printConfig = {
            printPage: function() {
                this.$("divPrintConfig").style.display = "none";
                if (window.print != null) {
                    window.print();
                } else {
                    alert('没有安装打印机');
                }
                window.setTimeout(function() {
                    printConfig.$("divPrintConfig").style.display = '';
                }, 500);
            },
            wordPrint: function() {
                this.$("<%=btnWordPrint.ClientID %>").click();
                return false;
            },
            $: function(_id) {
                return document.getElementById(_id);
            }
        };
        
        try {
            var printHTML = window.opener.document.getElementById("txtPrintHTML").value;
            printConfig.$("divPrintPreview").innerHTML = printHTML;
            printConfig.$("txtPrintHTML").value = printHTML;
            var pageTitle = "";
            var parentTitle = window.opener.document.title;
            if (parentTitle && parentTitle.length > 0) {
                pageTitle = parentTitle + "-打印预览";
            } else {
                pageTitle = "打印预览";
            }
            document.title = pageTitle;
        } catch (e) { }
    </script>
    </form>
</body>
</html>

