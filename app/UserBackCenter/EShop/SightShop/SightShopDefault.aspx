<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SightShopDefault.aspx.cs"
    Inherits="UserBackCenter.EShop.SightShop.SightShopDefault" %>

<%@ Import Namespace="EyouSoft.Common" %>

<asp:content id="SightShopDefault" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:5px;" class="tablewidth">
          <tr>
            <td width="25%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/wangdanshiyi.gif" width="91" height="23"/></td>
           
            <td width="18%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#007BBB; font-size:16px; text-align:right; font-weight:bold;"><img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/wangdanshiyi1.gif"/></td>

            <td width="17%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#FF6000; font-size:12px; text-align:left; font-weight:bold;"><DIV style="CURSOR: hand" onclick="javascript:Hide_ShowPanel('DivHiddenTourInfo1','HSText1');"><SPAN id="HSText1" style="background:#FFF9E8; border:1px solid #FF9933; padding:1px;">隐藏网店模版</SPAN></DIV></td>
            <td width="30%" align="left" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px;4 padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><a runat="server" id="hrefSightShop" target="_blank"><img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/right_bottom.gif" width="12" height="12" /> 进入我的网店 </a></td>
          </tr>
          <tr>
            <td colspan="5" style=" padding-left:60px; text-align:left;"><img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/dianxia.gif" width="16" height="5" /></td>
          </tr>
        </table>

      <div  id="DivHiddenTourInfo1">
	    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:1px solid #C6DDE9; padding:5px; margin:3px;" class="tablewidth">
          <tr> 
            <td width="30%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="p-dimg">
                <tr>
                  <td align="center"><img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/sightshop/mbslt.jpg" width="200" height="76" style="border:1px solid; border-color:#B5B5B5; margin:5px;"/></td>
                </tr>
                <tr>
                  <td align="center"></td>
                </tr>
            </table></td>
           
           
          </tr>
	       <tr>
      </tr>
        </table>
      </div>
      <table width="98%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><iframe frameborder=0 width=100% height=670 marginheight=0 marginwidth=0 scrolling=yes src="/eshop/sightshop/iframesightshop.aspx" style="float:left; border:2px solid #FFFF00"></iframe></td>
      </tr>
    </table>
    
    <script type="text/javascript">
    function Hide_ShowPanel(PanelId,PanelText)
    {
	    $("#" + PanelId).css("display",$("#"+PanelId).css("display")=='none'?'block':'none');
	    $("#" + PanelText).html($("#"+PanelId).css("display")=='none'?'显示网店模版':'隐藏网店模版');
    }

    </script>
</asp:content>
