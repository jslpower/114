<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSearch.aspx.cs" Inherits="IMFrame.Hotel.OrderSearch" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
   
    </script>
    <style>
<!--
BODY {color:#333;	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	text-align: center;	background:#fff;margin:0px;}
img {border: thin none;}
* { margin:auto 0; padding:0;}
.headleft{float:left;}
.headright{float:right;}
input {font-size:12px; height:16px; border:1px solid #7F9DB9; color:#FFFFFF;}
a.he{text-decoration:none;color: #6B2E03; font-weight:bold;}
a.he:hover {text-decoration: underline;color: #ff6600;}	
.imgb{border:1px solid #DDD; padding:2px;}
#sp_cont { width:210px; position:relative;}
#spmenu { height:21px; width:210px; overflow:hidden; padding:0px; margin:0px; list-style:none;}
#spmenu li { width:99px; float:left; margin-left:4px; text-align:center;line-height:21px; height:21px; background:url(<%=ImageServerUrl%>/IM/images/Hotel/navan.gif) no-repeat; color:#000;  font-weight:normal; }
#spmenu li.spmenuOn {background: url(<%=ImageServerUrl%>/IM/images/Hotel/navan-on.gif) no-repeat;  cursor:pointer; height:33px; font-weight:bold; color:#6B2E03; }
.spmenuCon {clear:both;}
.clear{clear:both;}
.nrbj{background: url(<%=ImageServerUrl%>/IM/images/Hotel/nrbg.gif) repeat-x top;}
.pand{padding:3px 0px 2px 14px}
.pand2{padding:3px 0px 2px 4px}
.pand3{padding:5px 0px}
a.jp { color:#083971; text-decoration:none}
a.jp:visited { color:#083971; text-decoration:none}
a.jp:hover { color:#f00; text-decoration: underline}
.airform2 { float:left; border:1px solid #4592BF; width:110px;background-position:100% -95px; margin:0; padding:0; height:18px; background:#FFFFFF;}
.f11{font-size:11px;}
.diggPage a{ text-decoration:none; margin:1px;}
#link_his,#link_now{ color:#000; text-decoration:none;}
-->
</style>
</head>

<body>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="35" background="<%=ImageServerUrl%>/IM/images/Hotel/topbg.gif"><a href="#"name="hash_top"></a>
	<div class="headleft"><img src="<%=ImageServerUrl%>/IM/images/Hotel/jdname.gif" /></div>
	<div class="headright"><img src="<%=ImageServerUrl%>/IM/images/Hotel/114logo.gif" /></div>
	
	</td>
  </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="21" align="left" background="<%=ImageServerUrl%>/IM/images/Hotel/navbg.gif">
	<div id="sp_cont">
        <ul id="spmenu">
          <li  id="spmenuMu_10" ><a href="HotelSearch.aspx"  style=" text-decoration:none; color:#000;" >酒店查询</a></li>
          <li id="spmenuMu_11" class="spmenuOn" >酒店订单查看</li>

       </ul>
      </div>
    </td>
  </tr>
</table>

<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="spmenuCon_11" >
  <tr>
    <td class="nrbj pand2">
<table width="100%" border="0" align="left" cellpadding="0" cellspacing="0" >
      <tr>
        <td width="195" height="22" align="left"><img src="<%=ImageServerUrl%>/IM/images/Hotel/jt.gif"  id="img_his"  /> <a href="javascript:;" onclick="return GetOrderList('','history')" id="link_his" >历史订单(<span id="span_hisNum"></span>)</a></td>
      </tr>
      <tr style="" id="tr_historyOrder">
         
      </tr>
      <tr>
        <td height="24" align="left" ><img src="<%=ImageServerUrl%>/IM/images/Hotel/jt.gif"  id="img_now"/><a href="javascript:;" onclick="return GetOrderList('','now')" id="link_now"> 当前订单(<span id="span_nowNum"></span>)</a></td>
      </tr>
      <tr id="tr_nowOrder"  style="">
         
      </tr>
    </table>
	</td>
  </tr>
</table>


<table><tr id="trt1"></tr></table>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
 <script type="text/javascript">
     //获取订单列表
     function GetOrderList(tarSpan, orderTypeP) {
         var hisTr=$("#tr_historyOrder");//历史订单行
         var nowTr = $("#tr_nowOrder");//当前订单行
         if (tarSpan == "") {//如果是初次加载
             if (orderTypeP == "" || orderTypeP == "now") {//如果类型传空或者now则取当前订单
                 if ($("#img_now").attr("src") == "<%=ImageServerUrl%>/IM/images/Hotel/jt2.gif") {//如果原来是打开状态 则关闭
                     nowTr.hide();
                     $("#img_now").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt.gif");
                     return false;
                 }
                 else {//如果原来是关闭状态 则打开，且隐藏历史订单
                     nowTr.show();
                     hisTr.hide();
                     $("#img_now").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt2.gif");
                     $("#img_his").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt.gif");
                 }


             }
             else {
                 if ($("#img_his").attr("src") == "<%=ImageServerUrl%>/IM/images/Hotel/jt2.gif") {//如果原来是打开状态 则关闭
                     hisTr.hide();
                     $("#img_his").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt.gif");
                     return false;
                 }
                 else {//如果原来是关闭状态 则打开，且隐藏历史订单
                     hisTr.show();
                     nowTr.hide();
                     $("#img_his").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt2.gif");
                     $("#img_now").attr("src", "<%=ImageServerUrl%>/IM/images/Hotel/jt.gif");
                 }
             }
         }
         var pageIndex = tarSpan == "" ? 1 : $(tarSpan).attr("gotopage");//获取页码
         $.ajax({
             type: "GET",
             dataType: "text",
             url: "AjaxOrderSearch.aspx",
             data: { orderType: orderTypeP, Page: pageIndex },
             cache: false,
             success: function(result) {
                 if (orderTypeP == "history") {
                     hisTr.html(result); //加载历史订单
                 }
                 else {
                     nowTr.html(result); //加载当前订单
                 }
                 if (tarSpan == "" && orderTypeP == "") {//如果是初次加载则显示订单数
                     var nums = $("#orderNum").html().split("|");
                     $("#span_hisNum").html(nums[1]);//历史订单数
                     $("#span_nowNum").html(nums[0]);//当前订单数
                 }
                 window.location.hash = "hash_top";//定位到页头
             }
         });
     }
     $(document).ready(function() {
        GetOrderList("", "");
     });
 </script>
</body>
</html>
