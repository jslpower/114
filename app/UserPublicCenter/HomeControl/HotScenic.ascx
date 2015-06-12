<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotScenic.ascx.cs" Inherits="UserPublicCenter.HomeControl.HotScenic" %>
<div class="remenjd">
    <h3>
        <span class="tupian"></span><span class="jqwenzi">热门景区</span><span class="more"><a
            href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/jingqu_<%=cityid %>">更多&gt;&gt;</a></span></h3>
    <div class="jiing">
        <asp:Repeater ID="rptHotScenic" runat="server">
            <ItemTemplate>
                <dl>
                    <dd>
                        <a href='jingquinfo_<%#Eval("ScenicId") %>'>
                            <img alt=" " height="80px" width="106px" src="<%#GetScenicImg(Eval("Img")) %>"></a></dd>
                    <dt><a href='jingquinfo_<%#Eval("ScenicId") %>'>
                        <%#EyouSoft.Common.Utils.GetText2(Convert.ToString(Eval("ScenicName")),8,false)%></a></dt>
                </dl>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
