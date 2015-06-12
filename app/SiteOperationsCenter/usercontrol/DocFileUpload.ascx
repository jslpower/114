<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DocFileUpload.ascx.cs" Inherits="SiteOperationsCenter.usercontrol.DocFileUpload" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div style="margin: 0px 10px;">
    <div>
        <input type="hidden" id="hidFileName" runat="server" />
        <input type="text" disabled="true" id="txtFileName" runat="server" style="display: none;" /><span
            runat="server" id="spanButtonPlaceholder"></span> <span id="errMsg_<%=hidFileName.ClientID %>"
                class="errmsg"></span>
    </div>
    <div id="divFileProgressContainer" runat="server">
    </div>
</div>

<script type="text/javascript">
		var <%=this.ClientID %>;
		<%=this.ClientID %> = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=Domain.FileSystem %>/SiteOperation/docUpload.aspx",
				    file_post_name:"Filedata",
                    post_params : {
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_SSO].Value %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginMasterCookie_UserName].Value %>",
                        "ToCompanyId":"<%=_ToCompanyId %>",
                        "Module":"<%=(int)_SiteModule %>"
                    },

				    // File Upload Settings
				    file_size_limit : "2 MB",
				    file_types : "*.xls;*.rar;*.pdf;*.doc;*.swf",
				    file_types_description : "附件上传",
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
				    upload_success_handler : uploadSuccess,
				    upload_complete_handler : uploadComplete,

				    // Button settings
				    button_image_url : "<%=ImageManage.GetImagerServerUrl(1) %>/images/swfupload/XPButtonNoText_160x22.png",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 160,
				    button_height: 22,
				    button_text : '<span class="button"><%= _ButtonText %><span class="buttonSmall">(最大2 MB)</span></span>',
				    button_text_style : '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
				    button_text_top_padding: 1,
				    button_text_left_padding: 5,
				    button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
				    button_cursor: SWFUpload.CURSOR.HAND, 

				    // Flash Settings
				    flash_url : "<%=ImageManage.GetImagerServerUrl(1) %>/js/swfupload/swfupload.swf",	// Relative to this file

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
</script>

