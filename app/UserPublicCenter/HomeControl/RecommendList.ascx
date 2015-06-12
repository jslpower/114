<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecommendList.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.RecommendList" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<div class="spmm2">
    <div class="lineimg">
        <cc1:CustomRepeater ID="repImagesList" runat="server">
        <ItemTemplate>
             <a href="<%# EyouSoft.Common.Utils.SiteAdvUrl(Eval("RedirectURL").ToString()) %>" target="<%# Eval("RedirectURL").ToString().Trim() ==EyouSoft.Common.Utils.EmptyLinkCode ? "_self":"_blank" %>" >
            <img src="<%=EyouSoft.Common.Domain.FileSystem %><%#Eval("ImgPath") %>" width="120" height="80" border="0" title="<%# Eval("Title") %>" /><span> <%# EyouSoft.Common.Utils.GetText(Eval("Title").ToString(),8) %></span></a>
        </ItemTemplate>
        </cc1:CustomRepeater>
       
    </div>
    <div style="height: 10px; clear: both">
    </div>
    <cc1:CustomRepeater ID="repCommendList" runat="server">
        <ItemTemplate>
            <div class="clear">
                <div class="lineleft">
                    <a href="/PlaneInfo/NewsDetailInfo.aspx?NewsID=<%#Eval("AdvId") %>&CityId=<%=CityId %>" target="_blank" class="linetitle">
                        <%# EyouSoft.Common.Utils.GetText2(Eval("Title").ToString(), 33,false)%></a> <%# GetCompanyUrl( Eval("CompanyId")==null?"": Eval("CompanyId").ToString(),Eval("CompanyName")==null?"": Eval("CompanyName").ToString())%>
                        </div>
                     
            </div>
        </ItemTemplate>
    </cc1:CustomRepeater>
</div>
<div class="spb">
</div>
