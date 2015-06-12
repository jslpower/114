<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourTeamApplyDetails.aspx.cs"
    Inherits="SiteOperationsCenter.AirTicktManage.TourTeamApplyDetails" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团队票申请详细页</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
            <tr>
                <td height="30px" align="center" colspan="3">
                    <h3>
                        团队票申请详情</h3>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF">
                    行程类型：
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrJourneyType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    &nbsp;城市从：
                </td>
                <td width="52%" bgcolor="#F7FBFF">
                    &nbsp;<font color="#FF0000"><asp:Literal ID="ltrStartCity" runat="server"></asp:Literal></font>
                    &nbsp;到
                    <font color="#FF0000"><asp:Literal ID="ltrEndCity" runat="server"></asp:Literal></font>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    乘机人数：
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrPersonCount" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    联系电话：
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrTelephone" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    航空公司：
                </td>
                <td bgcolor="#F7FBFF">
                    &nbsp;<asp:Literal ID="ltrPlaneCompany" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    去程航班号：
                </td>
                <td bgcolor="#F7FBFF">
                    &nbsp;<asp:Literal ID="ltrToPlaneNo" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#D7E9FF" class="regformth">
                    出发时间：
                </td>
                <td bgcolor="#F7FBFF">
                    &nbsp;<asp:Literal ID="ltrStarDate" runat="server"></asp:Literal>
                    &nbsp;&nbsp;时间范围：<asp:Literal ID="ltrRangeTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF" class="regformth">
                    备注：
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrNotes" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF" class="regformth">
                    <font color="#FF0000">联系人:</font>
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrContactName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF" class="regformth">
                    <font color="#FF0000">联系QQ:</font>
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrContactQQ" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF" class="regformth">
                    <font color="#FF0000">希望价格:</font>
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrWishPirce" runat="server"></asp:Literal>￥
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF" class="regformth">
                    <font color="#FF0000">邮件地址:</font>
                </td>
                <td bgcolor="#F7FBFF">
                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="" align="right" bgcolor="#D7E9FF">
                    乘客信息：
                </td>
                <td bgcolor="#F7FBFF">
                    <cc1:CustomRepeater ID="crp_PassongerList" runat="server">
                        <HeaderTemplate>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#d0ccca"
                                id="PassengersInfo">
                                <tr bgcolor="#EFF5F9">
                                    <th width="18%" height="25" align="center">
                                        姓名
                                    </th>
                                    <th width="18%" align="center">
                                        乘客类型
                                    </th>
                                    <th width="20%" align="center">
                                        证件类型
                                    </th>
                                    <th width="24%" align="center">
                                        证件号码
                                    </th>
                                    <th width="20%">
                                        手机
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td height="25" align="center">
                                    <%#Eval("UName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PassengerType")%>
                                </td>
                                <td align="center">
                                    <%#Eval("DocumentType")%>
                                </td>
                                <td align="center">
                                    <%#Eval("DocumentNo")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Mobile")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </cc1:CustomRepeater>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
