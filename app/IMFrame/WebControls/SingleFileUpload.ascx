<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleFileUpload.ascx.cs" Inherits="IMFrame.WebControls.SingleFileUpload" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div style="margin: 0px;">
    <div>
        <input type="hidden" id="hidFileName" runat="server" />
        <input type="text" disabled="true" id="txtFileName" runat="server" style="display:none;" />
        <span runat="server" id="spanButtonPlaceholder"></span>
        <span id="<%= FType %>" style="display:none;"></span>
    </div>
    <div id="divFileProgressContainer" style="display:none;" runat="server">
    </div>
    <div id="thumbnails"  style="display:none;" style="cursor:pointer;">
    </div> 
</div>  
<script type="text/javascript">
		var <%=this.ClientID %>;
		$(function () {
			    <%=this.ClientID %> = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=EyouSoft.Common.Domain.FileSystem%>/MQ/upload.aspx",
				    file_post_name:"Filedata",
                    post_params : {
                        "mqid": "<%=Utils.GetInt(Request.QueryString[Utils.MQLoginIdKey], 0)%>",
                        "mqpwd":"<%=Utils.GetString(Request.QueryString[Utils.MQPwKey],"") %>",
                        "ImageWidth":"300",
                        "ImageHeight":"300"
                    },
				    // File Upload Settings
				    file_size_limit : "1 MB",
				    file_types : "*.jpg;*.png;*.gif",
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
				    button_image_url : "<%=FPath %>",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 140,
				    button_height: <%=FHeight %>,
				    button_text : '',
				    button_text_style : '',
				    button_text_top_padding: 0,
				    button_text_left_padding: 5,
				    button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,

				    // Flash Settings
				    flash_url : "/swfupload/swfupload.swf",	// Relative to this file

				    custom_settings : {
					    upload_target : "<%=divFileProgressContainer.ClientID %>",
				        FileNameId:"<%=txtFileName.ClientID %>",
				        HidFileNameId:"<%=hidFileName.ClientID %>",
				        ErrMsgId:"<%= FType %>",
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

