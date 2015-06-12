<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationEShop.aspx.cs" Inherits="UserBackCenter.GeneralShop.ApplicationEShop" MasterPageFile="~/MasterPage/Site1.Master"%>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="ctnGeneShop" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
p{ margin:0; padding:0;}
body{ margin:0; padding:0; font-size:12px;}
ul,li,ol{ list-style-type:none; margin:0; padding:0;}
.container{ margin:0; padding:0;}
.addshop_banner{
	background:url(<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/banner.jpg) no-repeat top center;
	height:297px;
	margin:0;
	padding:0;
}
.addshop_banner ul{

	width:170px;
}
.addshop_banner ul li{
	font-size:12px;
	color:#d20000;
}
.Content{ 
	margin:0 auto;
	padding:0;
	width:801px;
	overflow:hidden;
	font-size:12px;
}
.Content_left{
	float:left;
	width:350px;
	background:url(<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/tedian_bg.gif) no-repeat left center;
	height:235px;
	margin-left:40px;
	display:inline;
	zoom:1;
}
.Content_left ul{
	 padding:55px 0 0 0;
}
.Content_left ul li{
	 text-align:left;
	 color:#000000;
	 line-height:20px;
	 text-indent:35px;
}
.Content_right{
	float:right;
	background:url(<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/form_bg.gif) no-repeat right top; 
	width:284px;
	margin-right:40px;
	height:230px;
	padding:25px 0 0 0;
	display:inline;
	zoom:1;
}
.Content_bottom{ 
	background:url(<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/bottom_bg.gif) no-repeat center top;
	width:719px;
	height:149px;
	text-align:left;
	padding:40px 0 0 50px;
	line-height:20px;
	margin:-20px auto 0 auto;
}
</style>

  <div class="container">
    	<div class="addshop_banner">
    	  <table width="800" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td colspan="2" align="right" valign="bottom"><a href="/ShopStaticPage/demo/index.html" target="_blank"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/ys.gif" alt="点击查看demo演示" width="418" height="179" border="0" /></a>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="10"></td>
                  </tr>
              </table></td>
              <td width="361" height="212" align="center" valign="bottom">&nbsp;</td>
            </tr>
            <tr>
              <td colspan="2" align="right" valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="47%" height="24">&nbsp;</td>
                  <td width="19%" align="left"><%=Utils.GetBigImgMQ("28733") %></td>
                  <td width="34%" align="left"><a href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img src="http://wpa.qq.com/pa?p=1:774931073:10" border="0" alt="点击这里给我发消息" width="61" height="16" /></a></td>
                </tr>
                <tr>
                  <td height="23">&nbsp;</td>
                  <td align="left" valign="top"><%=Utils.GetBigImgMQ("22547") %></td>
                  <td align="left" valign="top"><a href="tencent://message/?uin=774931073&Site=同业114&Menu=yes" target="blank"><img src="http://wpa.qq.com/pa?p=1:774931073:10" border="0" alt="点击这里给我发消息" width="61" height="16" /></a></td>
                </tr>
                
              </table></td>
              <td height="74" align="center" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="72%" align="right"><a href="/ShopStaticPage/demo/index.html" target="_blank" style="font-size:14px; color:#333333;">点此体验同业高级网店功能</a></td>
                  <td width="28%" align="center">&nbsp;</td>
                </tr>
              </table></td>
            </tr>
          </table>
    	</div>
       <!-- Content start-->
        <div class="Content">
        <!--Content_left start-->
        	<div class="Content_left">
            	<ul>
                	<li>1、使用独立一级域名发布；</li>
                    <li>2、真正属于自己的品牌推广；</li>
                    <li>3、唯一展示店主的线路产品；</li>
                    <li>4、全方位展现企业的服务内涵；</li>
                    <li>5、共享同业114平台现有近3万且不断增长的客户资源；</li>
                    <li>6、拥有功能更强大企业ＭＱ即时通讯软件；</li>
                    <li>7、加入MQ就是加入中国最大的万人级旅游圈……</li>
                </ul>
				<div style="clear:both;"></div>
            </div>
			
			
            <!--Content_left end-->
            <!--Content_right start-->
            <div class="Content_right">
            <form id="formadd">
				<table width="80%" border="0" cellspacing="0" cellpadding="0" align="center" id="startTable">
                  <tr align="left">
                    <td height="30"><strong>公司名称：</strong></td>
                    <td><input  type="text" id="appleshop_companyName" name="appleshop_companyName" valid="required" runat="server" readonly="readonly" errmsg="公司名称不能为空" class="shurukuangsm" size="20" style="border:0px solid #ffffff;" />
            </td>
                  </tr>
                  <tr  align="left">
                    <td height="30"><strong>申请人：</strong></td>
                    <td><input runat="server" valid="required" errmsg="申请人不能为空" name="appleshop_appleperson" type="text" id="appleshop_appleperson" class="shurukuangsm" size="20" />
                  
                    </td>
                  </tr>
                  <tr  align="left">
                    <td height="30"><strong>联系电话：</strong></td>
                    <td><input runat="server" type="text" id="appleshop_tel" name="appleshop_tel"  valid="isPhone" errmsg="电话号码填写错误" class="shurukuangsm" size="20" />
                    </td>
                  </tr>
                  <tr  align="left">
                    <td height="30"><strong>手机：</strong></td>
                    <td><input runat="server" valid="required|isMobile" errmsg="手机不能为空|手机填写错误" type="text" id="appleshop_mod" name="appleshop_mod" class="shurukuangsm" size="20" />
                    </td>
                  </tr>
                  <tr  align="left">
                    <td height="30"><strong>域名：</strong></td>
                    <td><input valid="required|isUrl" errmsg="域名不能为空|域名格式错误" type="text" runat="server" id="appleshop_address" name="appleshop_address" class="shurukuang" size="20" value="http://"/>
                    </td>
                  </tr>
                  <tr  align="center">
                    <td colspan="2">
                      <a id="btnaddEshop" runat="server" style="cursor:pointer" ><img  src="<%=ImageManage.GetImagerServerUrl(1)%>/images/shop/add_button.gif" /> </a> 
                        <asp:label id="lbl_ApplSucss" runat="server" text=""></asp:label>
                    </td>                       
                  </tr>
                </table>
                <div  runat="server" style="font:Red;" id="divApplSuss" >
                    <span style="text-align:30px; color:Red" > 您已成功提交开通“同业高级网店”申请!</span>
                </div>
				<table width="90%" border="0" align="center" cellpadding="3" cellspacing="0"  id="showresult" style=" display:none;">
                  <tr>
                    <td style="line-height:160%;">&nbsp;&nbsp;&nbsp;<strong style="color:#cc0000;"> 您已成功提交开通“同业高级网店”申请!
                    </strong>                   
               &nbsp;&nbsp;&nbsp; “同业高级网店”属于同业114平台的收费项目之一，我们将会在一个工作日内与您联系，如您急需开通，请拨打0571-56884627，谢谢！</td>
                  </tr>
                </table>
                </form>
            </div>
            <!--Content_right end-->
            <div style="clear:both;">
                <a href="javascript:void(0)" onclick="ApplicationEShop.BackUrl()"><h4>&#60;&#60;返回上一页</h4></a>
            </div>
            <!--Content_bottom start-->
            <!--Content_bottom end-->
        </div>
        <!-- Content end-->
    </div>
<script type="text/javascript">
$(document).ready(function(){
    $("#ctl00_ContentPlaceHolder1_btnaddEshop").click(function()
    {
        if(ValiDatorForm.validator($("#ctl00_ContentPlaceHolder1_btnaddEshop").closest("form").get(0),"alert"))
        {
            var person=escape($("#<%=appleshop_appleperson.ClientID %>").val());
            var tel=escape($("#<%=appleshop_tel.ClientID%>").val());
            var mob=escape($("#<%=appleshop_mod.ClientID%>").val());
            var net=$("#<%=appleshop_address.ClientID %>").val();
            var companyName=escape($("#<%=appleshop_companyName.ClientID %>").val());
        $.newAjax({  
            url:"/GeneralShop/ApplicationEShop.aspx?AddEShop=1&p="+person+"&t="+tel+"&m="+mob+"&n="+net+"&c="+companyName,
            cache:false,
            success:function(msg)
            {
                if(msg=="1") //申请成功
                {
                    $("#showresult").show();
                    $("#startTable").hide();
                }else  if(msg=="2")       //申请失败
                {                    
                    $("#showresult").hide();
                    $("#startTable").show();
                }else if(msg=="3")
                {
                    alert("请输入完整信息！");
                }else if(msg=="4")
                {
                    alert("请输入正确的数据类型！");
                }else
                {
                    alert("操作失败！");
                }
            },
             error:function()
             {
                alert("操作失败!");
             }
        });
        }else{
        return false;
        }
       
    });
});
var ApplicationEShop=
{
   BackUrl:function (){
       topTab.url(topTab.activeTabIndex,"/GeneralShop/GeneShopMainPage.aspx");
       return false;
    }
}
</script>
</asp:Content>
