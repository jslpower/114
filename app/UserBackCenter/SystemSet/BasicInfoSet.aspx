<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInfoSet.aspx.cs" Inherits="UserBackCenter.SystemSet.BasicInfoSet" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<asp:Content id="SystemIndex" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

	<table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr><td valign="top" >
      <uc1:sznb id="sznb1" runat="server" TabIndex="tab7"></uc1:sznb>
	<table width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom:1px solid #ABC9D9; border-left:1px solid #ABC9D9; border-right:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top">
                
                <table width="100%" border="0" cellspacing="0" cellpadding="0"  margin-top:10px;">
                  <tr>
                    <td width="4%">&nbsp;</td>
                    <td width="96%" align="left"><img src="<%=ImageServerUrl%>/images/jichu3.gif" width="121" height="33" /></td>
                  </tr>
                </table>
                <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td><table width="400" border="0" cellpadding="0" cellspacing="0" style="border:1px solid #EBEBEB; padding:10px;">
                        <tr>
                          <td width="55%"><table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#E0E0E0">
                            <tr bgcolor="#FFFFFF">
                              <td width="30%" align="right" bgcolor="#DBF7FD">默认停收时间：</td>
                              <td width="70%" bgcolor="#FFFFFF">出团前
                                <input name="textfield224" style="height:20px; width:50px;" type="text"size="20" />
                                天
                                <input name="Submit224" type="submit" value="  保存  "  style="height:26px;"/>                              </td>
                            </tr>
                          </table></td>
                          <td width="45%" align="left" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;<span class="chengse"><strong>注：</strong></span>设置后线路自动在团队发团前停收)</td>
                        </tr>
                    </table></td>
                  </tr>
                </table></td>
            </tr>
          </table></td>
      </tr></table>

</asp:Content>

