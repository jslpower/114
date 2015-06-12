<%@ Page  Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master"
    AutoEventWireup="true" CodeBehind="Train.aspx.cs" Inherits="UserPublicCenter.visa.Train" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <meta  name="keywords" content="火车查询，火车票查询，火车时刻表，火车票余票查询，火车票网，火车票查询时刻表"/>
    <meta  name="description" content="同业114火车票提供2011年最新最全火车时刻表，可查询全国3956个车站，3142次列车时刻表。" />
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
                <li class="b2 b2Active" ><a title="火车查询" href="/visa/Train.aspx">火车查询</a></li>
                <li class="b3"><a title="电子地图" href="/visa/Map.aspx">电子地图</a></li>
                <li class="b4 "><a title="天气查询" href="/visa/Weather.aspx">天气查询</a></li>
            </ul>
        </div>
        <div class="form">
            <iframe width="100%" hspace="0" height="2250" src="http://bdoem.kuxun.cn/tongyong/chezhan.php?from=%E5%8C%97%E4%BA%AC&to=%E4%B8%8A%E6%B5%B7&if=aa&sbox=1&fromid=Kgocn-S1328241-T1183881&width=900"  scrolling="no" frameborder="0"></iframe>
        </div>
        <div class="hr-10">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
</asp:Content>
