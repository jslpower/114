<%@ Page Language="C#" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="SalesRegionManage.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.SalesRegionManage" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="../usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售区域维护</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }

    </script>

    <style type="text/css">
        <!-- body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        --></style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="78%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif">
                <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
             
                &nbsp;&nbsp;&nbsp; 状态：<asp:DropDownList ID="ddlIsEnable" runat="server">
                    <asp:ListItem Value="-1">请选择</asp:ListItem>
                    <asp:ListItem Value="0">禁用</asp:ListItem>
                    <asp:ListItem Value="1">启用</asp:ListItem>
                </asp:DropDownList>
                <asp:ImageButton ID="imb_Qurey" runat="server" Width="62" Height="21" OnClick="imb_Qurey_Click" />
               
            </td>
        </tr>
    </table>
    <asp:Repeater ID="RepeaterList" runat="server" OnItemDataBound="RepeaterList_ItemDataBound"
        OnItemCommand="RepeaterList_ItemCommand">
        <HeaderTemplate>
            <table width="98%" id="ListTable" border="0" align="center" cellpadding="0" cellspacing="1"
                class="kuang">
                <tr class="white" height="23">
                    <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>销售城市</strong>
                    </td>
                    <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>是否出港</strong>
                    </td>
                    <td width="10%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>关联专线商</strong>
                    </td>
                    <td width="50%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>关联线路区域</strong>
                    </td>
                    <td width="12%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>维护线路区域</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                        <strong>操作</strong>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td align="center">
                    <%# DataBinder.Eval(Container.DataItem, "CityName")%>
                </td>
                <td align="center">
                    <!--设置或者取消出港-->
                    <asp:Label ID="lbl_SetOCity" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"IsSite") %>'></asp:Label>
                    <asp:LinkButton ID="lbt_SetOutCity" CommandName="SetCity" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CityId") %>'
                        runat="server"></asp:LinkButton>
                </td>
                <td align="center">
                    <%# GetRelationCompanyCount(DataBinder.Eval(Container.DataItem, "AreaAdvNum"), int.Parse(DataBinder.Eval(Container.DataItem, "CityId").ToString()), int.Parse(DataBinder.Eval(Container.DataItem, "ProvinceId").ToString()))%>
                </td>
                <td align="center">
                    <table width="100%" border="1" cellspacing="0" cellpadding="2" style="margin: 5px;">
                        <tr>
                            <td width="20%" align="right">
                                国内长线：
                            </td>
                            <td width="80%" align="left">
                                <%# GetGNAreaName(DataBinder.Eval(Container.DataItem, "CityAreaControls"), DataBinder.Eval(Container.DataItem, "AreaAdvNum"),int.Parse(DataBinder.Eval(Container.DataItem, "CityId").ToString()),0)%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                国际线：
                            </td>
                            <td align="left">
                                <%# GetGNAreaName(DataBinder.Eval(Container.DataItem, "CityAreaControls"), DataBinder.Eval(Container.DataItem, "AreaAdvNum"),int.Parse(DataBinder.Eval(Container.DataItem, "CityId").ToString()),2)%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                周边游：
                            </td>
                            <td align="left">
                                <%# GetGNAreaName(DataBinder.Eval(Container.DataItem, "CityAreaControls"), DataBinder.Eval(Container.DataItem, "AreaAdvNum"),int.Parse(DataBinder.Eval(Container.DataItem, "CityId").ToString()),1)%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center">
                    <%# EditeAreaName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CityId")), Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProvinceId")))%>
                </td>
                <td align="center">
                    <!--操作启用或停用-->
                    <asp:Label ID="lblIsEnable" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"IsEnabled") %>'></asp:Label>
                    <asp:LinkButton ID="linkState" CommandName="Lock" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CityId") %>'
                        runat="server"></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>销售城市</strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>是否出港</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联专线商</strong>
                </td>
                <td width="50%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联线路区域</strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>维护线路区域</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="NoData" runat="server" Visible="False">
        <table width="98%" id="ListTable" border="0" align="center" cellpadding="0" cellspacing="1"
            class="kuang">
            <tr class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>销售城市</strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>是否出港</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联专线商</strong>
                </td>
                <td width="50%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联线路区域</strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>维护线路区域</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
            <tr bgcolor="#f9f9f4">
                <td align="center" colspan="6" height="100">
                    暂无信息
                </td>
            </tr>
            <tr class="white" height="23">
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>销售城市</strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>是否出港</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联专线商</strong>
                </td>
                <td width="50%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>关联线路区域</strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>维护线路区域</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif">
                    <strong>操作</strong>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo2" runat="server" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
