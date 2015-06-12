<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGetInformationList.aspx.cs"
    Inherits="SiteOperationsCenter.NewsCenterControl.AjaxGetInformationList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPageByBtn" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<cc1:CustomRepeater ID="repInformationList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C7DEEB"
            class="table_basic" id="tbInformationList">
            <tr>
                <th>
                    <input type="checkbox" name="checkAll" id="checkAll" onclick="Informationindustry.ChAll(this);" />
                    <label for="checkAll">
                        序号</label>
                </th>
                <th nowrap="nowrap">
                    标题
                </th>
                <th nowrap="nowrap">
                    操作单位
                </th>
                <th nowrap="nowrap">
                    类别
                </th>
                <th nowrap="nowrap">
                    发布时间
                </th>
                <th nowrap="nowrap">
                    发布人
                </th>
                <th nowrap="nowrap">
                    点击量
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <input type="checkbox" name="ckInformationId" value='<%#Eval("NewId") %>'>
                <%# GetCount() %>
            </td>
            <td height="25" align="center">
                <a href="Javascript:void(0)" onclick="Informationindustry.EditInfomationOpenDialog('<%#Eval("NewId") %>'); return false">
                    <%#Eval("Title")%></a>
            </td>
            <td align="left">
                <a href="<%#GetShopURL(Eval("CompanyId").ToString()) %>" target="_blank">
                    <%#Eval("CompanyName") %></a>
                <%#GetMq(Eval("CompanyId").ToString())%>
            </td>
            <td align="center">
                <%#(EyouSoft.Model.NewsStructure.PeerNewType)Eval("TypeId")%>
            </td>
            <td align="center">
                <%#Eval("IssueTime")%>
            </td>
            <td align="center">
                <%#Eval("OperatorName")%>
            </td>
            <td align="center">
                <%#Eval("ClickNum")%>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
<div align="right">
    <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }
</script>

