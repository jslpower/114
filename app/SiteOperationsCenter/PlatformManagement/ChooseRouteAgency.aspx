<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseRouteAgency.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.ChooseRouteAgency" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售区域维护第三步：选择批发商</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script language="JavaScript">
 
      function mouseovertr(o) {
	      o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
      }
      function mouseouttr(o) {
	      o.style.backgroundColor=""
      }
      function openDialog(linkUrl)
      {
         Boxy.iframeDialog({title:"批发商选择", iframeUrl:linkUrl,width:"780px",height:"480",draggable:true,data:null});
      }
    </script>

    <style type="text/css">
        <!
        -- body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        -- ></style>
</head>
<body>
    <form name="form1" id="form1" runat="server">
    <table width="855" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
        <tr>
            <td style="background: #FFF9E7; border: 1px solid #F6D886; padding: 5px;">
                第一步：选择出港城市。<span class="unnamed1">第二步：设置分站的线路区域</span>&nbsp; 保存提交，成功！
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#C3E3F2">
                指定专线商：
            </td>
            <td width="720" align="left" bgcolor="#C3E3F2">
                单条线路区域最多5家
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                国内长线
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_GNLongRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex + 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <img src="<%=ImageServerUrl%>/images/2004112.gif" width="12" height="12" />
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                </td>
                                <td width="20%" align="left">
                                    <%# GetAgencyCount(DataBinder.Eval(Container.DataItem, "AreaId").ToString())%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                国内短线
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_GNShortRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex + 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <img src="<%=ImageServerUrl%>/images/2004112.gif" width="12" height="12" />
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                </td>
                                <td width="20%" align="left">
                                     <%# GetShortAgencyCount(DataBinder.Eval(Container.DataItem, "AreaId").ToString())%>
                               
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td height="18" align="left" bgcolor="#E8F4FA">
                出境线路
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td width="855">
                <asp:DataList ID="dl_OutRouteList" runat="server" BorderWidth="0px" CellPadding="0"
                    CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="3"
                    RepeatDirection="Horizontal" Width="98%" >
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="<%# Container.ItemIndex + 1 %>">
                            <tr>
                                <td valign="top" align="left" width="40%">
                                    <img src="<%=ImageServerUrl%>/images/2004112.gif" width="12" height="12" />
                                    <%# DataBinder.Eval(Container.DataItem, "AreaName")%>
                                </td>
                                <td width="20%" align="left">
                                     <%# GetOutAgencyCount(DataBinder.Eval(Container.DataItem, "AreaId").ToString())%>
                               
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
