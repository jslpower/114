<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamCustomizationManage.aspx.cs" Inherits="UserBackCenter.EShop.AjaxTeamCustomizationManage" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>

<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
        <tr>
            <td width="7%" class="hang">
                排序
            </td>
            <td width="25%" class="hang">
                单位名称
            </td>
            <td width="15%" class="hang">
                联系人
            </td>
            <td width="10%" class="hang">
                电话
            </td>
            <td width="10%" class="hang">
                计划日期
            </td>
            <td width="10%" class="hang">
                计划人数
            </td>
            <td width="10%" class="hang">
                行程要求
            </td>
            <td width="12%" class="hang">
                操作
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptTeamCustomization" OnItemDataBound="rptTeamCustomization_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <td align="center" height="26">
                        <asp:Literal ID="ltrXH" runat="server"></asp:Literal>
                    </td>
                    <td align="left">
                        <%#Eval("ContactCompanyName")%>
                    </td>
                    <td align="center">
                        <%#Eval("ContactName")%>
                    </td>
                    <td align="center">
                        <%#Eval("ContactTel")%>
                    </td>
                    <td align="center">
                        <%#Eval("PlanDate", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("PlanPeopleNum")%>
                    </td>
                    <td align="center">
                        <%#Eval("TravelContent").ToString()!=""?"<font color='red'>有</font>":"无"%>
                    </td>
                    <td align="center">
                        <a href="#" lookid="<%#Eval("Id") %>">查看</a>|<a href="javascript:void(0);" teamid="<%#Eval("Id") %>">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
         <tr runat="server" id="NoData" visible="false">
                <td colspan="8" align="center"  style=" padding:6px;">
                    对不起，没有你要找的团队定制信息！
                </td>
            </tr>
        <tr>
            <td colspan="8" id="TeamCustomizationManage_ExportPage" class="F2Back" style="text-align: right;">
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
                    runat="server"></cc2:ExportPageInfo>
            </td>
        </tr>
    </table>
    <input type="hidden" id="hid_intPageIndex" value="<%=intPageIndex %>" />