<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EShopFooter.ascx.cs" Inherits="UserBackCenter.usercontrol.EShopControl.EShopFooter" %>
<div class="boxgrid3 infocfull3">
    <div class="firendbar">
        友情链接</div>
    <div class="firendlist">
        <ul>
            <asp:Repeater ID="rptFriendLinks" runat="server">
                <ItemTemplate>
                    <li><a href='#' target="_blank" class="huizi">
                        •&nbsp;&nbsp;链接名称</a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
<div class="boxgrid6 infocfull6">
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;" id="tblcopyright">
    <tr>
        <td align="center" class="bottom1" style="height:auto;">
            <asp:Literal ID="ltrCopyRight" runat="server"></asp:Literal>
            <td align="center" class="bottom1"><br />
            <img src="images/MQWORD.gif" width="49" height="16" />&nbsp;&nbsp;<img src="images/qq.gif" width="61" height="16" />&nbsp;&nbsp;<img src="images/msn1.gif" width="67" height="17" /><br />
    <br />
</td>
        </td>
    </tr>
</table>
</div>
<script type="text/javascript">
$(".boxgrid3 h3").css("height",$('.boxgrid3').height());
$(".boxgrid6 h3").css("height",$('.boxgrid6').height());
if($("#tblcopyright").parent("div").height() < 50)
{
    $("#tblcopyright").parent("div").css("height","60px")
}
</script>