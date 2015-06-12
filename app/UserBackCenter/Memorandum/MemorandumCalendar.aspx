<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemorandumCalendar.aspx.cs"
    Inherits="UserBackCenter.Memorandum.MemorandumCalendar" %>
<%@ Register Src="~/usercontrol/MemoCalendar.ascx" TagName="memocalendar" TagPrefix="uc1" %>
<asp:content id="MemorandumCalendar" contentplaceholderid="ContentPlaceHolder1" runat="server">
	<table height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td valign="top" ><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td colspan="2" align="left"><img src="<%=ImageServerPath %>/images/topjs.gif" width="490" height="55" /></td>
            <td colspan="2">&nbsp;</td>
          </tr>
          <tr>
            <td width="19" align="left" background="<%=ImageServerPath %>/images/skybar2.gif"><img src="<%=ImageServerPath %>/images/ubar1.gif" width="9" height="27" /></td>

            <td width="471" align="left" background="<%=ImageServerPath %>/images/skybar2.gif">当前位置 记事本</td>
            <td width="328" align="right" background="<%=ImageServerPath %>/images/skybar2.gif">有了备忘录，工作生活真轻松！</td>
            <td width="37" align="right" background="<%=ImageServerPath %>/images/skybar2.gif"><img src="<%=ImageServerPath %>/images/skybar3.gif" width="9" height="27" /></td>
          </tr>
        </table>
        <uc1:memocalendar ID="memocalendar1" runat="server" IsBackDefault="false" />
       </td>
    </tr>
</table>
</asp:content>
