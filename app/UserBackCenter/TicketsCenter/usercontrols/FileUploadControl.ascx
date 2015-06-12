<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploadControl.ascx.cs" Inherits="UserBackCenter.TicketsCenter.usercontrols.FileUploadControl" %>
<div style="margin: 0px 10px;">
    <div>
        <input type="hidden" id="hidFileName" runat="server" />
        <span runat="server" id="spanButtonPlaceholder"></span>
        <span id="errMsg_<%=hidFileName.ClientID %>" class="errmsg"></span>
    </div>
    <div id="divFileProgressContainer" runat="server">
    </div>
    <div id="thumbnails">
    </div>
</div>  

<script type="text/javascript">
	var <%=this.ClientID %>;
		//$(function () {
		   //alert("<%=this.ClientID %>");
			    <%=this.ClientID %> = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/TicketCenter/upload.aspx",
				    file_post_name:"Filedata",
                    post_params : {
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value %>",
                        "ImageWidth":"<%=_ImageWidth %>",
                        "ImageHeight":"<%=_ImageHeight %>",
                       "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_Password %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password].Value:string.Empty %>"
                    },

				    // File Upload Settings
				    file_size_limit : "1 MB",
				    file_types : "*.jpg;*.png;*.gif;*.jpeg<%=!_IsUploadSwf?"":";*.swf" %>",
				    file_types_description : "图像文件<%=!_IsUploadSwf?"":"或者Falsh文件" %>",
				    file_upload_limit : "1",    // Zero means unlimited

				    // Event Handler Settings - these functions as defined in Handlers.js
				    //  The handlers are not part of SWFUpload but are part of my website and control how
				    //  my website reacts to the SWFUpload events.
				    swfupload_loaded_handler :function(){document.title="<%=pageTitle %>"},
				    file_dialog_start_handler : fileDialogStart,
				    file_queued_handler : fileQueued,
				    file_queue_error_handler : fileQueueError,
				    file_dialog_complete_handler : fileDialogComplete,
				    upload_progress_handler : uploadProgress,
				    upload_error_handler : uploadError,
				    upload_success_handler : uploadSuccess,
				    upload_complete_handler : uploadComplete,

				    // Button settings
				    button_image_url : "<%=ImageServerUrl%>/images/swfupload/XPButtonNoText_160x22.png",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 160,
				    button_height: 22,
				    button_text : '<span class="button">选择图像文件<span class="buttonSmall">(最大1 MB)</span></span>',
				    button_text_style : '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
				    button_text_top_padding: 1,
				    button_text_left_padding: 5,
				    button_cursor: SWFUpload.CURSOR.HAND, 

				    // Flash Settings
				    flash_url : "<%=ImageServerUrl%>/js/swfupload/swfupload.swf",	// Relative to this file

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
			    
			    try{
			        currentSwfuploadInstances.push( <%=this.ClientID %>);
			    }
			    catch(e){}
			    
		   // } );
</script>