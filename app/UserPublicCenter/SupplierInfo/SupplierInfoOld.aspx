<%@ Page Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master" AutoEventWireup="true"
    CodeBehind="SupplierInfoOld.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.SupplierInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopicControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc2" %>
<%@ Register Src="~/SupplierInfo/UserControl/PopularityCompanyAdv.ascx" TagName="PopularityCompanyAdv"
    TagPrefix="uc4" %>
<%@ Register Src="~/SupplierInfo/UserControl/NewActivityControl.ascx" TagName="NewActivityControl"
    TagPrefix="uc5" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop10"
        style="border: 1px solid #d5d5d5; padding: 1px;">
        <tr>
            <td width="23">
                <div class="gqchuxiao4">
                    促销广告</div>
            </td>
            <td width="25%">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="heise12">
                    <asp:Repeater runat="server" ID="rptPromotionsAdv">
                        <ItemTemplate>
                            <tr>
                                <td height="23" class="gqhong">
                                    <a <%# GetHtmlAFontColor(Container.ItemIndex) %> <%#Eval("AdvId").ToString()=="0" ?" href=\"javascript:void(0)\"":string.Format(" target=\"_blank\" href=\"/PlaneInfo/NewsDetailInfo.aspx?NewsID={0}&CityId={1}\"",Eval("AdvId"),CityId) %>>
                                        ·<%# Utils.GetText(Eval("Title").ToString(), 15, true)%>
                                    </a>
                                </td>
                            </tr>
                            <%# Container.ItemIndex != 0 && Container.ItemIndex != 15 && (Container.ItemIndex + 1) % 4 == 0 ? "</table></td><td width=\"25%\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"heise12\">" : ""%>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop10">
        <tr>
            <td valign="top" width="250">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FEC698;
                    padding: 1px; background: url(<%= ImageServerPath %>/images/UserPublicCenter/gqhuodongbg.gif);
                    margin-bottom: 10px;">
                    <tr>
                        <td width="23">
                            <div class="gqchuxiao4">
                                最新活动</div>
                        </td>
                        <td width="92%" valign="top" style="padding-top: 7px;">
                            <uc5:NewActivityControl runat="server" ID="NewActivityControl1" />
                        </td>
                    </tr>
                </table>
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
                                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/wordg.gif" width="16" height="16" />
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
                <%--本周人气企业广告开始--%>
                <uc4:PopularityCompanyAdv runat="server" ID="PopularityCompanyAdv1" />
                <%--本周人气企业广告结束--%>
                <!-- 左侧资讯开始 -->
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl1" PartText="同业之星访谈"
                    PartCss="hangleft" TextCss="hanglk" TopNumber="1" />
                <div class="maintop10">
                </div>
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl2" PartText="<a  style='color:#333333;' href='/SupplierInfo/SchoolIntroduction.aspx' target='_blank'>同业学堂</a>"
                    PartCss="hangleft" TextCss="hanglk" TopNumber="12" />
                <div class="maintop10">
                </div>
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl3" PartText="最新行业资讯"
                    PartCss="hangleft" TextCss="hanglk" TopNumber="10" />
                <div class="maintop10">
                </div>
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl4" PartText="景区" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="8" />
                <div class="maintop10">
                </div>
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl5" PartText="旅行社" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="8" />
                <div class="maintop10">
                </div>
                <uc1:CommonTopicControl runat="server" ID="CommonTopicControl6" PartText="酒店" PartCss="hangleft"
                    TextCss="hanglk" TopNumber="8" />
                <!-- 左侧资讯结束 -->
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td>
                            <!-- 4张图片广告 开始 -->
                            <table width="100%" border="0" cellspacing="0" cellpadding="0 " style="border: 1px solid #FEC698;
                                padding: 2px; height: 94px; margin-bottom: 10px;">
                                <tr>
                                    <asp:Repeater runat="server" ID="rptBannerAdv">
                                        <ItemTemplate>
                                            <td style="width:25%;">
                                                <%#Eval("RedirectURL").ToString().Trim() == Utils.EmptyLinkCode ? string.Format("<a title=\"{0}\" href=\"{1}\" style=\"width:170px; height:84px;\" ><img alt=\"{0}\" src=\"{2}\" width=\"170\" height=\"84\" /></a>", Eval("Title"), Eval("RedirectURL"), Domain.FileSystem + Eval("ImgPath")) : string.Format("<a title=\"{0}\" target=\"_blank\"  href=\"{1}\" style=\"width:170px; height:84px;\" ><img alt=\"{0}\" src=\"{2}\" width=\"170\" height=\"84\" /></a>", Eval("Title"), Eval("RedirectURL"), Domain.FileSystem + Eval("ImgPath"))%>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                            <!-- 4张图片广告 结束 -->
                            <!-- 发布供求信息  开始 -->
                            <asp:Panel runat="server" ID="panAddExchange" Visible="false">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 2px solid #72AB0D;
                                    margin-bottom: 10px;">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #649807;
                                                text-align: left; background: #FFF;">
                                                <tr>
                                                    <td width="558" style="padding: 4px;">
                                                        <div style="width: 550px; margin-bottom: 5px;" id="Tags">
                                                            <div class="gqbqx1">
                                                                <span class="lanse">标签：</span></div>
                                                            <asp:Repeater runat="server" ID="rptExchangeTag">
                                                                <ItemTemplate>
                                                                    <div class="gqbqx">
                                                                        <input type="radio" id="radioTag<%# Container.ItemIndex + 1 %>" name="radioTag" value="<%# Eval("value") %>" /></div>
                                                                    <div class="gqbiaoqian<%# Container.ItemIndex + 1 %>">
                                                                        <label for="radioTag<%# Container.ItemIndex + 1 %>">
                                                                            <%# Eval("text") %></label></div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div style="line-height: 6px; height: 20px; width: 550px;" id="Types">
                                                            <nobr><span class="lanse">类别：</span>
                                                            <asp:Repeater runat="server" ID="rptExchangeType">
                                                            <ItemTemplate>
                                                                <input type="radio" name="radioType" id="radioType<%# Container.ItemIndex + 1 %>"
                                                                    value="<%# Eval("value") %>" /><label for="radioType<%# Container.ItemIndex + 1 %>"><%# Eval("text") %></label>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        </nobr>
                                                        </div>
                                                        <div style="margin-top: 10px; width: 550px;">
                                                            <textarea runat="server" id="txtInfo" name="txtInfo" style="height: 110px; width: 540px;
                                                                border: 1px solid #666;"></textarea>
                                                        </div>
                                                        <div style="width: 532px; height: 18px; background: #EEEEEE; border: 1px solid #ccc;
                                                            padding: 0px 0px 3px 10px; line-height: 18px;">
                                                            <label style="cursor: pointer; width: 400px; float: left;" onclick="javascript:ShowProvince();">
                                                                默认信息为本地发送，点击此处将此信息发布到以下区域 <span class="gqlvse"><strong id="strongShow">（展开）</strong></span>
                                                            </label>
                                                            <div style="float: left;">
                                                                <label id="fileInfo" style="margin-right: 4px;">
                                                                </label>
                                                                <span id="sp1" style="position: relative; display: block; float: left"><span id="sp"
                                                                    style="float: left; position: absolute; left: 0px; top: 0px">
                                                                    <uc2:SingleFileUpload runat="server" ID="SingleFileUpload1" />
                                                                </span></span>
                                                            </div>
                                                        </div>
                                                        <div style="border: 1px solid #ccc; border-top: 0px; width: 522px; padding: 5px 10px 5px 10px;
                                                            display: none;" id="menu_1" divstate="0">
                                                            <asp:Repeater runat="server" ID="rptProvince">
                                                                <FooterTemplate>
                                                                    <div style="float:right;"><input type="checkbox" id="CkAllProvinces" /><label for="CkAllProvinces">(全选/反选)</label></div>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <nobr><input id="ckb<%# Container.ItemIndex + 1 %>" name="ckbProvince" value="<%# Eval("ProvinceId") %>"
                                                                        type="checkbox"><label for="ckb<%# Container.ItemIndex + 1 %>"><%# Eval("ProvinceName") %></label></nobr>
                                                                </ItemTemplate> 
                                                            </asp:Repeater>
                                                        </div>
                                                        <div style="text-align: right; padding: 3px 5px 10px 0; height: 26px; width: 540px;">
                                                            <div style="float: left; width: 370px; padding-top: 7px;">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td align="">
                                                                            联系人：
                                                                        </td>
                                                                        <td align="left">
                                                                            <input runat="server" id="txtName" type="text" style="width: 40px; border: 1px solid #ccc;
                                                                                height: 16px; font-size: 14px; color: #666; text-align: center; padding-top: 0px;" />
                                                                        </td>
                                                                        <td>
                                                                            MQ：
                                                                        </td>
                                                                        <td align="left">
                                                                            <input runat="server" id="txtMQ" readonly type="text" style="width: 55px; border: 1px solid #ccc;
                                                                                height: 16px; font-size: 14px; color: #666; text-align: center; padding-top: 0px;" />
                                                                        </td>
                                                                        <td>
                                                                            联系电话：
                                                                        </td>
                                                                        <td align="left">
                                                                            <input runat="server" id="txtTel" type="text" style="width: 90px; border: 1px solid #ccc;
                                                                                height: 16px; font-size: 14px; color: #666; text-align: center; padding-top: 0px;" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div>
                                                                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click">
                                                                    <img src="<%= ImageServerPath %>/images/UserPublicCenter/gqfban.gif" width="161" height="33" border="0" /> 
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td width="144" valign="top" style="border-left: 1px solid #C8EA8B;">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td class="gqsrzi">
                                                                    <asp:Label runat="server" ID="ltrCompanyName"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-bottom: 1px solid #C8EA8B;">
                                                                    <asp:Literal runat="server" ID="ltrCompanyLog"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 10px; height: 26px;
                                                                    height: 26px;">
                                                                    <a runat="server" id="aShop" target="_blank">[进入网店]</a>&nbsp;<a runat="server" id="aEditUser">[修改资料]</a>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                                <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 20px; height: 26px;">
                                                                    <a href="#">收件箱（5/30）</a>
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 20px; height: 26px;">
                                                                    <a runat="server" id="aSupplyInfo1">查看已发布的供应</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="gqlvse" style="border-bottom: 1px solid #C8EA8B; padding-left: 20px; height: 26px;">
                                                                    <a runat="server" id="aSupplyInfo2">查看已发布的需求 </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="gqlvse" style="padding-left: 20px; height: 26px;">
                                                                    <a runat="server" id="aSupplyInfo3">查看关注的商机</a>
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
                            <!-- 发布供求信息  结束 -->
                            <!-- 供求信息列表  开始 -->
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="chaxun">
                                <tr>
                                    <td width="28%">
                                        <div>
                                            <a id="a5" aname="atime" value="-4" state="1" href="javascript:void(0);" class="jintian">
                                                全部</a></div>
                                        <div>
                                            <a id="a1" aname="atime" value="0" state="0" href="javascript:void(0);" class="zuotian">
                                                今天</a></div>
                                        <div>
                                            <a id="a2" aname="atime" value="-1" state="0" href="javascript:void(0);" class="zuotian">
                                                昨天</a></div>
                                        <div>
                                            <a id="a3" aname="atime" value="-2" state="0" href="javascript:void(0);" class="zuotian">
                                                前天</a></div>
                                        <div>
                                            <a id="a4" aname="atime" value="-4" state="0" href="javascript:void(0);" class="zuotian">
                                                更早</a></div>
                                    </td>
                                    <td width="2%" align="right">
                                        <img src="<%= ImageServerPath %>/images/UserPublicCenter/chatu.gif" width="15" height="16" />
                                    </td>
                                    <td width="6%" align="right">
                                        <span class="heise1"><strong>查询： </strong></span>
                                    </td>
                                    <td width="56%" align="left">
                                        <strong>
                                            <asp:DropDownList runat="server" ID="ddlTag">
                                                <asp:ListItem Text="--标签--" Value="-1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="ddlType">
                                                <asp:ListItem Text="--类别--" Value="-1"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="ddlProvince">
                                                <asp:ListItem Text="--省份--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <input id="txtKeyWord" name="txtKeyWord" value="请输入关键字" type="text" style="height: 16px;
                                                width: 120px; border: 1px solid #999; color: #999999" />
                                        </strong>
                                    </td>
                                    <td width="8%">
                                        <strong>
                                            <img alt="查询" style="cursor: hand;" id="imgQuery" src="<%= ImageServerPath %>/images/UserPublicCenter/chaxunannui2.gif"
                                                width="45" height="19" /></strong>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10"
                                style="border: 1px solid #FFC54B;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="591">
                                                    <div id="Qhmain_comm">
                                                        <div class="bar_on_comm" id="che_link_1" divname="chelink" state="1" value="-1">
                                                            <a href="javascript:void(0)">全部信息</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_2" divname="chelink" state="0" value="1">
                                                            <a href="javascript:void(0)">团队询价</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_3" divname="chelink" state="0" value="2">
                                                            <a href="javascript:void(0)">地接报价</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_4" divname="chelink" state="0" value="3">
                                                            <a href="javascript:void(0)">直通车</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_5" divname="chelink" state="0" value="4">
                                                            <a href="javascript:void(0)">车辆</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_6" divname="chelink" state="0" value="5">
                                                            <a href="javascript:void(0)">酒店</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_7" divname="chelink" state="0" value="6">
                                                            <a href="javascript:void(0)">导游/招聘</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_8" divname="chelink" state="0" value="7">
                                                            <a href="javascript:void(0)">票务/签证</a>
                                                        </div>
                                                        <div class="bar_un_comm" id="che_link_9" divname="chelink" state="0" value="8">
                                                            <a href="javascript:void(0)">其他</a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td class="huise" style="background: url(<%= ImageServerPath %>/images/UserPublicCenter/hangri.gif) repeat-x;
                                                    text-align: right; padding-right: 10px;">
                                                    <a href="/SupplierInfo/ExchangeList.aspx?cityId=<%= base.CityId %>" target="_blank">
                                                        更多>></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="che_div_1">
                                            <tr>
                                                <td>
                                                    <table id="TopExchange" width="100%" border="0" cellspacing="0" cellpadding="0" class="gqtjbg">
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater runat="server" ID="rptTopExchange" OnItemDataBound="rptTopExchange_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="gqtjbia" style="border-bottom: 1px dashed #D49C79;
                                                                            padding: 5px;">
                                                                            <tr>
                                                                                <td width="15%" height="80">
                                                                                    <asp:Literal runat="server" ID="ltrImg"></asp:Literal>
                                                                                </td>
                                                                                <td width="85%">
                                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td colspan="2" class="chengse14">
                                                                                                <strong><a target="_blank" href="<%# EyouSoft.Common.Utils.GetWordAdvLinkUrl(1,int.Parse(Eval("AdvId").ToString()),-1,CityId) %>" title="<%# Eval("Title") %>">
                                                                                                    <%# Utils.GetText(Eval("Title").ToString(),40) %></a>
        <%--<a target="_blank" href="<%# EyouSoft.Common.Utils.Utils.GeneratePublicCenterUrl(EyouSoft.Common.Domain.UserPublicCenter + "/SupplierInfo/ExchangeList.aspx", CityId) %>" title="<%# Eval("Title") %>"><%# Utils.GetText(Eval("Title").ToString(),40) %></a>--%>
                                  </strong>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="48%">
                                                                                                <span class="huise">供应商：</span><%# Eval("CompanyName")%><asp:Literal runat="server"
                                                                                                    ID="ltrMQ"></asp:Literal>
                                                                                            </td>
                                                                                            <td width="52%">
                                                                                                <span class="huise">发布日期：</span><%# Eval("IssueTime","{0:MM-dd hh:mm}") %>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <span class="huise">内容介绍：</span><%# Eval("Remark")!=null?Utils.GetText(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Remark").ToString()),90,true):"" %>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="divExchangeList">
                                                    </div>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="TdPage" style="margin-bottom: 12px;">
                                                        <tr>
                                                            <td>
                                                                <div id="DivPage" class="digg">
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
                            <!-- 供求信息列表  结束 -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

    <script type="text/javascript" language="javascript">
        var LoadCount=0;
        function fileQueueError(file, errorCode, message) {
            try {
                var object = this.getStats();
                switch (errorCode) {
                    case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
                        var fileCount = this.getSetting("file_upload_limit");
                        errorName = "当前只能上传" + fileCount + "个文件.";
                        break;
                    case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                        errorName = "您选择的文件是空的"
                        break;
                    case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                        errorName = "您选择的文件超过了指定的大小" + this.getSetting("file_size_limit");
                        break;
                    case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                        errorName = "错误的文件类型,只能上传" + this.getSetting("file_types");
                        break;
                    default:
                        errorName = message;
                        break;
                }
                alert(errorName);

            } catch (ex) {
                //this.debug(ex);
            }

        }
        function fileQueued(file) {
            try {
                var self = this;
                var hidFileName = document.getElementById(this.customSettings.HidFileNameId);
                hidFileName.value = "";
                var newName;
                var str = file.name.split('.');
                if (str[0].length > 5) {
                    newName = str[0].substring(0, 5);
                }
                else {
                    newName = str[0];
                }
                newName += "." + str[1];
                $("#fileInfo").show();
                $("#fileInfo").html("<label title=\"" + file.name + "\">" + newName + "</label><a id=\"delfile\" href=\"javascript:;\">&nbsp;删除</a>");
                $("#delfile").click(function() {
                    $("#fileInfo").hide();
                    if($.browser.mozilla)
                    {
                         $("#SWFUpload_0").attr("height", 20);
                         $("#SWFUpload_0").attr("width", 95);
                    }
                    else
                    {
                        $("#sp1").css({left:'0px'});
                        $("#sp1").css({position:'relative'});
                    }
                    resetSwfupload(self, file);
                    return false;
                });  
                if($.browser.mozilla)
                {
                     $("#SWFUpload_0").attr("height", 0);
                     $("#SWFUpload_0").attr("width", 0);
                }
                else
                {           
                    $("#sp1").css({position:'absolute',left:'-100px'});
                }
                var progress = new FileProgress(file, this.customSettings.upload_target, this);

            } catch (e) {
            }
        }
        //显示/隐藏省份选择框
        function ShowProvince() {
            if ($("#menu_1").attr("divState") == 0) {
                $("#menu_1").show();
                $("#menu_1").attr("divState", "1");
                $("#strongShow").html("（关闭）");
            }
            else {
                $("#menu_1").hide();
                $("#menu_1").attr("divState", "0");
                $("#strongShow").html("（展开）");
            }
        }
        
        function LoadExchangeList(SearchMode, pageIndex,pageSize) {
            var time = 0;
            var type = -1;
            var tag = -1;
            var pid = "<%= CityModel.ProvinceId %>";
            var kw = '';
            var cityid=0;
            var urlParams = '';
            var psize=30;
            psize=pageSize!=null?pageSize:psize;
            $(":div[divname]='chelink'").each(function() {
                if ($(this).attr("state") == "1") {
                    type = $(this).attr("value");
                }
            });
            $(":a[aname]='atime'").each(function() {
                if ($(this).attr("state") == '1') {
                    time = $(this).attr("value");
                }
            });
            tag = $("#<%= ddlTag.ClientID %>").val();
            if (SearchMode == "Query") //此处为按钮提交
            {
                type = $("#<%= ddlType.ClientID %>").val();
                $(":div[divname]='chelink'").each(function() {
                    if ($(this).attr("value") == type) {
                        divClick(this);
                    }
                });
            }
            if(type==-1)
            {
                psize=20;
                $("#TopExchange").show();
            }
            else
            {
                $("#TopExchange").hide();
            }
            cityid="<%= base.CityId %>";
            pid = $("#<%= ddlProvince.ClientID %>").val()!="0"?$("#<%= ddlProvince.ClientID %>").val():pid;
            kw = $("#txtKeyWord").val();
            kw=kw=="请输入关键字"?"":kw;
            urlParams = "psize="+psize+"&cityId="+cityid+"&time=" + time + "&type=" + type + "&tag=" + tag + "&pindex=" + pageIndex + "&pid=" + pid + "&kw=" + encodeURI(kw)+"&round="+Math.round(Math.random()*1000);
            if (urlParams != '') {
                $("#divExchangeList").html("<img id=\"img_loading\" src='\<%= ImageServerPath %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
                $.ajax({
                    type: "GET",
                    url: "/SupplierInfo/AjaxSupplierInfo.aspx",
                    data: urlParams,
                    async: false,
                    cache:false,
                    success: function(msg) {
                        $("#divExchangeList").html(msg);
                    }
                });
                if(LoadCount>0)
                {
                     scroll(0,300);
                }
                var config = {
                    pageSize: parseInt($("#hPageSize").val()),
                    pageIndex: parseInt($("#hPageIndex").val()),
                    recordCount: parseInt($("#hRecordCount").val()),
                    pageCount: 0,
                    gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                    showPrev: true,
                    showNext: true
                }
                AjaxPageControls.replace("DivPage", config);
                AjaxPageControls.gotoPage = function(pIndex) {
                    LoadExchangeList("link", pIndex);
                }
                LoadCount+=1;
            }
        }
        var isSubmit = false; //区分按钮是否提交过
        //模拟一个提交按钮事件
        function doSubmit() {
            isSubmit = true;
            $("#<%=btnSave.ClientID%>").click();
        }
        $(document).ready(function() {
            $("#CkAllProvinces").change(function(){
                var state=$(this).attr("checked");
                $(":checkbox[name]='ckbProvince'").each(function(){
                    $(this).attr("checked",state);
                });
            });
            $("#txtKeyWord").blur(function(){
                if($(this).val()=="")
                    $(this).val("请输入关键字");
            });
            $("#txtKeyWord").focus(function(){
                if($(this).val()=="请输入关键字")
                    $(this).val("");
            });
            $("#<%= btnSave.ClientID %>").click(function() {
                var sfu1 = ctl00_ctl00_Main_SupplierMain_SingleFileUpload1;
                var _islogin = "<%= IsLogin %>";
                if (_islogin == "False") {
                    location.href = "/Register/Login.aspx?returnurl=/SupplierInfo/SupplierInfo.aspx";
                    return false;
                }
                else {  
                    if (isSubmit) {
                        //如果按钮已经提交过一次验证，则返回执行保存操作
                        __doPostBack('<%= btnSave.UniqueID %>','')
                    }
                    var errmsg = "";
                    if ($("#Tags").children().find(":radio[checked]").length == 0) {
                        errmsg += "请选择标签\n";
                    }
                    if ($("#Types").children().find(":radio[checked]").length == 0) {
                        errmsg += "请选择类别\n";
                    }
                    if ($.trim($("#<%= txtInfo.ClientID %>").val()) == "") {
                        errmsg += "请输入供求信息内容\n";
                    }
                    if ($.trim($("#<%= txtMQ.ClientID %>").val()) == "") {
                        errmsg += "请输入MQ\n";
                    }
                    if ($.trim(errmsg) != "") {
                        alert(errmsg);
                        return false;
                    }
                    if (sfu1.getStats().files_queued <= 0) {
                        return true;
                    }
                    sfu1.customSettings.UploadSucessCallback = doSubmit;
                    sfu1.startUpload();
                    return false;
                }
            });

            $(":a[aname]='atime'").each(function() {
                $(this).click(function() {
                    aClick(this);
                    LoadExchangeList("link", 1);
                });
            });

            $("#imgQuery").bind("click", function() {
                LoadExchangeList("Query", 1);
            });

            $(":div[divname]='chelink'").each(function() {
                $(this).click(function() {
                    divClick(this);
                    LoadExchangeList("link", 1);
                });
            });
           
            LoadExchangeList("link", 1,20);
        });

        function aClick(object) {
            $(":a[aname]='atime'").each(function() {
                $(this).removeAttr("state");
                $(this).attr("state", "0");
                $(this).removeClass();
                $(this).addClass("zuotian");
            });
            $(object).addClass("jintian");
            $(object).attr("state", '1');
        }

        function divClick(object) {
            $(":div[divname]='chelink'").each(function() {
                $(this).removeAttr("state");
                $(this).attr("state", "0");
                $(this).removeClass();
                $(this).addClass("bar_un_comm");
            });
            $(object).removeClass();
            $(object).addClass("bar_on_comm");
            $(object).attr("state", '1');
        }

        function DivSixShow(ShowId, HideId, ShowTableId, HideTableId, ShowStyle, HideStyle) {
            $("#" + ShowId).show();
            $("#" + ShowTableId).show();
            $("#" + HideTableId).hide();
            $("#" + ShowId).attr("class", ShowStyle);
            $("#" + HideId).attr("class", HideStyle);
        }
    </script>

</asp:Content>
