<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSupplyInfo.aspx.cs"
    Inherits="UserBackCenter.SupplyInformation.AddSupplyInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SupplyInformation/SupplyInfoTab.ascx" TagName="SupplyInfoTab"
    TagPrefix="uc2" %>
<%@ Register Src="../SupplyInformation/UserControl/AttatchPathFileUpload.ascx" TagName="AttatchPathFileUpload"
    TagPrefix="uc3" %>
<asp:content id="AddSupplyInfo" runat="server" contentplaceholderid="ContentPlaceHolder1">
<style type="text/css">
.lanse { font-size:14px; font-weight:bold;}
</style>
<script type="text/javascript" src="/kindeditor/kindeditor.js?v=2" cache="true" ></script>
<script type="text/javascript">
 //初始化编辑器
 KE.init({
	id : 'AddSupplyInfo_ExchangeText',//编辑器对应文本框id
	width : '570px',
	height: '250px',
	skinsPath:'/kindeditor/skins/',
	pluginsPath:'/kindeditor/plugins/',
	scriptPath:'/kindeditor/skins/',
    resizeMode : 0,//宽高不可变
    imageUploadJson: '<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload_json.ashx',
    blankPageUrl: '<%=EyouSoft.Common.Domain.UserBackCenter%>/kindeditor/plugins/image/blankpage.html',
	items:keSimple_Img
});
</script>
<table width="99%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <uc2:SupplyInfoTab ID="SupplyInfoTab1"  TabIndex="0" runat="server" />
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #B9D3E7;
                        margin: 0px 5px 5px 5px; text-align: left; padding: 5px;">
                        <tr>
                            <td style="padding: 4px;width: 550px;">
                                
                                <div style="font-size:14px; <%=model==null?"display:none;":(model.ExchangeCategory== EyouSoft.Model.CommunityStructure.ExchangeCategory.供?"display:none;":"") %> " id="qiu">&nbsp;&nbsp;&nbsp;<strong>我要发布求购信息</strong>&nbsp;&nbsp;&nbsp;<span><a href="javascript:;" id="changetofa">我要发布供应信息</a></span></div>
                                <div style="font-size:14px; <%=model==null?"":(model.ExchangeCategory== EyouSoft.Model.CommunityStructure.ExchangeCategory.求?"display:none;":"") %>" id="fa">&nbsp;&nbsp;&nbsp;<span><a href="javascript:;" id="changetoqiu">我要发布求购信息</a></span>&nbsp;&nbsp;&nbsp;<strong>我要发布供应信息</strong></div>
                                <input type="hidden" name="applycat" id="applycat" value="<%=model==null?"1":(model.ExchangeCategory== EyouSoft.Model.CommunityStructure.ExchangeCategory.供?"1":"2") %>" />  
                                
                                <div style="clear: both">
                                </div>
                               <div style="line-height: 20px; display:inline;">
                                <table>
                                    <tr>
                                        <td><span class="lanse"><em style="color:Red; margin-right:5px;">*</em>类别：</span></td>
                                        <td>
                                        <%=ExchangeTypeHtmlq%><%=ExchangeTypeHtmlg%>&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_rbtExchangeType" style="position:absolute;"></span>
                                        </td>
                                    </tr>
                                </table>                                                                            
                                </div>
                                <div style="clear: both">
                                </div>
                                <div style="width: 550px; margin-bottom: 5px;">
                                    <div class="gqbqx1">
                                        <span class="lanse"><em style="color:Red; margin-right:5px;">*</em>标签：</span></div>
                                       <%=ExchangeTagHtml%>&nbsp;&nbsp;&nbsp;<span class="errmsg" id="errMsg_rbtExchangeTag" style="position:absolute;"></span>
                                </div>
                                
                                <div style="width: 550px; margin-bottom: 5px;">
                                    <div class="gqbqx1"><span class="lanse"><em style="color:Red; margin-right:5px;">*</em>标题：</span></div><input type="text" id="jtitle" name="applytitle" size="50" value="<%= model==null?"请填写供应信息标题":model.ExchangeTitle %>" />
                                </div>
                                

                                <div style="margin-top: 5px; width: 550px;">
                                    <textarea name="AddSupplyInfo_ExchangeText" id="txt_AddSupplyInfo_ExchangeText" 
                                    style=" height: 130px; width: 540px; border: 1px solid #666; font-size:13px;"  ><%=ExchangeText%></textarea>
                                    <input type="hidden" id="ExchangeText" valid="custom" custom="AddSupplyInfo.checkExchanageText" errmsg="请填写供求信息内容!" />
                                </div>
                                <div style="height:20px;display:none;"><span id="errMsg_ExchangeText" style="position:absolute;" class="errmsg">请填写供求信息内容</span></div>
                                <div style="width: 538px;border: 1px solid #ccc;
                                    padding: 0px 0px 3px 10px; cursor: pointer; position:relative;">
                                  
                                     <a id="a_AddSupplyInfo_Show" style="cursor:pointer; ">默认信息为本地发送，点击此处将此信息发布到以下区域<span class="gqlvse"><strong id="s_AddSupplyInfo_OpenOrClose">（展开）</strong></span></a>
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                   
                                     <div style="position:absolute; left:360px; top:0px; margin-right:0px;">                                       
                                      <uc3:AttatchPathFileUpload ID="AttatchPathFileUpload1" runat="server" />
                                     </div>
                                      <div id="div_AddSupplyInfo_AttatchPath" style="margin-right:0px; margin-top:0px; margin-left:180px;">                                      
                                      </div>                                
                                </div>
                                <div id="div_AddSupplyInfo_Province" style="border: 1px solid #ccc; border-top: 0px; width: 542px;
                                     display: none;" >
                                   <%=ExchangeProvinceHtml %>                                   
                                  <a> <input type="checkbox" id="ckb_All" /><label for="ckb_All">全选</label></a>
                                </div>
                                <div style="text-align: left; padding: 3px 5px 0 0; width: 540px;">
                                    <table width="100%" height="25" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="right" width="100">
                                                联系人：
                                            </td>
                                            <td align="left">
                                                <input type="text" id="txt_AddSupplyInfo_UserName"  name="AddSupplyInfo_UserName" style="width: 60px; border: 1px solid #ccc; height: 16px; font-size: 14px;
                                                    color: #666; padding-top: 0px;" value="<%=ConnectName %>" />
                                            </td>
                                            <td>
                                                MQ：
                                            </td>
                                            <td align="left">
                                                <input type="text" name="AddSupplyInfo_MQ" readonly="readonly" style="width: 60px; border: 1px solid #ccc; height: 16px; font-size: 14px;
                                                    color: #666; padding-top: 0px;" value="<%=MQ %>" />
                                            </td>
                                            <td>
                                                联系电话：
                                            </td>
                                            <td align="left">
                                                <input name="AddSupplyInfo_Tel" id="txt_AddSupplyInfo_Tel" type="text" style="width: 90px; border: 1px solid #ccc; height: 16px;
                                                    font-size: 14px; color: #666; padding-top: 0px;" value="<%=Tel %>" />
                                                   
                                            </td>
                                        </tr>
                                       <%-- <tr>
                                            <td align="right">
                                                上传图片：<br /> 
                                                <span style="text-align:left;color:Blue">(宽144*高89像素)</span>                                                                                                                                                         
                                            </td>
                                            <td align="left"  colspan="5">
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                           &nbsp; 1
                                                        </td>
                                                        <td >
                                                            <uc2:sznb2 id="Upload_SupplyInfoImge1" runat="server"  ImageWidth="144" ImageHeight="89" />
                                                            <input id="hid_MyOwenerShop_Upload_ProductInfo1" name="hid_MyOwenerShop_Upload_ProductInfo1" value="<%=SupplyInfoImgePath1 %>" type="hidden" />                                                                                                                                                                                                                                                       
                                                        </td>
                                                         <td  style="width:60px;">
                                                        <a href="<%=Domain.FileSystem+SupplyInfoImgePath1 %>"  target="_blank"><%=SupplyInfoImgePath1 != "" ? "查看图片" : ""%></a>
                                                        <input id="hidImageId1" name="hidImageId1" value="<%=imgId1 %>" type="hidden" />
                                                        </td>                                                                                                                                                                 
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           &nbsp; 2
                                                        </td>
                                                        <td >
                                                          <uc2:sznb2 id="Upload_SupplyInfoImge2" runat="server"  ImageWidth="144" ImageHeight="89" />
                                                          <input id="File2" name="hid_MyOwenerShop_Upload_ProductInfo2" value="<%=SupplyInfoImgePath2 %>" type="hidden" />
                                                          
                                                        </td>
                                                        <td style="width:60px;">
                                                        <a href="<%=Domain.FileSystem+SupplyInfoImgePath2 %>"  target="_blank"><%=SupplyInfoImgePath2 != "" ? "查看图片" : ""%></a>
                                                        <input id="hidImageId2" name="hidImageId2" value="<%=imgId2 %>" type="hidden" />
                                                        </td>
                                                      
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           &nbsp; 3
                                                        </td>
                                                        <td>
                                                            <uc2:sznb2 id="Upload_SupplyInfoImge3" runat="server"  ImageWidth="144" ImageHeight="89" />
                                                             <input  name="hid_MyOwenerShop_Upload_ProductInfo3"  value="<%=SupplyInfoImgePath3 %>" type="hidden" />
                                                                                                                    
                                                        </td>
                                                         <td style="width:60px;">
                                                        <a href="<%=Domain.FileSystem+SupplyInfoImgePath3 %>"  target="_blank"><%=SupplyInfoImgePath3 != "" ? "查看图片" : ""%></a>
                                                        <input id="hidImageId3" name="hidImageId3" value="<%=imgId3 %>" type="hidden" />
                                                        </td>
                                                                                                                                                             
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           &nbsp; 4
                                                        </td>
                                                        <td>
                                                         <uc2:sznb2 id="Upload_SupplyInfoImge4" runat="server"  ImageWidth="144" ImageHeight="89" />
                                                         <input id="File4" name="hid_MyOwenerShop_Upload_ProductInfo4" value="<%=SupplyInfoImgePath4 %>" type="hidden" />                                                                                                                                                                                      
                                                        </td>
                                                           <td style="width:60px;">
                                                        <a href="<%=Domain.FileSystem+SupplyInfoImgePath4 %>"  target="_blank"><%=SupplyInfoImgePath4 != "" ? "查看图片" : ""%></a>
                                                        <input id="hidImageId4" name="hidImageId4" value="<%=imgId4 %>" type="hidden" />
                                                        </td>                                                                                                                                                                   
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                    </table>
                                </div>
                                <div style="width:600px; padding-top:5px;">
                                    <a href="javascript:void(0)" id="btn_AddSupplyInfo_save">
                                        <img src='<%=ImageServerUrl %>/images/<%= publishCount >= 15 ? "gqfban_old.gif" : "gqfban.gif"%>' width="161" height="33" border="0" />
                                    </a>&nbsp;&nbsp;<span style=" color:Red;font-weight:bold; font-size:16px; height:25px; line-height:25px;"><%= publishCount>=15?"今天已发布15条供求信息，发布条数已满。":"" %></span>
                                </div>
                                <div id="div_AddSupplyInfo_SaveUploadFile" style="color:Red;"></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <input id="hidExchangeID"  name="ExchangeID" value="<%=ExchangeID %>" type="hidden" />        
        <input id="hidAttatchPath"  name="AttatchPath" value="<%=AttatchPath %>" type="hidden" />
       <script language="javascript" type="text/javascript">          
            var AddSupplyInfo={
                action:"<%=ActionType %>",
                AttatchPathFileUpload1:<%=AttatchPathFileUpload1.ClientID %>,
                save:function(){
                        $("#btn_AddSupplyInfo_save").hide();
                        $("#div_AddSupplyInfo_SaveUploadFile").show().html("正在保存，请稍后...")   
                        $.newAjax({
                           type: "POST",
                           url: "/SupplyInformation/AddSupplyInfo.aspx?action="+AddSupplyInfo.action,
                           data:$($("#txt_AddSupplyInfo_UserName").closest("form").get(0)).serialize(),
                           success: function(msg){
                             $("#btn_AddSupplyInfo_save").show();
                             var returnMsg=eval(msg);                         
                             if(returnMsg)
                             {
                                if(returnMsg[0].isSuccess){
                                    $("#div_AddSupplyInfo_SaveUploadFile").html("保存成功...");
                                    topTab.url(topTab.activeTabIndex,"/supplyinformation/allsupplymanage.aspx"); 
                                }
                                alert(returnMsg[0].ErrorMessage)
                                $("#div_AddSupplyInfo_SaveUploadFile").hide();
                             }else{
                                $("#div_AddSupplyInfo_SaveUploadFile").html("保存失败...").hide()
                                alert('对不起，保存失败！')
                             }                             
                           },error:function(){
                                $("#div_AddSupplyInfo_SaveUploadFile").hide();
                                alert("对不起，保存失败！");
                           }
                        },true);
                    
                },
                checkExchanageText:function(e,formElements){
                    var text = KE.text('AddSupplyInfo_ExchangeText');
                    var html = KE.html('AddSupplyInfo_ExchangeText');
                    $("#txt_AddSupplyInfo_ExchangeText").val(html);
                    if(text==""){
                        return false;
                    }else{
                        return true;
                    }
                },
                upFile_function:function(fileObj,callBack){
                                         
                     if(fileObj.getStats().files_queued>0)
                     {  
                        fileObj.customSettings.UploadSucessCallback = callBack;
                        fileObj.startUpload();
                     }
                    else
                     {
                         callBack();
                     }
                },
                upFile_AttatchFile:function(){//附件                    
                    AddSupplyInfo.upFile_function(AddSupplyInfo.AttatchPathFileUpload1,AddSupplyInfo.save);
                },
//                upFile_SupplyInfoImge1:function(){
//                    AddSupplyInfo.upFile_function(AddSupplyInfo.Upload_SupplyInfoImge1,AddSupplyInfo.upFile_SupplyInfoImge2);
//                },
//                upFile_SupplyInfoImge2:function(){
//                    AddSupplyInfo.upFile_function(AddSupplyInfo.Upload_SupplyInfoImge2,AddSupplyInfo.upFile_SupplyInfoImge3);
//                },
//                upFile_SupplyInfoImge3:function(){
//                    AddSupplyInfo.upFile_function(AddSupplyInfo.Upload_SupplyInfoImge3,AddSupplyInfo.upFile_SupplyInfoImge4);
//                },
//                upFile_SupplyInfoImge4:function(){
//                    AddSupplyInfo.upFile_function(AddSupplyInfo.Upload_SupplyInfoImge4,AddSupplyInfo.save);
//                },
                pageInit:function(){
                    $("#a_AddSupplyInfo_Show").toggle(function(){                        
                            $("#s_AddSupplyInfo_OpenOrClose").text("（收缩）");  
                            $("#div_AddSupplyInfo_Province").show();           
                        },function(){
                             $("#s_AddSupplyInfo_OpenOrClose").text("（展开）");
                             $("#div_AddSupplyInfo_Province").hide();
                        });
                    $("#btn_AddSupplyInfo_save").click(function(){
                        if ("<%=publishCount %>" < 15) {
                            if ($.trim($("#jtitle").val()) === "请填写求购信息标题" || $.trim($("#jtitle").val()) === "请填写供应信息标题") {
                                alert("请填写标题！");
                                $("#jtitle").focus();
                                return false;
                            }
                            var boolSameTime = GetAjaxSameTitle($.trim($("#jtitle").val()));
                            if (boolSameTime == "false") {
                                if( ValiDatorForm.validator($("#a_AddSupplyInfo_Show").closest("form").get(0),"alertspan")){
                                    AddSupplyInfo.upFile_AttatchFile();
                                }
                            }else {
                                alert("您已经发布过该供求信息，请修改后重新发布！");
                                return false;
                            }
                            return false;
                        }
                    });                   
                    $("#ckb_All").click(function(){
                        var province_CheckBox=$("#div_AddSupplyInfo_Province input");                       
                        var isCheckAll= $(this).attr("isCheckAll");
                        var strChecked=this.checked?"checked":"";
                        var checkText="全选";
                        if(isCheckAll){
                            $(this).removeAttr("isCheckAll");                                                        
                            strChecked="";
                        }else{                            
                            strChecked=this.checked?"checked": "";
                            checkText=this.checked?"清空": "全选";
                            $(this).attr("isCheckAll","true")
                        }
                        $("label[for='ckb_All']").text(checkText);                                                
                        province_CheckBox.each(function(i){                             
                                $(this).attr("checked",strChecked);                             
                        });
                    });                    
                }
            };
            $(function(){
                setTimeout(
                    function(){
                      KE.create('AddSupplyInfo_ExchangeText',0);//创建编辑器
                      //KE.html('AddSupplyInfo_ExchangeText',htmlDecode($("#AddSupplyInfo_ExchangeText").html())) //赋值
                       $("#jtitle").get(0).focus();
                    },100);
                $("#changetoqiu").click(function() {
                    $("#qiu").show();
                    $("#fa").hide();
                    $("#applycat").val(2);
                    $("#ExchangeTypeq").show();
                    $("#ExchangeTypeg").hide();
                    $("#jtitle").focus().blur();
                    return false;
                });
                $("#changetofa").click(function() {
                    $("#fa").show();
                    $("#qiu").hide();
                    $("#applycat").val(1);
                    $("#ExchangeTypeq").hide();
                    $("#ExchangeTypeg").show();
                    $("#jtitle").focus().blur();
                    return false;
                });
                
                ///标题效果
                $("#jtitle").focus(function() {
                    var that = $(this);
                    if (that.val() === '请填写供应信息标题' || that.val() === '请填写求购信息标题') {
                        that.val('');
                    }
                }).blur(function() {
                    var that = $(this);
                    if (!that.val()) {
                        that.val($("#applycat").val() === '1' ? '请填写供应信息标题' : '请填写求购信息标题');
                    }
                });
                AddSupplyInfo.pageInit();
                
                //FV_onBlur.initValid($("#div_AddSupplyInfo_Province").closest("form").get(0));
            });
            
            function GetAjaxSameTitle(title) {
             var m = true;
             $.newAjax({
                 type: "POST",
                 url: "/SupplyInformation/AddSupplyInfo.aspx?ExchangeID=<%=ExchangeID %>&type=sametitle&titleinfo=" + escape(title),
                 cache: false,
                 async: false,
                 success: function(results) {
                     m = results;
                 }
             });
             return m;
         }
        </script>
</asp:content>
