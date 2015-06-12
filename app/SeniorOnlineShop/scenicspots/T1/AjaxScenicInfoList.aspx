<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxScenicInfoList.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.AjaxScenicInfoList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="news_list">
    <asp:repeater runat="server" id="rptData" onitemdatabound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td width="9%" height="30" align="right">
                                <asp:Literal ID="ltrXH" runat="server"></asp:Literal>、
                            </td>
                            <td width="61%" align="left">
                                <asp:Literal ID="ltrType" runat="server"></asp:Literal><a <%#Container.ItemIndex==0?"class=\"C_green\"":"" %>
                                    href="/scenicspots/T1/ScenicInfoDetail.aspx?st=<%=TabIndex %>&id=<%#Eval("ID") %>&cid=<%=CompanyId %>">
                                    <%#Eval("Title")%></a>
                            </td>
                            <td width="30%" align="center">
                                [<%#Eval("IssueTime","{0:yyyy-MM-dd}")%>]
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
    <tr runat="server" id="NoData" visible="false">
        <td colspan="3" height="30" align="center">
            未找到你要的景区信息！
        </td>
    </tr>
</table>
<%--<div class="clearboth">
</div>--%>
<div class="digg" style=" width:97%; text-align:center; line-height:20px;" id="page">
    <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
</div>
