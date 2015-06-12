<%@ Page Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true" CodeBehind="Hotel.aspx.cs" Inherits="SeniorOnlineShop.shop.Hotel" Title="无标题页" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register src="../GeneralShop/GeneralShopControl/SecondMenu.ascx" tagname="SecondMenu" tagprefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("head2011") %>" rel="Stylesheet" type="text/css" />
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("xinmian") %>" rel="Stylesheet" type="text/css" />
<div class="xinmain">
    <uc1:SecondMenu ID="SecondMenu1" runat="server" />
      <div class="main_center2">
          <div class="gscp3">
            <ul>
            
          <cc2:CustomRepeater ID="rptHotelList" runat="server">
          <ItemTemplate>
             <li class="">
               <div class="ptjd_img"><img width="86" height="74" alt=" " src="<%=Domain.ServerComponents%>/images/hotel/pic01.gif"></div>
               <div class="ptjd_right" style="text-align:left;">
                 <p class="ptjd_title"><a href="<%=Domain.UserPublicCenter%>/HotelManage/HotelDetail.aspx">新疆东升鸿福大饭店</a> <span> 准5星</span></p>
                 <p class="ptjd_mian">您选择新疆昌吉东升鸿福大饭店作为您的家外之家  </p>
               </div>
             </li>
          </ItemTemplate>
          </cc2:CustomRepeater>
             <li class="">
               <div class="ptjd_img"><img width="86" height="74" alt=" " src="<%=Domain.ServerComponents%>/images/hotel/pic01.gif"></div>
               <div class="ptjd_right" style="text-align:left;">
                 <p class="ptjd_title"><a href="<%=Domain.UserPublicCenter%>/HotelManage/HotelDetail.aspx">新疆东升鸿福大饭店</a> <span> 准5星</span></p>
                 <p class="ptjd_mian">您选择新疆昌吉东升鸿福大饭店作为您的家外之家  </p>
               </div>
             </li>
              <li class="">
               <div class="ptjd_img"><img width="86" height="74" alt=" " src="<%=Domain.ServerComponents%>/images/hotel/pic01.gif"></div>
               <div class="ptjd_right" style="text-align:left;">
                 <p class="ptjd_title"><a href="<%=Domain.UserPublicCenter%>/HotelManage/HotelDetail.aspx">新疆东升鸿福大饭店</a> <span> 准5星</span></p>
                 <p class="ptjd_mian">您选择新疆昌吉东升鸿福大饭店作为您的家外之家  </p>
               </div>
             </li>
            </ul>
          </div>
          <div class="fangye">
          
          <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />
          </div>
        </div>
        <div style="clear:both; height:10px;"></div>
</div>
<%--浮动咨询开始--%>
    <div id="divZX" style="display:none;z-index:99999;">
        <table height="140" cellspacing="0" cellpadding="0" border="0" background="<%= Domain.ServerComponents %>/images/seniorshop/zixunbg.gif"
            width="400">
            <tbody>
                <tr>
                    <td height="5" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" valign="top" colspan="2">
                        &nbsp;&nbsp;您好，<asp:Label runat="server" ID="lbCompanyName"></asp:Label>竭诚为您服务
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="2">
                        <asp:Label runat="server" Text="欢迎您,有什么可以帮助您的吗？" ID="lbGuestInfo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a href="/OlServer/Default.aspx?cid=<%= Master.CompanyId %>" target="blank">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/jieshou.gif"></a>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onclick="CloseLeft();">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>
<script type="text/javascript">
    $(function(){
         $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
    });
</script>
</asp:Content>
