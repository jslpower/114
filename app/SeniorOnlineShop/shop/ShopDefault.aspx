<%@ Page Language="C#" MasterPageFile="~/master/GeneralShop.Master" AutoEventWireup="true" CodeBehind="ShopDefault.aspx.cs" Inherits="SeniorOnlineShop.shop.ShopDefault" Title="无标题页" %>
<%@ Register src="../GeneralShop/GeneralShopControl/SecondMenu.ascx" tagname="SecondMenu" tagprefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("head2011") %>" rel="Stylesheet" type="text/css" />
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("xinmian") %>" rel="Stylesheet" type="text/css" />
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet" type="text/css" />
<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("boxy") %>"></script>
<script type="text/javascript" src="<%= EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>
<script type="text/javascript"> 
</script>
<script type="text/javascript">
//公告滚动
function AutoScroll(obj){
        $(obj).find("ul:first").animate({
                marginTop:"-15px"
        },500,function(){
                $(this).css({marginTop:"0px"}).find("li:first").appendTo(this);
        });
}
$(document).ready(function(){
setInterval('AutoScroll("#scrollDiv")',2000)
});
</script>

<script language="javascript">
function showinf(n)
{
  var showcount=3;
  for (var i=1;i<=showcount;i++)
   { 
	 if(i==n)
	 {
	 	document.getElementById("tu"+n).style.display="block";
	 	document.getElementById("pro_a"+n).src="../同业114.com新版/images/images/flashImg/pro_a"+n+"_a.gif";
	 }	
	else
	{
	 	document.getElementById("tu"+i).style.display="none";
	 	document.getElementById("pro_a"+i).src="../同业114.com新版/images/images/flashImg/pro_a"+i+".gif";
	}
  }	
}
</script>
<script language="javascript">
$(function(){
	$("#link1").click(function(){
		var url = $(this).attr("href");
		Boxy.iframeDialog({
			iframeUrl:url,
			title:"专家团队为您服务！立即申请或请致电：0571-56884627",
			modal:true,
			width:"611px",
			height:"405px"
		});
		return false;
	});
});
</script>
<style>
.boxy-wrapper .title-bar h2 { font-size: 14px; color: #f16202; line-height: 1; margin: 0; padding:1px 0 0 0; font-weight: bold; text-align:left; height:40px; line-height:30px; overflow:hidden; text-indent:50px; background:url(../同业114.com新版/images/images/yxsq_02.png) no-repeat 25px 5px;}
</style>
<script type="text/javascript">
function tt(id) { return document.getElementById(id); }

function addLoadEvent(func){
	var oldonload = window.onload;
	if (typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function(){
			oldonload();
			func();
		}
	}
}

function moveElement(elementID,final_x,final_y,interval) {
  if (!document.getElementById) return false;
  if (!document.getElementById(elementID)) return false;
  var elem = document.getElementById(elementID);
  if (elem.movement) {
    clearTimeout(elem.movement);
  }
  if (!elem.style.left) {
    elem.style.left = "0px";
  }
  if (!elem.style.top) {
    elem.style.top = "0px";
  }
  var xpos = parseInt(elem.style.left);
  var ypos = parseInt(elem.style.top);
  if (xpos == final_x && ypos == final_y) {
		return true;
  }
  if (xpos < final_x) {
    var dist = Math.ceil((final_x - xpos)/10);
    xpos = xpos + dist;
  }
  if (xpos > final_x) {
    var dist = Math.ceil((xpos - final_x)/10);
    xpos = xpos - dist;
  }
  if (ypos < final_y) {
    var dist = Math.ceil((final_y - ypos)/10);
    ypos = ypos + dist;
  }
  if (ypos > final_y) {
    var dist = Math.ceil((ypos - final_y)/10);
    ypos = ypos - dist;
  }
  elem.style.left = xpos + "px";
  elem.style.top = ypos + "px";
  var repeat = "moveElement('"+elementID+"',"+final_x+","+final_y+","+interval+")";
  elem.movement = setTimeout(repeat,interval);
}

function classNormal(iFocusBtnID,iFocusTxID){
	var iFocusBtns= tt(iFocusBtnID).getElementsByTagName('li');
	//var iFocusTxs = tt(iFocusTxID).getElementsByTagName('li');
	for(var i=0; i<iFocusBtns.length; i++) {
		iFocusBtns[i].className='normal';
		//iFocusTxs[i].className='normal';
	}
}

function classCurrent(iFocusBtnID,iFocusTxID,n){
	var iFocusBtns= tt(iFocusBtnID).getElementsByTagName('li');
	//var iFocusTxs = tt(iFocusTxID).getElementsByTagName('li');
	iFocusBtns[n].className='current';
	//iFocusTxs[n].className='current';
}

function iFocusChange() {
	if(!tt('ifocus')) return false;
	tt('ifocus').onmouseover = function(){atuokey = true};
	tt('ifocus').onmouseout = function(){atuokey = false};
	var iFocusBtns = tt('ifocus_btn').getElementsByTagName('li');
	var listLength = iFocusBtns.length;
	iFocusBtns[0].onmouseover = function() {
		moveElement('ifocus_piclist',0,0,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',0);
	}
	if (listLength>=2) {
		iFocusBtns[1].onmouseover = function() {
			moveElement('ifocus_piclist',0,-181,5);
			classNormal('ifocus_btn','ifocus_tx');
			classCurrent('ifocus_btn','ifocus_tx',1);
		}
	}
	if (listLength>=3) {
		iFocusBtns[2].onmouseover = function() {
			moveElement('ifocus_piclist',0,-362,5);
			classNormal('ifocus_btn','ifocus_tx');
			classCurrent('ifocus_btn','ifocus_tx',2);
		}
	}
	if (listLength>=4) {
		iFocusBtns[3].onmouseover = function() {
			moveElement('ifocus_piclist',0,-443,5);
			classNormal('ifocus_btn','ifocus_tx');
			classCurrent('ifocus_btn','ifocus_tx',3);
		}
	}
}

setInterval('autoiFocus()',5000);
var atuokey = false;
function autoiFocus() {
	if(!tt('ifocus')) return false;
	if(atuokey) return false;
	var focusBtnList = tt('ifocus_btn').getElementsByTagName('li');
	var listLength = focusBtnList.length;
	for(var i=0; i<listLength; i++) {
		if (focusBtnList[i].className == 'current') var currentNum = i;
	}
	if (currentNum==0&&listLength!=1 ){
		moveElement('ifocus_piclist',0,-181,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',1);
	}
	if (currentNum==1&&listLength!=2 ){
		moveElement('ifocus_piclist',0,-362,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',2);
	}
	if (currentNum==2&&listLength!=3 ){
		moveElement('ifocus_piclist',0,-443,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',3);
	}
	if (currentNum==3 ){
		moveElement('ifocus_piclist',0,0,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',0);
	}
	if (currentNum==1&&listLength==2 ){
		moveElement('ifocus_piclist',0,0,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',0);
	}
	if (currentNum==2&&listLength==3 ){
		moveElement('ifocus_piclist',0,0,5);
		classNormal('ifocus_btn','ifocus_tx');
		classCurrent('ifocus_btn','ifocus_tx',0);
	}
}
addLoadEvent(iFocusChange);
</script>
<div class="xinmain">
   <uc1:SecondMenu ID="SecondMenu1" runat="server" />
    
    <div class="main_center">
           <div class="about">
             <div id="ifocus">
	<div id="ifocus_pic">
		<div style="left: 0px; top: -362px;" id="ifocus_piclist">
			<ul>
				<li><a target="_blank" href="<%=CompanyImg1 %>"><img width="272" height="181" alt="" src="<%=CompanyImg1 %>"></a></li>
				<li><a target="_blank" href="<%=CompanyImg2 %>"><img width="272" height="181" alt="" src="<%=CompanyImg2 %>"></a></li>
				<li><a target="_blank" href="<%=CompanyImg3 %>"><img width="272" height="181" alt="" src="<%=CompanyImg3 %>"></a></li>
			</ul>
		</div>

	</div>
	<div id="ifocus_btn">
		<ul>
			<li class="normal"><img alt="" src="<%=CompanyImg1 %>"></li>
			<li class="normal"><img alt="" src="<%=CompanyImg2 %>"></li>
			<li class="current"><img alt="" src="<%=CompanyImg3 %>"></li>
		</ul>
	</div>
</div>
             <span class="aboutlan">&nbsp;&nbsp;&nbsp;&nbsp;<%=CompanyName %></span>
            <asp:Literal runat="server" ID="ltrCompanyInfo"></asp:Literal>
           </div>
           <div class="x_contact">
            <div class="x_contactT">
                <span class="xiaodian1">联系方式</span></div>
            <div class="x_contactL">
                <div class="x_contactLT">
                    <span class="xiaoxiaoT">联系我们</span><br />
                    Email：
                    <asp:Literal runat="server" ID="ltrEmail"></asp:Literal>
                    <br />
                    QQ：<asp:Literal runat="server" ID="ltrQQ"></asp:Literal>
                    &nbsp;&nbsp;&nbsp;MSN：<asp:Literal runat="server" ID="ltrMSN"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                    MQ:
                    <asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                </div>
                <span class="xiaoxiaoT">业务优势</span>
                <asp:Literal runat="server" ID="ltrYWYS"></asp:Literal>
            </div>
           <div class="x_contactR" id="GMap" style="width:330px; height:270px"></div>
        </div>
          <%if (!IsLogin)
            {%>
           <div class="huiyuanzq"><a href="javascript:void(0)">注册登录查看更多同业联系方式</a>
           </div>
           <%} %>
           <%if (IsLogin)
             { %>
         <asp:PlaceHolder runat="server" ID="plnLoginUser">
            <div class="tylxfs">
                <div class="tylxfs_t">
                    同业联系方式</div>
                <div class="tylxfs_m">
                    <div class="lianxi_wenzi">
                        <asp:Literal runat="server" ID="ltrTYLXFS"></asp:Literal>
                    </div>
                    <table class="lianxi_biao" cellpadding="0" cellspacing="0">
                        <tr class="lianxi_di1">
                            <td>真实姓名</td>
                            <td>公司职务</td>
                            <td>电话</td>
                            <td>手机</td>
                            <td>传真</td>
                            <td>QQ</td>
                            <td style="width: 120px;">MSN</td>
                            <td style="width: 150px;" class="lianxi_di1_right">Email</td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptCompanyUser">
                            <ItemTemplate>
                                <tr class="lianxi_di2">
                                    <%# GetCompanyUserInfo(Eval("ContactInfo"), Eval("Job"))%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="tylxfs">
                <div class="tylxfs_t">
                    银行账户</div>
                <div class="tylxfs_m">
                    <div class="gszh">
                        <span class="gszh_t">·公司银行账户</span> <strong>公司全称：</strong><asp:Literal runat="server"
                            ID="ltrCompanyBank"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;<strong>开户行：</strong><asp:Literal runat="server" ID="ltrBankName"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>帐号：</strong><asp:Literal runat="server" ID="ltrAccount"></asp:Literal>
                    </div>
                    <div class="gszh">
                        <span class="gszh_t">·个人银行账户</span>
                        <asp:Repeater runat="server" ID="rptPersonalAccount">
                            <ItemTemplate>
                                <strong>户 名：</strong><%# Eval("BankAccountName")%>
                                &nbsp;&nbsp;&nbsp;&nbsp;<strong>开户行：</strong><%# Eval("BankName")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                <strong>帐号：</strong><%# Eval("AccountNumber")%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="tylxfs">
                <div class="tylxfs_t">
                    证书</div>
                <div class="tylxfs_m">
                
                    <ul>
                        <li><a href=<%= LicenceImg %>" target="_blank">
                            <img src="<%= LicenceImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="javascript:void(0)" target="_blank">营业执照</a></span></li>
                        <li><a href="<%= BusinessCertImg %>" target="_blank">
                            <img src="<%= BusinessCertImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="javascript:void(0)" target="_blank">经营许可证</a></span></li>
                        <li><a href="<%= TaxRegImg %>" target="_blank">
                            <img src="<%= TaxRegImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="javascript:void(0)" target="_blank">税务登记证</a></span></li>
                        <%--<li><a href="#">
                            <img src="images/putong_34.jpg" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="#">公司电子章</a></span></li>
                        <li><a href="#">
                            <img src="images/putong_34.jpg" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                href="#">企业LOGO</a></span></li>--%>
                    </ul>
                </div>
            </div>
        </asp:PlaceHolder>
          <%} %>
          
        </div>
         <div style="clear:both; height:10px;"></div>
        </div>
       <%--浮动咨询开始--%>
    <div id="divZX" style="display:none;z-index:99999;">
        <table height="140" cellspacing="0" cellpadding="0" border="0" background="<%= Domain.ServerComponents %>/images/seniorshop/zixunbg.gif"
            width="400">
            <tbody>
                <tr>
                    <td height="5" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left" valign="top" colspan="2">
                        &nbsp;&nbsp;您好，<%=CompanyName %>竭诚为您服务
                    </td>
                </tr>
                <tr>
                    <td valign="middle" colspan="2">
                        <asp:Label runat="server" Text="欢迎您,有什么可以帮助您的吗？" ID="lbGuestInfo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <a href="/OlServer/Default.aspx?cid=<%= master.CompanyId %>" target="blank">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/jieshou.gif"></a>
                    </td>
                    <td align="left">
                        <a href="javascript:;" onclick="CloseLeft();">
                            <img border="0" src="<%= Domain.ServerComponents %>/images/seniorshop/hulue.gif"></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--浮动咨询结束--%>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery.floating.js") %>"></script>

    <script type="text/javascript">
     function ClickCalendar(TourId,obj,areaType) {
 
            SingleCalendar.config.isLogin ="<%=this.IsLogin  %>"; //是否登陆
            SingleCalendar.config.stringPort ="<%=EyouSoft.Common.Domain.UserPublicCenter %>";//配置
            SingleCalendar.initCalendar({
              currentDate:<%=thisDate %>,//当时月
                firstMonthDate: <%=thisDate %>,//当时月
                srcElement: obj,
                areatype:areaType,//当前模板团线路区域类型 
                TourId:TourId,//模板团ID
                AddOrder:AddOrder
            });
            
            return false;
        }
         function AddOrder(TourId) {
            if ("<%=this.IsLogin %>" == "True") {
                var strParms = { TourId: TourId }
                Boxy.iframeDialog({ title: "预定", iframeUrl: "<%=EyouSoft.Common.Domain.UserBackCenter %>/TeamService/RouteOrder.aspx", width: "800", height: GetAddOrderHeight(), draggable: true, data: strParms });
            } else {
                //登录
                window.location.href ='<%= EyouSoft.Common.Domain.UserPublicCenter %>/Register/Login.aspx?isShow=1&CityId=<%=CityId %>&returnurl='+escape('<%= EyouSoft.Common.Domain.SeniorOnlineShop %><%=Request.ServerVariables["SCRIPT_NAME"]%>?TourId=<%=Request.QueryString["TourId"] %>&<%=Request.QueryString%>');
            }
        }
         function mouseovertr(o) 
         {
            o.style.backgroundColor="#FFF6C7";
             //o.style.cursor="hand";
        }
         function mouseouttr(o) {
             o.style.backgroundColor=""
         }
         function initialize() {
  var title="Map";
  var map;
  var infowindow = new google.maps.InfoWindow();//地图内置信息窗口
  var marker;
//var lat=30.28174;
//var lng=120.13194;
var lat='<%=weidu %>';
var lng='<%=jingdu %>';
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
    var oMapContainer = document.getElementById("GMap");//地图容器
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
            google.maps.event.addListener(marker, 'click', function() {
              infowindow.open(map,marker);
            });
            }
        }
     });
  }
        $(function(){
            initialize();
            $("#divZX").easydrag();
            $("#divZX").floating({ position: "left", top: 100, left: 10, width: 400 });
             $(".huiyuanzq a").click(function() {
            if ("<%=IsLogin %>" == "False") {
                Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                return false;
            }
        });
        });
    </script>
                    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
</asp:Content>
