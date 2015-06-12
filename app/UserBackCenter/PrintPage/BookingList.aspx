<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="BookingList.aspx.cs"
    Inherits="UserBackCenter.PrintPage.BookingList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_1") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_2") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("style_3") %>" rel="Stylesheet"
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="696" border="0" id="tbl_top" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="420">              
               &nbsp;
            </td>
            <td width="270" align="right">
                字体：<a href="JavaScript:changeSheets(3)">大</a>&nbsp;&nbsp;<a href="JavaScript:changeSheets(2)">中</a>&nbsp;&nbsp;<a
                    href="JavaScript:changeSheets(1)">小</a>&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/dayin2.gif" style="cursor:pointer;" onclick="printPage()"
                        width="57" height="19">
                <asp:ImageButton ID="imgbtnToWord"  OnClick="btnWord_Click" Width="57" Height="19" runat="server" />
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <div id="printPage">
        <div id="htmlBody" style="margin-left: auto; margin-right: auto;">
            <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="font_hang" align="center" style="border-bottom: 4px solid #F70000; color: #000000;
                        font-size: 25px; line-height: 35px; font-weight: bold;">
                        <%=RouteName %><br />
                        <%=LeaveTime%>出发&nbsp;
                        <asp:Literal ID="ltrBookName" Text="团队人员汇总名单" runat="server"></asp:Literal>                        
                    </td>
                </tr>
            </table>
            <table width="696" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#000000"
                style="margin-top: 10px;">
                <asp:Repeater runat="server" ID="rptBookingList" OnItemDataBound="rpt_BookingList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td colspan="7" align="left" bgcolor="#eeeeee">
                                <strong>
                                    <%#Eval("BuyCompanyName")%></strong>&nbsp; 电话：<%#Eval("ContactTel")%>&nbsp; 订购时间：<%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptBookingList2">
                            <HeaderTemplate>
                                <tr>
                                    <td width="6%" align="center">
                                        序号
                                    </td>
                                    <td width="12%" align="center">
                                        姓名
                                    </td>
                                    <td width="8%" align="center">
                                        成/童
                                    </td>
                                    <td width="8%" align="center">
                                        性别
                                    </td>
                                    <td width="18%" align="center">
                                        联系电话
                                    </td>
                                    <td width="15%" align="center">
                                        证件类型
                                    </td>
                                    <td width="33%" align="center">
                                        证件号
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%#Container.ItemIndex+1 %>
                                        <!--序号-->
                                    </td>
                                    <td align="center">
                                        <strong>
                                            <%#Eval("VisitorName")%></strong>
                                    </td>
                                    <td align="center">
                                        <%#Eval("VisitorType").ToString() == "True"?"成人":"儿童"%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Sex").ToString() == "True" ? "男" : "女"%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("ContactTel")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("CradNumber").ToString().Trim()==""?"暂无证件":Eval("CradType")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("CradNumber")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>                
            </table>            
        </div>
        <div style="line-height:10px;">&nbsp;</div>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
             <tr runat="server" id="tr_SpecialContent">
                <td style="border: 1px solid #000000; width:120px; margin-top:10px;">
                    特殊要求说明：
                </td>
                <td style="border: 1px solid #000000;">
                    <asp:Literal ID="ltrSpecialContent" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    备注：
                </td>
            </tr>
            <tr>
                <td  colspan="2" style="border: 1px solid #000000; padding: 1px;">
                    <textarea name="Remark" id="txtRemark" style="width: 98%;" rows="3" class="bottow_no"></textarea>
                    <span id="span_Remark"></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="60%" id="tbl_Footer" height="40" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <input id="btnPrint" class="baocun_an" type="button" value="打　印" onclick="printPage()" />                
            </td>
            <td>
                <asp:Button ID="btnWord" runat="server" Text="word格式打印" CssClass="baocun_an" OnClick="btnWord_Click" />
            </td>
            <td>
                <asp:Button ID="btnToWord" runat="server" Text="导出到word" CssClass="baocun_an" OnClick="btnWord_Click" />
            </td>
        </tr>
    </table>
    <input id="hidrptPrintHTML" name="rptPrintHTML" type="hidden" />
    <input id="hidRemark" name="rptRemark" type="hidden" />
    </form>    
    <script language="JavaScript" type="text/javascript">
        var doAlerts=false;
        function changeSheets(whichSheet){
          whichSheet=whichSheet-1;
          if(document.styleSheets){
            var c = document.styleSheets.length;
            if (doAlerts) alert('Change to Style '+(whichSheet+1));
            for(var i=0;i<c;i++){
              if(i!=whichSheet){
                document.styleSheets[i].disabled=true;
              }else{
                document.styleSheets[i].disabled=false;
              }
            }
          }
        }
        function $(id)
        {
            return document.getElementById(id);
        }
        function getHtml(){
            $("hidRemark").value=$("txtRemark").value;            
            $("hidrptPrintHTML").value=$("htmlBody").innerHTML;
        }
        function printPage() {
                $("txtRemark").style.display='none';
                $("tbl_top").style.display='none';           
                $("tbl_Footer").style.display='none';
                $("span_Remark").innerHTML=$("txtRemark").value;
                if (window.print != null) {
                    window.print();
                    $("span_Remark").innerHTML=""
                    $("txtRemark").style.display='';
                    $("tbl_top").style.display='';   
                    $("tbl_Footer").style.display=''; 
                } else {
                    alert('没有安装打印机');
                }
       }
    </script>

</body>
</html>
