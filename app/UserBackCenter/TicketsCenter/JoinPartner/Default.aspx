<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserBackCenter.TicketsCenter.JoinPartner.Default" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="JoinPartnerD" runat="server" contentplaceholderid="ContentPlaceHolder1">
<table id="JoinPartnerD_tbl" width="90%" cellspacing="0" cellpadding="0" border="0" style="border: 1px solid rgb(125, 171, 216);">
          <tbody><tr>
            <td height="25" align="left" colspan="2"><span class="title02">选择签约支付平台</span></td>
          </tr>
          <tr>
            <td width="22%" height="60" align="center"><a code="1" href="javascript:void(0)"><img width="133" height="38" src="<%=ImageServerUrl %>/images/jipiao/cft_btn.gif"></a></td>
            <td width="78%" height="25" align="left">&nbsp;</td>
          </tr>
          <tr>
            <td width="22%" height="60" align="center"><a code="2" href="javascript:void(0)"><img width="133" height="39" src="<%=ImageServerUrl %>/images/jipiao/zfb_btn.gif"></a></td>
            <td width="78%" height="25" align="left">&nbsp;</td>
          </tr>
        </tbody></table>
        
        <table id="JoinPartner_tbl2" style="display:none;" width="90%" cellspacing="0" cellpadding="0" border="0" style="border: 1px solid rgb(125, 171, 216);">
          <tbody><tr>
            <td height="30" align="left" colspan="2"></td>
          </tr>
          <tr>
            <th width="25%" align="center">请输入要签约的账户：</th>
            <td width="75%" height="25" align="left"><input type="text" size="30" id="JoinPartner_txtaccount" class="bdtext" name="JoinPartner_txtaccount">
            <input type="button" value="绑定" id="JoinPartnerD_bind">&nbsp;&nbsp;<a id="JoinPartner_back2" href="javascript:;">返回</a></td>
          </tr>
          <tr>
            <td height="30" align="left" colspan="2"></td>
          </tr>
        </tbody></table>
        <table id="JoinPartner_tbl3" style="display:none;" width="90%" cellspacing="0" cellpadding="0" border="0" style="border: 1px solid rgb(125, 171, 216);">
          <tbody><tr>
            <td height="30" align="left" colspan="2"></td>
          </tr>
          <tr>
            <th width="35%" align="center"><span>当前接口的帐户已经加入支付圈</span>：</th>
            <td width="65%" height="25" align="left">绑定的帐户：<span id="JoinPartner_spanAccount"></span>
            &nbsp;&nbsp;<a id="JoinPartner_Sign" href="" target="_blank" style="display:none;">签约</a>
            &nbsp;&nbsp;<a id="JoinPartner_back3" href="javascript:;">返回</a></td>
          </tr>
          <tr>
            <td height="30" align="left" colspan="2"></td>
          </tr>
        </tbody></table>
        
        <script type="text/javascript">
        var JoinPartnerD={
            selectCode:null,
            select:function(code){
                JoinPartnerD.selectCode = code;
                //判断在该接口上是否已经签约
                $.newAjax({
                    type:"GET",
                    url:"/TicketsCenter/JoinPartner/Default.aspx",
                    data:{accountType:code,type:"issign"},
                    cache:false,
                    dataType:"json",
                    success:function(result){
                        if(result.success!=undefined && result.success=="0"){
                            alert(result.message);
                            return;
                        }else if(result.isBind!=undefined){
                            JoinPartnerD.show(result,1);
                        }
                    },
                    error:function(XMLHttpRequest, textStatus, errorThrown){
                        alert(textStatus);
                    }
                });
            },
            show:function(data,type){
                if(data.isBind){
                   if(data.isSign){
                    $("#JoinPartner_spanAccount").html(data.accountNumber);
                    $("JoinPartner_Sign").hide();
                    
                   }else{
                    if(type==1){
                        $("#JoinPartner_tbl3").find("span").eq(0).html("请点【签约】链接加入支付圈");
                    }else if(type==2){
                        $("#JoinPartner_tbl3").find("span").eq(0).html("绑定成功，请点【签约】链接加入支付圈");
                    }
                    $("#JoinPartner_spanAccount").html(data.accountNumber);
                    $("#JoinPartner_Sign").css("display","");
                    $("#JoinPartner_Sign").attr("href",decodeURIComponent(data.qianyueurl));
                   }
                   $("#JoinPartnerD_tbl").css("display","none");
                    $("#JoinPartner_tbl3").css("display","");
                }
                else{
                 $("#JoinPartnerD_tbl").css("display","none");
                 $("#JoinPartner_tbl2").css("display","");
                }
            },
            bind:function(){
                var account = $("#JoinPartner_txtaccount").val();
                account = $.trim(account);
                if(account==""){
                    alert("请填写帐户信息");
                    return;
                }
                $.newAjax({
                    type:"GET",
                    url:"/TicketsCenter/JoinPartner/Default.aspx",
                    data:{account:account,accountType:JoinPartnerD.selectCode,type:"bindaccount"},
                    cache:false,
                    dataType:"json",
                    success:function(result){
                        if(result.success!=undefined && result.success=="0"){
                            alert(result.message);
                            return;
                        }else if(result.success==1){
                            $("#JoinPartner_tbl2").css("display","none");
                            JoinPartnerD.show(result,2);
                        }
                    },
                    error:function(){
                    }
                });
            }
        }
        $(function(){
            $("#JoinPartnerD_tbl").find("a").click(function(){
                JoinPartnerD.select($(this).attr("code"));
                return false;
            });
            $("#JoinPartnerD_bind").click(function(){
                JoinPartnerD.bind();
            });
            $("#JoinPartner_back3").click(function(){
                $("#JoinPartnerD_tbl").css("display","");
                $("#JoinPartner_tbl3").css("display","none");
            });
            $("#JoinPartner_back2").click(function(){
                 $("#JoinPartnerD_tbl").css("display","");
                $("#JoinPartner_tbl2").css("display","none");
            });
        });
        </script>
</asp:content>