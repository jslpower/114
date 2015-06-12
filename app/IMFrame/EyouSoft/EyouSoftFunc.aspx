<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EyouSoftFunc.aspx.cs" Inherits="IMFrame.EyouSoft1.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
<!--
BODY { 	color:#333;	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	text-align: center;	background:#fff;margin:0px;}
img {	border: thin none;}
table{	border-collapse:collapse; margin:0px auto; padding:0px auto; }
TD {	FONT-SIZE: 12px; COLOR: #0E3F70; line-height: 20px;  FONT-FAMILY:"宋体",Arial, Helvetica, sans-serif; }
div {	margin: 0px auto;	text-align: left;	padding:0px auto;	border:0px;}
textarea {	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	COLOR: #333;}
select {	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	COLOR: #333;}
.ff0000 {color:#f00;}
a {COLOR:#0E3F70; TEXT-DECORATION: none;}
a:hover {COLOR:#f00; TEXT-DECORATION: underline;}
a:active {color:#f00; TEXT-DECORATION: none;}
.yytbj div{width:212px; height:234px;overflow:hidden; margin-bottom:5px;}
.yytbj ul{ list-style:none; margin:0; padding:0; width:105%; margin-left:1px;_margin-left:-2px; height:241px;background:url(images/listylei.gif) repeat-y 104px top;_background:url(images/listylei.gif) repeat-y 108px top; margin-top:-3px;}
.yytbj { padding:5px; background:none; }
.yytbj ul li{ margin:0; padding:0; float:left;  width:97px; height:44px;color:#013d62; font-weight:normal; margin-left:4px; margin-right:4px; margin-top:3px;background:url(images/lityheng.gif) repeat-x 104px bottom;}
.yytbj ul li a{ display:block; width:97px; height:40px; line-height:40px;  overflow:hidden; font-weight:bolder;}
.yytbj ul li a:hover{background:url(images/allimg.gif) no-repeat left top; text-decoration:none; color:#013d62;}
.yytbj ul li span.icon{ display:inline-block; width:27px; height:27px; background-image:url(images/allimg.gif); background-repeat:no-repeat; background-attachment:scroll; margin-top:6px;*+margin-top:-1px!important;_margin-top:6px; margin-left:7px;}
.yytbj ul li span.text{ position:relative; top:-20%;_top:-12%;*top:-11%!important;left:5px;}
.yytbj ul li span.icon1{ background-position:0px -40px;}
.yytbj ul li span.icon2{ background-position:-27px -40px;}
.yytbj ul li span.icon3{ background-position:0px -67px;}
.yytbj ul li span.icon4{ background-position:-27px -67px;}
.yytbj ul li span.icon5{ background-position:0px -94px;}
.yytbj ul li span.icon6{ background-position:-27px -94px;}
.yytbj ul li span.icon7{ background-position:0px -121px;}
.yytbj ul li span.icon8{ background-position:-27px -121px;}
.yytbj ul li span.icon9{ background-position:0px -148px;}.yytbj ul li span.icon10{ background-position:-27px -148px;}
.yytbjtel { color:#9A1E1E;}
-->	
</style>
</head>
<body>
<form id="form1" runat="server">
   
        <table width="210" cellspacing="0" cellpadding="0" border="0" align="center">
            <tbody>
                <tr>
                    <td>
                        <img height="59" width="210" alt="易游通5+2" src="images/yytlogo.gif">
                    </td>
                </tr>
                <tr>
                    <td>
                        <a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.个人中心)%>">
                            <img height="33" width="212" border="0" alt="立即进入软件后台" src="images/softui_03.gif" /></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img height="15" width="212" border="0" alt="易游通5=2功能介绍" src="images/softui_05.gif" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="yytbj">
                        <div>
                            <ul>
                               
                                <%  if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.发布计划))
                                  { %> 
                                   <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.发布计划)%>"><span class="icon icon1"></span><span class="text">发布计划</span></a></li>
                                  <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.我要报价))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.我要报价)%>"><span
                                    class="icon icon2"></span><span class="text">我要报价</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.预订报名))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.预订报名)%>"><span
                                    class="icon icon3"></span><span class="text">预订报名</span></a></li>
                                <%} %>
				<%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.计调安排))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.计调安排)%>"><span
                                    class="icon icon10"></span><span class="text">计调安排</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.统计分析))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.统计分析)%>"><span
                                    class="icon icon4"></span><span class="text">统计分析</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.组团收入))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.组团收入)%>"><span class="icon icon5"></span><span class="text">我要收款</span></a></li>
                                <%} %>
                                <%else if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.地接收入))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.地接收入)%>"><span class="icon icon5"></span><span class="text">我要收款</span></a></li>
                                <%} %>
                                <%else if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.入境收入))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.入境收入)%>"><span class="icon icon5"></span><span class="text">我要收款</span></a></li>
                                <%} %>
                                <%else if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.销售收款))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.销售收款)%>"><span class="icon icon5"></span><span class="text">我要收款</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.我要付款))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.我要付款)%>"><span
                                    class="icon icon6"></span><span class="text">我要付款</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.导游中心))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.导游中心)%>"><span class="icon icon7"></span><span class="text">导游中心</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.行政中心))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.行政中心)%>"><span class="icon icon8"></span><span class="text">行政中心</span></a></li>
                                <%} %>
                                <%if (IsHavePower(IMFrame.EyouSoft1.PowerFunc.单项服务))
                                  { %>
                                <li><a target="_blank" href="<%=GetFullPath(IMFrame.EyouSoft1.Path.单项服务)%>"><span class="icon icon9"></span><span class="text">单项服务</span></a></li>
                                <%} %>
                            </ul>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
