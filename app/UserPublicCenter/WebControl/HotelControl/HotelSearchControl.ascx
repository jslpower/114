<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotelSearchControl.ascx.cs"
    Inherits="UserPublicCenter.HotelManage.HotelSearchControl" %>
<%@ Import Namespace="EyouSoft.Common" %>

<script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

<link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
<div class="sidebar_1">
    <h1 class="T">
        酒店搜索</h1>
    <!--search_box start-->
    <div class="search_box">
        <div class="field">
            <label style="width: 210px;">
                <font class="C_red">*</font> 选择城市：<input name="txtCity" type="text" id="txtCity"
                    size="7" autocomplete="off" style="width: 110px; margin-left: 10px;" />
            </label>
        </div>
        <div style="width: 175px; height: 25px; margin-left: 15px; margin-top: 3px;">
            <asp:DropDownList ID="ddl_City" runat="server" Width="200px">
                <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="field">
            <label>
                <font class="C_red">*</font> 入住日期：</label><input name="txtInTime" type="text" id="txtInTime"
                    size="12" runat="server" onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){SearchControl.TxtGetFocus();}})">
        </div>
        <div class="field">
            <label>
                <font class="C_red">*</font> 离店日期：</label><input name="txtLeaveTime" type="text"
                    id="txtLeaveTime" size="12" runat="server" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'});">
        </div>
        <div class="field">
            <label>
                价格范围：</label><input name="txtPriceBegin" type="text" id="txtPriceBegin" runat="server"
                    style="width: 45px;">
            -
            <input name="txtPriceEnd" type="text" id="txtPriceEnd" size="7" runat="server" style="width: 45px;">
        </div>
        <div class="field">
            <label>
                酒店等级：</label><asp:DropDownList ID="ddl_HotelLevel" runat="server">
                </asp:DropDownList>
        </div>
        <div class="field">
            <label>
                酒店名称：</label><input name="txtHotelName" type="text" size="12" id="txtHotelName" runat="server">
        </div>
        <div class="field">
            <label>
                地理位置：</label>
            <div style="width: 175px; height: 25px; margin-left: 26px;">
                <asp:DropDownList ID="ddlGeography" runat="server" Width="165px">
                    <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="submit">
            <a href="javascript:void(0);" id="imgSearchBtn">
                <img src="<%=ImageServerPath %>/images/hotel/search_but.gif" /></a><a href="/HotelManage/AdvanceSearch.aspx?CityId=<%=CityId %>">高级搜索>></a></div>
        <div class="field">
            <h2 class="T_sub">
                缩小范围搜索</h2>
            <ul>
                <li>
                    <asp:CheckBox ID="cbx_Instant" runat="server" /><font class="C_Grb">即时确认</font></li>
                <li>
                    <asp:CheckBox ID="cbx_Service" runat="server" /><font class="C_Grb">接机服务</font></li>
                <li>
                    <asp:CheckBox ID="cbx_Internet" runat="server" /><font class="C_Grb">上网设施</font></li>
                <li>装修时间：
                    <asp:DropDownList ID="ddl_Decoration" runat="server">
                        <asp:ListItem Value="">-请选择-</asp:ListItem>
                        <asp:ListItem Value="1998">1998</asp:ListItem>
                        <asp:ListItem Value="1999">1999</asp:ListItem>
                        <asp:ListItem Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>特殊房型：
                    <asp:DropDownList ID="ddl_Special" runat="server">
                        <asp:ListItem Value="" Text="-请选择-"></asp:ListItem>
                        <asp:ListItem Value="特殊" Text="特殊"></asp:ListItem>
                        <asp:ListItem Value="蜜月">蜜月</asp:ListItem>
                        <asp:ListItem Value="海景">海景</asp:ListItem>
                        <asp:ListItem Value="山景">山景</asp:ListItem>
                        <asp:ListItem Value="江景">江景</asp:ListItem>
                        <asp:ListItem Value="湖景">湖景</asp:ListItem>
                        <asp:ListItem Value="家庭">家庭</asp:ListItem>
                        <asp:ListItem Value="别墅">别墅</asp:ListItem>
                        <asp:ListItem Value="两室">两室</asp:ListItem>
                        <asp:ListItem Value="三室">三室</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>床&nbsp;&nbsp;&nbsp;&nbsp;型：
                    <asp:DropDownList ID="ddl_Bed" runat="server">
                        <asp:ListItem Value="">-请选择-</asp:ListItem>
                        <asp:ListItem Value="双床">双床</asp:ListItem>
                        <asp:ListItem Value="大床">大床</asp:ListItem>
                        <asp:ListItem Value="三床">三床</asp:ListItem>
                        <asp:ListItem Value="大/双">大/双</asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
            <div class="search_bottom">
            </div>
        </div>
    </div>
    <!--search_box end-->
    <input type="hidden" id="hideCityCode" runat="server" value="" />
    <input type="hidden" id="hideGeography" runat="server" value="" />
</div>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetHotelCity") %>"></script>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>

<script type="text/javascript">
    $(function() {
        $("#txtCity").tipsy({ fade: true, content: '城市中文、拼音、三字码筛选', gravity: "s" });
        //查询按钮事件
        $("#imgSearchBtn").click(function() {

            //获得页面值
            var cityCode = $("#<%=ddl_City.ClientID %>").val();
            var inTime = $.trim($("#<%=txtInTime.ClientID %>").val());
            var leaveTime = $.trim($("#<%=txtLeaveTime.ClientID %>").val());
            var priceBegin = $("#<%=txtPriceBegin.ClientID %>").val();
            var priceEnd = $("#<%=txtPriceEnd.ClientID %>").val();
            var hotelLevel = $("#<%=ddl_HotelLevel.ClientID %>").val();
            var hotelName = $("#<%=txtHotelName.ClientID %>").val();
            var instant = 0;
            var landMark = $("#<%=ddlGeography.ClientID %>").val();
            var landMarkTxt = $("#<%=ddlGeography.ClientID %> option:selected").text();
            if (landMark == "") { landMarkTxt = ""; }
            if ($("#<%=cbx_Instant.ClientID %>").attr("checked")) {
                instant = 1;
            }
            var service = 0;
            if ($("#<%=cbx_Service.ClientID %>").attr("checked")) {
                service = 1;
            }
            var internet = 0;
            if ($("#<%=cbx_Internet.ClientID %>").attr("checked")) {
                internet = 1;
            }

            var decoration = $("#<%=ddl_Decoration.ClientID %>").val();
            var special = $("#<%=ddl_Special.ClientID %>").val();
            var bed = $("#<%=ddl_Bed.ClientID %>").val();

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

            //设置参数
            var para = { cityCode: "", inTime: "", leaveTime: "", priceBegin: "", priceEnd: "", hotelLevel: "", keyWord: "", instant: "", service: "", internet: "", decoration: "", special: "", bed: "", CityId: "", landMark: "", landMarkTxt: "", sort: "" };

            para.cityCode = cityCode;
            para.inTime = inTime;
            para.leaveTime = leaveTime;
            para.priceBegin = priceBegin;
            para.priceEnd = priceEnd;
            para.hotelLevel = hotelLevel;
            para.hotelName = hotelName;
            para.instant = instant;
            para.service = service;
            para.internet = internet;
            para.decoration = decoration;
            para.special = special;
            para.bed = bed;
            para.CityId = "<%=CityId %>";
            para.landMark = landMark;
            para.landMarkTxt = landMarkTxt;
            if (priceBegin == "" && priceEnd == "" && hotelLevel == "0" && hotelName == "") {
                para.sort = "3";
            }
            window.location.href = "/HotelManage/HotelSearchList.aspx?" + $.param(para);
            return false;

        });

        //下拉框事件
        $("#<%=ddl_City.ClientID%>").change(function() {
            SearchControl.AddItemToGeography($(this).val());
        });

        //城市数据添加到下拉框
        SearchControl.AddItemToCity("Load");

        //设置城市选中
        if ($("#<%=hideCityCode.ClientID%>").val() != "") {
            setTimeout(function() {
                $("#<%=ddl_City.ClientID %>").val($("#<%=hideCityCode.ClientID%>").val());
            }, 1000)

            SearchControl.AddItemToGeography($("#<%=hideCityCode.ClientID%>").val());
        }
        //设置地理位置选中
        if ($("#<%=hideGeography.ClientID%>").val() != "") {
            setTimeout(function() {
                $("#<%=ddlGeography.ClientID %>").val($("#<%=hideGeography.ClientID%>").val());
            }, 1000);

        }
        //文本框输入事件
        $("#txtCity").keyup(function() {
            if (SearchTimeOut != null) {
                clearTimeout(SearchTimeOut);
            }
            SearchTimeOut = setTimeout(SearchControl.TxtKeyUp, 200);
        });


    });

    //时间变量
    var SearchTimeOut = null;
    var SearchControl = {
        //添加城市的方法
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
                //城市筛选
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
        //添加地理位置的方法
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
        TxtKeyUp: function() {
            var val = $("#txtCity").val();
            if ($.trim(val) != "") {
                if (SearchControl.AddItemToCity(val)) {
                    var code = $("#<%=ddl_City.ClientID %>").val();
                    //添加地理位置到下拉框
                    SearchControl.AddItemToGeography(code);
                }
            }
        },
        TxtGetFocus: function() {
            $("#<%=txtLeaveTime.ClientID%>").focus();
        }
    }
</script>

