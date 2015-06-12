<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourDetail.aspx.cs" Inherits="SeniorOnlineShop.seniorshop.TourDetail"
    MasterPageFile="~/master/SeniorShop.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <style>
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: white;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .100;
            filter: alpha(opacity=88);
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 650px;
            left: 25%;
            width: 600px;
            height: 120px;
            text-align: right;
            padding: 5px;
            border: 2px solid #FFC267;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        #divAllTourList table
        {
            border: solid 1px #E3E3E3;
            border-collapse: collapse;
        }
        #divAllTourList tr
        {
            border: solid 1px #E3E3E3;
        }
        #divAllTourList td
        {
            border: solid 1px #E3E3E3;
            background-color: White;
        }
        .planinfo
        {
            margin: 15px;
            text-align: center;
        }
        .trl_d_anpai2
        {
            background: #1959aa url(../images/line3.gif) repeat-x bottom;
            height: 17px;
            margin-top: 10px;
            padding-top: 8px;
            padding-left: 5px;
            color: #fff;
            clear: both;
        }
    </style>
    <div class="rightn">
        <div class="newsdetail2" style="margin: 0px;">
            <div class="xianluhangcloer">
                <div id="pngtu" class="IE6png">
                    <div class="xianluhangleft">
                        <h1>
                            <asp:Label ID="labRouteName" runat="server"></asp:Label></h1>
                    </div>
                    <div class="xianluhangright">
                        <span class="zhuangtai">
                            <asp:Label ID="labTourSpreadState" runat="server"></asp:Label></span>&nbsp;&nbsp;
                        <%=PrintUrl %>
                    </div>
                </div>
            </div>
            <div class="trl_xin">
                <div class="trl_imgleft">
                    <asp:Image ID="RouteImgUrl" runat="server" Width="295" Height="195" />
                </div>
                <div class="trl_right">
                    <ul>
                        <li class="diyigeli">市 场 价： <span class="huang16">
                            <asp:Literal ID="LitRetailPrices" runat="server"></asp:Literal></span>
                            <%=MQUrlHtml %>
                        </li>
                        <li id="liDjShowOrHiden" runat="server">订金：成人 <span class="STYLE1">
                            <asp:Literal ID="LitAdultPriceDj" runat="server"></asp:Literal></span> 儿童 <span
                                class="STYLE1">
                                <asp:Literal ID="litChildrenPriceDj" runat="server"></asp:Literal></span>
                        
                        
                        
                        </li>
                        <li>旅游主题：<%= themeList.ToString() %>
                        </li>
                        <li>旅程天数：<strong><span class="ff0000"><asp:Literal ID="LitDays" runat="server"></asp:Literal></span></strong>天
                            <strong><span class="ff0000">
                                <asp:Literal ID="Litlateness" runat="server"></asp:Literal></span> </strong>
                            晚 请提前<strong> <span class="ff0000">
                                <asp:Literal ID="litSigleUp" runat="server"></asp:Literal></span></strong>天报名</li>
                        <li>往返说明：<asp:Literal ID="litStartTriffic" runat="server"></asp:Literal>&nbsp <span
                            style="margin-left: 55px;">
                            <asp:Literal ID="litEndTriffic" runat="server"></asp:Literal></span></li>
                        <%=BrowSeAreaHtml.ToString() %><%=qianzhengHtml.ToString()%>
                        <li class="tuan">
                            <%=IsCerTainHtml%></li>
                    </ul>
                </div>
            </div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maint10" style="float: left;">
                <tr>
                    <td style="border: 2px solid #FFC267; padding: 15px;">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="planInfo" runat="server">
                            <tr>
                                <td colspan="2" class="px14" style="border-bottom: 1px dashed #ccc; line-height: 30px;">
                                    <strong>在线预订:</strong>
                                    <label>
                                        <asp:DropDownList ID="DropPowerList" runat="server">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hidTourID" runat="server" />
                                    </label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;成人
                                    <input type="text" style="width: 50px;" id="txtAdultNumber" runat="server" />
                                    人； 儿童
                                    <input type="text" id="txtChildrenNumber" style="width: 50px;" runat="server" />
                                    人；
                                </td>
                            </tr>
                            <tr>
                                <td width="56%" class="px14" style="border-bottom: 1px dashed #ccc; line-height: 20px;">
                                    <span style="float: left;">联系人：
                                        <input type="text" id="txtContectName" style="width: 50px;" runat="server" />
                                        联系电话:
                                        <input id="txtContectPhone" type="text" size="20" runat="server" />
                                        &nbsp;</span>
                                </td>
                                <td width="44%" valign="middle" class="px14" style="border-bottom: 1px dashed #ccc;
                                    padding: 5px 0; line-height: 20px;">
                                    <span style="float: left; padding-right: 5px; cursor: pointer;" id="spanOrderTour"
                                        runat="server">
                                        <img src="<%=ImageServerPath %>/images/sightshop/route_order_btn1.gif" width="110"
                                            height="31" class="SanKeYD" />
                                    </span>
                                    <asp:Panel ID="panSingleTour" runat="server">
                                        <span style="float: left; cursor: pointer;" id="spanSingleTour" runat="server">
                                            <img src="<%=ImageServerPath %>/images/sightshop/xinan_05.gif" width="110" height="31"
                                                alt="单团预订" class="SingleTourYD" />
                                        </span>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panshowMsg" runat="server" Visible="false" CssClass="planinfo">
                            暂无计划信息！</asp:Panel>
                    </td>
                </tr>
            </table>
            <a name="t1"></a>
            <div class="xianlutese">
                <div class="xianlutese2">
                    <span class="mudizi">线路特色</span><br />
                    <asp:Literal ID="litHotRoute" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="bar-line">
                <div class="bar-line-title">
                    出发日期与价格</div>
            </div>
            <div id="divAllTourList">
            </div>
            <div class="bar-line">
                <div class="bar-line-title">
                    行程安排</div>
            </div>
            <asp:Literal ID="LiteralTourPlan" runat="server"></asp:Literal>
            <div class="trl_d_anpai">
                <strong>费用包含：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Label ID="lblServiceStandard" runat="server"></asp:Label>
            </div>
            <div class="trl_d_anpai">
                <strong>费用不包含：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Literal ID="lblServiceStandardNo" runat="server"></asp:Literal>
            </div>
            <div class="trl_d_anpai">
                <strong>儿童及其他安排：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Label ID="lblAdultAndOtherPlan" runat="server"></asp:Label>
            </div>
            <div class="trl_d_anpai">
                <strong>赠送项目：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Literal ID="LitPresentedProject" runat="server"></asp:Literal>
            </div>
            <div class="trl_d_anpai">
                <strong>自费项目：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Literal ID="LitCopaymentsProject" runat="server"></asp:Literal>
            </div>
            <div class="trl_d_anpai">
                <strong>购物点：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Literal ID="LitShoppingPlace" runat="server"></asp:Literal>
            </div>
            <div class="trl_d_anpai">
                <strong>备注信息：</strong>
            </div>
            <div style="padding: 10px; line-height: 18px;">
                <asp:Literal ID="LitRemarkInfo" runat="server"></asp:Literal>
            </div>
            <div style="font-weight: bold; font-size: 14px; padding-left: 10px; padding-top: 10px;">
                同业销售须知 <a href="javascript:void(0);" class="Userlogin">登录后查看</a>
            </div>
            <asp:Panel ID="panIsLogin" runat="server">
                <div class="trl_d_anpai2">
                    <strong>同业销售须知：</strong>
                </div>
                <div style="padding: 10px; line-height: 18px;">
                    <asp:Literal ID="litSalesNotice" runat="server"></asp:Literal>
                </div>
            </asp:Panel>
            <a name="yd" id="yd"></a>
        </div>
    </div>
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
            function AddOrder(TourId) {
                if ("<%=IsLogin %>" == "False") {
                    parent.Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl(" + TourId + ") %>", width: "400px", height: "250px", modal: true });
                } else {
                    if ("<%=Role %>" == "1") { //专线身份
                        window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + TourId;
                    }
                    if ("<%=Role %>" == "2") { //组团身份
                        window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/OrderByTour.aspx?tourID=" + TourId;
                    }
                    if ("<%=Role %>" == "3") { ////其它身份
                        window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Default.aspx?tourID=" + TourId;
                    }
                }
            }
           
           //团队状态设置日历出团时间的表格颜色 客满 停收 #BBD279   收客 成团 #ffd648
           function InitStateColor() {
               for (var i = 0; i < $(".calendarTable").find("span").length; i++) {
                   if ($(".calendarTable").find("span").eq(i).attr("state") == "3" || $(".calendarTable").find("span").eq(i).attr("state") == "4") {
                       $(".calendarTable").find("span").eq(i).parent("td").css({ "background-color": "#ffe488" });
                   } else {
                       $(".calendarTable").find("span").eq(i).parent("td").css({ "background-color": "#BBD279" });
                   }
               }
           }

           $(function() {
               //初始化日历表格
               QGD.config.isLogin = "<%=IsLogin %>"; //是否登陆
               QGD.config.stringPort ="<%= EyouSoft.Common.Domain.UserPublicCenter %>";
               QGD.initCalendar({
                   containerId: "divAllTourList",//放日历容器ID
                   currentDate:<%=thisDate %>,//当前月
                   firstMonthDate:<%=thisDate %>,//当前月
                   nextMonthDate: <%=NextDate %>,//下一个月
                   areatype: <%=AreaType %>, //团队线路区域类型
                   AddOrder: AddOrder,
                   nextfn:function(){InitStateColor();},
                   prevfn:function(){InitStateColor();}               
               });  

               //设置日历表格的样式.
               $(".calendarTable").css({ "border": "solid 0px #fff", "border-collapse": "collapse" });
               $(".calendarTable").find("tr").css({ "border": "solid 0px #fff" });
               $(".calendarTable").find("td").css({ "border": "solid 0px #fff" });
               $(".datacol").find("tr").css({ "border": "solid 1px #E3E3E3" });
               $(".datacol").find("td").css({ "border": "solid 1px #E3E3E3", "background-color": "#ffffff" });
               $(".mbkgurl").attr("style", "");
               $(".mbkgurl").find("tr").attr("style", "");

               //根据团队状态初始化表格颜色
               InitStateColor();

               //登录显示同业销售需知
               if ("<%=IsLogin %>" == "False") {
                   $("#<%=panIsLogin.ClientID %>").hide();
               } else {
                   $(".Userlogin").parent().hide();
                   $("#<%=panIsLogin.ClientID %>").show();
                   $("#<%=txtContectName.ClientID %>").val('<%=Contect %>');
                   $("#<%=txtContectPhone.ClientID %>").val('<%=contecttel %>');
               }

               //散客预定
               $(".SanKeYD").click(function() {
                   var tourID = $("#<%=DropPowerList.ClientID %>").val(), adult = $("#<%=txtAdultNumber.ClientID %>").val(), child = $("#<%=txtChildrenNumber.ClientID %>").val(), contact = $("#<%=txtContectName.ClientID %>").val(), tel = $("#<%=txtContectPhone.ClientID %>").val();
                   if ("<%=IsLogin %>" == "False") {
                       Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl(" + tourID + "," + adult + "," + child + "," + contact + "," + tel + ") %>", width: "400px", height: "250px", modal: true });
                   }
                   else {
                       if ("<%=Role %>" == "1") { //专线身份
                           window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + tourID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
                       }
                       if ("<%=Role %>" == "2") { //组团身份
                           window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/OrderByTour.aspx?tourID=" + tourID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
                       }
                       if ("<%=Role %>" == "3") { //其它身份
                           window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/Default.aspx?tourID=" + TourId;
                       }
                   }
                   return false;
               });

               //单团预定
               $(".SingleTourYD").click(function() {
                   var RouteID = "<%=EyouSoft.Common.Utils.GetQueryStringValue("routeId") %>", adult = $("#<%=txtAdultNumber.ClientID %>").val(), child = $("#<%=txtChildrenNumber.ClientID %>").val(), contact = $("#<%=txtContectName.ClientID %>").val(), tel = $("#<%=txtContectPhone.ClientID %>").val();
                   if ("<%=IsLogin %>" == "False") {
                       Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrlsingle(" + RouteID + "," + adult + "," + child + "," + contact + "," + tel + ") %>", width: "400px", height: "250px", modal: true });
                   } else {
                       window.location.href = "<%=EyouSoft.Common.Domain.UserBackCenter %>/TeamService/SingleGroupPre.aspx?isZT=true&routeId=" + RouteID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel + "";
                   }
                   return false;
               });

               $(".Userlogin").click(function() {
                   if ("<%=IsLogin %>" == "False") {
                       Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                   } else {
                       $(this).parent("div").hide();
                       $("#<%=panIsLogin.ClientID %>").show();
                   }
                   return false;
               });

               $("#<%=DropPowerList.ClientID %>").change(function() {
                   var tourID = $("#<%=DropPowerList.ClientID %>").val();
                   var cid = '<%=this.Master.CompanyId %>';
                   $.ajax({
                       type: "GET",
                       url: "/seniorshop/TourDetail.aspx?tourid=" + tourID + "&t=1&cid=" + cid,
                       cache: false,
                       dataType: "json",
                       success: function(result) {
                           if (result != null) {
                               $(".huang16").html(result);
                           }
                       },
                       error: function(XMLHttpRequest, textStatus, errorThrown) {
                           alert("请求异常!");
                       }
                   });
               });
           });        
    </script>

</asp:Content>
