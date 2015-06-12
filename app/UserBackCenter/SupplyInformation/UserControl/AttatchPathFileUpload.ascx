<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttatchPathFileUpload.ascx.cs"
    Inherits="SupplyInformation.UserControl.AttatchPathFileUpload" %>
    <%@ Import Namespace="EyouSoft.Common" %>
<div style="vertical-align: top;">
    <table>
        <tr>
            <td>
                <input type="hidden" id="hidFileName" runat="server" />
                <span runat="server" id="spanButtonPlaceholder"></span><span id="errMsg_<%=hidFileName.ClientID %>"
                   class="errmsg" style="position:absolute; width:300px;"></span>
            </td>
            <td>
                <a target="_blank" href="<%=Domain.FileSystem+LastDocFile %>">
                    <%=LastDocFile != "" ? "查看附件" : ""%></a>
            </td>
        </tr>
    </table>
</div>
<div style="display: none;" id="divFileProgressContainer" runat="server">
</div>

<script type="text/javascript">
		var <%=this.ClientID %>;
		$(function () {
			    <%=this.ClientID %> = new SWFUpload({
				    // Backend Settings
				    upload_url: "<%=EyouSoft.Common.Domain.FileSystem%>/UserBackCenter/docUpload.aspx",
				    file_post_name:"Filedata",
                    post_params : {
                        "ASPSESSID" : "<%=Session.SessionID %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO %>": "<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_SSO].Value %>",
                        "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_UserName].Value %>",
                        "ImageWidth":"0",
                        "ImageHeight":"0",
                        "filetype":"worddoc",
                       "<%=EyouSoft.Security.Membership.UserProvider.LoginCookie_Password %>":"<%=Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password]!=null?Request.Cookies[EyouSoft.Security.Membership.UserProvider.LoginCookie_Password].Value:string.Empty %>"
                    },
				    // File Upload Settings
				    file_size_limit : "1 MB",
				    file_types : "*.doc;*.docx;",
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
				    upload_error_handler : uploadError,
				    upload_success_handler : uploadSuccess,
				    upload_complete_handler : uploadComplete,

				    // Button settings
				    button_image_url : "<%=ImageServerUrl%>/images/UserPublicCenter/addfile.png",
				    button_placeholder_id : "<%=spanButtonPlaceholder.ClientID %>",
				    button_width: 100,
				    button_height: 22,
				    button_text : '',
				    button_text_style : '',
				    button_text_top_padding: 1,
				    button_text_left_padding: 5,
				    button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
				    button_cursor: SWFUpload.CURSOR.HAND, 

				    // Flash Settings
				    flash_url : "<%=ImageServerUrl%>/js/swfupload/swfupload.swf",	// Relative to this file

				    custom_settings : {
					    upload_target : "div_AddSupplyInfo_AttatchPath",
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

