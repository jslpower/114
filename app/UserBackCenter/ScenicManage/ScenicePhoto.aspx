<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicePhoto.aspx.cs" Inherits="UserBackCenter.ScenicManage.ScenicePhoto" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<asp:content id="ScenicePhoto" runat="server" contentplaceholderid="ContentPlaceHolder1">
<link href="<%=CssManage.GetCssFilePath("rightnew") %>" rel="stylesheet" type="text/css" />
<style>
    #divPicList ul
    {
        width: 100%;
        float: left;
        list-style: none;
        margin: 0;
    }
    #divPicList li
    {
        padding: 3px 7px 3px 7px;
        width: 162px;
        line-height: 20px;
        float: left;
        height: 160px;
    }
</style>

<table border="0" align="center" cellpadding="3" cellspacing="0"  class="margintop5 padd5" style="width:100%;">
        <tr>
            <td align="left" valign="top"><strong>景区图片上传</strong></td>
        </tr>
        <tr>
            <td align="left" valign="top">
                为了更加直观的把贵景区呈现给客人，请上传清晰的图片，景区景点和活动等图片请在“更多”选项中上传。<br />
                图片要求：并在2M以内。<br />
                图片格式为jpg，png格式<br />
                请勿提供处理后的效果图<br />
                图片上不要有水印，文本文字等内容 
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#9dc4dc">
                    <tr >
                        <td width="16%" align="right" bgcolor="#f2f9fe">
                                        景区名称：
                        </td>
                        <td colspan="2" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="DpScenicArea" runat="server">
                            </asp:DropDownList>
                            <input type="hidden" id="sceniceId" value="<%=scenicid %>" />
                        </td>
                    </tr> 
                    <tr>
                        <td width="16%" align="right" bgcolor="#f2f9fe">
                            景区形象照片：
                        </td>
                        <td width="63%" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                    <uc1:SingleFileUpload ID="SfpScenicimage" runat="server" IsGenerateThumbnail="true" IsUploadSwf="false" />
                                    </td>
                                    <td>
                                    图片说明<input type="text" name="txtSceniceimage" readonly="readonly" id="txtSceniceimage" runat="server" /> <span id="errMsgsfuPhotoImg" class="errmsg"></span>
                                <asp:Button ID="Btn_Scenicimage" runat="server" Text="上 传"/>本图片作为列表和景区详细展示使用
                                    </td>
                                </tr>
                            </table>                           
                        </td>
                        <td width="21%" bgcolor="#FFFFFF">
                            <a href="<%=Utils.GetNewImgUrl(modelScenicImg1.Address,3)%>" target="_blank"><img id="Scenicimage" src="<%=Utils.GetNewImgUrl(modelScenicImg1.ThumbAddress,3) %>" alt="" class="hotel_pic" style="margin:15px" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="right" bgcolor="#f2f9fe">
                            景区导览图：
                        </td>
                        <td width="63%" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <uc1:SingleFileUpload ID="SfpScenicview" runat="server" IsGenerateThumbnail="true" IsUploadSwf="false" />
                                    </td>
                                    <td>
                                    图片说明<input type="text" name="txtSceniceview" readonly="readonly" id="txtSceniceview" runat="server" />
                                        <span id="errScenicview" class="errmsg"></span>
                                        <input id="Hidden2" type="hidden" runat="server" />
                                        <asp:Button ID="Btn_Scenicview" runat="server" Text="上 传" />                                           景区导览图
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="21%" bgcolor="#FFFFFF">
                           <a href="<%=Utils.GetNewImgUrl(modelScenicImg2.Address,3) %>" target="_blank"> <img id="Scenicview" src="<%=Utils.GetNewImgUrl(modelScenicImg2.ThumbAddress,3) %>" width="120" height="120" alt="" class="hotel_pic" style="margin:15px" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" align="right" bgcolor="#f2f9fe">
                            更多图片：
                        </td>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td><uc1:SingleFileUpload ID="SfpScenicimages" runat="server"  IsGenerateThumbnail="true" IsUploadSwf="false" /></td><td>图片说明<input type="text" name="txtDescription" id="txtDescription" /><span id="errMsg_txtDescription" style="display:none; color:Red;">请输入图片说明</span><asp:Button ID="Btn_Scenicimages" runat="server" Text="上 传"  />
                                    <span id="errScenicimages" class="errmsg"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                   </tr>
                   <tr>
                   <td colspan="2">
                        <div id="divPicList" style="width:100%;">
                        </div>
                   </td>
                   </tr>
                </table>
            </td>
        </tr>
</table>
<script type="text/javascript">
        var sfu1 = <%=SfpScenicimage.ClientID %>;//景区形象照片
        var sfu2 = <%=SfpScenicview.ClientID %>;//景区导览图
        var sfu3 = <%=SfpScenicimages.ClientID %>;//更多图片
        $(function() {
            GetPicList();
            //景区形象照片
            $("#<%=Btn_Scenicimage.ClientID %>").click(function() {
                if (sfu1.getStats().files_queued <= 0) {
                    $("#errMsgsfuPhotoImg").html("请上传大图片");
                    return  false;                        
                }
                if(sfu1.getStats().files_queued > 0){
                    $("#errMsgsfuPhotoImg").html("");
                }
                if (sfu1.getStats().files_queued > 0) {//有图片
                    sfu1.customSettings.UploadSucessCallback = FormSubmit;
                    sfu1.startUpload();
                    return false;
                }
            });
            //景区导览图
            $("#<%=Btn_Scenicview.ClientID %>").click(function() {
                if (sfu2.getStats().files_queued <= 0) {
                    $("#errScenicview").html("请点击上传图片");
                    return  false;
                }
                if(sfu2.getStats().files_queued > 0){
                    $("#errScenicview").html("");
                }
                if (sfu2.getStats().files_queued > 0) {//有图片
                    sfu2.customSettings.UploadSucessCallback = FormSubmit1;
                    sfu2.startUpload();
                    return false;
                }
            });
            //更多图片
            $("#<%=Btn_Scenicimages.ClientID %>").click(function() {
                if (sfu3.getStats().files_queued <= 0) {
                    $("#errScenicimages").html("请点击上传图片");
                    return false;
                }
                if(sfu3.getStats().files_queued > 0){
                    $("#errScenicimages").html("");
                }
                if($("#txtDescription").val()==""){
                    $("#errMsg_txtDescription").show();
                    $("#txtDescription").focus();
                    return false;
                }
                else{
                    $("#errMsg_txtDescription").hide();
                }
                if (sfu3.getStats().files_queued > 0) {//有图片
                    sfu3.customSettings.UploadSucessCallback = FormSubmit2;
                    sfu3.startUpload();
                    return false;
                }
            });
            
            $("#<%=DpScenicArea.ClientID %>").change(function(){
                var sceniceId=$("#<%=DpScenicArea.ClientID %>").val();
                //alert(sceniceId);
                topTab.url(topTab.activeTabIndex,'/ScenicManage/ScenicePhoto.aspx?EditId='+sceniceId);
            })
        
        });

        function FormSubmit() {      
            var filevalue=$("#ctl00_ContentPlaceHolder1_SfpScenicimage_hidFileName").val();
            var scenicid=$("#sceniceId").val();
            var Description=$("#imgDescription").val();
             $.newAjax({
                    url:"/ScenicManage/ScenicePhoto.aspx?type=Scenicimage&filevalue="+filevalue+"&EditId="+scenicid+"&Description="+Description ,
                    dataType:"text",
                    cache:false,
                    type:"post",
                    success:function(result){
                        alert(result);
                        topTab.url(topTab.activeTabIndex,'/ScenicManage/ScenicePhoto.aspx?EditId=<%=Request.QueryString["EditId"]%>');
//                        $("#imgDescription").val("");
//                        var Arry=filevalue.split('|');
//                        $("#Scenicimage").attr("src","<%=Domain.FileSystem%>"+Arry[0]);
                    }
             })
             return false;
        }
        function FormSubmit1() {
            var filevalue=$("#ctl00_ContentPlaceHolder1_SfpScenicview_hidFileName").val()
            var scenicid=$("#sceniceId").val();
            var Description=$("#vieDescription").val();
           $.newAjax({
                    url:"/ScenicManage/ScenicePhoto.aspx?type=Scenicview&filevalue="+filevalue+"&EditId="+scenicid+"&Description="+Description ,
                    dataType:"text",
                    cache:false,
                    type:"post",
                    success:function(result){
                        alert(result);
                        topTab.url(topTab.activeTabIndex,'/ScenicManage/ScenicePhoto.aspx?EditId=<%=Request.QueryString["EditId"]%>');
//                        $("#vieDescription").val("");
//                        var Arry=filevalue.split('|');
//                        var src="<%=Domain.FileSystem%>"+Arry[0];
//                        $("#Scenicview").attr("src",src);
                    }
                })
                return false;
        }
        function FormSubmit2() {
            var filevalue=$("#ctl00_ContentPlaceHolder1_SfpScenicimages_hidFileName").val()
            var scenicid=$("#sceniceId").val();
            var Description=$("#txtDescription").val();
            $.newAjax({
                    url:"/ScenicManage/ScenicePhoto.aspx?type=Scenicimages&filevalue="+filevalue+"&EditId="+scenicid+"&Description="+encodeURIComponent(Description) ,
                    dataType:"text",
                    cache:false,
                    type:"post",
                    success:function(result){
                      alert(result);
                      topTab.url(topTab.activeTabIndex,'/ScenicManage/ScenicePhoto.aspx?EditId=<%=Request.QueryString["EditId"]%>');
                      //GetPicList();
//                      $("#txtDescription").val("");
                    }
                })
                return false;
        }
        
        function GetPicList(){
            //alert("aa");
            var scenicid=$("#sceniceId").val();
            $.ajax({
                type: "GET",
                dataType: 'html',
                url: "/ScenicManage/GetImageList.ashx?EditId="+scenicid,
                cache: false,
                success: function(html) {
                    $("#divPicList").html(html);
                }
            });
        }
        
        function deleteImage(ImgId){
            if(confirm("确定删除该图片")){
                $.ajax({
                    url:"/ScenicManage/GetImageList.ashx?ImgId="+ImgId,
                    dataType:"text",
                    success:function(result){
                        alert(result);
                        GetPicList();
                    }
                });
            }
        }
        
        $("#txtDescription").keydown(function(event) {
            if (event.keyCode == 13) {
                $("#<%=Btn_Scenicimages.ClientID %>").click();
            }
        });
        
</script>
    
</asp:content>
