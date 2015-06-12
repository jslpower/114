<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamorderDetail.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.TeamorderDetail" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>单团订单管理-查看</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic ">
        <tr>
            <td width="31%" align="right">
                订单编号：
            </td>
            <td width="69%" align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.OrderNo %>
            </td>
        </tr>
        <tr>
            <td align="right">
                线路名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <a href="AddLine.aspx?LineId=<%=modelMTourList.RouteId %>">
                    <%=modelMTourList.RouteName%></a>出发日期：<%=modelMTourList.StartDate.ToString("yyyy-MM-dd")%>
            </td>
        </tr>
        <tr>
            <td align="right">
                组团社：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <a href="/CompanyManage/AddBusinessMemeber.aspx?EditId=<%=modelMTourList.Travel%>">
                    <%=modelMTourList.TravelName%></a>
                <%=EyouSoft.Common.Utils.GetMQ(modelMTourList.TravelMQ) %>
                联系人：<%=modelMTourList.TravelContact%>
            </td>
        </tr>
        <tr>
            <td align="right">
                专线商：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <a href="/CompanyManage/AddBusinessMemeber.aspx?EditId=<%=modelMTourList.Business%>"
                    target="_blank">
                    <%=modelMTourList.BusinessName%></a>
                <%=EyouSoft.Common.Utils.GetMQ(modelMTourList.BusinessMQ) %>
                联系人：<asp:Literal ID="ContactInfo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right">
                游客：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                
                联系人：<asp:Literal ID="litVisitorContact" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right">
                预定时间：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.IssueTime%>
            </td>
        </tr>
        <tr>
            <td align="right">
                订单联系：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.VisitorContact%>
            </td>
        </tr>
        <tr>
            <td align="right">
                团队参考价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:Literal ID="litPriceTeam" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="31%" align="right">
                往返城市：
            </td>
            <td width="69%" align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.StartCityName %>-
                <%=modelMTourList.EndCityName %>
            </td>
        </tr>
        <tr>
            <td width="31%" align="right">
                往返交通：
            </td>
            <td width="69%" align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.StartTraffic%>-<%=modelMTourList.EndTraffic%>
            </td>
        </tr>
        <tr>
            <td align="right">
                预定人数：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.ScheduleNum%>
            </td>
        </tr>
        <tr>
            <td align="right">
                定金：
            </td>
            <td align="left" bgcolor="#FFFFFF">
              成人  <%=modelMTourList.AdultPrice.ToString("0.00")%>
              儿童  <%=modelMTourList.ChildrenPrice.ToString("0.00")%>
            </td>
        </tr>
        <tr>
            <td align="right">
                最小成团人数：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.GroupNum%>
            </td>
        </tr>
        <tr>
            <td align="right">
                游客备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.VisitorNotes%>
            </td>
        </tr>
        <tr>
            <td align="right">
                组团社备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.TravelNotes%>
            </td>
        </tr>
        <tr>
            <td align="right">
                专线商备注：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=modelMTourList.BusinessNotes%>
            </td>
        </tr>
        <tr>
            <td align="right">
                状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=strStatus %>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" bgcolor="#FFFFFF">
                <asp:Button ID="btnsanve" runat="server" Text="提交保存" OnClick="btnsanve_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
