<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleFileUpload.ascx.cs" Inherits="SupplyInformation.UserControl.SingleFileUpload" %>
<div style="margin: 0px 10px;">
    <div>
        <input type="hidden" id="hidFileName" runat="server" />
        <input type="text" disabled="true" id="txtFileName" runat="server" style="display:none;" /><span runat="server" id="spanButtonPlaceholder"></span>
        <span id="errMsg_<%=hidFileName.ClientID %>" class="errmsg"></span>
    </div>
    <div id="divFileProgressContainer" style="display:none;" runat="server">
    </div>
    <div id="thumbnails" style="cursor:pointer;">
    </div> 
</div>  
<script type="text/javascript">
		var <%=this.ClientID %>;
		$(function () {
			    <%=this.ClientID %> = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/upload.aspx",
				    file_post_name:"Filedata",
                    post_params : {
                        "ASPSESSID" : "<%=Session.SessionID %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value:"" %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value:"" %>",
                        "ImageWidth":"<%=_ImageWidth %>",
                        "ImageHeight":"<%=_ImageHeight %>"
                    },

				    // File Upload Settings
				    file_size_limit : "1 MB",
				    file_types : "*.jpg;*.png;*.gif;*.jpeg;*.doc;*.docx;",
				    file_types_description : "附件文件",
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
				    //upload_error_handler : uploadError,
				    upload_success_handler : uploadSuccess,
				    upload_complete_handler : uploadComplete,

				    // Button settings
				    button_image_url : "<%=ImageServerUrl%>/images/UserPublicCenter/addfile.png",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 95,
				    button_height: 20,
				    button_text : '',
				    button_text_style : '',
				    button_text_top_padding: 0,
				    button_text_left_padding: 5,

				    // Flash Settings
				    flash_url : "<%=ImageServerUrl%>/js/swfupload/swfupload.swf",	// Relative to this file

				    custom_settings : {
					    upload_target : "<%=divFileProgressContainer.ClientID %>",
				        FileNameId:"<%=txtFileName.ClientID %>",
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
			    
		    } );
</script>