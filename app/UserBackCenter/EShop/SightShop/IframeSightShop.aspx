<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IframeSightShop.aspx.cs"
    Inherits="UserBackCenter.EShop.SightShop.IframeSightShop" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.Function" %>
<%@ Register Src="GoogleMapControl.ascx" TagName="GoogleMapControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>景区高级网店后台</title>
    <link href="<%=CssManage.GetCssFilePath("sightshop.style") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("sightshop.ext-all") %>" type="text/css"
        rel="stylesheet">
    <link href="<%=CssManage.GetCssFilePath("sightshop.bodyframe") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("sightshop.style3") %>" rel="stylesheet"
        type="text/css" />
    <!-- CSS for Drop Down Tabs Menu #1 -->
    <link href="<%=CssManage.GetCssFilePath("sightshop.bluetabs") %>" rel="stylesheet"
        type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="wrap">
        <div class="boxgrid captionfull" id="boxgrid1">
            <img id="imgBanner" runat="server" style="width: 972px; height: 233px;" />
            <div class="cover boxcaption">
                <a href="javascript:void(0)">
                    <h3 style="height: 100px; padding-top: 80px; cursor: pointer">
                        点击可上传背景图片</h3>
                </a>
            </div>
        </div>
        <!--menu start-->
        <div class="menu">
            <ul>
                <li><a href="IframeSightShop.aspx" class="menu-on">首页</a></li>
                <li><a href="javascript:void(0)" id="sight0">景区门票</a></li>
                <li><a href="javascript:void(0)" id="sight1">景区动态</a></li>
                <li><a href="javascript:void(0)" id="sight2">景区美图</a></li>
                <%--<li><a href="javascript:void(0)" id="sight3">门票政策</a></li>--%>
                <li><a href="javascript:void(0)" id="sight4">景区攻略</a></li>
                <li><a href="javascript:void(0)" id="sight5">景区导游</a></li>
                <li><a href="javascript:void(0)" id="aboutus">联系我们</a></li>
            </ul>
        </div>
        <!--menu end-->
        <!--main start-->
        <div class="main">
            <!--sidebar start-->
            <div class="sidebar">
                <div class="sidebar_1">
                    <p class="more">
                        <span>联系我们</span></p>
                    <div class="sidebar_1_text" style="height: 212px;">
                        <div class="boxgrid6 infocfull6" id="boxgrid2" style="height: 100px;">
                            <img id="imgLogo" runat="server" class="jqlog" style="width: 218px; height: 88px;">
                            <div class="cover boxcaption6">
                                <a href="javascript:void(0)">
                                    <h3 style="height: 50px; padding-top: 40px; cursor: pointer; text-align: center;">
                                        点击添加公司LOGO</h3>
                                </a>
                            </div>
                        </div>
                        <ul>
                            <div class="boxgrid4 infocfull4" id="boxgrid3" style="height: 115px;">
                                <li>联系人：<asp:Literal ID="ltrContactName" runat="server"></asp:Literal></li>
                                <li>电话：<asp:Literal ID="ltrTel" runat="server"></asp:Literal></li>
                                <li>传真：<asp:Literal ID="ltrFax" runat="server"></asp:Literal></li>
                                <li>网址：<asp:Literal ID="ltrWebSite" runat="server"></asp:Literal></li>
                                <li>地址：<asp:Literal ID="ltrAdress" runat="server"></asp:Literal></li>
                                <li>
                                    <asp:Literal ID="ltrMQ" runat="server"></asp:Literal>
                                    <label>
                                        <asp:Literal ID="ltrQQ" runat="server" Text="在线客服QQ"></asp:Literal></label>
                                </li>
                                <div class="cover boxcaption4">
                                    <a href="javascript:void(0)">
                                        <h3 style="height: 50px; padding-top: 30px; cursor: pointer; text-align: center;">
                                            点击添加公司档案</h3>
                                    </a>
                                </div>
                            </div>
                        </ul>
                    </div>
                </div>
                <div class="boxgrid6 infocfull6" id="boxgrid4" style="width: 250px; margin-top: 10px;">
                    <div class="sidebar_1">
                        <p class="more">
                            <span>景区动态</span></p>
                        <div class="sidebar_1_text">
                            <ul>
                                <asp:Repeater ID="rptTripGuideDT" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <%# StringValidate.SafeHtmlEndcode(Utils.GetText((DataBinder.Eval(Container.DataItem, "Title").ToString()), 15, true))%></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="cover boxcaption6" style="width: 250px;">
                            <a href="javascript:void(0)">
                                <h3 style="height: 100px; padding-top: 50px; cursor: pointer; text-align: center;">
                                    点击添加景区动态</h3>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="boxgrid4 infocfull4" style="width: 250px; height: 228px; margin-top: 10px;"
                    id="boxgrid5">
                    <uc1:GoogleMapControl ID="GoogleMapControl1" runat="server" />
                    <div class="cover boxcaption4" style="height: 226px; width: 250px;">
                        <a href="javascript:void(0)">
                            <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                点击设置地图</h3>
                        </a>
                    </div>
                </div>
                <div class="boxgrid6 infocfull6" id="boxgrid6" style="width: 250px;">
                    <div class="sidebar_1">
                        <p class="more">
                            <span>景区线路</span></p>
                        <div class="sidebar_1_text" style="height: 120px;">
                            <ul>
                                <asp:Repeater ID="rptTripGuideXL" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <%# StringValidate.SafeHtmlEndcode(Utils.GetText((DataBinder.Eval(Container.DataItem, "Title").ToString()), 15, true))%></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                        <div class="cover boxcaption6" style="width: 250px;">
                            <a href="javascript:void(0)">
                                <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                    点击添加景区线路</h3>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--sidebar end-->
        <!--sidebar02 start-->
        <div class="sidebar02">
            <!--sidebar02_1-->
            <div class="sidebar02_1">
                <div class="sidebar02_1_L">
                    <div class="boxpadding">
                        <div id="Slide">
                            <div class="boxgrid2 infocfull" id="boxgrid7">
                                <img id="focpic" runat="server" />
                                <div class="cover boxcaption2">
                                    <a href="javascript:void(0)">
                                        <h3 style="height: 150px; padding-top: 120px; cursor: pointer; text-align: center;">
                                            点击可上传轮换图片息</h3>
                                    </a>
                                </div>
                            </div>
                            <asp:Literal ID="ltrAdvInfo" runat="server"></asp:Literal>
                        </div>
                        <div class="clearboth">
                        </div>
                    </div>
                </div>
                <div class="sidebar02_1_R" style="height: 296px;">
                    <div style="width: 95%; margin: 0px auto; margin-top: 5px; line-height: 22px;">
                        <h1>
                            <font class="C_orange">
                                <asp:Literal runat="server" ID="ltrAboutUsTitle"></asp:Literal>
                            </font>
                        </h1>
                        <p class="Scenic_spot" style="float: left;">
                            <asp:Literal runat="server" ID="ltrAboutUs"></asp:Literal></p>
                    </div>
                </div>
                <div class="clearboth">
                </div>
            </div>
            <!--sidebar02_2-->
            <div class="sidebar02_2">
                <ul class="subnav">
                    <li><a href="javascript:void(0);" class="tab-five-on" id="five1" onmousemove="settab('five',1)"
                        rel="<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>">
                        攻略</a><input type="hidden" id="hidTradeGuid4" name="hidTradeGuid4" /></li>
                    <li><a href="javascript:void(0);" id="five2" onmousemove="settab('five',2)" rel="<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区美食 %>">
                        美食</a><input type="hidden" id="hidTradeGuid7" name="hidTradeGuid7" /></li>
                    <li><a href="javascript:void(0);" id="five3" onmousemove="settab('five',3)" rel="<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区住宿 %>">
                        住宿</a><input type="hidden" id="hidTradeGuid10" name="hidTradeGuid10" /></li>
                    <li><a href="javascript:void(0);" id="five4" onmousemove="settab('five',4)" rel="<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区交通 %>">
                        交通</a><input type="hidden" id="hidTradeGuid6" name="hidTradeGuid6" /></li>
                    <li><a href="javascript:void(0);" id="five5" onmousemove="settab('five',5)" rel="<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区购物 %>">
                        购物</a><input type="hidden" id="hidTradeGuid5" name="hidTradeGuid5" /></li>
                    <li style="background: #FFF6C7; border: 1px solid #FF8624; color: #339933; float: left;
                        height: 21px; line-height: 22px; width: 200px; cursor: pointer;" id="boxy_guid1">
                        <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/gan.gif" />
                        请点击！<strong id="li_text">【添加景区攻略】</strong></li>
                    <div class="clearboth">
                    </div>
                </ul>
                <div class="clearboth">
                </div>
                <div class="sidebar02_2Content" id="con_five_1" style="height: 140px;">
                </div>
            </div>
            <div style="width: 715px; height: 74px; margin-bottom: -5px; float: left;">
                <div class="boxgrid6 infocfull6" style="width: 710px; height: 74px;" id="boxgrid8">
                    <img id="imgAdv" runat="server" class="add002" height="74" width="710">
                    <div class="cover boxcaption6" style="height: 74px; width: 710px;">
                        <a href="javascript:void(0)">
                            <h3 style="height: 45px; padding-top: 30px; cursor: pointer; text-align: center;">
                                点击上传广告图片</h3>
                        </a>
                    </div>
                </div>
            </div>
            <div class="sidebar02_2" style="float: left;">
                <p class="more more02">
                </p>
                <ul class="ScenicPhoto">
                    <asp:DataList ID="rptTripGuideMT" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                        HorizontalAlign="Left">
                        <ItemTemplate>
                            <li>
                                <img src="<%# Utils.GetLineShopImgPath(DataBinder.Eval(Container.DataItem, "Address").ToString(),5)%>"
                                    style="width: 147px; height: 102px;" /><br>
                                <%# Utils.GetText(DataBinder.Eval(Container.DataItem,"Description").ToString(),8) %></li>
                        </ItemTemplate>
                    </asp:DataList>
                    <div class="clearboth">
                    </div>
                </ul>
            </div>
        </div>
        <div class="clearboth">
        </div>
        <!--sidebar02 end-->
        <div class="boxgrid3 infocfull3" id="boxgrid9" style="margin-top: 10px;">
            <div class="friendlink">
                <p>
                    <asp:Literal ID="ltrFriendLink" runat="server"></asp:Literal>
                </p>
            </div>
            <div class="cover boxcaption3">
                <a href="javascript:void(0)">
                    <h3 style="height: 30px; padding-top: 20px; cursor: pointer; text-align: center;">
                        点击添加友情链接</h3>
                </a>
            </div>
        </div>
        <div class="boxgrid3 infocfull3" id="boxgrid10" style="margin-top: -10px;">
            <div class="copyright">
                <p>
                    <asp:Literal ID="ltrCopyright" runat="server"></asp:Literal></p>
                <div class="cover boxcaption3">
                    <a href="javascript:void(0)">
                        <h3 style="height: 30px; padding-top: 10px; cursor: pointer; text-align: center;">
                            点击添加版权信息</h3>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <!--main end-->

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>

    <script type="text/javascript">
    function boxymethods(pageurl,strtitle,width,height)
    {
         Boxy.iframeDialog({title:strtitle, iframeUrl:pageurl,width:width,height:height,draggable:true,data:{one:'1',two:'2',callBack:'cancel'}});
         return false;
    }
    function boxymethodssmall(pageurl,strtitle)
    {
         Boxy.iframeDialog({title:strtitle, iframeUrl:pageurl,width:650,height:180,draggable:true,data:{one:'1',two:'2',callBack:'cancel'}});
         return false;
    }
     
    function settab(name,index)
    {
        var ele = $("#"+name+index).addClass("tab-five-on");
//        $("#li_text").html("【添加"+ $(".subnav .tab-five-on").attr("guidtype") +"】");
        $("a[id^="+ name +"][id!="+ name + index +"]").not(index-1).removeClass("tab-five-on");
        var ele = $("#hidTradeGuid"+ $(".subnav .tab-five-on").attr("rel"));
        if(ele.val() == '' || ele.val() == null || ele.val() == undefined)
        {
            GetData($(".subnav .tab-five-on").attr("rel"),$(".sidebar02_2Content"));
        }else{
            $(".sidebar02_2Content").html(ele.val());
        }
    }
    
    function GetData(type,ele)
    {
        $.ajax({
            url:'/eshop/sightshop/iframesightshop.aspx?flag=get&typeid='+ type +'&rnd='+ Math.random(),
            cache:false,
            async:false,
            success:function(html){
                if(html != '')
                {
                    ele.html(html);
                    $("#hidTradeGuid"+type).val(html);
                }
            } 
        });        
    }
     
    $(function(){
        
        GetData(<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>,$(".sidebar02_2Content"));
        
        // 背景图片
        $('#boxgrid1').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'200px'},{queue:false,duration:100});
		});
		
		// LOGO图片
		$('#boxgrid2').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'80px'},{queue:false,duration:100});
		});
		
		// 公司档案
		$('#boxgrid3').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'90px'},{queue:false,duration:100});
		});
		
		// 景区动态
		$('#boxgrid4').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'110px'},{queue:false,duration:100});
		});
		
		// 设置地图
		$('#boxgrid5').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'198px'},{queue:false,duration:100});
		});
		
		// 景区线路
		$('#boxgrid6').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'110px'},{queue:false,duration:100});
		});
		
		// 轮换图片
		$('#boxgrid7').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'196px'},{queue:false,duration:100});
		});
		
		// 广告图片
		$('#boxgrid8').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'44px'},{queue:false,duration:100});
		});
		
		// 友情链接
		$('#boxgrid9').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'30px'},{queue:false,duration:100});
		});
		
		// 版权信息
		$('#boxgrid10').hover(function(){
			$(".cover", this).stop().animate({top:'0px'},{queue:false,duration:100});
		}, function() {
			$(".cover", this).stop().animate({top:'50px'},{queue:false,duration:100});
		});		
		
		$("#sight1").click(function(){return boxymethods('/EShop/SetTravelGuidList.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区动态 %>','景区动态',760,560);});
		
		$("#sight2").click(function()
		{
		    var url = "/ScenicManage/MyScenice.aspx";
            window.parent.topTab.open(url, "我的景区", { isRefresh: false });
            return false;
		});
		
		$("#sight3").click(function(){return boxymethods('/eshop/SightShop/SetVotePolicy.aspx','门票政策',730,470);});
		
		$("#sight4").click(function(){return boxymethods('/EShop/SetTravelGuidList.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>','景区攻略',760,560);});
		
		$("#sight5").click(function(){return boxymethods('/EShop/SetTravelGuidList.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区导游 %>','景区导游',760,560);});
		
		$("#boxgrid4").click(function(){return boxymethods('/EShop/SetTravelGuid.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区动态 %>','景区动态',760,560);});
		
		$("#boxgrid6").click(function(){return boxymethods('/EShop/SetTravelGuid.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区线路 %>','景区线路',760,560);});
		
		$("#boxy_guid1").click(function(){return boxymethods('/EShop/SetTravelGuid.aspx?GuideType=1&TypeId=<%=(int)EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.景区攻略 %>','景区攻略',760,560)});
		
		$("#boxgrid2").click(function(){return boxymethodssmall('/eshop/sightshop/setlogo.aspx','上传LOGO');});
		
		$("#boxgrid8").click(function(){return boxymethodssmall('/eshop/sightshop/setadvinfo.aspx','上传广告');});
		
		$("#boxgrid5").click(function(){
		    window.open('/eshop/SightShop/SetGoogleMap.aspx','_self');
		    return false;
		});
		
		$("#boxgrid7").click(function(){return boxymethods('/eshop/rotationpicmanage.aspx','上传轮换图片',730,470);});
		
		$("#boxgrid1").click(function(){return boxymethodssmall('/eshop/setlogopic.aspx','上传背景图片');});
		
		$("#sight0").click(function()
		{
		    var url = "/ScenicManage/MyScenice.aspx";
            window.parent.topTab.open(url, "我的景区", { isRefresh: false });
            return false;
		});
		
		$("#boxgrid3,#aboutus").click(function(){return boxymethods('/eshop/CompanyProfile.aspx','联系我们',730,250);});
		
		$("#boxgrid9").click(function(){return boxymethods('/eshop/SetFriendLinkManage.aspx','友情链接管理',730,470);});
		
		$("#boxgrid10").click(function(){return boxymethods('/eshop/SetCopyRight.aspx','版权信息',730,470);});
    });
    </script>

    </form>
</body>
</html>
