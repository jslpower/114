<%@ Page Title="" Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SeniorOnlineShop.GeneralShop.SightShop.Default" %>

<%@ Register Src="~/GeneralShop/GeneralShopControl/SecondMenu.ascx" TagName="SecondMenu"
    TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/master/GeneralShop.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SecondMenu runat="server" ID="SecondMenu1" CurrMenuIndex="1" />
    <div class="main_center">
        <div class="about">
            <%--<div id="ifocus">
                <div id="ifocus_pic">
                    <div id="ifocus_piclist" style="left: 0; top: 0;">
                        <ul>
                            <li><a href="http://www.lanrentuku.com/" target="_blank">
                                <img src="images/01.jpg" alt="懒人图库" width="272" height="181" /></a></li>
                            <li><a href="http://www.lanrentuku.com/" target="_blank">
                                <img src="images/02.jpg" alt="懒人图库" width="272" height="181" /></a></li>
                            <li><a href="http://www.lanrentuku.com/" target="_blank">
                                <img src="images/03.jpg" alt="懒人图库" width="272" height="181" /></a></li>
                        </ul>
                    </div>
                </div>
                <div id="ifocus_btn">
                    <ul>
                        <li class="current">
                            <img src="images/btn_01.jpg" alt="" /></li>
                        <li class="normal">
                            <img src="images/btn_02.jpg" alt="" /></li>
                        <li class="normal">
                            <img src="images/btn_03.jpg" alt="" /></li>
                    </ul>
                </div>
            </div>--%>
            <asp:Literal runat="server" ID="ltrCompanyInfo"></asp:Literal>
        </div>
        <div class="x_contact">
            <div class="x_contactT">
                <span class="xiaodian1">联系方式</span></div>
            <div class="x_contactL">
                <div class="x_contactLT">
                    <span class="xiaoxiaoT">联系我们</span><br />
                    Email：
                    <asp:Literal runat="server" ID="ltrEmail"></asp:Literal>
                    <br />
                    QQ：<asp:Literal runat="server" ID="ltrQQ"></asp:Literal>
                    &nbsp;&nbsp;&nbsp;MSN：<asp:Literal runat="server" ID="ltrMSN"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                    MQ:
                    <asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                </div>
                <span class="xiaoxiaoT">业务优势</span>
                <asp:Literal runat="server" ID="ltrYWYS"></asp:Literal>
            </div>
            <div class="x_contactR">
                <%--<img src="images/putong_23.jpg" width="327" height="270" />--%></div>
        </div>
        <div class="huiyuanzq">
            注册登录查看更多同业联系方式 （以下为登录会员看到的内容）
        </div>
        <asp:PlaceHolder runat="server" ID="plnLoginUser">
            <div class="tylxfs">
                <div class="tylxfs_t">
                    同业联系方式</div>
                <div class="tylxfs_m">
                    <div class="lianxi_wenzi">
                        <asp:Literal runat="server" ID="ltrTYLXFS"></asp:Literal>
                    </div>
                    <div class="lianxi_biao">
                        <ul class="lianxi_di1">
                            <li>真实姓名</li>
                            <li>MQ</li>
                            <li>电话</li>
                            <li>手机</li>
                            <li>传真 </li>
                            <li>QQ</li>
                            <li style="width: 150px;">MSN</li>
                            <li style="width: 150px;">Email</li>
                        </ul>
                        <asp:Repeater runat="server" ID="rptCompanyUser">
                            <ItemTemplate>
                                <ul class="lianxi_di2">
                                    <%# GetCompanyUserInfo(Eval("ContactInfo"))%>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="tylxfs">
                <div class="tylxfs_t">
                    银行账户</div>
                <div class="tylxfs_m">
                    <div class="gszh">
                        <span class="gszh_t">·公司银行账户</span> <strong>公司全称：</strong><asp:Literal runat="server"
                            ID="ltrCompanyBank"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;<strong>开户行：</strong><asp:Literal runat="server" ID="ltrBankName"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>帐号：</strong><asp:Literal runat="server" ID="ltrAccount"></asp:Literal>
                    </div>
                    <div class="gszh">
                        <span class="gszh_t">·个人银行账户</span>
                        <asp:Repeater runat="server" ID="rptPersonalAccount">
                            <ItemTemplate>
                                <strong>户 名：</strong><%# Eval("BankAccountName")%>
                                &nbsp;&nbsp;&nbsp;&nbsp;<strong>开户行：</strong><%# Eval("BankName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                <strong>帐号：</strong><%# Eval("AccountNumber")%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="tylxfs">
                <div class="tylxfs_t">
                    证书</div>
                <div class="tylxfs_m">
                    <ul>
                        <li><a href="<%= LicenceImg %>" target="_blank">
                            <img src="<%= LicenceImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="<%= LicenceImg %>" target="_blank">营业执照</a></span></li>
                        <li><a href="<%= BusinessCertImg %>" target="_blank">
                            <img src="<%= BusinessCertImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="<%= BusinessCertImg %>" target="_blank">经营许可证</a></span></li>
                        <li><a href="<%= TaxRegImg %>" target="_blank">
                            <img src="<%= TaxRegImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="<%= TaxRegImg %>" target="_blank">税务登记证</a></span></li>
                        <%--<li><a href="#">
                            <img src="images/putong_34.jpg" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="#">公司电子章</a></span></li>
                        <li><a href="#">
                            <img src="images/putong_34.jpg" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="#">企业LOGO</a></span></li>--%>
                    </ul>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
