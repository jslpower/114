<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EShopSet.aspx.cs" Inherits="UserBackCenter.EShop.EShopSet" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .topbannercss11
        {
            margin: 0 auto;
            padding-top: 0px;
            background-repeat: no-repeat;
        }
        .fontdiv
        {
            font-size: 12px;
            position: absolute;
            right: 0px;
            width: 100%;
        }
    </style>
</head>
<body>
    <link href="<%=CssManage.GetCssFilePath("bodyframe")%>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("bluetabs") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("picrotation")%>"></script>

    <form id="form1" runat="server">
    <div class="boxgrid captionfull">
        <div>
            <div class="topbanner">
                <%=BannerImage%>
            </div>
        </div>
        <div class="cover boxcaption">
            <a href="javascript:void(0)" id="boxy_companylogo">
                <h3 style="height: 100px; padding-top: 80px; cursor: pointer">
                    点击可上传高级网店头部图片</h3>
            </a>
        </div>
    </div>
    <div id="bluemenu" class="nav">
        <ul>
            <li><a href="javascript:void(0)" class="nav-on">首 页</a></li>
            <li><a href="javascript:void(0)" id="boxy_sanping">散拼计划</a></li>
            <li><a href="javascript:void(0)" title="点击此图标维护本项信息！" id="boxy_guide">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/xxit.gif" alt="点击此图标维护本项信息！" />出游指南</a></li>
            <li><a href="javascript:void(0)" title="点击此图标维护本项信息！" id="boxy_resource">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/xxit.gif" alt="点击此图标维护本项信息！" />旅游资源推荐</a></li>
            <li><a href="javascript:void(0)" title="点击此图标维护本项信息！" id="boxy_news">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/xxit.gif" alt="点击此图标维护本项信息！" />最新动态</a></li>
            <li><a href="javascript:void(0)" title="点击此图标维护本项信息！" id="boxy_Notices">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/xxit.gif" alt="点击此图标维护本项信息！" />同业资讯</a></li>
            <li><a href="javascript:void(0)" title="点击此图标维护本项信息！" id="boxy_aboutwe">
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/xxit.gif" alt="点击此图标维护本项信息！" />关于我们</a></li>
        </ul>
    </div>
    <div class="mainbox">
        <div class="left">
            <div class="loginbar">
                同行登录口</div>
            <div class="login">
                <div class="login-left">
                    用户名：<input name="eshopset_txtusername" type="text" size="15" />
                    <br />
                    密&nbsp;&nbsp; <span lang="zh-cn">&nbsp;</span>码：<input name="eshopset_txtuserpwd"
                        type="text" size="15" />
                    <br />
                    验证码：<input name="eshopset_txtvali" type="text" size="6" /></div>
                <div style="width: 92px; float: right;">
                    <div class="login-right">
                        <input type="input" name="Submit" value="提交" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <div class="deng">
                        <a class="FF6600">组团社(零售商) 注 册</a></div>
                </div>
            </div>
            <div style="height: 15px;">
            </div>
            <div class="boxgrid4 infocfull4">
                <div class="leftn">
                    <div class="neileftk">
                        <div class="neilefth">
                            公司档案</div>
                        <div class="neileftxx">
                            <table width="94%" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop5">
                                <tr>
                                    <td align="left" style="font-weight: bold; font-size: 14px;">
                                        品牌名称：

                                        <script language="JavaScript">
                        <!--                                            Begin
                                            Brandtext = "<%=CompanyBrand %>"; //显示的文字

                                            color1 = "blue"; //文字的颜色

                                            color2 = "red"; //转换的颜色

                                            fontsize = "5"; //字体大小
                                            speed = 100; //转换速度 (1000 = 1 秒) 
                                            i = 0;
                                            if (navigator.appName == "Netscape") {
                                                document.write("<layer id=a visibility=show></layer><br>");
                                            }
                                            else {
                                                document.write("<div id=a></div><br>");
                                            }
                                            function changeCharColor() {
                                                var str;
                                                if (navigator.appName == "Netscape") {
                                                    str = "<center><font face=arial size =" + fontsize + "><font color=" + color1 + ">";
                                                    for (var j = 0; j < Brandtext.length; j++) {
                                                        if (j == i) {
                                                            str += "<font face=arial color=" + color2 + ">" + Brandtext.charAt(i) + "</font>";
                                                        }
                                                        else {
                                                            str += Brandtext.charAt(j);
                                                        }
                                                    }
                                                    str += "</font></font></center>";
                                                    document.getElementById("a").innerHTML = str;
                                                }
                                                if (navigator.appName == "Microsoft Internet Explorer") {
                                                    str = "<center><font face=arial size=" + fontsize + "><font color=" + color1 + ">";
                                                    for (var j = 0; j < Brandtext.length; j++) {
                                                        if (j == i) {
                                                            str += "<font face=arial color=" + color2 + ">" + Brandtext.charAt(i) + "</font>";
                                                        }
                                                        else {
                                                            str += Brandtext.charAt(j);
                                                        }
                                                    }
                                                    str += "</font></font></center>";
                                                    document.getElementById("a").innerHTML = str;
                                                }
                                                (i == Brandtext.length) ? i = 0 : i++;
                                            }
                                            setInterval("changeCharColor()", speed);
                        // End -->
                                        </script>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Literal ID="ltr_LinkPerson" runat="server"></asp:Literal>
                                        &nbsp;<%=Mqpath%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltr_Mobile" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltr_Phone" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltr_Fax" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltr_Address" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="cover boxcaption4">
                    <a href="javascript:void(0)" id="boxy_CompanyProf">
                        <h3 style="height: 60px; padding-top: 80px; cursor: pointer; text-align: center;">
                            点击添加公司档案</h3>
                    </a>
                </div>
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;
                border: 1px solid #B5CBD0;">
                <tr>
                    <td>
                        <div class="boxgrid5 infocfull5">
                            <img src="<%=CompanyCardPath%>" width="261" height="142" /><div class="cover boxcaption5">
                                <a href="javascript:void(0)" id="boxy_card">
                                    <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                        点击上传名片</h3>
                                </a>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0" style="margin-top: 10px;
                border: 1px solid #B5CBD0;">
                <tr>
                    <td width="34%" height="20" align="center" valign="bottom">
                        <a href="http://www.travelsky.com/travelsky/static/home/" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/fly.gif" width="18"
                                height="18" />
                            航班查询</a>
                    </td>
                    <td width="33%" align="center" valign="bottom">
                        <a href="http://qq.ip138.com/train/" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/huoche.gif" width="18"
                                height="18" />
                            火车查询</a>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="center" valign="bottom">
                        <a href="http://weather.tq121.com.cn/" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/tianqi.gif" width="18"
                                height="18" />
                            天气查询</a>
                    </td>
                    <td align="center" valign="bottom">
                        <a href="http://www.hao123.com/haoserver/kuaidi.htm" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/kuaid.gif" width="18"
                                height="18" border="0" />
                            快递查询</a>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="center" valign="bottom">
                        <a href="http://site.baidu.com/list/wannianli.htm" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/wnl.gif" width="18"
                                height="18" />
                            万 年 历</a>
                    </td>
                    <td align="center" valign="bottom">
                        <a href="http://www.ip138.com/post/" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/youbian.gif" width="18"
                                height="18" />
                            邮编查询</a>
                    </td>
                </tr>
                <tr>
                    <td height="20" align="center" valign="bottom">
                        <a href="http://qq.ip138.com/idsearch/" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/sfz.gif" width="18"
                                height="18" />
                            身 份 证</a>
                    </td>
                    <td align="center" valign="bottom">
                        <a href="http://www.ip138.com/sj/index.htm" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/shouji.gif" width="18"
                                height="18" />
                            手机号码</a>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="20" valign="bottom">
                        <a href="http://www.ip138.com/ips8.asp" target="_blank">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/ip.gif" width="18"
                                height="18" />IP&nbsp; 查询</a>
                    </td>
                    <td align="center" width="33%" valign="bottom">
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 15px;">
                <tr>
                    <td>
                        <iframe border="0" name="play" marginwidth="0" marginheight="0" src="http://www.soso.com/tb.q"
                            frameborder="0" width="260" scrolling="no" height="195"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <div class="right">
            <div class="fouse">
                <div class="boxgrid2 infocfull">

                    <script type="text/javascript">
            var pic_width=385; //图片宽度
            var pic_height=170; //图片高度
            var button_pos=4; //按扭位置 1左 2右 3上 4下
            var stop_time=3000; //图片停留时间(1000为1秒钟)
            var show_text=1; //是否显示文字标签 1显示 0不显示
            var txtcolor="000000"; //文字色
            var bgcolor="DDDDDD"; //背景色
            var imag=new Array();
            var link=new Array();
            var text=new Array();
            <%=initFlashJs %>
            var swf_height=show_text==1?pic_height+20:pic_height;
            var pics="", links="", texts="";
            for(var i=1; i<imag.length; i++){
	            pics=pics+("|"+imag[i]);
	            links=links+("|"+link[i]);
	            texts=texts+("|"+text[i]);
            }
            pics=pics.substring(1);
            links=links.substring(1);
            texts=texts.substring(1);
            document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cabversion=6,0,0,0" width="'+ pic_width +'" height="'+ swf_height +'">');
            document.write('<param name="movie" value="<%=ImageServerUrl %>/images/seniorshop/focus.swf">');
            document.write('<param name="quality" value="high"><param name="wmode" value="transparent">');
            document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'">');            
            document.write('<embed src="<%=ImageServerUrl %>/images/seniorshop/focus.swf" FlashVars="pics='+pics+'&links='+links+'&texts='+texts+'&pic_width='+pic_width+'&pic_height='+pic_height+'&show_text='+show_text+'&txtcolor='+txtcolor+'&bgcolor='+bgcolor+'&button_pos='+button_pos+'&stop_time='+stop_time+'" quality="high" width="'+ pic_width +'" height="'+ swf_height +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" wmode="transparent"/>');
document.write('</object>');
            </script>

                    <div class="cover boxcaption2">
                        <a href="javascript:void(0)" id="boxy_RotationPic">
                            <h3 style="height: 100px; padding-top: 80px; cursor: pointer; text-align: center;">
                                点击可上传轮换图片息</h3>
                        </a>
                    </div>
                </div>
            </div>
            <div class="newsleft">
                <div class="newsleftbar">
                    <span>最新旅游动态</span><a>更多>></a></div>
                <ul style="height: 148px;">
                    <asp:Repeater ID="creptNews" runat="server">
                        <ItemTemplate>
                            <li>•&nbsp;&nbsp;<a class="huizi"><%#Utils.GetText(Eval("Title").ToString(),40) %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div id="div_NewsNoMessage" style="display: none; text-align: center; margin-top: 75px;
                        margin-bottom: 75px;">
                        暂无最新动态信息！
                    </div>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div class="bar-line">
                <div class="bar-line-title">
                    我的旅游线路</div>
                <div class="bar-line-mid">
                </div>
                <div class="bar-line-more">
                    <a>查看全部旅游线路</a></div>
            </div>
            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3"
                class="liststyle">
                <tr>
                    <th width="10%" align="center">
                        单房差
                    </th>
                    <th width="45%" align="center">
                        团队基本信息
                    </th>
                    <th width="18%" align="center">
                        班次
                    </th>
                    <th width="19%" align="center">
                        市场价
                    </th>                  
                    <th width="8%" align="center">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="crptLinelist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%# Eval("StartCityName")%>
                            </td>
                            <td align="left">
                                <div class="listtitle">
                                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/ico.gif" width="11" height="11" />
                                    <%#ShowTourStateInfo(((int)Eval("RecommendType")).ToString())%>
                                    <a href="javascript:void(0)">
                                        <%#Utils.GetText(Eval("RouteName").ToString(), 18)%></a>
                                </div>
                            </td>
                             <%# TeamPlanDesAndPrices(Eval("RouteId").ToString(),Eval("RouteSource").ToString(),Eval("TeamPlanDes").ToString(),Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0")) %>                     
                            <td align="center" width="80">
                                <a href="javascript:void(0)" class="goumai0">预订</a><br />
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/MQWORD.gif" width="49" height="16" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="div_NoDataMessage" style="display: none">
                    <td colspan="5">
                        <div style="text-align: center; margin-top: 75px; margin-bottom: 75px;">
                            暂无旅游线路信息！</div>
                    </td>
                </tr>
            </table>
            <div class="mudidikuang">
                <div class="mudidi">
                    <div class="mudizi">
                        目的地指南</div>
                    <div style="background: #FFF6C7; border: 1px solid #FF8624; margin: 0 10px 0 10px;
                        padding: 1px; color: #339933; width: 350px; float: left;">
                        <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/gan.gif" />
                        请点击！<a href="javascript:void(0)" id="boxy_guid1" title="点击此图标维护本项信息！" class="FF6600"><strong>【添加目的地指南】</strong></a><a
                            href="javascript:void(0)" id="boxy_guidList" title="点击此图标维护本项信息！" class="FF6600"><strong>【管理目的地指南】</strong></a></div>
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="33%" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="margin-left: 5px;">
                                <tr>
                                    <td valign="top" class="muhang">
                                        <strong>风土人情介绍</strong><span><a>更多>></a></span>
                                    </td>
                                </tr>
                                <%=strTripGuide1%>
                            </table>
                        </td>
                        <td width="33%" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="margin-left: 5px;">
                                <tr>
                                    <td class="muhang" valign="top">
                                        <strong>温馨提醒</strong><span><a>更多&gt;&gt;</a></span>
                                    </td>
                                </tr>
                                <%=strTripGuide2%>
                            </table>
                        </td>
                        <td width="34%" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="margin-left: 5px;">
                                <tr>
                                    <td class="muhang">
                                        <strong>综合介绍</strong><span><a>更多&gt;&gt;</a></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Repeater ID="crptTripGuide3" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        •&nbsp;&nbsp;<a href="javascript:void(0)" class="huizi"><%#Utils.GetText(Eval("Title").ToString(),25)%><</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div id="div_GuideNoMessage" runat="server" visible="false" style="text-align: center;
                                            margin-top: 50px; margin-bottom: 50px;">
                                            暂无<span lang="zh-cn">目的地</span>指南信息！</div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mudidikuang">
                <div class="mudidi">
                    <div class="mudizi">
                        旅游资源推荐</div>
                    <div style="background: #FFF6C7; border: 1px solid #FF8624; margin: 0 10px 0 10px;
                        padding: 1px; color: #339933; width: 300px; float: left;">
                        <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/gan.gif" />
                        请点击！<a href="javascript:void(0)" id="boxy_Reso1" title="点击此图标维护本项信息！" class="FF6600"><strong>【添加旅游资源】</strong></a><a
                            href="javascript:void(0)" id="boxy_ResoList" title="点击此图标维护本项信息！" class="FF6600"><strong>【管理旅游资源】</strong></a></div>
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <cc1:CustomRepeater ID="crptReso" runat="server">
                            <ItemTemplate>
                                <td width="33%">
                                    <table width="125" border="0" cellspacing="0" cellpadding="0" style="margin: 10px 5px 10px 5px;">
                                        <tr>
                                            <td>
                                                <a>
                                                    <img src="<%# Utils.GetLineShopImgPath(Eval("ImagePath").ToString(),4) %>" width="123"
                                                        height="82" border="0" /></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <a>
                                                    <%#Utils.GetText(Eval("Title").ToString(),8) %></a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                        <tr id="tr_ResoNoMessage" style="display: none">
                            <td>
                                <div style="text-align: center; margin-top: 50px; margin-bottom: 50px;">
                                    暂无旅游推荐信息！</div>
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="boxgrid3 infocfull3">
        <div class="firendbar" style="background: #fff;">
            友情链接</div>
        <div class="firendlist" style="background: #fff;">
            <asp:Repeater ID="rptLinkFriend" runat="server">
                <ItemTemplate>
                    <ul>
                        <li>·<%#Eval("LinkName")%></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="cover boxcaption3">
            <a href="javascript:void(0)" id="boxy_FiendLink">
                <h3 style="height: 30px; padding-top: 10px; cursor: pointer; text-align: center;">
                    点击添加友情链接</h3>
            </a>
        </div>
    </div>
    <div class="boxgrid3 infocfull3">
        <div class="firendlist" style="background: #fff;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;"
                id="Table1">
                <tr>
                    <td align="center" class="bottom1" style="height: auto;">
                        <asp:Literal ID="ltrCopyRight" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cover boxcaption3">
            <a href="javascript:void(0)" id="boxy_CopyRight">
                <h3 style="height: 30px; padding-top: 10px; cursor: pointer; text-align: center;">
                    点击版权</h3>
            </a>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("dcommon") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>    
    <script type="text/javascript">
        $(".boxgrid3 h3").css("height", $('.boxgrid3').height());
        $(".boxgrid6 h3").css("height", $('.boxgrid6').height());
        if ($("#tblcopyright").parent("div").height() < 50) {
            $("#tblcopyright").parent("div").css("height", "60px")
        }

        function boxymethods(pageurl, strtitle) {
            Boxy.iframeDialog({ title: strtitle, iframeUrl: pageurl, width: 730, height: 470, draggable: true, data: { one: '1', two: '2', callBack: 'cancel'} });
            return false;
        }
        function boxymethodssmall(pageurl, strtitle) {
            Boxy.iframeDialog({ title: strtitle, iframeUrl: pageurl, width: 650, height: 330, draggable: true, data: { one: '1', two: '2', callBack: 'cancel'} });
            return false;
        }
        $(document).ready(function() {
            $("#div_showList1").mouseover(function() { $("#div_showList2").show(); $("#div_showList2").load("date.html"); });
            $("#div_showList1").mouseout(function() { $("#div_showList2").hide(); });

            $('.boxgrid.captionfull').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '100px' }, { queue: false, duration: 100 });
            });
            //
            $('.boxgrid2.infocfull').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '150px' }, { queue: false, duration: 100 });
            });
            $('.boxgrid3.infocfull3').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '36px' }, { queue: false, duration: 100 });
            });

            $('.boxgrid4.infocfull4').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '250px' }, { queue: false, duration: 100 });
            });
            $('.boxgrid5.infocfull5').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '122px' }, { queue: false, duration: 100 });
            });

            $(".boxgrid6 h3").css("height", $('.boxgrid6').height());
            if ($("#tblcopyright").parent("div").height() < 50) {
                $("#tblcopyright").parent("div").css("height", "60px")
            };
            $('.boxgrid6.infocfull6').hover(function() {
                $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 100 });
            }, function() {
                $(".cover", this).stop().animate({ top: '122px' }, { queue: false, duration: 100 });
            });
            //boxy
            $('#boxy_CompanyProf').click(function() { return boxymethodssmall("CompanyProfile.aspx", "修改公司档案"); });
            $('#boxy_aboutwe').click(function() { return boxymethods("SetAboutUs.aspx", "关于我们"); });
            $('#boxy_CopyRight').click(function() { return boxymethods("SetCopyRight.aspx", "修改版权"); });
            $('#boxy_companylogo').click(function() { return boxymethodssmall("SetLogoPic.aspx", "上传高级网店头部图片"); });
            $('#boxy_RotationPic').click(function() { return boxymethods("RotationPicManage.aspx", "上传轮换图片"); });
            $('#boxy_FiendLink').click(function() { return boxymethods("SetFriendLinkManage.aspx", "友情链接管理"); });
            $('#boxy_card').click(function() { return boxymethodssmall("SetCard.aspx", "上传名片"); });
            $('#boxy_guide').click(function() { return boxymethods("SetTravelGuid.aspx?GuideType=-1", "目的地指南管理"); });
            $('#boxy_resource').click(function() { return boxymethods("SetResources.aspx", "旅游资源推荐管理"); });
            $('#boxy_news').click(function() { return boxymethods("SetNews.aspx", "最新旅游动态管理"); });
            $("#boxy_Notices").click(function() {
                parent.topTab.open("/TongYeInfo/InfoList.aspx", "同业资讯", { isRefresh: false });
                return false;
            });
            $('#boxy_ResoList').click(function() { return boxymethods("SetResourcesList.aspx", "旅游资源管理"); });
            $('#boxy_Reso1').click(function() { return boxymethods("SetResources.aspx", "旅游资源推荐管理"); });
            $('#boxy_guid1').click(function() { return boxymethods("SetTravelGuid.aspx?GuideType=-1", "目的地指南管理"); });
            $('#boxy_guidList').click(function() { return boxymethods("SetTravelGuidList.aspx?GuideType=-1", "目的地指南管理"); });

        });
    </script>

    </form>
</body>
</html>
