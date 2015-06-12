<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecommendCompany.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.RecommendCompany" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div class="mainbox04-sidebar03">
    <div class="imgArea-title">
        <h2 style="font-size: 14px;">
            <span class="hyzx"></span>推荐企业</h2>
    </div>
    <div class="imgArea-cont03" style="margin: 0px;">
        <ul class="tjqy fixed" style="width: 324px; height: 170px; padding-bottom: 14px;">
            <asp:Repeater ID="rptrecommendC" runat="server">
                <ItemTemplate>
                    <li style="padding: 2px; height: 71px; width: 92px;"><a style="display: block;" href='<%#Eval("RedirectURL") %>'>
                        <img style="height: 70px; width: 92px;" src="<%# Utils.GetNewImgUrl((string)Eval("ImgPath"),3) %>"
                            alt="<%# Utils.GetText(Eval("Title").ToString(),11) %>" /></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="hr_10" style="font-size: 1px;">
        </div>
        <div class="zxjr" style="overflow: hidden; zoom: 1; height: 110px; padding-bottom: 0px;">
            <h2 style="font-size: 14px;">
                <span class="fddj">&nbsp;</span>最新加入</h2>
            <ul style="height: 80px; overflow: hidden; padding-bottom: 0px; padding-top: 2px;"
                class="newjoin">
                <li runat="server" id="emptydata" visible="false">暂无信息</li>
                <asp:Repeater ID="rptNewCompany" runat="server">
                    <ItemTemplate>
                        <li>·<%# Utils.GetText(Eval("CompanyName").ToString(), 18)%><em>[<%# Convert.ToDateTime(Eval("RegTime")).ToString("yyyy-MM-dd")%>]</em></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
