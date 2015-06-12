<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneShopMainPage.aspx.cs" Inherits="UserBackCenter.GeneralShop.GeneShopMainPage" MasterPageFile="~/MasterPage/Site1.Master"%>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="ctnGeneShop" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tablewidth">
      <tr>
        <td width="42%" class="addgshop">
            <span id="spanApplyShop" runat="server">
                <a id="ApplShop" href="javascript:void(0)" onclick="return SystemIndex.tabChange('ApplicationEShop.aspx')">申请高级网店</a>
            </span>
            </td>
	    <td width="58%" align="left"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/gshopys.gif" width="470" height="48" /></td>
      </tr>
    </table>

      <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:5px;" class="tablewidth">
          <tr>
            <td width="19%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:35px; padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/wangdanshiyi.gif" width="91" height="23"/></td>
            <td width="81%" align="left" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:35px; padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><a href="<%=ShopPath %>" target="_blank"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/right_bottom.gif" width="12" height="12" /> 进入我的网店</a></td>
          </tr>
          <tr>
            <td colspan="2" style=" padding-left:60px; text-align:left;"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/dianxia.gif" width="16" height="5" /></td>
          </tr>
        </table>
	    <table width="98%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><iframe frameborder=0 width="100%" height="600" marginheight=0 marginwidth=0 scrolling=yes src="/GeneralShop/GeneShopPageSet.aspx" style="float:left; border:2px solid #FFFF00"></iframe></td>
      </tr>
    </table>
    <script type="text/javascript">
 var SystemIndex=
 {
   tabChange:function(source_link){
       topTab.url(topTab.activeTabIndex,"/GeneralShop/"+source_link);       
       return false;
    }
 }
</script>
</asp:Content>
