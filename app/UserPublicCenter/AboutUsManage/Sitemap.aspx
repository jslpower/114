<%@ Page Title="网站地图" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="UserPublicCenter.AboutUsManage.Sitemap" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="AboutUsHeadControl.ascx" tagname="AboutUsHeadControl" tagprefix="uc1" %>

<%@ Register src="AboutUsLeftControl.ascx" tagname="AboutUsLeftControl" tagprefix="uc2" %>
<%@ Register Src="~/WebControl/DefaultRouteControl.ascx" TagName="DefaultRouteControl" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
	<style>
        .chengse14 {color:#FF6600; font-size:14px;}
        .chengse14 a{color:#FF6600;}
        .chengse16 a { font-size:16px; color:#990000; font-weight:bold}
	</style>
    <div id="header">
       
        <uc1:AboutUsHeadControl ID="AboutUsHeadControl1" runat="server" />
       
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="142" valign="top" style="background: #FFF7D7;">
                
                <uc2:AboutUsLeftControl ID="AboutUsLeftControl1" runat="server" />
                
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="818" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="background: #F6F6F6; padding: 5px;">
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/companytu.gif" width="811" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td width="10%" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/companyleft.gif) no-repeat left top;
                            height: 10px;">
                        </td>
                        <td width="90%" style="background: #F6F6F6; border: 1px solid #E5E5E5; border-bottom: 0px;
                            border-left: 0px;">
                        </td>
                    </tr>
                </table>                  
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background:#F6F6F6;">
        <tr>
          <td width="2%" height="25">&nbsp;</td>
          <td colspan="2" align="left">

		  <style>
		  .chengse16 a { font-size:16px; color:#990000; font-weight:bold}
		  </style>
		  <span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/beijing">北京旅游同业114网</a></strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td width="2%" align="left">&nbsp;</td>
          <td align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_3_19" target="_blank"> 北京地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_19" target="_blank">北京旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_19" target="_blank">北京旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_19" target="_blank">北京供求信息</a></td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>北京国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </td>
        </tr>

        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="30" align="left"><span class="chengse14"><strong>北京出境旅游线路</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>

          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/TourList_602_19"></a>
              <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>北京周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="25" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/guangzhou">广州旅游同业114网</a></strong></span></td>
        </tr>

        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_6_19" target="_blank">广州地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_48" target="_blank">广州旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_48" target="_blank">广州旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_48" target="_blank">广州供求信息</a></td>
        </tr>

        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>广州国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>

          <td height="25" align="left">
              <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>广州出境旅游线路</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>广州周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>
          <td height="25" colspan="3"></td>
        </tr>
        <tr>

          <td height="25">&nbsp;</td>
          <td colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/hangzhou">杭州旅游同业114网</a></strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_33_19" target="_blank">杭州地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_362" target="_blank">杭州旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_362" target="_blank">杭州旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_362" target="_blank">杭州供求信息</a></td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>杭州国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>杭州出境旅游专线</strong></span></td>

        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>杭州周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/TourList_487_362"></a><asp:Literal 
                  ID="Literal9" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="25" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/shanghai">上海旅游同业114网</a></strong></span></td>
        </tr>

        <tr>
          <td></td>
          <td align="left"></td>
          <td align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_25_292" target="_blank">上海地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_292" target="_blank">上海旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_292" target="_blank">上海旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_292" target="_blank">上海供求信息</a></td>
        </tr>

        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>上海国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>

          <td height="25" align="left">
              <asp:Literal ID="Literal10" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>上海出境旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal11" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>上海周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal12" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>

          <td height="15" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/nanjing">南京旅游同业114网</a></strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_16_192" target="_blank">南京地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_192" target="_blank">南京旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_192" target="_blank">南京旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_192" target="_blank">南京供求信息</a></td>
        </tr>
        <tr>
          <td height="35">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>南京国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal13" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>南京出境旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal14" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>南京周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal15" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>
          <td height="25" colspan="3"></td>
        </tr>
        <tr>

          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/jinan">济南旅游同业114网</a></strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_22_257" target="_blank">济南地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_257" target="_blank">济南旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_257" target="_blank">济南旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_257" target="_blank">济南供求信息</a></td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>济南国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal16" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>济南出境旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal17" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>济南周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal18" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>

          <td height="15" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/ningbo">宁波旅游同业114网</a></strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_33_367" target="_blank">宁波地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_367" target="_blank">宁波旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_367" target="_blank">宁波旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_367" target="_blank">宁波供求信息</a></td>
        </tr>
        <tr>
          <td></td>

          <td align="left"></td>
          <td align="left"><span class="chengse14"><strong>宁波国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal19" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>宁波出境旅游专线</strong></span></td>
        </tr>
        <tr>

          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal20" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>宁波周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal21" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="15" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/kunming">昆明旅游同业114网</a></strong></span></td>
        </tr>

        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_32_352" target="_blank">昆明地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_352" target="_blank">昆明旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_352" target="_blank">昆明旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_352" target="_blank">昆明供求信息</a></td>
        </tr>

        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>昆明国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>

          <td height="25" align="left">
              <asp:Literal ID="Literal22" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>昆明出境旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal23" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>昆明周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal24" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="15" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td height="25" colspan="2" align="left"><span class="chengse16"><strong><a href="<%=Domain.UserPublicCenter %>/shenyang">沈阳旅游同业114网</a></strong></span></td>
        </tr>

        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="<%=Domain.UserPublicCenter %>/dijie_18_225" target="_blank">沈阳地接社</a> <a href="<%=Domain.UserPublicCenter %>/bus_225" target="_blank">沈阳旅游车队</a> <a href="<%=Domain.UserPublicCenter %>/products_225" target="_blank">沈阳旅游用品</a> <a href="<%=Domain.UserPublicCenter %>/info_225" target="_blank">沈阳供求信息</a></td>
        </tr>

        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>沈阳国内旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>

          <td height="25" align="left">
              <asp:Literal ID="Literal25" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>沈阳出境旅游专线</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal26" runat="server"></asp:Literal>
                                    </td>

        </tr>
        <tr>
          <td height="35">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"><span class="chengse14"><strong>沈阳周边游</strong></span></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left">
              <asp:Literal ID="Literal27" runat="server"></asp:Literal>
                                    </td>
        </tr>
        <tr>
          <td height="10" colspan="3"></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>

          <td align="left">&nbsp;</td>
          <td height="25" align="left"><strong>更多链接&gt;&gt;</strong></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td height="25" align="left"><a href="http://club.tongye114.com/">旅游同业社区</a> <a href="http://corp.tongye114.com/">同业动态</a> <a href="http://v.tongye114.com/">同业网店</a> <a href="<%=Domain.UserPublicCenter %>/AirTickets/Login.aspx">团队机票</a> <a href="<%=Domain.UserPublicCenter %>/hotel_225">同业酒店</a> <a href="http://im.tongye114.com/">同业MQ</a> <a href="<%=Domain.UserPublicCenter %>/ToCutCity.aspx?Index=1">更多城市列表</a></td>

        </tr>
    </table>
    
    </td>
  </tr>
</table>

</asp:Content>
