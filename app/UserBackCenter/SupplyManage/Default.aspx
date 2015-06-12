<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.SupplyManage.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplyInformation/UserControl/ExchangeListControl.ascx" TagName="exchangelist"
    TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/WebHeader.ascx" TagName="webHeader" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrol/WebFooter.ascx" TagName="webFooter" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>供应商后台_同业114</title>
    <style type="text/css">
        body
        {
        	visibility:visible;        	        	
        }
        .topbar, .divtopfull, .divmainfull, .margin10
        {
            position: absolute;
            top: -10000px;
            left: -10000px;
        }
        #top
        {
            background-image: url(<%=ImageServerUrl %>/images/UserPublicCenter/sitebarbj.gif);
        }
        #top
        {
            background-repeat: repeat-x;
            height: 26px;
            text-align: left;
        }
        .topda
        {
            width: 970px;
            margin: 0 auto;
            padding: 0;
            padding-top: 5px;
        }
        .topda .daleft
        {
            width: 460px;
            float: left;
        }
        .topda .daright
        {
            width: 510px;
            float: left;
            text-align: right;
        }
        .topda .daright a
        {
            color: #333;
        }
    </style>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" runat="server" id="tbIsCheck"
        visible="false" cellspacing="0" style="border: 2px solid #EF9739; background: #FFF8E2">
        <tr>
            <td style="padding: 5px;" align="center">
                <img src="<%=ImageServerUrl %>/images/woring.gif" width="15" height="15" />&nbsp; 当前您的帐号未审核，您暂时还不能发布供求和维护网店！ 我们会在24小时内通知您审核结果！或请致电0571-56884627，QQ：1397604721。
            </td>
        </tr>
    </table>
    <div id="loadingItem" style="position: absolute; top: 8em; left: 2em">
        <br />
        <font size="+2">正在加载...</font><br />
    </div>
    <noscript>
        <style type="text/css">
            div
            {
                display: none;
            }
            table
            {
                display: none;
            }
            #noscript
            {
                padding: 3em;
                font-size: 130%;
            }
        </style>
        <p id="noscript">
            要使用当前网站平台，必须启用 JavaScript。不过，JavaScript 似乎已被禁用或者您的浏览器不支持 JavaScript。要使用网站平台，请更改您的浏览器选项，启用
            JavaScript，然后 <a href="/Default.aspx">重试</a>。</p>
    </noscript>
    <div id="topmessage" class="topmessage">
        <span>正在载入...</span></div>
    <cc1:webHeader ID="header" runat="server" />
    </div>
    <div class="divtopfull">
        <div class="logo">
            <img id="imgLogo_supply_default" src="<%=LogoImagePath %>" width="130" height="50" /></div>
        <div class="headright">
            <span>
                <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal>&nbsp;</span>
            <div class="option" id="optionTab">
                <div class="option-tab-on-index">
                    <span><a href="javascript:;">首页</a></span></div>
            </div>
        </div>
    </div>
    <!--主体-->
    <div class="divmainfull">
        <div class="leftmenu">
            <div class="topopeara">
                <b>操作台</b></div>
            <div>
                <a href="javascript:void(0)" class="gongnengzu">公司信息</a></div>
            <div class="gongnengxx">
            </div>
            <div class="gongneng">
                <%if (IsHighShopSupply)
                  {%>
                <a href="/eshop/sightshop/sightshopdefault.aspx" rel="toptab" class="commbar">我的网店</a>
                <a href="/supplymanage/myowenershop.aspx" class="commbar" rel="toptab">单位信息</a>
                <%}
                  else
                  { %>
                <a href="/supplymanage/freeshop.aspx" rel="toptab" class="commbar">公司信息</a>
                <%} %>
                <div style="height: 7px; clear: both">
                </div>
            </div>
            <%--<div><a href="/eshop/sightshop/sightshopdefault.aspx" rel="toptab">景区高级网店测试</a></div>--%>
 <div style="clear: both;">
            </div>
             <div id="divHotelCenter">
                <div class="bigclassbar" id="div1">
                    <a href="javascript:void(0);">酒店订单管理</a>
                </div>
                <div class="gongnengxx" style="display: none;">
                </div>
                <div class="gongneng" style="display: none;">
                    <a href="/hotelcenter/hotelordermanage/HotelOrderList.aspx?IsFirstKey=1" id="url_orderSearch" class="gongnengej" rel="toptab">订单查询</a>
                    <a href="/hotelcenter/hotelordermanage/hotelvisitormanage.aspx" class="gongnengej" rel="toptab">常旅客管理</a>
                    <a href="/hotelcenter/hotelordermanage/SettlementAccount.aspx" class="gongnengej" rel="toptab">结算账户设置</a>
                    <a href="/hotelcenter/hotelordermanage/teamonlinesubmit.aspx" class="gongnengej" rel="toptab">港澳/团队申请
</a>
                </div>
            </div>
            <div>
                <a href="/supplyinformation/addsupplyinfo.aspx" class="bigclassbar" rel="toptab">供求信息</a></div>
            <div class="gongnengzu" style="cursor: pointer;">
                常用工具</div>
            <div class="gongnengxx">
            </div>
            <div class="gongneng">
                <a href="/Tickets/Default.aspx" class="gongnengjp" rel="toptab">机票查询</a> <a href="/smscenter/sendsms.aspx"
                    class="gongnengup" rel="toptab">短信中心</a> <a href="/Memorandum/MemorandumCalendar.aspx"
                        rel="toptab" class="gongnengbw" rel="toptab">备忘录</a>
                <div style="height: 7px; clear: both">
                </div>
            </div>
            <div class="gongneng" style="height: 245px;">
                &nbsp;
            </div>
        </div>
        <div class="right" id="optionPanel">
            <div class="option-panel-on">
                <table width="99%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="margin: 0px; padding: 0px; height:72px;">
                            <div class="rigtop" style="margin: 0px; padding: 0px;">
                                <div class="rigtopl" style="width:auto;">
                                    <div style="margin-top: 0px; margin-bottom: 5px;">
                                        您好，<asp:Label ID="lblLoginUser" runat="server"></asp:Label>
                                        欢迎登录同业114。<br />
                                        目前平台已有<strong class="chengse"><asp:Label ID="lblRouteAgencyCount" runat="server"></asp:Label></strong>家旅行社，
                                        <strong class="chengse">
                                            <asp:Label ID="lblHotelCount" runat="server"></asp:Label></strong>家酒店，<strong class="chengse"><asp:Label
                                                ID="lblSightCount" runat="server"></asp:Label></strong>家景区， <strong class="chengse">
                                                    <asp:Label ID="lblCarCount" runat="server"></asp:Label></strong>家车队<span style="display:none;">，<strong class="chengse"><asp:Label
                                                        ID="lblShoppingCount" runat="server"></asp:Label></strong>家购物点</span>加盟</div>
                                </div>
                               <div class="rigtopr" style="width: 400px; height: 62px; display:none;">
                                    <iframe src="http://m.weather.com.cn/m/pn11/weather.htm?id=101210101T " width="100%"
                                        marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0" allowtransparency="true"
                                        scrolling="no"></iframe>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>                           
                            <table width="100%" border="0" cellspacing="0" cellpadding="10" style="vertical-align: top;">
                                <%if (!IsHighShopSupply){ %>
                                <tr>
                                    <td width="25%" align="center">
                                        <a href="<%=WebSiteUrl %>" target="_blank">
                                            <img src="<%=ImageServerUrl%>/images/freeshopqt.gif" width="210" height="40" border="0" /></a>
                                    </td>
                                    <td width="24%" align="center">

                                        <a href="/supplymanage/highapplication.aspx" id="a_Supply_Application">
                                            <img src="<%=ImageServerUrl%>/images/sqgjwd.gif" width="211" height="40" border="0" /></a>                                   
                                    </td>
                                    <td width="51%">
                                        &nbsp;
                                    </td>
                                </tr>
                                 <%}else{ %>
                                <tr>
                                    <td width="25%" align="center">
                                        <a href="<%=WebSiteUrl %>" target="_blank">
                                            <img src="<%=ImageServerUrl%>/images/manageroption1.gif"  border="0" /></a>
                                    </td>
                                    <td width="24%" align="center">
                                        <a href="/eshop/sightshop/sightshopdefault.aspx" id="a_Supply_newlist">
                                            <img src="<%=ImageServerUrl%>/images/sightshop/bjwd.gif"  border="0" /></a>                                   
                                    </td>
                                    <td width="51%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <%} %>
                                <tr>
                                    <td colspan="3" valign="top">
                                        <div class="tixing">
                                            <uc1:exchangelist ID="exchangelist" runat="server" />
                                        </div>
                                        <table width="628" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                                            <tr align="left">
                                                <asp:DataList ID="dlAdv" runat="server" HorizontalAlign="left" RepeatColumns="2">
                                                    <ItemTemplate>
                                                        <td width="10">
                                                        </td>
                                                        <td>
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="310px" align="left">
                                                                        <a href="<%# EyouSoft.Common.Utils.GetWordAdvLinkUrl(0,Convert.ToInt32(DataBinder.Eval(Container.DataItem,"ID")),0,362) %>"
                                                                            target="_blank">•&nbsp;&nbsp;<%# EyouSoft.Common.Utils.GetText(Convert.ToString(DataBinder.Eval(Container.DataItem, "AfficheTitle")),20,true)%></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <uc1:webFooter ID="footer" runat="server" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dhtmlHistory") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">        
    function historyChangeHandler(hash,historyData){
        var url = decodeURIComponent(hash).replace("page:","");
        var index = topTab.getIndexByUrl(url);
        if(index!=undefined){
            topTab.select(index);
        }else{
            if(historyData==null||historyData==undefined){
                var currentLink = $(".leftmenu").find("a[href*='"+url+"']");
                if(currentLink.length>0){
                    historyData = currentLink.eq(0).text();
                }
            }
            if(historyData!=null && historyData!=undefined){
                 topTab.add(url,historyData);
            }
        }
    }
    var topTab,currentSwfuploadInstances=[],LoginUrl="<%=EyouSoft.Security.Membership.UserProvider.Url_Login %>";
    var commonTourModuleData={
        _data:[],
        add:function(obj){
            this._data[obj.ContainerID] = obj;
        },
        get:function(id){
            return this._data[id];
        }
    };
    function renderBody(){
        topTab = new TopTab({
            onSelect:function(tabObj){
                 if(tabObj.index==0){
                    dhtmlHistory.add("",tabObj.title);
                 }else{
                    dhtmlHistory.add(encodeURIComponent("page:"+tabObj.url),tabObj.title);
                 }
            },
            onAdd:function(tabObj){
                //alert(tabObj.index+tabObj.url+tabObj.title);return;
                if(tabObj.index==0){
                    dhtmlHistory.add("",tabObj.title);
                 }else{
                    dhtmlHistory.add(encodeURIComponent("page:"+tabObj.url),tabObj.title);
                 }
            }
        });
        
        dhtmlHistory.initialize();
       
       // add ourselves as a DHTML History listener
       dhtmlHistory.addListener(historyChangeHandler);
       
       //currentHash
       var currentHash = decodeURIComponent(dhtmlHistory.getCurrentLocation()).replace("page:","");
        var currentLink = $(".leftmenu").find("a[href*='"+currentHash+"']");
        if(currentHash!=""&&currentLink.length>0){
            var b = topTab.open(currentHash,currentLink.eq(0).text());
        }else{
        
            //find in child tab.
            var parentUrl =ChildTab.getParentUrl(currentHash);
            var parentUrlLink = $(".leftmenu").find("a[href*='"+parentUrl+"']");
            if(parentUrl!=""){
                topTab.open(parentUrl,parentUrlLink.eq(0).text(),{desUrl:currentHash});
            }
        }
        
        var tab = topTab;
        $("a[href][rel='toptab']").click(function(){
            var text = $(this).text();
            tabOpen(this,tab,true,text);
            return false;
        });
        $("#a_default_myshop").click(function(){
            tabOpen(this,tab,false,"我的网店");
            return false;
        });
        $("#a_Supply_newlist").click(function(){
            tabOpen(this,tab,false,"我的网店");
            return false;
        });
        $("#a_Supply_Application").click(function(){
            tabOpen(this,tab,false,"高级网店申请");
            return false;
        });
        $("#loadingItem").html("").css({top:"-1000px",left:"-1000px"});
        $(".topbar,.divtopfull,.divmainfull,.margin10").css({
            position:"static",top:"0px",left:"0px"
        });
        if($.browser.mozilla||$.browser.safari){
            createStyleRule("body",'color:#333;font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif; text-align: center; background:#fff; margin:0px;');
            createStyleRule("input,textarea,select",'font-size:12px;font-family:"宋体",Arial, Helvetica, sans-serif;COLOR: #333;');
        }
    };
    function tabOpen(obj,tab,tabrefresh,text){
        var a = $(obj);
        var href = a.attr("href");
        var hash = href.replace(/^.*#/, '');
        if(href.indexOf("#")==-1){
            var b = tab.open(href,text,{isRefresh:tabrefresh});
        }
        return false;
    }
    renderBody(); 
     
    function Hide_Show_Div(obj) {
            if ($(obj).next().next().css("display") == "none") {
                $(obj).next().css("display", "");
                $(obj).next().next().css("display", "");
            } else {
                $(obj).next().css("display", "none");
                $(obj).next().next().css("display", "none");
            }
        }   

    $(".bigclassbar2,.bigclassbar,.gongnengzu").click(function() {
            Hide_Show_Div(this);
        });       
    </script>

    </form>
</body>
</html>
