<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserPublicCenter.HotelManage.Default"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/HotelControl/NewJoinHotelControl.ascx" TagName="NewJoinHotelControl"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/HotelControl/StarHotelControl.ascx" TagName="StarHotelControl"
    TagPrefix="uc3" %>
<%@ Register Src="../WebControl/HotelControl/PromoHotelControl.ascx" TagName="PromoHotelControl"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/HotelControl/ImgFristControl.ascx" TagName="ImgFristControl"
    TagPrefix="uc5" %>
<%@ Register Src="../WebControl/HotelControl/ImgSecondControl.ascx" TagName="ImgSecondControl"
    TagPrefix="uc6" %>
<%@ Register Src="../WebControl/HotelControl/ImgBannerControl.ascx" TagName="ImgBannerControl"
    TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .slider
        {
            position: absolute;
        }
        .slider li
        {
            list-style: none;
            display: inline;
        }
        .slider img, .slider2 img
        {
            width: 419px;
            height: 258px;
            display: block;
        }
        .slider2 li
        {
            float: left;
        }
        .slider2
        {
            width: 3200px;
        }
        .num
        {
            position: absolute;
            right: 5px;
            bottom: 5px;
        }
        .num li
        {
            float: left;
            color: #FF7300;
            text-align: center;
            line-height: 16px;
            width: 16px;
            height: 16px;
            font-family: Arial;
            font-size: 12px;
            cursor: pointer;
            overflow: hidden;
            margin: 3px 1px;
            border: 1px solid #FF7300;
            background-color: #fff;
        }
        .num li.on
        {
            color: #fff;
            line-height: 21px;
            width: 21px;
            height: 21px;
            font-size: 16px;
            margin: 0 1px;
            border: 0;
            background-color: #FF7300;
            font-weight: bold;
        }
        .huandengtu
        {
            background: url(<%=ImageServerPath %>/images/hotel/huand_03.gif) bottom no-repeat;
            height: 257px;
            width: 418px;
        }
        .huandengtu th
        {
            font-size: 14px;
            background: url(<%=ImageServerPath %>/images/hotel/huand_01.gif) repeat-x;
            color: #FFFFFF;
        }
        .huandengtu td
        {
            font-size: 12px;
            height: 30px;
            line-height: 23px;
            background: url(<%=ImageServerPath %>/images/hotel/huand_02.gif) bottom repeat-x;
        }
        .huandengtu a
        {
            font-weight: bold;
            color: #FF4A03;
            text-decoration: none;
        }
        .huandengtu a:hover
        {
            text-decoration: underline;
        }
        .huandengtu .pand4
        {
            padding-left: 4px;
        }
        .sidebar02_2_content a
        {
            color: #000000;
        }
        .sidebar02_2_content a:hover
        {
            color: red;
        }
        .sidebar02_2_2 a:hover
        {
            color: red;
        }
        .imgRArea a
        {
            color: #074387;
        }
        .sidebar_2 ul li
        {
            border: solid 0px red; *height:21px;}</style>
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="main">
        <uc7:ImgBannerControl ID="ImgBannerControl1" runat="server" />
        <!--content start-->
        <div class="content">
            <!--sidebar start-->
            <div class="sidebar">
                <!--sidebar_1-->
                <div class="sidebar_1" style="height: 260px; overflow: hidden;">
                    <h1 class="T" style="height: 33px;">
                        酒店搜索</h1>
                    <div class="search_box">
                        <div class="field" style="padding-top: 0px;">
                            <label style="*margin-left: -10px;">
                                <font class="C_red">*</font> 输入城市：</label><input name="txtCity" type="text" id="txtCity"
                                    size="7" autocomplete="off" style="width: 130px;" />
                        </div>
                        <div class="field field02" style="padding-left: 25px;">
                            <asp:DropDownList ID="ddl_City" runat="server" Width="200px">
                            </asp:DropDownList>
                        </div>
                        <div class="field" style=" height:22px;">
                            <label>
                                <font class="C_red">*</font>入住日期：</label><input name="txtInTime" type="text" id="txtInTime"
                                    size="12" onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){$('#txtLeaveTime').focus();}})"><img
                                        src="<%=ImageServerPath %>/images/hotel/time02.gif" class="timespic" onclick="javascript:$('#txtInTime').focus()">
                        </div>
                        <div class="field" style=" height:20px;">
                            <label>
                                <font class="C_red">*</font> 离店日期：</label><input name="txtLeaveTime" type="text"
                                    id="txtLeaveTime" size="12" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'});"><img
                                        src="<%=ImageServerPath %>/images/hotel/time02.gif" class="timespic" onclick="javascript:$('#txtLeaveTime').focus()"></div>
                        <div class="field">
                            <label>
                                价格范围：</label><input name="txtPriceBegin" type="text" id="txtPriceBegin" size="7">
                            -
                            <input name="txtPriceEnd" type="text" id="txtPriceEnd" size="7">
                        </div>
                        <div class="field">
                            <label>
                                酒店名称：</label><input name="txtHotelName" type="text" size="17" id="txtHotelName">
                        </div>
                        <div class="field">
                            <label>
                                地理位置：</label><asp:DropDownList ID="ddlGeography" runat="server" Width="165px">
                                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="submit">
                        <a href="javascript:void(0);" id="searchBtn" hidefocus="true">
                            <img src="<%=ImageServerPath %>/images/hotel/search_but.gif" /></a><a hidefocus="true"
                                href="/HotelManage/AdvanceSearch.aspx?CityId=<%=this.CityId %>">高级搜索>></a></div>
                </div>
                <div>
                    <uc5:ImgFristControl ID="ImgFristControl1" runat="server" />
                </div>
                <!-- sidebar_2-->
                <uc2:NewJoinHotelControl ID="NewJoinHotelControl1" runat="server" />
                <div style="margin-top: 10px;">
                    <uc6:ImgSecondControl ID="ImgSecondControl1" runat="server" />
                </div>
                <!-- sidebar_2-->
                <%=Utils.GetWebRequest("http://club.tongye114.com/forumblock/aggregationhotel.aspx")%>
            </div>
            <!--sidebar02 start-->
            <div class="sidebar02">
                <!--sidebar02_1-->
                <div class="sidebar02_1" style="height: 258px; overflow: hidden;">
                    <div class="flash_img" id="idTransformView" style="width: 416px; overflow: hidden;
                        position: relative; float: left; height: 258px;">
                        <ul class="slider slider2" id="idSlider" style="position: absolute; left: 0px; top: 0pt;">
                            <!--<li>
                                <div class="huandengtu">
                                    <table cellspacing="0" cellpadding="0" border="0" align="center" width="416">
                                        <tbody>
                                            <tr>
                                                <th height="29" align="center" width="111">
                                                    酒店名称
                                                </th>
                                                <th height="29" align="center" width="41">
                                                    城市
                                                </th>
                                                <th height="29" align="center" width="42">
                                                    星级
                                                </th>
                                                <th height="29" align="center" width="48">
                                                    佣金
                                                </th>
                                                <th height="29" align="center" width="110">
                                                    地标及周边景观
                                                </th>
                                                <th height="29" align="center" width="64">
                                                    推荐语
                                                </th>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO10445",CityId)%>"
                                                        target="_blank" title="欣燕都连锁酒店(北京永定门店)">欣燕都连锁酒店..</a>
                                                </td>
                                                <td align="center">
                                                    北京
                                                </td>
                                                <td align="center">
                                                    准2
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">15% </font></strong>
                                                </td>
                                                <td class="pand4">
                                                    <span title="天坛公园、自然博物馆等、木樨园">天坛公园..</span>
                                                                   </td>
                                                <td>
                                                   <span title="近永定门城楼脚下，毗邻北京天安门，所处属宣南文化中心地带。">近永定..</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO10511",CityId)%>"
                                                        target="_blank" title="上海七宝老街速8酒店">上海七宝老街速.. </a>
                                                </td>
                                                <td align="center">
                                                    上海
                                                </td>
                                                <td align="center">
                                                    准2
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">15%</font></strong>
                                                </td>
                                                <td class="pand4">
                                                     <span title="七宝老街购物、小吃一条街、七宝镇人民政府">七宝老街购..</span>
                                                </td>
                                                <td>
                                                   <span title="酒店拥有各类温馨舒适的茶文化主题客房和七宝茶城">酒店拥..</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO9724",CityId)%>"
                                                        target="_blank" title="昆山花桥希尔顿逸林酒店">昆山花桥希尔顿..</a>
                                                </td>
                                                <td align="center">
                                                    昆山
                                                </td>
                                                <td align="center">
                                                    准5
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">14% </font></strong>
                                                </td>
                                                <td class="pand4">
                                                   <span title="上海汽车博物馆，上海国际赛车场、锦溪古镇 ">上海汽车博..</span>
                                                </td>
                                                <td>
                                                   <span title="酒店毗邻上海国际汽车城，临近上海国际赛车场，便利的地理位置，位于上海市与景色迷人的江苏省之间。">酒店毗..</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO10353",CityId)%>"
                                                        target="_blank" title="北京首都机场希尔顿酒店 ">北京首都机场希..</a>
                                                </td>
                                                <td align="center">
                                                    北京
                                                </td>
                                                <td align="center">
                                                    5
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">14% </font></strong>
                                                </td>
                                                <td class="pand4">
                                                    <span title="机场3号航站楼">机场3号航站楼</span>
                                                </td>
                                                <td>
                                                     <span title="机场3号航站楼">机场3..</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO1662",CityId)%>"
                                                        target="_blank" title="格林豪泰深圳东门酒店 ">格林豪泰深圳东..</a>
                                                </td>
                                                <td align="center">
                                                    深圳
                                                </td>
                                                <td align="center">
                                                    3
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">15% </font></strong>
                                                </td>
                                                <td class="pand4">
                                                 <span title="罗湖区临近东门步行街">罗湖区临近..</span></td>
                                                <td>
                                                    <span title="距罗湖口岸10分钟车程">距罗湖..</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="pand4">
                                                    <a href="<%=EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl("SOHOTO9336",CityId)%>"
                                                        target="_blank" title="飘HOME连锁酒店（前门店）">飘HOME连锁..</a>
                                                </td>
                                                <td align="center">
                                                    北京
                                                </td>
                                                <td align="center">
                                                    准2
                                                </td>
                                                <td align="center">
                                                    <strong><font color="#ff0000">15%</font></strong>
                                                </td>
                                                <td class="pand4">
                                                    <span title="北京市前门珠市口西大街韩家胡同">北京市前门..</span>
                                                </td>
                                                <td>
                                                <span title="西面是纪晓岚故居和晋阳饭庄，东邻丰泽园饭庄，与毗邻的陕西巷同为北京著名的八大胡同。">西面是..</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </li>-->
                            <li><a>
                                <img src="<%=ImageServerPath %>/images/hotel/index_03.jpg" /></a></li>
                            <!--<li><a>
                                <img src="<%=ImageServerPath %>/images/hotel/index_02.jpg" /></a></li>-->
                        </ul>
                        <ul id="idNum" class="num">
                            <li>1</li>
                            <!--<li>2</li>-->
                        </ul>
                    </div>
                    <uc4:PromoHotelControl ID="PromoHotelControl1" runat="server" />
                    <div class="clear" style="height: 1px;">
                    </div>
                </div>
                <div class="sidebar02_2">
                    <!--sidebar02_2_1-->
                    <div class="sidebar02_2_1" name="sidebar02_2_1">
                        <ul class="add_menu">
                            <li style="text-align: center"><a class="default" id="a_PEK" href="Default.aspx?cityCode=PEK&CityId=<%=this.CityId %>"
                                hidefocus="true">北京</a></li>
                            <li style="text-align: center"><a id="a_SHA" href="Default.aspx?cityCode=SHA&CityId=<%=this.CityId %>"
                                hidefocus="true">上海</a></li>
                            <li style="text-align: center"><a id="a_CAN" href="Default.aspx?cityCode=CAN&CityId=<%=this.CityId %>"
                                hidefocus="true">广州</a></li>
                            <li style="text-align: center"><a id="a_SZX" href="Default.aspx?cityCode=SZX&CityId=<%=this.CityId %>"
                                hidefocus="true">深圳</a></li>
                            <li style="text-align: center"><a id="a_HGH" href="Default.aspx?cityCode=HGH&CityId=<%=this.CityId %>"
                                hidefocus="true">杭州</a></li>
                            <li style="text-align: center"><a id="a_SYX" href="Default.aspx?cityCode=SYX&CityId=<%=this.CityId %>"
                                hidefocus="true">三亚</a></li>
                            <li style="text-align: center"><a id="a_CTU" href="Default.aspx?cityCode=CTU&CityId=<%=this.CityId %>"
                                hidefocus="true">成都</a></li>
                            <li style="text-align: center"><a id="a_NKG" href="Default.aspx?cityCode=NKG&CityId=<%=this.CityId %>"
                                hidefocus="true">南京</a></li>
                            <li style="text-align: center"><a id="a_TAO" href="Default.aspx?cityCode=TAO&CityId=<%=this.CityId %>"
                                hidefocus="true">青岛</a></li>
                            <li style="text-align: center"><a id="a_XMN" href="Default.aspx?cityCode=XMN&CityId=<%=this.CityId %>"
                                hidefocus="true">厦门</a></li>
                            <li style="text-align: center"><a id="a_WUH" href="Default.aspx?cityCode=WUH&CityId=<%=this.CityId %>"
                                hidefocus="true">武汉</a></li>
                            <li style="text-align: center"><a id="a_SHE" href="Default.aspx?cityCode=SHE&CityId=<%=this.CityId %>"
                                hidefocus="true">沈阳</a></li>
                        </ul>
                        <div style="width: 682px; height: 295px;">
                            <div class="sidebar02_2_content">
                                <div id="sidebar_div_PEK" style="height: 295px; padding-left: 2px;">
                                    <div style='width: 100%; text-align: center;'>
                                        <img src='<%=EyouSoft.Common.Domain.ServerComponents %>/images/loadingnew.gif' border='0'
                                            align='absmiddle' />&nbsp;正在加载...&nbsp;</div>
                                </div>
                                <div id="sidebar_div_SHA" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_CAN" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_SZX" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_HGH" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_SYX" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_CTU" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_NKG" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_TAO" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_XMN" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_WUH" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                                <div id="sidebar_div_SHE" style="height: 295px; display: none; padding-left: 2px;">
                                </div>
                            </div>
                        </div>
                        <%-- 广告--%>
                        <div style="margin-top: 10px;">
                            <a href="/HotelManage/HotelAdv/index.html">
                                <img width="682px" height="81px" src="<%=ImageServerPath%>/images/hotel/hotelqinggou.jpg"
                                    border="0"><a></div>
                        <uc3:StarHotelControl ID="StarHotelControl1" runat="server" />
                    </div>
                </div>
            </div>
            <!--sidebar02 end-->
        </div>
        <div class="clear">
        </div>
        <!--content end-->
        <input type="hidden" runat="server" id="hideCityCode" />
        <input type="hidden" id="hideGeography" runat="server" />
        <input type="hidden" id="hideCity" runat="server" />
        <input type="hidden" id="hideJSCity" runat="server" />
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetHotelCity") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("HomeImages") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        $(function() {
            //文本框悬浮提示
            $("#txtCity").tipsy({ fade: true, content: '城市中文、拼音、三字码筛选', gravity: "s" });

            //查询事件
            $("#searchBtn").click(function() {
                var cityCode = $("#<%=ddl_City.ClientID %>").val();
                var inTime = $("#txtInTime").val();
                var leaveTime = $("#txtLeaveTime").val();
                var priceBegin = $("#txtPriceBegin").val();
                var priceEnd = $("#txtPriceEnd").val();
                var hotelName = $("#txtHotelName").val();
                var landMark = $("#<%=ddlGeography.ClientID %>").val();
                var landMarkTxt = $("#<%=ddlGeography.ClientID %>  option:selected").text();
                if (landMark == "") { landMarkTxt = ""; }
                //验证
                if (cityCode == "") {
                    alert("请输入或选择一个城市!");
                    return;
                }

                if (inTime == "") {
                    alert("请输入入住日期!");
                    return;
                }
                if (leaveTime == "") {
                    alert("请输入离店日期!");
                    return;
                }
                //参数
                var para = { cityCode: "", inTime: "", leaveTime: "", priceBegin: "", priceEnd: "", hotelName: "", landMark: "", CityId: "", sort: "", landMarkTxt: "" };
                //赋值
                para.cityCode = cityCode;
                para.inTime = inTime;
                para.leaveTime = leaveTime;
                para.priceBegin = priceBegin;
                para.priceEnd = priceEnd;
                para.hotelName = hotelName;
                para.landMark = landMark;
                para.landMarkTxt = landMarkTxt;
                para.CityId = "<%=this.CityId %>";
                if (priceBegin == "" && priceEnd == "" && hotelName == "" && landMark == "") {
                    para.sort = "3";
                }
                window.location.href = "/HotelManage/HotelSearchList.aspx?" + $.param(para);
                return false;
            });

            //城市下拉框选择事件
            $("#<%=ddl_City.ClientID %>").change(function() {
                var cityCode = $(this).val();
                $("#txtCity").val("");
                DefaultPage.AddItemToGeography(cityCode);
            });
            //加载城市数据
            DefaultPage.AddItemToCity("Load");

            //城市文本框按键事件
            $("#txtCity").keyup(function() {
                if (SearchTimeOut != null) {
                    clearTimeout(SearchTimeOut);
                }
                SearchTimeOut = setTimeout(DefaultPage.TxtKeyUp, 200);
            });
            //页面后退的时候

            setTimeout(function() {
                if ($("#txtCity").val() != "") {

                    if (DefaultPage.AddItemToCity($("#txtCity").val())) {
                        var code = $("#<%=ddl_City.ClientID %>").val();
                        //添加地理位置到下拉框
                        DefaultPage.AddItemToGeography(code);
                    }
                }
            }, 1000)

            //轮换广告初始化
            HomeBigImages.init(420, 1);

            //特推酒店城市点击获取酒店列表
            $(".add_menu a").click(function() {
                if ($(this).attr("class") == "default") {
                    return false;
                }
                var cityCode = $(this).attr("id").replace("a_", "");
                var isGet = DefaultPage.GetHotelHtmlByCode(cityCode);
                if (oldObj != null) {
                    $(oldObj).hide();
                }
                if (!isGet) {
                    DefaultPage.GetSpecialHotel(cityCode);
                    $("#sidebar_div_" + cityCode).fadeIn("slow");
                    //显示加载等待的图片
                    $("#sidebar_div_" + cityCode).html("<div style='width:100%; text-align:center;'><img src='<%=EyouSoft.Common.Domain.ServerComponents %>/images/loadingnew.gif' border='0' align='absmiddle'/>&nbsp;正在加载...&nbsp;</div>");
                } else {
                    $("#sidebar_div_" + cityCode).fadeIn("slow");
                }
                oldObj = $("#sidebar_div_" + cityCode);
                $(".add_menu a").removeClass("default");
                $(this).addClass("default");
                return false;
            });
            //加载北京特推酒店
            DefaultPage.GetSpecialHotel("PEK");
        });
        var SearchTimeOut = null;
        var SpecialHotel = new Array();
        var hotelModel = function() { this.cityCode = ""; this.isGet = false; };
        var oldObj = $("#sidebar_div_PEK");
        var DefaultPage = {
            GetHotelHtmlByCode: function(cityCode) {
                var isGet = false;
                if (SpecialHotel != null && SpecialHotel.length > 0) {
                    for (var i = 0; i < SpecialHotel.length; i++) {
                        var model = SpecialHotel[i];
                        if (model.cityCode == cityCode) {
                            isGet = model.isGet;
                        }
                    }
                }
                return isGet;
            },
            GetSpecialHotel: function(cityCode) {
                $.ajax({
                    type: "Get",
                    url: "/HotelManage/AjaxDefault.aspx?cityCode=" + cityCode + "&CityId=<%=this.CityId %>",
                    cache: false,
                    success: function(result) {
                        $("#sidebar_div_" + cityCode).html(result);
                        var model = new hotelModel();
                        model.cityCode = cityCode;
                        model.isGet = true;
                        SpecialHotel.push(model);
                    }
                });

            },
            //添加地理位置
            AddItemToGeography: function(cityCode) {
                $("#<%=ddlGeography.ClientID %>").empty();
                $("<option value=''>--请选择--</option>").appendTo($("#<%=ddlGeography.ClientID %>"));
                if (GeographyList.length > 0) {
                    for (var i = 0; i < GeographyList.length; i++) {
                        var id = GeographyList[i].ID;
                        var Code = GeographyList[i].C;
                        var por = GeographyList[i].P;
                        if (cityCode.toUpperCase() == Code.toUpperCase()) {
                            $("<option value='" + id + "'>" + por + "</option>").appendTo($("#<%=ddlGeography.ClientID %>"));
                        }
                    }
                }
            },
            //添加城市
            AddItemToCity: function(val) {
                var isThere = false;
                if (val == "Load") {
                    $("#<%=ddl_City.ClientID %>").empty();
                    $("<option value=''>--请选择--</option>").appendTo($("#<%=ddl_City.ClientID %>"));
                    if (CityList.length > 0) {
                        for (var i = 0; i < CityList.length; i++) {
                            var Ping = CityList[i].P;
                            var Code = CityList[i].C;
                            var cityName = CityList[i].CN;
                            $("<option value='" + Code + "'>" + Ping + cityName + Code + "</option>").appendTo($("#<%=ddl_City.ClientID %>"));
                        }
                    }
                } else if ($.trim(val) != "") {
                    if (CityList.length > 0) {
                        for (var i = 0; i < CityList.length; i++) {
                            var Ping = CityList[i].P;
                            var Code = CityList[i].C;
                            var cityName = CityList[i].CN;
                            var indexVal = (Ping + cityName + Code).toUpperCase().indexOf(val.toUpperCase())
                            if (indexVal == 0) {
                                $("#<%=ddl_City.ClientID %>").val(Code);
                                return true;
                            }
                            if (indexVal > 0) {
                                $("#<%=ddl_City.ClientID %>").val(Code);
                                isThere = true;
                            }
                        }
                    }
                }
                return isThere;
            },
            //按键事件
            TxtKeyUp: function() {
                var val = $("#txtCity").val();
                if ($.trim(val) != "") {
                    if (DefaultPage.AddItemToCity(val)) {
                        var code = $("#<%=ddl_City.ClientID %>").val();
                        //添加地理位置到下拉框
                        DefaultPage.AddItemToGeography(code);
                    }
                }
            }
        };
    </script>

    </a>
</asp:Content>
