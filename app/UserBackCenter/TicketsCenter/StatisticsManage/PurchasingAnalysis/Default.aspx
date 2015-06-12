<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.TicketsCenter.StatisticsManage.PurchasingAnalysis.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="PurchasingAnalysis" runat="server" contentplaceholderid="ContentPlaceHolder1">

    <table width="835" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" class="admin_tablebox" id="tb_PurchasingAnalysis">
	  <tr>
        <td colspan="3" height="10"></td>
      </tr>
      <tr>
        <td width="175" height="30" align="right">用户名：</td>
        <td align="left">&nbsp;</td>
        <td align="left"><input type="text" name="txtUserName" id="txtUserName" /></td>
      </tr>
      <tr>
    <td height="30" align="right">成功购买机票数：</td>
        <td align="left">&nbsp;</td>
        <td align="left"><input type="text" name="txtTicketNumber" id="txtTicketNumber" /></td>
      </tr>
      <tr>
        <td height="30" align="right">成功购买金额数：</td>
        <td align="left">&nbsp;</td>
        <td align="left"><input type="text" name="txtTicketMoney" id="txtTicketMoney" /></td>
      </tr>
      <tr>
        <td height="30" align="right">起始日期：</td>
        <td align="left">&nbsp;</td>
        <td align="left"><input name="startDateTime" id="startDateTime" type="text"  onfocus="WdatePicker();" />
                <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl %>/images/jipiao/time.gif" width="16" height="13" onclick="$('#tb_PurchasingAnalysis').find('input[id=startDateTime]').focus();" /></td>
      </tr>
      <tr>
        <td height="30" align="right">结束日期：</td>
        <td align="left">&nbsp;</td>
        <td align="left"><input name="txtEndDateTime" id="txtEndDateTime" type="text"  onfocus="WdatePicker();" />
                <img style="position:relative; left:-24px; top:3px; *top:1px;" src="<%=ImageServerUrl %>/images/jipiao/time.gif" width="16" height="13" onclick="$('#tb_PurchasingAnalysis').find('input[id=txtEndDateTime]').focus();"  /></td>
      </tr>
      <tr>
        <td height="40" align="right"></td>
        <td width="53" align="center">&nbsp;</td>
        <td width="605" align="left"><a href="javascript:void(0);" onclick="PurchasingAnalysis.OnSearch();return false;"><img src="<%=ImageServerUrl %>/images/jipiao/admin_orderform_ybans_03.jpg" width="79" height="25" alt="查询"/></a></td>
      </tr>
    </table>
  <script type="text/javascript">
      var PurchasingAnalysis = {
          OnSearch: function() {
              var Searchparms = { "UserName": "", "TicketNumber": "", "TicketMoney": "", "SartDateTime": "", "EndDateTime": "" };
              Searchparms.UserName = $.trim($("#tb_PurchasingAnalysis").find("input[id=txtUserName]").val());
              Searchparms.TicketNumber = $.trim($("#tb_PurchasingAnalysis").find("input[id=txtTicketNumber]").val());
              Searchparms.TicketMoney = $.trim($("#tb_PurchasingAnalysis").find("input[id=txtTicketMoney]").val());
              Searchparms.SartDateTime = $.trim($("#tb_PurchasingAnalysis").find("input[id=startDateTime]").val());
              Searchparms.EndDateTime = $.trim($("#tb_PurchasingAnalysis").find("input[id=txtEndDateTime]").val());
              var goToUrl = " /TicketsCenter/StatisticsManage/PurchasingAnalysis/PurchasingList.aspx?" + $.param(Searchparms);
              topTab.url(topTab.activeTabIndex, goToUrl);
          }
      };
      $("#tb_PurchasingAnalysis input").bind("keypress", function(e) {
          if (e.keyCode == 13) {
              PurchasingAnalysis.OnSearch();
              return false;
          }
      });
     
  </script>
</asp:content>
