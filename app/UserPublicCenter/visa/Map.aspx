<%@ Page Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true"
    CodeBehind="Map.aspx.cs" Inherits="UserPublicCenter.visa.Map" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="电子地图，电子地图查询，电子地图导航，电子地图软件" />
    <meta name="description" content="同业114提供浏览电子地图、搜索出行地点、查询公交驾车线路，是您的出行指南、生活好帮手。" />
    <link href='<%=CssManage.GetCssFilePath("quiry2011")%>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
<%if (Request.Browser.Browser == "IE" && Request.Browser.MajorVersion == 6)
   { %>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Png") %>"></script>
       <script type="text/javascript">
           if ($.browser.msie && $.browser.version == "6.0") {

               DD_belatedPNG.fix('div,ul,li,a,p,img,s,span');
           }
    </script>
      <%} %>
    <div class="c" id="main">
        <div class="hr-10">
        </div>
        <div class="topBTN">
            <ul>
                <li class="b1"><a title="旅游签证" href="/visa/visaList.aspx">旅游签证</a></li>
                <li class="b2"><a title="火车查询" href="/visa/Train.aspx">火车查询</a></li>
                <li class="b3 b2Active"><a title="电子地图" href="/visa/Map.aspx">电子地图</a></li>
                <li class="b4 "><a title="天气查询" href="/visa/Weather.aspx">天气查询</a></li>
            </ul>
        </div>
        <div class="form">
            <iframe height="666" frameborder="0" width="937" scrolling="no" src="http://searchbox.mapbar.com/channel/index.jsp?mapZoom=8&amp;pageType=0&amp;CID=w11447&amp;city=&amp;maxMap="
                framespacing="0" marginheight="0" marginwidth="0" hspace="0" vspace="0" border="0"
                id="mapbarframe"></iframe>
        </div>
        <div class="hr-10">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
