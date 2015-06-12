<%@ Page Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true"
    CodeBehind="rival.aspx.cs" EnableEventValidation="false" Inherits="UserPublicCenter.FindRival.rival" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc5" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute"
    TagPrefix="uc5" %>
<%@ Register Src="/WebControl/LeftList.ascx" TagName="LeftList" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="keywords" content="旅行社  组团社资料大全 组团社 专线 地接社 旅游同行 旅游同行网 旅游同行网站 " />
    <meta name="description" content="找同行频道，同业114旅游同行，中国最大的旅游同业在线查询，热点查询，同业之星访问，发布最新线路，加盟企业，方便，快捷" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("indexmain")%>' rel="Stylesheet" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("body")%>' rel="Stylesheet" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("gongqiu2011")%>' rel="Stylesheet" />
    <link type="text/css" href='<%=CssManage.GetCssFilePath("news2011")%>' rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
    <div id="mainContanct">
        <div id="news-list-ad">
        </div>
        <div style="overflow: hidden" class="body">
            <div id="supply-bar">
                <!--列表 start-->
                <uc1:LeftList ID="LeftList" runat="server" />
                <!--列表 end-->
            </div>
            <div style="width: 73%; border: 0; padding: 0; margin: 0;" class="addVg" id="supply-right">
                <a title="" target="_blank" href="http://im.tongye114.com/">
                    <img src="<%=ImageServerPath%>/Images/new2011/search_partner_03.gif" /></a>
                <div class="hr-10">
                </div>
                <div id="searchMain">
                    <form id="Form1" runat="server">
                    <div id="formMain">
                        <label>
                            省份</label><asp:DropDownList ID="ddl_ProvinceList" runat="server">
                            </asp:DropDownList>
                        <label>
                            城市</label><asp:DropDownList ID="ddl_CityList" runat="server">
                            </asp:DropDownList>
                        <label>
                            县区</label><asp:DropDownList ID="ddl_CountyList" runat="server">
                            </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddl_CompanyTypeList">
                        </asp:DropDownList>
                        <label>
                            关键字</label><input type="text" onkeypress="return isEnter(event);" style="width: 80px;"
                                class="text" name="CompanyName" id="companyname">
                        <input type="button" id="btnSearch" class="sub" value="搜素">
                    </div>
                    </form>
                    <div class="hr-10">
                    </div>
                    <div class="site">
                        <asp:Repeater ID="province" runat="server">
                            <ItemTemplate>
                                <a title='<%#Eval("ProvinceName")%>' onclick='GetCity()' href='<%#EyouSoft.Common.URLREWRITE.tonghang.GetTonghangProviceUrl(Eval("ProvinceId").ToString())%>'>
                                    <%#Eval("ProvinceName")%></a>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="hr-10">
                    </div>
                    <div class="hy-bt">
                        <span class="left"></span><span class="biaoti">
                            <%=provinceName%>旅游行业企业黄页</span> <span class="shul">共 <b class="fontred">
                                <%=recordCount%></b> 家企业</span>
                        <div class="clear">
                        </div>
                    </div>
                    <div class="hr-10">
                    </div>
                    <div class="hylilt">
                        <ul>
                            <cc1:CustomRepeater ID="rivalList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="hylilt-img">
                                            <img width="100" height="100" src='<%# GetCompanyLogoSrc(Eval("AttachInfo")) %>'></div>
                                        <div class="hylilt-xinxi">
                                            <h2>
                                                <%#Eval("ServiceId").ToString() == "2" ? "<a style='color:#FF6000' target='_blank' title='" + Eval("CompanyName") + "' href='" + EyouSoft.Common.Utils.GetEShopUrl(Eval("ID").ToString()) + "'>" + Eval("CompanyName").ToString() + "</a>" : "<a style='color:#FF6000' target='_blank' title='" + Eval("CompanyName") + "' href='" + EyouSoft.Common.Utils.GetShopUrl(Eval("ID").ToString()) + "'>" + Eval("CompanyName").ToString() + "</a>"%>
                                                <%# EyouSoft.Common.Utils.GetCompanyLevImg((EyouSoft.Model.CompanyStructure.CompanyLev)Eval("CompanyLev"))  %></h2>
                                            <ul>
                                                <li class="left">公司类型：<font class="fonthui"><%#GetCompanyType(Eval("CompanyRole"))%></font></li>
                                                <li class="right">公司区域：<font class="fonthui"><%#Eval("ProvinceName")%></font></li>
                                                <li class="left">联 系 人：<font class="fonthui"><%#Eval("ContactInfo") == null ? "" : Eval("ContactInfo.ContactName")%></font></li>
                                                <li class="right">公司地址：<font class="fonthui"><a style="color: #666666; cursor: pointer;
                                                    text-decoration: none" title="<%#Eval("CompanyAddress") %>" id="aCompanyAddress"><%#Utils.GetText2(Convert.ToString(Eval("CompanyAddress")), 17, false)%></a></font></li>
                                                <li class="left">客服电话：<font class="fonthui"><a style="color: #666666; cursor: pointer;
                                                    text-decoration: none" title="<%# Eval("ContactInfo") == null ? "" : Eval("ContactInfo.Tel")==null?"":Eval("ContactInfo.Tel").ToString()%>"><%#Eval("ContactInfo") == null ? "" : EyouSoft.Common.Utils.GetText2(Eval("ContactInfo.Tel") == null ? "" : Eval("ContactInfo.Tel").ToString(), 10, false)%></a></font></li>
                                                <li class="right">MQ洽谈：<%#Judge((EyouSoft.Model.CompanyStructure.ContactPersonInfo)Eval("ContactInfo"), "MQ")%></li>
                                                <li class="left">QQ洽谈：<%#Judge((EyouSoft.Model.CompanyStructure.ContactPersonInfo)Eval("ContactInfo"), "QQ")%></li>
                                                <div class="clear">
                                                </div>
                                            </ul>
                                        </div>
                                        <div class="hylilt-jian">
                                            <%#GetIsState(Convert.ToString(Eval("ID")))%></div>
                                        <div class="clear">
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </cc1:CustomRepeater>
                        </ul>
                    </div>
                    <!--分页 开始-->
                    <div class="digg">
                        <cc5:ExporPageInfoSelect ID="pageInfor" runat="server" PageStyleType="NewButton" />
                    </div>
                    <!--分页 结束-->
                    <div class="hr-10">
                    </div>
                </div>
                <div class="hr-10">
                </div>
            </div>
            <div class="hr-10">
            </div>
            <!--新闻列表结束-->
            <!--bottom nav start-->
            <div class="hr-10">
            </div>
            <uc5:HotRoute ID="hotRoute" runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">

    <script type="text/javascript">

        $(function() {
            var ProvinceID = "<%=provinceID%>";
            var cityID = "<%=cityID%>";
            var countryID = "<%=areaID%>";
            var companyname = "<%=companyName %>";
            var companytype = "<%=companyType %>";
            $("#companyname").val(companyname);
            $("#<%=ddl_CompanyTypeList.ClientID %>").val(companytype);
            $("#<%=ddl_ProvinceList.ClientID %>").val(ProvinceID);
            if (ProvinceID > 0) {
                $(".site > a").eq(ProvinceID - 1).addClass("active");
                GetCity();
            }
            $("#<%=ddl_CityList.ClientID %>").val(cityID);
            if (cityID > 0) {
                GetCountry();
            }
            $("#<%=ddl_CountyList.ClientID %>").val(countryID);
        });
        function GetCity() {
            ChangeList('<%=ddl_CityList.ClientID %>', '<%=ddl_CountyList.ClientID %>', $("#<%=ddl_ProvinceList.ClientID %>").val());
        }
        function GetCountry() {
            ChangeCountyList('<%=ddl_CountyList.ClientID %>', $("#<%=ddl_CityList.ClientID %>").val(), '<%=ddl_ProvinceList.ClientID %>');
        }
        //判断是否按回车
        function isEnter(event) {
            event = event ? event : window.event;
            if (event.keyCode == 13) {
                $("#btnSearch").click();
                return false;
            }
        }
        $("#btnSearch").click(function() {
            ProvinceID = $("#<%=ddl_ProvinceList.ClientID %>").val();
            cityID = $("#<%=ddl_CityList.ClientID %>").val();
            countryID = $("#<%=ddl_CountyList.ClientID %>").val();
            companyname = $("#companyname").val();
            companytype = $("#<%=ddl_CompanyTypeList.ClientID %>").val();
            //~/rival_(\d+)_(\d+)_(\d+)_(\d+)
            //window.location.href="/rival_"+cityID+"_"+ProvinceID+"_"+countryID+"_"+escape(companyname);
            window.location.href = "/FindRival/rival.aspx?cId=" + cityID + "&provinceID=" + ProvinceID + "&areaid=" + countryID + "&companyname=" + escape(companyname) + "&companytype=" + companytype;
        })
    </script>

</asp:Content>
