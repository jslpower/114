<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyMQ.aspx.cs" Inherits="UserBackCenter.SystemSet.ApplyMQ" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content id="ApplyMQ" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<style>
.am_tableleft td
{
  text-align:left;
}
.td_left
{
  text-align:left;
}
</style>
 <table width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr><td valign="top" >
	 <uc1:sznb id="sznb1" runat="server" ></uc1:sznb>


<table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_01.jpg" width="133" height="77" /></td>
    <td colspan="2" background="<%=ImageServerUrl%>/images/sqbj.gif"><table width="100%" border="0" align="center" cellpadding="5" cellspacing="0">
      <tr>
        <td style="color:#ff0000; font-size:16px; font-weight:bold;"><%=mq_message %>
      </tr>
    </table></td>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_02.jpg" width="45" height="77" /></td>
  </tr>
  <tr>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_03.jpg" width="133" height="99" /></td>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_04.jpg" width="345" height="99" /></td>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_05.jpg" width="277" height="99" /></td>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_06.jpg" width="45" height="99" /></td>
  </tr>
</table>
<table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_07.jpg" width="245" height="52" /></td>
    <td><a href="#tar_a" ><img src="<%=ImageServerUrl%>/images/mqyd_08.jpg" width="233" height="52" border="0" /></a></td>
    <td><img src="<%=ImageServerUrl%>/images/mqyd_09.jpg" width="322" height="52" /></td>
  </tr>
</table>

<table width="800" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:15px;">
  <tr>
    <td width="375" valign="top"><table class="am_tableleft" width="100%" height="350" border="0" cellpadding="4" style="padding-top:0px;" cellspacing="1" bgcolor="#9FCBE5">
      <tr>
        <td colspan="2" align="center"   style=" text-align:center" valign="top" bgcolor="#73B4D9"><strong>基 本 服 务 功 能</strong></td>
        </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">1、即时聊天 </td>
        <td width="451" valign="top" bgcolor="#FFFFFF">聊天洽谈、传送文件、视频等类似QQ、MSN功能，加好友数量不限，支持群发文件和消息 </td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">2、查找好友</td>
        <td width="451" valign="top" bgcolor="#FFFFFF">目前MQ中有近3万家且不断增长的旅游同行用户，用户可分类查找并加为全国各地的同行好友，进入中国最大的万人级旅游圈</td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">3、建多个群 </td>
        <td width="451" valign="top" bgcolor="#FFFFFF">可以任意建多个群，更方面同行和客户之间的交流 </td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">4、线路预订 </td>
        <td width="451" valign="top" bgcolor="#FFFFFF">有近5万多条散拼计划供组团社客户实时查询、洽谈和预订。专线可通过MQ实时候发布产品、订单处理和业务管理 </td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">5、机票预订 </td>
        <td width="451" valign="top" bgcolor="#FFFFFF">实时查询和预订机票，返点高达20%，出票快、退款快和支付方便 </td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF">6、互动交流 </td>
        <td width="451" valign="top" bgcolor="#FFFFFF">提供同行之间的相互交流的社区，包括询报价、散客拼车、求助、招聘、各地动态等</td>
      </tr>
      <tr>
        <td width="120" valign="top" bgcolor="#FFFFFF"><p>7、近期热点 </p></td>
        <td width="451" valign="top" bgcolor="#FFFFFF"><p>提供同业114平台的主题活动，例如：世博专题、特惠促销等 </p></td>
      </tr>
    </table></td>
    <td width="50" align="center"><img src="<%=ImageServerUrl%>/images/duibiq.gif" width="48" height="57" /></td>
    <td width="375" valign="top">
	<div style="position:relative; text-align:left">
		<div style="position:absolute; top:-6px; left:-5px; background:url(<%=ImageServerUrl%>/images/1_39.gif) no-repeat; width:74px; height:78px;"></div>
	</div>
	<table  width="100%" height="350"  border="0" cellpadding="4" cellspacing="1" bgcolor="#EE8B2A">
      <tr>
        <td colspan="2" align="center" valign="top" bgcolor="#EE8B2A"><strong>增 值 功 能</strong></td>
      </tr>
      <tr>
        <td colspan="2" align="center" style="text-align:center" valign="top" bgcolor="#FFFFFF"><strong>企业MQ在基本服务功能基础上加增值功能</strong></td>
        </tr>
      <tr>
        <td width="120" height="64" align="center" valign="top" bgcolor="#FFFFFF" ><img src="<%=ImageServerUrl%>/images/zhizh.gif" width="32" height="32" /><br />
          <strong>子帐号管理</strong> </td>
        <td width="451" valign="top" bgcolor="#FFFFFF" class="td_left">　　免费MQ用户只能一人使用。<br />
          　　企业MQ用户可以通过开设子帐号的形式，让公司所有员工进行便捷的资源共享、统一收客、协同开展服务。<a href="<%=ImageServerUrl%>/images/ys1.jpg" target="_blank" style="color:#ff0000;"><strong>查看功能演示</strong></a></td>
      </tr>
      <tr>
        <td width="120" height="76" align="center" valign="top" bgcolor="#FFFFFF"><img src="<%=ImageServerUrl%>/images/gggl.gif" width="32" height="32" /><br />
          <strong>广告管理</strong></td>
        <td width="451" valign="top" bgcolor="#FFFFFF"  class="td_left">　　免费MQ用户只能在聊天窗口右侧展示简单的公司名称和联系方式信息。
          <br />
          　　企业MQ用户在除了展示免费MQ的基本信息外，还可展示公司主营业务、产品特色、促销信息等、聊天窗口右侧放自己的广告图片和定义链接，让自己的业务做得更有特色，人脉资源得到更充分的利用。<a href="<%=ImageServerUrl%>/images/ys2.jpg" target="_blank" style="color:#ff0000;"><strong>查看功能演示</strong></a></td>
      </tr>
      <tr>
        <td width="120" height="73" align="center" valign="top" bgcolor="#FFFFFF" ><img src="<%=ImageServerUrl%>/images/khgl.gif" width="32" height="32" /><br />
          <strong>客户管理</strong> </td>
        <td width="451" valign="top" bgcolor="#FFFFFF" class="td_left" >　　免费MQ用户只能在MQ好友列表管理客户。<br />
          　　企业MQ用户拥有专门的客户管理栏目，在这里有同业114近三万且不断增长的有效客户资源，供您拓展业务时选择，还能辅助您有效的管理销售记录，让市场拓展和日常工作变更省心、省事！ 
          <a href="<%=ImageServerUrl%>/images/ys3.jpg" target="_blank" style="color:#ff0000;"><strong>查看功能演示</strong></a>          </td>
      </tr>

    </table></td>
  </tr>
</table>
<table width="800" border="0" align="center" cellpadding="5" cellspacing="0" style="margin-top:15px;">
  <tr>
    <td bgcolor="#E03231"><span style="font-size:14px; font-weight:bold; color:#ffffff;">立即申请<a name="tar_a" id="sq"></a></span></td>
  </tr>
</table>

<table width="800" border="0" align="center" cellpadding="0" cellspacing="0" style="background:url(<%=ImageServerUrl%>/images/b-bj.gif) no-repeat top;">
  <tr>
    <td width="101" align="center" style="font-size:14px; padding:10px;"><img src="<%=ImageServerUrl%>/images/dhsq.gif" width="117" height="39" /></td>
    <td width="699" align="left" style="font-size:14px; padding:10px;">0571-56884918 MQ：<%=Utils.GetBigImgMQ("28733") %> QQ：<a
                                    href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img src="http://wpa.qq.com/pa?p=1:774931073:10"
                                        border="0" alt="点击这里给我发消息" width="61" height="16" /></a> 陈小姐； 
      <br />
    0571- 56884627 MQ：<%=Utils.GetBigImgMQ("22547") %> QQ：<a
                                    href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img src="http://wpa.qq.com/pa?p=1:774931073:10"
                                        border="0" alt="点击这里给我发消息" width="61" height="16" /></a> 何小姐。</td>
  </tr>
  <tr>
    <td height="400" colspan="2" valign="top">
	
	<table width="80%" border="0" cellspacing="0" cellpadding="0" align="center">
	<thead><tr><th width="30%"></th><th width="70%"></th></tr>
	</thead>
      <tr align="left">
        <td height="30" align="right"><strong>公司名称：</strong></td>
        <td>
      <%=companyName %>
        </td>
      </tr>
      <tr  align="left">
        <td height="30" align="right"><span style="color:#f00">*</span><strong>申请人：</strong></td>
        <td id="am_tdContact"><input name="am_txtContact" type="text" class="shurukuangsm" id="am_txtContact" size="20" valid="required" errmsg="请填写申请人"  runat="server"  />
        <span id="errMsg_<%=am_txtContact.ClientID %>" class="errmsg"></span><%=contantName %>
        </td>
      </tr>
      <tr  align="left">
        <td height="30" align="right"><span style="color:#f00">*</span><strong>联系电话：</strong></td>
        <td id="am_tdTel"><input name="am_txtTel" type="text" class="shurukuangsm" id="am_txtTel" size="20" valid="required|isPhone" errmsg="请填写联系电话|请填写有效的电话格式"  runat="server"/>
        <span id="errMsg_<%=am_txtTel.ClientID %>" class="errmsg"></span><%=tel %>
        </td>
      </tr>
      <tr  align="left">
        <td height="30" align="right"><span style="color:#f00">*</span><strong>手机：</strong></td>
        <td id="am_tdMoible"><input name="am_txtMoible" type="text" class="shurukuangsm" size="20" id="am_txtMoible"  valid="required|isMobile" errmsg="请填写联系电话|请填写有效的手机格式" runat="server"/>
         <span id="errMsg_<%=am_txtMoible.ClientID %>" class="errmsg"></span><%=moible %>
        </td>
      </tr>
      <tr  align="left">
        <td height="30" align="right"><strong>地址：</strong></td>
        <td id="am_tdAddress"><input name="am_txtAddress" id="am_txtAddress" type="text" class="shurukuang" size="20" runat="server"/>
        <%=adress %>
        </td>
      </tr>
      <tr  align="center">
        <td>&nbsp;</td>
        <td align="left"><input type="image" name="imageField" id="am_applyMQ"  onclick="return ApplyMQ.save(this)" src="<%=ImageServerUrl%>/images/add_button.gif" <%=isApply %> /></td>
      </tr>
     <%-- <tr  align="center">
        <td height="200">&nbsp;</td>
        <td align="left">&nbsp;</td>
      </tr>--%>
    </table>
	
	<table width="90%" border="0"  id="am_isSuccess" align="center" cellpadding="3" cellspacing="0" style="margin-top:20px; display:none">
      <tr>
        <td bgcolor="#f5f5f5" style="line-height:160%;">&nbsp;&nbsp;&nbsp;<strong style="color:#cc0000;"> 您已成功提交开通“企业MQ”申请! </strong> &nbsp;&nbsp;&nbsp; “企业MQ”属于同业114平台的收费项目之一，我们将会在一个工作日内与您联系，如您急需开通，请拨打0571-56884627，谢谢！</td>
      </tr>
    </table></td>
  </tr>
</table>

	 </td>
	 </tr>
 </table>
<script type="text/javascript">
  $(document).ready(function()
    {
        FV_onBlur.initValid($("#am_applyMQ").closest("form").get(0));
    });
var ApplyMQ=
{
  save:function(tar_save){
     var form = $(tar_save).closest("form").get(0);
     var form1=$(form);
     var contactValue=form1.find("#<%=am_txtContact.ClientID %>").val();
     var telValue=form1.find("#<%=am_txtTel.ClientID %>").val();
     var moibleValue=form1.find("#<%=am_txtMoible.ClientID %>").val();
     var addressValue=form1.find("#<%=am_txtAddress.ClientID %>").val();
     if(ValiDatorForm.validator(form,"span"))
     {
          $.newAjax(
              {
               url:"/SystemSet/ApplyMQ.aspx",
               data:{method:"applyMQ",contact:contactValue,tel:telValue,mobile:moibleValue,address:addressValue},
               dataType:"json",
               cache:false,
               type:"post",
               success:function(result){
                   if(result.success=='1')
                   {
                     form1.find("#am_isSuccess").css("display","");
                     form1.find("#am_tdContact").html("<span>"+contactValue+"</span>");
                     form1.find("#am_tdTel").html("<span>"+telValue+"</span>");
                     form1.find("#am_tdMoible").html("<span>"+moibleValue+"</span>");
                     form1.find("#am_tdAddress").html("<span>"+addressValue+"</span>");
                     form1.find("#am_applyMQ").css("display","none");
                   }
                   if(result.success=='0')
                   {  
                     form1.find("#am_isSuccess").find("td").html(result.message).css("color","red");
                   }
                },
                error:function(){
                   alert("申请时发生错误!");
               }
             })
             return false;
         
     }
     return false;
  }
  
}
</script>
</asp:Content>
