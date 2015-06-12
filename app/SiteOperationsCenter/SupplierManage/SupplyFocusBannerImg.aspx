<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplyFocusBannerImg.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.SupplyFocusBannerImg" %>

<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <title>供求首页五张焦点图</title>
    <style>
        #jiaodianList
        {
            list-style: none;
            padding-left: 0;
        }
        #jiaodianList li input
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="5" cellspacing="0" width="100%">
            <tr>
                <td style="border-bottom: 1px solid;" height="20">
                    <strong>
                        <img src="<%= ImageServerUrl %>/images/yunying/icn_pen02.gif" width="13" height="13"></strong>
                    供求管理 &gt; 供求首页五张焦点图
                </td>
            </tr>
        </table>
        <table width="960" border="1" cellspacing="0" cellpadding="8" style="border: 1px solid #ccc;
            padding: 1px;">
            <tr>
                <td width="200" bgcolor="#C0DEF3" style="background: #C0DEF3; height: 28px; text-align: right;
                    font-weight: bold;">
                    五张焦点图片切换<br>
                    （建议图片大小219px*134px）
                </td>
                <td width="760" style="background: #fff; height: 28px; text-align: left;">
                    <!--如果上传的和手工输入的URL都有数值，可以默认为URL的-->
                    <ul id="jiaodianList">
                        <li>1.图片:
                        <%--<div style="height: 25px;">
                                <div style="float: left; line-height: 25px;">--%>
                            <uc1:SingleFileUpload ID="sufLoadImg1" name="sfuPhotoImg" runat="server" IsUploadSwf="true" />
                             <%--</div>
                                <div style="float: left; margin-left: 10px; line-height: 25px;">--%>
                            <asp:Label ID="lblImg1" runat="server" Text=""></asp:Label><%--</div>--%>
                             <%--</div>--%>
                            <input id="hdfoldimgpath1" type="hidden" runat="server" />
                           <%--<div>--%>
                            &nbsp; 链接:
                            <input type="text" runat="server" name="txtFocusImgHref1" id="txtFocusImgHref1" size="40"
                                value=""><%--</div>--%>
                        </li>
                        <hr size='1'>
                        <li>2.图片:
                        <%-- <div style="height: 25px;">
                                <div style="float: left; line-height: 25px;">--%>
                            <uc1:SingleFileUpload ID="sufLoadImg2" name="sfuPhotoImg" runat="server" IsUploadSwf="true" />
                            <%-- </div>
                                <div style="float: left; margin-left: 10px; line-height: 25px;">--%>
                            <asp:Label ID="lblImg2" runat="server" Text=""></asp:Label><%--</div>
                             </div>--%>
                            <input id="hdfoldimgpath2" type="hidden" runat="server" />
                           <%--  <div>--%>
                            &nbsp; 链接:
                            <input type="text" runat="server" name="txtFocusImgHref2" id="txtFocusImgHref2" size="40"
                                value=""><%--</div>--%>
                        </li>
                        <hr size='1'>
                        <li>3.图片:
                           <%-- <div style="height: 25px;">--%>
                              <%--  <div style="float: left; line-height: 25px;">--%>
                                    <uc1:SingleFileUpload ID="sufLoadImg3" name="sfuPhotoImg" runat="server" IsUploadSwf="true" />
                               <%-- </div>
                                <div style="float: left; margin-left: 10px; line-height: 25px;">--%>
                                    <asp:Label ID="lblImg3" runat="server" Text=""></asp:Label><%--</div>
                            </div>--%>
                            <input id="hdfoldimgpath3" type="hidden" runat="server" />
                           <%-- <div>--%>
                                &nbsp; 链接:
                                <input type="text" runat="server" name="txtFocusImgHref3" id="txtFocusImgHref3" size="40"
                                    value=""><%--</div>--%>
                        </li>
                        <hr size='1'>
                        <li>4.图片:
                           <%-- <div style="height: 25px;">
                                <div style="float: left; line-height: 25px;">--%>
                                    <uc1:SingleFileUpload ID="sufLoadImg4" name="sfuPhotoImg" runat="server" IsUploadSwf="true" />
                              <%--  </div>
                                <div style="float: left; margin-left: 10px; line-height: 25px;">--%>
                                    <asp:Label ID="lblImg4" runat="server" Text=""></asp:Label>
                             <%--   </div>
                            </div>--%>
                            <input id="hdfoldimgpath4" type="hidden" runat="server" />
                           <%-- <div>--%>
                                &nbsp; 链接:
                                <input type="text" runat="server" name="txtFocusImgHref4" id="txtFocusImgHref4" size="40"
                                    value=""><%--</div>--%>
                        </li>
                        <hr size='1'>
                        <li>5.图片:
                            <%--<div style="height: 25px;">
                                <div style="float: left; line-height: 25px;">--%>
                                    <uc1:SingleFileUpload ID="sufLoadImg5" name="sfuPhotoImg" runat="server" IsUploadSwf="true" />
                             <%--   </div>
                                <div style="float: left; margin-left: 10px; line-height: 25px;">--%>
                                    <asp:Label ID="lblImg5" runat="server" Text=""></asp:Label>
                              <%--  </div>
                            </div>--%>
                            <input id="hdfoldimgpath5" type="hidden" runat="server" />
                           <%-- <div>--%>
                                &nbsp; 链接:
                                <input type="text" runat="server" name="txtFocusImgHref5" id="txtFocusImgHref5" size="40"
                                    value="">
                          <%--  </div>--%>
                        </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#C0DEF3">
                    &nbsp;
                </td>
                <td height="28">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript">         
        
        var strSFFocusOne, strSFFocusTwo, strSFFocusShree,strSFFocusFour,strSFFocusFive;
        var isPass=true;
        $(function() {
             strSFFocusOne = <%=sufLoadImg1.ClientID %>;
             strSFFocusTwo = <%=sufLoadImg2.ClientID %>;
             strSFFocusShree = <%=sufLoadImg3.ClientID %>;             
             strSFFocusFour = <%=sufLoadImg4.ClientID %>;
             strSFFocusFive = <%=sufLoadImg5.ClientID %>;            
             SWFUpload.WINDOW_MODE = "TRANSPARENT";             

            $("#<%=btnSave.ClientID %>").click(function() {
                var form = $(this).closest("form").get(0);                  
                if (isPass) {
                    if(strSFFocusOne.getStats().files_queued ==0
                    &&strSFFocusTwo.getStats().files_queued ==0
                    &&strSFFocusShree.getStats().files_queued ==0
                    &&strSFFocusFour.getStats().files_queued==0
                    &&strSFFocusFive.getStats().files_queued==0){
                        return true;
                    }
                    UploadLicenceImg();
                    return false;
                }else{
                    return false;
                }
            });
        });
        
       function UploadLicenceImg() {
            if (strSFFocusOne.getStats().files_queued > 0) {
                strSFFocusOne.customSettings.UploadSucessCallback = UploadBusinessSFFocusTwo;
                strSFFocusOne.startUpload();
            } else {
                UploadBusinessSFFocusTwo();
            }
        }
        function UploadBusinessSFFocusTwo() {
            if (strSFFocusTwo.getStats().files_queued > 0) {
                strSFFocusTwo.customSettings.UploadSucessCallback = UploadTaxRegSFFocusShree;
                strSFFocusTwo.startUpload();
            } else {
                UploadTaxRegSFFocusShree();
            }
        }        
        
         function UploadTaxRegSFFocusShree() {
            if (strSFFocusShree.getStats().files_queued > 0) {
                strSFFocusShree.customSettings.UploadSucessCallback = UploadTaxRegSFFocusFour;
                strSFFocusShree.startUpload();
            } else {
                UploadTaxRegSFFocusFour();
            }
        }        
        
         function UploadTaxRegSFFocusFour() {
            if (strSFFocusFour.getStats().files_queued > 0) {
                strSFFocusFour.customSettings.UploadSucessCallback = UploadBusinessSFFocusFive;
                strSFFocusFour.startUpload();
            } else {
                UploadBusinessSFFocusFive();
            }
        }        
        
         function UploadBusinessSFFocusFive() {
            if (strSFFocusFive.getStats().files_queued > 0) {
                strSFFocusFive.customSettings.UploadSucessCallback = UploadSave;
                strSFFocusFive.startUpload();
            } else {
                UploadSave();
            }
        }

        function UploadSave() {
           isPass=true;
           $("#<%=btnSave.ClientID %>").click();
        }
   
    </script>

</body>
</html>
