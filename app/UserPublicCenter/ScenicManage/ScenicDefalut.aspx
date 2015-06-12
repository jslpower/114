<%@ Page Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ScenicDefalut.aspx.cs" Inherits="UserPublicCenter.ScenicManage.ScenicDefalut" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="~/ScenicManage/TicketsControl.ascx" TagName="NewTicketsCotrol"
    TagPrefix="uc2" %>
<%@ Register Src="ViewRightControl.ascx" TagName="ViewRightControl" TagPrefix="uc3" %>
<%@ Register Src="NewAttrControl.ascx" TagName="NewAttrControl" TagPrefix="uc4" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="Main" runat="server" ID="Content1">
    <link href="<%= CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("toplist") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

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

    <!--城市和菜单 start-->
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <!--城市和菜单 end-->
    <!-- 频道通栏广告1 start-->
    <div class="boxbanner">
        <asp:Literal ID="ltlImgBoxBanner" runat="server"></asp:Literal>
    </div>
    <!-- 频道通栏广告1 end-->
    <!-- 热门目的地 start -->
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="226" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="gwright2">
                            <div class="gwrhang2">
                                <div class="retubiao2">
                                    热门目的地</div>
                            </div>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="25%" class="lan14" height="26">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=362",this.CityId) %>">
                                            杭州</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=367",this.CityId) %>">
                                            宁波</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=292",this.CityId) %>">
                                            上海</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=19",this.CityId) %>">
                                            北京</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lan14" height="26">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=48",this.CityId) %>">
                                            广州</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=59",this.CityId) %>">
                                            深圳</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=192",this.CityId) %>">
                                            南京</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=257",this.CityId) %>">
                                            济南</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" class="lan14" height="26">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=262",this.CityId) %>">
                                            青岛</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=166",this.CityId) %>">
                                            长沙</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=295",this.CityId) %>">
                                            成都</a>
                                    </td>
                                    <td width="25%" class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=352",this.CityId) %>">
                                            昆明</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lan14" height="26">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=288",this.CityId) %>">
                                            西安</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=225",this.CityId) %>">
                                            沈阳</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=217",this.CityId) %>">
                                            大连</a>
                                    </td>
                                    <td class="lan14">
                                        <a href="<%=Utils.GeneratePublicCenterUrl("/ScenicManage/ScenicList.aspx?cid=373",this.CityId) %>">
                                            重庆</a>
                                    </td>
                                </tr>
                            </table>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100%" class="hui12">
                                        全国共<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>个景区
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10">
                    <tr>
                        <td>
                            <asp:Literal ID="ltlImgTitleAdv" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="504" valign="top">
                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                    width="504" height="360">
                    <param name="movie" value="<%=ImageServerPath %>/images/UserPublicCenter/map.swf" />
                    <param name="quality" value="high" />
                    <param name="flashvars" value="cityid=<%=this.CityId %>&sitedomain=<%=Domain.UserPublicCenter %>" />
                    <param name="AllowScriptAccess" value="always" />
                    <embed src="<%=ImageServerPath %>/images/UserPublicCenter/map.swf" quality="high"
                        flashvars="cityid=<%=this.CityId %>&sitedomain=<%=Domain.UserPublicCenter %>"
                        allowscriptaccess="always" pluginspage="http://www.macromedia.com/go/getflashplayer"
                        type="application/x-shockwave-flash" width="504" height="360"></embed>
                </object>
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jdrh_xin">
                            <div class="zuixin">
                                金牌景区</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="jdrk_gai">
                            <uc4:NewAttrControl ID="NewAttrControl1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!-- 热门目的地 end-->
    <!--频道通栏广告2 start-->
    <div class="boxbanner">
        <asp:Literal ID="litImgBoxBannerSecond" runat="server"></asp:Literal>
    </div>
    <!--频道通栏广告2 end-->
    <!-- 景区列表 start-->
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
                                <input name="textfield" id="txtScenicName" type="text" value="输入景点关键字" style="height: 22px;
                                    width: 200px; color: #999; border: 1px solid #7E9DB0;" />
                            </td>
                            <td width="58%" align="left" valign="middle">
                                <a href="javascript:GoScenicList()">
                                    <img src="<%=ImageServerPath %>/Images/UserPublicCenter/jq4_07.jpg" width="126" height="38"
                                        alt=" " /></a>
                            </td>
                        </tr>
                    </table>

                    <script type="text/javascript">
                        $(function() {
                            $("#txtScenicName").focus(function() {
                                $(this).css("color", "black");
                                if ($(this).val() == "输入景点关键字") {
                                    $(this).val("");
                                }
                            });
                            $("#txtScenicName").keydown(function(event) {
                                if (event.keyCode == 13) {
                                    GoScenicList();
                                    return false;
                                }
                            });
                            $("#txtScenicName").focus(function() {
                                $(this).css("color", "black");
                                if ($(this).val() == "输入景点关键字") {
                                    $(this).val("");
                                }
                            });
                            $("#txtScenicName").blur(function() {
                                $(this).css("color", "#999");
                                if ($(this).val() == "") {
                                    $(this).val("输入景点关键字");
                                }
                            });
                        });
                        function GoScenicList() {
                            var scenicName = $("#txtScenicName").val();
                            var url;
                            if (scenicName == "" || scenicName == "输入景点关键字")
                                url = "/ScenicManage/ScenicList.aspx"
                            else
                                url = "/ScenicManage/ScenicList.aspx?searchVal=" + encodeURIComponent(scenicName);
                            window.location.href = url;
                        }
                    </script>

                    <div class="xuanzexian">
                        <ul>
                            <li>
                                <div class="xianlu_zt">
                                    <strong>省份：</strong></div>
                                <div class="xianlink" id="province">
                                    <asp:Literal ID="ltlProvince" runat="server"></asp:Literal>
                                </div>
                            </li>
                            <li>
                                <div class="xianlu_zt">
                                    <strong>主题：</strong></div>
                                <div class="xianlink" id="city">
                                    <asp:Literal ID="ltlTheme" runat="server"></asp:Literal>
                                </div>
                            </li>
                        </ul>
                        <div class="xianluss">
                        </div>
                    </div>
                </div>
                <div class="santu">
                    <ul>
                        <asp:Repeater ID="rpt_ScenicImg" runat="server">
                            <ItemTemplate>
                                <li><a target="_blank" href='<%# Utils.GetShopUrl(Eval("CompanyId").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.景区,0)%>'
                                    title='<%# Eval("ScenicName") %>'>
                                    <img src="<%# Utils.GetNewImgUrl(Eval("ThumbAddress").ToString(),2) %>" width="160"
                                        height="120" class="dianjihou" alt='<%# Eval("ScenicName") %>' /></a> <a target="_blank"
                                            href='<%# Utils.GetShopUrl(Eval("CompanyId").ToString(),EyouSoft.Model.CompanyStructure.CompanyType.景区,0)%>'>
                                            <%# Eval("ScenicName") %>
                                        </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <asp:Repeater ID="rpt_ScenicList" runat="server">
                    <ItemTemplate>
                        <div class="gouwucp_xin">
                            <div class="gwcptu_xin">
                                <a href='/jingquinfo_<%# Eval("Id") %>' title='<%# Eval("ScenicName") %>'>
                                    <img src="<%# Utils.GetNewImgUrl(GetScenicImg(Eval("Img")),2) %>" alt='<%# Eval("ScenicName") %>'
                                        width="112" height="84" border="0" /></a></div>
                            <div class="gouwunei_xin">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="huise">
                                    <tr>
                                        <td width="49%">
                                            <strong><span class="lan14"><a href='/jingquinfo_<%# Eval("Id") %>'>
                                                <%# Eval("ScenicName") %>
                                                <%#EyouSoft.Common.Utils.GetCompanyLevImg(((EyouSoft.Model.CompanyStructure.CompanyInfo)Eval("Company")).CompanyLev)%></a></span></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="line-height: 140%;">
                                            <%# Utils.GetText2(Utils.InputText(Eval("Description")),171,true) %>
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
                            <div class="digg">
                                <cc1:ExporPageInfoSelect ID="ExportPageInfo" runat="server" LinkType="3" PageStyleType="NewButton"
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
                                特价门票</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="jdrk_gai">
                            <uc2:NewTicketsCotrol ID="NewTicketsControl" runat="server" />
                        </td>
                    </tr>
                </table>
                <uc3:ViewRightControl runat="server" ID="ViewRightControl1" />
            </td>
        </tr>
    </table>
    <!-- 景区列表 end-->
    <div class="clear">
    </div>
    <div class="hr_10">
    </div>
</asp:Content>
