<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLineCompanyList.aspx.cs"
    Inherits="SiteOperationsCenter.Statistics.LineCompanyList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
    <asp:repeater id="rpt_List" runat="server">
        <ItemTemplate>
        <%# (Container.ItemIndex+1)%2==1? "<tr class='baidi' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">" : "<tr bgcolor='#f3f7ff' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">"%>
            <td width="50px"  height="25" align="center" valign="middle">
                <strong>
                    <%#((pageIndex - 1) * pageSize) + Container.ItemIndex + 1%>
                </strong>
            </td>
            <td align="center" width="110px">
                <a href="javascript:void(0)" class="hong18" style="cursor: pointer" onclick="OpenProPage('CompanyInfo.aspx','公司信息','<%#Eval("companyId") %>','')" >
                    <%#Eval("CompanyName")%></a>
            </td>
            <td align="left" valign="middle" width="140px">
           <%#GetAreaByList(Eval("AreaStatinfo"),Eval("companyId").ToString())%> 
            </td>
            <td align="center" valign="middle" width="50px">
                <a href="javascript:void(0)" onclick="OpenProPage('ProductList.aspx','产品列表','<%#Eval("CompanyId") %>','')"><strong><%#Eval("SumTourNum")%></strong></a>
            </td>
            <td align="center" valign="middle" width="70px">
                <a href="javascript:void(0)" onclick="OpenOrderPage('1','2','<%#Eval("CompanyId") %>')" style="cursor:pointer"><strong><%#Eval("OrdainNum") %></strong></a>
            </td>
            <td align="center" valign="middle" width="70px">
                <strong><a href="javascript:void(0)" onclick="OpenOrderPage('4','2','<%#Eval("CompanyId") %>')" style="cursor:pointer"><%#Eval("SaveSeatNum")%></a></strong>
            </td>
            <td width="80px" align="center" valign="middle">
                <strong><a href="javascript:void(0)" onclick="OpenOrderPage('2','2','<%#Eval("CompanyId") %>')" style="cursor:pointer"><%#Eval("SaveSeatExpiredNum")%></a></strong>
            </td>
            <td width="80px" align="center" valign="middle">
                <strong><a href="javascript:void(0)" onclick="OpenOrderPage('3','2','<%#Eval("CompanyId") %>')" style="cursor:pointer"><%#Eval("NotAcceptedNum")%></a></strong> 
            </td>
            <td width="80px" align="center" valign="middle">
                <a href="javascript:void(0)" onclick="OpenProPageTwo('LoginRecord.aspx','登录次数','<%#Eval("CompanyId") %>','')"><strong><%#Eval("LoginNum")%></strong></a>
            </td>
            <td width="70px" align="center" valign="middle">
                <a href="javascript:void(0)" onclick="OpenProPageTwo('ViewedCompanyList.aspx','被查看次数','<%#Eval("CompanyId") %>','d')"><strong><%#Eval("VisitedNum")%></strong></a>
            </td>
        </tr>
        
        </ItemTemplate>
        </asp:repeater>
</table>
<table width="98%" border="0" cellpadding="0" cellspacing="1" class="kuang">
    <tr>
        <td align="center">
        <div id="div_AjaxLineCompanyList">
            <asp:label id="lblMsg" runat="server" text=""></asp:label>
            <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </div>
        </td>
    </tr>
</table>
