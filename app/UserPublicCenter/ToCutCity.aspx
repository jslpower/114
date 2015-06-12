<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToCutCity.aspx.cs" Inherits="UserPublicCenter.ToCutCity"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<asp:Content ContentPlaceHolderID="Main" runat="server" ID="cph_Main">
<style type="text/css">
   h3 { margin:0 auto; padding:0} 
</style>  
  <div id="header">
        <div class="logo">
            <a href="<%=DefaultUrl %>">
                <img src="<%=UnionLogo %>" alt="同业114" border="0"  height="70" width="170" /></a></div>
        <div style="width: 300px; height: 30px; float: left; padding-top: 40px; color: #ff6600;
            font-size: 30px; text-align: left">
            <b>&nbsp;选择城市</b></div>
        <div class="tymq">
            <a href="http://im.tongye114.com" target="_blank" title="同业MQ免费下载">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/mqgif.gif" alt="同业MQ免费下载"
                    border="0" width="99px" height="76px"  /></a></div>
    </div>
    <div class="maintop15">
    </div>

    <div class="body">
        <table cellspacing="0" cellpadding="5" border="0" align="center" width="80%">
            <tbody>
                <tr>
                    <td align="left">
                        <strong>请选择城市</strong>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table cellspacing="0" cellpadding="5" border="0" width="100%" style="border: 1px solid rgb(255, 235, 191);
                            background: none repeat scroll 0% 0% rgb(254, 247, 205);">
                            <tbody>
                                <tr>
                                    <td align="right" width="10%">
                                        热门城市：
                                    </td>
                                    <td align="left" width="90%" >
                                        <h3>
                                          <a href="<%=SubStation.CityUrlRewrite(19) %>" style="color: rgb(255, 0, 0);">北京</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(48) %>" style="color: rgb(255, 0, 0);">广州</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(362) %>" style="color: rgb(255, 0, 0);">杭州</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(292) %>" style="color: rgb(255, 0, 0);">上海</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(192) %>" style="color: rgb(255, 0, 0);">南京</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(257) %>" style="color: rgb(255, 0, 0);">济南</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(367) %>" style="color: rgb(255, 0, 0);">宁波</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(352) %>" style="color: rgb(255, 0, 0);">昆明</a>&nbsp;&nbsp;<a href="<%=SubStation.CityUrlRewrite(225) %>" style="color: rgb(255, 0, 0);">沈阳</a>
                                            </h3>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                 <%=strAllCityList%>
            </tbody>
        </table>
    </div>
</asp:Content>
