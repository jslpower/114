<%@ Page Title="标题_类别（特价机票）_同业114特价机票频道" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true" CodeBehind="AirPlaneInfo.aspx.cs" Inherits="UserPublicCenter.AirTickets.AirPlaneInfo" %>
<%@ Register Src="../WebControl/InfomationControl/HotRouteRecommend.ascx" TagName="HotRoute"
    TagPrefix="uc5" %>
    <%@ Register Src="../WebControl/InfomationControl/InfoRight.ascx" TagName="RightMenu"
    TagPrefix="uc3" %>
    <%@ Register Src="../WebControl/InfomationControl/AllCountryTourInfo.ascx" TagName="AllCountryMenu"
    TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta name="keywords" content="特价机票，免票，控票，特价机票网，特价机票预订，机票频道，特价机票退票" />
<meta name="description" content="同业114提供普通机票，免票，控票，特价机票，打折机票，更多超低价机票任您挑选，国航1折特价机票和折扣机票优惠多多，尽在tongye114。搜索上百家旅游预订网站机票报价和航空公司直 销机票价格,方便您的出行。" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">

<!--新闻列表开始--><style type="text/css">
.bread-nav a{ color:#074387}
.btmContent{ padding-top:0px;}
.btmNav{ margin-top:0px}
.btmNavC h3{ top:0px;}
.btmContent{ top:0px;}
</style>
   <div class="hr-10">
    </div>
    <div class="body" style="overflow: hidden">
        <div id="news-list-left" class="addBg">
            <!--列表 start-->
            <div class="box">
                <div class="box-main">
                    <div class="box-content box-content-main box-content-read">
                        <!--咨询详细 开始-->
                        <div class="news-read">
                          
                            <div class="bread-nav" style="padding-top:5px;" >
                                <p>
                                    <span><a href="<%= EyouSoft.Common.URLREWRITE.SubStation.CityUrlRewrite(CityId) %>">首页
                                        </a></span> <span>&gt;</span> <a href="<%=EyouSoft.Common.URLREWRITE.Plane.PlaneDefaultUrl(CityId) %>">机票
                                           </a> <span>&gt;</span> 正文
                                </p>
                            </div>
                            <div class="title">
                                <h1>
                                    <font color="333">
                                        <%= fareTitle %></font></h1>
                                <p style=" margin-top:10px;">
                                    <span>
                                        <%= fareDate %></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;来源：<span></span></p>
                            </div>
                            <div class="content">
                            <div style="position:relative"><div style=" width:80px; height:17px; position: absolute; top:35px; left:280px;"><a href="http://im.tongye114.com:9000/webmsg.cgi?version=1&amp;uid=37683" title="在线即时交谈" style="display:block; width:100%; height:100%"></a></div><div  style="width:98px; height:25px; position:absolute; top:52px; left:360px;"><a href="tencent://message/?Menu=yes&uin=34737111&Site=80fans&Service=300&sigT=45a1e5847943b64c6ff3990f8a9e644d2b31356cb0b4ac6b24663a3c8dd0f8aa12a545b1714f9d45" title="在线即时交谈" style="display:block;width:100%; height:100%"></a></div>
                            <img src="<%=ImageServerPath %>/images/new2011/index/tickets_03.gif"/></div>
                               <%= fareContent %>
                            </div>
                            <div style="text-align: center; padding-top: 20px; padding-bottom: 20px; font-weight: 700;
                                font-size: 14px;">
                            
                            </div>
                            <div style="text-align: left; padding-top: 20px; padding-bottom: 20px; font-weight: 700;
                                font-size: 14px;">
                                <!-- JiaThis Button BEGIN -->
                                <div id="jiathis_style_32x32" style="clear: both; width: 600px; text-align: center;
                                    margin-bottom: 10px;">
                                    <a class="jiathis_button_tsina"></a><a class="jiathis_button_qzone"></a><a class="jiathis_button_xiaoyou">
                                    </a><a class="jiathis_button_tqq"></a><a class="jiathis_button_kaixin001"></a><a
                                        class="jiathis_button_renren"></a><a class="jiathis_button_baidu"></a><a class="jiathis_button_taobao">
                                        </a>
                                </div>


                                <!-- JiaThis Button END -->
                            </div>
                            <div class="news-read-footter">
                                
                                    <span class="left">
                                    </span><span class="right" style="float:right"><b>责任编辑：</b><span><%=fareAuthor %></span></span>
                                    <p  style="border:none">
                                       
                                    </p>
                            </div>
                         
                          
                          
                        </div>
                        <!--咨询列表 结束-->
                    </div>
                </div>
            </div>
            <!--列表 end-->
        </div>
        <!--右边 开始-->
        <uc3:RightMenu ID="rightMenu" runat="server" />
        <!--右边 结束-->
        <div class="hr-10">
        </div>
        <!--列表 start-->
        <uc7:AllCountryMenu ID="allCountry" runat="server" />
        <!--列表 end-->
    </div>
    <!--新闻详情结束-->
    <!--bottom nav start-->
    <div class="hr-10">
    </div>
    <uc5:HotRoute ID="hotRoute" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">

</asp:Content>
