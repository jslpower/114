<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutePrint.aspx.cs" ValidateRequest="false"
    Inherits="UserBackCenter.RouteAgency.RouteManage.RoutePrint" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        <!
        -- BODY
        {
            background: #E2F4FF;
        }
        .printbox
        {
            border: 1px solid #CE965F;
            margin-top: 10px;
            background: #ffffff;
        }
        .padding2
        {
            padding: 2px;
        }
        .padding30
        {
            padding-left: 30px;
        }
        h1
        {
            font-size: 28px;
            line-height: 120%;
        }
        .h2
        {
            font-size: 14px;
            line-height: 120%;
            font-weight: bold;
        }
        .color1
        {
            color: #C5670C;
            font-weight: bold;
        }
        .color2
        {
            color: #C50C0C;
            font-size: 14px;
        }
        .detailpint
        {
            border-bottom: 1px dashed #cccccc;
            font-size: 12px;
            text-align: left;
        }
        .detailpint p
        {
            margin: auto 0;
            padding: 0;
            clear: both;
            text-align: left;
        }
        .detailpint td, h1, h2, h3, h4
        {
            font-size: 12px;
            font-weight: normal;
            text-align: left;
        }
        -- ></style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtPrintHTML" name="txtPrintHTML" />
    <table width="760" border="0" align="center" cellpadding="0" cellspacing="0" class="printbox">
        <tr>
            <td class="padding2">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFE08B"
                    style="border-bottom: 3px solid #CBC1B7;">
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="right">
                            <table width="50%" border="0" align="right" cellpadding="4" cellspacing="0">
                                <tr>
                                    <td width="18%" align="right">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="8%" align="left" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print1.gif" width="13" height="13" /><a onclick="window.open('/RouteAgency/RouteManage/SetPrintContent.aspx?RouteID=<%=RouteID %>','行程单打印','height=700,width=970,top=0,left=30,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no') "
                                                        href="javascript:void(0)"><span style="color: #8D2800">【打印行程单】</span> </a>
                                                </td>
                                                <td width="3%" align="right" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="22%" align="right">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="7%" align="left" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-l.gif" width="4" height="23" />
                                                </td>
                                                <td width="89%" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/word1.gif" width="13" height="13" /><a href="javascript:void(0);"
                                                        onclick="return setPrintHTML();"><span style="color: #8D2800">【行程单word格式】</span></a>
                                                </td>
                                                <td width="4%" align="right" background="<%=ImageServerPath %>/images/print-m.gif">
                                                    <img src="<%=ImageServerPath %>/images/print-r.gif" width="4" height="23" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="13%">
                                        <a href="javascript:void(0);" onclick="window.close();">【关闭】</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="divPrint">
                    <table width="760" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFEFA" align="center">
                        <tr>
                            <td height="50" align="center" valign="bottom">
                                <h1 style="margin-bottom: 2px; margin-top: 10px;">
                                    <asp:Label ID="lblCompanyName" runat="server"></asp:Label></h1>
                                <span style="font-size: 12px; line-height: 15px;">许可证号:<asp:Label ID="lblLicense"
                                    runat="server"></asp:Label></span>
                            </td>
                        </tr>
                    </table>
                    <table width="760" border="0" cellspacing="0" cellpadding="0" class="detailheaderword" align="center">
                        <tr>
                            <td width="100%" colspan="4" align="left">
                                <strong>联系人：</strong><asp:Label ID="lblContactName" runat="server"></asp:Label><strong>
                                    &nbsp;电话：</strong><asp:Label ID="lblContactTel" runat="server"></asp:Label><strong>
                                        &nbsp;地址：</strong><asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="760" border="0" cellpadding="0" cellspacing="0" class="margin10" align="center">
                        <tr>
                            <td height="43" align="center" background="<%=ImageServerPath %>/images/tour_title.gif"
                                class="h2">
                                <asp:Label ID="lblRouteName" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="760" border="0" cellpadding="3" cellspacing="1" bgcolor="#E7D0A2" align="center"
                        class="margin5">
                        <tr>
                            <td width="73%" align="left" bgcolor="#FFFFFF">
                                <strong>天数：</strong><asp:Label ID="lblTourDays" runat="server"></asp:Label>天
                            </td>
                        </tr>
                    </table>
                    <table width="760" border="0" align="center" cellspacing="1" bgcolor="#FFFFFF" id="tblPriceInfo">
                        <asp:Literal ID="ltrPriceDetail" runat="server"></asp:Literal>
                    </table>
                    <asp:Panel ID="pnlPlan" runat="server">
                        <table width="760" border="0" cellpadding="3" cellspacing="0" class="margin10" align="center">
                            <tr>
                                <td align="left" bgcolor="#F8EEE6" style="border-bottom: 1px solid #E3CAB7;">
                                    <strong>行程安排：<img src="<%=ImageServerPath %>/images/ttt.gif" width="15" height="16" /></strong>
                                </td>
                            </tr>
                            <asp:Literal ID="ltrTourPlan" runat="server"></asp:Literal>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlServeice" runat="server">
                        <table width="760" border="0" cellspacing="0" cellpadding="0" align="center">
                            <tr>
                                <td colspan="2" align="left" bgcolor="#F8EEE6" style="border-bottom: 1px solid #E3CAB7;">
                                    <strong>服务标准及说明：<img src="<%=ImageServerPath %>/images/ttt.gif" width="15" height="16" /></strong>
                                </td>
                            </tr>
                        </table>
                        <table width="760" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#cccccc" align="center">
                            <tr>
                                <td width="86" align="right">
                                    包含项目：
                                </td>
                                <td width="604" align="left">
                                    <asp:Literal ID="ltrContainContent" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    其他说明：
                                </td>
                                <td align="left">
                                    <asp:Literal ID="ltrStandardService" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    备注：
                                </td>
                                <td align="left">
                                    <asp:Literal ID="ltrRemark" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <asp:Button ID="btnWordPrint" runat="server" OnClick="btnWordPrint_Click" Style="display: none;" />
            </td>
        </tr>
        <div id="divPrintBody" style="display: none">
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    function setPrintHTML() {
        var printHTML = $("#divPrint").html();
        $("#divPrintBody").html(printHTML);
        //替换掉打印内容的价格input
        $("#divPrintBody  #tblPriceInfo").find("input").each(function() {
            var v = this.value;
            $(this).parent().html("<div>" + v + "元／人</div>");
        });
        //替换掉打印内容的价格标准的删除图片片
        $("#divPrintBody #tblPriceInfo").find("img").each(function() {
            $(this).parent().hide();
        });
        $("#txtPrintHTML").val($("#divPrintBody").html());
        $("#divPrintBody").html('');
        document.getElementById("<%=btnWordPrint.ClientID %>").click();
        return false;
    }
    </script>

    </form>
</body>
</html>
