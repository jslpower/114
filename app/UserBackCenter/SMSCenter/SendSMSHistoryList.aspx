<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendSMSHistoryList.aspx.cs"
    Inherits="UserBackCenter.SMSCenter.SendSMSHistoryList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="SmsHeaderMenu"
    TagPrefix="uc1" %>
<asp:content id="SendSMS" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <uc1:SmsHeaderMenu ID="SmsHeaderMenu1" runat="server" TabIndex="tab2" />
    <div id="div_hideprint">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="mobilebox" style="margin-bottom:10px;">
            <tr>
              <td width="85%" align="left">查询：发送时间
                  <input name="BeginDate" id="txt_SMS_BeginDate"  style="width:60px;" value="<%=ShowBeginDate %>" onfocus="WdatePicker()"  
                         size="9" /> -
                   <input name="EndDate" id="txt_SMS_EndDate" style="width:60px;" value="<%=ShowEndDate %>" onfocus="WdatePicker()"
                         size="9" />
                关键字
                <input name="txtKey" type="text" id="txtKey" runat="server"  size="30" autocomplete="off" />
             <select name="SendStatus" id="SendStatus" runat="server" style="width: auto">
                <option value="0">发送状态</option>
                <option value="1">发送成功</option>
                <option value="2">发送失败</option>
                </select>  
               <input type="button" id="btn_search" style="height:25px;width:45px" value="搜 索" /></td>
              <td width="15%" align="center"><a href="javascript:void(0);" onclick="PrintPage();return false;" onfocus="this.blur()"> 打印当页</a>
          <input type="button" value="导出全部" id="btnExcel"></asp:LinkButton>  </td>
            </tr>
          </table>
        <div id="divprint">
          <asp:Repeater runat="server" ID="ReListMoblie" OnItemDataBound="ReListMoblie_ItemDataBound">
          <HeaderTemplate>
          <table width="98%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0" style="table-layout: fixed">
            <tr>
                <td width="7%" align="center" bgcolor="#C5DCF5">序 号<br />
              <td width="14%" align="center" bgcolor="#C5DCF5">发送时间<br /></td>
              <td width="17%" align="center" bgcolor="#C5DCF5">号码<br /></td>
              <td width="41%" align="center" bgcolor="#C5DCF5">发送内容<br /></td>
              <td width="9%" align="center" bgcolor="#C5DCF5">价格(元)<br /></td>
              <td width="12%" align="center" bgcolor="#C5DCF5">状态</td>
            </tr>
            </HeaderTemplate>
            <ItemTemplate>
            <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
              <td align="center"><asp:Label ID="lblSMSID" runat="server"></asp:Label></td>
              <td align="center"><%# DataBinder.Eval(Container.DataItem, "SendTime")%></td>
              <td align="center"><%# EyouSoft.Common.Utils.GetEncryptMobile(DataBinder.Eval(Container.DataItem, "Mobile").ToString(),bool.Parse(Eval("IsEncrypt").ToString()))%></td>
              <td align="left" style="word-break: break-all; white-space: normal; word-wrap: break-word;"><%#DataBinder.Eval(Container.DataItem, "SMSContent")%></td>
              <td align="center"><%#decimal.Round(Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "UseMoeny")), 2)%></td>
              <td align="center" style="width:20%"><%#DataBinder.Eval(Container.DataItem, "ReturnResult").ToString() == "0" ? "发送成功" : "<font color='#FF0000'>发送失败</font>"%></td>
            </tr>
            
            </ItemTemplate>
        <FooterTemplate>  </table></FooterTemplate>
          </asp:Repeater>       
            <asp:Panel ID="NoData" runat="server" Visible="false">
                <table width="98%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0">
            <tr>
                <td width="7%" align="center" bgcolor="#C5DCF5">序 号<br />
              <td width="14%" align="center" bgcolor="#C5DCF5">发送时间<br /></td>
              <td width="17%" align="center" bgcolor="#C5DCF5">号码<br /></td>
              <td width="41%" align="center" bgcolor="#C5DCF5">发送内容<br /></td>
              <td width="9%" align="center" bgcolor="#C5DCF5">价格(元)<br /></td>
              <td width="12%" align="center" bgcolor="#C5DCF5">状态</td>
            </tr>
                    <tr>
                        <td align="center" colspan="6" height="30">
                            暂无发送历史信息记录
                        </td>
                    </tr>
                </table>
            </asp:Panel>
          </div> 
     <table id="sum_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
      <tr>
        <td class="F2Back" align="right" height="40">
        <div id="div_Expage" runat="server">
             <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </div>
        </td>
     </tr>
    </table>
 </div>      
<div id="div_savePrintInfo" style="display:none"></div>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Smskeys") %>"></script>
     <script type="text/javascript">
         var SendSMSHistoryList = {
             queryData: function() {
                 var action = "Query";
                 var queryUrl = "/SMSCenter/SendSMSHistoryList.aspx?" + SendSMSHistoryList.getParam();
                 topTab.url(topTab.activeTabIndex, queryUrl);
                 return false;
             },
             getParam: function() {
                 var StatusId = $("#<%=SendStatus.ClientID %>").val();
                 var keyword = encodeURI($.trim($("#<%=txtKey.ClientID%>").val()));
                 var leavDate = $("#txt_SMS_BeginDate").val();
                 var returnDate = $("#txt_SMS_EndDate").val();
                 return $.param({ StatusId: StatusId, keyword: keyword, leavDate: leavDate, returnDate: returnDate })
             },
             btnExcel: function() {
                 var queryUrl = "/SMSCenter/SendSMSHistoryList.aspx?isExcel=1&urltype=tab&" + SendSMSHistoryList.getParam();
                 window.open(queryUrl, "导出excel");
                 return false;
             }
         };
         $(document).ready(function() {
             $("#btn_search").click(function() {
                 SendSMSHistoryList.queryData();
                 return false;
             })
             $("#btnExcel").click(function() {
                 SendSMSHistoryList.btnExcel();
                 return false;
             })
             $("#sum_ExportPage").find("a").click(function() {
                 topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                 return false;
             });
         });
         function mouseovertr(o) {
             o.style.backgroundColor = "#FFF9E7";
             //o.style.cursor="hand";
         }
         function mouseouttr(o) {
             o.style.backgroundColor = "";
         }
    </script>
</asp:content>
