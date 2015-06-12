<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelSearch.aspx.cs" Inherits="IMFrame.Hotel.HotelSearch" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>酒店查询</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style>
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
            overflow: hidden;
        }
        img
        {
            border: thin none;
        }
        *
        {
            margin: auto 0;
            padding: 0;
        }
        .headleft
        {
            float: left;
        }
        .headright
        {
            float: right;
        }
        input
        {
            font-size: 12px;
            height: 16px;
            color: #FFFFFF;
        }
        a.he
        {
            text-decoration: none;
            color: #6B2E03;
            font-weight: bold;
        }
        a.he:hover
        {
            text-decoration: underline;
            color: #ff6600;
        }
        .imgb
        {
            border: 1px solid #DDD;
            padding: 2px;
        }
        #sp_cont
        {
            width: 210px;
            position: relative;
        }
        #spmenu
        {
            height: 21px;
            width: 210px;
            overflow: hidden;
            padding: 0px;
            margin: 0px;
            list-style: none;
        }
        #spmenu li
        {
            width: 99px;
            float: left;
            margin-left: 4px;
            text-align: center;
            line-height: 21px;
            height: 21px;
            background: url(<%= ImageServerUrl %>/IM/images/hotel/navan.gif) no-repeat;
            color: #000;
            font-weight: normal;
        }
        #spmenu li.spmenuOn
        {
            background: url(<%= ImageServerUrl %>/IM/images/hotel/navan-on.gif) no-repeat;
            cursor: pointer;
            height: 33px;
            font-weight: bold;
            color: #6B2E03;
        }
        .spmenuCon
        {
            clear: both;
        }
        .clear
        {
            clear: both;
        }
        .nrbj
        {
            background: url(<%= ImageServerUrl %>/IM/images/hotel/nrbg.gif) repeat-x top;
        }
        .pand
        {
            padding: 3px 0px 2px 14px;
        }
        .pand2
        {
            padding: 3px 0px 2px 4px;
        }
        .pand3
        {
            padding: 5px 0px;
        }
        a.jp
        {
            color: #083971;
            text-decoration: none;
        }
        a.jp:visited
        {
            color: #083971;
            text-decoration: none;
        }
        a.jp:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        .airform2
        {
            float: left;
            border: 1px solid #4592BF;
            width: 110px;
            background-position: 100% -95px;
            margin: 0;
            padding: 0;
            height: 18px;
            background: #FFFFFF;
        }
        .f11
        {
            font-size: 11px;
        }
    </style>

    <script src="../DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow: hidden" scroll="no">
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%" style="background: url(<%= ImageServerUrl %>/IM/images/hotel/topbg.gif) repeat-x;">
            <tbody>
                <tr>
                    <td height="35" background="<%= ImageServerUrl %>/IM/images/hotel/topbg.gif" style="background: url(<%= ImageServerUrl %>/IM/images/hotel/topbg.gif) repeat-x;
                        overflow: hidden">
                        <div style="width: 210px;">
                            <div class="headleft">
                                <img src="<%= ImageServerUrl %>/IM/images/hotel/jdname.gif"></div>
                            <div class="headright">
                                <img src="<%= ImageServerUrl %>/IM/images/hotel/114logo.gif"></div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
            <tbody>
                <tr>
                    <td height="21" background="<%= ImageServerUrl %>/IM/images/hotel/navbg.gif" align="left">
                        <div id="sp_cont">
                            <ul id="spmenu">
                                <li class="spmenuOn">酒店查询</li>
                                <li class=""><a href="OrderSearch.aspx" style="text-decoration: none; color: Black;">
                                    酒店订单查看</a></li>
                            </ul>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%" id="spmenuCon_10"
            style="display: block;">
            <tbody>
                <tr>
                    <td class="nrbj pand">
                        <table cellspacing="0" cellpadding="0" border="0" align="left" width="196">
                            <tbody>
                                <tr>
                                    <td height="22" align="left" width="68">
                                        输入城市：
                                    </td>
                                    <td height="22" align="left" width="128">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td height="22" align="left" colspan="2">
                                        <input name="txtCity" id="txtCity" style="color: Black" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td height="22" align="left" colspan="2">
                                        <asp:DropDownList ID="ddl_City" runat="server" Width="190px">
                                            <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22" align="left">
                                        入住日期：
                                    </td>
                                    <td height="22" align="left" valign="middle">
                                        <input value="" style="width: 90px; color: Black" id="txtInTime" onfocus="WdatePicker({onpicked:function(){$('#txtLeaveTime').focus();}})" />
                                        <img onclick="javascript:$('#txtInTime').focus()" src="<%= ImageServerUrl %>/IM/images/Hotel/jpinputr.gif"
                                            style="position: relative; top: 3px; left: 0px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22" align="left">
                                        离店日期：
                                    </td>
                                    <td height="22" align="left" valign="middle">
                                        <input value="" style="width: 90px; color: Black" id="txtLeaveTime" onfocus="WdatePicker()" />
                                        <img onclick="javascript:$('#txtLeaveTime').focus()" src="<%= ImageServerUrl %>/IM/images/Hotel/jpinputr.gif"
                                            style="position: relative; top: 3px; left: 0px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22" align="left">
                                        酒店名称：
                                    </td>
                                    <td height="22" align="left">
                                        <input value="" id="txtHotelName" style="color: Black">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="22" align="left">
                                        地理位置：
                                    </td>
                                    <td height="22" align="left">
                                        <asp:DropDownList ID="ddlGeography" runat="server" Width="120px">
                                            <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="33" align="left" colspan="2">
                                        <a href="javascript:void(0);" id="btnSearch">
                                            <img border="0" height="25" width="86" src="<%= ImageServerUrl %>/IM/images/hotel/guoneijiu.gif" /></a>
                                        <a href="javascript:void(0);" id="btnSearchGuoJi">
                                            <img border="0" height="25" width="86" src="<%= ImageServerUrl %>/IM/images/hotel/guojijiu.gif" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="line-height: 16px;" colspan="2">
                                        <font color="#623301"><strong>客服 </strong>
                                            <%=Utils.GetMQ("35791")%>
                                            <br />
                                            电话：0571-56893761<br />
                                            手机：15356126700</font>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="0" align="left" width="210">
                            <tbody>
                                <tr>
                                    <td height="38">
                                        <img height="28" width="207" src="<%= ImageServerUrl %>/IM/images/hotel/lipin.gif" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetHotelCity") %>"></script>

    <script type="text/javascript">
        $(function() {
            //
            SearchPage.AddItemToCity("Load");
            $("#txtCity").keyup(function() {
                if (SearchTimeOut != null) {
                    clearTimeout(SearchTimeOut);
                }
                SearchTimeOut = setTimeout(SearchPage.TxtKeyUp, 200);
            });
            $("#btnSearch").click(function() {
                var cityCode = $("#<%=ddl_City.ClientID%>").val();
                var inTime = $.trim($("#txtInTime").val());
                var leaveTime = $.trim($("#txtLeaveTime").val());
                var hotelName = $("#txtHotelName").val();
                var landMark = $("#<%=ddlGeography.ClientID %>").val();
                var landMarkTxt = $("#<%=ddlGeography.ClientID %>  option:selected").text();

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

                var para = { cityCode: "", inTime: "", leaveTime: "", hotelName: "", landMark: "", CityId: "", sort: "", landMarkTxt: "" };

                para.cityCode = cityCode;
                para.inTime = inTime;
                para.leaveTime = leaveTime;
                para.landMark = landMark;
                para.landMarkTxt = landMarkTxt;
                para.sort = "3";
                para.CityId = "<%=CityId %>";

                window.open("<%=Domain.UserPublicCenter %>/HotelManage/HotelSearchList.aspx?" + $.param(para));
            })

            $("#btnSearchGuoJi").click(function() {
                window.open("http://hotel.tongye114.com/");
            });

        })
        var SearchTimeOut = null;
        var SearchPage = {
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
            TxtKeyUp: function() {
                var val = $("#txtCity").val();
                if ($.trim(val) != "") {
                    if (SearchPage.AddItemToCity(val)) {
                        var code = $("#<%=ddl_City.ClientID %>").val();
                        //添加地理位置到下拉框
                        SearchPage.AddItemToGeography(code);
                    }
                }
            },
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
            }
        }
    </script>

    </form>
</body>
</html>
