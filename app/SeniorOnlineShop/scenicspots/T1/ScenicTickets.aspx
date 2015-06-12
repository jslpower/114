<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicTickets.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicTickets" MasterPageFile="~/master/ScenicSpotsT1.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/scenicspots/usercontrol/ChildMenu.ascx" TagName="ChildMenu" TagPrefix="uc1" %>
<%@ Register Src="/scenicspots/usercontrol/GoogleMapControl.ascx" TagName="GoogleMapControl"
    TagPrefix="uc2" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
    <!--main start-->
    <div class="main">
        <!--sidebar02 start-->
        <div class="sidebar02 sidebar02Scenic">
            <!-- 所在位置 -->
            <uc1:ChildMenu ID="ChildMenu1" runat="server" />
            <div class="content">
                <div class="jieshao">
                    <div class="jieshaotop">
                        <label id="lblScenicName" runat="server">
                        </label>
                    </div>
                    <div class="jieshaoimg">
                        <a href="" runat="server" target="_blank" id="hrefImg">
                            <img src="" width="244" height="199" id="imgScenicImg" runat="server" /></a>
                    </div>
                    <ul>
                        <li><span class="a3a3a">所属地区:</span><label id="lblDiqu" runat="server"></label></li>
                        <li><span class="a3a3a">主题分类:</span><span class="zhutifl" id="lblTheme" runat="server"></span><span
                            id="lblScenicLevel" runat="server" class="fff0f"></span></li>
                        <li><span class="a3a3a">客服电话:</span><label runat="server" id="lblTelephone"></label>
                        </li>
                        <li><span class="a3a3a">景区联系人:</span><label id="lblContact" runat="server"></label></li>
                        <li><span class="a3a3a">开放时间:</span><label id="lblOpenTime" runat="server"></label></li>
                    </ul>
                </div>
                <div class="lianxi_biao">
                    <asp:Literal ID="ltlScenicTickets" runat="server"></asp:Literal>
                </div>
                <div class="faq">
                    <div class="faq-title">
                        <ul class="clearfix">
                            <li><a class="hover" onclick="setTab('faq',1,4)" id="faq1">景区介绍</a></li>
                            <li><a onclick="setTab('faq',2,4);initialize();return false;" id="faq2">地图信息</a></li>
                            <li><a onclick="setTab('faq',3,4)" id="faq3">美图美景</a></li>
                            <li><a onclick="setTab('faq',4,4)" id="faq4">周边设施</a></li>
                        </ul>
                    </div>
                    <div class="faq-main">
                        <div id="con_faq_1">
                            <label id="lblDescription" runat="server">
                            </label>
                        </div>
                        <div id="con_faq_2" style="display: none;">
                            <div class="jqxxdz">
                                <strong>景区详细地址：</strong><label id="lblAddress" runat="server"></label></div>
                            <style type="text/css">
                                #map_canvas
                                {
                                    width: 681px;
                                    height: 415px;
                                    border: 0px solid gray;
                                }
                            </style>
                            <div id="map_canvas">
                                正在载入...
                            </div>
                            <label id="lblDescriptionDetail" runat="server">
                            </label>
                        </div>
                        <div id="con_faq_3" style="display: none;">
                            <asp:Repeater ID="rpt_ScenicImg" runat="server">
                                <HeaderTemplate>
                                    <ul class="ScenicSpotPic">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li><a href="/scenicspots_<%#Eval("ImgId") %>_<%# Eval("CompanyId") %>">
                                        <img width="186" height="140" alt='<%# Eval("ScenicName") %>' src="<%#Utils.GetNewImgUrl(Eval("ThumbAddress").ToString(),3) %>" /><br>
                                        <%# Eval("ScenicName") %></a></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div id="con_faq_4" style="display: none;">
                        <span id="lblFacilities" runat="server"></span>
                    </div>
                </div>
            </div>
            <div class="clearboth">
            </div>
        </div>
    </div>
    <div class="clearboth">
    </div>
    <!--sidebar02 end-->

    <script type="text/javascript" src="http://ditu.google.com/maps?file=api&amp;v=2&amp;key=<%= GoogleMapKey %>"></script>

    <script type="text/javascript">
    var IsLoad = false;
    /**
    * 创建地图控件
    */
    function initialize() {
        if(IsLoad) {
            return;
        }
        if (GBrowserIsCompatible()) {
            map = new GMap2(document.getElementById('map_canvas'));
            // 将视图定位到当前经纬度
            map.setCenter(new GLatLng(<%= Latitude %>, <%= Longitude %>), 13);

            var ico = new GIcon(G_DEFAULT_ICON, '<%= EyouSoft.Common.Domain.ServerComponents %>/scenicspots/T1/images/googleMapRed.png');
            var targetPt = new GLatLng(<%= Latitude %>, <%= Longitude %>);
            var marker = new GMarker(targetPt, { icon: new GIcon(ico) });
            
            marker.openInfoWindow(createInfoWindow());
            map.addControl(new GLargeMapControl());
            map.addControl(new GOverviewMapControl());
        
            //启用鼠标滚轮缩放地图
            map.enableScrollWheelZoom();
            // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
	        GEvent.addListener(marker, 'click', function() { 
			        marker.openInfoWindow(createInfoWindow()); 
	        } );
            map.addOverlay(marker);
            map.setZoom(6);
        }
        
        IsLoad = true;
    }

    /**
    * 为气泡提示窗口创建 DOM 对象
    */
    function createInfoWindow() {
        // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
        var div = document.createElement('div');
        div.innerHTML = '<div style="font-size: 9pt; width:300px;height:20px"><%= ShowTitleText %></div>';

        // 当用户关闭气泡时 Google Map API 会自动释放该对象  
        return div;
    }
    
    $(window).bind("unload",function() {GUnload();})
    </script>

    <script type="text/javascript">
$(function(){
	$(".llbiao dl").hover(function(){$(this).addClass("hover")},function(){
	$(this).removeClass("hover")
	});
	});
    </script>

    <script type="text/javascript">
/*页面---Tab选项卡---效果*/
function setTab(name,cursel,n){
    for(i=1;i<=n;i++){
    var menu=document.getElementById(name+i);
    var con=document.getElementById("con_"+name+"_"+i);
    menu.className=i==cursel?"hover":"";
    con.style.display=i==cursel?"block":"none";
    }
}
    </script>

</asp:Content>
