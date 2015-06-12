<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxExchangeList.aspx.cs"
    Inherits="TourUnion.WEB.IM.TourAgency.ExchangeTopic.AjaxExchangeList" %>

<%@ Register Assembly="ExporPage" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:repeater id="repTopicList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#C4E5F3"
                class="hdbox" >
                <tr>
                    <td width="80%" align="left" class="hdbartable">
                        主 题
                    </td>
                    
                    <td width="20%" align="center" class="hdbartable">
                        时间
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td height="30" align="left" bgcolor="#FFFFFF">
                <%# GetTitle(Eval("CategoryId").ToString(), Eval("CategoryName").ToString(), Eval("TopicId").ToString(), Eval("Title").ToString(), Eval("ToProvinceName").ToString(), Convert.ToInt32(Eval("ReplyCount").ToString()), Convert.ToInt32(Eval("ViewCount").ToString()))%> 
                </td>
                <td align="center"  bgcolor="#FFFFFF" class="afontsize12">
                  <%#Convert.ToDateTime(Eval("IssueTime").ToString()).Month + "-" + Convert.ToDateTime(Eval("IssueTime").ToString()).Day%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:repeater>
<table width="100%" border="0" visible="false" id="NoDate" runat="server" align="center"
    cellpadding="3" cellspacing="1" bgcolor="#C4E5F3" class="hdbox">
    <tr>
        <td width="80%" align="left" class="hdbartable">
            主 题
        </td>
        <td width="20%" align="center" class="hdbartable">
            时间
        </td>
    </tr>
    <tr>
        <td height="100" colspan="5" align="center" bgcolor="#FFFFFF">
            暂无相关话题
        </td>
    </tr>
</table>
<table width="100%" border="0" align="center" cellspacing="0" cellpadding="0">
    <tr>
        <td height="22" align="right">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageLinkCount="7" PageStyleType="MostEasyNewButtonStyle"
                runat="server"></cc1:ExporPageInfoSelect>
        </td>
    </tr>
</table>
