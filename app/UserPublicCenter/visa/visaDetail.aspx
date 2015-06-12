<%@ Page Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true"
    CodeBehind="visaDetail.aspx.cs" Inherits="UserPublicCenter.visa.visaDetail" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="旅游签证，各国旅游签证，出国旅游签证，旅游出国，商务签证、留学签证、工作签证" />
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
                <li class="b4"><a title="天气查询" href="/visa/Weather.aspx">天气查询</a></li>
            </ul>
        </div>
        <div class="topSearch">
            <form action="/visa/visaDetail.aspx" method="get" id="form1">
            <label>
                国家</label>
            <input id="stateID" type="hidden" value="0" name="stateID" />
            <input type="text" style="display: none" />
            <input id="country" type="text" class="input" maxlength="30" autocomplete="off" />
            <input id="search" type="button" class="btn" value="搜索" />
            </form>
        </div>
        <div class="form con">
            <div class="ftop">
                <h3>
                    <%=Mcoun==null? "" : Mcoun.CountryCn+"("+Mcoun.CountryEn+")"%></h3>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div class="fcen">
                    <div class="fcen_l">
                        <img src='<%=ImageServerPath+Mcoun.FlagPath%>'><br />
                        <h3>
                            <%=Mcoun == null ? "" : Mcoun.CountryCn + "商务签证"%></h3>
                    </div>
                    <div class="fcen_r">
                        <h3 class="ttop">
                            使馆信息</h3>
                        <div class="fcen_r_l">
                            <%=Mvisa.EmbassyInfo%>
                        </div>
                        <div class="fcen_r_r">
                            <h3>
                                站点信息!</h3>
                            <ul>
                                <%if (Mvisa.Links != null)
                                  {
                                      int len = Mvisa.Links.Count;%>
                                <%for (int k = 0; k < len; k++)
                                  { %>
                                <li><a title="<%=Mvisa.Links[k].LinkTile%>" href='<%=Utils.SiteAdvUrl(Mvisa.Links[k].LinkUrl)%>'>
                                    <%=(k+1).ToString()%>、<%=Mvisa.Links[k].LinkTile%></a></li>
                                <%} %>
                                <% }%>
                            </ul>
                        </div>
                    </div>
                    <div class="hr-10">
                    </div>
                    <div class="wrp">
                        <h3 class="ttop ttop1">
                            所需签证资料
                        </h3>
                        <div id="tags">
                            <ul>
                                <li class="selectTag"><a href="javascript:void(0)" onclick="selectTag('tagContent0',this)">
                                    个人身份证明</a> </li>
                                <li class=""><a href="javascript:void(0)" onclick="selectTag('tagContent1',this)">资产证明</a>
                                </li>
                                <li class=""><a href="javascript:void(0)" onclick="selectTag('tagContent2',this)">工作证明</a>
                                </li>
                                <li class=""><a href="javascript:void(0)" onclick="selectTag('tagContent3',this)">学生及儿童</a>
                                </li>
                                <li class=""><a href="javascript:void(0)" onclick="selectTag('tagContent4',this)">老人</a>
                                </li>
                                <li class=""><a href="javascript:void(0)" onclick="selectTag('tagContent5',this)">其他</a>
                                </li>
                            </ul>
                        </div>
                        <div id="tagContent">
                            <div id="tagContent0" class="tagContent selectTag">
                                <%=typeValue[0]%>
                            </div>
                            <div style="display: none;" id="tagContent1" class="tagContent">
                                <%=typeValue[1]%>
                            </div>
                            <div style="display: none;" id="tagContent2" class="tagContent">
                                <%=typeValue[2]%>
                            </div>
                            <div style="display: none;" id="tagContent3" class="tagContent">
                                <%=typeValue[3]%>
                            </div>
                            <div style="display: none;" id="tagContent4" class="tagContent">
                                <%=typeValue[4]%>
                            </div>
                            <div style="display: none;" id="tagContent5" class="tagContent">
                                <%=typeValue[5]%>
                            </div>
                        </div>

                        <script type="text/javascript">
                            function selectTag(showContent, selfObj) {
                                var tag = document.getElementById("tags").getElementsByTagName("li");
                                var taglength = tag.length;
                                for (i = 0; i < taglength; i++) {
                                    tag[i].className = "";
                                }
                                selfObj.parentNode.className = "selectTag";
                                for (i = 0; j = document.getElementById("tagContent" + i); i++) {
                                    j.style.display = "none";
                                }
                                document.getElementById(showContent).style.display = "block";
                            }
</script>

                    </div>
                    <div class="wrp wrp1">
                        <h3 class="ttop ttop2">
                            特别提示</h3>
                        <div>
                            <%=Mvisa.HintInfo%>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <div align="center" style="font-size: 24;">
                    没有需要的信息
                </div>
            </asp:Panel>
            <div class="fbottom">
            </div>
        </div>
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
