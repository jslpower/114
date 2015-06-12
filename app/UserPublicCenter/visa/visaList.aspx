<%@ Page Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true"
    CodeBehind="visaList.aspx.cs" Inherits="UserPublicCenter.visa.visaList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="旅游签证，各国旅游签证，出国旅游签证，旅游出国，商务签证、留学签证、工作签证 " />
    <meta name="description" content="提供美国，加拿大，德国，法国，英国，日本，韩国，泰国，新加坡等出国旅游签证，签证类型包括：旅游签证、商务签证、留学签证、工作签证及探亲访友签证，数量涵盖全球五大洲，近200个国家的各种类型签证产品。并提供签证产品搜索、比价等功能，及各国家使馆信息和签证表下载服务。" />
    <link href='<%=CssManage.GetCssFilePath("quiry2011")%>' rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("517autocomplete") %>" rel="stylesheet" type="text/css" />
     
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
                <li class="b1 b2Active"><a title="旅游签证" href="/visa/visaList.aspx">旅游签证</a></li>
                <li class="b2"><a title="火车查询" href="/visa/Train.aspx">火车查询</a></li>
                <li class="b3"><a title="电子地图" href="/visa/Map.aspx">电子地图</a></li>
                <li class="b4 "><a title="天气查询" href="/visa/Weather.aspx">天气查询</a></li>
            </ul>
        </div>
        <div class="topSearch">
            <form action="/visa/visaDetail.aspx" method="get" id="form1">
            <label>
                国家</label>
            <input type="text" style="display: none" />
            <input id="stateID" type="hidden" value="0" name="stateID" />
            <input id="country" type="text" class="input" maxlength="30" />
            <input id="search" type="button" class="btn" value="搜索" />
            </form>
        </div>
        <div class="text">
            <h3>
                同业114旅游搜索引擎签证频道</h3>
            <p>
                同业114旅游签证频道为您提供全球签证便捷服务。数量涵盖全球五大洲，近200个国家的各种类型签证查询。并提供各国家驻华使馆信息、签证表下载、所需签证资料（个人身份证明 资产证明 工作证明 学生及儿童 老人）服务。
</p>
        </div>
        <div class="text">
            <span>热门标签：</span> 
            <%=HotNameList%>
        </div>
        <table class="list">
            <tbody>
                <tr>
                    <td style="vertical-align: middle" class="left" />
                    欧洲 </td>
                    <td class="right">
                        <asp:Repeater ID="Europe" runat="server">
                            <ItemTemplate>
                                <a href='<%#EyouSoft.Common.URLREWRITE.visa.GetVisaUrl(Eval("ID").ToString())%>'>
                                    <%#Eval("CountryCn")%>签证</a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="list">
            <tbody>
                <tr>
                    <td style="vertical-align: middle" class="left">
                        亚洲
                    </td>
                    <td class="right">
                        <asp:Repeater ID="Asia" runat="server">
                            <ItemTemplate>
                                <a href='<%#EyouSoft.Common.URLREWRITE.visa.GetVisaUrl(Eval("ID").ToString())%>'>
                                    <%#Eval("CountryCn")%>签证</a>
                            </ItemTemplate>
                        </asp:Repeater>
                </tr>
            </tbody>
        </table>
        <table class="list">
            <tbody>
                <tr>
                    <td style="vertical-align: middle" class="left">
                        美洲
                    </td>
                    <td class="right">
                        <asp:Repeater ID="America" runat="server">
                            <ItemTemplate>
                                <a href='<%#EyouSoft.Common.URLREWRITE.visa.GetVisaUrl(Eval("ID").ToString())%>'>
                                    <%#Eval("CountryCn")%>签证</a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </tbody>
        </table>
        <table class="list">
            <tbody>
                <tr>
                    <td style="vertical-align: middle" class="left">
                        非洲
                    </td>
                    <td class="right">
                        <asp:Repeater ID="Africa" runat="server">
                            <ItemTemplate>
                                <a href='<%#EyouSoft.Common.URLREWRITE.visa.GetVisaUrl(Eval("ID").ToString())%>'>
                                    <%#Eval("CountryCn")%>签证</a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr-10">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Newautocomplete") %>"></script>

    <script type="text/javascript">
        function search() {
            var id = $("#stateID").val();
            var name = $("#country").val();
            if (id == 0 || name == "") {
                alert("请选择国家");
                return false;
            }
            else {
                return true;
            }
        }

        $(function() {

            $("#country").focus();

            $("#country").change(function() {
                $("#stateID").val("0");
            });

            $("#search").click(function() {
                if (search()) {
                    $("#form1")[0].submit();
                }
            });


            $("#country").autocomplete("/visa/visaList.aspx", {
                width: 260,
                selectFirst: true,
                blur: true
            }).result(function(e, data) {
                $("#stateID").val(data[1]);
                if ($(document.activeElement).attr("id") == "country") {
                    $("#form1")[0].submit();
                }
            });

        });
    

    </script>

</asp:Content>
