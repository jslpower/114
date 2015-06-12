<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeLeft.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.ExchangeLeft" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopicControl"
    TagPrefix="uc1" %>
<asp:Panel runat="server" ID="panLogin" Visible="false">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;
        border: 1px solid #FEC698; padding: 1px;">
        <tr>
            <td>
                <img src="<%= ImageServerPath %>/images/UserPublicCenter/loginda.gif" width="246"
                    height="155" border="0" usemap="#Map" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel runat="server" ID="panIsLogin" Visible="false">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #72AB0D;
        margin-bottom: 10px;">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #649807;
                    text-align: left; background: #FFF;">
                    <tr>
                        <td width="558">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" style="border-bottom: 1px solid #C8EA8B;">
                                        <a id="aShop" runat="server" title="点击进去网店" target="_blank">
                                            <asp:Literal runat="server" ID="ltrLog"></asp:Literal></a>
                                    </td>
                                    <td width="54%" style="border-bottom: 1px solid #C8EA8B;">
                                        <span class="chengse14">欢迎您！<asp:Label runat="server" ID="ltrContactName"></asp:Label></span><br />
                                        <span class="chengse">
                                            <asp:Label runat="server" ID="ltrCompanyname"></asp:Label></span><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 20px; height: 26px;">
                                        <a href="<%= Domain.UserBackCenter %>/SystemSet/CompanyInfoSet.aspx">[修改资料]</a>
                                    </td>
                                    <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 10px; height: 26px;">
                                        <a runat="server" id="aSupplyInfo1">查看已发布的供应</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 20px; height: 26px;">
                                        <a runat="server" id="aSupplyInfo2">查看关注的商机</a>
                                    </td>
                                    <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 10px; height: 26px;">
                                        <a runat="server" id="aSupplyInfo3">查看已发布的需求</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FEC698;
    margin-bottom: 10px; background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqhuodongbg.gif) repeat-x bottom;">
    <tr>
        <td>
            <div id="Qhmain_comm1">
                <div class="bar_on_comm1l">
                </div>
                <div id="divSix" class="bar_on_comm1" onmouseover="DivSixShow('divSix','divHelp','you_div_1','you_div_2','bar_on_comm1','bar_un_comm1')">
                    <a href="javascript:void(0)">六大优势</a>
                </div>
                <div id="divHelp" class="bar_un_comm1" onmouseover="DivSixShow('divHelp','divSix','you_div_2','you_div_1','bar_on_comm1','bar_un_comm1')">
                    <a href="javascript:void(0)">帮助中心</a>
                </div>
                <div class="bar_on_comm1r">
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding: 5px; height: 80px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="you_div_1">
                <tr>
                    <td>
                        <table width="96%" border="0" align="center" cellpadding="0" cellspacing="2">
                            <tr>
                                <td width="11%" align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/youshi1.gif" />
                                </td>
                                <td width="39%" align="left">
                                    信息量大
                                </td>
                                <td width="9%" align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/youshi4.gif" />
                                </td>
                                <td width="41%" align="left">
                                    MQ双向沟通
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/youshi2.gif" />
                                </td>
                                <td align="left">
                                    查找方便
                                </td>
                                <td align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/youshi6.gif" />
                                </td>
                                <td align="left">
                                    回复即时提醒
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/youshi3.gif" />
                                </td>
                                <td align="left">
                                    标签醒目<br />
                                </td>
                                <td align="center">
                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/wordg.gif" />
                                </td>
                                <td align="left">
                                    附件上传下载
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="you_div_2" style="display: none;">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%" class="xuetang1">
                                    <ul>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu1.htm">同业供求是什么？</a></li>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu2.htm">我如何发布供求？</a></li>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu3.htm">我如何发布商机？</a></li>
                                    </ul>
                                </td>
                                <td width="50%" class="xuetang1">
                                    <ul>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu4.htm">同业信息安全保障</a></li>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu5.htm">发布信息不显示原因</a></li>
                                        <li>·<a target="_blank" href="/HelpCenter/help/Help_Index.aspx#/HelpCenter/help/gongqiu6.htm">如何查看关注的商机</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FEC698;
    margin-bottom: 10px; background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqhuodongbg.gif) repeat-x bottom;">
    <tr>
        <td class="gqdenglu">
            信息检索
        </td>
    </tr>
    <tr>
        <td style="padding: 4px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td id="ltrTime" style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left;">
                        时间：<asp:Literal runat="server" ID="ltrTime"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding: 8px 0 8px 0;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="17%" valign="top">
                                    标签：
                                </td>
                                <td id="ltrTag" width="83%" style="line-height: 26px;">
                                    <asp:Literal runat="server" ID="ltrTag"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="17%" valign="top">
                                    类别：
                                </td>
                                <td id="ltrType" width="83%" style="line-height: 26px;">
                                    <asp:Literal runat="server" ID="ltrType"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding: 8px 0 8px 0;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="17%" valign="top">
                                    省份：
                                </td>
                                <td id="ltrProvince" width="83%" style="line-height: 26px;">
                                    <asp:Literal runat="server" ID="ltrProvince"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px dashed #F3CDB3; height: 30px; text-align: left; padding: 8px 0 8px 0;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right" valign="bottom">
                                    <strong>查询： </strong>
                                </td>
                                <td>
                                    <input id="txtKeyWord" name="txtKeyWord" type="text" style="height: 16px; width: 140px;
                                        border: 1px solid #999; color: #999999" value="请输入关键字" />
                                </td>
                                <td>
                                    <a id="imgQuery" href="javascript:void(0)">
                                        <img style="cursor: pointer" src="<%= ImageServerPath %>/images/UserPublicCenter/chaxunannui2.gif"
                                            width="45" height="19" /></a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
    <tr>
        <td>
            <asp:Literal runat="server" ID="ltrAdv"></asp:Literal>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="hangleft">
            <strong>查看最多排行榜</strong>
        </td>
    </tr>
    <tr>
        <td class="hanglk">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="xuetang3">
                        <ul>
                            <asp:Repeater runat="server" ID="rptMostArticle">
                                <ItemTemplate>
                                    <li><span class="hong16">
                                        <%# Container.ItemIndex + 1 %></span><a href="/SupplierInfo/ArticleInfo.aspx?Id=<%# Eval("ID") %>"><%# Utils.GetText(Eval("ArticleTitle").ToString(), 17)%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--学堂介绍Start--%>
<uc1:CommonTopicControl runat="server" ID="CommonTopicControl1" PartCss="hangleft"
    TextCss="hanglk" TopNumber="3" PartText="图片新闻" />
<%--学堂介绍End--%>
<asp:Panel runat="server" ID="panUrl" Visible="false">
    <map name="Map" id="Map">
        <area shape="rect" coords="14,104,123,144" href="/Register/CompanyUserRegister.aspx" />
        <area shape="rect" coords="128,102,232,145" href="<%= EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(Utils.GeneratePublicCenterUrl("/SupplierInfo/ExchangeList.aspx", CurrCityId),"") %>" />
    </map>
</asp:Panel>

<script type="text/javascript" language="javascript">
    $(function() {
        $("#ltrType").children().each(function() {
            $(this).click(function() {
                SelectDom(this);
            });
        });
        $("#ltrTime").children().each(function() {
            $(this).click(function() {
                SelectDom(this);
            });
        });
        $("#ltrTag").children().each(function() {
            $(this).click(function() {
                SelectDom(this);
            });
        });
        $("#ltrProvince").children().each(function() {
            $(this).click(function() {
                SelectDom(this);
            });
        });
        $("#imgQuery").click(function() {
            Query();return false;
        });
        $("#txtKeyWord").blur(function() {
            if ($.trim($(this).val()) == "") {
                $(this).val("请输入关键字");
            }
        });
        $("#txtKeyWord").focus(function() {
            if ($.trim($(this).val()) == "请输入关键字") {
                $(this).val("");
            }
        });
    });
    function SelectDom(obj) {
        var currType = $(obj).attr("value");
        $(obj).parent().children().each(function() {
            $(this).removeAttr("state");
            $(this).removeAttr("style");
            if ($(this).attr("value") == currType) {
                $(this).attr("state", "1");
                $(this).attr("style", "background: #CCCCCC; padding: 3px;")
            }
            else {
                $(this).attr("state", "0");
                $(this).attr("style", "padding: 3px;");
            }
        });
    }

    function Query() {
        var time = 0;
        var type = -1;
        var tag = -1;
        var pid = 0;
        var kw = '';
        $("#ltrType").children().each(function() {
            if ($(this).attr("state") == "1") {
                type = $(this).attr("value");
            }
        });
        $("#ltrTime").children().each(function() {
            if ($(this).attr("state") == '1') {
                time = $(this).attr("value");
            }
        });
        $("#ltrTag").children().each(function() {
            if ($(this).attr("state") == "1") {
                tag = $(this).attr("value");
            }
        });
        $("#ltrProvince").children().each(function() {
            if ($(this).attr("state") == "1") {
                pid = $(this).attr("value");
            }
        });
        kw = $("#txtKeyWord").val();
        var url = "/SupplierInfo/ExchangeList.aspx?SearchType=1&time=" + time + "&type=" + type + "&tag=" + tag + "&pid=" + pid + "&kw=" + encodeURI(kw) + "&CityId=" + <%= CurrCityId %>;
        location.href = url;
    }

    function DivSixShow(ShowId, HideId, ShowTableId, HideTableId, ShowStyle, HideStyle) {
        $("#" + ShowId).show();
        $("#" + ShowTableId).show();
        $("#" + HideTableId).hide();
        $("#" + ShowId).attr("class", ShowStyle);
        $("#" + HideId).attr("class", HideStyle);
    }
</script>

