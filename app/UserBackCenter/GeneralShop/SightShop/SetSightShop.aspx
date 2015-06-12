<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetSightShop.aspx.cs" Inherits="UserBackCenter.GeneralShop.SightShop.SetSightShop" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置景区普通网店</title>
    <link href="<%=CssManage.GetCssFilePath("head2011") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("index2011") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("xinmian") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style type="text/css">
        /*wanggp*/.Obtn
        {
            margin-top: 104px;
            width: 25px;
            background: url(../images/img3-5_1.gif) no-repeat;
            float: left;
            height: 150px;
            margin-left: -1px;
        }
        .boxgrid
        {
            width: 640px;
            height: 180px;
            float: left;
            border: solid 2px #8399AF;
            overflow: hidden;
            position: relative;
        }
        .boxgrid h3
        {
            font-size: 30px;
        }
        .boxcaption
        {
            float: left;
            position: absolute;
            background: #BBE1F3;
            height: 180px;
            width: 100%;
            filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=80);
        }
        .captionfull .boxcaption
        {
            top: 100;
            left: 0;
        }
        .boxgrid2
        {
            width: 315px;
            height: 180px;
            float: left;
            border: solid 2px #8399AF;
            overflow: hidden;
            position: relative;
        }
        .boxgrid2 h3
        {
            font-size: 30px;
            line-height: 30px;
        }
        .boxcaption2
        {
            float: left;
            position: absolute;
            background: #BBE1F3;
            height: 180px;
            width: 100%;
            filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=80);
        }
        .infocfull .boxcaption2
        {
            top: 100;
            left: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="site-nav">
        <div id="site-nav-bd">
            <p class="login-info">
                <font class="font12c6">您好，欢迎来同业114！</font> <a href="javascript:void(0)">登录</a> <a
                    href="javascript:void(0)">注册</a></p>
            <ul class="quick-menu">
                <li><a target="_blank" href="javascript:void(0)" title="同业114动态">同业114动态</a></li>
                <li class="Download-MQ"><a href="javascript:void(0)" target="_blank" title="同业MQ免费下载">
                    免费下载同业MQ</a><span>|</span></li>
                <li><a href="javascript:void(0)" target="_blank">提建议</a><span>|</span></li>
                <li><a href="javascript:void(0)">帮助中心</a><span>|</span></li>
                <li><a href="javascript:void(0)">设为首页</a><span>|</span></li>
                <li><a href="javascript:void(0)">加入收藏 </a></li>
            </ul>
        </div>
    </div>
    <div class="hr_5">
    </div>
    <div class="head">
        <!--masthead start-->
        <div class="masthead">
            <div class="tongye-logo">
                <a href="javascript:void(0)">
                    <img src="<%=UnionLogo %>" width="155px" height="57px" alt="同业114" /></a></div>
            <div class="weather-city">
                <span class="weather-city-name">
                    <asp:Label ID="labCityName" runat="server"></asp:Label></span><span id="s_wea"></span>
                <div class="tocity-list">
                    <a href="javascript:void(0)">[切换城市]</a> 热点：<a href="javascript:void(0)">北京</a>&nbsp;&nbsp;<a
                        href="javascript:void(0)">广州</a>&nbsp;&nbsp;<a href="javascript:void(0)">杭州</a>&nbsp;&nbsp;<a
                            href="javascript:void(0)">上海</a>&nbsp;&nbsp;<a href="javascript:void(0)">南京</a>&nbsp;&nbsp;<a
                                href="javascript:void(0)">济南</a>&nbsp;&nbsp;<a href="javascript:void(0)">宁波</a>&nbsp;&nbsp;<a
                                    href="javascript:void(0)">昆明</a>&nbsp;&nbsp;<a href="javascript:void(0)">沈阳</a></div>
                <div class="download">
                    <img src="<%= ImageManage.GetImagerServerUrl(1) %>/images/new2011/index/MQdownload.gif"
                        alt="MQ下载" border="0" usemap="#Map" />
                    <map name="Map" id="Map">
                        <area shape="rect" coords="-1,1,154,48" href="javascript:void(0)" target="_blank" />
                        <area shape="rect" coords="156,1,249,52" href="javascript:void(0)" target="_blank" />
                    </map>
                </div>
            </div>
            <!--masthead end-->
            <div class="hr_10">
            </div>
            <!--nav start-->
            <div class="nav fixed">
                <ul id="headMenu">
                    <li><a href="javascript:void(0)">首页</a></li>
                    <li><a href="javascript:void(0)">线路</a></li>
                    <li><a href="javascript:void(0)">机票</a></li>
                    <li><a href="javascript:void(0)">酒店</a></li>
                    <li><a class="select" href="javascript:void(0)">景区</a></li>
                    <li><a href="javascript:void(0)">供求</a></li>
                    <li><a href="javascript:void(0)">资讯</a></li>
                </ul>
                <a href="javascript:void(0)" class="mq-links">同业MQ，让您的生意做的更精彩！</a>
            </div>
            <!--nav end-->
            <div class="Notice fixed" style="margin-top:20px">
                <p class="Notice-L">
                    <s class="Notice-icon"></s>同业114现共有采购商 <b><font color="#FFFF00">
                        <asp:Literal ID="litBuy" runat="server"></asp:Literal></font></b> 供应商 <b><font color="#FFFF00">
                            <asp:Literal ID="litSup" runat="server"></asp:Literal></font></b> 近期供求 <b><font color="#FFFF00">
                                <asp:Literal ID="litNum" runat="server"></asp:Literal></font></b>
                </p>
                <div class="Notice-R fixed">
                    <span class="Notice-R-title"><s class="Notice-icon02"></s><b>公告：</b></span>
                    <ul class="scroll_state">
                        <li><a href="#">·同业114改版了！ 同业114改版了！</a></li><li><a href="#">·同业114改版了！ 同业114改版了！2</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="hr_10">
    </div>
    <div class="xinmain">
        <div class="xinleft">
            <div class="boxgrid captionfull">
                <img src="<%= ImagePath %>" width="652" height="182" alt=" " /><div class="cover boxcaption">
                    <a id="hrefImg" href="javascript:void(0)" onclick="return showImagePage()">
                        <h3 style="height: 100px; padding-top: 80px; cursor: pointer">
                            点击可修改替换此图片</h3>
                    </a>
                </div>
            </div>
        </div>
        <div class="xinright">
            <div class="boxgrid2 infocfull">
                <div class="xinright_t">
                    <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></div>
                <div class="xinright_c">
                    <div class="xinrightlogo">
                        <img src="<%= LogoPath %>" width="95" height="62" /></div>
                    <div class="xinrightwenzi">
                        品牌名称：<span class="xinlv"><asp:Literal runat="server" ID="ltrBrandName"></asp:Literal></span><br />
                        销售地区：<asp:Literal runat="server" ID="ltrSaleArea"></asp:Literal>
                        <br />
                        公司类型：<asp:Literal runat="server" ID="ltrCompanyType"></asp:Literal>
                    </div>
                </div>
                <div class="xinright_b">
                    <li><strong>联系人：</strong><asp:Literal runat="server" ID="ltrContact"></asp:Literal></li>
                    <li><strong>电 话：</strong><asp:Literal runat="server" ID="ltrTel"></asp:Literal></li>
                    <li><strong>传真：</strong><asp:Literal runat="server" ID="ltrFax"></asp:Literal></li>
                    <li class="xindizhi"><strong>地址：</strong><asp:Literal runat="server" ID="ltrAddress"></asp:Literal></li></div>
                <div class="cover boxcaption2">
                    <a id="hrefCompany" href="javascript:void(0)" onclick="return showCompanyInfoPage()">
                        <h3 style="height: 100px; padding-top: 80px; cursor: pointer">
                            点击可修改专线商信息</h3>
                    </a>
                </div>
            </div>
        </div>
        <div style="clear: both">
        </div>
        <div class="zhongnav">
            <ul>
                <li class="x_xuanzhong"><a href="javascript:void(0)">公司介绍</a></li>
                <li><a href="javascript:void(0)">景区</a></li>
            </ul>
        </div>
        <div class="main_center">
            <div class="about">
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
                <div class="x_contactR"></div>
            </div>
            <div class="huiyuanzq">
                注册登录查看更多同业联系方式 （以下为登录会员看到的内容）
            </div>
            <asp:PlaceHolder runat="server" ID="plnLoginUser">
                <div class="tylxfs">
                    <div class="tylxfs_t">
                        同业联系方式</div>
                    <div class="tylxfs_m">
                        <div class="lianxi_wenzi">
                            <asp:Literal runat="server" ID="ltrTYLXFS"></asp:Literal>
                        </div>
                        <div class="lianxi_biao">
                            <ul class="lianxi_di1">
                                <li>真实姓名</li>
                                <li>MQ</li>
                                <li>电话</li>
                                <li>手机</li>
                                <li>传真 </li>
                                <li>QQ</li>
                                <li style="width: 150px;">MSN</li>
                                <li style="width: 150px;">Email</li>
                            </ul>
                            <asp:Repeater runat="server" ID="rptCompanyUser">
                                <ItemTemplate>
                                    <ul class="lianxi_di2">
                                        <%# GetCompanyUserInfo(Eval("ContactInfo"))%>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
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
                            <li><a href="javascript:void(0)">
                                <img src="<%= LicenceImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                    href="javascript:void(0)">营业执照</a></span></li>
                            <li><a href="javascript:void(0)">
                                <img src="<%= BusinessCertImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                    href="javascript:void(0)">经营许可证</a></span></li>
                            <li><a href="javascript:void(0)">
                                <img src="<%= TaxRegImg %>" width="173" height="130" alt=" " /></a><span class="shuoming"><a
                                    href="javascript:void(0)">税务登记证</a></span></li>
                        </ul>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    <div style="clear: both">
    </div>
    <div class="footer" style="margin-top: 10px;">
        <div class="footer-nav-bd" style="text-align: center">
            <%=UnionRight%>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("common")%>" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("marquee") %>"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            //禁用所有的链接
            $("a").attr("href", "javascript:void(0)");
            $("a").attr("onclick", "");

            $("#hrefImg").bind("click", showImagePage);
            $("#hrefCompany").bind("click", showCompanyInfoPage);
        });

        $('.boxgrid.captionfull').hover(function() {
            $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 150 });
        }, function() {
            $(".cover", this).stop().animate({ top: '150px' }, { queue: false, duration: 150 });
        });
        $('.boxgrid2.infocfull').hover(function() {
            $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 150 });
        }, function() {
            $(".cover", this).stop().animate({ top: '150px' }, { queue: false, duration: 150 });
        });
        
        function showImagePage() {
            Boxy.iframeDialog({ title: "修改图片", iframeUrl: "/GeneralShop/SetLeftImage.aspx", width: 650, height: 330, draggable: true, data: { one: '1', two: '2', callBack: 'cancel'} });
            return false;
        }
        function showCompanyInfoPage() {
            Boxy.iframeDialog({ title: "修改专线商信息", iframeUrl: "/GeneralShop/SetCompanyInfo.aspx", width: 730, height: 500, draggable: true, data: { one: '1', two: '2', callBack: 'cancel'} });
            return false;
        }
    </script>

    </form>
</body>
</html>
