<%@ Page Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ScenicDetails.aspx.cs" Inherits="UserPublicCenter.ScenicManage.ScenicDetails" %>

<%--旧页面   <%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="ViewRightControl.ascx" TagName="ViewRightControl" TagPrefix="uc2" %>
<%@ Register src="NewAttrControl.ascx" tagname="NewAttrControl" tagprefix="uc4" %>
<%@ Register src="TicketsControl.ascx" tagname="TicketsControl" tagprefix="uc5" %>
<%@ Register Src="../WebControl/GeneralShopControl.ascx" TagName="GeneralShopControl"
    TagPrefix="uc3" %>--%>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<%@ Register Src="ViewRightControl.ascx" TagName="ViewRightControl" TagPrefix="uc2" %>
<%@ Register Src="TicketsControl.ascx" TagName="NewTicketsControl" TagPrefix="uc3" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc4" %>
<%@ Register Src="GoogleMapControl.ascx" TagName="GoogleMapControl" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <style type="text/css">
        #map_canvas
        {
            width: 708px;
            height: 415px;
            border: 1px solid gray;
        }
    </style>
    <%--            width: 708px;
            height: 415px;--%>
    <link href="<%=CssManage.GetCssFilePath("head2011") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("toplist") %>"></script>

    <script language="javascript" type="text/javascript">
        function pucker_show(name, no, hiddenclassname, showclassname, count) {
            for (var i = 1; i < count + 1; i++) {
                document.getElementById(name + i).className = hiddenclassname;
            }
            document.getElementById(name + no).className = showclassname;
        }
        function pucker_show1(name, no, hiddenclassname, showclassname, count) {
            for (var i = 1; i < count + 1; i++) {
                document.getElementById(name + i).className = hiddenclassname;
            }
            document.getElementById(name + no).className = showclassname;
        }
        $(function() {
            $(".gouwucp_xin").hover(function() { $(this).addClass("hover") }, function() {
                $(this).removeClass("hover")
            });
        });
    </script>

    <script type="text/javascript">
        /*页面---Tab选项卡---效果*/
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                var con = document.getElementById("con_" + name + "_" + i);
                menu.className = i == cursel ? "hover" : "";
                con.style.display = i == cursel ? "block" : "none";
            }
        }
    </script>

    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <uc4:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="boxbanner">
        <asp:Literal ID="litImgBoxBannerSecond" runat="server"></asp:Literal></div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="220" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="gwright_xin">
                            <div class="gwrhang_xin">
                                景区搜索</div>
                            <table width="204" border="0" cellspacing="0" cellpadding="0" style="margin-left: 8px;
                                margin-top: 5px;" id="searchTable">
                                <tr>
                                    <td width="62" height="25">
                                        景区名字：
                                    </td>
                                    <td width="142">
                                        <input name="txtScenicName" id="txtScenicName" type="text" style="height: 18px; width: 140px;
                                            color: #999; border: 1px solid #ccc;" value="请输入关键字" onfocus="$(this).css('color', 'black');if(this.value == '请输入关键字') {this.value = '';}"
                                            onblur="if (this.value == '') {$(this).css('color', '#999');this.value = '请输入关键字';}" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25">
                                        所在区域：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_ProvinceList" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddl_CityList" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30">
                                        &nbsp;
                                    </td>
                                    <td align="center" valign="middle">
                                        <img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/UserPublicCenter/susoss_03.jpg"
                                            width="59" height="20" alt="" id="SearchImage" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10">
                    <tr>
                        <td class="gwright2">
                            <div class="gwrhang_xin">
                                特价门票</div>
                            <uc3:NewTicketsControl ID="NewTicketsControl1" runat="server" TopNum="10" />
                        </td>
                    </tr>
                </table>
                <uc2:ViewRightControl ID="ViewRightControl1" runat="server" />
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10"
                    style="display: none;">
                    <tr>
                        <td align="left" class="gwright_xin">
                            <div class="gwrhang_xin">
                                附近酒店</div>
                            <div class="gwrnei">
                                <ul>
                                    <li><a href="#">·宁波野鹤湫风景区</a></li>
                                    <li><a href="#">·杭州白云源风景区旅游开发有限公</a></li>
                                    <li><a href="#">·丽水市瓯江漂流有限公司</a></li>
                                    <li><a href="#">·宁波野鹤湫风景区</a></li>
                                    <li><a href="#">·杭州白云源风景区旅游开发有限公</a></li>
                                    <li><a href="#">·丽水市瓯江漂流有限公司</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="maintop10"
                    style="display: none;">
                    <tr>
                        <td align="left" class="gwright_xin">
                            <div class="gwrhang_xin">
                                目的地线路</div>
                            <div class="gwrnei">
                                <ul>
                                    <li><a href="#">·宁波野鹤湫风景区</a></li>
                                    <li><a href="#">·杭州白云源风景区旅游开发有限公</a></li>
                                    <li><a href="#">·丽水市瓯江漂流有限公司</a></li>
                                    <li><a href="#">·宁波野鹤湫风景区</a></li>
                                    <li><a href="#">·杭州白云源风景区旅游开发有限公</a></li>
                                    <li><a href="#">·丽水市瓯江漂流有限公司</a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="735" valign="top">
                <div class="xiaodaohang">
                    <div>
                        您的位置： <a href="<%=SubStation.CityUrlRewrite(CityId) %>">同业114</a>&nbsp;&gt;&nbsp;<a
                            href="<%=ScenicSpot.ScenicDefalutUrl(CityId) %>">景区</a>&nbsp;&gt;&nbsp;<%=ScenicModel.CityName != "" ? "<a href=\"/ScenicManage/ScenicList.aspx?cid=" + ScenicModel.CityId + "\">" + ScenicModel.CityName + "景区</a>" : ""%>&nbsp;&gt;&nbsp;<span
                                class="zuouhou"><%=ScenicModel.ScenicName%></span></div>
                </div>
                <div class="contentbox">
                    <div class="jieshao">
                        <div class="jieshaotop" title="<%=ScenicModel.ScenicName %>">
                            <%=Utils.GetText2(Utils.LoseHtml(ScenicModel.ScenicName), 20, true)%>
                        </div>
                        <div class="jieshaoimg">
                            <a href="<%=Utils.GetNewImgUrl(result!=""?result.Split('$')[1]:"",3) %>" target="_blank"
                                title="<%=result!=""?result.Split('$')[2]:"暂无" %>">
                                <img src="<%=Utils.GetNewImgUrl(result!=""?result.Split('$')[0]:"",3) %>" width="250"
                                    height="190" onerror="this.src='<%=Utils.GetNewImgUrl("",2)%>';" />
                            </a>
                        </div>
                        <ul>
                            <li><span class="a3a3a">所属地区:</span><%=ScenicModel.ProvinceName != "" ? "<a href=\"jingqumap_" + CityId + "_" + ScenicModel.ProvinceId + "\">" + ScenicModel.ProvinceName + "</a>" : ""%>
                                <%=ScenicModel.CityName != "" ? "<a href=\"/ScenicManage/ScenicList.aspx?cid=" + ScenicModel.CityId + "\">" + ScenicModel.CityName + "</a>" : ""%>
                                <%=ScenicModel.CountyName %></li>
                            <li title="<%= GetThemeName(ScenicModel.ThemeId)%>"><span class="a3a3a">主题分类:</span><span
                                class="zhutifl">
                                <%=Utils.GetText2(Utils.LoseHtml(GetThemeName(ScenicModel.ThemeId)), 25, true)%>
                            </span></li>
                            <li title="<%=ScenicModel.Telephone+"  "+GetContactName(ScenicModel.ContactOperator)%>">
                                <span class="a3a3a">客服电话:</span><%=Utils.GetText2(ScenicModel.Telephone,13,true) %>
                                <%--<%=Utils.GetText2(GetContactName(ScenicModel.ContactOperator),10,true)%>--%></li>
                            <li><span class="a3a3a">管理公司:</span>
                                <%=GetRe(ScenicModel.Company == null ? "" : ScenicModel.Company.ID)%></li>
                            <li title="<%=ScenicModel.CnAddress %>"><span class="a3a3a">景区地址:</span><%=Utils.GetText2(Utils.LoseHtml(ScenicModel.CnAddress), 20, true)%></li>
                            <li><span class="a3a3a">景区等级:</span><%=ScenicModel.ScenicLevel!=0?Enum.Format(typeof(EyouSoft.Model.ScenicStructure.ScenicLevel), ScenicModel.ScenicLevel, "G"):"暂无等级"%></li>
                            <li title="<%=ScenicModel.OpenTime %>"><span class="a3a3a">开放时间:</span><%=Utils.GetText2(Utils.LoseHtml(ScenicModel.OpenTime), 20, true)%></li>
                        </ul>
                    </div>
                    <div class="lianxi_biao">
                        <ul class="lianxi_di1">
                            <li>票型</li>
                            <li>票价时限</li>
                            <li>门市价</li>
                        </ul>
                        <% if (ScenicModel.TicketsList != null)
                           {%>
                        <%foreach (var item in ScenicModel.TicketsList)
                          {%>
                        <ul class="lianxi_di2">
                            <li title="<%=item.TypeName %>">
                                <%=Utils.GetText2(item.TypeName,10,true) %></li>
                            <li>
                                <%=GetGoodTime(item.StartTime,item.EndTime) %>
                                <%--<%=Convert.ToDateTime(item.StartTime).ToShortDateString() %>--<%=Convert.ToDateTime(item.EndTime).ToShortDateString()%>--%></li>
                            <li>
                                <%=string.Format("{0:C0}", item.RetailPrice)%></li>
                        </ul>
                        <%} %>
                        <%} %>
                    </div>
                    <div class="faq">
                        <div class="faq-title">
                            <ul class="clearfix">
                                <li><a class="hover" onclick="setTab('faq',1,4)" id="faq1">景区介绍</a></li>
                                <li><a onclick="setTab('faq',2,4);initialize();" id="faq2">地图信息</a></li>
                                <li><a onclick="setTab('faq',3,4)" id="faq3">美图美景</a></li>
                                <li><a onclick="setTab('faq',4,4)" id="faq4">周边设施</a></li>
                            </ul>
                        </div>
                        <div class="faq-main">
                            <div id="con_faq_1">
                                <%=ScenicModel.Description %>
                                <div id="jqcontact" style="display: none;">
                                    <div class="dlhck">
                                        景区联系人</div>
                                    <div class="lianxiren">
                                        <div class="denglukan">
                                        </div>
                                        <div class="dlkanxx">
                                            <ul class="biaotilele">
                                                <li class="XINGMING"><strong>姓名</strong></li>
                                                <li class="TEL"><strong>电话</strong></li>
                                                <li class="SHOUJI"><strong>手机</strong></li>
                                                <li class="CHUANZHEN"><strong>传真</strong></li>
                                                <li class="MQ"><strong>MQ</strong></li>
                                                <li class="QQ"><strong>QQ</strong></li>
                                            </ul>
                                            <ul class="neironglele">
                                                <li class="XINGMING"><strong>
                                                    <asp:Literal ID="txt_ContactName" runat="server"></asp:Literal>
                                                </strong></li>
                                                <li class="TEL">
                                                    <asp:Literal ID="txt_Tel" runat="server"></asp:Literal>
                                                </li>
                                                <li class="SHOUJI">
                                                    <asp:Literal ID="txt_Mobile" runat="server"></asp:Literal>
                                                    <br />
                                                </li>
                                                <li class="CHUANZHEN">
                                                    <asp:Literal ID="txt_Fax" runat="server"></asp:Literal>
                                                </li>
                                                <li class="MQ">
                                                    <%=Mq%>
                                                </li>
                                                <li class="QQ">
                                                    <%=QQ %><a href="#">
                                                        <asp:Literal ID="txt_QQ" runat="server"></asp:Literal>
                                                    </a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="con_faq_2" style="display: none;">
                                <div class="jqxxdz">
                                    <strong>景区详细地址：</strong><%=ScenicModel.CnAddress %></div>
                                <div>
                                    <div id="map_canvas">
                                        load...
                                    </div>
                                    <%= Utils.TextToHtml(ScenicModel.Traffic) %>
                                </div>
                            </div>
                            <div id="con_faq_3" style="display: none;">
                                <ul class="ScenicSpotPic">
                                    <% if (ScenicModel.Img != null)
                                       { %>
                                    <%for (int i = 0; i < ScenicModel.Img.Count; i++)
                                      { %>
                                    <% if (i < 11)
                                       {%>
                                    <li title="<%=ScenicModel.Img[i].Description %>"><a href="<%=Utils.GetNewImgUrl(ScenicModel.Img[i].Address,3) %>"
                                        target="_blank">
                                        <img src="<%=Utils.GetNewImgUrl(ScenicModel.Img[i].ThumbAddress,3) %>" onerror="this.src='<%=Utils.GetNewImgUrl("",2)%>';"
                                            width="186" height="140" /><br>
                                        <%=Utils.GetText2(ScenicModel.Img[i].Description, 15,true)%></a></li>
                                    <%} %><%} %>
                                    <%} %>
                                </ul>
                            </div>
                            <div id="con_faq_4" style="display: none; text-align: left;">
                                <%= Utils.TextToHtml(ScenicModel.Facilities)%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <div class="fjjd">
                    <ul>
                        <%=strb %>
                        <%--                        <li class="dangqian">杭州旅游景点</li>
                        <li><a href="#">宁波旅游景点</a></li>
                        <li><a href="#">温州旅游景点</a></li>
                        <li><a href="#">嘉兴旅游景点</a></li>
                        <li><a href="#">湖州旅游景点</a></li>
                        <li><a href="#">绍兴旅游景点</a></li>
                        <li><a href="#">金华旅游景点</a></li>
                        <li><a href="#">舟山旅游景点</a></li>
                        <li><a href="#">台州旅游景点</a></li>
                        <li><a href="#">衢州旅游景点</a></li>
                        <li><a href="#">丽水旅游景点</a></li>--%>
                    </ul>
                </div>
            </td>
        </tr>
    </table>
    <div class="clear">
    </div>
    <div class="hr_10">
    </div>

    <script type="text/javascript" src="http://ditu.google.com/maps?file=api&amp;v=2&amp;key=<%=GoogleMapKey %>"></script>

    <script type="text/javascript">

        $(function() {
            if ('<%=IsorNoLogin %>' == "True") {
                $("#jqcontact").show();
                //$("#jqcontact").css("display","");
            }
            else
                $("#jqcontact").hide();
            //$("#jqcontact").css("display","none;");

        });

    </script>

    <script type="text/javascript">
    
    var IsLoad = false;
    /**
    * 创建地图控件
    */
    function initialize() {
        if(IsLoad)
        {
            return;
        }
        var b1=false;
        b1=GBrowserIsCompatible();
        if (b1) {//判断浏览器是否支持google地图
            document.getElementById('map_canvas').innerHTML="";
            map = new GMap2(document.getElementById('map_canvas'));
            // 将视图定位到当前经纬度<%=_Longitude %>, <%=_Latitude %> ||<%=_Latitude %>,<%=_Longitude %>
            map.setCenter(new GLatLng(<%=_Latitude %>,<%=_Longitude %>), 13);

            
            var ico = new GIcon(G_DEFAULT_ICON, '<%=Domain.ServerComponents %>/scenicspots/T1/images/googleMapRed.png');
            var targetPt = new GLatLng(<%=_Latitude %>,<%=_Longitude %>);
            var marker = new GMarker(targetPt, { icon: new GIcon(ico) });
            if (true)
            {
                marker.openInfoWindow(createInfoWindow());
            }
            // 平移及缩放控件（左上角）、缩略图控件（右下角）
            if(true)
            {
                map.addControl(new GLargeMapControl());
            }
            if(true)
            {
                map.addControl(new GOverviewMapControl());
            }
            //启用鼠标滚轮缩放地图
            map.enableScrollWheelZoom();
            // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
	        GEvent.addListener(marker, 'click', function() { 
			        marker.openInfoWindow(createInfoWindow()); 
	        } );
            map.addOverlay(marker);
            map.setZoom(8);
        }
        else{
            document.getElementById('map_canvas').innerHTML="您的浏览器不支持google地图";
        }
        IsLoad = true;
    }

    /**
    * 为气泡提示窗口创建 DOM 对象
    */
    function createInfoWindow() {
        // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
        var div = document.createElement('div');
        div.innerHTML = '<div style="font-size: 9pt; width:300px;height:20px"><%=defaultname %></div>';

        // 当用户关闭气泡时 Google Map API 会自动释放该对象  
        return div;
    }
    
    </script>

    <script type="text/javascript">

        function SetProvince(ProvinceId) {
            $("#<%=ddl_ProvinceList.ClientID %>").attr("value", ProvinceId);
        }
        function SetCity(CityId) {
            $("#<%=ddl_CityList.ClientID %>").attr("value", CityId);
        }           

    </script>

    <script type="text/javascript" language="javascript">
        //搜索
        var Parms = { ProvinceId: 0, CityId: 0, CountyId: 0, ScenicName: "" };
        $(function() {
            $("#searchTable").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#SearchImage").click();
                    event.returnValue = false;
                    return false;
                }
            });
            $("#SearchImage").click(function() {
                Parms.ProvinceId = $("#<%=ddl_ProvinceList.ClientID %>").val();
                Parms.CityId = $("#<%=ddl_CityList.ClientID %>").val();
                var searchVal = $.trim($("#txtScenicName").val());
                if (searchVal != "请输入关键字")
                    Parms.ScenicName = escape($.trim($("#txtScenicName").val()));
                window.location.href = "/ScenicManage/ScenicList.aspx?pid=" + Parms.ProvinceId + "&cid=" + Parms.CityId + "&searchVal=" + Parms.ScenicName + "";
            });
        })
    </script>

</asp:Content>
