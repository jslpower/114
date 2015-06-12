<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneShopPageSet.aspx.cs" Inherits="UserBackCenter.GeneralShop.GeneShopPageSet"%>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportHtmlPageInfo" tagprefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>旅游同业分销系统</title>
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
  <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

<style type="text/css">
    /*wanggp*/
.Obtn {	MARGIN-TOP: 104px; WIDTH: 25px; BACKGROUND: url(../images/img3-5_1.gif) no-repeat; FLOAT: left; HEIGHT: 150px; MARGIN-LEFT: -1px}
.boxgrid{ width:640px; height: 180px;float:left; background:#161613; border: solid 2px #8399AF; overflow: hidden; position: relative; }
.boxgrid h3 { font-size:30px;}
.boxcaption{ float: left; position: absolute; background: #BBE1F3;height: 180px; width: 100%; filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=80);}
.captionfull .boxcaption { top: 100; left: 0;}
.boxgrid2{ width:315px; height: 180px;float:left; background:#161613; border: solid 2px #8399AF; overflow: hidden; position: relative; }
.boxgrid2 h3 { font-size:30px; line-height:30px;}
.boxcaption2{ float: left; position: absolute; background: #BBE1F3;height: 180px; width: 100%; filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=80);}
.infocfull .boxcaption2 { top: 100; left: 0;}
</style>

</head>
<body>

    <form id="form1" runat="server">
<div id="header">
   <div class="logo">
        <a href="javascript:void(0)">
            <img src="<%=UnionLogo%>" alt="同业114" height="70" width="170"
                border="0" /></a></div>
    <div class="eare">
        <span>杭州</span><a href="#"></a> </div>
    <div class="tymq">
        <a target="_blank"  title="同业MQ免费下载">
            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/UserPublicCenter/mqgif.gif" alt="同业MQ免费下载"  border="0" /></a></div>
    <div class="menu">
        <ul>
            <li class="on"><a href="javascript:void(0)"><span>首 页</span></a></li>
            <li><a href="javascript:void(0)"><span>线 路</span></a></li>
            <li><a href="javascript:void(0)"><span>机 票</span></a></li>
            <li><a href="javascript:void(0)"><span>景 区</span></a></li>
            <li><a href="javascript:void(0)"><span>酒 店</span></a></li>
            <li><a href="javascript:void(0)"><span>车 队</span></a></li>
            <li><a href="javascript:void(0)"><span>旅游用品</span></a></li>
            <li><a ahref="javascript:void(0)"><span>购物点</span></a></li>
            <li><a href="javascript:void(0)"><span>供求信息</span></a></li>
        </ul>
    </div>
</div>



<table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="margin10">
      <tr>
        <td width="647" align="left" valign="top"><div class="boxgrid captionfull">
          <img src="<%=ImagePath%>" width="660" height="180" />
          <div class="cover boxcaption" ><a href="javascript:void(0)" onclick="return showImagePage()"> <h3 style="height:100px; padding-top:80px; cursor:pointer">点击可修改替换此图片</h3></a></div></div>
        </td>
        <td width="323" valign="top">
		
		
<div class="boxgrid2 infocfull">
  <table width="318" height="182" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td valign="top" style="background:url(<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/cardbj.gif) no-repeat">
			<div class="card-companyname"><a href="#"><asp:Literal ID="ltr_CompanyName" runat="server"></asp:Literal></a></div>			
			<table width="98%" border="0" align="center" cellpadding="1" cellspacing="0">
              <tr>
                <td width="47%" align="center"><img src="<%= LogoPath%>" width="140" height="62" style="border:1px solid #E9E9E9;"/></td>
                <td width="53%" align="left" class="fenstyle">
                <font style="color:#666666;">品牌名称：</font><asp:Literal ID="ltr_BrandName" runat="server"></asp:Literal><br />
              </td>
              </tr>
            </table>
			<table width="99%" border="0" align="right" cellpadding="0" cellspacing="0" style="margin-top:3px;">
              <tr>
                <td width="55%" align="left">联系人：<asp:Literal ID="ltr_LinkPerson" runat="server"></asp:Literal><%=MQHtml%></td>
                <td width="45%" align="left"><div style="width:140px; height:20px; overflow:hidden; white-space:nowrap;"><a href="javascript:void(0)" >手机：<asp:Literal ID="ltr_Mobile" runat="server"></asp:Literal></a></div></td>
              </tr>
              <tr>
                <td align="left"><div style="width:170px; height:20px; overflow:hidden; white-space:nowrap;"><a href="javascript:void(0)">电话： 
                    <asp:Literal ID="ltr_Phone" runat="server"></asp:Literal></a></div></td>
                <td align="left"><div style="width:140px; height:20px; overflow:hidden;">传真：<asp:Literal ID="ltr_Faxs" runat="server"></asp:Literal> </div></td>
              </tr>
              <tr>
                <td colspan="2" align="left"><div style="width:310px; height:20px; overflow:hidden;">地址：<asp:Literal ID="ltr_Address" runat="server"></asp:Literal></div></td>
              </tr>
            </table>
			</td>
          </tr>
        </table>
  <div class="cover boxcaption2"><a href="javascript:void(0)" onclick="return showCompanyInfoPage()"  ><h3 style="height:100px; padding-top:80px; cursor:pointer">点击可修改专线商信息</h3></a></div>
</div>
      	</td>
      </tr>
</table>
<table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="margin6" style="background:url(<%=ImageManage.GetImagerServerUrl(1) %>/images/listheadbj.gif)">
  <tr>
    <td width="100" height="26" align="center" valign="bottom" class="liston">&nbsp;<a href="javascript:void(0)">旅游线路</a></td>
    <td width="100" align="center" valign="bottom" class="listun">&nbsp;<a href="javascript:void(0)">专线介绍 </a> </td>
    <td width="100" align="center" valign="bottom" class="listun">&nbsp;<a href="javascript:void(0)">同业资讯 </a> </td>
	<td>&nbsp;</td>
	<td>&nbsp;</td>
    <td >&nbsp; </td>
    <td ></td>
  </tr>
</table>
<table width="970" height="10" border="0" align="center" cellpadding="0" cellspacing="0" class="xianluhangcx" style="line-height:10px; padding:0px;border:1px solid #ccc; border-bottom:0px;">
  <tr>
    <td width="67%" align="left" style="padding-left:65px;"><strong>团队基本信息</strong></td>
    <td width="22%" align="left" style="padding-left:45px;"><strong>成人价/儿童价</strong></td>
    <td width="11%"></td>
  </tr>
</table>
     <cc2:CustomRepeater ID="crptLineList" runat="server">
     <HeaderTemplate>
        <table width="970" border="0" align="center" cellpadding="0" cellspacing="0"  style="border:1px solid #D8D8D8; text-align:left;">
     </HeaderTemplate>
        <ItemTemplate>
            <tr bgcolor="#FFFFFF" >
                <td width="489" style="border-bottom: 1px dashed #ccc; height: 85px; padding-top: 5px;">
                  <img src="<%=ImageManage.GetImagerServerUrl(1) %>/images/seniorshop/ico.gif" width="11" height="11" /><a style="font-weight:bold;" href="javascript:void(0)" class="lan14"><%# Utils.GetText(Eval("RouteName").ToString(),27)%>（<%#Eval("TourDays")%>天）</a>
                   <br />
                    &nbsp;&nbsp;<span class="danhui">供应商：</span><span class="huise"> <%#Eval("CompanyName")%> </span>
                    <br />
                    &nbsp;&nbsp;<span class="danhui">最近一班：</span><span class="huise"><%# GetLeaveInfo(Convert.ToDateTime(Eval("LeaveDate").ToString()))%>/</span><span class="chengse"><strong>剩:<%# Eval("RemnantNumber")%></strong></span><span class="huise"> 其它13个发团日期>></span>
                </td>
                <td width="150" style="border-bottom: 1px dashed #ccc; line-height: 18px;">
                   门市价：<span class='chengse'>￥<%#Convert.ToDecimal(Eval("RetailAdultPrice").ToString()).ToString("F0")%> </span>起/<%# Convert.ToDecimal(Eval("RetailChildrenPrice").ToString()).ToString("F0") %>起<br />同行价：<span class='chengse'>￥<%# Convert.ToDecimal(Eval("TravelAdultPrice").ToString()).ToString("F0")%></span> 起/<%#Convert.ToDecimal(Eval("TravelChildrenPrice").ToString()).ToString("F0") %>起
                </td>
                <td width="94" style="border-bottom: 1px dashed #ccc; line-height: 14px;">
                    <div style="width: 65px; text-align: center">
                        <a href="javascript:void(0)" class="goumai0">预订</a></div>
                    <div style="width: 65px; text-align: center; padding-top: 3px;">
                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/MQWORD.gif" width="49" height="16" />
                        </div>
                </td>
            </tr>            
        <tr>
          
       </ItemTemplate>
       <FooterTemplate>
       </table>
       </FooterTemplate>
    </cc2:CustomRepeater>   
       <table width="990" border="0" align="center" cellpadding="0" cellspacing="0" >
      <tr>
        <td  align="right" bgcolor="#E9EFF3">
            <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" CurrencyPageCssClass="RedFnt" LinkType="4"   />
          </td>     
      </tr>
    </table>
<table width="990" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td width="1%" align="left" bgcolor="#D8DDE0"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/bottom_l.gif" width="4" height="27" /></td>
      <td width="99%" align="center" bgcolor="#D8DDE0">版权所有：杭州易诺科技有限公司<asp:Literal ID="ltr_Copyright" runat="server"></asp:Literal></td>
      <td width="0%" align="right"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/bottom_r.gif" width="3" height="27" /></td>
    </tr>
</table>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("common")%>"/>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("marquee") %>"></script>

<script type="text/javascript">
    $(document).ready(function(){
    //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
            $('.liststyle tbody tr:even').addClass('odd');
	        $('.liststyle tbody tr').hover(
		        function() {$(this).addClass('highlight');},
		        function() {$(this).removeClass('highlight');}
	        );

	        // 如果单选框默认情况下是选择的，变色.
	        $('.liststyle input[type="radio"]:checked').parents('tr').addClass('selected');
        	
	        // 单选框
	        $('.liststyle tbody tr').click(
		        function() {
			        $(this).siblings().removeClass('selected');
			        $(this).addClass('selected');
			        $(this).find('input[type="radio"]').attr('checked','checked');
		        }
	        );
            });
            $('.boxgrid.captionfull').hover(function(){
		        $(".cover", this).stop().animate({top:'0px'},{queue:false,duration:150});
	        }, function() {
		        $(".cover", this).stop().animate({top:'150px'},{queue:false,duration:150});
	        });
	        //
	        $('.boxgrid2.infocfull').hover(function(){
		        $(".cover", this).stop().animate({top:'0px'},{queue:false,duration:150});
	        }, function() {
		        $(".cover", this).stop().animate({top:'150px'},{queue:false,duration:150});		    	  
	    });
	    function showImagePage()
	    {
	           Boxy.iframeDialog({title:"修改图片", iframeUrl:"SetLeftImage.aspx",width:650,height:330,draggable:true,data:{one:'1',two:'2',callBack:'cancel'}});
                return false;
	    }
	    function showCompanyInfoPage()
	    {
	         Boxy.iframeDialog({title:"修改专线商信息", iframeUrl:"SetCompanyInfo.aspx",width:730,height:500,draggable:true,data:{one:'1',two:'2',callBack:'cancel'}});
                return false;
	    }
</script>
    </form>
</body>
</html>
