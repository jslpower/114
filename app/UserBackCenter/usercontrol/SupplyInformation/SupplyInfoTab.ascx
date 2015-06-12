<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplyInfoTab.ascx.cs"
    Inherits="UserBackCenter.usercontrol.SupplyInformation.SupplyInfoTab" %>
<table width="100%" id="<%=this.ClientID %>" border="0" cellspacing="0" cellpadding="0" class="zttoolbar">
    <tr>
        <td>
            <div id="div_SupplyInfoTab_0" class="<%=TabIndexOneClass %>" style="padding-top: 5px;">
                <a href="/supplyinformation/addsupplyinfo.aspx">发布供求</a></div>
            <div id="div_SupplyInfoTab_1" class="<%=TabIndexTwoClass %>" style="padding-top: 5px;">
                <a href="/supplyinformation/allsupplymanage.aspx">我的供求</a></div>
            <%--<div class="zttooltitleun" style="padding-top: 5px;">
                <a href="gongqiu2.html">收件箱</a></div>--%>
            <div id="div_SupplyInfoTab_3" class="<%=TabIndexThreeClass %>" style="padding-top: 5px;">
                <a href="/supplyinformation/hassupplyfavorites.aspx">我关注的商机 </a>
            </div>
             <div id="div_SupplyInfoTab_4" style="font-size:14px;padding-top: 5px; width:140px; height:28px; float:left; margin-right:4px; background-image:url(<%=EyouSoft.Common.Domain.ServerComponents%>/images/zttoolunbj.gif)">
                <a href="<%=MoreSupplyInfoUrl %>" target="_blank">更多供求>></a>
            </div>
        </td>
    </tr>
</table>
<script type="text/javascript">
    $(function(){
        $("#<%=this.ClientID %> div[class^='zttooltitle'] a").click(function(){
            topTab.url(topTab.activeTabIndex,$(this).attr("href"));            
            return false;            
        });
    })    
</script>