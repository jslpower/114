<%@ Page Title="景区" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ScenicList.aspx.cs" Inherits="UserPublicCenter.ScenicManage.ScenicList"
    EnableEventValidation="false" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<%@ Register Src="ViewRightControl.ascx" TagName="ViewRightControl" TagPrefix="uc3" %>
<%@ Register Src="NewAttrControl.ascx" TagName="NewAttrControl" TagPrefix="uc4" %>
<%@ Register Src="TicketsControl.ascx" TagName="TicketsControl" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("toplist") %>"></script>

    <script language="javascript" type="text/javascript">
        function pucker_show(name, no, hiddenclassname, showclassname, count) {
            for (var i = 1; i < count + 1; i++) {
                document.getElementById(name + i).className = hiddenclassname;
            }
            document.getElementById(name + no).className = showclassname;
        }
        function pucker_show1(name, no, hiddenclassname, showclassname, count) {
            for (var i = 1; i < count + 1; i++) {
                document.getElementById(name + i).className = hiddenclassname;
            }
            document.getElementById(name + no).className = showclassname;
        }
        $(function() {
            $(".gouwucp_xin").hover(function() { $(this).addClass("hover") }, function() {
                $(this).removeClass("hover")
            });
        });
    </script>

    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="boxbanner">
        <asp:Literal ID="litImgBoxBannerSecond" runat="server"></asp:Literal>
        <div class="xiaodaohang2">
      <div>您的位置：<a href="<%=SubStation.CityUrlRewrite(CityId) %>">同业114</a>&nbsp;&gt;&nbsp;<a href="<%=ScenicSpot.ScenicDefalutUrl(CityId) %>">景区</a>&nbsp;&gt;&nbsp;<span class="zuouhou"><%=ProvinceName+CityName %>景区</span></div>
</div>
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <div class="goulefthx_xin">
                    <table style="border-bottom: 1px dotted #CCC;" width="100%" height="50" border="0"
                        cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="13%" class="hei14">
                                <img src="<%=ImageServerPath %>/Images/UserPublicCenter/jq4_10.jpg" width="84" height="28"
                                    alt="景区名称" />
                            </td>
                            <td width="29%">
                                <input name="textfield" id="textfield" runat="server" type="text" value="输入景点关键字"
                                    style="height: 22px; width: 200px; color: #999; border: 1px solid #7E9DB0;" />
                            </td>
                            <td width="58%" align="left" valign="middle">
                                <a href="javascript:void(0)">
                                    <img src="<%=ImageServerPath %>/Images/UserPublicCenter/jq4_07.jpg" width="126" height="38"
                                        id="ImgSearch" /></a>
                            </td>
                        </tr>
                    </table>
                    <div class="xuanzexian">
                        <ul>
                            <li>
                                <div class="xianlu_zt">
                                    <a id="SetProvince" style="cursor: pointer"><strong>省份</strong></a>：</div>
                                <div class="xianlink">
                                    <%=Province%>
                                    <input type="hidden" id="hidProvinceList" runat="server" />
                                </div>
                            </li>
                            <li>
                                <div class="xianlu_zt">
                                    <a id="SetCity" style="cursor: pointer"><strong>城市</strong></a>：</div>
                                <div class="xianlink">
                                    <%=CityList %>
                                    <input type="hidden" id="hidCityList" runat="server" />
                                </div>
                            </li>
                            <li>
                                <div class="xianlu_zt">
                                    <a id="SetTheme" style="cursor: pointer"><strong>主题</strong></a>：</div>
                                <div class="xianlink">
                                    <%=ThemeList%>
                                    <input type="hidden" id="hidThemeList" runat="server" />
                                </div>
                            </li>
                        </ul>
                        <div class="xianluss">
                        </div>
                    </div>
                </div>
                <asp:Repeater runat="server" ID="SceniceList">
                    <ItemTemplate>
                        <div class="gouwucp_xin">
                            <div class="gwcptu_xin">
                                <a href='/jingquinfo_<%# Eval("Id") %>'>
                                    <img src="<%# Utils.GetNewImgUrl(GetSceniceImage(Eval("Img")),2) %>" width="112" alt='<%#Eval("ScenicName") %>'
                                        height="84" border="0" /></a>
                            </div>
                            <div class="gouwunei_xin">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="huise">
                                    <tr>
                                        <td width="49%">
                                            <strong><span class="lan14"><a href='/jingquinfo_<%#Eval("Id") %>'>
                                                <%#Utils.GetText2(Eval("ScenicName").ToString(),15,false)%></a></span></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="line-height: 140%;">
                                            <%#EyouSoft.Common.Utils.GetText(Utils.InputText(Eval("Description") == null ? "" : Eval("Description").ToString()), 140, true)%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clear">
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div class="digg" style="text-align: center">
                                <cc2:ExporPageInfoSelect ID="ExportPageInfo1" runat="server" LinkType="3" PageStyleType="NewButton"
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
                        <td class="jdrh_xin">
                            <div class="zuixin">
                                热门景点</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="jdrk_gai">
                            <uc4:NewAttrControl ID="NewAttrControl1" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="gwright2">
                            <div class="gwrhang_xin">
                                特价门票</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc5:TicketsControl ID="TicketsControl1" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td>
                            <uc3:ViewRightControl runat="server" ID="ViewRightControl1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="clear">
    </div>
    <div class="rmxl">
    </div>
    <div class="hr_10">
    </div>

    <script type="text/javascript">
        $(function() {
            $(".Province").click(function() {
                var ProvinceId = $(this).attr("href");
                $("#<%=hidProvinceList.ClientID %>").val(ProvinceId);
                $("#<%=hidCityList.ClientID %>").val("0");
                Redirect();
                return false;
            });
            $(".City").click(function() {
                var CityId = $(this).attr("href");
                $("#<%=hidCityList.ClientID %>").val(CityId);
                Redirect();
                return false;
            });
            $(".Theme").click(function() {
                var ThemeId = $(this).attr("href");
                //alert(ThemeId);
                $("#<%=hidThemeList.ClientID %>").val(ThemeId);
                Redirect();
                return false;
            });
            $("#ImgSearch").click(function() {
                if ($("#<%=textfield.ClientID %>").val() == "输入景点关键字") {
                    $("#<%=textfield.ClientID %>").val() == "";
                }
                Redirect();
            });
            $("#<%=textfield.ClientID %>").focus(function() {
                $(this).css("color", "black");
                if ($(this).val() == "输入景点关键字") {
                    $(this).val("");
                }
            });
            $("#<%=textfield.ClientID %>").blur(function() {
                $(this).css("color", "#999");
                if ($(this).val() == "") {
                    $(this).val("输入景点关键字");
                }
            });
            $("#<%=textfield.ClientID %>").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#ImgSearch").click();
                    return false;
                }
            });
            $("#SetProvince").click(function() {
                $("#<%=hidProvinceList.ClientID %>").val("0");
                $("#<%=hidCityList.ClientID %>").val("0");
                Redirect();
            });
            $("#SetCity").click(function() {
                $("#<%=hidCityList.ClientID %>").val("0");
                Redirect();
            });
            $("#SetTheme").click(function() {
                $("#<%=hidThemeList.ClientID %>").val("0");
                Redirect();
            });
            $("#<%=textfield.ClientID %>").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#ImgSearch").click();
                    return false;
                }
            })
        });

        function Redirect() {
            var searchVal = $("#<%=textfield.ClientID %>").val();
            var ProvinceId = $("#<%=hidProvinceList.ClientID %>").val();
            var CityId = $("#<%=hidCityList.ClientID %>").val();
            var ThemeId = $("#<%=hidThemeList.ClientID %>").val();
            if (searchVal == "输入景点关键字") {
                searchVal = "";
            }
            var par = $.param({ searchVal: searchVal, pid: ProvinceId, cid: CityId, tid: ThemeId });
            window.location.href = "/ScenicManage/ScenicList.aspx?" + par;
        }
    </script>

</asp:Content>
