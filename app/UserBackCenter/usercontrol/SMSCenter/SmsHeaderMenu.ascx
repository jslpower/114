<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmsHeaderMenu.ascx.cs" Inherits="UserBackCenter.usercontrol.SMSCenter.SmsHeaderMenu" %>
<%@ Import Namespace="EyouSoft.Common" %>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="tablewidth">
  <tr>
    <td colspan="2" align="left"><img src="<%=Domain.ServerComponents%>/images/topjsdx.gif" width="490" height="55" /></td>
    <td width="32%">&nbsp;</td>
  </tr>
  <tr>
    <td width="2%" align="left" background="<%=Domain.ServerComponents%>/images/skybar2.gif"><img src="<%=Domain.ServerComponents%>/images/ubar1.gif" width="9" height="27" /></td>
    <td width="66%" align="left" background="<%=Domain.ServerComponents%>/images/skybar2.gif">短信群发好帮手！马上开通高级网店，即刻享受十万组团社资料进行精准短信营销。</td>
    <td align="right" background="<%=Domain.ServerComponents%>/images/skybar2.gif"><img src="<%=Domain.ServerComponents%>/images/skybar3.gif" width="9" height="27" /></td>
  </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:10px;width:99%" id="smsHeaderMenu_table">
    <tr>
      <td width="80%" align="center"> <a href="/SMSCenter/SendSMS.aspx" id="smsHeaderMenu_tab1" class="zhuanxian">发送短信</a>  <a href="/SMSCenter/SendSMSHistoryList.aspx"  id="smsHeaderMenu_tab2" class="zhuanxian">发送历史</a>  <a href="/SMSCenter/PhraseList.aspx"  id="smsHeaderMenu_tab3" class="zhuanxian">常用短信</a>  <a href="/SMSCenter/CustomerList.aspx"  id="smsHeaderMenu_tab4" class="zhuanxian">客户资料</a>  <a href="/SMSCenter/AccountInfo.aspx"  id="smsHeaderMenu_tab5" class="zhuanxian">帐户信息</a></td>
    </tr>
 </table>
<script type="text/javascript">

$(document).ready(function()
{ 
  var smsHeaderMenuTab=$("#smsHeaderMenu_<%=TabIndex%>");
  if(smsHeaderMenuTab)
  {
     smsHeaderMenuTab.attr("class","zhuanxianon");
  }
    $("#smsHeaderMenu_table a").click(function(){
     
        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
        return false;
    });
 
});

</script>