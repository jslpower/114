<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourDetail.aspx.cs" Inherits="UserPublicCenter.TourManage.TourDetail"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/RouteRightControl.ascx" TagName="RouteRightControl"
    TagPrefix="uc2" %>
<%@ Register Src="../WebControl/UCRightList.ascx" TagName="UCRightList" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
        type="text/css" />

    <script src="../DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("common") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("marquee") %>"></script>

    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="yudingbox">
        <uc3:UCRightList ID="UCRightList1" runat="server" />
        <div class="yudingright">
            <div class="xiaodaohang">
                <div>
                    您的位置：<a href="<%=EyouSoft.Common.Domain.UserPublicCenter%>">同业114</a>&nbsp;&gt;&nbsp;<a
                        href="<%=EyouSoft.Common.URLREWRITE.Tour.GetXianLuUrl(CityId) %>">线路</a>&nbsp;&gt;&nbsp;<span
                            class="zuouhou">线路预订</span></div>
            </div>
            <div class="jieshao">
                <div class="jieshaotop">
                    <asp:Label ID="lbRouteName" runat="server" Text=""></asp:Label>
                </div>
                <div class="jieshaoimg">
                    <img style="width: 295px; height: 195px;" alt=" " src="<%=RouteImgHref %>"></div>
                <ul>
                    <li>市 场 价： <span class="z24">
                        <asp:Label ID="lbPrice_c" runat="server" Text=""></asp:Label></span> </li>
                    <%if (RouteType == 1)
                      { %>
                    <li>定金：成人 <span class="STYLE1">
                        <asp:Label ID="lb_d_Price" runat="server" Text=""></asp:Label></span> 儿童 <span class="STYLE1">
                            <asp:Label ID="lbPrice_e" runat="server" Text=""></asp:Label></span></li><%} %>
                    <li>品牌：<span class="bgzi"></span><asp:Label ID="lbpinpai" runat="server"
                        Text=""></asp:Label><%= EyouSoft.Common.Utils.GetMQ(CompanyMQ)%></li>
                    <li>旅游主题：<span class="bgzi"><asp:Label ID="lbtheme" runat="server" Text=""></asp:Label></span>
                    </li>
                    <li>旅程天数：<span class="hong123"><asp:Label ID="lbdays" runat="server" Text="11"></asp:Label></span>天<asp:Label ID="lbnight" runat="server" Text="9"></asp:Label>
                        请提前<span class="hong123"><asp:Label ID="lbtqdays" runat="server" Text="30"></asp:Label></span>天报名</li>
                    <li style="width: 400px; height: 50px;">往返说明：<asp:Literal ID="lbgo" runat="server"></asp:Literal></br></asp:Label><asp:Label
                        Style="margin-left: 60px;" ID="lbBack" runat="server" Text=""></asp:Label></li>
                    <li>游览区域：<asp:Label ID="lbarea" runat="server" Text=""></asp:Label></li>
                    <%if (IsVisa)
                      { %>
                    <li class="qianzheng">签证：<asp:Label ID="lbqianzheng" runat="server" Text=""></asp:Label></li><%} %>
                    <%=IsTeam %>
                </ul>
            </div>
            <div class="yudingk">
                <div class="yudingktitle">
                    <img alt="在线预订" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_15.jpg"></div>
                <div class="yudingkrong">
                    <div class="yudingf" style="display:block;" id="ShowDiv">
                        <ul>
                            <li>
                                <asp:DropDownList ID="dropTravelAllTourList" runat="server">
                                </asp:DropDownList>
                                <label id="lbadultCount" runat="server">
                                    <asp:TextBox ID="txtCRCount" runat="server" Width="50px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                        onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>成人
                                </label>
                                
                                <label id="lbchildCount" runat="server">
                                    <asp:TextBox ID="txtRTCount" runat="server" Width="50px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                                        onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>小孩
                                </label>
                                </li>
                            <li class="daying"><a target="_blank" style="cursor: pointer" href='<%=EyouSoft.Common.Domain.UserBackCenter %>/PrintPage/LineTourInfo.aspx?RouteId=<%=RouteId %>'>
                                <img width="90" height="19" alt="打印行程" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/but03.gif" /></a></li>
                            <li style="width: 330px; padding-top: 5px; line-height: 20px;"><label id="lbContactName" runat="server"> 联系人：<asp:TextBox ID="txtContactName"
                                Width="70px" runat="server"></asp:TextBox></label><label id="lbContactTel" runat="server">联系电话：<asp:TextBox ID="txtContactTel" Width="110px"
                                    runat="server"></asp:TextBox></label> </li>
                            <li style="width: 255px; text-align:right; font-size:0;">
                                <img width="123" style="cursor: pointer" height="35" alt="散客预订" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_19.jpg"
                                    id="btnSKYD"  />
                                <img alt="单团预订" style="cursor: pointer; margin-left:5px" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_21.jpg" class="btnDTYD">
                            </li>
                        </ul>
                    </div>
                    <div class="yudingf" id="HiddenDiv" style="display:none">
                        <a style="display:block; float:left; height:67px; width:456px;" href="http://im.tongye114.com/"><img src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/xlgg.jpg" title="同业MQ,让您的生意更精彩." /></a>
                        <a target="_blank" style="cursor: pointer; margin-left:35px;" href='<%=EyouSoft.Common.Domain.UserBackCenter %>/PrintPage/LineTourInfo.aspx?RouteId=<%=RouteId %>'>
                                <img width="90" height="19" alt="打印行程" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/but03.gif" /></a>
                         <img alt="单团预订" style="cursor: pointer; margin:9px 0 0 5px; float:left;" src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_21.jpg" class="btnDTYD">
                    </div>
                </div>
            </div>
            <div class="xianlutd" id="divCharacteristic" runat="server">
                <div class="xianlutdtop">
                    <img src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_25.jpg"></div>
                <div class="xianlutdc">
                    <div class="xianlutdt">
                        线路特点</div>
                    <asp:Label ID="lbCharacteristic" runat="server" Text=""></asp:Label>
                </div>
                <div class="xianlutdb">
                    <img src="<%=EyouSoft.Common.Domain.ServerComponents%>/images/yuding_29.jpg"></div>
            </div>
            <div id="tourDate" runat="server">
                <div class="shuoming">
                    出发日期与价格 (以下信息仅供参考，请您谅解。)</div>
                <div class="biaoge" id="divAllTourList">
                </div>
            </div>
            <div class="neiwen" id="divTourPlanHead" runat="server">
                <div class="neiwentitle">
                    行程</div>
                <div class="neiwennei">
                    <ul>
                        <asp:Label ID="LiteralTourPlan" runat="server" Text=""></asp:Label>
                    </ul>
                </div>
            </div>
            <div id="divServiceStandardInfo" runat="server">
                <div class="neiwen" id="divServiceStandard" runat="server">
                    <div class="neiwentitle">
                        报价包含</div>
                    <div class="neiwennei">
                        <asp:Label ID="lblServiceStandard" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="neiwen" id="divNotContainService" runat="server">
                    <div class="neiwentitle">
                        报价不含</div>
                    <div class="neiwennei">
                        <asp:Label ID="lblNotContainService" runat="server" Text=""></asp:Label></div>
                </div>
                <div class="neiwen" id="divChildandplan" runat="server">
                    <div class="neiwentitle">
                        儿童及其他安排</div>
                    <div class="neiwennei">
                        <asp:Label ID="LbChildandplan" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="neiwen" id="divZsService" runat="server">
                    <div class="neiwentitle">
                        赠送项目</div>
                    <div class="neiwennei">
                        <asp:Label ID="lbZsService" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="neiwen" id="divSelfService" runat="server">
                    <div class="neiwentitle">
                        自费项目</div>
                    <div class="neiwennei">
                        <asp:Label ID="lbSelfService" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="neiwen" id="divShopping" runat="server">
                    <div class="neiwentitle">
                        购物点</div>
                    <div class="neiwennei">
                        <asp:Label ID="lbShopping" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="neiwen" id="divRemarks" runat="server">
                    <div class="neiwentitle">
                        备注信息</div>
                    <div class="neiwennei">
                        <asp:Label ID="lbRemarks" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <%if (!IsLogin)
              { %>
            <div class="neiwen2">
                <span class="STYLE6">同行业销售须知</span> <a style="cursor: pointer;" id="showinfo">登录后查看</a>
            </div>
            <%} %>
            <%if (IsLogin)
              { %>
            <div class="neiwen">
                <div class="neiwentitle2">
                    同行业销售须知</div>
                <div class="neiwennei">
                    <asp:Label ID="lbsaleinfo" runat="server" Text=""></asp:Label></div>
            </div>
            <%} %>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("toplist") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
        $(function() {
        if("<%=IsDijieRoute %>"=="True" || "<%=IsPlan %>"=="False")
            {
                $("#HiddenDiv").show();
                $("#ShowDiv").hide();
            }
        $("#<%=dropTravelAllTourList.ClientID %>").change();
        LoadingImg.ShowLoading("divAllTourList");
            
            QGD.config.isLogin = "<%=IsLogin %>"; //是否登陆
            QGD.config.stringPort ="";
            QGD.initCalendar({
                containerId: "divAllTourList",//放日历容器ID
                currentDate:<%=thisDate %>,//当前月
                firstMonthDate:<%=thisDate %>,//当前月
                nextMonthDate: <%=NextDate %>,//下一个月
                areatype: <%=RouteType %>,//团队线路区域类型
                AddOrder: AddOrder
            });
            if ("<%=IsLogin %>" == "False") {
                $(".neiwen2").show();
            }
        });
        
        function AddOrder(type) {
            var TourId;
            if(type!=2 && type!=1)
            {
                TourId=type;
            }
            else{
                if($("#<%=dropTravelAllTourList.ClientID %>").length>0 && $("#<%=dropTravelAllTourList.ClientID %>").val().split('|').length>0)
                {
                    TourId = $("#<%=dropTravelAllTourList.ClientID %>").val().split('|')[0];
                }
            }
            var AlultNum=$("#<%=txtCRCount.ClientID %>").val();
            var ChildNum=$("#<%=txtRTCount.ClientID %>").val();
            var ContactName=$.trim($("#<%=txtContactName.ClientID %>").val());
            var ContactTel=$.trim($("#<%=txtContactTel.ClientID %>").val());
            if ("<%=IsLogin %>" == "True") {
            var Url="";
            if(type==2)//单团预定
            {
                    Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/TeamService/SingleGroupPre.aspx?routeId=<%=RouteId %>&adult="+AlultNum+"&child="+ChildNum+"&contact="+escape(ContactName)+"&tel="+escape(ContactTel)+"&isZT=true";
            window.location.href=Url;
            }
            else//散客预定
            {
                if("<%=IsTour %>"=="True")
                {
                  //组团身份
                    Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/OrderByTour.aspx?tourID="+TourId+"&adult="+AlultNum+"&child="+ChildNum+"&contact="+escape(ContactName)+"&tel="+escape(ContactTel);
                    window.location.href=Url;
                 }
                 else if("<%=IsRoute %>"=="True")
                 {
                   //专线身份
                   Url="<%=EyouSoft.Common.Domain.UserBackCenter %>/Order/RouteAgency/AddOrderByRoute.aspx?tourID="+TourId+"&adult="+AlultNum+"&child="+ChildNum+"&contact="+escape(ContactName)+"&tel="+escape(ContactTel);
                   window.location.href=Url;
                 }
                 else
                 {
                    Url="<%=EyouSoft.Common.Domain.UserBackCenter %>";
                    window.location.href=Url;
                 }
              }
                  
            } else {
                //登录
             Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                return false;
            }
        }
        
            $("#btnSKYD").click(function() {
                AddOrder(1);
            });
            $(".btnDTYD").click(function(){
                AddOrder(2);
            });
            $(".neiwen2 a").click(function() {
                if ("<%=IsLogin %>" == "False") {
                
                    Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true });
                }
                return false;
			})		
			
			 $("#<%=dropTravelAllTourList.ClientID %>").change(function(){
			 if($(this).val()!=null && $(this).val()!="" && $(this).val().split('|').length==2){
			    $("#<%=lbPrice_c.ClientID %>").html($(this).val().split('|')[1]+"元/人起")
			 }
			 else{
			    $("#<%=lbPrice_c.ClientID %>").html("电询");
			 }
        });
        
 
    </script>

</asp:Content>
