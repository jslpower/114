<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportNumFromFile.aspx.cs"
    Inherits="UserBackCenter.SMSCenter.ImportNumFromFile" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .s1
        {
            width: 100px;
        }
        body
        {
            font-size: 12px;
        }
        #mobileList
        {
            margin: 0px;
            padding: 0px;
            list-style-type: none;
            width: 500px;
            overflow: hidden;
            border-left: 1px solid #efefef;
            border-top: 1px solid #efefef;
            margin-top: 10px;
        }
        #mobileList li
        {
            height: 20px;
            line-height: 20px;
            float: left;
            list-style-type: none;
            border-bottom: 1px solid #efefef;
            border-right: 1px solid #efefef;
        }
        #mobileList li.No
        {
            width: 123px;
            text-align: center;
            color: #666666;
            font-weight: bold;
        }
        #mobileList li.CompanyName
        {
            width: 123px;
            text-align: center;
            color: #666666;
            padding-left: 2px;
        }
        #mobileList li.FullName
        {
            width: 123px;
            text-align: center;
            color: #666666;
        }
        #mobileList li.Mobile
        {
            width: 123px;
            text-align: center;
            color: #666666;
            padding-left: 2px;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table height="15" cellspacing="0" cellpadding="0" width="100%" align="center" background="<%=ImageServerUrl%>/images/bian_tou.gif"
            border="0">
            <tbody>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <img src="<%=ImageServerUrl%>/images/ax_goforward.gif" width="18" height="18" style="margin-bottom: 3px;
            vertical-align: middle; float: left" /><span style="float: left; margin-top: 3px;">从文件导入号码</span>
        <div style="margin-left: 10px; margin-bottom: 3px; margin-right: 5px; float: left;">
            <div>
                <input type="hidden" id="hidFileName" runat="server" />
                <span runat="server" id="spanButtonPlaceholder"></span><span id="errMsg_<%=hidFileName.ClientID %>"
                    class="errmsg"></span>
            </div>
            <div id="divFileProgressContainer" runat="server">
            </div>
            <div id="thumbnails">
            </div>
        </div>
        <input type="button" value="确定导入" onclick="inff.upLoad();" />
        <br />
        <div style="clear: left">
            （只能导入格式为.xls和.txt的文件,txt文件必须一个号码一行）<a href="<%=Domain.ServerComponents %>/SMSmodel/短信模板.xls"
                target="_blank"><span style="color: #ff0000;">文件模板下载</span></a></div>
        <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
            <tr>
                <td>
                    <fieldset>
                        <legend>源数据预览&nbsp;&nbsp;&nbsp;&nbsp;</legend>
                        <div style="text-align: center;">
                            <img id="loading" src="<%=ImageServerUrl%>/images/loadingnew.gif" style="display: none;">
                        </div>
                        <ul id="mobileList" style="margin: auto; text-align:center">
                        </ul>
                        <table height="30" cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
                            <tr>
                                <td>
                                    <span id="checkSpan"><a href="javascript:inff.checkAll(0)">全选</a> <a href="javascript:inff.checkAll(1)">
                                        反选</a>&nbsp;&nbsp;<a href="javascript:inff.checkAll(2)">清空</a> </span>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
            <tbody>
                <tr>
                    <td style="padding-top: 15px;">
                        <fieldset>
                            <legend>请设置对应字段</legend>
                            <table cellspacing="0" cellpadding="5" width="98%" align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            手机号码：
                                            <label>
                                                <select id="lstNo" name="lstNo" class="s1">
                                                </select>
                                            </label>
                                        </td>
                                        <td>
                                            单位名称：
                                            <select id="lstCompany" name="lstCompany" class="s1">
                                            </select>
                                        </td>
                                        <td>
                                            姓名：
                                            <select id="lstName" name="lstName" class="s1">
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="shenghui" align="center">
                                        <input name="importTelPhone" runat="server" id="importTelPhone" type="button" value="确定" />
                                        <asp:Button ID="btn_ImportInfo" Text="确定" Visible="false" runat="server" OnClick="btn_ImportInfo_Click">
                                        </asp:Button>
                                        &nbsp;<input name="Candeled" type="button" value="取消" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </tbody>
            <input id="hidTel" type="hidden" name="hidTel" runat="server">
            <input id="hidCompany" type="hidden" name="hidCompany" runat="server">
            <input id="hidName" type="hidden" name="hidName" runat="server">
        </table>
    </div>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript">
       var p = parent;
       var maxLength =1000;
       var strData ="";
       var arrayLength = 0;
       var inff={
           swfObj:null,
           count:0,
           isfirst:true,
           funHandleIeLayout:null,
           isScroll:false,
            handleIELayout:function(){
            if(inff.isScroll){
                inff.isScroll = false;
                document.body.style.display="none";
                document.body.style.display="";
                clearInterval(inff.funHandleIeLayout);
                inff.funHandleIeLayout = null;
                }
             },
             scroll1:function()//IE6 下下拉框BUG
             { 
                inff.isScroll = true;
                clearInterval(inff.funHandleIeLayout);
                inff.funHandleIeLayout=setInterval(inff.handleIELayout,300);
            },
           //创建数据
           createData:function(arr){
              var length=arr.length;
              arrayLength = arr.length;
              if(length==0){alert("未能导入任何数据!");return false;}
              var s=new Array();
              
               //绑定标题
               if(inff.isfirst)
               {  
                  
                  s.push('<li style="background-color: #A2DDFF" class="No">序号</li>');
                  s.push('<li style="background-color: #A2DDFF" class="Mobile">' +  (arr[0].Mobile != null ? arr[0].Mobile : '') + '</li>');
                  s.push('<li style="background-color: #A2DDFF" class="CompanyName">' + (arr[0].CompanyName != null ? arr[0].CompanyName : '')+ '</li>');
                  s.push('<li style="background-color: #A2DDFF" class="FullName">' + (arr[0].FullName != null ? arr[0].FullName : '')   + '</li>');
                  $("#mobileList").append(s.join(''));
                  //为下拉列表设置对应的值
                  
                  var selHTML1="<option selected='selected' value='0'>"+arr[0].Mobile+"</option><option value='1'>"+arr[0].CompanyName+"</option><option value='2'>"+arr[0].FullName+"</option>";
                  var selHTML2="<option  value='0'>"+arr[0].Mobile+"</option><option selected='selected' value='1'>"+arr[0].CompanyName+"</option><option value='2'>"+arr[0].FullName+"</option>";
                  var selHTML3="<option  value='0'>"+arr[0].Mobile+"</option><option  value='1'>"+arr[0].CompanyName+"</option><option selected='selected' value='2'>"+arr[0].FullName+"</option>";
                  $("#lstNo").html(selHTML1);
                  $("#lstCompany").html(selHTML2);
                  $("#lstName").html(selHTML3);
                 
                  inff.isfirst=false;
                  }
              //绑定列表数据
              for (var i = 1; i < arr.length; i++)
              { 
                inff.count++;
	            var s = new Array();
	            if(i<maxLength)
	            {
                s.push('<li class="No"><input type="checkbox" name="chkMessage" checked=\"checked\" id="chk_mobile_' +inff.count+ '"/>' +  inff.count + '</li>');
                s.push('<li class="Mobile">' + (arr[i].Mobile != null ? arr[i].Mobile : '') + '</li>');
                s.push('<li class="CompanyName">' + (arr[i].CompanyName != null ? arr[i].CompanyName : '') + '</li>');
                s.push('<li class="FullName">' + (arr[i].FullName != null ? arr[i].FullName : '') + '</li>');
	            $("#mobileList").append(s.join(''));
	            }
	            if(arr[i].Mobile!=null && arr[i].Mobile!="")
	            {
	                strData +=arr[i].Mobile+",";
	            }
	          }
	          
	          
	          strData = strData.substring(0,strData.length-1);
	          if(arr.length>maxLength)
	          {
	             $("#mobileList").append("<font size=3 color=red>待发送号码:"+arr.length+"条,当前显示999条。</font><br />·<br />·<br />·");
	            $("#checkSpan").html("");
	          }
	          
	          $("#loading").hide();
           },
           upLoad:function(){
                if(inff.swfObj.getStats().files_queued>0)
	            {  
	               $("#loading").show();
                   inff.swfObj.startUpload();
                }
                return true;
           },
           upLoadSuccess:function(file,serverData){
             try 
	          {
	           
	            var obj = eval(serverData);
        	    if(obj.error)
        	    {
                  return;
	            }
	            else
	            {  
	              
	               var progress = new FileProgress(file,  this.customSettings.upload_target);
	               progress.setStatus("上传成功.");
	               inff.createData(obj);
	               resetSwfupload(inff.swfObj,file);
	            }
              } 
	          catch (ex){ }
            },
            checkAll:function(type)// 全选 反选 清空
		    {
			    switch(type)
			    {
			     case 0:
			        $("#mobileList input[type='checkbox']").each(function(){
			            this.checked=true;
			        })
			     break;
			     case 1:
			        $("#mobileList input[type='checkbox']").each(function(){
			            this.checked=(!this.checked);
			        })
			     break;
			     case 2:
			      $("#mobileList input[type='checkbox']").each(function(){
			            this.checked=false;
			      })
			     break;
			    }
		    },
		    //验证手机号码
	     ValidateMobile:function(telephones) {
			var tels = [];
		    var msg = new String();
		    var repeatmsg = new String();
		    var repeatArr = [];
		    var errorArr=[];
		    var trueNum=[];
		    for (var i = 0; i < telephones.length; i++) {
                tels.push(telephones[i].p);
		        var result = telephones[i].p.match(RegExps.isMobile);
		        var result_phone = telephones[i].p.match(RegExps.isPhone);
		        if(result == null && result_phone==null ){
		            msg += telephones[i].p+ ",";
		            errorArr.push(telephones[i].c);
		        }else
		        {
		            //将正确的号码返回
		            trueNum.push(telephones[i].p);
		        }
		        var re = new RegExp(telephones[i].p, "ig");
		        var result = tels.toString().match(re);
		        if (result != null && result.length > 1 && repeatArr[telephones[i].p] != null) {
		            repeatmsg += telephones[i].p + ',';
		            repeatArr[telephones[i].p] = "re";
		        }
		    }
		   
		    if (msg != "") {
               if(confirm(msg +'以上号码格式不正确,是否自动取消无效的号码！')){  
                    for (var k=0;k<errorArr.length ;k++ )    
                    {       
                        errorArr[k].checked=false;
                    }    
               }
               return null;
		    }
		    if (repeatmsg != "") {
		        repeatmsg = repeatmsg + "以上号码出现重复!";
		        window.alert(repeatmsg);
		        return null;
		    }
		    return trueNum;
		 },
		  checkCusPhone:function ()
	      {
			var typeid=<%=pageTypeId %>; //标识是从发送信息页面链接过去的还是从客户列表连接过去的
	        var telephone = $("#lstNo").val();
			var isds = new Array();
			var d0=new  Array();
			var d1=new  Array();
			var d2=new Array();
			if(arrayLength >maxLength)
			{
			     var phoneNum = p.$("#Phone_Txt_Count").html();
	                phoneNum=parseInt(phoneNum)+ parseInt(arrayLength);	                 
	                p.$("#Phone_Txt_Count").html(phoneNum);
                    p.$("#Phone_All_Count").html(phoneNum + parseInt(p.$("#Phone_114_Count").html()));
            
				        if(parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value==""){
					        parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value += strData;
				        }
				        else{
					        parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value += "," + strData;
				        }
				    
				    window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
			}else
			{
			
			$("input[type=checkbox]").each(function(){
				if(this.checked)
				{
				var d=new Array();
				d.push($(this).parent().next().html());//手机号码
				d.push($(this).parent().next().next().html());//单位名称
				d.push($(this).parent().next().next().next().html());//姓名
					//每个字段所选择的列
					var a0=$("#lstNo").val();
					var a1=$("#lstCompany").val();
					var a2=$("#lstName").val();
					
					d0.push(d[a0]);
					d1.push(d[a1]);
					d2.push(d[a2]);
				    isds.push({
				        p:d[a0],
				        c:this
				    });
				}
			}); 
			if(isds.length != 0)
			{
			if(typeid==1){
			    if(confirm("您确定设置这些对应字段吗？"))
			    {
			    d0.join("{~@}");
			    d1.join("{~@}");
			    d2.join("{~@}");
    			
			    $("#hidName").val(d2.join("{~@}"));
			    $("#hidCompany").val(d1.join("{~@}"));
			    $("#hidTel").val(d0.join("{~@}"));
			    }
			}
			var factNum=[];
			factNum=inff.ValidateMobile(isds);
				if (factNum==null)
				  {
	                return false;
	               }
	           if(typeid==0){
	                
	                var phoneNum = p.$("#Phone_Txt_Count").html();
	                phoneNum=parseInt(phoneNum)+ parseInt(isds.length);	                 
	                p.$("#Phone_Txt_Count").html(phoneNum);
                    p.$("#Phone_All_Count").html(phoneNum + parseInt(p.$("#Phone_114_Count").html()));
            
				        if(parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value==""){
					        parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value += factNum.toString();
				        }
				        else{
					        parent.document.getElementById("ctl00_ContentPlaceHolder1_txt_TelePhoneGroup").value += "," + factNum.toString();
				        }
				    
				    window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
				    }
				    else{
				        return true;
				    }
			           }
			        else
			        { 
			         alert("未选择任何数据");return false; 
			        }
		         }
	    }
	 }
           
    
        $(function () {
                 
			     inff.swfObj = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=EyouSoft.Common.Domain.UserBackCenter%>/SMSCenter/UpLoadFile.ashx",
				    file_post_name:"Filedata",
                    post_params : {
                        "ASPSESSID" : "<%=Session.SessionID %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value %>",
                       "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_Password %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password].Value:string.Empty %>"
                    },

				    // File Upload Settings
				    file_size_limit : "1 MB",
				    file_types : "*.txt;*.xls",
				    file_types_description : "",
				    file_upload_limit : "1",    // Zero means unlimited

				    // Event Handler Settings - these functions as defined in Handlers.js
				    //  The handlers are not part of SWFUpload but are part of my website and control how
				    //  my website reacts to the SWFUpload events.
				    swfupload_loaded_handler :swfUploadLoaded,
				    file_dialog_start_handler : fileDialogStart,
				    file_queued_handler : fileQueued,
				    file_queue_error_handler : fileQueueError,
				    file_dialog_complete_handler : fileDialogComplete,
				    upload_progress_handler : uploadProgress,
				    upload_error_handler : uploadError,
				    upload_success_handler : inff.upLoadSuccess,
				    upload_complete_handler : uploadComplete,

				    // Button settings
				    button_image_url : "<%=Domain.ServerComponents%>/images/swfupload/XPButtonNoText_160x22.png",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 160,
				    button_height: 22,
				    button_text : '<span class="button">选择文件<span class="buttonSmall">(最大1 MB)</span></span>',
				    button_text_style : '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
				    button_text_top_padding: 1,
				    button_text_left_padding: 5,
				    button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
				    button_cursor: SWFUpload.CURSOR.HAND, 

				    // Flash Settings
				    flash_url : "<%=Domain.ServerComponents%>/js/swfupload/swfupload.swf",	// Relative to this file

				    custom_settings : {
					    upload_target : "<%=divFileProgressContainer.ClientID %>",
				        HidFileNameId:"<%=hidFileName.ClientID %>",
				        ErrMsgId:"errMsg_<%=hidFileName.ClientID %>",
				        UploadSucessCallback:null
				    },

				    // Debug Settings
				    debug: false,
    				
				    // SWFObject settings
		            minimum_flash_version : "9.0.28",
		            swfupload_pre_load_handler : swfUploadPreLoad,
		            swfupload_load_failed_handler : swfUploadLoadFailed
			    });
			    
			   var typeid=<%=pageTypeId %>;
			   if(typeid==0){
				$("#importTelPhone").click(function(){
				    
					inff.checkCusPhone();
				});
				}
			    
		      if ( $.browser.msie )
               if($.browser.version=="6.0")
               { 
                 window.parent.$('#<%=Request.QueryString["iframeId"] %>').get(0).contentWindow.document.body.onscroll=inff.scroll1;
               }
		    } );
		    
		   
		    
		    
		    
		    
    </script>

</body>
</html>
