<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicArea.aspx.cs" 
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicArea" MasterPageFile="~/master/ScenicSpotsT1.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="/scenicspots/usercontrol/ChildMenu.ascx" TagName="ChildMenu" TagPrefix="uc1" %>
<%@ Register Src="~/scenicspots/usercontrol/ScenicPic.ascx" TagName="ScenicImg" TagPrefix="uc2" %>

<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
        <!--sidebar02 start-->
        <div class="sidebar02 sidebar02Scenic">
        	<!-- 所在位置 -->
        	<uc1:ChildMenu ID="ChildMenu1" runat="server" />
            <div class="content">
               <div class="llbiao">
                 <asp:Repeater ID="rpt_Scenic" runat="server">
                    <ItemTemplate>
                        <dl>
                           <dd><a href='/ScenicTickets_<%# Eval("Id") %>_<%= CompanyId  %>' title='<%# Eval("ScenicName") %>'><img src='<%# Utils.GetNewImgUrl(GetScenicImg(Eval("Img")),2) %>' width="92" height="84" alt="<%# Eval("ScenicName") %>" /></a></dd>
                           <dt class="dttitle"><a href='/ScenicTickets_<%# Eval("Id") %>_<%= CompanyId  %>'><%# Eval("ScenicName") %></a></dt>
                           <dt class="dtmain"><%# Utils.GetText(Utils.InputText(Eval("Description")),140,true)%></dt>
                         </dl>
                    </ItemTemplate>
                 </asp:Repeater>
               </div>
                <div class="fangye">
                    <!--分页-->
                    <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1"  LinkType="3" CurrencyPageCssClass="RedFnt" PageStyleType="NewButton" runat="server" />
                </div>

              <DIV class="clearboth"></DIV>
              <ul class="contentpic">
                <uc2:ScenicImg ID="ScenicImg1" runat="server" />
                <DIV class="clearboth"></DIV>
            </ul>
             <DIV class="clearboth"></DIV>
            </div>
        </div>
        <DIV class="clearboth"></DIV>
</asp:Content>
