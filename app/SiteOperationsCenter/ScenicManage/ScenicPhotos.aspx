<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicPhotos.aspx.cs" Inherits="SiteOperationsCenter.ScenicManage.ScenicPhotos" %>

<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>景区管理-照片</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

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
            width: 132px;
            line-height: 25px;
            float: left;
            height: 160px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="1" cellspacing="0" class="table_basic">
        <tr>
            <td align="left" valign="top">
                <strong>景区图片上传</strong>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                为了更加直观的把贵酒店呈现给客人，请上传整体外观、酒店招牌、大堂及合作房型的图片，餐厅等设施图片请在“其他”选项中上传。
                <br />
                图片要求：分辨率需大于800×600（500像素以上相机拍摄），并在5M以内。
                <br />
                图片格式必须为jpg格式<br />
                请勿提供处理后的效果图
                <br />
                图片上不要有水印，文本文字等内容
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <table width="98%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#cccccc"
                    class="lr_hangbg table_basic">
                    <tr>
                        <td width="16%" align="right">
                            所属景区：
                        </td>
                        <td colspan="2" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="DpScenicArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DpScenicArea_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            景区形象照片：
                        </td>
                        <td width="63%" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <uc1:SingleFileUpload ID="SfpScenicimage" runat="server" IsGenerateThumbnail="true" />
                                        <span id="errMsgsfuPhotoImg" class="errmsg"></span>
                                    </td>
                                    <td>
                                        图片说明:
                                        <input id="Scenicimagedes" runat="server" type="text" readonly="readonly" />
                                        <asp:Button ID="Btn_Scenicimage" runat="server" Text="上 传" OnClick="Btn_Scenicimage_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="21%" bgcolor="#FFFFFF">
                            <% if (modelScenicImg1.Address != null)
                               { %>
                            <a href="<%=Utils.GetNewImgUrl(modelScenicImg1.Address,3) %>" target="_blank">
                                <img src="<%=Utils.GetNewImgUrl(modelScenicImg1.Address,3) %>" onerror="this.src='<%=Utils.GetNewImgUrl("",2)%>';"
                                    width="120" height="120" alt="" class="jq_pic" /></a>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            景区导游地图：
                        </td>
                        <td width="63%" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <uc1:SingleFileUpload ID="SfpScenicview" runat="server" IsGenerateThumbnail="true" />
                                        <span id="errScenicview" class="errmsg"></span>
                                    </td>
                                    <td>
                                        图片说明:
                                        <input id="Scenicviewdes" runat="server" type="text" readonly="readonly" />
                                        <asp:Button ID="Btn_Scenicview" runat="server" Text="上 传" OnClick="Btn_Scenicview_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="21%" bgcolor="#FFFFFF">
                            <% if (modelScenicImg2.Address != null)
                               { %>
                            <a href="<%=Utils.GetNewImgUrl(modelScenicImg2.Address,3) %>" target="_blank">
                                <img src="<%=Utils.GetNewImgUrl(modelScenicImg2.Address,3) %>" onerror="this.src='<%=Utils.GetNewImgUrl("",2)%>';"
                                    width="120" height="120" alt="" class="jq_pic" /></a>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" align="right">
                            更多图片：
                        </td>
                        <td colspan="2" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <uc1:SingleFileUpload ID="SfpScenicimages" runat="server"  IsGenerateThumbnail="true" />
                                        <span id="errScenicimages" class="errmsg"></span>
                                    </td>
                                    <td>
                                        图片说明:
                                        <input type="text" name="txtDescription" id="txtDescription" runat="server" />
                                        <asp:Button ID="Btn_Scenicimages" runat="server" Text="上 传" OnClick="Btn_Scenicimages_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" bgcolor="#FFFFFF">
                            <div id="divPicList" style="width: 100%;">
                                <ul>
                                    <asp:Repeater ID="RephotoList" runat="server">
                                        <ItemTemplate>
                                            <li id="li<%# Eval("ImgId") %>" title="<%#Eval("Description") %>"><a href="<%# Utils.GetNewImgUrl(Eval("Address").ToString().Split('|')[0],3) %>"
                                                target="_blank">
                                                <img width="12" height="120" src="<%# Utils.GetNewImgUrl(Eval("Address").ToString().Split('|')[0],3) %>"
                                                    onerror="this.src='<%=Utils.GetNewImgUrl("",2)%>';" class="hotel_pic" /></a>
                                                <div style='width: 130px; text-align: center; height: 20px; line-height: 25px; margin-top: -10px'
                                                    title="<%#Eval("Description") %>">
                                                    <%# Utils.GetText2(Eval("Description").ToString(),7,true) %>
                                                    &nbsp;&nbsp;<a onclick="DelImage('<%# Eval("ImgId") %>')" href="javascript:void(0)">删除</a>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <li></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    <%--        <tr>
            <td colspan="2" bgcolor="#FFFFFF" id="<%# Eval("ImgId")  %>">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <asp:Repeater ID="RephotoList" runat="server">
                            <ItemTemplate>
                                <td>
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <img src="<%=Domain.FileSystem%><%# Eval("Address") %>" width="120" height="120"
                                                    alt="" class="jq_pic" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <%#Eval("Description")%>
                                                <a href="#" onclick="DelImage('<%# Eval("ImgId") %>')">删除</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </td>
        </tr>--%>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        var sfu1 = <%=SfpScenicimage.ClientID %>;//景区形象照片
        var sfu2 = <%=SfpScenicview.ClientID %>;//景区地图
        var sfu3 = <%=SfpScenicimages.ClientID %>;//更多图片
        var isSubmit = false;

        $(function() {
        
            //景区形象照片
            $("#<%=Btn_Scenicimage.ClientID %>").click(function() {
                if (!isSubmit) {
                    if (sfu1.getStats().files_queued <= 0) {
                        $("#errMsgsfuPhotoImg").html("请选择一张图片");
                        return false;                        
                    }

                    if (sfu1.getStats().files_queued > 0) {//有图片
                        sfu1.customSettings.UploadSucessCallback = FormSubmit;
                        sfu1.startUpload();
                        return false;
                    }
                } else {
                    return true;
                }
            });
        });
        
        
        
      $(function(){
            //景区地图
            $("#<%=Btn_Scenicview.ClientID %>").click(function() {
                if (!isSubmit) {
                    if (sfu2.getStats().files_queued <= 0) {
                        var img = $("#imgpath").attr("href");
                        if (img == undefined || img == "") {
                            $("#errScenicview").html("请选择一张图片");
                            return false;
                        }
                    }

                    if (sfu2.getStats().files_queued > 0) {//有图片
                        sfu2.customSettings.UploadSucessCallback = FormSubmit1;
                        sfu2.startUpload();
                        return false;
                    }
                } else {
                    return true;
                }
            });
        
        })
        
        //初始化图片说明
        $(function(){
           $("#Scenicimagedes").val('<%=scenicname %>'+"_景区形象照片");
           $("#Scenicviewdes").val('<%=scenicname %>'+"_景区导游地图");
        })
        
        
        $(function(){
            //更多图片
            $("#<%=Btn_Scenicimages.ClientID %>").click(function() {
                if (!isSubmit) {
                    if (sfu3.getStats().files_queued <= 0) {
                        var img = $("#imgpath").attr("href");
                        if (img == undefined || img == "") {
                            $("#errScenicimages").html("请选择一张图片");
                            return false;
                        }
                    }

                    if (sfu3.getStats().files_queued > 0) {//有图片
                        sfu3.customSettings.UploadSucessCallback = FormSubmit2;
                        sfu3.startUpload();
                        return false;//这样使异步调用数据，使form表单里面有值
                    }
                } else {
                    return true;
                }
            });
        
        });

        
        function FormSubmit() {
            isSubmit = true;
            $("#<%=Btn_Scenicimage.ClientID %>").click();
        }
        function FormSubmit1() {
            isSubmit = true;
            $("#<%=Btn_Scenicview.ClientID %>").click();
        }
        function FormSubmit2() {
            isSubmit = true;
            $("#<%=Btn_Scenicimages.ClientID %>").click();
        }
        
        
        //删除图片
        function DelImage(id)
        {
            if (!confirm("你确定要删除该条数据吗？")) {
               return false;
            }
            $.ajax({
             url: "AjaxAll.ashx?type=DelImage&arg="+id,
             cache: false,
             type: "post",
             success: function(result) {
                 if(result=="error")
                 {
                    alert("删除失败");
                 }
                 else{
                    $("#li"+id).html("");
                    alert("删除成功");
                    window.location.reload(true);
                 }
             },
             error: function() {
                 alert("操作失败!");
             }    
        });
        }
    
    </script>

</body>
</html>
