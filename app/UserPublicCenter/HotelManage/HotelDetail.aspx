<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HotelDetail.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelDetail" %>

<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/HotelControl/HotelSearchControl.ascx" TagName="HotelSearch"
    TagPrefix="uc2" %>
<%@ Register Src="~/WebControl/HotelControl/SpecialHotelControl.ascx" TagName="SpecialHotel"
    TagPrefix="uc3" %>
<%@ Register Src="~/WebControl/HotelControl/HotHotelControl.ascx" TagName="HotHotel"
    TagPrefix="uc4" %>
<%@ Register Src="~/WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUser"
    TagPrefix="uc5" %>
<%@ Register Src="~/WebControl/HotelControl/ImgFristControl.ascx" TagName="ImgFrist"
    TagPrefix="uc6" %>
<%@ Register Src="~/WebControl/HotelControl/ImgSecondControl.ascx" TagName="ImgSecond"
    TagPrefix="uc7" %>
<%@ Register Src="~/WebControl/HotelControl/ImgBannerControl.ascx" TagName="ImgBanner"
    TagPrefix="uc8" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <style type="text/css">
        #map_canvas
        {
            width: 236px;
            height: 226px;
            border: 1px solid gray;
        }
    </style>
    <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="main">
        <uc8:ImgBanner ID="ImgBanner1" runat="server" />
        <!--content start-->
        <div class="content">
            <!--sidebar start-->
            <div class="sidebar sidebarSearch">
                <!--sidebar_1-->
                <uc2:HotelSearch ID="HotelSearch1" runat="server" />
                <!--sidebar_1 end-->
                <!--sidebar_1 end-->
                <uc6:ImgFrist ID="ImgFrist1" runat="server" ImageWidth="224px" />
                <!-- sidebar_2 start-->
                <uc5:CommonUser ID="CommonUser1" runat="server" />
                <uc3:SpecialHotel ID="SpecialHotel1" runat="server" />
                <!-- sidebar_2 end-->
                <uc7:ImgSecond ID="ImgSecond1" runat="server" ImageWidth="224px" />
                <uc4:HotHotel ID="HotHotel1" runat="server" />
                <!-- sidebar_2 end-->
            </div>
            <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search" id="hd_Detail">
                <div class="sidebar02_1">
                    <p class="xuanzhe">
                        <span>
                            <%=hotelModel.HotelName %></span><img src="<%=ImageServerPath%>/Images/hotel/liucheng.gif" /></p>
                    <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        <div class="Tab">
                            <ul>
                                <li><a href="#" class="Tabdefault02">基本信息</a></li>
                                <li><a href="#t1">酒店图片</a></li>
                                <li><a href="#t2">酒店简介</a></li>
                                <li><a href="#t3">前台现付</a></li>
                                <li><a href="#t4">相关信息</a></li>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="xx_content">
                            <p class="L">
                                <font class="fbb">酒店名称：</font><font class="C_Grb"><%=hotelModel.HotelName %></font>
                                <br />
                                <font style="font-weight: bold">开业时间：</font>
                                <%=Regex.Replace(hotelModel.Opendate,@"\b(\d{4}).*","$1") %>年开业<br>
                                <font class="fbb">楼层数量：</font>
                                <%= hotelModel.Floor %><font class="fbb fbb03">装修时间：</font>
                                <%=Regex.Replace(hotelModel.Fitment,@"\b(\d{4}).*","$1") %>年<br>
                                <font class="fbb">房间数量：</font><font class="frb"><%=hotelModel.RoomQuantity %></font>
                                &nbsp;&nbsp;<font class="fbb fbb04">酒店电话：</font>
                                <%=hotelModel.Tel %><br>
                                <font class="fbb">酒店星级：</font>：<font color="#F68300"><font size="2"><%=((int)hotelModel.Rank) > 5 ?"准"+((int)hotelModel.Rank - 5).ToString() :((int)hotelModel.Rank).ToString()%>星级</font></font><br>
                                <font class="fbb">酒店地址：</font><%=hotelModel.HotelPosition.Address %><br>
                                <font class="fbb">交通信息：</font> 无
                                <br />
                                <font class="fbb">周围景观：</font> 无
                            </p>
                            <div id="map_canvas">
                                loading...</div>
                            <%--<img src="<%=ImageServerPath%>/Images/hotel/map.gif" border="0" usemap="#Map" class="R">
                                <map name="Map" id="Map"><area shape="rect" coords="4,1,252,232" href="#" /></map>--%>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="Tab">
                            <ul>
                                <li><a href="#" name="t1" class="Tabdefault02">酒店图片</a></li></ul>
                            <div class="clear">
                            </div>
                        </div>
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="hotelpictable">
                            <tr>
                                <%if (hotelModel.HotelImages != null && hotelModel.HotelImages.Count > 0)
                                  {
                                      foreach (EyouSoft.Model.HotelStructure.HotelImagesInfo image in hotelModel.HotelImages)
                                      {%>
                                <td width="33%" align="center">
                                    <img src="<%=EyouSoft.HotelBI.Utils.ImagesUrl%><%=image.ImageURL  %>" width="207"
                                        height="146" />
                                </td>
                                <%}
                                  }%>
                            </tr>
                        </table>
                        <div class="Tab">
                            <ul>
                                <li><a href="#" name="t2" class="Tabdefault02">酒店简介</a></li></ul>
                            <div class="clear">
                            </div>
                        </div>
                        <div style="padding: 10px;">
                            <%=hotelModel.LongDesc %></div>
                        <div class="Tab">
                            <ul>
                                <li><a href="#" name="t3" class="Tabdefault02">前台现付</a></li></ul>
                            <div style="text-align: right; padding-right: 10px; padding-top: 3px;">
                                入住日期：<input name="txtcomeDate" type="text" value="<%=comeDate %>" id="txtComeDate"
                                    style="width: 90px; vertical-align: middle;" onfocus='WdatePicker({onpicked:function(){HotelDetail.TxtGetFocus();},minDate:"%y-%M-%d"})' />
                                离店日期：<input name="txtLeaveDate" type="text" id="txtLeaveDate" value="<%=leaveDate %>"
                                    style="width: 90px; margin-right: 5px; vertical-align: middle;" onfocus="WdatePicker({minDate:'%y-%M-%d'});" />
                                <a href="javascript:;" onclick="return changeRateType();">
                                    <img style="vertical-align: middle;" src="<%=ImageServerPath %>/images/hotel/hotelupdate.gif" /></a>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dddddd"
                            style="margin-bottom: 10px;">
                            <tr id="hd_priceTitle">
                                <th width="10%" height="25" align="center" bgcolor="#f2f2f2">
                                    房型
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    门市价
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    销售价
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    返佣价
                                </th>
                                <th width="10%" align="center" nowrap="nowrap" bgcolor="#f2f2f2">
                                    返佣比例
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    结算价
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    早餐
                                </th>
                                <th width="10%" align="center" bgcolor="#f2f2f2">
                                    床型
                                </th>
                                <th width="8%" align="center" bgcolor="#f2f2f2">
                                    宽带
                                </th>
                                <th width="12%" align="center" bgcolor="#f2f2f2">
                                    预定
                                </th>
                            </tr>
                            <%=strRateHtml %>
                        </table>
                        <div class="Tab">
                            <ul>
                                <li><a href="#" name="t4" class="Tabdefault02">相关信息</a></li></ul>
                            <div class="clear">
                            </div>
                        </div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="aboutIntro">
                            <tr>
                                <th width="15%" align="right">
                                    额外收费：
                                </th>
                                <td width="88%">
                                    <%=efPriceHTML%>
                                </td>
                            </tr>
                            <tr>
                                <th align="right">
                                    地标信息：
                                </th>
                                <td>
                                    <%=hotelModel.HotelPosition.POR %>
                                </td>
                            </tr>
                            <tr>
                                <th align="right">
                                    酒店设施：
                                </th>
                                <td>
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <th align="right">
                                    地区查询：
                                </th>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--sidebar02SearchC end-->
                </div>

                <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

                <script type="text/javascript">
        function initialize() {
  var title="<%=hotelModel.HotelName %>"
  var map;
  var infowindow = new google.maps.InfoWindow();//地图内置信息窗口
  var marker;
  var lat = <%=hotelModel.HotelPosition.Latitude %>;//维度
  var lng =<%=hotelModel.HotelPosition.Longitude %>;//经度
  var oLatLng = new google.maps.LatLng(lat,lng);//地图经纬度对象
     
    /*
    地图初始化参数
    */
    var myOptions = {
      zoom: 15,
      center: oLatLng,
      mapTypeId: google.maps.MapTypeId.ROADMAP,
      mapTypeControl:false,
      streetViewControl:false
    }
    var oMapContainer = document.getElementById("map_canvas");//地图容器
    map = new google.maps.Map(oMapContainer, myOptions);
    //初始化对应经纬度的标记
    marker = new google.maps.Marker({
      position: oLatLng, 
      map: map, 
      title:title/* 鼠标放上去的提示 */
    });   
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'latLng': oLatLng
     },function(results,status){
        if(status==google.maps.GeocoderStatus.OK){
            if (results[0]) {
            infowindow.setContent(results[0].formatted_address);
//            google.maps.event.addListener(marker, 'click', function() {
//              infowindow.open(map,marker);
//            });
            }
        }
     });
  }
  $(function(){
    initialize();
   
  });
    var HotelDetail={
     TxtGetFocus: function() {
        $("#txtLeaveDate").focus();
      }
    }
                </script>

            </div>
        </div>
        <!--sidebar02 end-->
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript">
           
	              function changeRateType() {
	                  var hdpriceTitle = $("#hd_priceTitle");
	                  hdpriceTitle.nextAll("tr").remove();
	                  hdpriceTitle.after("<tr><td colspan='10'><div style='text-align:center; padding-top:20px;'><img src='<%=ImageServerPath%>/Images/loadingnew.gif'/><div>正在加载价格信息…</div></div></td></tr>");
	                  $.ajax(
	                  {
	                      url: "/HotelManage/HotelDetail.aspx?hotelCode=<%=hotelCode %>&IsLogin=<%=IsLogin %>&cityId=<%=cityId %>",
	                      data: { method: "changeDate", comeDate: $("#txtComeDate").val(), leaveDate: $("#txtLeaveDate").val() },
	                      dataType: "text",
	                      cache: false,
	                      type: "get",
	                      success: function(result) {
	                          if (result.length > 30) {
	                              hdpriceTitle.nextAll("tr").remove();
	                              hdpriceTitle.after(result);
	                          }
	                          else {
	                              hdpriceTitle.nextAll("tr").remove()
	                              hdpriceTitle.after("<tr><td colspan='10'><div style='text-align:center;color:red;padding-top:10px;'>" + result + "</div></td></tr>");
	                          }

	                      },
	                      error: function() {
	                      hdpriceTitle.after("<tr><td colspan='10'><div style='text-align:center;color:red;padding-top:10px;'> 查询出错</div></td></tr>");
	                      }
	                  });
	              }
          
    </script>

</asp:Content>
