<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlaneNewsList.aspx.cs" 
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Inherits="UserPublicCenter.PlaneInfo.PlaneNewsList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">

    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc2:AdveControl ID="AdveControl1" runat="server" />
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="gqline">
                                      <div id="div_Title" runat="server">
                                        </div>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td class="gqline" style="height: 144px">
                                        <cc1:CustomRepeater ID="rpt_NewsListInfo" runat="server">
                                            <ItemTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin: 15px 0 15px 0">
                                                    <tr>
                                                        <td width="88%" class="gqlist">
                                                            <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString())%>
                                                        </td>
                                                        <td width="12%">
                                                            <%#GetTime(DateTime.Parse(DataBinder.Eval(Container.DataItem, "IssueTime").ToString()))%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </cc1:CustomRepeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="gqline">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <div class="digg" align="center">
                                                        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var PlaneNewsPage={
              mouseovertr: function(o) {
	              o.style.backgroundColor="#E2EDFF";
              },
              mouseouttr: function(o) {
	              o.style.backgroundColor=""
              }
        };
    </script>

</asp:Content>
