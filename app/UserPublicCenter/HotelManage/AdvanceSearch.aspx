<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvanceSearch.aspx.cs"
    Inherits="UserPublicCenter.HotelManage.AdvanceSearch" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUserControl"
    TagPrefix="uc2" %>
<%@ Register src="../WebControl/HotelControl/ImgFristControl.ascx" tagname="ImgFristControl" tagprefix="uc3" %>
<%@ Register src="../WebControl/HotelControl/ImgSecondControl.ascx" tagname="ImgSecondControl" tagprefix="uc4" %>
<%@ Register src="../WebControl/HotelControl/ImgBannerControl.ascx" tagname="ImgBannerControl" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("tipsy") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <!--main start-->
    <div class="main">
        <uc5:ImgBannerControl ID="ImgBannerControl1" runat="server" />
        <!--content start-->
        
        <div class="content">
            <!--sidebar start-->
            <div class="sidebar sidebarSearch" style="margin-top: -10px">
                <!-- sidebar_2 start-->
                <uc2:CommonUserControl ID="CommonUserControl1" runat="server" />
                &nbsp;<div style="margin-top: 10px;">
                    <uc3:ImgFristControl ID="ImgFristControl1" runat="server" />
                    </div>
&nbsp;<div style="margin-top: 10px;">
                    <uc4:ImgSecondControl ID="ImgSecondControl1" runat="server" />
                    </div>
            </div>
            <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
                <div class="sidebar02_1">
                    <p class="xuanzhe">
                        <span>酒店高级搜索</span><img src="<%=ImageServerPath %>/images/hotel/liuchengcx.gif" /></p>
                    <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        <div class="gaoji_content">
                            <span class="chooseCityT">选择城市（*必填）</span>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="20%" height="30" align="right">
                                        <font class="C_red">*</font> 目的城市：
                                    </td>
                                    <td width="20%" align="center">
                                        <input name="txtCity" type="text" id="txtCity" size="20" autocomplete="off" style="color: Gray" /><span
                                            id="cityMsg"></span>
                                    </td>
                                    <td width="60%" align="left">
                                        <asp:DropDownList ID="ddl_City" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="right" valign="top">
                                        常用城市：
                                    </td>
                                    <td colspan="2" valign="top">
                                        <table width="65%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="20%" height="28" align="left" valign="top">
                                                    <input name="radio" value="PEK" type="radio" />
                                                    北京
                                                </td>
                                                <td width="20%" align="left" valign="top">
                                                    <input type="radio" name="radio" value="SHA" />
                                                    上海
                                                </td>
                                                <td width="20%" align="left" valign="top">
                                                    <input type="radio" name="radio" value="CAN" />
                                                    广州
                                                </td>
                                                <td width="20%" align="left" valign="top">
                                                    <input type="radio" name="radio" value="SZX" />
                                                    深圳
                                                </td>
                                                <td width="20%" align="left" valign="top">
                                                    <input type="radio" name="radio" value="HGH" />
                                                    杭州
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="28">
                                                    <input type="radio" name="radio" value="NKG" />
                                                    南京
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="CTU" />
                                                    成都
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="WUH" />
                                                    武汉
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="TAO" />
                                                    青岛
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="DLC" />
                                                    大连
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="28">
                                                    <input type="radio" name="radio" value="CKG" />
                                                    重庆
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="TSN" />
                                                    天津
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="SZV" />
                                                    苏州
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="NGB" />
                                                    宁波
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="SIA" />
                                                    西安
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="28">
                                                    <input type="radio" name="radio" value="HRB" />哈尔滨
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="SYX" />三亚
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="KMG" />昆明
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="SHE" />沈阳
                                                </td>
                                                <td>
                                                    <input type="radio" name="radio" value="HKG" />香港
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="right">
                                        行政区：
                                    </td>
                                    <td align="center">
                                        <input name="txtRegion" type="text" id="txtRegion" size="20" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRegion" runat="server">
                                        </asp:DropDownList>
                                        （可以输入也以在列表中选择）
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30" align="right">
                                        地理位置：
                                    </td>
                                    <td align="center">
                                        <input name="txtGeography" type="text" id="txtGeography" size="20" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGeography" runat="server">
                                        </asp:DropDownList>
                                        （可以输入也以在列表中选择）
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--gaoji_content end-->
                        <div class="gaoji_content">
                            <span class="chooseCityT">入住和离店日期</span>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="15%" height="35" align="right">
                                        <font class="C_red">*</font> 入店日期：
                                    </td>
                                    <td width="20%" align="left">
                                        <input name="txtInTime" type="text" id="txtInTime" size="20" onfocus="WdatePicker({minDate:'%y-%M-#{%d}',onpicked:function(){document.getElementById('txtLeaveTime').focus();}});" />
                                        <img src="<%=ImageServerPath %>/images/hotel/time02.gif" style="vertical-align: middle;"
                                            onclick="$('#txtInTime').focus()">
                                    </td>
                                    <td width="10%" align="right">
                                        <font class="C_red">*</font> 离店日期：
                                    </td>
                                    <td width="45%" align="left">
                                        <input name="txtLeaveTime" type="text" id="txtLeaveTime" size="20" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'});" />
                                        <img src="<%=ImageServerPath %>/images/hotel/time02.gif" style="vertical-align: middle;"
                                            onclick="$('#txtLeaveTime').focus()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--gaoji_content end-->
                        <div class="gaoji_content">
                            <span class="chooseCityT">更多搜索条件</span>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="30%" align="center">
                                        酒店中英文名称：
                                    </td>
                                    <td width="26%" align="left">
                                        价格范围：
                                    </td>
                                    <td width="44%" align="left">
                                        酒店星级：
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <input name="txtHotelName" type="text" id="txtHotelName" value="中英文/拼音首字母" />
                                    </td>
                                    <td>
                                        <input name="txtPriceBegin" type="text" id="txtPriceBegin" size="8" />
                                        -
                                        <input name="txtPriceEnd" type="text" id="txtPriceEnd" size="8" />
                                        元
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlHotelStar" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="45" align="center">
                                        查询方式：
                                        <asp:DropDownList ID="ddlSearchWay" runat="server">
                                            <asp:ListItem Value="T" Text="前台现付"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="代收代付"></asp:ListItem>
                                            <asp:ListItem Value="V" Text="预付"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="40" align="center">
                                        &nbsp;
                                    </td>
                                    <td valign="top">
                                        <a href="javascript:void(0);" id="btnImgSearch">
                                            <img src="<%=ImageServerPath %>/images/hotel/search_but.gif" width="104" height="25" /></a>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--gaoji_content end-->
                    </div>
                    <!--sidebar02SearchC end-->
                </div>
            </div>
            <!--sidebar02 end-->
        </div>
        <!--content end-->
        <div class="clear">
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetHotelCity") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("tipsy") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        $(function() {
            $("#txtCity").tipsy({ fade: true, content: '城市中文、拼音、三字码筛选', gravity: "s" });
            $("#btnImgSearch").click(function() {
                var cityCode = $("#<%=ddl_City.ClientID %>").val();
                var district = $("#<%=ddlRegion.ClientID %>").val();
                var districtTxt = $("#<%=ddlRegion.ClientID %>  option:selected").text();
                if (district == "") { districtTxt = ""; }
                var landMark = $("#<%=ddlGeography.ClientID %>").val();
                var landMarkTxt = $("#<%=ddlGeography.ClientID %>  option:selected").text();
                if (landMark == "") { landMarkTxt = ""; }
                var inTime = $("#txtInTime").val();
                var leaveTime = $("#txtLeaveTime").val();
                var hotelName = $("#txtHotelName").val();
                var priceBegin = $("#txtPriceBegin").val();
                var priceEnd = $("#txtPriceEnd").val();
                var hotelLevel = $("#<%=ddlHotelStar.ClientID %>").val();
                var searchWay = $("#<%=ddlSearchWay.ClientID %>").val();
                if ($.trim(hotelName) == "中英文/拼音首字母") {
                    hotelName = "";
                }
                //验证
                var val = $("#txtCity").val();
                if (cityCode == "") {
                    alert("请输入或选择一个城市!");
                    return;
                }

                if (inTime == "") {
                    alert("请选择入店日期!");
                    return;
                }
                if (leaveTime == "") {
                    alert("请选择离店日期");
                    return;
                }

                var para = { cityCode: "", district: "", geography: "", inTime: "", leaveTime: "", hotelName: "", priceBegin: "", priceEnd: "", hotelLevel: "", searchWay: "", districtTxt: "", landMarkTxt: "" };
                para.cityCode = cityCode;
                para.district = district;
                para.landMark = landMark;
                para.inTime = inTime;
                para.leaveTime = leaveTime;
                para.hotelName = hotelName;
                para.priceBegin = priceBegin;
                para.priceEnd = priceEnd;
                para.hotelLevel = hotelLevel;
                para.searchWay = searchWay;
                para.districtTxt = districtTxt;
                para.landMarkTxt = landMarkTxt;

                window.location.href = "/HotelManage/HotelSearchList.aspx?" + $.param(para);

                return false;
            });
            $(".gaoji_content input[type='radio']").click(function() {
                AdvancePage.AddItemToRegion($(this).val());
                AdvancePage.AddItemToGeography($(this).val());
                AdvancePage.RadioClick($(this).val());
                $("#txtCity").val("");
            });

            //城市下拉框change事件
            $("#<%=ddl_City.ClientID %>").change(function() {
                var code = $(this).val();
                $("#txtCity").val("");
                //添加行政区到下拉框
                AdvancePage.AddItemToRegion(code);
                //添加地理位置到下拉框 
                AdvancePage.AddItemToGeography(code);
            });
            //加载城市数据
            AdvancePage.AddItemToCity("Load");

            $("#txtHotelName").focus(function() {
                if ($.trim($("#txtHotelName").val()) == "中英文/拼音首字母") {
                    $("#txtHotelName").val("");
                }
            });
            $("#txtHotelName").blur(function() {
                if ($.trim($("#txtHotelName").val()) == "") {
                    $("#txtHotelName").val("中英文/拼音首字母");
                }
            });

            $("#txtCity").keyup(function() {
                if (SearchTimeOut != null) {
                    clearTimeout(SearchTimeOut);
                }
                SearchTimeOut = setTimeout(AdvancePage.TxtKeyUp, 200);
            });

            //处理页面后退
            setTimeout(function() {
                if ($("#txtCity").val() != "") {
                    if (AdvancePage.AddItemToCity($("#txtCity").val())) {
                        var code = $("#<%=ddl_City.ClientID %>").val();
                        //添加行政区到下拉框
                        AdvancePage.AddItemToRegion(code);
                        //添加地理位置到下拉框 
                        AdvancePage.AddItemToGeography(code);
                    }
                }
            }, 1000)
        });
        var SearchTimeOut = null;
        var AdvancePage = {
            //添加行政区域
            AddItemToRegion: function(cityCode) {
                $("#<%=ddlRegion.ClientID %>").empty();
                $("<option value=''>--请选择--</option>").appendTo($("#<%=ddlRegion.ClientID %>"));
                if (RegionList.length > 0) {
                    for (var i = 0; i < RegionList.length; i++) {
                        var id = RegionList[i].ID;
                        var Code = RegionList[i].C;
                        var areaName = RegionList[i].AN;
                        if (cityCode.toUpperCase() == Code.toUpperCase()) {
                            $("<option value='" + id + "'>" + areaName + "</option>").appendTo($("#<%=ddlRegion.ClientID %>"));
                        }
                    }
                }
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
                    if (CityList.length > 0) {
                        $("#<%=ddl_City.ClientID %>").empty();
                        $("<option value=''>--请选择--</option>").appendTo($("#<%=ddl_City.ClientID %>"));
                        for (var i = 0; i < CityList.length; i++) {
                            var Ping = CityList[i].P;
                            var Code = CityList[i].C;
                            var cityName = CityList[i].CN;
                            $("<option value='" + Code + "'>" + Ping + cityName + Code + "</option>").appendTo($("#<%=ddl_City.ClientID %>"));
                        }
                    }
                } else if ($.trim(val) != "") {
                    for (var i = 0; i < CityList.length; i++) {
                        var Ping = CityList[i].P;
                        var Code = CityList[i].C;
                        var cityName = CityList[i].CN;
                        var indexVal = (Ping + cityName + Code).toUpperCase().indexOf(val.toUpperCase());

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
                return isThere;
            }, RadioClick: function(cityCode) {
                $("#<%=ddl_City.ClientID %>").val(cityCode);
            },
            TxtKeyUp: function() {
                var val = $("#txtCity").val();
                if ($.trim(val) != "") {
                    if (AdvancePage.AddItemToCity(val)) {
                        var code = $("#<%=ddl_City.ClientID %>").val();
                        //添加行政区到下拉框
                        AdvancePage.AddItemToRegion(code);
                        //添加地理位置到下拉框 
                        AdvancePage.AddItemToGeography(code);
                    }
                }
            }
        };
    </script>

    <input type="hidden" id="hideRegion" runat="server" />
    <input type="hidden" id="hideGeography" runat="server" />
    <input type="hidden" id="hideCity" runat="server" />
    </div>
</asp:Content>
