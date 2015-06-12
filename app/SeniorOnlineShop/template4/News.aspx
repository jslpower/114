<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="SeniorOnlineShop.template4.News"
    MasterPageFile="~/master/T4.Master" %>

<%@ MasterType VirtualPath="~/master/T4.Master" %>
<asp:Content ContentPlaceHolderID="MainPlaceHolder" ID="c1" runat="server">
    <div class="linetj">
        <div class="linetjtk">
            <div class="lineinth">
                当前位置：<a href="/Default_<%=CompanyId %>">首页</a> -><strong><a href="/template4/NewsList.aspx?cid=<%=CompanyId %>">旅游动态</a></strong>
            </div>
            <div class="linetjxx" style="height: 1000px;">
                <div class="plantitle">                    
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                </div>
                <div class="guidedate">
                    发布日期：<asp:Literal ID="ltrIssuetime" runat="server"></asp:Literal></div>
                <div class="guidecontend">
                    <p>
                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal></p>
                </div>
            </div>
        </div>
    </div>       
</asp:Content>
