<%@ Page Language="C#" MasterPageFile="~/master/T4.Master" AutoEventWireup="true"
    CodeBehind="GuideBookInfo.aspx.cs" Inherits="SeniorOnlineShop.template4.GuideBookInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server" ID="Content1">
    <!--tuijian line-->
    <div class="linetj">
        <div class="linetjtk">
            <div class="lineinth">
                当前位置：<a href="/default_<%= cMaster.CompanyId %>">首页</a> -> <a href="/GuideBookList_<%= cMaster.CompanyId %>">出游指南</a>-> <strong runat="server" id="sTypeName"></strong>
            </div>
            <div class="linetjxx" style="height: 1000px;">
                <div class="plantitle">
                    <label runat="server" id="lbTitle"></label>
                </div>
                <div class="guidedate">
                    发布日期：<asp:Label runat="server" ID="lbAddTime"></asp:Label></div>
                <div class="guidecontend">
                    <p runat="server" id="pContent"></p>
                </div>
            </div>
        </div>
    </div>
    <!--tuijian line end-->
    </div>
    <!--right end-->
</asp:Content>
