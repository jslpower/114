<%@ Page  Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="TravelDefault.aspx.cs" Inherits="UserPublicCenter.TravelManage.TravelDefault" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="TravelRightControl.ascx" TagName="TravelRightControl" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    &nbsp;
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <asp:Repeater ID="rptHeadList" runat="server">
                <ItemTemplate>
                    <td align="center">
                        <div class="wupings">
                          <%#Eval("RedirectURL").ToString() == Utils.EmptyLinkCode ? "<a target=\"_self\" href=\"" + Eval("RedirectURL") + "\">" : "<a target=\"_blank\" href=\"" + Eval("RedirectURL") + "\">"%>
                            
                                <img src='<%# Domain.FileSystem + Eval("ImgPath").ToString() %>' width="144px" height="92px"
                                    border="0" /></a></div>
                        <div class="wupingx">
                            <%#Eval("RedirectURL").ToString() == Utils.EmptyLinkCode ? "<a target=\"_self\" href=\"" + Eval("RedirectURL") + "\">" : "<a target=\"_blank\" href=\"" + Eval("RedirectURL") + "\">"%>
                                <%#Utils.GetText(Eval("Title").ToString(),11,true)%></a></div>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
    </table>
    <table width="970px" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <div class="goulefths">
                    <strong>旅游用品列表</strong></div>
                <div class="goulefthx">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="5%" class="hei14">
                                搜索
                            </td>
                            <td width="60%">
                                <input id="txtSearchVal" type="text" value="输入关键字" style="height: 18px; width: 160px;
                                    line-height: 18px; color: #999; border: 1px solid #7E9DB0;" runat="server" />
                                <uc3:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                            </td>
                            <td width="35%">
                                <img id="ImgSearch" src="<%=ImageServerPath %>/images/UserPublicCenter/gwss.gif"
                                    width="69" height="21" style="cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Repeater ID="rptList" runat="server" OnItemCreated="rptList_ItemCreated">
                    <ItemTemplate>
                        <div class="gouwucp">
                            <div class="gwcptu">
                               <a  target="_blank"  href="<%#EyouSoft.Common.Utils.GetCompanyDomain(Eval("id").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店,this.CityId) %>">
                                    <img src="<%#Utils.GetNewImgUrl(Eval("CompanyImgThumb").ToString(),3) %>" width="92"
                                        height="84" /></a>
                                        
                                        </div>
                            <div class="gouwunei">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="huise" style="table-layout: fixed">
                                    <tr>
                                        <td width="49%">
                                           <span class="lan14"> 
                                           
                                         <a  target="_blank" title="<%#Eval("CompanyName") %>"  class="lan" href="<%#EyouSoft.Common.Utils.GetCompanyDomain(Eval("id").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店,this.CityId) %>">
                                                <strong>
                                                    <%#Utils.GetText(Eval("CompanyName").ToString(),15,true) %></strong></span></a>
                                        </td>
                                        <td width="51%">
                                            地区：<strong>
                                                <asp:Label ID="lblProvince" runat="server" Text=""></asp:Label>&nbsp;<asp:Label ID="lblCity"
                                                    runat="server" Text=""></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="word-wrap: break-word;">
                                            主营产品：<%# Utils.GetText(Eval("ShortRemark").ToString(),90,true)%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlTelPhone" runat="server">
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlFax" runat="server">
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlContact" runat="server">
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                 <div style=" clear:both;"></div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="digg">
                                <cc2:ExporPageInfoSelect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="NewButton"
                                    CurrencyPageCssClass="RedFnt" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="gwright">
                            <div class="gwrhang">
                                旅游用品新货上架</div>
                            <div class="gwrnei">
                                <ul>
                                    <asp:Repeater ID="rptNewTravel" runat="server">
                                        <ItemTemplate>
                                            <li><a  target="_blank"  href='<%#Utils.GetWordAdvLinkUrl(1,Convert.ToInt32(Eval("advId")),-1,this.CityId)%>'>
                                                <%#Utils.GetText(Eval("Title").ToString(),15,true)%>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <uc2:TravelRightControl ID="TravelRightControl1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        $(function() {
            $("#ImgSearch").click(function() {
                if ($("#ctl00_Main_txtSearchVal").val() == "输入关键字") {
                    $("#ctl00_Main_txtSearchVal").val("");
                }
                var params = { searchVal: encodeURIComponent($("#ctl00_Main_txtSearchVal").val()), pid: $("#ctl00_Main_ProvinceAndCityList1_ddl_ProvinceList").val(), cid: $("#ctl00_Main_ProvinceAndCityList1_ddl_CityList").val(),CityId:<%=this.CityId %>  };
                var str = $.param(params);
                window.location.href = "/TravelManage/TravelDefault.aspx?" + str;
            });

            $("#<%=txtSearchVal.ClientID %>").focus(function() {
            $(this).css("color", "black");
            if ($(this).val() == "输入关键字") {
                   
                    $(this).val("");
                }
            });
            
            $("#<%=txtSearchVal.ClientID %>").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#ImgSearch").click();
                    return false;
                }
            }); 
        });

    
    </script>

</asp:Content>
