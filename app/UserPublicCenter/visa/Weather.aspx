<%@ Page Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true"
    CodeBehind="Weather.aspx.cs" Inherits="UserPublicCenter.visa.Weather" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="天气查询，天气预报，天气查询网，一周天气查询" />
    <meta name="description" content="天气预报，便捷查询今日天气，明日天气，一周天气预报，天气预报还提供本市各区县的晨练气息指数、着装气息指数、郊游气息指数、感冒气息指数，空气污染气息指数，及时发布气象预警信号、各类气象资讯。" />
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
                <li class="b3"><a title="电子地图" href="/visa/Map.aspx">电子地图</a></li>
                <li class="b4 b2Active"><a title="天气查询" href="/visa/Weather.aspx">天气查询</a></li>
            </ul>
        </div>
        <div class="form">
            <iframe height="1000" frameborder="0" width="937" scrolling="no" src="http://tq121.weather.com.cn/citybook/weatherframe/sant/index.php"
                framespacing="0" marginheight="0" marginwidth="0" hspace="0" vspace="0" border="0"
                id="mapbarframe"></iframe>
        </div>
        <div class="hr-10">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
