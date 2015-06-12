<%@ Page Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true" CodeBehind="Industry.aspx.cs" Inherits="SeniorOnlineShop.shop.Industry" Title="无标题页" %>

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
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet" type="text/css" />
<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("boxy") %>"></script>
<div class="xinmain">
    <uc1:SecondMenu ID="SecondMenu1" runat="server" />
      <div class="main_center2">
          <div class="gscp2">
            <ul>
              <li class="gs_title" style="text-align:left;">
              <span class="gs_xuhao">序号</span>
              <span class="gs_biaoti">标题</span>
              <span class="gs_riqi">发布日期</span>
              </li>
              
          <cc2:CustomRepeater ID="rptNewsList" runat="server">
          <ItemTemplate>
             <li onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
              <span class="gs_xuhao2"><span class="fd7b19"><%#Container.ItemIndex + 1 %></span></span>
              <span class="gs_biaoti2"><a href="<%=Domain.UserBackCenter %>/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>"><%# Eval("Title")%>&gt;&gt;</a></span>   
              <span class="gs_riqi2"><%#Eval("IssueTime")%></span>
              </li>
          </ItemTemplate>
          </cc2:CustomRepeater>
             
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
 function mouseovertr(o) {
            o.style.backgroundColor = "#F1EDF4";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
 $(function() {

  $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
        $(".gscp2 a").click(function() {
            if ("<%=IsLogin %>" == "False") {
                Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                return false;
            }
        });
    });
    </script>
</asp:Content>
