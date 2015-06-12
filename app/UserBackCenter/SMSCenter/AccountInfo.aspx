<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="UserBackCenter.SMSCenter.AccountInfo" %>

<%@ Register Src="../usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="SmsHeaderMenu"
    TagPrefix="uc1" %>
<asp:content id="AccountInfo" runat="server" contentplaceholderid="ContentPlaceHolder1">

     <uc1:SmsHeaderMenu ID="AccountInfo_SmsHeaderMenu" runat="server" />
      <table cellspacing="0" cellpadding="4"  border="0"  class="mobilebox" style="margin-bottom:10px; margin-top:10px; width:99%;">
        <tr>
          <td width="20%" align="left" >
            当前您的余额为：<span style="font-size:16px; color:#ff0000; font-weight:bold;"><%=CompanyMoney%>元</span> 
          </td>
          <td align="left">
            <asp:Repeater runat="server" id="rpAccountList">
                 <HeaderTemplate>
                    <ul>
                 </HeaderTemplate>
                 <ItemTemplate>
                    <li>【<%# Eval("ChannelName")%>】剩余条数：<%# Eval("Number")%>条</li>
                 </ItemTemplate>
                 <FooterTemplate>
                    </ul>
                 </FooterTemplate>
            </asp:Repeater>
          </td>
          <td width="55%" align="left">
            <a id="AccountInfo_Pay" href="/SMSCenter/PayMoney.aspx" target="_blank"><img src="<%=ImageServerUrl %>/images/result.gif" border="0" /></a>
          </td>
        </tr>
      </table>
      <table  cellspacing="0" cellpadding="4"  border="0" class="mobilebox" style="margin-bottom:10px; margin-top:10px; width:99%;">
        <tr>
          <td width="20%" align="right" style="border-bottom:1px dashed #999999;">充值明细：</td>
          <td width="80%" align="left" style="border-bottom:1px dashed #999999;" id="divPayMoneys"> </td>
        </tr>
        <tr>
          <td width="20%" align="right">消费明细：</td>
          <td  width="80%" align="left" id="divExpenseDetails">
          </td>
        </tr>
      </table>
      <div class="jiange"></div>
  <script type="text/javascript">
      var PayMoneyParms = { "CompanyId": "<%=SiteUserInfo.CompanyID %>", Page: 1 };
      var ExpenseDetailParms = { "CompanyId": "<%=SiteUserInfo.CompanyID %>", Page: 1 };

      var AccountInfo = {
          GetPayMoneyList: function() {
              $.newAjax
               ({
                   url: "/SMSCenter/GetPayMoneys.aspx",
                   cache: false,
                   data: PayMoneyParms,
                   success: function(html) {
                       $("#divPayMoneys").html(html);
                   }
               });

          },
          GetExpenseDetails: function() {
              $.newAjax
               ({
                   url: "/SMSCenter/GetExpenseDetails.aspx",
                   cache: false,
                   data: ExpenseDetailParms,
                   success: function(html) {
                       $("#divExpenseDetails").html(html);
                   }
               });
          },
          PayMoneyLoadData: function(PageObj) {
              PayMoneyParms.Page = exporpage.getgotopage(PageObj);
              AccountInfo.GetPayMoneyList();
          },
          ExpenseDetailLoadData: function(PageObj) {
              ExpenseDetailParms.Page = exporpage.getgotopage(PageObj);
              AccountInfo.GetExpenseDetails();
          },
          GetSendDetails: function(isSendContent, TotalId, SendStatus) {
              var parms = { "isSendContent": isSendContent, "TotalId": TotalId, "SendStatus": SendStatus };
              var strUrl = "/SMSCenter/SendContentDetail.aspx";
              var strType = '号码';
              if (isSendContent == "1") {
                  strType = '内容';
              }
              Boxy.iframeDialog({ title: strType, iframeUrl: strUrl, width: 500, height: 400, draggable: true, data: parms });
          }

      };

      function mouseovertr(o) { o.style.backgroundColor = "#D8E5FF"; }
      function mouseouttr(o) { o.style.backgroundColor = ""; }

      $(function() {
          AccountInfo.GetPayMoneyList();
          AccountInfo.GetExpenseDetails();
          $("#AccountInfo_Pay").click(function() {
              topTab.open($(this).attr("href"), "短信充值");
              return false;
          });
      });
  </script>

</asp:content>
